using System;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace KSSG.Toolbox.Services
{
    public interface IPrintServices
    {
        PrinterSettings.StringCollection GetInstallesPrinters();

        void InitService(string printer, int copy, string paperName);

        bool PrintFile(string path, string printer, int copy, string paperName);
        
        bool PrintFile(string path);
    }
}