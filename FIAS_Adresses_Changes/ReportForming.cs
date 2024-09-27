using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System.Drawing;
using System.Xml.Linq;

namespace FIAS_Addresses_Changes
{
    internal class ReportForming
    {
        private static string root = Environment.CurrentDirectory + "\\gar_delta_actual_";

        /// <summary>
        /// Формирует таблицу в Word с помощью библиотеки Spire.Doc
        /// Документ хранится в корне проекта
        /// </summary>
        /// <param name="data"></param>
        /// <param name="date"></param>
        public static void FormWordReport(List<List<MyDataCollection>> data, string date)
        {
            Dictionary<int, string> levelsName = new Dictionary<int, string>();

            DirectoryInfo info = new DirectoryInfo(root + DateTime.Parse(date).ToString("yyyy.MM.dd"));
            string[] files = Directory.GetFiles(info.FullName, "AS_OBJECT_LEVELS*");
            XDocument docXml = XDocument.Load(files[0]);
            XElement? element = docXml.Element("OBJECTLEVELS");
            if (element != null)
            {
                foreach (XElement e in element.Elements("OBJECTLEVEL"))
                {
                    XAttribute? name = e.Attribute("NAME");
                    XAttribute? level = e.Attribute("LEVEL");

                    levelsName.Add(int.Parse(level.Value), name.Value.ToString());
                }
            }

            Spire.Doc.Document doc = new Spire.Doc.Document();

            {
                Section s = doc.AddSection();

                Table head = s.AddTable(true);
                head.ResetCells(1, 1);
                TableRow row = head.Rows[0];
                row.IsHeader = true;
                row.Height = 40;
                Paragraph h = row.Cells[0].AddParagraph();
                row.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                h.Format.HorizontalAlignment = HorizontalAlignment.Center;
                TextRange text = h.AppendText($"Отчет по добавленным адресным объектам за {date}");
                text.CharacterFormat.FontSize = 38;
                text.CharacterFormat.TextColor = Color.Blue;
                text.CharacterFormat.FontName = "Arial";
            }

            string[] Header = {"Тип объекта", "Наименование"};

            for(int i  = 0; i < data.Count; i++) 
            {
                if (data[i].Count == 0)
                    continue;

                Section s = doc.AddSection();

                Table head = s.AddTable(true);
                head.ResetCells(1, 1);
                TableRow row = head.Rows[0];
                row.IsHeader = true;
                row.Height = 30;
                Paragraph h = row.Cells[0].AddParagraph();
                row.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                h.Format.HorizontalAlignment = HorizontalAlignment.Center;
                TextRange text = h.AppendText(levelsName[i + 1]);
                text.CharacterFormat.FontSize = 28;
                text.CharacterFormat.TextColor = Color.Blue;
                text.CharacterFormat.FontName = "Arial";


                Table table = s.AddTable(true);

                table.ResetCells(data[i].Count + 1, Header.Length);

                //Set the first row as table header

                TableRow FRow = table.Rows[0];

                FRow.IsHeader = true;

                //Set the height and color of the first row

                FRow.Height = 24;

                for (int j = 0; j < Header.Length; j++)
                {
                    Paragraph p = FRow.Cells[j].AddParagraph();

                    FRow.Cells[j].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                    p.Format.HorizontalAlignment = HorizontalAlignment.Center;

                    TextRange TR = p.AppendText(Header[j]);

                    TR.CharacterFormat.FontName = "Arial";

                    TR.CharacterFormat.FontSize = 20;

                    TR.CharacterFormat.Bold = true;
                }

                for (int x = 0; x < data[i].Count; x++)
                {
                    TableRow dataRow = table.Rows[x + 1];
                    dataRow.Height = 20;

                    dataRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    dataRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                    Paragraph p2 = dataRow.Cells[0].AddParagraph();
                    p2.Format.HorizontalAlignment = HorizontalAlignment.Center;

                    TextRange text2 = p2.AppendText(data[i][x].Typename);
                    text2.CharacterFormat.FontSize = 18;
                    text2.CharacterFormat.FontName = "Arial";
                    

                    Paragraph p3 = dataRow.Cells[1].AddParagraph();
                    p3.Format.HorizontalAlignment = HorizontalAlignment.Center;

                    TextRange text3 = p3.AppendText(data[i][x].Name);
                    text3.CharacterFormat.FontSize = 18;
                    text3.CharacterFormat.FontName = "Arial";
                }

                doc.SaveToFile($"Report_{date}.docx", FileFormat.Docx2013);
            }
        }
    }
}
