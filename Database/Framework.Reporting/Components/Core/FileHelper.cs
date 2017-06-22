using System;
using System.IO;

namespace Library.CommonServices.Utils
{
    public class FileHelper
    {
        public static string GetTempFileName(string subdirectory, string fileName)
        {
            return GetUniqueFileName(Path.Combine(Path.GetTempPath(), subdirectory), fileName, true);
        }

        public static string GetUniqueFileName(string path, string fileName, bool createDirectory)
        {

            fileName = StripChars(fileName);

            string fullName = String.Empty;

            if (createDirectory)
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }

            fullName = Path.Combine(path, fileName);

            int i = 0;

            while (File.Exists(fullName))
            {
                i++;

                int splitIndex = fileName.LastIndexOf('.');

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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="path"></param>
		/// <param name="text"></param>
		public static string ReadFile(string path)
		{
			#region implementation
			FileStream reader = new FileStream(path,FileMode.Open,FileAccess.Read,FileShare.Read);

			System.Text.StringBuilder text = new System.Text.StringBuilder();
			StringWriter writer = new StringWriter(text);

			try
			{
				FileInfo info = new FileInfo(path);
				if(info.Length>0)
				{

					byte[] buffer = new byte[16384]; 
					int len; 
					while ( (len=reader.Read (buffer, 0, buffer.Length)) > 0) 
					{ 
						writer.Write (System.Text.Encoding.ASCII.GetString(buffer, 0, len)); 
					} 
				}
			}
			catch(Exception e)
			{
				throw e;
			}
			finally
			{
				writer.Close();
				reader.Close();
			}
			return text.ToString();
			#endregion
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="scriptStream"></param>
		/// <param name="text"></param>
		public static void WriteFile(FileStream stream, string text)
		{
			#region implementation
			if(stream!=null)
			{
				byte[] bytes = System.Text.Encoding.ASCII.GetBytes(string.Format("{0}",text));
				stream.Write(bytes, 0, bytes.Length);
				stream.Flush();
			}
			#endregion
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="path"></param>
		/// <param name="text"></param>
		public static void WriteFile(string path, string text)
		{
			#region implementation
			WriteFile(path,text,FileMode.Append);
			#endregion
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="path"></param>
		/// <param name="text"></param>
		public static void WriteFile(string path, string text, FileMode mode)
		{
			#region implementation
			FileStream stream = new FileStream(path, 
				mode, FileAccess.Write, FileShare.ReadWrite);

			try
			{
				WriteFile(stream,text);
			}
			catch(Exception e)
			{
				throw e;
			}
			finally
			{
				stream.Close();
			}
			#endregion
		}

		

    }
}
