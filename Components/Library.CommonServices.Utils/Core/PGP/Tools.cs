using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Diagnostics;

namespace Library.CommonServices.Utils
{
	/// <summary>
	/// Summary description for Tools.
	/// </summary>
	public class Tools
	{
		public Tools()
		{
		}

		//public  static string  LocalTempFolder=@"C:/BO/";
		public string PgpPath="";
		public string GoldmanPgpPath="";
		public enum BrokersList:int
		{	Goldman,
			Morgan,
			Citi,
			Iss,
			Other
		}
		public enum CryptType:int
		{
			Encr,
			Decr
		}

		//---------------------delete file---------------------------------
		public static void DeleteFile(string FileName)
		{
			if(FileName == null || FileName == string.Empty)
				return;
			try
			{
				if (File.Exists(FileName))
				{
					File.Delete(FileName); }
			}
			 
			catch (Exception e)
			{
				throw e;
			}
		}
		//--------------------Copy File---------------------------------
		public static void CopyFile(string InputFile,string OutputFile)
		{
			try
			{
				if (File.Exists(OutputFile))
				{File.Delete(OutputFile);}
				
				if(File.Exists(InputFile))
				{File.Copy(InputFile,OutputFile);}
			
			}

			catch(Exception e)
			{
				throw e;
			}
		}
        //-------------delete folder---------------------------------
		public static void DeleteFolder(string FolderName)
		{
			try
			{
				if (Directory.Exists(FolderName))
				{
					Directory.Delete(FolderName); 
                }
			}
			 
			catch (Exception e)
			{
				throw e;
			}
		}

		public static void CreateFolder(string FolderName)
		{
			try
			{
				if (Directory.Exists(FolderName))
				{}
				else
				{Directory.CreateDirectory(FolderName);			
				}
			}
			 
			catch (Exception e)
			{
				throw e;
			}
		}
		//----------------------------------------------------------------
		public static void TempFolders()
		{
			try
			{
				Tools.CreateFolder(Tools.GetTempFolder());
				Tools.CreateFolder(Tools.GetTempFolderIn());
				Tools.CreateFolder(Tools.GetTempFolderOut());
			}
			 
			catch (Exception e)
			{
				throw e;
			}
		}

//----------------------copy folder-----------------------------
        public static void CopyFolder(string InputFolder, string OutputFolder)
        {
            CopyFolder(InputFolder, OutputFolder, true);
        }

		public static void CopyFolder(string InputFolder,string OutputFolder, bool deleteExisting)
		{
			try
			{
                if (deleteExisting && Directory.Exists(OutputFolder))
                {
                    Directory.Delete(OutputFolder, true);
                }
                if (!Directory.Exists(OutputFolder))
                {
                    Directory.CreateDirectory(OutputFolder);                    
                }

                String[] Files = Directory.GetFileSystemEntries(InputFolder);
				foreach(string Element in Files)
				{
					// Sub directories
					if(Directory.Exists(Element))
                        CopyFolder(Element, Path.Combine(OutputFolder, Path.GetFileName(Element)), deleteExisting);
						// Files in directory
					else 
						File.Copy(Element, Path.Combine(OutputFolder, Path.GetFileName(Element)), true);
				}
			
			}

			catch(Exception e)
			{
				throw e;
			}
		}
//----------------------Delete files from the folder  with names including FileName
		public static void DeleteFileLikeInFolder(string FileName, string FolderName)
		{
			try
			{  string s="";
			
				if( Directory.Exists(FolderName))
				{String[] Files=Directory.GetFileSystemEntries(FolderName);
					foreach(string Element in Files)
					{
						s=Element.ToString();
						if(s.IndexOf(FileName)>0)
						{File.Delete(s);}			
					}
				}
			}
			catch(Exception e)
			{
				throw e;
			}
		}

