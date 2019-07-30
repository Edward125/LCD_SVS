using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Edward;
using System.IO;

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
        // picture set
        public static int MinGray = 0;
        public static int MaxGray = 0;
        public static int Sigma1 = 10;
        public static int Sigma2 = 2;
        public static int Mult = 1;
        public static int Radius = 3;
        public static int Alpha = 1;
        public static double MinGray2 = -0.012866;
        public static double MaxGray2 = -0.005549;
        public static int Top_L = 10;
        public static int Top_R = 10;
        public static int Bot_L = 10;
        public static int Bot_R = 10;
        public static int MinArea = 800;
        public static int MaxArea = 99999;

        public enum IniSection
        {
            SysConfig,
            Capture,
            PictureSet
        }

        /// <summary>
        /// 
        /// </summary>
        public static  void CheckFolder()
        {

            if (!Directory.Exists(AppFolder))
                Directory.CreateDirectory(AppFolder);
            if (!Directory.Exists(OKImgFolder))
                Directory.CreateDirectory(OKImgFolder);
            if (!Directory.Exists(NGImgFolder))
                Directory.CreateDirectory(NGImgFolder);

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
            //
            IniFile.IniWriteValue(IniSection.PictureSet.ToString(), "MinGray", MinGray, inifilepath);
            IniFile.IniWriteValue(IniSection.PictureSet.ToString(), "MaxGray", MaxGray, inifilepath);
            IniFile.IniWriteValue(IniSection.PictureSet.ToString(), "Sigma1", Sigma1, inifilepath);
            IniFile.IniWriteValue(IniSection.PictureSet.ToString(), "Sigma2", Sigma2, inifilepath);
            IniFile.IniWriteValue(IniSection.PictureSet.ToString(), "Mult",  Mult , inifilepath);
            IniFile.IniWriteValue(IniSection.PictureSet.ToString(), "Radius", Radius, inifilepath);
            IniFile.IniWriteValue(IniSection.PictureSet.ToString(), "Alpha", Alpha, inifilepath);
            IniFile.IniWriteValue(IniSection.PictureSet.ToString(), "MinGray2", MinGray2, inifilepath);
            IniFile.IniWriteValue(IniSection.PictureSet.ToString(), "MaxGray2", MaxGray2, inifilepath);
            IniFile.IniWriteValue(IniSection.PictureSet.ToString(), "Top_L", Top_L , inifilepath);
            IniFile.IniWriteValue(IniSection.PictureSet.ToString(), "Top_R", Top_R, inifilepath);
            IniFile.IniWriteValue(IniSection.PictureSet.ToString(), "Bot_L", Bot_L, inifilepath);
            IniFile.IniWriteValue(IniSection.PictureSet.ToString(), "Bot_R", Bot_R, inifilepath);
            IniFile.IniWriteValue(IniSection.PictureSet.ToString(), "MinArea", MinGray, inifilepath);
            IniFile.IniWriteValue(IniSection.PictureSet.ToString(), "MaxArea", MaxGray, inifilepath);


        }

        /// <summary>
        /// read ini file value 
        /// </summary>
        /// <param name="inifilepath">ini file path</param>
        public static void readIniValue(string inifilepath)
        {
            SystemVersion  = IniFile.IniReadValue(IniSection.SysConfig.ToString(), "SystemVersion", inifilepath);
            SystemName = IniFile.IniReadValue(IniSection.SysConfig.ToString(), "SystemName", inifilepath);
            OKSaveImg = IniFile.IniReadValue(IniSection.Capture.ToString(), "OKSaveImg", inifilepath);
            OKImgFolder = IniFile.IniReadValue(IniSection.Capture.ToString(), "OKImgFolder", inifilepath);
            NGSaveImg = IniFile.IniReadValue(IniSection.Capture.ToString(), "NGSaveImg", inifilepath);
            NGImgFolder = IniFile.IniReadValue(IniSection.Capture.ToString(), "NGImgFolder", inifilepath);

            try
            {
                MinGray = Convert.ToInt16(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "MinGray", inifilepath));
                MaxGray = Convert.ToInt16(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "MaxGray", inifilepath));
                Mult = Convert.ToInt16(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "Mult", inifilepath));
                Radius = Convert.ToInt16(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "Radius", inifilepath));
                Alpha = Convert.ToInt16(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "Alpha", inifilepath));
                MinGray2 = Convert.ToDouble(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "MinGray2", inifilepath));
                MaxGray2 = Convert.ToDouble(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "MinGray2", inifilepath));
                Top_L = Convert.ToInt16(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "Top_L", inifilepath));
                Top_R = Convert.ToInt16(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "Top_R", inifilepath));
                Bot_L = Convert.ToInt16(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "Bot_L", inifilepath));
                Bot_R = Convert.ToInt16(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "Bot_R", inifilepath));
                MinArea = Convert.ToInt16(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "MinArea", inifilepath));
                MaxArea = Convert.ToInt16(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "MaxArea", inifilepath));
            }
            catch (Exception e)
            {

                throw e;
            }



        }




    }
}
