using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Edward;

namespace LCD_SVS
{
    class p
    {

        //
        public static string IniFilePath = @".\SysConfig.ini";
        public static string AppFolder = Application.StartupPath +@"\LCD_SVS";
        public static string SystemVersion = Application.ProductVersion;
        public static string SystemName = Application.ProductName;
        // default value 
        public static string OKSaveImg = "1";
        public static string OKImgFolder = AppFolder + @"\OK";
        public static string NGSaveImg = "1";
        public static string NGImgFolder = AppFolder + @"\NG";



        public enum IniSection
        {
            SysConfig,
            Capture
        }





        /// <summary>
        /// create ini file
        /// </summary>
        /// <param name="inifilepath">ini file path </param>
        public static void createIniFile(string inifilepath)
        {
            IniFile.CreateIniFile(inifilepath);
            //File.SetAttributes(inifilepath, FileAttributes.Hidden);
            IniFile.IniWriteValue(IniSection.SysConfig.ToString(), "SystemVersion", SystemVersion, inifilepath);
            IniFile.IniWriteValue(IniSection.SysConfig.ToString(), "SystemName", SystemName , inifilepath);
            IniFile.IniWriteValue(IniSection.Capture.ToString(), "OKSaveImg", OKSaveImg , inifilepath);
            IniFile.IniWriteValue(IniSection.Capture.ToString(), "OKImgFolder", OKImgFolder, inifilepath);
            IniFile.IniWriteValue(IniSection.Capture.ToString(), "NGSaveImg", NGSaveImg, inifilepath);
            IniFile.IniWriteValue(IniSection.Capture.ToString(), "NGImgFolder", NGImgFolder, inifilepath);
        }

        /// <summary>
        /// read ini file value 
        /// </summary>
        /// <param name="inifilepath">ini file path</param>
        public static void readIniValue(string inifilepath)
        {
            SystemVersion  = IniFile.IniReadValue(IniSection.SysConfig.ToString(), "SystemVersion", inifilepath);
            SystemName  = IniFile.IniReadValue(IniSection.SysConfig.ToString(), "SystemName", inifilepath);
            OKSaveImg  = IniFile.IniReadValue(IniSection.Capture.ToString(), "OKSaveImg", inifilepath);
            OKImgFolder  = IniFile.IniReadValue(IniSection.Capture .ToString(), "OKImgFolder", inifilepath);
            NGSaveImg = IniFile.IniReadValue(IniSection.Capture.ToString(), "NGSaveImg", inifilepath);
            NGImgFolder = IniFile.IniReadValue(IniSection.Capture.ToString(), "NGImgFolder", inifilepath);
        }




    }
}
