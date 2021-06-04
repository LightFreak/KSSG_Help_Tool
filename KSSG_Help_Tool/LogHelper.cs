using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSSG_Help_Tool
{
    class LogHelper
    {
        public void writeLog(string path,string filename, string log)
        {

            // To do:
            // - Log schreiben als Json, XML oder Txt
            // - Strukturiert
            // Ausgabe:
            // Allfällige Fehler
            // Ev. Normale Operations
            File.WriteAllText(path,log);




        }
    }
}
