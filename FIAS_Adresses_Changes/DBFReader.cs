using System;
using System.Data.Odbc;
using System.Data;
using System.Data.SqlClient;
using DbfDataReader;
using System.Text;


namespace FIAS_Addresses_Changes
{
    class DBFReader
    {
        public void DBF()
        {
            var dbfPath = "C:\\Users\\пользователь\\Desktop\\Directum\\base\\KLADR.dbf";
            Encoding e = DbfDataReader.EncodingProvider.GetEncoding(866);
            using (var dbfTable = new DbfTable(dbfPath, e))
            {
                var dbfRecord = new DbfRecord(dbfTable);

                while (dbfTable.Read(dbfRecord))
                {
                    if (true && dbfRecord.IsDeleted)
                    {
                        continue;
                    }

                    foreach (var dbfValue in dbfRecord.Values)
                    {
                        Console.WriteLine(dbfValue);
                        var stringValue = dbfValue.ToString();
                        var obj = dbfValue.GetValue();
                    }
                }
            }
        }
    }
}
