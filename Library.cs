using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hashcode2020CSharp
{
    class Library
    {
        public int numberOfBooks { get; set; }
        public int timeSignUp { get; set; }
        public int timeScan { get; set; }
        public int booksPerDay { get; set; }
        public int id { get; set; }
        public int[] books { get; set; } //TODO: delete duplicates in constructor
        public int points { get; set; }
        public double factor { get; set; }

        public Library(int[] fl, int[] sl, int id)
        {
            numberOfBooks = fl[0];
            timeSignUp = fl[1];
            booksPerDay = fl[2];
            points = 0;
            this.id = id;

            books = sl.Distinct().ToArray();
        }

        public void SetFactor()
        {
            timeScan = (int)Math.Round((double)books.Length / booksPerDay, 0, MidpointRounding.AwayFromZero);
            factor = points / (timeSignUp + timeScan);
        }
    }
}