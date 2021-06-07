using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace KSSG_Toolbox.Services
{
    public interface IPrinter
    {
        List<String> GetInstallesPrinters();

        bool PrintFile(string path, string printer, int copy);
    }
}