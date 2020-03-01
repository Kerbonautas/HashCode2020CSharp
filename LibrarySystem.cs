using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.IO;
using System.Linq;

namespace Hashcode2020CSharp
{
    class LibrarySystem
    {
        public string input { get; set; }
        public string output { get; set; }
        public int[] firstLine { get; set; } //0-Number of books, 1-Number of libraries, 2-Days for the process
        public int[] fl { get; set; } //To create libraries //0-Numbre of books, 1-signup time, 2-Scan books per day

        public List<Book> bs = new List<Book>(); //List with all books with their points
        public List<Library> libraries = new List<Library>(); //List with all libraries

        public LibrarySystem(string input, string output)
        {
            this.input = input;
            this.output = output;

            Thread TRun = new Thread(Run);
            TRun.Start();
        }

        private void Run()
        {
            Reader();

            StringBuilder sb = new StringBuilder();
            int count = 0;

            do
            {
                CalculateFactor();

                libraries = libraries.OrderByDescending(x => x.factor).ToList();

                sb.AppendLine(libraries[0].id + " " + libraries[0].books.Count);

                foreach (Book bk in libraries[0].books)
                {
                    sb.Append(bk.id + " ");
                }

                sb.Remove(sb.Length - 1, 1);
                sb.Append("\n");


                firstLine[2] -= libraries[0].timeSignUp;

                List<Book> booksToRemove = libraries[0].books;
                libraries.Remove(libraries[0]);
                RemoveScanned(booksToRemove);

                count++;
            } while (firstLine[2] > 0 && libraries.Count != 0);

            sb.Insert(0, count + "\n");
            Writer(sb.ToString());
        }

        private void CalculateFactor()
        {
            foreach(Library lb in libraries)
            {
                lb.SetFactor(firstLine[2]);
            }
        }

        private void RemoveScanned(List<Book> scannedBooks)
        {
            foreach (Library lb in libraries)
            {
                lb.RemoveItemsAlreadyScanned(scannedBooks);
            }
        }

        private int[] ParseToIntArray (string[] sourceToParse)
        {
            int[] intArray = Array.ConvertAll<string, int>(sourceToParse, int.Parse);
            return intArray;
        }

        private void parseInitialBooks (string[] sourceToParse)
        {
            //This method receive all book values and generate the list with all pairs of id and value

            int[] intArray = Array.ConvertAll<string, int>(sourceToParse, int.Parse);

            for (int i = 0; i < intArray.Length; i++)
            {
                bs.Add(new Book(i, intArray[i]));
            }
        }

        private List<Book> ParseBooks(string[] sourceToParse)
        {
            //This method receive an string array and return a list with the books referred to the array id's
            int[] intArray = Array.ConvertAll<string, int>(sourceToParse, int.Parse);

            List<Book> returnedBooks = new List<Book>();

            foreach (int i in intArray)
            {
                returnedBooks.Add(bs.Find(x => x.id == i));
            }

            //This will remove duplicatd books and return it
            return returnedBooks.Distinct().ToList();
        }

        #region txt actions
        private void Reader()
        {
            using (StreamReader fileInput = new StreamReader(input))
            {
                try
                {
                    firstLine = ParseToIntArray(fileInput.ReadLine().Split());
                    parseInitialBooks(fileInput.ReadLine().Split());
                } catch (Exception e) { ExceptionHandler.HandleException(e); }
                
                int i = 0;

                string ln;
                Boolean pairOrNot = true;

                while ((ln = fileInput.ReadLine()) != null)
                {
                    if (pairOrNot)
                    {
                        try
                        {
                            fl = ParseToIntArray(ln.Split());
                        } catch (Exception e) { ExceptionHandler.HandleException(e); }

                        pairOrNot = false;
                    }
                    else
                    {
                        try
                        {
                            List<Book> sl = new List<Book>();
                            sl = ParseBooks(ln.Split());
                            libraries.Add(new Library(fl, sl, i));

                            i++;
                        } catch (Exception e) { ExceptionHandler.HandleException(e); }

                        pairOrNot = true;
                    }
                }
            }
            Debug.WriteLine("Beast " + input + " feeded!");
        }
        private void Writer(string outputString)
        {
            using (StreamWriter fileOutput = new StreamWriter(output))
            {
                fileOutput.Write(outputString);
            }
            Debug.WriteLine("Finished :" + output);
        }
        #endregion
    }
}