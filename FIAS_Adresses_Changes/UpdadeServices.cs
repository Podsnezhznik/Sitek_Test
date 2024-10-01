using System.IO;
using System.Text.Json;
using System.IO.Compression;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace FIAS_Addresses_Changes
{
    public class UpdadeServices
    {
        static string root = Environment.CurrentDirectory;
        static string zipFile = "gar_delta_actual.zip";
        static string unzipFolder = "gar_delta_actual";

        /// <summary>
        /// Формирует объект DownloadFileInfo из полученной даты выгрузки
        /// </summary>
        /// <returns>Загруженная и распакованные папки хранятся в корне проекта</returns>
        public static DownloadFileInfo GetLastDownloadFileInfo()
        {

            string version = DownloadAndUnzip();

            DownloadFileInfo fileInfo = new DownloadFileInfo();

            fileInfo.SetUrlDate(version);

            fileInfo.SetDatesAndVersion(version);

            return fileInfo;
        }

        private static readonly JsonSerializerOptions options = new JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All), // Вот эта строка Вам поможет с кодировкой
            WriteIndented = true
        };

        /// <summary>
        /// Метод ищет все сохраненные папки с ГАР таблицами, и на основе их версии формирует DownloadFileInfo
        /// </summary>
        /// <returns>Json файл хранится в корне проекта</returns>
        public static List<DownloadFileInfo> GetAllDownloadFileInfo()
        {
            List<DownloadFileInfo> list = new List<DownloadFileInfo>();

            string[] dirs = Directory.GetDirectories(root, "gar_delta_actual*");

            for(int i = dirs.Length - 1; i >= 0; i--) //(string dir in dirs) 
            {
                var file = Directory.GetFiles(dirs[i], "version*");
                using (StreamReader reader = new StreamReader(file[file.Length - 1]))
                {
                    list.Add(new DownloadFileInfo(reader.ReadLine()));
                }
            }

            using (FileStream fileStream = new FileStream("GetAllDownloadFileInfo.json", FileMode.OpenOrCreate))
            {
                JsonSerializer.SerializeAsync<List<DownloadFileInfo>>(fileStream, list, options);
            }

            return list;
        }

        /// <summary>
        /// Метод скачивает актуальный архив поссылке, распаковывает его, далее либо переименовывает распакованную директорию, добавляя дату выгрузки, либо удаляет ее.
        /// </summary>
        /// <returns>Загруженная и распакованные папки хранятся в корне проекта</returns>
        private static string DownloadAndUnzip()
        {
            using (var client = new WebClient())
            {
                Console.WriteLine("Загрузка архива...");

                client.DownloadFile("https://fias.nalog.ru/Public/Downloads/Actual/gar_delta_xml.zip", zipFile);

                Console.WriteLine("Актуальный архив загружен");

                Console.WriteLine("Распаковка архива...");

                ZipFile.ExtractToDirectory(root + "\\" + zipFile, root + "\\" + unzipFolder);

                Console.WriteLine("Актуальный архив распакован");
            }

            string path = root + "\\" + unzipFolder + "\\version.txt";
            string version = string.Empty;

            using (StreamReader reader = new StreamReader(path))
            {
                version = reader.ReadLine();
            }

            if (!Directory.Exists(root + "\\" + unzipFolder + $"_{version}"))
            {
                Directory.Move(root + "\\" + unzipFolder, root + "\\" + unzipFolder + $"_{version}");
            }
            else
            {
                Directory.Delete(root + "\\" + unzipFolder, true);
            }

            return version;
        }
    }
}
