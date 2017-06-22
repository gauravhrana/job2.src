using System;
using System.Diagnostics;
using System.IO;
using System.Configuration;

namespace Library.CommonServices.Utils
{
	/// <summary>
	/// Summary description for PGPUtils.
	/// </summary>
	public class PGPUtils
	{
		public PGPUtils()
		{
		}
		#region IPGPUtils Members
		private Process process;
//--------------------------------------Decrypt-------------------------------------
		public string Decrypt(string mInputFileName, string mInputFolder,string mOutputFileName,string mOutputFolder, string mPassPhrase)
		{
			string sReturn="";
			String ts=Tools.CreateTempStamp();
            string mTempFileIn = Tools.GetTempFolder() + Path.GetFileNameWithoutExtension(mInputFileName) + ts + Path.GetExtension(mInputFileName);
            string mTempFileOut = Tools.GetTempFolder() + Path.GetFileNameWithoutExtension(mOutputFileName) + ts + Path.GetExtension(mOutputFileName);
			string mInputFile=mInputFolder+mInputFileName;
			string mOutputFile=mOutputFolder+mOutputFileName;
			if ( ! System.IO.File.Exists(mInputFile) )
			{throw new Exception("Decrypt:no input file to decrypt:"+ mInputFileName);}
			try
			{
				//delete temp file and copy In file into temp file
				Tools.DeleteFile(mTempFileOut);
				Tools.CopyFile(mInputFile,mTempFileIn);

				// decrypt temp files 
				sReturn =PGPProcess(mTempFileIn, mTempFileOut,"",false);

				// copy result and delete temp files
				Tools.DeleteFile(mOutputFile);
				Tools.CopyFile(mTempFileOut,mOutputFile);
				Tools.DeleteFile(mTempFileIn);
				Tools.DeleteFile(mTempFileOut);
				return "OK " +sReturn;
			}
			catch (Exception e)
			{
				throw new Exception("ERR_PGP " + e.Message+ "|" +e.StackTrace); 
			}		
		}

//------------------------------------Encrypt------------------------------------------------------	
		public string Encrypt(string mInputFileName, string mInputFolder,string mOutputFileName,string mOutputFolder, string mPassPhrase,string mRecipID)
		{
			
			string sReturn="";
			String ts=Tools.CreateTempStamp();
            string mTempFileIn = Tools.GetTempFolder() + Path.GetFileNameWithoutExtension(mInputFileName) + ts + Path.GetExtension(mInputFileName);
            string mTempFileOut = Tools.GetTempFolder() + Path.GetFileNameWithoutExtension(mOutputFileName) + ts + Path.GetExtension(mOutputFileName);
			string mInputFile=mInputFolder+mInputFileName;
			string mOutputFile=mOutputFolder+mOutputFileName;
			if ( ! System.IO.File.Exists(mInputFile) )
			{throw new Exception("Encrypt:no input file to encrypt:"+ mInputFileName);}

            try
            {
                Tools.DeleteFile(mTempFileOut);
                Tools.CopyFile(mInputFile, mTempFileIn);
                // encrypt temp files 
                sReturn = PGPProcess(mTempFileIn, mTempFileOut, mRecipID, true);

                // copy result and delete temp files
                Tools.DeleteFile(mOutputFile);
                Tools.CopyFile(mTempFileOut, mOutputFile);
                return "OK " + sReturn;
            }
            catch (Exception e)
            {
                throw new Exception("ERR_PGP " + e.Message + "|" + e.StackTrace);
            }
            finally
            {
                Tools.DeleteFile(mTempFileIn);
                Tools.DeleteFile(mTempFileOut);
                Tools.DeleteFileLikeInFolder(mOutputFileName + ts, Tools.GetTempFolder());
            }

		}



		//---------------------------------Crypt-------------------------------
		public string PGPProcess(string mTempFileIn , string mTempFileOut,string mRecipID,bool encrypt)
		{
			string s="";
			string mes="";
			string line=null;
  
			if (encrypt)//Encryption	
			{
                s = String.Format("-e \"{0}\" -r \"{1}\" --output \"{2}\"", mTempFileIn, mRecipID,  mTempFileOut);
			}			
			else //Decription			
			{
                //s=@" "+ mTempFileIn + " -o "  +  mTempFileOut + " " + " -z " +  q1 + mPassPhrase + q1;
				
			}
		
			try
			{
				//process
				string pgpPath = ConfigurationManager.AppSettings["PGPPath"];

                ProcessStartInfo pPgp = new ProcessStartInfo(pgpPath);
				pPgp.UseShellExecute=false;
				pPgp.Arguments= s;
				pPgp.RedirectStandardInput = false;
				pPgp.RedirectStandardOutput = true;
				pPgp.RedirectStandardError = true;	
				process = Process.Start(pPgp);
				
				
				while((line = process.StandardOutput.ReadLine()) != null) 
				{
					mes = mes + line + "\r\n";
				}
                if (!process.HasExited)
                {
                    process.WaitForExit(36000);
                }
                
                string resultFile = mTempFileOut;

				if (System.IO.File.Exists(resultFile))
				{					
					return  "OK " + mes;
				}
				else
				{
					throw new Exception("No out file:" + mes); 
				}
				
 
			}
			catch (Exception e)
			{
				
				throw new Exception("ERR_PGP " + e.Message+ "|" +e.StackTrace); 
			}
			finally
			{
				process.Close();
				
			}


//--------------------------------------------------------------------------
		
		}
#endregion
	}
}
