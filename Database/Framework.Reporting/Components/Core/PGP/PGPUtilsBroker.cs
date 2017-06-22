using System;
//using EP.Utils.Common;
using System.Diagnostics;
using System.Text;
using System.IO;

namespace Library.CommonServices.Utils
{
	/// <summary>
	/// Summary description for PGPUtilsBroker.
	/// </summary>
	public class PGPUtilsBroker : MarshalByRefObject
	{
		public PGPUtilsBroker()
		{
		}
		//--------------------------------------------------------------------------------		

        //public string DecryptBroker(string mInputFileName, string mInputFolder,string mOutputFileName,string mOutputFolder, FTPConnInfo mConnInfo, int br)
        //{
        //    string s="";
        //    string mPassPhrase=mConnInfo.Passphrase;
        //    string mRecipID=mConnInfo.RecipID;
        //    try
        //    {s=Crypt(mInputFileName,mInputFolder , mOutputFileName,mOutputFolder, mPassPhrase,mRecipID,br, (int) Tools.CryptType.Decr);
        //    return s;
        //    }
			
        //    catch (Exception e)
        //    {
        //    throw(e);}
        //    finally
        //    {
        //    }
	
        //}
//		//----------------------------------------------------------------------------------------------------
        //public string EncryptBroker(string mInputFileName, string mInputFolder,string mOutputFileName,string mOutputFolder, FTPConnInfo mConnInfo, int br)
        //{
        //    string s="";
        //    string mPassPhrase=mConnInfo.Passphrase;
        //    string mRecipID=mConnInfo.RecipID;
			
        //    try
        //    {s=Crypt(mInputFileName,mInputFolder , mOutputFileName,mOutputFolder, mPassPhrase,mRecipID,br, (int) Tools.CryptType.Encr);
        //        return s;}
        //    catch (Exception e)
        //    {throw(e);
        //    }
			
