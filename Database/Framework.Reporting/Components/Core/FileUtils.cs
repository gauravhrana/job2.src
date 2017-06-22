using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using log4net;

namespace Library.CommonServices.Utils
{
    public static class FileUtils
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static string GetEmbeddedFile(Assembly assembly, string fileName)
        {
            var resourceList = (from resource in assembly.GetManifestResourceNames()
                                where resource.Equals(fileName, StringComparison.OrdinalIgnoreCase)
                                select resource).ToList();


            var assemblyName = assembly.GetName().Name;

            if (resourceList.Count == 0)
                throw new InvalidOperationException(String.Format("Could not find resource named [{0}] in the assembly [{1}].", fileName, assemblyName));

            if (resourceList.Count > 1)
                throw new InvalidOperationException(String.Format("Found more than one resource with name matching [{0}] in assembly [{1}]. Please supply a more specific name.", fileName, assemblyName));

            var stream = assembly.GetManifestResourceStream(resourceList.First());

            if (stream == null)
                throw new InvalidOperationException(String.Format("Could not find resource named [{0}] in the assembly [{1}].", fileName, assemblyName));

            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static string[] SplitLine(string nextLine)
        {
            return SplitLine(nextLine, ',');
        }

        public static string[] SplitLine(string nextLine, char delimiter)
        {
            //replace the = sign, it will not be required 
            nextLine = nextLine.Replace("=", "");
            //we'll also replace any escaped quotes. In the off chance that they exist they will mess us up
            nextLine = nextLine.Replace("\"\"", "");

            List<string> parts = new List<string>();
            StringBuilder part = new StringBuilder(nextLine.Length);
            var insideQuotePart = false;

            for (var i = 0; i < nextLine.Length; i++)
            {
                char c = nextLine[i];
                if (c == '"')
                {
                    //reset the flag 
                    insideQuotePart = !insideQuotePart;
                    //we will just continue if we come accross a quote
                    continue;
                }

                if (c == delimiter && !insideQuotePart)
                {
                    parts.Add(part.ToString());
                    part.Remove(0, part.Length);
                }
                else
                {
                    part.Append(c);
                }
            }
            parts.Add(part.ToString());
            return parts.ToArray();
        }


        public static string GetFirstLine(string filePath)
        {
            if (String.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("filePath");

            if (!File.Exists(filePath))
                throw new ArgumentException(String.Format("File specified by filePath [{0}] doesn't exist.", filePath));

            using (TextReader reader = new StreamReader(filePath))
            {
                return reader.ReadLine();
            }
        }

        // Wrapper method which guarantees that writer is disposed
        public static void WriteCSVFile(DataTable dt, string filePath, bool append, bool printHeaders)
        {
            using (TextWriter writer = new StreamWriter(filePath, append))
            {
                WriteCSVFileEx(dt, writer, printHeaders, ',', true);
            }
        }

        // Wrapper method which guarantees that writer is disposed
        public static void WriteCSVFileEx(DataTable dt, string filePath, bool append, bool printHeaders, char delimiter,
                                          bool hideDelimiter)
        {
            using (TextWriter writer = new StreamWriter(filePath, append))
            {
                WriteCSVFileEx(dt, writer, printHeaders, delimiter, hideDelimiter);
            }
        }


        public static void WriteCSVFileEx(DataTable dt, TextWriter sw, bool printHeaders, char delimiter,
                                          bool hideDelimiter)
        {
            WriteCSVFileEx(dt, sw, printHeaders, delimiter, hideDelimiter, false, false);
        }

        //generates csv stream from datatable values and writes to a TextWriter stream
        public static void WriteCSVFileEx(DataTable dt, TextWriter sw, bool printHeaders, char delimiter,
                                          bool hideDelimiter, bool dateToString, bool moneyUnformat)
        {
            var firstColumn = true;

            if (printHeaders)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    if (firstColumn)
                        firstColumn = false;
                    else
                        sw.Write(delimiter);

                    sw.Write(dc.ColumnName);
                }

                sw.WriteLine();
            }

            foreach (DataRow dr in dt.Rows)
            {
                firstColumn = true;

                foreach (DataColumn dc in dt.Columns)
                {
                    if (firstColumn)
                        firstColumn = false;
                    else
                        sw.Write(delimiter);

                    DateTime datResult;
                    if (DateTime.TryParse(dr[dc].ToString(), out datResult))
                    {
                        if (dateToString)
                            sw.Write(DateTimeUtils.GetYYYYMMDDFromDate(Convert.ToDateTime(dr[dc])));
                        else
                            sw.Write(dr[dc]);
                    }
                    else
                    {
                        Decimal decResult;
                        if (
                            Decimal.TryParse(
                                dr[dc].ToString().Replace("(", "-").Replace(")", String.Empty).Replace(",", String.Empty),
                                out decResult))
                        {
                            if (moneyUnformat)
                                sw.Write(dr[dc].ToString().Replace("(", "-").Replace(")", String.Empty).Replace(",",
                                                                                                                String.Empty));
                            else
                                sw.Write(dr[dc]);
                        }
                        else
                        {
                            if (hideDelimiter)
                            {
                                var strValue = dr[dc].ToString();
                                sw.Write(strValue.Replace(delimiter, Char.MinValue));
                            }
                            else
                            {
                                sw.Write(dr[dc]);
                            }
                        }
                    }
                }

                sw.WriteLine();
            }
        }

