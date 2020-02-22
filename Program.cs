using System;
using System.Threading;

namespace Hashcode2020CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            LibrarySystem A = new LibrarySystem(@".\GoogleFiles\a_example.txt", @".\GoogleFiles\OutputA.txt");
            //LibrarySystem B = new LibrarySystem(@".\GoogleFiles\b_read_on.txt", @".\GoogleFiles\OutputB.txt");
            //LibrarySystem C = new LibrarySystem(@".\GoogleFiles\c_incunabula.txt", @".\GoogleFiles\OutputC.txt");
            //LibrarySystem D = new LibrarySystem(@".\GoogleFiles\d_tough_choices.txt", @".\GoogleFiles\OutputD.txt");
            //LibrarySystem E = new LibrarySystem(@".\GoogleFiles\e_so_many_books.txt", @".\GoogleFiles\OutputE.txt");
            //LibrarySystem F = new LibrarySystem(@".\GoogleFiles\f_libraries_of_the_world.txt", @".\GoogleFiles\OutputF.txt");
            Console.WriteLine("Pulse a key to end program");
            Console.ReadKey();
        }
    }
}