		//------------------------------------------------------------------
		public static  string CreateTempStamp()
		{ 
			 string ss=""; 
			 System.DateTime dd; 
			 dd = System.DateTime.Now; 
			 ss="T" + dd.ToString("hhmmff");
			 return ss;
		}
        //------------------------------------------------------------------
		public static string  GetPgpPath() 
	   {
		    string s ="";
            s = @System.Configuration.ConfigurationManager.AppSettings.Get("PgpPath");

            return s;
	   }
       //------------------------------------------------------------------
       public static string GetDesPath()
       {
           string s = "";
           s = @System.Configuration.ConfigurationManager.AppSettings.Get("DesPath");

           return s;
       }
       //------------------------------------------------------------------
		public static string  GetPgpPath_GS() 
		{
			string s ="";
            s = @System.Configuration.ConfigurationManager.AppSettings.Get("PgpPath_GS");
			return s;
		}
		//------------------------------------------------------------------
		public static string  GetPgpWorkFolder() 
		{
			string s ="";
            s = @System.Configuration.ConfigurationManager.AppSettings.Get("PgpWorkFolder");
			return s;
		}
		//------------------------------------------------------------------
		public static string  GetPgpWorkFolder_GS() 
		{
			string s ="";
            s = @System.Configuration.ConfigurationManager.AppSettings.Get("PgpWorkFolder_GS");
			return s;
		}
		//------------------------------------------------------------------
		public static string  GetZipPath() 
		{
			string s ="";
            s = @System.Configuration.ConfigurationManager.AppSettings.Get("ZipPath");
   
           
            //s = System.Configuration.configurationm
			return s;
		}
		//------------------------------------------------------------------
		public static string  GetUnZipPath() 
		{
			string s ="";
            s = @System.Configuration.ConfigurationManager.AppSettings.Get("UnZipPath");
			return s;
		}
		//------------------------------------------------------------------
		public static string  GetTempFolder() 
		{
            //string s =@"C:\PGP\";
            return Path.GetTempPath();
            //s = @System.Configuration.ConfigurationManager.AppSettings.Get("TempFolder");
            //return s;
		}
		public static string  GetTempFolderIn() 
		{
			string s ="";
            s = @System.Configuration.ConfigurationManager.AppSettings.Get("TempFolderIn");
			return s;
		}

		public static string  GetTempFolderOut() 
		{
			string s ="";
            s = @System.Configuration.ConfigurationManager.AppSettings.Get("TempFolderOut");
			return s;
		}

        public static string ExecuteProcess(string filename, string args)
        {
            string returnValue  = string.Empty;
            string errorMessage = string.Empty;   
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo(filename);
                psi.UseShellExecute         = false;
                psi.Arguments               = args;
                psi.RedirectStandardInput   = false;
                psi.RedirectStandardOutput  = true;
                psi.RedirectStandardError   = true;

                Process process = Process.Start(psi);
                try
                {
                    if (!process.WaitForExit(36000))
                    {
                        process.Kill();
                    }

                    //  get output/error messages if any
                    returnValue     = ReadText(process.StandardOutput);
                    errorMessage    = ReadText(process.StandardError);

                    //  check for exit error codes
                    if (process.HasExited && process.ExitCode != 0)
                    {
                        errorMessage = errorMessage.Length > 0 ? errorMessage : returnValue;
                        throw new ApplicationException(string.Format("{0} {1} \n Exit Code {2}: {3}", filename, args, process.ExitCode, errorMessage));
                    }

                    if (!process.HasExited)
                    {
                        errorMessage = string.Format("{0} {1}\n Error: Process timed out.", filename, args);
                        throw new ApplicationException(errorMessage);
                    }
                }
                finally
                {
                    process.Close();
                }

                return returnValue;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("PROCESS ERROR: {0} | {1}", ex.Message, ex.StackTrace));
            }
        }

        private static string ReadText(TextReader tr)
        {
            //  TODO: why not tr.ReadToEnd()?!

            string line;
            string ms = string.Empty;
            long i = 0;

            while ((line = tr.ReadLine()) != null)
            {
                ms = ms + line + " ";
                i++;
                if (i > 100)
                { break; }
            }

            return ms;
        }
	}
}

