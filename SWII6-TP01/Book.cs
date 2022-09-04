using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWII6_TP01
{
    internal class Book
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public Author[] Authors { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }

        public Book(string name, Author[] author, double price)
        {
            Name = name;
            Authors = author;
            Price = price;
            Qty = 0;

        }
        public Book(string name, Author[] author, double price, int qty)
        {
            Name = name;
            Authors = author;
            Price = price;
            Qty = qty;
        }

        public override string ToString()
        {

            string list = "";

            foreach (Author author in Authors)
            {
                list += $"[name={author.Name},email={author.Email},gender={author.Gender}]";

                if (author != Authors.Last())
                {
                    list += ",";
                }
            }

            string stringFormatada = "Book[" + Name + ",authors={Authors" + list + "},price=" + Price + ",qty=" + Qty + "]";

            return stringFormatada;
        }
        public string getAuthorNames()
        {
            string result = "";

            foreach (Author author in Authors)
            {
                result += author.Name;

                if (author != Authors.Last())
                {
                    result += ",";
                }
            }
            return result;

        }
    }
}
