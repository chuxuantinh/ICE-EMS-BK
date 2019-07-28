using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Threading;

/// <summary>
/// Summary description for Log
/// </summary>
public class Log
{
	public Log()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static void WriteLog(string p1, string p2, string p3, string p4, string p5,string p6,String p7)
    {
        string FileName = HttpContext.Current.Server.MapPath("~/LOG/" + p1 + ".log");
        string strText = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt ") + "\t\t" + p1 + "\t" + p2 + "\t" + p3 + "\t" + p4 + "\t" + p5 + "\t" + p6 + "\t" + p7;

        if (!File.Exists(FileName))
        {
            // Create New Text File
            FileStream fStream = new FileStream(FileName, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fStream);
            sw.WriteLine(strText);
            sw.Close();
            fStream.Close();

        }
        else
        {
            FileInfo finfo = new FileInfo(FileName);

            while (IsFileLocked(finfo))
            {
                Thread.Sleep(500);
            }

            finfo = null;

            FileStream aFile = new FileStream(FileName, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(aFile);
            sw.WriteLine(strText);
            sw.Close();
            aFile.Close();
        }
    }
    public static void WriteLog(string p1, string p2, string p3, string p4,string p5)
    {
        string FileName = HttpContext.Current.Server.MapPath("~/LOG/" + p1 + ".log");
        string strText = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + "\t\t" + p1 + "\t" + p2 + "\t" + p3 + "\t" + p4 + "\t" + p5;

        if (!File.Exists(FileName))
        {
            // Create New Text File
            FileStream fStream = new FileStream(FileName, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fStream);
            sw.WriteLine(strText);
            sw.Close();
            fStream.Close();

        }
        else
        {
            FileInfo finfo = new FileInfo(FileName);

            while (IsFileLocked(finfo))
            {
                Thread.Sleep(500);
            }

            finfo = null;

            FileStream aFile = new FileStream(FileName, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(aFile);
            sw.WriteLine(strText);
            sw.Close();
            aFile.Close();
        }
    }


    private static bool IsFileLocked(FileInfo file)
    {
        FileStream stream = null;

        try
        {
            stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
        }
        catch (IOException)
        {
            //the file is unavailable because it is:
            //still being written to
            //or being processed by another thread
            //or does not exist (has already been processed)
            return true;
        }
        finally
        {
            if (stream != null)
                stream.Close();
        }

        //file is not locked
        return false;
    }

}