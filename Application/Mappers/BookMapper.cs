using Application.Responses;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enumerations;

namespace Application.Mappers
{
    public static class BookMapper
    {
        public static BookResponse Map(Book book)
        {
            var newBook = new BookResponse
            {
                Title = book.Title,
                SubTitle = string.IsNullOrEmpty(book.SubTitle) ? "N/A" : book.SubTitle,
                NumberPages = string.IsNullOrEmpty(book.Number_of_pages) ? "N/A" : book.Number_of_pages,
                PublishDate = book.Publish_date,
                ISBN = book.ISBN
            };

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < book.Authors.Count; i++)
            {
                if (i < book.Authors.Count - 1)
                    sb.Append(book.Authors[i].Name + ";");
                else
                    sb.Append(book.Authors[i].Name);
            }

            newBook.Authors = sb.ToString();
            return newBook;
        }

        public static BookResponse Map(BookResponse book)
        {
            return new BookResponse
            {
                Type = BookTypes.Cache,
                Title = book.Title,
                SubTitle = book.SubTitle,
                NumberPages = book.NumberPages,
                PublishDate = book.PublishDate,
                ISBN = book.ISBN,
                Authors = book.Authors
            };
        }
    }
}
