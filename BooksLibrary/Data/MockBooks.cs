using BooksLibrary.Data.Models;
using BooksLibrary.Data.Repos;
using BooksLibrary.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksLibrary.Data
{
    public static class MockBooks
    {
        public static async Task InitDB(ApplicationDbContext context)
        {
            BooksRepo booksRepo = new BooksRepo(context);
            List<Book> books = await booksRepo.GetAll();


            foreach (var book in GetBooks())
            {
                if (!books.Contains(book))
                {
                    await booksRepo.Add(book);
                }  
            }
        }

        private static List<Book> GetBooks()
        {
            return new List<Book>()
            {
                new Book
                {
                    Id = new Guid(),
                    Title = "Joyland",
                    Author = "Stephen King",
                    ReleaseDate = new DateTime(2013, 4, 6),
                    Description = "Powieść kryminalna Stephena Kinga z 2013 roku. Jej akcja rozgrywa się latem 1973 roku w małym miasteczku w Karolinie Północnej. Głównym bohaterem jest Devin Jones, uczeń college'u, który przyjeżdża do lokalnego parku rozrywki, by tam podjąć sezonowe zatrudnienie. Na miejscu staje twarzą w twarz ze zbrodnią i musi zmierzyć się z fatum umierającego dziecka."
                },
                new Book
                {
                    Id = new Guid(),
                    Title = "A Game of Thrones",
                    Author = "George R. R. Martin",
                    ReleaseDate = new DateTime(1996, 8, 1),
                    Description = "Powieść z gatunku fantasy, pierwszy tom sagi Pieśń lodu i ognia George’a R.R. Martina. Pierwsze wydanie w języku angielskim miało premierę 1 sierpnia 1996 roku. Polski przekład ukazał się w roku 1998 nakładem wydawnictwa Zysk i S-ka. Akcja książki toczy się w fikcyjnym świecie na kontynentach Westeros i Essos, gdzie pory roku mogą trwać wiele lat."
                },
                new Book
                {
                    Id = new Guid(),
                    Title = "The Lord of the Rings",
                    Author = "J. R. R. Tolkien",
                    ReleaseDate = new DateTime(1954, 7, 29),
                    Description = "Powieść high fantasy J.R.R. Tolkiena, której akcja rozgrywa się w mitologicznym świecie Śródziemia. Jest ona kontynuacją innej powieści tego autora zatytułowanej Hobbit, czyli tam i z powrotem."
                }
            };
        }

    }
}
