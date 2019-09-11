using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace CubiscanUpload
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string path = @"C:\CubiScan\Export";
            DirectoryMonitor monitor = new DirectoryMonitor(path);
            Console.ReadKey();
                       
        }

       
    }
}


