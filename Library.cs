using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hashcode2020CSharp
{
    class Library
    {
        public int timeSignUp { get; set; }
        public int timeScan { get; set; } // Time needed to scan all books
        public int booksPerDay { get; set; }
        public int id { get; set; }
        public List<Book> books { get; set; }
        public int points { get; set; }
        public double factor { get; set; }

        public Library(int[] fl, List<Book> sl, int id)
        {
            timeSignUp = fl[1];
            booksPerDay = fl[2];
            this.id = id;

            books = sl.OrderByDescending(x => x.value).ToList();
            SetTimeScan();
            CalculatePoints();
        }

        public void SetTimeScan() { timeScan = (int)Math.Round((double)books.Count / booksPerDay, 0, MidpointRounding.AwayFromZero); }
        public void SetTimeScan(int days) { timeScan = days; }
        public void CalculatePoints()
        {
            points = 0;

            foreach(Book b in books)
            {
                points += b.value;
            }
        }
        public void SetFactor(int daysLeft)
        {
            /*
             * First case: this library have time enough to send all books, library will set it factor with all books
             * Second case: this library don't have time enough to send all books, library will set it factor with the books it can send and delete the rest of books
             * Third case: this library use all time left with signup process, factor will be 0 in order to became the last library
             */

            if(daysLeft > timeSignUp + timeScan)
            {
                CalculatePoints();
                factor = points / (timeSignUp + timeScan);
            } else if(daysLeft > timeSignUp)
            {
                SetTimeScan(daysLeft - timeSignUp);

                int maxSendBooks = timeScan * booksPerDay;

                if (maxSendBooks < books.Count)
                {
                    books.RemoveRange(maxSendBooks, books.Count - maxSendBooks);
                }

                CalculatePoints();

                factor = points / (timeSignUp + timeScan);
                
            } else
            {
                factor = 0;
            }
        }

        public void RemoveItemsAlreadyScanned(List<Book> alreadyScanned) { foreach (Book bk in alreadyScanned) books.Remove(books.Find(x => x.id == bk.id)); }
    }
}