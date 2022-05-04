using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
namespace Codematic
{
    /// <summary>
    /// ��־��¼��
    /// </summary>
    public static class LogInfo
    {
        static string logfilename = Application.StartupPath + "\\logInfo.txt";
        static string cmcfgfile = Application.StartupPath + @"\cmcfg.ini";
        

        /// <summary>
        /// д��־
        /// </summary>
        /// <param name="loginfo"></param>
        public static void WriteLog(string loginfo)
        {
            try
            {
                if (File.Exists(cmcfgfile))
                {
                    Maticsoft.Utility.INIFile cfgfile = new Maticsoft.Utility.INIFile(cmcfgfile);
                    string val = cfgfile.IniReadValue("loginfo", "save");
                    if (val.Trim() == "1")
                    {
                        StreamWriter sw = new StreamWriter(logfilename, true);
                        sw.WriteLine(DateTime.Now.ToString() + ":" + loginfo);
                        sw.Close();
                    }
                }
            }
            catch
            { }
        }

        /// <summary>
        /// д��־
        /// </summary>
        /// <param name="loginfo"></param>
        public static void WriteLog(System.Exception ex)
        {
            try
            {
                if (File.Exists(cmcfgfile))
                {
                    Maticsoft.Utility.INIFile cfgfile = new Maticsoft.Utility.INIFile(cmcfgfile);
                    string val = cfgfile.IniReadValue("loginfo", "save");
                    if (val.Trim() == "1")
                    {
                        StreamWriter sw = new StreamWriter(logfilename, true);
                        sw.WriteLine(DateTime.Now.ToString() + ":");
                        sw.WriteLine("������Ϣ��" + ex.Message);
                        sw.WriteLine("Stack Trace:" + ex.StackTrace);
                        sw.WriteLine("Source: " + ex.Source);
                        sw.WriteLine("");
                        sw.Close();
                    }
                }
            }
            catch
            { }
        }
        
        /// <summary>
        /// д��־
        /// </summary>
        /// <param name="loginfo"></param>
        //public static void WriteLog(System.Reflection.MethodBase mb, System.Exception ex)
        //{
        //    if (File.Exists(cmcfgfile))
        //    {
        //        Maticsoft.Utility.INIFile cfgfile = new Maticsoft.Utility.INIFile(cmcfgfile);
        //        string val = cfgfile.IniReadValue("loginfo", "save");
        //        if (val.Trim() == "1")
        //        {                    
        //            string loginfo = mb.ReflectedType.FullName+mb.Name+"������Ϣ��\n\n" + ex.Message + "\n\n" + "Stack Trace:\n" + ex.StackTrace;
        //            StreamWriter sw = new StreamWriter(logfilename, true);
        //            sw.WriteLine(DateTime.Now.ToString() + ":" + loginfo);
        //            sw.Close();
        //        }
        //    }
        //}

    }
}