        public static string GetTempFileName(string subdirectory, string fileName)
        {
            return GetUniqueFileName(Path.Combine(Path.GetTempPath(), subdirectory), fileName, true);
        }

        public static string GetUniqueFileName(string path, string fileName, bool createDirectory)
        {
            fileName = StripChars(fileName);

            if (createDirectory)
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }

            var fullName = Path.Combine(path, fileName);

            var i = 0;

            while (File.Exists(fullName))
            {
                i++;

                var splitIndex = fileName.LastIndexOf('.');

                fullName = Path.Combine(path,
                                        fileName.Substring(0, splitIndex) +
                                        "_" + i.ToString().PadLeft(4, '0') +
                                        fileName.Substring(splitIndex, fileName.Length - splitIndex));

                if (i > 1000)
                {
                    //assume infinite loop
                    throw new Exception("Failed to get a unique file name");
                }
            }

            return fullName;
        }

        public static string StripChars(string pTicker)
        {
            return pTicker.Replace("/", "")
                .Replace("\\", "")
                .Replace("?", "")
                .Replace("*", "")
                .Replace(":", "")
                .Replace("|", "")
                .Replace("\"", "")
                .Replace("<", "")
                .Replace(">", "");
        }

        public static byte[] ReadFileBinary(string path)
        {
            #region implementation

            var reader = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            var buffer = new byte[reader.Length];

            try
            {
                var info = new FileInfo(path);
                if (info.Length > 0)
                {
                    while ((reader.Read(buffer, 0, buffer.Length)) > 0)
                    {
                    }
                }
            }
            finally
            {
                reader.Close();
            }

            return buffer;

            #endregion
        }

        public static string ReadFile(string path)
        {
            #region implementation

            var reader = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

            var text = new StringBuilder();
            var writer = new StringWriter(text);

            try
            {
                var info = new FileInfo(path);
                if (info.Length > 0)
                {
                    var buffer = new byte[16384];
                    int len;
                    while ((len = reader.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        writer.Write(Encoding.ASCII.GetString(buffer, 0, len));
                    }
                }
            }
            finally
            {
                writer.Close();
                reader.Close();
            }
            return text.ToString();

            #endregion
        }

        public static void WriteFile(FileStream stream, string text)
        {
            if (stream != null)
            {
                var bytes = Encoding.ASCII.GetBytes(String.Format("{0}", text));
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }
        }

        public static void WriteFile(string path, MemoryStream memStream)
        {
            WriteFile(path, memStream, FileMode.Create);
        }

        public static void WriteFile(string path, MemoryStream memStream, FileMode mode)
        {
            var stream = new FileStream(path,
                                        mode, FileAccess.Write, FileShare.ReadWrite);

            try
            {
                var bytes = memStream.GetBuffer();
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }
            finally
            {
                stream.Close();
            }
        }

        public static void WriteFile(string path, string text)
        {
            WriteFile(path, text, FileMode.Create);
        }

        public static void WriteFile(string path, string text, FileMode mode)
        {
            #region implementation

            var stream = new FileStream(path,
                                        mode, FileAccess.Write, FileShare.ReadWrite);

            try
            {
                WriteFile(stream, text);
            }
            finally
            {
                stream.Close();
            }

            #endregion
        }

        public static DataTable ReadCSV(string csvFile, bool firstRowColumnNames)
        {
            DataTable dt;
            using (var fs = new StreamReader(csvFile))
            {
                dt = TextReaderToDataTable(fs, firstRowColumnNames);
                fs.Close();
            }
            return dt;
        }


        public static DataTable TextReaderToDataTable(TextReader fs, bool firstRowColumnNames)
        {
            var Lines = new Queue<string[]>();

            var nextLine = fs.ReadLine();
            while (nextLine != null)
            {
                var Parts = nextLine.Split(",".ToCharArray());

                if (nextLine.Contains("\""))
                {
                    var FixedParts = new List<string>();
                    var building = false;
                    var builder = new StringBuilder();
                    foreach (string s in Parts)
                        if (building)
                        {
                            builder.Append(s);
                            if (s.EndsWith("\""))
                            {
                                FixedParts.Add(builder.ToString());
                                building = false;
                            }
                            else
                                builder.Append(",");
                        }
                        else if (s.StartsWith("\""))
                        {
                            building = true;
                            builder = new StringBuilder(s + ",");
                        }
                        else
                            FixedParts.Add(s);
                    Lines.Enqueue(FixedParts.ToArray());
                }
                else
                    Lines.Enqueue(Parts);
                nextLine = fs.ReadLine();
            }

            var dt = new DataTable();
            if (Lines.Count > 0)
            {
                var max = 0;
                foreach (var line in Lines)
                {
                    max = line.Length > max ? line.Length : max;
                }

                var colNames = new string[max];

                if (firstRowColumnNames)
                {
                    var names = Lines.Dequeue();
                    for (var i = 0; i < names.Length; i++)
                    {
                        colNames[i] = names[i];
                    }
                }

                for (var i = 0; i < colNames.Length; i++)
                {
                    dt.Columns.Add(colNames[i]);
                }

                while (Lines.Count > 0)
                {
                    dt.Rows.Add(Lines.Dequeue());
                }
            }
            return dt;
        }

        public static bool TryDelete(string file)
        {
            try
            {
                File.Delete(file);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static void UnZipFile(string filename, string outputDirectory)
        {
            using (var s = new ZipInputStream(File.OpenRead(filename)))
            {
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    logger.DebugFormat(theEntry.Name);

                    var directoryName = Path.Combine(outputDirectory, Path.GetDirectoryName(theEntry.Name));
                    var fileName = Path.Combine(directoryName, Path.GetFileName(theEntry.Name));

                    // create directory
                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(directoryName);
                    }

                    if (fileName != String.Empty)
                    {
                        using (var streamWriter = File.Create(fileName))
                        {
                            var data = new byte[2048];
                            while (true)
                            {
                                var size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static List<string> ExtractZipFiles(string zipFilePath, string targetFolder)
        {
            var extractedFiles = new List<string>();
            var zs = new ZipInputStream(File.OpenRead(zipFilePath));
            while (true)
            {
                var zipEntry = zs.GetNextEntry();
                if (zipEntry == null || (!zipEntry.IsFile))
                    break;

                var entryFileName = Path.GetFileName(zipEntry.Name);
                var fullZipToPath = Path.Combine(targetFolder, entryFileName);
                var buffer = new byte[4096];
                using (var streamWriter = File.Create(fullZipToPath))
                {
                    while (true)
                    {
                        var nBytes = zs.Read(buffer, 0, buffer.Length);
                        if (nBytes <= 0)
                            break;
                        streamWriter.Write(buffer, 0, nBytes);
                    }
                }

                extractedFiles.Add(fullZipToPath);
            }
            TryDelete(zipFilePath);
            return extractedFiles;
        }
    }
}