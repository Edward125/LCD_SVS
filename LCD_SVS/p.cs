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
        public static string AppCapFolder = AppFolder + @"\Capture";
        public static string SystemVersion = Application.ProductVersion;
        public static string SystemName = Application.ProductName;
        // default value 
        public static string OKSaveImg = "1";
        public static string OKImgFolder = AppFolder + @"\OK";
        public static string NGSaveImg = "1";
        public static string NGImgFolder = AppFolder + @"\NG";
        public static string UseCamera = "1";
        public static string AnalysisPicture = "1";
        // picture set
        public static int MinGray = 0;
        public static int MaxGray = 0;
        public static double  Sigma1 = 10;
        public static double  Sigma2 = 2;
        public static double  Mult = 1.0;
        public static int Radius = 3;
        public static double  Alpha = 1.0;
        public static double MinGray2 = -0.012866;
        public static double MaxGray2 = -0.005549;
        public static int Top_L = 10;
        public static int Top_R = 10;
        public static int Bot_L = 10;
        public static int Bot_R = 10;
        public static int MinArea = 800;
        public static Int64 MaxArea = 99999;

        //
        public static string UseWebService = "1";
        public static string UseTestSN = "1"; //鏈接webservice時查詢數據，加快後續運行時的速度
        public static string Stage = "TU";
        public static string TestSN = "F3NZLT2";
        public static string WebSite = "http://10.62.201.215/Tester.WebService/WebService.asmx";
        //
        public static string UseComPort = "1";
        public static string UseCapture1 = "1";
        public static string UseCapture2 = "1";
        public static string Capture1Signal = "A";
        public static string Capture2Signal = "B";
        public static string ComPort = "";
        //
        public static string UseNet = "1";
        public static string IP = "127.0.0.1";
        public static string Port = "10086";



        public enum IniSection
        {
            SysConfig,
            Capture,
            PictureSet,
            WebSet,
            ComSet,
            NetSet


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
            if (!Directory.Exists(AppCapFolder))
                Directory.CreateDirectory(AppCapFolder);

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
            IniFile.IniWriteValue(IniSection.Capture.ToString(), "UseCamera", UseCamera, inifilepath);
            IniFile.IniWriteValue(IniSection.Capture.ToString(), "OKSaveImg", OKSaveImg , inifilepath);
            IniFile.IniWriteValue(IniSection.Capture.ToString(), "OKImgFolder", OKImgFolder, inifilepath);
            IniFile.IniWriteValue(IniSection.Capture.ToString(), "NGSaveImg", NGSaveImg, inifilepath);
            IniFile.IniWriteValue(IniSection.Capture.ToString(), "NGImgFolder", NGImgFolder, inifilepath);
            IniFile.IniWriteValue(IniSection.Capture.ToString(), "AnalysisPicture", AnalysisPicture, inifilepath);
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
            IniFile.IniWriteValue(IniSection.PictureSet.ToString(), "MinArea", MinArea, inifilepath);
            IniFile.IniWriteValue(IniSection.PictureSet.ToString(), "MaxArea", MaxArea, inifilepath);
            //
            IniFile.IniWriteValue(IniSection.WebSet.ToString(), "UseWebService", UseWebService, inifilepath);
            IniFile.IniWriteValue(IniSection.WebSet.ToString(), "UseTestSN", UseTestSN, inifilepath);
            IniFile.IniWriteValue(IniSection.WebSet.ToString(), "WebSite", WebSite, inifilepath);
            IniFile.IniWriteValue(IniSection.WebSet.ToString(), "Stage", Stage, inifilepath);
            IniFile.IniWriteValue(IniSection.WebSet.ToString(), "TestSN", TestSN, inifilepath);//
            //
            IniFile.IniWriteValue(IniSection.ComSet.ToString(), "UseComPort", UseComPort, inifilepath);
            IniFile.IniWriteValue(IniSection.ComSet.ToString(), "UseCapture1", UseCapture1, inifilepath);
            IniFile.IniWriteValue(IniSection.ComSet.ToString(), "UseCapture2", UseCapture2, inifilepath);
            IniFile.IniWriteValue(IniSection.ComSet.ToString(), "Capture1Signal", Capture1Signal, inifilepath);
            IniFile.IniWriteValue(IniSection.ComSet.ToString(), "Capture2Signal", Capture2Signal, inifilepath);
            IniFile.IniWriteValue(IniSection.ComSet.ToString(), "ComPort", ComPort, inifilepath);
            //
            IniFile.IniWriteValue(IniSection.NetSet.ToString(), "UseNet", UseNet, inifilepath);
            IniFile.IniWriteValue(IniSection.NetSet.ToString(), "IP", IP, inifilepath);
            IniFile.IniWriteValue(IniSection.NetSet.ToString(), "Port",Port, inifilepath);



        }

        /// <summary>
        /// read ini file value 
        /// </summary>
        /// <param name="inifilepath">ini file path</param>
        public static void readIniValue(string inifilepath)
        {
            SystemVersion  = IniFile.IniReadValue(IniSection.SysConfig.ToString(), "SystemVersion", inifilepath);
            SystemName = IniFile.IniReadValue(IniSection.SysConfig.ToString(), "SystemName", inifilepath);
            UseCamera = IniFile.IniReadValue(IniSection.Capture.ToString(), "UseCamera", inifilepath);
            OKSaveImg = IniFile.IniReadValue(IniSection.Capture.ToString(), "OKSaveImg", inifilepath);
            OKImgFolder = IniFile.IniReadValue(IniSection.Capture.ToString(), "OKImgFolder", inifilepath);
            NGSaveImg = IniFile.IniReadValue(IniSection.Capture.ToString(), "NGSaveImg", inifilepath);
            NGImgFolder = IniFile.IniReadValue(IniSection.Capture.ToString(), "NGImgFolder", inifilepath);
            AnalysisPicture = IniFile.IniReadValue(IniSection.Capture.ToString(), "AnalysisPicture", inifilepath);
            try
            {
                MinGray = Convert.ToInt16(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "MinGray", inifilepath));
                MaxGray = Convert.ToInt16(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "MaxGray", inifilepath));
                Sigma1 = Convert.ToDouble(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "Sigma1", inifilepath));
                Sigma2 = Convert.ToDouble(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "Sigma2", inifilepath));
                Mult = Convert.ToDouble (IniFile.IniReadValue(IniSection.PictureSet.ToString(), "Mult", inifilepath));
                Radius = Convert.ToInt16(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "Radius", inifilepath));
                Alpha = Convert.ToDouble (IniFile.IniReadValue(IniSection.PictureSet.ToString(), "Alpha", inifilepath));
                MinGray2 = Convert.ToDouble(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "MinGray2", inifilepath));
                MaxGray2 = Convert.ToDouble(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "MaxGray2", inifilepath));
                Top_L = Convert.ToInt16(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "Top_L", inifilepath));
                Top_R = Convert.ToInt16(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "Top_R", inifilepath));
                Bot_L = Convert.ToInt16(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "Bot_L", inifilepath));
                Bot_R = Convert.ToInt16(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "Bot_R", inifilepath));
                MinArea = Convert.ToInt16(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "MinArea", inifilepath));
                MaxArea = Convert.ToInt64(IniFile.IniReadValue(IniSection.PictureSet.ToString(), "MaxArea", inifilepath));
            }
            catch (Exception e)
            {

                throw e;
            }
            //
            UseWebService = IniFile.IniReadValue(IniSection.WebSet.ToString(), "UseWebService", inifilepath);
            UseTestSN = IniFile.IniReadValue(IniSection.WebSet.ToString(), "UseTestSN", inifilepath);
            WebSite = IniFile.IniReadValue(IniSection.WebSet.ToString(), "WebSite", inifilepath);
            Stage = IniFile.IniReadValue(IniSection.WebSet.ToString(), "Stage", inifilepath);
            TestSN = IniFile.IniReadValue(IniSection.WebSet.ToString(), "TestSN", inifilepath);
            //
            UseComPort = IniFile.IniReadValue(IniSection.ComSet.ToString(), "UseComPort", inifilepath);
            UseCapture1 = IniFile.IniReadValue(IniSection.ComSet.ToString(), "UseCapture1", inifilepath);
            UseCapture2 = IniFile.IniReadValue(IniSection.ComSet.ToString(), "UseCapture2", inifilepath);
            Capture1Signal = IniFile.IniReadValue(IniSection.ComSet.ToString(), "Capture1Signal", inifilepath);
            Capture2Signal = IniFile.IniReadValue(IniSection.ComSet.ToString(), "Capture2Signal", inifilepath);
            ComPort = IniFile.IniReadValue(IniSection.ComSet.ToString(), "ComPort", inifilepath);
            //
            UseNet = IniFile.IniReadValue(IniSection.NetSet.ToString(), "UseNet", inifilepath);
            IP = IniFile.IniReadValue(IniSection.NetSet.ToString(), "IP", inifilepath);
            Port = IniFile.IniReadValue(IniSection.NetSet.ToString(), "Port", inifilepath);

        }




    }
}
