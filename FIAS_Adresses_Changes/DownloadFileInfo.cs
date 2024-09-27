namespace FIAS_Addresses_Changes
{
    public class DownloadFileInfo
    {
        private int versionId;

        /// <summary>
        /// Идентификатор	версии	(в прямых выгрузках дата выгрузки вида yyyyMMdd)
        /// </summary>
        public int VersionId
        {
            get { return versionId; }
            set { versionId = value; }
        }

        private string? textVersion;

        /// <summary>
        /// Описание версии файла в текстовом виде
        /// </summary>
        public string? TextVersion
        {
            get { return textVersion; }
            set { textVersion = value; }
        }

        private string? fiasCompleteDbfUrl;

        /// <summary>
        /// URL полной версии ФИАС в формате DBF
        /// </summary>
        public string? FiasCompleteDbfUrl
        {
            get { return fiasCompleteDbfUrl; }
            set { fiasCompleteDbfUrl = value; }
        }

        private string? fiasCompleteXmlUrl;

        /// <summary>
        /// URL полной версии ФИАС в формате XML
        /// </summary>
        public string? FiasCompleteXmlUrl
        {
            get { return fiasCompleteXmlUrl; }
            set { fiasCompleteXmlUrl = value; }
        }

        private string? fiasDeltaDbfUrl;

        /// <summary>
        /// URL дельта версии ФИАС в формате DBF
        /// </summary>
        public string? FiasDeltaDbfUrl
        {
            get { return fiasDeltaDbfUrl; }
            set { fiasDeltaDbfUrl = value; }
        }

        private string? fiasDeltaXmlUrl;

        /// <summary>
        /// URL дельта версии ФИАС в формате XML
        /// </summary>
        public string? FiasDeltaXmlUrl
        {
            get { return fiasDeltaXmlUrl; }
            set { fiasDeltaXmlUrl = value; }
        }

        private string? kladr4ArjUrl;

        /// <summary>
        /// URL	версии КЛАДР 4 сжатого в формате ARJ
        /// </summary>
        public string? Kladr4ArjUrl
        {
            get { return kladr4ArjUrl; }
            set
            { 
                kladr4ArjUrl = value; 
            }
        }

        private string? kladr47ZUrl;

        /// <summary>
        /// URL версии КЛАДР 4 сжатого в формате 7Z
        /// </summary>
        public string? Kladr47ZUrl
        {
            get { return kladr47ZUrl; }
            set 
            {
                kladr47ZUrl = value;
            }
        }

        private string? garXMLFullURL;

        /// <summary>
        /// URL полной версии ГАР в формате XML сжатого в zip
        /// </summary>
        public string? GarXMLFullURL
        {
            get { return garXMLFullURL; }
            set { garXMLFullURL = value; }
        }

        private string? garXMLDeltaURL;

        /// <summary>
        /// URL дельта версии ГАР в формате XML сжатого в zip
        /// </summary>
        public string? GarXMLDeltaURL
        {
            get { return garXMLDeltaURL; }
            set { garXMLDeltaURL = value; }
        }

        private string? expDate;

        /// <summary>
        /// Дата экспорта (yyyy-MM-ddTHH24:MI:SS)
        /// </summary>
        public string? ExpDate
        {
            get { return expDate; }
            set { expDate = value; }
        }

        private string? date;

        /// <summary>
        /// Дата выгрузки (dd.MM.yyyy)
        /// </summary>
        public string? Date
        {
            get { return date; }
            set { date = value; }
        }

        public DownloadFileInfo()
        {
            FiasCompleteDbfUrl = string.Empty;
            FiasCompleteXmlUrl = string.Empty;
            FiasDeltaDbfUrl = string.Empty;
            FiasDeltaXmlUrl = string.Empty;
        }

        public DownloadFileInfo(string version)
        {
            SetDatesAndVersion(version);
            SetUrlDate(version);

            FiasCompleteDbfUrl = string.Empty;
            FiasCompleteXmlUrl = string.Empty;
            FiasDeltaDbfUrl = string.Empty;
            FiasDeltaXmlUrl = string.Empty;
        }

        public void SetUrlDate(string date)
        {
            Kladr4ArjUrl = $"https://fias.nalog.ru/Public/Downloads/{date}/base.arj";
            Kladr47ZUrl = $"https://fias.nalog.ru/Public/Downloads/{date}/base.7z";
            GarXMLFullURL = $"https://fias.nalog.ru/Public/Downloads/{date}/gar_xml.zip";
            GarXMLDeltaURL = $"https://fias.nalog.ru/Public/Downloads/{date}/gar_delta_xml.zip";
        }

        public void SetDatesAndVersion(string version)
        {
            DateTime date = DateTime.Parse(version);

            VersionId = int.Parse(version.Replace(".", ""));

            TextVersion = $"БД ФИАС от {date.Date.ToString("dd.MM.yyyy")}";

            ExpDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

            Date = date.ToString("dd.MM.yyyy");
        }

    }
}
