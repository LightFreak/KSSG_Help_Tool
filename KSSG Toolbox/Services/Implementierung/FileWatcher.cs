using System;
using System.IO;

namespace KSSG.Toolbox.Services.Implementierung
{
    public class FileWatcher : IFileWatcher
    {
        private IPrintServices _print;
        public bool start(string folder, string filetype, IPrintServices print)
        {
            _print = print;
            FileSystemWatcher watcher = new FileSystemWatcher(folder);

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

            watcher.Filter = _filetype;
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
        }

        public bool stop(string folder)
        {
            throw new System.NotImplementedException();
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            
            _print.PrintFile(e.FullPath,)
            File.Delete(e.FullPath);
        }
    }

    private void 

}