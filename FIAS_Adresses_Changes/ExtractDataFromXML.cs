using System.Xml.Linq;

namespace FIAS_Addresses_Changes
{
    internal class ExtractDataFromXML
    {
        private static string root = Environment.CurrentDirectory + "\\gar_delta_actual_";

        /// <summary>
        /// Метод проходит по указанной директории, открывая каждую папку в поиске файла с заданным именем. 
        /// В каждом найденном файле извекает нужные атрибуты. 
        /// </summary>
        /// <param name="endOfPath"></param>
        /// <returns>Возвращает список списков, где индекс внутреннего списка соответсвует его LEVEL - 1</returns>
        public static List<List<MyDataCollection>> Extract(string endOfPath)
        {
            DirectoryInfo info = new DirectoryInfo(root + DateTime.Parse(endOfPath).ToString("yyyy.MM.dd"));
            List<List<MyDataCollection>> data = new List<List<MyDataCollection>> ();

            for (int i = 0; i < 17; i++)
            {
                data.Add(new List<MyDataCollection>());
            }

            foreach (var item in info.GetDirectories())
            {
                string[] files = Directory.GetFiles(item.FullName, "AS_ADDR_OBJ_2*");
                XDocument doc = XDocument.Load(files[0]);
                XElement? element = doc.Element("ADDRESSOBJECTS");
                if (element != null) 
                {
                    foreach (XElement e in element.Elements("OBJECT"))
                    {
                        string typename = e.Attribute("TYPENAME").Value.ToString();
                        string name = e.Attribute("NAME").Value.ToString();
                        int level = int.Parse(e.Attribute("LEVEL").Value.ToString());
                        string isActive = e.Attribute("ISACTIVE").Value.ToString();

                        if (name.Contains(":"))
                        {
                            continue;
                        }

                        MyDataCollection newData = new MyDataCollection(name, typename, level);

                        if (isActive == "1")
                        {
                            data[level - 1].Add(newData);
                        }
                    }
                }
            }

            foreach (var item in data) 
            {
                if (item.Count > 1)
                {
                    item.Sort();
                }
            }

            foreach (var item in data) //Удаление дубликатов
            {
                if(item.Count > 1)
                {
                    int j = 0;
                    while(j < item.Count - 1)
                    {
                        if (item[j].Name == item[j+1].Name && item[j].Typename == item[j+1].Typename)
                        {
                            item.Remove(item[j]);
                            j--;
                        }
                        j++;
                    }
                }
            }

            return data;
        }
    }
}
