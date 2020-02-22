using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.IO;

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
            CalculateFactor();

            Writer();
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
                firstLine = Parsing(fileInput.ReadLine().Split());
                bs = Parsing(fileInput.ReadLine().Split());

                int i = 0;

                string ln;
                Boolean pairOrNot = true;

                while ((ln = fileInput.ReadLine()) != null)
                {
                    if (pairOrNot)
                    {
                        fl = Parsing(ln.Split());

                        pairOrNot = false;
                    }
                    else
                    {
                        sl = Parsing(ln.Split());
                        libraries.Add(new Library(fl, sl, i));
                        i++;

                        pairOrNot = true;
                    }
                }
            }
            Debug.WriteLine("Beast " + input + " feeded!");
        }
        private void Writer()
        {

        }
        #endregion
    }
}