using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfiumViewer;
using System.Drawing.Printing;

namespace KSSG_Help_Tool
{
    class PrinterHelper
    {
        
        public bool PrintPDF(
            string printer,
            string paperName,
            string filename,
            int copies)
        {
            try
            {
                // Create the printer settings for our printer
                var printerSettings = new PrinterSettings
                {
                    PrinterName = printer,
                    Copies = (short)copies,
                };

                // Create our page settings for the paper size selected
                var pageSettings = new PageSettings(printerSettings)
                {
                    Margins = new Margins(0, 0, 0, 0),
                };
                foreach (PaperSize paperSize in printerSettings.PaperSizes)
                {
                    if (paperSize.PaperName == paperName)
                    {
                        pageSettings.PaperSize = paperSize;
                        break;
                    }
                }

                // Now print the PDF document
                using (var document = PdfDocument.Load(filename))
                {
                    using (var printDocument = document.CreatePrintDocument())
                    {
                        printDocument.PrinterSettings = printerSettings;
                        printDocument.DefaultPageSettings = pageSettings;
                        printDocument.PrintController = new StandardPrintController();
                        printDocument.Print();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
