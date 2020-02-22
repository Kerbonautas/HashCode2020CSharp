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

        #region txt actions
        private void Reader()
        {
            using (StreamReader fileInput = new StreamReader(input))
            {
                try
                {
                    firstLine = Parsing(fileInput.ReadLine().Split());
                    bs = Parsing(fileInput.ReadLine().Split());
                } catch (Exception e) { }
                
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
                        } catch (Exception e) { }

                        pairOrNot = false;
                    }
                    else
                    {
                        try
                        {
                            sl = Parsing(ln.Split());
                            libraries.Add(new Library(fl, sl, i));
                            i++;
                        } catch (Exception e) { }

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