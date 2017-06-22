/* This class was adapted from web: http://www.aspsurvival.com/asp/dataadapter-for-working-with-csv-comma-delimited-files.html
*  Original post was from http://www.opennetcf.com/
*/

using System;
using System.Collections;
using System.IO;
using System.Data;

namespace Library.CommonServices.Utils
{
    ///<summary>
    ///Supports the reading and writing of Comma separated Values into a DataSet
    ///</summary>

    public class CSVDataAdapter : IDataAdapter, IDisposable
    {
        #region Fields

        //nameof CSV file
        private string m_filename;
        //tablename
        private string m_tablename;
        //stream reader
        private StreamReader m_reader;
        //stream writer (for updates)
        private StreamWriter m_writer;

        //filecontains header row?
        private bool m_hasheader;
        
        //storeheader names (if applicable)
        private string[] m_headers;

        private string m_DelimiterString;
        private bool m_RemoveDelimiterString = false;

        private const string DEFAULT_DELIMITER_STRING = ",";
        private const bool DEFAULT_REMOVE_DELIMITER_STRING = false;

        #endregion

        
        #region Constructor
        ///<summary>

        ///Creates a new instance of the CSVDataAdapter class
        ///</summary>
        ///<param name=”filename”>Filename of a valid CSV data file.</param>
        ///<param name=”hasHeader”>True if first row is a header row naming the columns
        ///contained in the data, False if first row is a data row.</param>
        ///<param name="delimterString"></param>
        public CSVDataAdapter(string filename, bool hasHeader, string delimiterString, bool removeDelimiterString)
        {
            //setfilename
            m_filename = filename;

            m_DelimiterString = delimiterString;
            m_RemoveDelimiterString = removeDelimiterString;

            System.IO.FileInfo fi = new FileInfo(m_filename);

            //tablename is filename without path and extension
            m_tablename = fi.Name.Replace("." + fi.Extension,"");
        
            //set whether file contains a header row with column names
            m_hasheader = hasHeader;
        }
        ///Creates a new instance of the CSVDataAdapter class with default delimiter string
        ///</summary>
        ///<param name=”filename”>Filename of a valid CSV data file.</param>
        ///<param name=”hasHeader”>True if first row is a header row naming the columns
        ///contained in the data, False if first row is a data row.</param>
        ///<param name="delimterString"></param>
        public CSVDataAdapter(string filename, bool hasHeader, bool removeDelimiterString) : this(filename, hasHeader, DEFAULT_DELIMITER_STRING, removeDelimiterString) { }
        ///Creates a new instance of the CSVDataAdapter class with default delimiter string
        ///</summary>
        ///<param name=”filename”>Filename of a valid CSV data file.</param>
        ///<param name=”hasHeader”>True if first row is a header row naming the columns
        ///contained in the data, False if first row is a data row.</param>
        ///<param name="delimterString"></param>
        public CSVDataAdapter(string filename, bool hasHeader) : this(filename, hasHeader, DEFAULT_DELIMITER_STRING, DEFAULT_REMOVE_DELIMITER_STRING) { }
        ///<summary>
        ///Creates a new instance of the CSVDataAdapter class from a file with no header row.
        ///</summary>
        ///<param name=”filename”>Filename of a valid CSV data file.</param>

        public CSVDataAdapter(string filename) : this(filename, false){}
        #endregion

        #region Properties

        #region FileName
        ///<summary>
        ///Full path to the CSV file name
        ///</summary>
        public string FileName
        {
            get
            {
                return m_filename;
            }
            set
            {
                m_filename = value;
            }
        }
        #endregion

        #region HeaderRow
        ///<summary>
        ///Flag indicating if first row of files indicates column headers for the table.
        ///</summary>
        ///<value>True if first row of file contains column headers, else False (Defaultis False).</value>
        public bool HasHeaderRow
        {
            get
            {
                return m_hasheader;
            }
            set
            {
                m_hasheader = value;
            }
        }
        #endregion

        #endregion

        #region IDataAdapter Members

