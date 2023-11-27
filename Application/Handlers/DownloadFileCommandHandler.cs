using Application.Commands;
using Application.Responses;
using Core.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Handlers
{
    public class DownloadFileCommandHandler : IRequestHandler<DownloadFileCommand, byte[]>
    {
        private readonly IMemoryCache _memoryCache;

        public DownloadFileCommandHandler(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<byte[]> Handle(DownloadFileCommand request, CancellationToken cancellationToken)
        {
            var cachedBooks = (List<BookResponse>)_memoryCache.Get("books");

            return CreateCSVTextFile(cachedBooks);
        }

        private byte[] CreateCSVTextFile(List<BookResponse> books)
        {
            using (var ms = new MemoryStream())
            {
                using (TextWriter tw = new StreamWriter(ms))
                {
                    foreach (BookResponse book in books)
                    {
                        tw.WriteLine($"{book.RowNumber},{book.Type},{book.ISBN},{book.Title},{book.SubTitle},{book.Authors},{book.NumberPages},{book.PublishDate}");
                    }
                    tw.Flush();
                    ms.Position = 0;
                    return ms.ToArray();
                }
            }
        }
    }
}
