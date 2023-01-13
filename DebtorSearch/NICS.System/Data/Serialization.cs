using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace NICS.System.Data
{
    public class Serialization
    { 
        /// <summary>
        /// Creates an XML file of Type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="fileName"></param>
        public static void Persist<T>(object obj, string fileName)
        {
            bool flag = obj == null;
            if (flag)
            {
                throw new ArgumentNullException("obj",
                    "The object you want to serialize is not set to an instance of an object.");
            }

            bool flag2 = string.IsNullOrEmpty(fileName);
            if (flag2)
            {
                throw new ArgumentNullException("fileName", "The file name is required for serialization.");
            }

            using (StreamWriter streamWriter = File.CreateText(fileName))
            {
                XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
                xmlSerializerNamespaces.Add(string.Empty, string.Empty);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(streamWriter, obj, xmlSerializerNamespaces);
                streamWriter.Close();
                streamWriter.Dispose();
            }
        }
        /// <summary>
        /// Converts XML To Object of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetXml<T>(object obj)
        {
            bool flag = obj == null;
            if (flag)
            {
                throw new ArgumentNullException("obj",
                    "The object you want to serialize is not set to an instance of an object.");
            }

            string result;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                StreamWriter textWriter = new StreamWriter(memoryStream, Encoding.UTF8);
                XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
                xmlSerializerNamespaces.Add(string.Empty, string.Empty);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(textWriter, obj, xmlSerializerNamespaces);
                memoryStream.Seek(0L, SeekOrigin.Begin);
                StreamReader streamReader = new StreamReader(memoryStream, Encoding.UTF8);
                string text = streamReader.ReadToEnd();
                result = text;
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T Read<T>(string fileName)
        {
            bool flag = string.IsNullOrEmpty(fileName);
            if (flag)
            {
                throw new ArgumentNullException("fileName", "The file name is required for serialization.");
            }

            bool flag2 = !File.Exists(fileName);
            T result;
            if (flag2)
            {
                result = default(T);
            }
            else
            {
                using (XmlTextReader xmlTextReader = new XmlTextReader(fileName))
                {
                    try
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                        bool flag3 = !xmlSerializer.CanDeserialize(xmlTextReader);
                        if (flag3)
                        {
                            result = default(T);
                        }
                        else
                        {
                            T t = (T) ((object) xmlSerializer.Deserialize(xmlTextReader));
                            xmlTextReader.Close();
                            result = t;
                        }
                    }
                    catch (Exception)
                    {
                        result = default(T);
                    }
                }
            }

            return result;
        }
        /// <summary>
        /// Serialize xml to object of Type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T ConvertXmlToObject<T>(string xml)
        {
            bool flag = string.IsNullOrEmpty(xml);
            if (flag)
            {
                throw new ArgumentNullException("xml", "xml is required for serialization.");
            }

            T result;
            using (XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(xml)))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                bool flag2 = !xmlSerializer.CanDeserialize(xmlTextReader);
                if (flag2)
                {
                    result = default(T);
                }
                else
                {
                    T t = (T) ((object) xmlSerializer.Deserialize(xmlTextReader));
                    xmlTextReader.Close();
                    result = t;
                }
            }

            return result;
        }
        /// <summary>
        /// Call this method to convert xlsx/CSV/xls to Datatable
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static DataTable LoadDataTable(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);
            switch (fileExtension.ToLower())
            {
                case ".xlsx":
                    return ConvertExcelToDataTable(filePath, true);
                case ".xls":
                    return ConvertExcelToDataTable(filePath);
                case ".csv":
                    return ConvertCsvToDataTable(filePath);
                default:
                    return new DataTable();
            }

        }
        /// <summary>
        /// This method Converts the xl file to dataTable
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="isXlsx"></param>
        /// <returns></returns>
        public static DataTable ConvertExcelToDataTable(string filePath, bool isXlsx = false)
        {
            FileStream stream = null;
            IExcelDataReader excelReader = null;
            DataTable dataTable = null;
            stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            excelReader = isXlsx ? ExcelReaderFactory.CreateOpenXmlReader(stream) : ExcelReaderFactory.CreateBinaryReader(stream);
           // excelReader.IsFirstRowAsColumnNames = true;
            DataSet result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration()
                {

                    FilterRow = rowReader => rowReader.Depth > 1,
                    UseHeaderRow = true,
                    EmptyColumnNamePrefix = "F"
                }



            });
            if (result != null && result.Tables.Count > 0)
                dataTable = result.Tables[0];

            stream.Close();
            return dataTable;
        }
        /// <summary>
        /// this converts CSV to Datatable
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>

        public static DataTable ConvertCsvToDataTable(string filePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(filePath))
            {
                var headers = sr.ReadLine().Split(',');
                int index = 0;
                foreach (var header in headers)
                {
                    if (string.IsNullOrEmpty(header) || string.IsNullOrWhiteSpace(header) || header =="0")
                    {
                        var newvalue = "Column" + index;
                        dt.Columns.Add(newvalue);
                        index++;
                    }
                    else
                    {
                        dt.Columns.Add(header);
                    }
                }

                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }

            }
            return dt;
        }

    }
}