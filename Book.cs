using System;
using System.Collections.Generic;
using System.Text;

namespace Hashcode2020CSharp
{
    class Book : IComparable<Book>
    {
        public int id { get; set; }
        public int value { get; set; }
        public Book(int id, int value)
        {
            this.id = id;
            this.value = value;
        }

        public int CompareTo(Book element)
        {
            return element.value - value;
        }
    }
}