using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Datalayer
{
  public class LogWriter
    {
      
      string @LogFilePath = System.Configuration.ConfigurationManager.AppSettings["LogPath"]; 
        public void logwriter(string filename, string detail)
        {
            string FileDate = System.DateTime.Now.ToString("ddMMyyyy");
            if (!Directory.Exists(@LogFilePath + @"\" + "iOPAS_LOG"))
            {
                DirectoryInfo di = Directory.CreateDirectory(@LogFilePath + @"\" + "iOPAS_LOG");
            }

            if (!Directory.Exists(@LogFilePath + @"\" + "iOPAS_LOG" + @"\" + FileDate))
            {
                DirectoryInfo di = Directory.CreateDirectory(@LogFilePath + @"\" + "iOPAS_LOG" + @"\" + FileDate);
            }

            string path = @LogFilePath + @"\" + "iOPAS_LOG" + @"\" + FileDate + @"\" + filename + ".txt";
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter m_streamWriter = new StreamWriter(fs);
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            m_streamWriter.WriteLine(DateTime.Now.ToShortDateString() + " at " + DateTime.Now.ToLongTimeString() + "   " + "\n" + detail);
           // m_streamWriter.WriteLine(" *-------------* \n");
            m_streamWriter.Flush();
            m_streamWriter.Close();
        }
    }
}