        #region Fill
        ///<summary>
        ///Adds or refreshes rows in the DataSet to match those in the data source
        ///</summary>
        ///<param name=”dataSet”>A DataSet to fill with records and, if necessary, schema.</param>
        ///<returns>The number of rows successfully added to or refreshed in the DataSet.
        ///This does not include rows affected by statements that do not return rows.</returns>
        public int Fill(System.Data.DataSet dataSet)
        {
            //fill with default table name
            return Fill(dataSet, m_tablename);
        }
        ///<summary>
        /// using the DataSet and DataTable names.
        ///</summary>
        ///<param name=”dataSet”>A DataSet to fill with records and, if necessary, schema.</param>
        ///<param name=”tableName”>The name of the source table to use for table mapping.</param>
        ///<returns>The number of rows successfully added to or refreshed in the DataSet.
        ///This does not include rows affected by statements that do not return rows.</returns>
        public int Fill(System.Data.DataSet dataSet, string tableName)
        {
            //table within the dataset
            DataTable dt = dataSet.Tables.Add(tableName);
            //store records affected
            int records = 0;
            //raw row
            string rawrow;
            //store the current row
            string[] rowbuffer = new string[0];
            //more records
            bool morerecords = true;

            //open stream to read from file
            m_reader = new StreamReader(m_filename);

            if(m_hasheader)
            {
                //read header row and construct schema
                m_headers = SplitRow(m_reader.ReadLine(), m_DelimiterString);

                foreach(string column in m_headers)
                {
                    //add to columns collection
                    DataColumn dc = dt.Columns.Add(column);
                    dc.DataType = typeof(string);
                    dc.AllowDBNull = true;
                }

                //read first data row
                rawrow = m_reader.ReadLine();

                //check for null first - avoid throwing exception
                if(rawrow == null | rawrow == String.Empty)
                {
                    morerecords = false;
                }
                else
                {
                    rowbuffer = SplitRow(rawrow, m_DelimiterString);
                }
            }
            else
            {
                //read line
                rawrow = m_reader.ReadLine();

                if(rawrow == null | rawrow == String.Empty)
                {
                    morerecords = false;
                }
                else
                {
                    //read the first row and get the length
                    rowbuffer = SplitRow(rawrow, m_DelimiterString);
                }

                for(int iColumn = 0; iColumn < rowbuffer.Length; iColumn++)
                {
                    //add to columns collection
                    DataColumn dc = dt.Columns.Add("Column" + iColumn.ToString());
                    dc.DataType = typeof(string);
                    dc.AllowDBNull = true;
                }
            }

            //processing of further rows goes here
            while(morerecords)
            {
                //increment rows affected
                records++;
                //add values to row and insert into table
                dt.Rows.Add(rowbuffer);

                //read the next row
                rawrow = m_reader.ReadLine();

                if(rawrow == null | rawrow == String.Empty)
                {
                    morerecords = false;
                }
                else
                {
                    //read the first row and get the length
                    rowbuffer = SplitRow(rawrow, m_DelimiterString);
                }
            }

            //close stream
            m_reader.Close();

            //mark dataset as up-to-date
            dataSet.AcceptChanges();

            return records;
        }
        #endregion

        #region Interface Methods
        ///<summary>
        /// Not Supported
        ///</summary>
        System.Data.ITableMappingCollection System.Data.IDataAdapter.TableMappings
        {
            get
            {
                //TODO: Add CSVDataAdapter.TableMappings getter implementation
                return null;
            }
        }

        ///<summary>
        ///Not Supported
        ///</summary>
        System.Data.MissingSchemaAction System.Data.IDataAdapter.MissingSchemaAction
        {
            get
            {
                return MissingSchemaAction.Add;
            }
            set
            {
                //TODO: Add CSVDataAdapter.MissingSchemaAction setter implementation
            }
        }

        ///<summary>
        ///Not Supported
        ///</summary>
        System.Data.MissingMappingAction System.Data.IDataAdapter.MissingMappingAction
        {
            get
            {
                return System.Data.MissingMappingAction.Passthrough;
            }
            set
            {
                //TODO: Add CSVDataAdapter.MissingMappingAction setter implementation
            }
        }

        ///<summary>
        ///Not Supported
        ///</summary>
        ///<returns></returns>
        System.Data.IDataParameter[] System.Data.IDataAdapter.GetFillParameters()
        {
            //TODO: Add CSVDataAdapter.GetFillParameters implementation
            return null;
        }

        ///<summary>
        ///Not Supported
        ///</summary>

        ///<param name=”dataSet”></param>
        ///<param name=”schemaType”></param>
        ///<returns></returns>
        System.Data.DataTable[] System.Data.IDataAdapter.FillSchema(System.Data.DataSet dataSet, System.Data.SchemaType schemaType)
        {
            //TODO: Add CSVDataAdapter.FillSchema implementation
            return null;
        }

        #endregion

