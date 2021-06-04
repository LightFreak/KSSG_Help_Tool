using System;
using System.Collections.Generic;
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
            String folder = "H:\\Daten\\Downloads\\Incident\\";
            String filetype = "*.pdf";
            String printer = "\\\\print1.sg.hcare.ch\\P-001-722545-Mono";
            var watcher = new FileSystemWatcher(folder);

            Console.WriteLine("Willkommen zu Fabians Hilfs Sklave...");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine($"Überwachter Ordner: {folder}");
            Console.WriteLine($"Überwachter Dateityp: {filetype}");
            Console.WriteLine($"Drucker: {printer}");

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

            watcher.Filter = filetype;
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();

        }

        //private static void OnChanged(object sender, FileSystemEventArgs e)
        //{
        //    if (e.ChangeType != WatcherChangeTypes.Changed)
        //    {
        //        return;
        //    }
        //    Console.WriteLine($"Changed: {e.FullPath}");
        //}

        //private static void OnCreated(object sender, FileSystemEventArgs e)
        //{
        //    string value = $"Created: {e.FullPath}";
        //    Console.WriteLine(value);
        //}

        private static void OnDeleted(object sender, FileSystemEventArgs e) =>
            Console.WriteLine($"Deleted: {e.FullPath}");

        private static void OnRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"Renamed:");
            Console.WriteLine($"    Old: {e.OldFullPath}");
            Console.WriteLine($"    New: {e.FullPath}");
            PrinterHelper p = new PrinterHelper();
            p.PrintPDF("\\\\print1.sg.hcare.ch\\P-001-722545-Mono", "A4 (210 x 297 mm)", e.FullPath, 1);
            File.Delete(e.FullPath);
        }

        private static void OnError(object sender, ErrorEventArgs e) =>
            PrintException(e.GetException());

        private static void PrintException(Exception ex)
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
}
