using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SWII6_TP01
{
    internal class Books
    {
        public Books()
        {
            Console.WriteLine("=============");
            var author1 = new Author("John Perry", "john@email.com", 'M');
            var author2 = new Author("Stephen King", "stephen@email.com", 'M');
            Author[] authorsBook1 = { author1, author2 };
            var book1 = new Book(" A Arte da Procrastinacao", authorsBook1, 150, 15);

            Console.WriteLine($"autores: {book1.getAuthorNames()}");
            Console.WriteLine($"livro: {book1.ToString()}");
        }
    }
}
