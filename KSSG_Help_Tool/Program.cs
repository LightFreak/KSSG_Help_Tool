using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSSG_Help_Tool
{
    class Program
    {
        static void Main(string[] args)
        {
            var pt = new Printer();
            String folder = "H:\\Daten\\Downloads\\Incident\\";
            String filetype = "*.pdf";
            String filter1 = "Print*.pdf";
            String filter2 = "Service*.pdf";
            Collection<string> filters = new Collection<string>();
            filters.Add(filter1);
            filters.Add(filter2);
            String printer = pt.GetDefaultPrinter();
            
            var x = new PrintFile(printer,filetype,folder,filters);
            Console.WriteLine("Willkommen zu Fabians Hilfs Sklave...");
            Console.WriteLine("-------------------------------------");
            foreach (string filter in filters)
            {
                Console.WriteLine($"Überwachter Dateityp: {filter}");
            }
            Console.WriteLine($"Überwachter Ordner: {folder}");
            Console.WriteLine($"Drucker: {printer}");

           

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();

        }

        

       
    }

    public class PrintFile
    {
        private string _printer;
        private string _filetype;
        private string _folder;
        private string _papersize;
        
        private PrinterHelper _p;
        private FileSystemWatcher watcher;
        private Collection<string> _filtercollection;


        public PrintFile(string printer, string filetype, string folder, Collection<string> filters)
        {
            _printer = printer;
            _filetype = filetype;
            _folder = folder;
            _filtercollection = filters;
            _papersize = "A4(210 x 297 mm)";
            _p = new PrinterHelper();
            InitWatcher();
        }

        public bool PrintPDF(RenamedEventArgs e)
        {
            return _p.PrintPDF(_printer, _papersize, e.FullPath, 1);
        }

        private void InitWatcher()
        {
            foreach (var filter in _filtercollection)
            {
                watcher = new FileSystemWatcher(_folder);
                watcher.NotifyFilter = NotifyFilters.Attributes
                                       | NotifyFilters.CreationTime
                                       | NotifyFilters.DirectoryName
                                       | NotifyFilters.FileName
                                       | NotifyFilters.LastAccess
                                       | NotifyFilters.LastWrite
                                       | NotifyFilters.Security
                                       | NotifyFilters.Size;

                watcher.Deleted += OnDeleted;
                watcher.Renamed += OnRenamed;
                watcher.Error += OnError;

                watcher.Filter = filter;
                watcher.IncludeSubdirectories = true;
                watcher.EnableRaisingEvents = true;
            }
            
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"Renamed:");
            Console.WriteLine($"    Old: {e.OldFullPath}");
            Console.WriteLine($"    New: {e.FullPath}");
            Console.WriteLine("Print PDF");
            PrintPDF(e);
            File.Delete(e.FullPath);
        }

        private void OnDeleted(object sender, FileSystemEventArgs e) =>
            Console.WriteLine($"Deleted: {e.FullPath}");

        public void OnError(object sender, ErrorEventArgs e) => PrintException(e.GetException());

        private void PrintException(Exception ex)
        {
            if (ex != null)
            {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine("Stacktrace:");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                PrintException(ex.InnerException);
            }
        }
    }

    public class Printer
    {
        public string GetDefaultPrinter()
        {
            var res = "";
            PrinterSettings settings = new PrinterSettings();
            if (settings.IsDefaultPrinter)
            {
                return settings.PrinterName;
            }

            return "No Default Printer";
        }
    }
}