        #region Update
        ///<summary>
        ///Writes out the updated DataSet contents to CSV File.
        ///</summary>
        ///<param name=”dataSet”>The DataSet used to update the data source.</param>
        ///<returns>The number of rows successfully updated from the DataSet.</returns>
        public int Update(System.Data.DataSet dataSet)
        {
            //update default tablename
            return Update(dataSet, m_tablename);
        }
        ///<summary>
        ///Writes out the updated named DataTable from the DataSet to CSV File.
        ///</summary>
        ///<param name=”dataSet”>The DataSet used to update the data source.</param>
        ///<param name=”srcTable”>The DataTable.Name to be written.</param>
        ///<returns>The number of rows successfully updated from the DataSet.</returns>
        public int Update(System.Data.DataSet dataSet, string srcTable)
        {
            return Update(dataSet,srcTable,false);
        }
        public int Update(System.Data.DataSet dataSet, string srcTable, bool inclHeader)
        {
            if (inclHeader)
            {
                m_hasheader = true;
            }

            if(dataSet.HasChanges())
            {
                DataTable table;

                try
                {
                    table = dataSet.Tables[srcTable];
                }
                catch
                {
                    //could not find the table specified
                    throw new ArgumentException("srcTable does not exist in specified dataSet");
                }
                //open filestream (overwrite previous file)
                m_writer = new StreamWriter(m_filename, false);

                if(m_hasheader)
                {
                    string columnrow = "";

                    //write header row
                    foreach(DataColumn dc in table.Columns)
                    {
                        //write column name
                        columnrow += dc.ColumnName + m_DelimiterString;
                    }

                    //write assembled column names minus the trailing comma
                    m_writer.WriteLine(columnrow.TrimEnd(m_DelimiterString.ToCharArray()));
                }

                //count the number of rows written
                int rowsaffected = 0;

                //write out all the rows (unless they were deleted)
                foreach(DataRow thisrow in table.Rows)
                {
                    //write all except deleted rows
                    if(thisrow.RowState != DataRowState.Deleted)
                    {
                        //write assembled row minus the trailing comma
                        m_writer.WriteLine(BuildRow(thisrow.ItemArray, m_DelimiterString, m_RemoveDelimiterString));
                        rowsaffected ++;
                    }
                }

                //close filestream
                m_writer.Close();
                
                //mark dataset as up-to-date
                try
                {
                    dataSet.AcceptChanges();
                }
                catch
                {
                }
                //return number of rows written
                return rowsaffected;
            }
            else
            {
                //no changes - ignore
                return 0;
            }
        }

        #endregion

        #endregion

        #region Helper Functions

        #region Quote String
        ///<summary>
        ///Add quotes to a string if it contains a space or carriage return
        ///</summary>
        ///<param name=”inString”></param>
        ///<returns></returns>
        private static string QuoteString(object inString, string delimiterString, bool removeDelimiterString)
        {
            //if(inString.ToString().IndexOf(" ") > -1 || inString.ToString().IndexOf(",") > -1)
            if (inString.ToString().IndexOf(delimiterString) > -1)
            {
                if (removeDelimiterString)
                {
                    return inString.ToString().Replace(delimiterString, String.Empty);
                }
                else
                {
                    return "\"" + inString.ToString() + "\"";
                }
            }
            else

            {
                return inString.ToString();
            }
        }
        #endregion

        #region Build Row
        ///<summary>
        ///Builds a row into a single string with delimiting characters
        ///</summary>
        ///<returns></returns>
        private static string BuildRow(object[] values, string delimiterString, bool removeDelimiterString)
        {
            //create string for a single output row
            string row = "";

            //loop for each item in the row
            foreach(object column in values)
            {
                if(column != null)
                {
                    //add the item (with quotes as appropriate
                    row += QuoteString(column, delimiterString, removeDelimiterString);
                }
                //add the delimiting character
                row += delimiterString;
            }
            //remove the extra delimiter at the end
            row = row.Remove(row.Length - 1, 1);
            return row;
        }
        #endregion

        #region Split Row
        ///<summary>
        ///Splits a delimited row into an array of values
        ///</summary>
        ///<param name=”row”></param>
        ///<returns></returns>
        private static string[] SplitRow(string row, string delimiterString)
        {
            //basic splitting no special cases
            string[] segments = row.Split(delimiterString.ToCharArray());
            //store the fixedup segments
            ArrayList parsedsegments = new ArrayList();
            bool iscontinuation = false;
            string buffer = "";

            for(int iSegment = 0; iSegment < segments.Length; iSegment++)
            {
                //if value begins with a quote
                if(segments[iSegment].StartsWith("\""))
                {
                    //if value also ends with a quote
                    if(segments[iSegment].EndsWith("\""))
                    {
                        //string is unbroken quoted value
                        parsedsegments.Add(segments[iSegment].Trim('"'));
                    }
                    else
                    {
                        //string is beginning of quoted value (which contained a comma)
                        buffer = segments[iSegment].TrimStart('"');
                        //flag that following section(s) are part of this quoted string
                        iscontinuation = true;
                    }
                }
                else
                {
                    if(iscontinuation)
                    {
                        if(segments[iSegment].EndsWith("\""))
                        {
                            //add buffer, comma and this last section with quotes removed
                            parsedsegments.Add(buffer + delimiterString + segments[iSegment].TrimEnd('"'));
                            //this is the end of a continuation
                            iscontinuation = false;
                        }
                        else
                        {
                            //add this section and continue
                            buffer += delimiterString + segments[iSegment];
                        }
                    }
                    else
                    {
                        //item is an unquoted value - add straight in
                        parsedsegments.Add(segments[iSegment]);
                    }
                }
            }
            return (string[])parsedsegments.ToArray(typeof(string));
        }
        #endregion

        //TODO: Change the copied code to implement dispose logic
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            //Dispose
        }
        
        #endregion

    }
}



