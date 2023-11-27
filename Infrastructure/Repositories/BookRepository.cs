using Core.Entities;
using Core.Repositories;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        static readonly HttpClient client = new HttpClient();
        private readonly IConfiguration _configuration;

        public BookRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Book> GetByISBN(string ISBN)
        {
            string responseBody = await client.GetStringAsync($"{_configuration["OPEN_LIBRARY_API_URL"]}/books?bibkeys=ISBN:{ISBN}&jscmd=data&format=json");
            dynamic json = JObject.Parse(responseBody);

            Book book;

            book = JsonConvert.DeserializeObject<Book>(JsonConvert.SerializeObject(json[$"ISBN:{ISBN}"]));

            if (book == null)
            {
                throw new Exception("An error has occured, please check your uploaded file.");
            }
            book.ISBN = ISBN;

            return book;
        }
    }
}
