using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Net;

namespace CubiscanUpload
{
    class DirectoryMonitor
    {
        public string path { get; set; }

        public DirectoryMonitor(string path)
        {
            this.path = path;
            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
            fileSystemWatcher.Path = path;
            fileSystemWatcher.Created += FileSystemWatcher_Created;
            fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;
            fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;
            fileSystemWatcher.EnableRaisingEvents = true;
        }

        public void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)

        {
            //Console.WriteLine("File created: {0}", e.Name);
            List<string> lines = new List<string>();

            using (TextFieldParser parser = new TextFieldParser(e.FullPath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    lines.Add(parser.ReadLine());
                    //Processing row                
                }
            }

            string[] fields = lines[1].Split(',');
            string itemname = fields[0];
            string length = fields[3];
            string width = fields[4];
            string height = fields[5];
            string weight = fields[6];

            Console.WriteLine("Item: " + itemname);
            Console.WriteLine("Length: " + length);
            Console.WriteLine("Width: " + width);
            Console.WriteLine("Height: " + height);
            Console.WriteLine("Weight: " + weight);
            Console.WriteLine("");

            Item item = new Item(itemname);

            if (item.getItemId())
            {
                if(item.updateItemDimensions(length, width, height, weight))
                {
                    Console.WriteLine(itemname + " dimensions updated successfully.\n");
                    string sourceFile = e.FullPath;
                    string destinationFile = @"C:\CubiScan\Completed Imports\" + e.Name;
                    File.Move(sourceFile, destinationFile);
                }
                else
                {
                    Console.Write("Could not update item dimensions. Possibly multiple items with the same UPC. Try again.\n\n");
                }
         
            }
            else
            {
                Console.WriteLine("Try using SKU instead of UPC or vice versa.\n");
            }
   
        }

        public void FileSystemWatcher_Renamed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File renamed: {0}", e.Name);
        }

        public void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            //Console.WriteLine(e.Name+" moved to Completed Imports.");
        }


    }
}
