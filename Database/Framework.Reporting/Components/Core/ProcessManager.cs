using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections;

namespace Library.CommonServices.Utils
{
    #region ConsoleEvent
    public enum ConsoleEvent : int
    {
        CTRL_C = 0,
        CTRL_BREAK = 1,
        CTRL_CLOSE = 2,
        CTRL_LOGOFF = 5,
        CTRL_SHUTDOWN = 6,
    }
    #endregion

    #region ControlEventHandler
    public delegate void ControlEventHandler(ConsoleEvent e);
    #endregion

    /// <summary>
    /// Summary description for ProcessManager.
    /// </summary>
    public class ProcessManager : IDisposable
    {
        #region members
        private Hashtable _childProcesses = new Hashtable();
        private ControlEventHandler _handler;
        #endregion

        #region external declarations
        [DllImport("kernel32", SetLastError = true)]
        static extern bool SetConsoleCtrlHandler(ControlEventHandler e, bool add);
        #endregion



        #region constructor
        public ProcessManager()
        {
            //trap external process control events
            _handler = new ControlEventHandler(this.OnConsoleEvent);
            ProcessManager.SetConsoleCtrlHandler(_handler, true);
        }

        #endregion

        /// <summary>
        /// fired on kill of this process
        /// </summary>
        /// <param name="e"></param>
        private void OnConsoleEvent(ConsoleEvent e)
        {
            #region implementation

            switch (e)
            {
                case ConsoleEvent.CTRL_CLOSE:
                case ConsoleEvent.CTRL_C:
                case ConsoleEvent.CTRL_LOGOFF:
                case ConsoleEvent.CTRL_SHUTDOWN:
                    IEnumerator ie = _childProcesses.Values.GetEnumerator();
                    while (ie.MoveNext())
                    {
                        Process p = (Process)ie.Current;

                        if (!p.HasExited) p.Kill();
                        p.Close();

                    }
                    Process.GetCurrentProcess().Kill();
                    break;
            }

            #endregion
        }

        /// <summary>
        /// forks a new process
        /// </summary>
        /// <param name="process">exe name</param>
        /// <param name="args">process arguments</param>
        /// <returns></returns>
        public int RunProcess(string process, string args)
        {
            #region implementation

            string output = String.Empty;
            return RunProcess(process, args, Environment.CurrentDirectory, null, ref output);
            #endregion
        }

        /// <summary>
        /// runs the given process, piping input and output to this process
        /// </summary>
        /// <param name="process"></param>
        /// <param name="args"></param>
        /// <param name="workingDir"></param>
        /// <param name="sender"></param>
        /// <param name="eventHandler"></param>
        /// <param name="steps"></param>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public int RunProcess(string process, string args, string workingDir,
            //object sender, ProgressEventHandler eventHandler, int steps, 
            string input, ref string output)
        {
            #region implementation
            System.Diagnostics.Process p = null;



            try
            {
                output = String.Empty;


                ProcessStartInfo psi = new ProcessStartInfo(process);

                psi.WorkingDirectory = workingDir;

                psi.WindowStyle = ProcessWindowStyle.Hidden;
                psi.CreateNoWindow = true;

                psi.Arguments = string.Format(args);


                psi.RedirectStandardOutput = true;
                psi.RedirectStandardInput = true;
                psi.RedirectStandardError = true;
                psi.UseShellExecute = false;

                p = System.Diagnostics.Process.Start(psi);
                lock (_childProcesses)
                {
                    _childProcesses[p.Id] = p;
                }

                System.IO.StreamWriter writer = null;
                try
                {
                    writer = p.StandardInput;

                    if (input != null) writer.WriteLine(input);
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    writer.Close();
                }


                string line = p.StandardOutput.ReadLine();

                while (line != null)
                {
                    /*if(eventHandler!=null) 
                        eventHandler(sender,new ProgressEventArgs(line,steps));*/

                    line = p.StandardOutput.ReadLine();

                    output = string.Format("{0}\r\n{1}", output, line);
                }
                line = p.StandardError.ReadLine();
                while (line != null)
                {
                    /*if(eventHandler!=null) 
                        eventHandler(sender,new ProgressEventArgs(line,steps));*/

                    line = p.StandardError.ReadLine();

                    output = string.Format("{0}\r\n{1}", output, line);
                }

                return p.ExitCode;

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (p != null)
                {
                    _childProcesses.Remove(p.Id);
                    p.Close();
                    //if(!p.HasExited)p.Kill();

                }
            }
            #endregion
        }

        public void Dispose()
        {
            #region implementation
            GC.Collect();
            #endregion
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="logStream"></param>
        /// <param name="text"></param>
        private void WriteLog(FileStream logStream, string text)
        {
            #region implementation
            if (logStream != null)
            {
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes
                    (string.Format("\r\n{0}: {1}", DateTime.Now.ToString(), text));
                logStream.Write(bytes, 0, bytes.Length);
                logStream.Flush();
            }
            #endregion
        }
    }




}
