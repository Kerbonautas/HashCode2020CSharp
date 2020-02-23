﻿using System;
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
        public int[] firstLine { get; set; }
        public int[] fl { get; set; }
        public int[] sl { get; set; }
        public int[] bs { get; set; }
        public List<Library> libraries = new List<Library>();

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

            SortByValue();

            do
            {
                CalculateFactor();
                int idMax = 0;
                double max = 0;

                foreach (Library lb in libraries)
                {
                    if (max < lb.factor)
                    {
                        idMax = lb.id;
                        max = lb.factor;
                    }
                }

                Library maxFactorLibrary = libraries.Find(x => x.id == idMax);

                sb.AppendLine(maxFactorLibrary.id + " " + maxFactorLibrary.books.Length);

                foreach (int i in maxFactorLibrary.books)
                {
                    sb.Append(i + " ");
                }

                sb.Remove(sb.Length - 1, 1);
                sb.Append("\n");

                libraries.Remove(maxFactorLibrary);
                firstLine[2] -= maxFactorLibrary.timeSignUp;

                RemoveScanned(maxFactorLibrary.books);

                count++;
            } while (firstLine[2] > 0 && libraries.Count != 0);

            sb.Insert(0, count + "\n");
            Writer(sb.ToString());
        }

        private void SortByValue()
        {
            foreach (Library lb in libraries)
            {
                List<Book> books = new List<Book>();
                int[] i = lb.books;
                foreach(int j in i)
                {
                    books.Add(new Book(j, bs[j]));
                }
                books.Sort();
                int[] ret = new int[books.Count];
                int k = 0;
                foreach (Book bk in books)
                {
                    ret[k] = bk.id;
                    k++;
                }

                lb.books = ret;
            }
        }

        private void RemoveScanned(int[] booksScanned)
        {
            foreach (Library lb in libraries)
            {
                lb.RemoveItemsAlreadyScanned(booksScanned);
            }
        }
        private int[] Parsing (string[] sourceToParse)
        {
            int[] intArray = Array.ConvertAll<string, int>(sourceToParse, int.Parse);
            return intArray;
        }

        private void CalculateFactor()
        {
            foreach (Library lb in libraries)
            {
                int result = 0;
                foreach (int i in lb.books)
                {
                    result += bs[i];
                }
                lb.points = result;
                lb.SetFactor();
            }
        }
        private int[] SetBooksArray(int[]rawBooks)
        {
            List<Book> books = new List<Book>();
            
            foreach(int book in rawBooks.Distinct().ToArray())
            {
                books.Add(new Book(book, bs[book]));
            }

            books = books.OrderBy(x => x.value).ToList();

            int[] booksSorted = new int[books.Count];
            int i = 0;
            foreach (Book book in books)
            {
                booksSorted[i] = book.id;
                i++;
            }
            return booksSorted;
        }

        #region txt actions
        private void Reader()
        {
            using (StreamReader fileInput = new StreamReader(input))
            {
                try
                {
                    firstLine = Parsing(fileInput.ReadLine().Split());
                    bs = Parsing(fileInput.ReadLine().Split());
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
                            fl = Parsing(ln.Split());
                        } catch (Exception e) { ExceptionHandler.HandleException(e); }

                        pairOrNot = false;
                    }
                    else
                    {
                        try
                        {
                            sl = Parsing(ln.Split());
                            sl = SetBooksArray(sl);
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