        //}
		//-----------------------------------------------------------------------------		
		private  string Crypt(string mFileIn,string mFolderIn , string mFileOut,string mFolderOut, string mPassPhrase,string mRecipID,int br,int cr)
		{
			string s="";
			string mes="";
			string mTempFileIn="";
			string mTempFileOut="";
			string mTempFolderIn="";
			string mTempFolderOut="";
			string line=null;
			string pPassPhrase="";
			string pProcessPath="";
			string pWorkingFolder="";
			Process process=null;
			System.Diagnostics.ProcessStartInfo pPgp;

			try
			{
                string sInputFileName = Path.Combine(mFolderIn, mFileIn);
                if (!System.IO.File.Exists(sInputFileName))
                {
                    throw new Exception("Decrypt:no input file :" + sInputFileName);
                }

				if ( ! System.IO.Directory.Exists(mFolderOut) )
				{throw new Exception("Decrypt:no output folder :"+ mFolderOut);}

                string sOutputFileName = Path.Combine(mFolderOut, mFileOut);
                Tools.DeleteFile(sOutputFileName);

				pPassPhrase=mPassPhrase;
				pProcessPath=PGPProcPath(br,cr);
				pWorkingFolder=PGPWorkingFolder(br,cr);
		        mTempFileIn=mFileIn; 
				mTempFileOut=mFileOut;
				switch(br)	
				{
				
					case (int) Tools.BrokersList.Goldman:			
			
						mTempFolderIn=Tools.GetPgpWorkFolder_GS();
						mTempFolderOut=Tools.GetPgpWorkFolder_GS();
                        Tools.CopyFile(Path.Combine(mFolderIn, mFileIn), Path.Combine(mTempFolderIn, mTempFileIn));
						break;

					default:
						mTempFolderIn=mFolderIn;
						mTempFolderOut=mFolderOut;
						break;

				}
				Tools.DeleteFile(mTempFolderOut+mTempFileOut);		

				switch(br)
				{	

					// GOLDMAN
					case (int) Tools.BrokersList.Goldman:
					switch(cr)
					{
						case (int) Tools.CryptType.Encr:// Encryption
						//s = s = @" ENCRYPTION " + @mTempFolderIn + " " + mTempFileIn +" " + mTempFileOut + " " + mRecipID + " " + pPassPhrase;
						s = string.Format(" +batchmode +force -es {0} {1} -z {2} -o {3}", Path.Combine(mTempFolderIn, mTempFileIn), mRecipID , mPassPhrase, Path.Combine(mTempFolderOut, mTempFileOut));	
							break;
					    case (int) Tools.CryptType.Decr://decription
						s = @" DECRYPTION " + @mTempFolderIn + " " + mTempFileIn +" " + mTempFileOut + " " + mRecipID + " " + pPassPhrase;
		
						break;
					}				
					break;

					// all brokers except GOLDMAN 
					default:
					switch(cr)
					{
						case (int) Tools.CryptType.Encr:// Encryption
						s = string.Format(" +batchmode +force -es {0} {1} -z {2} -o {3}", Path.Combine(mTempFolderIn, mTempFileIn), mRecipID , mPassPhrase, Path.Combine(mTempFolderOut, mTempFileOut));
						break;
						case (int) Tools.CryptType.Decr://decription
						s=@" -p "+ Path.Combine(mTempFolderIn, mTempFileIn) + " -o "  +  Path.Combine(mTempFolderOut, mTempFileOut) + " " + " -z " + mPassPhrase ;	
						break;				
					}
					break;

				}
				//------------------process------------------------------------------
				
				pPgp=new System.Diagnostics.ProcessStartInfo(pProcessPath);
				
				pPgp.UseShellExecute=false;
				pPgp.Arguments= s;
				pPgp.WorkingDirectory = pWorkingFolder;

				pPgp.RedirectStandardInput = false;
				pPgp.RedirectStandardOutput = true;
				pPgp.RedirectStandardError = true;	
				process = Process.Start(pPgp);
				
				bool b = process.WaitForExit(36000);
				if(!b)
				{
					process.Kill();
				}

				while((line = process.StandardOutput.ReadLine()) != null) 
				{
					mes = mes + line ;
				}
			
				if (System.IO.File.Exists(Path.Combine(mTempFolderOut, mTempFileOut)))
				{
					if (mTempFolderOut!=mFolderOut | mTempFileOut!=mFileOut)
					{
					Tools.CopyFile(Path.Combine(mTempFolderOut, mTempFileOut), Path.Combine(mFolderOut, mFileOut));
					Tools.DeleteFileLikeInFolder(mFileOut,mTempFolderOut);
					Tools.DeleteFileLikeInFolder(mFileIn,mTempFolderIn);
					}

					return  "OK " + mes;
				}
				else
				{
					throw new Exception("---MY EX---: Process is  Over -  No out file:" + mTempFileOut +"  " +  mes); 
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
	
		}
//----------------------------------------------------------------------------
		private string PGPProcPath(int br, int cr)

			{
			string s="";
			switch(br)
			{
				case (int) Tools.BrokersList.Goldman:
					if (cr==(int) Tools.CryptType.Encr)
					{s =Tools.GetPgpPath();	}

					else
					{s =Tools.GetPgpPath_GS();}
			
					break;
				default:
					s =Tools.GetPgpPath();		
					break;
			}				
			
		return s;
		}
//------------------------------------------------------------------------------
		private string PGPWorkingFolder(int br,int cr)
		{
			string s="";
			switch(br)
			{
				case (int) Tools.BrokersList.Goldman:
					if (cr==(int) Tools.CryptType.Encr)
					{s =Tools.GetPgpWorkFolder();	}

					else
					{s =Tools.GetPgpWorkFolder_GS();}
					break;
				default:
					s =Tools.GetPgpWorkFolder();			
					break;
			}				
			
		return s;
		}
	}
}
