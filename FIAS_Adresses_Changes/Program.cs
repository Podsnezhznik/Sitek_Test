using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO;
using System.Reflection.Metadata;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;


namespace FIAS_Addresses_Changes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var info = UpdadeServices.GetLastDownloadFileInfo();

            var data = ExtractDataFromXML.Extract(info.Date);

            ReportForming.FormWordReport(data, info.Date);

            UpdadeServices.GetAllDownloadFileInfo();
        }
    }
}
