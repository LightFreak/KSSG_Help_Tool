namespace KSSG.Toolbox.Services
{
    public interface IFileWatcher
    {
        bool start(string folder, string filetype);

        bool stop(string folder);
    }
}