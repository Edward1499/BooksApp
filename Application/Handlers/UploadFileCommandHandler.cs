using Application.Commands;
using Application.Mappers;
using Application.Responses;
using Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Core.Enumerations;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Handlers
{
    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, List<BookResponse>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMemoryCache _memoryCache;

        public UploadFileCommandHandler(
            IBookRepository bookRepository,
            IMemoryCache memoryCache)
        {
            _bookRepository = bookRepository;
            _memoryCache = memoryCache;
        }

        public async Task<List<BookResponse>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(request.File.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(await reader.ReadLineAsync());
            }

            var newText = result.ToString().ReplaceLineEndings(",");
            string[] isbnNumbers = newText.Split(",").Where(x => !string.IsNullOrEmpty(x)).ToArray();

            var books = new List<BookResponse>();
            var cachedBooks = (List<BookResponse>)_memoryCache.Get("books");
            int rowNumber = 0;

            foreach (var isbn in isbnNumbers)
            {
                rowNumber++;
                BookResponse book;
                var existingBook = books.FirstOrDefault(x => x.ISBN == isbn);
                var cachedBook = cachedBooks?.FirstOrDefault(x => x.ISBN == isbn);

                if (existingBook != null)
                {
                    book = BookMapper.Map(existingBook);

                } 
                else if (cachedBook != null)
                {
                    book = BookMapper.Map(cachedBook);
                }
                else
                {
                    book = BookMapper.Map(await _bookRepository.GetByISBN(isbn));
                    book.Type = BookTypes.Server;
                    
                }

                book.RowNumber = rowNumber;
                books.Add(book);
            }

            _memoryCache.Set("books", books, TimeSpan.FromDays(1));

            return books;
        }
    }
}
