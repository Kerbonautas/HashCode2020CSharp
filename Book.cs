using System;
using System.Collections.Generic;
using System.Text;

namespace Hashcode2020CSharp
{
    class Book
    {
        public int id { get; set; }
        public int value { get; set; }
        public Book(int id, int value)
        {
            this.id = id;
            this.value = value;
        }
    }
}
