using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SVCamApi;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using Edward;
using HalconDotNet;
using System.Net;
using System.Web.Services.Description;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.Diagnostics;
using System.Management;
using System.Management.Instrumentation;
using Microsoft.Win32;
using System.Net.Sockets;


namespace LCD_SVS
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            gpanel = display.CreateGraphics();
            hwindow = hSmartWindowControl1.HalconWindow;
            this.MouseWheel += new MouseEventHandler(this.my_MouseWheel);

        }

        public Thread acqThread;
        public Thread anyThread; //analysis picture
        public Thread chkwebThread;
        public Thread listenThread;
        public Socket listenSocket;
        public TcpListener listener;
        bool acqThreadIsRuning = false;
        bool acqIsCapturePicture = false;
        bool anyThreadIsRuning = false;
        bool listenThreadIsRuning = false;
        bool isCapture1stPicture = false;
        bool isCapture2ndPicture = false;


        
        public string CurrentID = "";
        private Graphics gpanel;
        delegate void SetStatusCallBack();
        delegate void SetdisplayCallBack();
        delegate void treeUpdateCallBack();
        //treeUpdateCallBack treeUpdate = null;
        //string feature_info = null;
        private Rectangle outRectangle;
        Camera sv_cam = null;
        public Bitmap[] display_img_rgb = new Bitmap[4];
        public Bitmap[] display_img_mono = new Bitmap[4];
        private bool newsize = false;

        //
        WebReference.WebService ws = new WebReference.WebService();
        private static HWindow hwindow; //
        public HTuple hv_ExpDefaultHwinHandle;
        Dictionary<string, string> ComList = new Dictionary<string, string>(); //comport list


        class Cameracontainer
        {
            public SVcamApi myApi;
            public List<SVcamApi._SV_TL_INFO> sv_tl_info_list;
            public List<SVcamApi._SV_INTERFACE_INFO> sv_interface_info_list;
            public List<SVcamApi._SV_DEVICE_INFO> sv_Dev_info_list;
            public List<IntPtr> sv_cam_sys_hdl_list;
            public List<IntPtr> sv_interface_hdl_list;
            public List<Camera> Camlist;
            public bool initsdk_done;

            public Cameracontainer()
            {
                myApi = new SVcamApi();
                sv_tl_info_list = new List<SVcamApi._SV_TL_INFO>();
                sv_interface_info_list = new List<SVcamApi._SV_INTERFACE_INFO>();
                sv_cam_sys_hdl_list = new List<IntPtr>();
                sv_Dev_info_list = new List<SVcamApi._SV_DEVICE_INFO>();
                sv_interface_hdl_list = new List<IntPtr>();
                Camlist = new List<Camera>();


                initsdk_done = false;
            }

            ~Cameracontainer()
            {
                closeCameracontainer();
                sv_Dev_info_list.Clear();
                Camlist.Clear();
                sv_tl_info_list.Clear();
                sv_interface_info_list.Clear();
                sv_cam_sys_hdl_list.Clear();
                sv_interface_hdl_list.Clear();
            }

            public bool InitSDK()
            {
                if (initsdk_done)
                    return true;

                string SVGenicamGentl = null;
                string SVGenicamRoot = null;
                string SVGenicamCache = null;
                string SVCLProtocol = null;
                bool is64Env = IntPtr.Size == 8;

                // Check whether the environment variable exists.
                SVGenicamRoot = Environment.GetEnvironmentVariable("SVS_GENICAM_ROOT");
                if (SVGenicamRoot == null)
                {
                    Console.WriteLine("GetEnvironmentVariableA SVS_GENICAM_ROOT failed! ");
                    return false;
                }
                if (is64Env)
                {
                    SVGenicamGentl = Environment.GetEnvironmentVariable("GENICAM_GENTL64_PATH");
                    if (SVGenicamGentl == null)
                    {
                        Console.WriteLine("GetEnvironmentVariableA GENICAM_GENTL64_PATH failed! ");
                        return false;
                    }
                }
                else
                {
                    SVGenicamGentl = Environment.GetEnvironmentVariable("GENICAM_GENTL32_PATH");
                    if (SVGenicamGentl == null)
                    {
                        Console.WriteLine("GetEnvironmentVariableA GENICAM_GENTL32_PATH failed! ");
                        return false;
                    }
                }

                SVCLProtocol = Environment.GetEnvironmentVariable("SVS_GENICAM_CLPROTOCOL");
                if (SVCLProtocol == null)
                {
                    Console.WriteLine("GetEnvironmentVariableA SVS_GENICAM_CLPROTOCOL failed! ");
                    return false;
                }

                SVGenicamCache = Environment.GetEnvironmentVariable("SVS_GENICAM_CACHE");
                if (SVGenicamCache == null)
                {
                    Console.WriteLine("GetEnvironmentVariableA SVS_GENICAM_CACHE failed! ");
                    return false;
                }

                SVcamApi.SVSCamApiReturn ret = myApi.SVS_LibInit(SVGenicamGentl, SVGenicamRoot, SVGenicamCache, SVCLProtocol);

                if (ret != SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS)
                {
                    Console.WriteLine("SVS_LibInit  failed! ");
                    return false;
                }

                initsdk_done = true;

                return true;
            }

            public void deviceDiscovery()
            {
                InitSDK();
                SVcamApi.SVSCamApiReturn ret = SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS;
                //Open the System module
                UInt32 tlCount = 0;
                ret = myApi.SVS_LibSystemGetCount(ref tlCount);
                if (ret != SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS)
                {
                    Console.WriteLine("GetEnvironmentVariableA SVS_GENICAM_CACHE failed! ");
                    return;
                }

                uint timeout = 3000;
                bool bChanged = false;
                UInt32 numInterface = 0;

                // initialize device and get transport layer info
                for (UInt32 i = 0; i < tlCount; i++)
                {
                    SVcamApi._SV_TL_INFO pInfoOut = new SVcamApi._SV_TL_INFO();

                    ret = myApi.SVS_LibSystemGetInfo(i, ref pInfoOut);

                    if (ret != SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS)
                        continue;

                    string str = pInfoOut.tlType;
                    if (0 != string.Compare("CL", str))
                    {


                        IntPtr sv_cam_sys_hdl = new IntPtr();
                        ret = myApi.SVS_LibSystemOpen(i, ref sv_cam_sys_hdl);


                        sv_cam_sys_hdl_list.Add(sv_cam_sys_hdl);
                        myApi.SVS_SystemUpdateInterfaceList(sv_cam_sys_hdl, ref bChanged, timeout);

                        ret = myApi.SVS_SystemGetNumInterfaces(sv_cam_sys_hdl, ref numInterface);
                        for (uint j = 0; j < numInterface; j++)
                        {

                            uint interfaceIdSize = 0;

                            string interfaceId = null;
                            interfaceIdSize = 512;
                            //Queries the ID of the interface at iIndex in the internal interface list .
                            ret = myApi.SVS_SystemGetInterfaceId(sv_cam_sys_hdl, j, ref interfaceId, ref interfaceIdSize);

                            if (ret != SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS)
                                continue;


                            SVcamApi._SV_INTERFACE_INFO interfaceInfo = new SVcamApi._SV_INTERFACE_INFO();
                            ret = myApi.SVS_SystemInterfaceGetInfo(sv_cam_sys_hdl, interfaceId, ref interfaceInfo);


                            if (ret != SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS)
                                continue;
                            sv_interface_info_list.Add(interfaceInfo);


                            // Queries the information about the interface on this System module
                            IntPtr hInterface = IntPtr.Zero;
                            ret = myApi.SVS_SystemInterfaceOpen(sv_cam_sys_hdl, interfaceId, ref hInterface);
                            if (ret != SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS)
                                continue;

                            sv_interface_hdl_list.Add(hInterface);


                            //Updates the internal list of available devices on this interface.
                            ret = myApi.SVS_InterfaceUpdateDeviceList(hInterface, ref bChanged, timeout);
                            if (ret != SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS)
                                continue;
                            //Queries the number of available devices on this interface
                            UInt32 numDevices = 0;
                            ret = myApi.SVS_InterfaceGetNumDevices(hInterface, ref numDevices);
                            if (ret != SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS)
                                continue;


                            // Get device info for all available devices and add new device to the camera list.
                            for (UInt32 k = 0; k < numDevices; k++)
                            {
                                string deviceId = null;
                                uint deviceIdSize = 512;
                                ret = myApi.SVS_InterfaceGetDeviceId(hInterface, k, ref deviceId, ref deviceIdSize);
                                if (ret != SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS)
                                    continue;

                                SVcamApi._SV_DEVICE_INFO devInfo = new SVcamApi._SV_DEVICE_INFO();
                                ret = myApi.SVS_InterfaceDeviceGetInfo(hInterface, deviceId, ref devInfo);
                                sv_Dev_info_list.Add(devInfo);
                                sv_tl_info_list.Add(pInfoOut);

                            }
                        }
                    }
                }
            }

            public void updateDevInfolist(uint timeout)
            {
                IntPtr hInterface = IntPtr.Zero;
                bool bChanged = false;
                sv_Dev_info_list.Clear();
                for (int j = 0; j < sv_interface_hdl_list.Count; j++)
                {
                    hInterface = sv_interface_hdl_list.ElementAt(j);
                    //Updates the internal list of available devices on this interface.
                    SVcamApi.SVSCamApiReturn ret = myApi.SVS_InterfaceUpdateDeviceList(hInterface, ref bChanged, timeout);
                    if (ret != SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS)
                        continue;


                    //Queries the number of available devices on this interface
                    UInt32 numDevices = 0;
                    ret = myApi.SVS_InterfaceGetNumDevices(hInterface, ref numDevices);
                    if (ret != SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS)
                        continue;

                    // Get device info for all available devices and add new device to the camera list.
                    for (UInt32 k = 0; k < numDevices; k++)
                    {
                        string deviceId = null;
                        uint deviceIdSize = 512;
                        ret = myApi.SVS_InterfaceGetDeviceId(hInterface, k, ref deviceId, ref deviceIdSize);
                        if (ret != SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS)
                            continue;

                        SVcamApi._SV_DEVICE_INFO devInfo = new SVcamApi._SV_DEVICE_INFO();
                        ret = myApi.SVS_InterfaceDeviceGetInfo(hInterface, deviceId, ref devInfo);
                        sv_Dev_info_list.Add(devInfo);

                    }
                }
            }

            public void closeCameracontainer()
            {
                if (Camlist.Count == 0)
                    return;

                for (int j = 0; j < Camlist.Count; j++)
                {
                    Camera cam = Camlist.ElementAt(j);
                    cam.acquisitionStop();
                    cam.StreamingChannelClose();
                    cam.closeConnection();
                    cam.featureInfolist.Clear();
                }

                sv_Dev_info_list.Clear();
                Camlist.Clear();

                for (int j = 0; j < sv_interface_hdl_list.Count; j++)
                    myApi.SVS_InterfaceClose(sv_interface_hdl_list.ElementAt(j));

                for (int j = 0; j < sv_cam_sys_hdl_list.Count; j++)
                    myApi.SVS_SystemClose(sv_cam_sys_hdl_list.ElementAt(j));
                myApi.SVS_LibClose();

            }

        }

        public class NativeMethods
        {
            [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
            public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);
        }

        public struct imagebufferStruct
        {
            public byte[] imagebytes;
            public int sizeX;
            public int sizeY;
            public int dataLegth;
        };

        class Camera
        {
            // Image 
            public imagebufferStruct[] imagebufferMono = new imagebufferStruct[4];
            public imagebufferStruct[] imagebufferRGB = new imagebufferStruct[4];
            public int imageSizeX = 0;
            public int imageSizeY = 0;
            public bool isrgb = false;
            public int destdataIndex = 0;
            public IntPtr Imagptr = IntPtr.Zero;

            // Device
            public SVcamApi myApi = null;
            public IntPtr hRemoteDevice = IntPtr.Zero;
            public IntPtr hDevice = IntPtr.Zero;
            public SVcamApi._SV_DEVICE_INFO devInfo;
            public bool is_opened = false;

            // Streaming
            public IntPtr hStream = IntPtr.Zero;
            uint dsBufcount = 0;
            bool isStreaming = false;
            public bool threadIsRuning = false;

            // Camera Feature 
            public Queue<SVcamApi._SVCamFeaturInf> featureInfolist;
            public SVcamApi._SV_BUFFER_INFO bufferInfoDest = new SVcamApi._SV_BUFFER_INFO();
            public SVcamApi._SV_BUFFER_INFO bufferInfosrc = new SVcamApi._SV_BUFFER_INFO();


            public Camera(SVcamApi._SV_DEVICE_INFO _devinfo, SVcamApi _myApi)
            {
                devInfo = _devinfo;
                featureInfolist = new Queue<SVcamApi._SVCamFeaturInf>();
                hRemoteDevice = new IntPtr();
                hDevice = new IntPtr();
                myApi = _myApi;
                bufferInfoDest.pImagePtr = IntPtr.Zero;
                bufferInfosrc.pImagePtr = IntPtr.Zero;
            }

            ~Camera()
            {
                if (isStreaming)
                {
                    acquisitionStop();
                    StreamingChannelClose();
                }
                closeConnection();
                is_opened = false;
            }

            // Camera: Connection
            public SVcamApi.SVSCamApiReturn openConnection()
            {
                if (is_opened)
                    return 0;
                SVcamApi.SVSCamApiReturn ret = SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS;
                //Open the device with device id (devInfo.uid) connected to the interface (devInfo.hParentIF)
                ret = myApi.SVS_InterfaceDeviceOpen(devInfo.hParentIF, devInfo.uid, SVcamApi.SV_DEVICE_ACCESS_FLAGS_LIST.SV_DEVICE_ACCESS_CONTROL, ref hDevice, ref hRemoteDevice);
                //  Console.WriteLine("open connection");

                if (ret == SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS)
                    is_opened = true;

                return ret;
            }

            public SVcamApi.SVSCamApiReturn closeConnection()
            {
                //Console.WriteLine("close connection");
                SVcamApi.SVSCamApiReturn ret = SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS;
                ret = myApi.SVS_DeviceClose(hDevice);
                return ret;
            }


            // Camera: feature list
            public void getFeatureValue(IntPtr hFeature, ref SVcamApi._SVCamFeaturInf SvCamfeatureInfo)
            {
                myApi.SVS_FeatureGetInfo(hRemoteDevice, hFeature, ref SvCamfeatureInfo.SVFeaturInf);
                if ((int)SVcamApi.SV_FEATURE_TYPE.SV_intfIInteger == SvCamfeatureInfo.SVFeaturInf.type)
                {
                    Int64 value = 0;
                    myApi.SVS_FeatureGetValueInt64(hRemoteDevice, hFeature, ref value);
                    SvCamfeatureInfo.intValue = (ulong)value;
                    string st = value.ToString();
                    SvCamfeatureInfo.strValue = string.Copy(st);
                }
                else if ((int)SVcamApi.SV_FEATURE_TYPE.SV_intfIFloat == SvCamfeatureInfo.SVFeaturInf.type)
                {
                    double value = 0.0f;
                    myApi.SVS_FeatureGetValueFloat(hRemoteDevice, hFeature, ref value);
                    string st = value.ToString();
                    SvCamfeatureInfo.strValue = string.Copy(st);
                }
                else if ((int)SVcamApi.SV_FEATURE_TYPE.SV_intfIBoolean == SvCamfeatureInfo.SVFeaturInf.type)
                {
                    bool value = false;
                    myApi.SVS_FeatureGetValueBool(hRemoteDevice, hFeature, ref value);
                    SvCamfeatureInfo.booValue = value;
                    if (value)
                        SvCamfeatureInfo.strValue = "True";
                    else
                        SvCamfeatureInfo.strValue = "False";
                }
                else if ((int)SVcamApi.SV_FEATURE_TYPE.SV_intfICommand == SvCamfeatureInfo.SVFeaturInf.type)
                {
                    SvCamfeatureInfo.strValue = " = > Execute Command";
                }
                else if ((int)SVcamApi.SV_FEATURE_TYPE.SV_intfIString == SvCamfeatureInfo.SVFeaturInf.type)
                {
                    myApi.SVS_FeatureGetValueString(hRemoteDevice, hFeature, ref SvCamfeatureInfo.strValue, SVcamApi.DefineConstants.SV_STRING_SIZE);
                }
                else if ((int)SVcamApi.SV_FEATURE_TYPE.SV_intfIEnumeration == SvCamfeatureInfo.SVFeaturInf.type)
                {
                    int pInt64Value = 0;
                    uint buffSize = SVcamApi.DefineConstants.SV_STRING_SIZE;
                    SVcamApi.SVSCamApiReturn ret = myApi.SVS_FeatureEnumSubFeatures(hRemoteDevice, hFeature, (int)SvCamfeatureInfo.SVFeaturInf.enumSelectedIndex, ref SvCamfeatureInfo.subFeatureName, buffSize, ref pInt64Value);
                    SvCamfeatureInfo.intValue = (UInt64)pInt64Value;
                    SvCamfeatureInfo.strValue = SvCamfeatureInfo.subFeatureName;
                }
            }

            public void getDeviceFeatureList(SVcamApi.SV_FEATURE_VISIBILITY visibility)
            {
                //DSDeleteContainer(featureInfolist);
                uint iIndex = 0;
                SVcamApi.SVSCamApiReturn ret = SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS;
                while (true)
                {
                    if (iIndex == 500)
                        break;
                    IntPtr hFeature = IntPtr.Zero;
                    ret = myApi.SVS_FeatureGetByIndex(hRemoteDevice, iIndex++, ref hFeature);

                    if (SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS != ret)
                        break;
                    //Create a new Feature structure and
                    SVcamApi._SVCamFeaturInf camFeatureInfo = new SVcamApi._SVCamFeaturInf();
                    ret = myApi.SVS_FeatureGetInfo(hRemoteDevice, hFeature, ref camFeatureInfo.SVFeaturInf);

                    if (SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS != ret)
                    {
                        //  Console.WriteLine(" SVFeatureGetInfo Failed!:%d\n", ret);
                        continue;
                    }

                    //	retrive only a specific features 
                    if (camFeatureInfo.SVFeaturInf.visibility > (uint)visibility || (int)SVcamApi.SV_FEATURE_TYPE.SV_intfIPort == camFeatureInfo.SVFeaturInf.type)
                    {
                        continue;
                    }

                    // get the current value and feature info 
                    getFeatureValue(hFeature, ref camFeatureInfo);
                    //add the feature handle and remote device handle 
                    camFeatureInfo.hFeature = hFeature;
                    camFeatureInfo.hRemoteDevice = hRemoteDevice;
                    featureInfolist.Enqueue(camFeatureInfo);
                }
            }

            //  Stream: Channel creation and control
            public SVcamApi.SVSCamApiReturn StreamingChannelOpen()
            {
                SVcamApi.SVSCamApiReturn ret = SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS;
                string streamId0 = null;
                uint streamId0Size = 512;

                // retriev the stream ID 
                ret = myApi.SVS_DeviceGetStreamId(hDevice, 0, ref streamId0, ref streamId0Size);

                if (SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS != ret)
                    return ret;

                //open the Streaming channel with the retrieved stream ID
                ret = myApi.SVS_DeviceStreamOpen(hDevice, streamId0, ref hStream);
                return ret;
            }

            public SVcamApi.SVSCamApiReturn StreamingChannelClose()
            {
                return myApi.SVS_SVStreamClose(hStream);
            }

            public bool grab()
            {


                SVcamApi.SVSCamApiReturn ret = SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS;
                IntPtr hBuffer = IntPtr.Zero;
                IntPtr Imagptr2 = IntPtr.Zero;

                uint timeout = 1000;
                ret = myApi.SVS_StreamWaitForNewBuffer(hStream, ref Imagptr2, ref hBuffer, timeout);

                if (ret == SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS)
                {
                    ret = myApi.SVS_StreamBufferGetInfo(hStream, hBuffer, ref bufferInfosrc);
                    if (ret != SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS)
                    {
                        //  Console.Write("ERROR TIMEOUT !!");
                        return false;
                    }
                }

                if (bufferInfosrc.pImagePtr == IntPtr.Zero)
                    return false;

                if (bufferInfoDest.pImagePtr == IntPtr.Zero)
                    bufferInfoDest.pImagePtr = Marshal.AllocHGlobal(bufferInfosrc.iImageSize);

                try
                {
                    NativeMethods.CopyMemory(bufferInfoDest.pImagePtr, bufferInfosrc.pImagePtr, (uint)bufferInfosrc.iImageSize); //SDK may be some error

                    bufferInfoDest.iImageSize = bufferInfosrc.iImageSize;
                    bufferInfoDest.iSizeX = bufferInfosrc.iSizeX;
                    bufferInfoDest.iSizeY = bufferInfosrc.iSizeY;
                    bufferInfoDest.iPixelType = bufferInfosrc.iPixelType;
                    bufferInfoDest.iImageId = bufferInfosrc.iImageId;
                    bufferInfoDest.iTimeStamp = bufferInfosrc.iTimeStamp;
                    //Queues a particular buffer for acquisition.
                    myApi.SVS_StreamQueueBuffer(hStream, hBuffer);
                }
                catch (Exception)
                {
                    
                    
                }



                return true;
            }

            public SVcamApi.SVSCamApiReturn acquisitionStart(uint bufcount)
            {
                SVcamApi.SVSCamApiReturn ret = SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS;
                IntPtr hFeature = IntPtr.Zero;
                Int64 payloadSize = 0;

                //retrieve the payload size to allocate the buffers
                myApi.SVS_FeatureGetByName(hRemoteDevice, SVcamApi.CameraFeature.PayloadSize, ref  hFeature);
                myApi.SVS_FeatureGetValueInt64(hRemoteDevice, hFeature, ref  payloadSize);

                // allocat buffers with the retrieved payload size. 
                for (uint i = 0; i < bufcount; i++)
                {

                    IntPtr hBuffer = IntPtr.Zero;
                    myApi.SVS_StreamAllocAndAnnounceBuffer(hStream, (uint)payloadSize, Imagptr, ref hBuffer);
                    if (ret != SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS)
                    {
                        continue;
                    }

                    ret = myApi.SVS_StreamQueueBuffer(hStream, hBuffer);
                    if (SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS != ret)
                        continue;

                }

                myApi.SVS_StreamFlushQueue(hStream, SVcamApi.SV_ACQ_QUEUE_TYPE_LIST.SV_ACQ_QUEUE_ALL_TO_INPUT);
                ret = myApi.SVS_StreamAcquisitionStart(hStream, SVcamApi.SV_ACQ_START_FLAGS_LIST.SV_ACQ_START_FLAGS_DEFAULT, SVcamApi.DefineConstants.INFINIT);

                if (SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS != ret)
                {
                    for (UInt32 i = 0; i < bufcount; i++)
                    {
                        IntPtr hBuffer = IntPtr.Zero;
                        ret = myApi.SVS_StreamGetBufferId(hStream, 0, ref hBuffer);

                        IntPtr pBuffer = IntPtr.Zero;
                        IntPtr imaptr = IntPtr.Zero;

                        if (IntPtr.Zero != hBuffer)
                        {
                            myApi.SVS_StreamRevokeBuffer(hStream, hBuffer, ref pBuffer, ref (imaptr));
                        }
                    }
                }

                //  set acquisitionstart 
                uint ExecuteTimeout = 1000;
                hFeature = IntPtr.Zero;


                myApi.SVS_FeatureGetByName(hRemoteDevice, SVcamApi.CameraFeature.AcquisitionStart, ref  hFeature);
                myApi.SVS_FeatureCommandExecute(hRemoteDevice, hFeature, ExecuteTimeout);
                hFeature = IntPtr.Zero;
                ret = myApi.SVS_FeatureGetByName(hRemoteDevice, SVcamApi.CameraFeature.TLParamsLocked, ref  hFeature);
                Int64 paramsLocked = 1;
                ret = myApi.SVS_FeatureSetValueInt64(hRemoteDevice, hFeature, paramsLocked);

                if (SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS == ret)
                    dsBufcount = bufcount;
                return ret;
            }

            public SVcamApi.SVSCamApiReturn acquisitionStop()
            {
                SVcamApi.SVSCamApiReturn ret = SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS;
                //  set acquisitionstart 
                uint ExecuteTimeout = 1000;
                IntPtr hFeature = IntPtr.Zero;
                ret = myApi.SVS_FeatureGetByName(hRemoteDevice, SVcamApi.CameraFeature.AcquisitionStop, ref  hFeature);

                if (ret != SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS)
                    return ret;
                ret = myApi.SVS_FeatureCommandExecute(hRemoteDevice, hFeature, ExecuteTimeout);

                // 
                hFeature = IntPtr.Zero;
                ret = myApi.SVS_FeatureGetByName(hRemoteDevice, SVcamApi.CameraFeature.TLParamsLocked, ref  hFeature);
                if (ret != SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS)
                    return ret;
                ret = myApi.SVS_FeatureSetValueInt64(hRemoteDevice, hFeature, 0);
                if (ret != SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS)
                    return ret;

                ret = myApi.SVS_StreamAcquisitionStop(hStream, SVcamApi.SV_ACQ_STOP_FLAGS_LIST.SV_ACQ_STOP_FLAGS_DEFAULT);
                if (ret != SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS)
                    return ret;

                ret = myApi.SVS_StreamFlushQueue(hStream, SVcamApi.SV_ACQ_QUEUE_TYPE_LIST.SV_ACQ_QUEUE_INPUT_TO_OUTPUT);
                if (ret != SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS)
                    return ret;
                ret = myApi.SVS_StreamFlushQueue(hStream, SVcamApi.SV_ACQ_QUEUE_TYPE_LIST.SV_ACQ_QUEUE_OUTPUT_DISCARD);
                if (ret != SVcamApi.SVSCamApiReturn.SV_ERROR_SUCCESS)
                    return ret;


                IntPtr hBuffer = IntPtr.Zero;
                IntPtr pBuffer = IntPtr.Zero;
                IntPtr pBuffer2 = IntPtr.Zero;

                for (UInt32 i = 0; i < dsBufcount; i++)
                {

                    ret = myApi.SVS_StreamGetBufferId(hStream, 0, ref hBuffer);


                    if (hBuffer != IntPtr.Zero)
                    {
                        myApi.SVS_StreamRevokeBuffer(hStream, hBuffer, ref pBuffer, ref pBuffer2);
                    }
                    pBuffer = IntPtr.Zero;
                    pBuffer2 = IntPtr.Zero;
                    hBuffer = IntPtr.Zero;
                }
                return ret;
            }

            public void addnewImageData(SVcamApi._SV_BUFFER_INFO ImageInfo, bool isImgRGB)
            {
                // Obtain image information structure
                if (ImageInfo.pImagePtr == IntPtr.Zero)
                    return;

                int currentIdex = destdataIndex;
                {
                    // 8 bit Format
                    if (((int)ImageInfo.iPixelType & SVCamApi.SVcamApi.DefineConstants.SV_GVSP_PIX_EFFECTIVE_PIXELSIZE_MASK) == SVCamApi.SVcamApi.DefineConstants.SV_GVSP_PIX_OCCUPY8BIT)
                    {
                        if (isImgRGB)
                        {
                            myApi.SVS_UtilBufferBayerToRGB(ImageInfo, ref imagebufferRGB[currentIdex].imagebytes[0], imagebufferRGB[currentIdex].dataLegth);
                        }
                        else
                        {
                            System.Runtime.InteropServices.Marshal.Copy(ImageInfo.pImagePtr, imagebufferMono[currentIdex].imagebytes, 0, imagebufferMono[currentIdex].dataLegth);
                        }
                    }

                   //---12 bit Format-------------------------------------------------------------------------------------------------------------------

                    else if (((int)ImageInfo.iPixelType & SVCamApi.SVcamApi.DefineConstants.SV_GVSP_PIX_EFFECTIVE_PIXELSIZE_MASK) == SVCamApi.SVcamApi.DefineConstants.SV_GVSP_PIX_OCCUPY12BIT)
                    {
                        if (isImgRGB)
                        {
                            myApi.SVS_UtilBufferBayerToRGB(ImageInfo, ref imagebufferRGB[currentIdex].imagebytes[0], imagebufferRGB[currentIdex].dataLegth);
                        }

                        else
                        {

                            if (ImageInfo.pImagePtr != null)
                            {
                                // Convert to 8 bit 
                                myApi.SVS_UtilBuffer12BitTo8Bit(ImageInfo, ref imagebufferMono[currentIdex].imagebytes[0], imagebufferMono[currentIdex].dataLegth);
                            }
                        }
                    }
                    else
                        return;
                }
            }
        }

        private Cameracontainer SVSCam = new Cameracontainer();

        private void buttonDiscover_Click(object sender, EventArgs e)
        {
            this.buttonDiscover.Cursor = Cursors.WaitCursor;
            this.textBox_Result.Text = "Searching for cameras...";
            this.textBox_Result.Refresh();

            if (SVSCam.sv_cam_sys_hdl_list.Count() == 0)
                SVSCam.deviceDiscovery();
            else
                SVSCam.updateDevInfolist(1000);


            CamSelectComboBox.SelectedIndex = -1;
            CamSelectComboBox.Items.Clear();


            int number = SVSCam.sv_Dev_info_list.Count;


            for (int j = 0; j < SVSCam.sv_Dev_info_list.Count; j++)
            {
                string camInf = "[" + SVSCam.sv_tl_info_list.ElementAt(j).displayName + "]: " + SVSCam.sv_Dev_info_list.ElementAt(j).model + "  SN/ " + SVSCam.sv_Dev_info_list.ElementAt(j).serialNumber;
                //if (CamSelectComboBox.FindString(camInf) < 0)
                this.CamSelectComboBox.Items.Add(camInf);
            }


            if (number > 0)
            {
                //  buttonDiscover.Enabled = false;
                StringBuilder text = new StringBuilder();
                SetListText("Find " + number + " camera(s),select camera.");
                ShowMessageInternal ( MeaageType.Begin,"Find " + number + " camera(s),select camera.");
                text.AppendFormat(" Select camera!");
                this.textBox_Result.Text = text.ToString();
                this.textBox_Result.ForeColor = Color.Green;

            }
            else
            {
                this.textBox_Result.Text = "No cameras found!";
                SetListText("No cameras found!");
                ShowMessageInternal(MeaageType.Error, "No cameras found!");
                this.textBox_Result.ForeColor = Color.Red;

            }
            buttonDiscover.Cursor = Cursors.Default;
        }

        private void CamSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CamSelectComboBox.SelectedIndex < 0)
                return;
            Camera cam = null;

            // check if a new camera is selecetd
            for (int j = 0; j < SVSCam.Camlist.Count; j++)
            {
                Camera nextcam = SVSCam.Camlist.ElementAt(j);
                string camInf = nextcam.devInfo.serialNumber;
                if (camInf.CompareTo(SVSCam.sv_Dev_info_list.ElementAt(CamSelectComboBox.SelectedIndex).serialNumber) == 0)
                {
                    cam = SVSCam.Camlist.ElementAt(j);
                    break;
                }

            }

            if (cam == null)
                cam = new Camera(SVSCam.sv_Dev_info_list.ElementAt(CamSelectComboBox.SelectedIndex), SVSCam.myApi);

            if (cam.is_opened)
                return;
            sv_cam = cam;
            SetListText("Select camera:" + cam.devInfo.displayName + ",SN:" + cam.devInfo.serialNumber + ".");
            ShowMessageInternal(MeaageType.Begin, "Select camera:" + cam.devInfo.displayName + ",SN:" + cam.devInfo.serialNumber + ".");

            cam.openConnection();
           //SVCamControl camcontrol = new SVCamControl(cam);
           SVSCam.Camlist.Add(cam);
           outRectangle = new Rectangle(0, 0, this.display.Width, display.Height);
           gpanel = display.CreateGraphics();
           sv_cam.is_opened = true;
           sv_cam.StreamingChannelOpen();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Cursor = Cursors.WaitCursor;
            if (p.UseComPort == "1")
            {
                this.tabMain.SelectedTab = tabInspection;
                ShowMessageInternal(MeaageType.Begin, "Start Open Serial Port:" + p.ComPort + "...");
                try
                {
                    serialPort1.PortName = p.ComPort;
                    serialPort1.Open();
                    ShowMessageInternal(MeaageType.Success, "Open Serial Port:" + p.ComPort + " Sucessful...");
                }
                catch (Exception eCom)
                {
                    ShowMessageInternal(MeaageType.Error, "Failed to Open Serial Port:" + p.ComPort + " ...");
                    ShowMessageInternal(MeaageType.Error, eCom.Message);
                    buttonStart.Cursor = Cursors.Default;
                    return;
                }
      
            }



            if (p.UseCamera == "1")
            {
                this.tabMain.SelectedTab = tabCamera;
                if (sv_cam == null)
                {
                    SetListText("Select Camera first please.");
                    ShowMessageInternal(MeaageType.Warning, "Select Camera first please.");
                    return;
                }
                sv_cam.openConnection();
                clearControl();
                gpanel.Clear(Color.Black);

                if (sv_cam.bufferInfoDest.pImagePtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(sv_cam.bufferInfoDest.pImagePtr);
                    sv_cam.bufferInfoDest.pImagePtr = IntPtr.Zero;
                }
                if (sv_cam.bufferInfoDest.pImagePtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(sv_cam.bufferInfoDest.pImagePtr);
                    sv_cam.bufferInfoDest.pImagePtr = IntPtr.Zero;
                }

                sv_cam.acquisitionStart(4);

                if (!acqThreadIsRuning)
                    startAcquisitionThread();

                // updateViewTree();
                btnCapture.Visible = true;
            }

            if (p.UseNet == "1")
            {
                this.tabMain.SelectedTab = tabInspection;
                if (listenSocket == null)
                {
                    listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint ipEP = new IPEndPoint(IPAddress.Parse(p.IP), Convert.ToInt16(p.Port));
                    //listenSocket.Bind(ipEP);
                    //listenSocket.Listen(100);
                    listener = new TcpListener(ipEP);
                }
                try
                {
                    listener.Start();
                    listenThreadIsRuning = true;
                    listenThread = new Thread(new ThreadStart(ReceiveData));
                    listenThread.IsBackground = true;
                    listenThread.Start();
                    ShowMessageInternal(MeaageType.Begin, "Start Listen IP:" + p.IP + ",Port:" + p.Port);
                }
                catch (Exception eNet)
                {
                    ShowMessageInternal(MeaageType.Error, "Listen IP:" + p.IP + ",Port:" + p.Port + " Failed.");
                    ShowMessageInternal(MeaageType.Error, eNet.Message);
                    buttonStart.Cursor = Cursors.Default;
                    return;
                }
            }

            if (p.UseWebService == "1")
            {
                this.tabMain.SelectedTab = tabInspection;
                chkwebThread = new Thread(new ThreadStart(chkwebTHread));
                chkwebThread.IsBackground = true;
                chkwebThread.Start();
            }

            buttonStop.Visible = true;
            buttonStart.Visible = false;
            buttonStart.Cursor = Cursors.Default;
        }

        private void chkwebTHread()
        {
            bool connectWebService = false;
            Stopwatch sw = new Stopwatch();
            TimeSpan ts = new TimeSpan();
            sw.Start();

            ShowMessageInternal(MeaageType.Begin, "Initializing...");
            ws.Url = p.WebSite;
            ShowMessageInternal(MeaageType.Begin, "Loading Assembly...");
            try
            {
                ws.Discover();
                sw.Stop();
                ts = sw.Elapsed;
                ShowMessageInternal(MeaageType.Success, "Load assembly sucessful,Used time(ms):" + ts.Milliseconds);
                connectWebService = true;
            }
            catch (Exception e)
            {
                sw.Stop();
                ts = sw.Elapsed;
                ShowMessageInternal(MeaageType.Failure , "Fail to load assembly,Used time(ms):" + ts.Milliseconds);
                ShowMessageInternal(MeaageType.Error, e.Message);
                this.Invoke((EventHandler)(delegate
                {
                    buttonStop.Visible = false;
                    buttonStart.Visible = true;
                    buttonStart.Cursor = Cursors.Default;
                }));
            }
 
            if (connectWebService)
            {
                if (p.UseTestSN == "1")
                {
                    if (!string.IsNullOrEmpty(p.TestSN) && !string.IsNullOrEmpty(p.Stage))
                        LoadInfoFromWebService(p.TestSN, p.Stage);
                }
            }
        }

        private void LoadInfoFromWebService(string sn, string stage)
        {
            ShowMessageInternal(MeaageType.Begin, "Load info from WebService,SN:" + sn + ",Stage:" + stage);
            WebReference.clsRequestData rd = new WebReference.clsRequestData();
            rd = ws.GetUUTData(sn, stage , rd, 0);
            if (rd.Result == "OK")
            {
                if (!string.IsNullOrEmpty (rd.Model))
                    ShowMessageInternal(MeaageType.Success, "Load info sucessful,SN:" + sn + ",Model:" + rd.Model + ",MO:" + rd.MO);
                else
                    ShowMessageInternal(MeaageType.Failure , "Load info fail,there is no record.SN:" + sn + ",Stage:" + stage);
                if (rd.RequestItem != null)
                {
                    foreach (WebReference.clsRequestItem item in rd.RequestItem)
                    {
                        if (item.Item == "LCD")
                            ShowMessageInternal(MeaageType.Success, "SN:" + sn + ",LCD:" + item.Value);
                        if (item.Item == "MAINBOARD")
                            ShowMessageInternal(MeaageType.Success, "SN:" + sn + ",MAINBOARD:" + item.Value);
                    }
                }
            }
            else
                ShowMessageInternal(MeaageType.Failure, "Can't load info from WebService,SN:" + sn + ",Stage:" + stage );
        }

        private void clearControl()
        {
            //textBoxIInteger.Clear();
            //textBoxIString.Clear();
            //textBoxIFloat.Clear();
            //FeatureTooltip.Clear();

            //buttonICommand.Text = "";

            //hScrollbarIInteger.Minimum = 0;
            //hScrollbarIInteger.Maximum = 0;

            //hScrollbarIFloat.Minimum = 0;
            //hScrollbarIFloat.Maximum = 0;

            //label5.Text = "";

            //comboBoxIEnumeration.Items.Clear();
            //textBoxGenApiFeature.Clear();

            //textBoxIInteger.Visible = false;
            //textBoxIString.Visible = false;
            //textBoxIFloat.Visible = false;
            //buttonICommand.Visible = false;
            //hScrollbarIInteger.Visible = false;
            //hScrollbarIFloat.Visible = false;
            //label5.Visible = false;
            //comboBoxIEnumeration.Visible = false;
        }

        public void startAcquisitionThread()
        {
            acqThreadIsRuning = true;
            acqThread = new Thread(new ThreadStart(acqTHread));
            acqThread.Start();
        }

        public void acqTHread()
        {

            while (acqThreadIsRuning)
            {
                if (!sv_cam.grab())
                    continue;

                // Check if a RGB image( Bayer buffer format) arrived
                bool isImgRGB = false;
                int pDestLength = (int)(sv_cam.bufferInfoDest.iImageSize);
                int sizeX = (int)sv_cam.bufferInfoDest.iSizeX;
                int sizeY = (int)sv_cam.bufferInfoDest.iSizeY;
                CurrentID = Convert.ToString(sv_cam.bufferInfoDest.iImageId);

                if (((int)sv_cam.bufferInfoDest.iPixelType & SVCamApi.SVcamApi.DefineConstants.SV_GVSP_PIX_ID_MASK) >= 8)
                {
                    isImgRGB = true;
                    pDestLength = 3 * pDestLength;
                }
                if (!isImgRGB)
                    isImgRGB = false;

                this.initializeBuffer(isImgRGB, sizeX, sizeY);
                sv_cam.addnewImageData(sv_cam.bufferInfoDest, isImgRGB);
                sv_cam.isrgb = isImgRGB;
                setTodisplay();

            }

        }

        private void initializeBuffer(bool rgb, int camWidth, int camHeight)
        {
            newsize = false;
            int k;
            if (sv_cam == null)
                return;
            if (rgb)
            {


                if (sv_cam.imagebufferRGB[0].dataLegth != 3 * camWidth * camHeight)
                    newsize = true;

                for (k = 0; k < 4; k++)
                {
                    unsafe
                    {
                        if (newsize)
                            sv_cam.imagebufferRGB[k].imagebytes = new byte[3 * camWidth * camHeight];

                        fixed (byte* ColorPtr = sv_cam.imagebufferRGB[k].imagebytes)
                        {
                            if (newsize)
                                display_img_rgb[k] = new Bitmap(camWidth, camHeight, (3 * camWidth), System.Drawing.Imaging.PixelFormat.Format24bppRgb, (IntPtr)ColorPtr);
                            sv_cam.imagebufferRGB[k].sizeX = camWidth;
                            sv_cam.imagebufferRGB[k].sizeY = camHeight;
                            sv_cam.imagebufferRGB[k].dataLegth = 3 * camWidth * camHeight;
                        }
                    }
                }
            }

            else
            {

                if (sv_cam.imagebufferMono[0].dataLegth != camWidth * camHeight)
                    newsize = true;

                for (k = 0; k < 4; k++)
                {
                    unsafe
                    {

                        if (newsize)
                            sv_cam.imagebufferMono[k].imagebytes = new byte[camWidth * camHeight];

                        fixed (byte* MonoPtr = sv_cam.imagebufferMono[k].imagebytes)
                        {
                            if (newsize)
                                display_img_mono[k] = new Bitmap(camWidth, camHeight, camWidth, System.Drawing.Imaging.PixelFormat.Format8bppIndexed, (IntPtr)MonoPtr);
                            sv_cam.imagebufferMono[k].sizeX = camWidth;
                            sv_cam.imagebufferMono[k].sizeY = camHeight;
                            sv_cam.imagebufferMono[k].dataLegth = camWidth * camHeight;
                        }
                    }
                }
            }
        }

        private void setTodisplay()
        {

            if (!acqThreadIsRuning)
                return;

            {
                int currentIndex = this.sv_cam.destdataIndex;
                if (sv_cam.isrgb)
                {
                    if (display_img_rgb[currentIndex] != null)
                    {
                        // Bitmap resized = null;
                        double dbl = display_img_rgb[currentIndex].Width / (double)display_img_rgb[currentIndex].Height;
                        if (dbl < display.Width)
                        {
                            if ((int)((double)display.Height * dbl) <= display.Width)
                            {
                                outRectangle.Width = (int)((double)display.Height * dbl);
                                outRectangle.Height = display.Height;
                                // resized = new Bitmap(display_img_rgb[currentIndex], (int)((double)display.Height * dbl), display.Height);
                            }
                            else
                            {
                                outRectangle.Width = display.Width;
                                outRectangle.Height = (int)((double)display.Width / dbl);
                                // resized = new Bitmap(display_img_rgb[currentIndex], display.Width, (int)((double)display.Width / dbl));
                            }
                        }

                        gpanel.DrawImage(display_img_rgb[currentIndex], outRectangle);

                        if (sv_cam != null)
                        {

                            if (acqIsCapturePicture)
                            {
                                display_img_rgb[currentIndex].Save(DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + currentIndex + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                                SetListText("Capture OK," + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + currentIndex + ".bmp");
                                acqIsCapturePicture = false;
                                if (btnCapture.InvokeRequired)
                                {
                                    btnCapture.BeginInvoke((EventHandler)delegate
                                    {
                                        btnCapture.Enabled = true;
                                    });
                                }
                                this.Invoke((EventHandler)delegate
                                {
                                    picCapturePicture.ImageLocation = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + currentIndex + ".bmp";
                                    txtImgFile.Text = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + currentIndex + ".bmp";
                                });
                            }


                            if (isCapture1stPicture)
                            {
                                string filename = DateTime.Now.ToString("yyyyMMdd") + "_" + currentIndex + "_1st.bmp";
                                string filepath = p.AppCapFolder + @"\" + filename;

                                if (CheckFileExistDeleteFile(filepath, filename))
                                {
                                    display_img_rgb[currentIndex].Save(filename, System.Drawing.Imaging.ImageFormat.Bmp);
                                    ShowMessageInternal(MeaageType.Begin, "Capture 1st picture,File:" + filename);
                                }
                                else
                                    ShowMessageInternal(MeaageType.Warning, "Failed to Capture 1st picture,File:" + filename);
                                isCapture1stPicture = false;

                            }

                            if (isCapture2ndPicture)
                            {
                                string filename = DateTime.Now.ToString("yyyyMMdd") + "_" + currentIndex + "_2nd.bmp";
                                string filepath = p.AppCapFolder + @"\" + filename;

                                if (CheckFileExistDeleteFile(filepath, filename))
                                {
                                    display_img_rgb[currentIndex].Save(filename, System.Drawing.Imaging.ImageFormat.Bmp);
                                    ShowMessageInternal(MeaageType.Begin, "Capture 2nd picture,File:" + filename);
                                }
                                else
                                    ShowMessageInternal(MeaageType.Warning, "Failed to Capture 2nd picture,File:" + filename);
                                isCapture2ndPicture = false;
                            }
                        }
                    }
                }

                else
                {
                    if (display_img_mono[currentIndex] != null)
                    {
                        //Bitmap resized = null;
                        double dbl = display_img_mono[currentIndex].Width / (double)display_img_mono[currentIndex].Height;
                        System.Drawing.Imaging.ColorPalette imgpal = display_img_mono[currentIndex].Palette;

                        // Build bitmap palette Y8
                        for (uint i = 0; i < 256; i++)
                        {
                            imgpal.Entries[i] = Color.FromArgb(
                            (byte)0xFF,
                            (byte)i,
                            (byte)i,
                            (byte)i);
                        }
                        display_img_mono[currentIndex].Palette = imgpal;
                        imgpal = display_img_mono[currentIndex].Palette;
                        if (dbl < display.Width)
                        {
                            if ((int)((double)display.Height * dbl) <= display.Width)
                            {
                                outRectangle.Width = (int)((double)display.Height * dbl);
                                outRectangle.Height = display.Height;
                            }
                            else
                            {
                                outRectangle.Width = (int)((double)display.Height * dbl);
                                outRectangle.Height = display.Height;

                            }

                        }

                        gpanel.DrawImage(display_img_mono[currentIndex], outRectangle);
                        if (sv_cam != null)
                        {
                            if (acqIsCapturePicture)
                            {
                                display_img_rgb[currentIndex].Save(DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + currentIndex + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                                SetListText("Capture OK," + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + currentIndex + ".bmp");
                                acqIsCapturePicture = false;

                                if (btnCapture.InvokeRequired)
                                {
                                    btnCapture.BeginInvoke((EventHandler)delegate
                                    {
                                        btnCapture.Enabled = true;
                                    });
                                }

                                this.Invoke((EventHandler)delegate
                                {
                                    picCapturePicture.ImageLocation = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + currentIndex + ".bmp";
                                    txtImgFile.Text = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + currentIndex + ".bmp";
                                });
                            }


                            if (isCapture1stPicture)
                            {
                                string filename = DateTime.Now.ToString("yyyyMMdd") + "_" + currentIndex + "_1st.bmp";
                                string filepath = p.AppCapFolder + @"\" + filename;

                                if (CheckFileExistDeleteFile(filepath, filename))
                                {
                                    display_img_rgb[currentIndex].Save(filename, System.Drawing.Imaging.ImageFormat.Bmp);
                                    ShowMessageInternal(MeaageType.Begin, "Capture 1st picture,File:" + filename);
                                }
                                else
                                    ShowMessageInternal(MeaageType.Warning, "Failed to Capture 1st picture,File:" + filename);
                                isCapture1stPicture = false;

                            }

                            if (isCapture2ndPicture)
                            {
                                string filename = DateTime.Now.ToString("yyyyMMdd") + "_" + currentIndex + "_2nd.bmp";
                                string filepath = p.AppCapFolder + @"\" + filename;

                                if (CheckFileExistDeleteFile(filepath, filename))
                                {
                                    display_img_rgb[currentIndex].Save(filename, System.Drawing.Imaging.ImageFormat.Bmp);
                                    ShowMessageInternal(MeaageType.Begin, "Capture 2nd picture,File:" + filename);
                                }
                                else
                                    ShowMessageInternal(MeaageType.Warning, "Failed to Capture 2nd picture,File:" + filename);
                                isCapture2ndPicture = false;
                            }
                        }

                    }
                }

                this.sv_cam.destdataIndex++;
                if (this.sv_cam.destdataIndex == 4)
                    this.sv_cam.destdataIndex = 0;
            }

        }

        private void buttonStop_Click(object sender, EventArgs e)
        {

            buttonStop.Cursor = Cursors.WaitCursor;

            if (p.UseNet == "1")
            {
                listenThreadIsRuning = false;
                listener.Stop();
                listenSocket.Close();
                //listenSocket.Shutdown(SocketShutdown.Receive);
                listenThread.Join();
                listenThread.Abort();
            }

            if (p.UseComPort == "1")
            {
                if (serialPort1.IsOpen)
                    serialPort1.Close();
            }

            if (sv_cam != null)
            {
                if (acqThreadIsRuning)
                {
                    clearControl();
                    sv_cam.threadIsRuning = false;
                    acqThreadIsRuning = false;
                    acqThread.Join();
                    sv_cam.acquisitionStop();
                    acqThread.Abort();
                    Array.Clear(sv_cam.imagebufferMono, 0, 4);
                    Array.Clear(sv_cam.imagebufferRGB, 0, 4);
                    btnCapture.Visible = false;
                }
            }
            buttonStop.Cursor = Cursors.Default;
            buttonStop.Visible = false;
            buttonStart.Visible = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label6.Text = "imageID: " + CurrentID;
        }

        private void display_Resize(object sender, EventArgs e)
        {
            outRectangle = new Rectangle(0, 0, this.display.Width, display.Height);
            gpanel = display.CreateGraphics();
        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            buttonQuit.Cursor = Cursors.WaitCursor;
            if (sv_cam != null)
            {
                buttonStop_Click(this, null);
                sv_cam.StreamingChannelClose();
                sv_cam.closeConnection();
                sv_cam.is_opened = false;
                sv_cam = null;
            }
            buttonQuit.Cursor = Cursors.Default;
            this.Close();
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (!acqThreadIsRuning)
                return;
            acqIsCapturePicture = true;
            btnCapture.Enabled = false;

           
        }

        // 对 Windows 窗体控件进行线程安全调用
        private void SetListText( String text)
        {
            if (this.lstCapMsg .InvokeRequired)
            {
                this.lstCapMsg.BeginInvoke(new Action<String>((msg) =>
                {
                    this.lstCapMsg.Items.Add( DateTime.Now.ToString ("HH:mm:ss") +" " + msg);
                    if (lstCapMsg.Items.Count > 0)
                        lstCapMsg.SelectedIndex = lstCapMsg.Items.Count - 1;
                    if (lstCapMsg.Items.Count > 600)
                        lstCapMsg.Items.RemoveAt(0);
                    lstCapMsg.HorizontalScrollbar = true;
                    Graphics g = lstCapMsg.CreateGraphics();
                    int hzSize = (int)g.MeasureString(lstCapMsg.Items[lstCapMsg.Items.Count - 1].ToString(), lstCapMsg.Font).Width;
                    lstCapMsg.HorizontalExtent = hzSize;


                }), text);
            }
            else
            {
                this.lstCapMsg.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " " + text);
                if (lstCapMsg.Items.Count > 0)
                    lstCapMsg.SelectedIndex = lstCapMsg.Items.Count - 1;
                if (lstCapMsg.Items.Count > 600)
                    lstCapMsg.Items.RemoveAt(0);
                Graphics g = lstCapMsg.CreateGraphics();
                int hzSize = (int)g.MeasureString(lstCapMsg.Items[lstCapMsg.Items.Count - 1].ToString(), lstCapMsg.Font).Width;
                lstCapMsg.HorizontalExtent = hzSize;
            }
        }

        private void SetListText(ListBox  listbox,String text)
        {
            if (listbox.InvokeRequired)
            {
                listbox.BeginInvoke(new Action<String>((msg) =>
                {
                    listbox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " " + msg);
                    if (listbox.Items.Count > 0)
                        listbox.SelectedIndex = listbox.Items.Count - 1;
                    if (listbox.Items.Count > 600)
                        listbox.Items.RemoveAt(0);
                    listbox.HorizontalScrollbar = true;
                    Graphics g = listbox.CreateGraphics();
                    int hzSize = (int)g.MeasureString(listbox.Items[listbox.Items.Count - 1].ToString(), listbox.Font).Width;
                    listbox.HorizontalExtent = hzSize;


                }), text);
            }
            else
            {
                listbox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " " + text);
                if (listbox.Items.Count > 0)
                    listbox.SelectedIndex = listbox.Items.Count - 1;
                if (listbox.Items.Count > 600)
                    listbox.Items.RemoveAt(0);
                Graphics g = listbox.CreateGraphics();
                int hzSize = (int)g.MeasureString(listbox.Items[listbox.Items.Count - 1].ToString(), listbox.Font).Width;
                listbox.HorizontalExtent = hzSize;
            }
        }

        private void txtImgFile_DoubleClick(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openfile = new System.Windows.Forms.OpenFileDialog();
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                txtImgFile.Text = openfile.FileName;
                picCapturePicture.ImageLocation = txtImgFile.Text.Trim();
            }
        }

        private void comboSizeMode_SelectedIndexChanged(object sender, EventArgs e)
        {

            string sizemode = comboSizeMode.SelectedItem.ToString();

            switch (comboSizeMode.SelectedIndex)
            {
                case 0:
                    picCapturePicture.SizeMode = PictureBoxSizeMode.Normal;
                    break;
                case 1:
                    picCapturePicture.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 2:
                    picCapturePicture.SizeMode = PictureBoxSizeMode.AutoSize;
                    break;
                case 3:
                    picCapturePicture.SizeMode = PictureBoxSizeMode.CenterImage;
                    break;
                case 4:
                    picCapturePicture.SizeMode = PictureBoxSizeMode.Zoom;
                    break;
                default:
                    break;
            }
            //picCapturePicture.SizeMode = (PictureBoxSizeMode)Enum.ToObject(typeof(PictureBoxSizeMode), sizemode);


        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            
            if (!File.Exists(p.IniFilePath))
                p.createIniFile(p.IniFilePath);
            p.readIniValue(p.IniFilePath); //
            p.CheckFolder();
            for (int i = 0; i <= 255; i++)
            {
                comboMinGray.Items.Add(i);
                comboMaxGray.Items.Add(i);
            }
            GetSerialPort(comboPort);
            UpdateIniValueUI();
        }


        /// <summary>
        /// 讀取配置當中的參數值，更新界面
        /// </summary>
        private void UpdateIniValueUI()
        {
            //
            txtNGImgFolder.SetWatermark("Db-Click here to select folder.");
            txtOKImgFolder.SetWatermark ("Db-Click here to select folder.");
            txtVisionImgFile.SetWatermark("Double Click here to select image file.");
            txtImgFile.SetWatermark("Double Click here to select image file.");


            //
            this.Text = p.SystemName;
            if (p.OKSaveImg == "1")
                chkTestOKSavePictures.Checked = true;
            else
                chkTestOKSavePictures.Checked = false;
            txtOKImgFolder.Text = p.OKImgFolder;
            if (p.NGSaveImg == "1")
                chkTestNGSavePictures.Checked = true;
            else
                chkTestNGSavePictures.Checked = false;
            txtNGImgFolder.Text = p.NGImgFolder;

            comboMinGray.SelectedIndex = p.MinGray;
            comboMaxGray.SelectedIndex = p.MaxGray;
            comboSigma1.Text = p.Sigma1.ToString("F1");
            comboSigma2.Text = p.Sigma2.ToString("F1");
            comboRadius.Text = p.Radius.ToString();
            comboMult.Text = p.Mult.ToString("F1");
            txtMinGray.Text = p.MinGray2.ToString("F6");
            txtMaxGray.Text = p.MaxGray2.ToString("F6");
            txtTopL.Text = p.Top_L.ToString();
            txtTopR.Text = p.Top_R.ToString();
            txtBotL.Text = p.Bot_L.ToString();
            txtBotR.Text = p.Bot_R.ToString();
            txtMinArea.Text = p.MinArea.ToString();
            txtMaxArea.Text = p.MaxArea.ToString();

            //
            if (p.UseCamera == "1")
                chkUseCamera.Checked = true;
            if (p.UseCamera == "0")
                chkUseCamera.Checked = false;
            if (p.UseTestSN == "1")
                chkTestWebService.Checked = true;
            if (p.UseTestSN == "0")
                chkTestWebService.Checked = false;
            if (p.UseWebService == "1")
                chkUseWebService.Checked = true;
            if (p.UseWebService == "0")
                chkUseWebService.Checked = false;
            if (p.AnalysisPicture == "1")
                chkAnalysisImg.Checked  = true;
            if (p.AnalysisPicture == "0")
                chkAnalysisImg.Checked  = false;
            txtWebService.Text = p.WebSite;
            txtTestSN.Text = p.TestSN.ToUpper().Trim();
            txtStage.Text = p.Stage.ToUpper().Trim();

            //
            if (p.UseComPort == "1")
                chkUseCom.Checked = true;
            if (p.UseComPort == "0")
                chkUseCom.Checked =false;
            if (p.UseCapture1 == "1")
                chkCapture1.Checked = true;
            if (p.UseCapture1 == "0")
                chkCapture1.Checked = false;
            if (p.UseCapture2 == "1")
                chkCapture2.Checked = true;
            if (p.UseCapture2 == "0")
                chkCapture2.Checked = false;
            comboPort.Text = p.ComPort;
            txtCapture1Signal.Text = p.Capture1Signal;
            txtCapture2Signal.Text = p.Capture2Signal;
            //
            if (p.UseNet == "1")
                chkUseNet.Checked = true;
            if (p.UseNet == "0")
                chkUseNet.Checked = false;
            txtIP.Text = p.IP;
            txtPort.Text  = p.Port;
            
            //this.textBox_Result.Text = "Searching for cameras...";
            txtInspectionInfo.Text = "Waiting for test...";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkbox"></param>
        /// <param name="Param"></param>
        private void GetChkboxValue(CheckBox checkbox, string Param)
        {
            if (checkbox.Checked)
                Param = "1";
            else
                Param = "0";

        }

        private void chkTestOKSavePictures_CheckedChanged(object sender, EventArgs e)
        {
            GetChkboxValue(chkTestOKSavePictures, p.OKSaveImg);
            IniFile.IniWriteValue(p.IniSection.Capture.ToString(), "OKSaveImg", p.OKSaveImg, p.IniFilePath);
        }

        private void chkTestNGSavePictures_CheckedChanged(object sender, EventArgs e)
        {
            GetChkboxValue(chkTestNGSavePictures, p.NGSaveImg);
            IniFile.IniWriteValue(p.IniSection.Capture.ToString(), "NGSaveImg", p.NGSaveImg, p.IniFilePath);
        }

        private void txtOKImgFolder_TextChanged(object sender, EventArgs e)
        {
            p.OKImgFolder = txtOKImgFolder.Text.Trim();
            IniFile.IniWriteValue(p.IniSection.Capture.ToString(), "OKImgFolder", p.OKImgFolder, p.IniFilePath);
        }

        private void txtNGImgFolder_TextChanged(object sender, EventArgs e)
        {
            p.NGImgFolder = txtNGImgFolder.Text.Trim();
            IniFile.IniWriteValue(p.IniSection.Capture.ToString(), "NGImgFolder", p.NGImgFolder, p.IniFilePath);
        }

        private void txtVisionImgFile_DoubleClick(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openfile = new System.Windows.Forms.OpenFileDialog();
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                //txtImgFile.Text = openfile.FileName;
                txtVisionImgFile.Text = openfile.FileName;
               // picCapturePicture.ImageLocation = txtImgFile.Text.Trim();
                HOperatorSet.ClearWindow(hwindow);
                DisplayHalconImage(txtVisionImgFile.Text.Trim());
               
            }
        }

        private void DisplayHalconImage(HTuple imagefile)
        {
            hwindow.ClearWindow();
            HObject ho_Image = new HObject ();
            HOperatorSet.GenEmptyObj(out ho_Image);
            ho_Image.Dispose();
            HOperatorSet.ReadImage(out ho_Image,imagefile);
            
            #region 縮放圖像
            HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
            hv_Width.Dispose(); hv_Height.Dispose();
            bool needResizeImage = true;
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            int im_width = int.Parse(hv_Width.ToString());
            int im_height = int.Parse(hv_Height.ToString());

            double im_AspectRatio = (double)(im_width) / (double)(im_height);

            int w_width = hSmartWindowControl1.Size.Width;
            int w_height = hSmartWindowControl1.Size.Height;

            double w_AspectRatio = (double)(w_width) / (double)(w_height);
            HOperatorSet.SetSystem("int_zooming", "false");
            HTuple para = new HTuple("constant");
            HObject ho_zoomImage;
            HOperatorSet.GenEmptyObj(out ho_zoomImage);
            ho_zoomImage.Dispose();

            if (w_width < im_width && im_AspectRatio > w_AspectRatio)
            {
                //超寬圖像
                HOperatorSet.ZoomImageSize(ho_Image, out ho_zoomImage, w_width, w_width / im_AspectRatio, para);
            }
            else if (w_height < im_height && im_AspectRatio < w_AspectRatio)
            {
                //超高圖像
                HOperatorSet.ZoomImageSize(ho_Image, out ho_zoomImage, w_height * im_AspectRatio, w_height, para);
            }
            else
                needResizeImage = false;
            #endregion

            #region display
            hwindow.SetPart(0, 0, -2, -2);
            if (needResizeImage)
                hwindow.DispObj(ho_zoomImage);
            else
                hwindow.DispObj(ho_Image);

            #endregion

            ho_Image.Dispose();
            ho_zoomImage.Dispose();
            hv_Width.Dispose();
            hv_Height.Dispose();
        }

        private void DisplayHalconImage(HObject ho_image)
        {
            hwindow.ClearWindow();
            //HObject ho_Image = new HObject();
            //HOperatorSet.GenEmptyObj(out ho_Image);
            //ho_Image.Dispose();
            //HOperatorSet.ReadImage(out ho_Image, imagefile);

            #region 縮放圖像
            HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
            hv_Width.Dispose(); hv_Height.Dispose();
            bool needResizeImage = true;
            HOperatorSet.GetImageSize(ho_image, out hv_Width, out hv_Height);
            int im_width = int.Parse(hv_Width.ToString());
            int im_height = int.Parse(hv_Height.ToString());

            double im_AspectRatio = (double)(im_width) / (double)(im_height);

            int w_width = hSmartWindowControl1.Size.Width;
            int w_height = hSmartWindowControl1.Size.Height;

            double w_AspectRatio = (double)(w_width) / (double)(w_height);
            HOperatorSet.SetSystem("int_zooming", "false");
            HTuple para = new HTuple("constant");
            HObject ho_zoomImage;
            HOperatorSet.GenEmptyObj(out ho_zoomImage);
            ho_zoomImage.Dispose();

            if (w_width < im_width && im_AspectRatio > w_AspectRatio)
            {
                //超寬圖像
                HOperatorSet.ZoomImageSize(ho_image , out ho_zoomImage, w_width, w_width / im_AspectRatio, para);
            }
            else if (w_height < im_height && im_AspectRatio < w_AspectRatio)
            {
                //超高圖像
                HOperatorSet.ZoomImageSize(ho_image, out ho_zoomImage, w_height * im_AspectRatio, w_height, para);
            }
            else
                needResizeImage = false;
            #endregion

            #region display
            hwindow.SetPart(0, 0, -2, -2);
            if (needResizeImage)
                hwindow.DispObj(ho_zoomImage);
            else
                hwindow.DispObj(ho_image);

            #endregion

            ho_image.Dispose();
            ho_zoomImage.Dispose();
            hv_Width.Dispose();
            hv_Height.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ho_image"></param>
        /// <param name="IsClear"></param>
        private void VisionResizeImage(HObject ho_image,bool IsClearTuple)
        {
            #region 縮放圖像
            HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
            hv_Width.Dispose(); hv_Height.Dispose();
            bool needResizeImage = true;
            HOperatorSet.GetImageSize(ho_image, out hv_Width, out hv_Height);
            int im_width = int.Parse(hv_Width.ToString());
            int im_height = int.Parse(hv_Height.ToString());

            double im_AspectRatio = (double)(im_width) / (double)(im_height);

            int w_width = hSmartWindowControl1.Size.Width;
            int w_height = hSmartWindowControl1.Size.Height;

            double w_AspectRatio = (double)(w_width) / (double)(w_height);
            HOperatorSet.SetSystem("int_zooming", "false");
            HTuple para = new HTuple("constant");
            HObject ho_zoomImage;
            HOperatorSet.GenEmptyObj(out ho_zoomImage);
            ho_zoomImage.Dispose();

            if (w_width < im_width && im_AspectRatio > w_AspectRatio)
            {
                //超寬圖像
                HOperatorSet.ZoomImageSize(ho_image, out ho_zoomImage, w_width, w_width / im_AspectRatio, para);
            }
            else if (w_height < im_height && im_AspectRatio < w_AspectRatio)
            {
                //超高圖像
                HOperatorSet.ZoomImageSize(ho_image, out ho_zoomImage, w_height * im_AspectRatio, w_height, para);
            }
            else
                needResizeImage = false;
            #endregion

            #region display
            hwindow.SetPart(0, 0, -2, -2);
            if (needResizeImage)
                hwindow.DispObj(ho_zoomImage);
            else
                hwindow.DispObj(ho_image);

            #endregion
            
            if (IsClearTuple)
            {
                ho_image.Dispose();
            }
            ho_zoomImage.Dispose();
            hv_Width.Dispose();
            hv_Height.Dispose();
        }

        private void hSmartWindowControl1_Resize(object sender, EventArgs e)
        {
           // DisplayHalconImage(txtVisionImgFile.Text.Trim());
        }

        private void my_MouseWheel(object sendor, MouseEventArgs e)
        {
            System.Drawing.Point pt = this.Location;
            int leftBorder = hSmartWindowControl1.Location.X;
            int rightBorder = hSmartWindowControl1.Location.X + hSmartWindowControl1.Size.Width;
            int topBorder = hSmartWindowControl1.Location.Y;
            int bottomBorder = hSmartWindowControl1.Location.Y + hSmartWindowControl1.Size.Height;
            if (e.X > leftBorder && e.X < rightBorder && e.Y > topBorder && e.Y < bottomBorder)
            {
                MouseEventArgs newe = new MouseEventArgs(e.Button, e.Clicks, e.X - pt.X, e.Y - pt.Y, e.Delta);
                hSmartWindowControl1.HSmartWindowControl_MouseWheel(sendor, newe);
            }
        }

        private void btn2Gray_Click(object sender, EventArgs e)
        {



        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isvisiondebug"></param>
        private void VisionUpdateButton(bool isvisiondebug)
        {
            if (isvisiondebug)
            {
                btnReadImage.Visible = true;
                btnMeanThreshold.Visible = true;
            }
            else
            {
                btnReadImage.Visible = false;
                btnMeanThreshold.Visible = false;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="txtimg"></param>
        /// <returns></returns>
        private bool CheckImgFile(TextBox txtimg)
        {
            if (string.IsNullOrEmpty(txtimg.Text.Trim()))
            {
                ShowMsg show = new ShowMsg();
                show.ShowMessageBoxTimeout("Please select image file first.", "Not Find Image File", MessageBoxButtons.OK, MessageBoxIcon.Information, 1000); //单位毫秒
                txtVisionImgFile.Focus();
                return false;
            }

            if (!File.Exists(txtVisionImgFile.Text.Trim()))
            {
                ShowMsg show = new ShowMsg();
                show.ShowMessageBoxTimeout("You select image file is not exists,retry it.", "Not Find Image File", MessageBoxButtons.OK, MessageBoxIcon.Information, 1000); //单位毫秒
                txtVisionImgFile.SelectAll();
                txtVisionImgFile.Focus();
                return false;
            }
            return true;
        }

        private void btnReadImage_Click(object sender, EventArgs e)
        {



            return;
            ShowMessageInternal(MeaageType.Begin, "test");

            if (!anyThreadIsRuning)
                startAnalyzePictureThread();


            return;
            if (!CheckImgFile(txtVisionImgFile))
                return;
            btnReadImage.Cursor = Cursors.WaitCursor;
            int[] MaxThresh = new int[2];
            GetSuggestionMaxValue(txtVisionImgFile.Text.Trim(), out MaxThresh);
            comboMinGray.SelectedIndex = MaxThresh[0];
            comboMaxGray.SelectedIndex = MaxThresh[1];
            btnReadImage.Cursor = Cursors.Default;

        }

        private void  GetSuggestionMaxValue(string file,out int[] maxthresh)
        {
            maxthresh = new int[2];
            HObject ho_Image = new HObject(), ho_ImageMean = new HObject(), ho_ImageGray = new HObject(), ho_ImagePart = new HObject();
            HOperatorSet.GenEmptyObj(out ho_Image);
            ho_Image.Dispose();
            HOperatorSet.ReadImage(out ho_Image, file );
            //VisionResizeImage(ho_Image, false);
            HOperatorSet.DispObj(ho_Image, hwindow);


            //  獲取建議value
            HObject ho_ImageEmphasize = new HObject();
            HOperatorSet.GenEmptyObj(out ho_ImageMean);
            HOperatorSet.GenEmptyObj(out ho_ImageGray);
            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize);
            HOperatorSet.GenEmptyObj(out ho_ImagePart);
            ho_ImagePart.Dispose();
            ho_ImageGray.Dispose();
            HOperatorSet.Emphasize(ho_Image, out ho_ImageEmphasize, 7, 7, 1);
            HOperatorSet.Rgb3ToGray(ho_ImageEmphasize, ho_ImageEmphasize, ho_ImageEmphasize, out ho_ImageGray);
            ho_ImageMean.Dispose();

            
            HTuple hv_AbsoluteHisto = new HTuple(), hv_RelativeHisto = new HTuple();
            HTuple hv_MinThresh = new HTuple(), hv_MaxThresh = new HTuple();
            HOperatorSet.MeanImage(ho_ImageGray, out ho_ImageMean, 9, 9);
            hv_AbsoluteHisto.Dispose(); hv_RelativeHisto.Dispose();
            HOperatorSet.GrayHisto(ho_ImageMean, ho_ImageMean, out hv_AbsoluteHisto, out hv_RelativeHisto);
            hv_MinThresh.Dispose(); hv_MaxThresh.Dispose();
            HOperatorSet.HistoToThresh(hv_RelativeHisto, 20, out hv_MinThresh, out hv_MaxThresh);
            ho_ImageMean.Dispose();
            ho_ImageGray.Dispose();
            ho_ImageEmphasize.Dispose();
            maxthresh[0] = (int)hv_MaxThresh.TupleSelect(0);
            maxthresh[1] = (int)hv_MaxThresh.TupleSelect(1);

        }

        private void GetSuggestionMinValue(string file, out int[] minthresh)
        {
            minthresh = new int[2];
            HObject ho_Image = new HObject(), ho_ImageMean = new HObject(), ho_ImageGray = new HObject(), ho_ImagePart = new HObject();
            HOperatorSet.GenEmptyObj(out ho_Image);
            ho_Image.Dispose();
            HOperatorSet.ReadImage(out ho_Image, file );
            //VisionResizeImage(ho_Image, false);
            HOperatorSet.DispObj(ho_Image, hwindow);


            //  獲取建議value
            HObject ho_ImageEmphasize = new HObject();
            HOperatorSet.GenEmptyObj(out ho_ImageMean);
            HOperatorSet.GenEmptyObj(out ho_ImageGray);
            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize);
            HOperatorSet.GenEmptyObj(out ho_ImagePart);
            ho_ImagePart.Dispose();
            ho_ImageGray.Dispose();
            HOperatorSet.Emphasize(ho_Image, out ho_ImageEmphasize, 7, 7, 1);
            HOperatorSet.Rgb3ToGray(ho_ImageEmphasize, ho_ImageEmphasize, ho_ImageEmphasize, out ho_ImageGray);
            ho_ImageMean.Dispose();


            HTuple hv_AbsoluteHisto = new HTuple(), hv_RelativeHisto = new HTuple();
            HTuple hv_MinThresh = new HTuple(), hv_MaxThresh = new HTuple();
            HOperatorSet.MeanImage(ho_ImageGray, out ho_ImageMean, 9, 9);
            hv_AbsoluteHisto.Dispose(); hv_RelativeHisto.Dispose();
            HOperatorSet.GrayHisto(ho_ImageMean, ho_ImageMean, out hv_AbsoluteHisto, out hv_RelativeHisto);
            hv_MinThresh.Dispose(); hv_MaxThresh.Dispose();
            HOperatorSet.HistoToThresh(hv_RelativeHisto, 20, out hv_MinThresh, out hv_MaxThresh);
            ho_ImageMean.Dispose();
            ho_ImageGray.Dispose();
            ho_ImageEmphasize.Dispose();
            minthresh[0] = (int)hv_MinThresh.TupleSelect(0);
            minthresh[1] = (int)hv_MinThresh.TupleSelect(1);

        }

        #region Auto Close Messagebox

        public class CloseState
        {
            private int _Timeout;

            /// <summary>
            /// In millisecond
            /// </summary>
            public int Timeout
            {
                get
                {
                    return _Timeout;
                }
            }

            private string _Caption;

            /// <summary>
            /// Caption of dialog
            /// </summary>
            public string Caption
            {
                get
                {
                    return _Caption;
                }
            }

            public CloseState(string caption, int timeout)
            {
                _Timeout = timeout;
                _Caption = caption;
            }
        }

        public class ShowMsg  //自动关闭提示框
        {
            [DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

            [DllImport("user32.dll")]
            static extern bool EndDialog(IntPtr hDlg, out IntPtr nResult);

            //三个参数：1、文本提示-text，2、提示框标题-caption，3、按钮类型-MessageBoxButtons ，4、自动消失时间设置-timeout
            public void ShowMessageBoxTimeout(string text, string caption,
                MessageBoxButtons buttons, int timeout)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(CloseMessageBox),
                    new CloseState(caption, timeout));
                MessageBox.Show(text, caption, buttons);
            }

            public void ShowMessageBoxTimeout(string text, string caption,
            MessageBoxButtons buttons,MessageBoxIcon icons, int timeout)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(CloseMessageBox),
                    new CloseState(caption, timeout));
                MessageBox.Show(text, caption, buttons, icons);
            }



            private static void CloseMessageBox(object state)
            {
                CloseState closeState = state as CloseState;

                Thread.Sleep(closeState.Timeout);
                IntPtr dlg = FindWindow(null, closeState.Caption);

                if (dlg != IntPtr.Zero)
                {
                    IntPtr result;
                    EndDialog(dlg, out result);
                }
            }
        }

        #endregion

        private void btnMeanThreshold_Click(object sender, EventArgs e)
        {
            if (!CheckImgFile(txtVisionImgFile))
                return;

            btnMeanThreshold.Cursor = Cursors.WaitCursor;

            HOperatorSet.ClearWindow(hwindow);
            HObject ho_Image = new HObject(), ho_ImageGray = new HObject();
            HOperatorSet.GenEmptyObj(out ho_Image);
            ho_Image.Dispose();
            HOperatorSet.ReadImage(out ho_Image, txtVisionImgFile.Text.Trim());


            HObject ho_ImageEmphasize = new HObject();

            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize);
            ho_ImageEmphasize.Dispose();
            HOperatorSet.Emphasize(ho_Image, out ho_ImageEmphasize, 7, 7, 1);
            //
            HOperatorSet.GenEmptyObj(out ho_ImageGray);
            ho_ImageGray.Dispose();
            HOperatorSet.Rgb3ToGray(ho_ImageEmphasize, ho_ImageEmphasize, ho_ImageEmphasize, out ho_ImageGray);
            //VisionResizeImage(ho_ImageGray, false);
            HOperatorSet.DispObj(ho_ImageGray, hwindow);
            ho_Image.Dispose();


            HOperatorSet.SetDraw(hwindow, "margin");
            HOperatorSet.SetColor(hwindow, "red");
            HOperatorSet.SetLineWidth(hwindow, 1);


            HObject ho_Region = new HObject(), ho_ConnectedRegions = new HObject(), ho_SelectedRegions = new HObject();
            HObject ho_RegionFillUp = new HObject(), ho_RegionClosing = new HObject(), ho_RegionTrans = new HObject();
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans);

            HTuple hv_Area = new HTuple(), hv__ = new HTuple();
            HOperatorSet.Threshold(ho_ImageGray, out ho_Region, comboMinGray.SelectedIndex, comboMaxGray.SelectedIndex);
            ho_ConnectedRegions.Dispose();
            HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
            hv_Area.Dispose(); hv__.Dispose(); hv__.Dispose();
            HOperatorSet.AreaCenter(ho_ConnectedRegions, out hv_Area, out hv__, out hv__);
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                    "and", (hv_Area.TupleMax()) - 10, (hv_Area.TupleMax()) + 10);
            }
            ho_RegionFillUp.Dispose();
            HOperatorSet.FillUp(ho_SelectedRegions, out ho_RegionFillUp);
            ho_RegionClosing.Dispose();
            HOperatorSet.ClosingRectangle1(ho_RegionFillUp, out ho_RegionClosing, 10, 10);
            ho_RegionTrans.Dispose();
            HOperatorSet.ShapeTrans(ho_RegionClosing, out ho_RegionTrans, "convex");
            //
            HOperatorSet.DispObj(ho_RegionTrans, hwindow);
            HOperatorSet.ClearObj(ho_RegionTrans);
            HOperatorSet.ClearObj(ho_SelectedRegions);
            HOperatorSet.ClearObj(ho_RegionClosing);

            btnMeanThreshold.Cursor = Cursors.Default;

        }

        private void GetThreshholdRegion(string file, out HObject ho_region,HTuple mingray,HTuple maxgray)
        {

            //

            HObject ho_Image = new HObject(), ho_ImageGray = new HObject();
            HOperatorSet.GenEmptyObj(out ho_Image);
            ho_Image.Dispose();
            HOperatorSet.ReadImage(out ho_Image, file );
            HObject ho_ImageEmphasize = new HObject();
            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize);
            ho_ImageEmphasize.Dispose();
            HOperatorSet.Emphasize(ho_Image, out ho_ImageEmphasize, 7, 7, 1);
            //
            HOperatorSet.GenEmptyObj(out ho_ImageGray);
            ho_ImageGray.Dispose();
            HOperatorSet.Rgb3ToGray(ho_ImageEmphasize, ho_ImageEmphasize, ho_ImageEmphasize, out ho_ImageGray);
            //VisionResizeImage(ho_ImageGray, false);
            HOperatorSet.DispObj(ho_ImageGray, hwindow);
            ho_Image.Dispose();


            HOperatorSet.SetDraw(hwindow, "margin");
            HOperatorSet.SetColor(hwindow, "red");
            HOperatorSet.SetLineWidth(hwindow, 1);

            HOperatorSet.GenEmptyObj(out ho_region);
            ho_region.Dispose();
            HOperatorSet.Threshold(ho_ImageGray, out ho_region, mingray , maxgray);
            ho_ImageGray.Dispose();


        }

        private void btnGetROI_Click(object sender, EventArgs e)
        {
            if (!CheckImgFile(txtVisionImgFile))
                return;

            btnGetROI.Cursor = Cursors.WaitCursor;

            HOperatorSet.ClearWindow(hwindow);
            // Local iconic variables 

            HObject ho_Image, ho_ImageEmphasize, ho_GrayImage;
            HObject ho_Region, ho_ImageRotate, ho_Region1, ho_Rectangle1;
            HObject ho_ImageReduced2, ho_ImagePart1;

            // Local control variables 

            HTuple hv_WindowHandle = new HTuple(), hv_Phi = new HTuple();
            HTuple hv_Row12 = new HTuple(), hv_Column12 = new HTuple();
            HTuple hv_Row22 = new HTuple(), hv_Column22 = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize);
            HOperatorSet.GenEmptyObj(out ho_GrayImage);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ImageRotate);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced2);
            HOperatorSet.GenEmptyObj(out ho_ImagePart1);
            ho_Image.Dispose();
            HOperatorSet.ReadImage(out ho_Image, txtVisionImgFile.Text.Trim());
            hv_WindowHandle.Dispose();
            HOperatorSet.DispObj(ho_Image, hwindow);
            HOperatorSet.SetDraw(hwindow, "margin");
            HOperatorSet.SetColor(hwindow, "green");
            ho_ImageEmphasize.Dispose();
            HOperatorSet.Emphasize(ho_Image, out ho_ImageEmphasize, 7, 7, 1);
            ho_GrayImage.Dispose();
            HOperatorSet.Rgb1ToGray(ho_ImageEmphasize, out ho_GrayImage);
            HOperatorSet.DispObj(ho_GrayImage, hwindow);
            ho_Region.Dispose();
            HOperatorSet.Threshold(ho_GrayImage, out ho_Region, p.MinGray , p.MaxGray );
            HOperatorSet.DispObj(ho_Region, hwindow);
            hv_Phi.Dispose();
            HOperatorSet.OrientationRegion(ho_Region, out hv_Phi);
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_ImageRotate.Dispose();
                HOperatorSet.RotateImage(ho_GrayImage, out ho_ImageRotate, -(hv_Phi.TupleDeg()
                    ), "constant");
            }
            HOperatorSet.SetColor(hwindow, "red");
            ho_Region1.Dispose();
            HOperatorSet.Threshold(ho_ImageRotate, out ho_Region1, 8, 255);
            hv_Row12.Dispose(); hv_Column12.Dispose(); hv_Row22.Dispose(); hv_Column22.Dispose();
            HOperatorSet.InnerRectangle1(ho_Region1, out hv_Row12, out hv_Column12, out hv_Row22,
                out hv_Column22);
            HOperatorSet.SetColor( hwindow, "blue");
            ho_Rectangle1.Dispose();
            HOperatorSet.GenRectangle1(out ho_Rectangle1, hv_Row12, hv_Column12, hv_Row22,
                hv_Column22);
            ho_ImageReduced2.Dispose();
            HOperatorSet.ReduceDomain(ho_ImageRotate, ho_Rectangle1, out ho_ImageReduced2
                );
            ho_ImagePart1.Dispose();
            HOperatorSet.CropDomain(ho_ImageReduced2, out ho_ImagePart1);

            if (File.Exists(p.AppFolder + @"\TEMPROI.bmp"))
                File.Delete(p.AppFolder + @"\TEMPROI.bmp");

            HOperatorSet.WriteImage(ho_ImagePart1, "bmp", 0, p.AppFolder +@"\TEMPROI.bmp");
            HOperatorSet.DispObj(ho_ImagePart1,hwindow);

            btnGetROI.Cursor = Cursors.Default;
        }

        private void comboMult_SelectedIndexChanged(object sender, EventArgs e)
        {
            p.Mult = Convert.ToDouble (comboMult.Text);
            IniFile.IniWriteValue(p.IniSection.PictureSet.ToString(), "Mult", p.Mult, p.IniFilePath);

        }

        private void comboMinGray_SelectedIndexChanged(object sender, EventArgs e)
        {
            p.MinGray = comboMinGray.SelectedIndex;
            IniFile.IniWriteValue(p.IniSection.PictureSet.ToString(), "MinGray", p.MinGray, p.IniFilePath);
        }

        private void comboMaxGray_SelectedIndexChanged(object sender, EventArgs e)
        {
            p.MaxGray = comboMaxGray.SelectedIndex;
            IniFile.IniWriteValue(p.IniSection.PictureSet.ToString(), "MaxGray", p.MaxGray, p.IniFilePath);
        }

        private void comboSigma1_SelectedIndexChanged(object sender, EventArgs e)
        {
            p.Sigma1 = Convert.ToDouble(comboSigma1.Text);
            IniFile.IniWriteValue(p.IniSection.PictureSet.ToString(), "Sigma1",p.Sigma1,p.IniFilePath);
        }

        private void comboSigma2_SelectedIndexChanged(object sender, EventArgs e)
        {
            p.Sigma2 = Convert.ToDouble (comboSigma2.Text);
            IniFile.IniWriteValue(p.IniSection.PictureSet.ToString(), "Sigma2", p.Sigma2, p.IniFilePath);
        }

        private void comboRadius_SelectedIndexChanged(object sender, EventArgs e)
        {
            p.Radius = Convert.ToInt16(comboRadius.Text);
            IniFile.IniWriteValue(p.IniSection.PictureSet.ToString(), "Radius", p.Radius, p.IniFilePath);
        }

        private void txtMinGray_TextChanged(object sender, EventArgs e)
        {
            p.MinGray2 = Convert.ToDouble(txtMinGray.Text);
            IniFile.IniWriteValue(p.IniSection.PictureSet.ToString(), "MinGray2", p.MinGray2, p.IniFilePath);
        }

        private void txtMaxGray_TextChanged(object sender, EventArgs e)
        {
            p.MaxGray2 = Convert.ToDouble(txtMaxGray.Text);
            IniFile.IniWriteValue(p.IniSection.PictureSet.ToString(), "MaxGray2", p.MaxGray2, p.IniFilePath);

        }

        private void txtTopL_TextChanged(object sender, EventArgs e)
        {
            p.Top_L = Convert.ToInt16(txtTopL.Text);
            IniFile.IniWriteValue(p.IniSection.PictureSet.ToString(), "Top_L", p.Top_L, p.IniFilePath);

        }

        private void txtTopR_TextChanged(object sender, EventArgs e)
        {
            p.Top_R = Convert.ToInt16(txtTopR.Text);
            IniFile.IniWriteValue(p.IniSection.PictureSet.ToString(), "Top_R", p.Top_R, p.IniFilePath);
        }

        private void txtBotL_TextChanged(object sender, EventArgs e)
        {
            p.Bot_L = Convert.ToInt16(txtBotL.Text);
            IniFile.IniWriteValue(p.IniSection.PictureSet.ToString(), "Bot_L", p.Bot_L, p.IniFilePath);
        }

        private void txtBotR_TextChanged(object sender, EventArgs e)
        {
            p.Bot_R = Convert.ToInt16(txtBotR.Text);
            IniFile.IniWriteValue(p.IniSection.PictureSet.ToString(), "Bot_R", p.Bot_R, p.IniFilePath);
        }

        private void txtMinArea_TextChanged(object sender, EventArgs e)
        {
            p.MinArea = Convert.ToInt16(txtMinArea.Text);
            IniFile.IniWriteValue(p.IniSection.PictureSet.ToString(), "MinArea", p.MinArea, p.IniFilePath);
        }

        private void txtMaxArea_TextChanged(object sender, EventArgs e)
        {
            p.MaxArea = Convert.ToInt64 (txtMaxArea.Text);
            IniFile.IniWriteValue(p.IniSection.PictureSet.ToString(), "MaxArea", p.MaxArea, p.IniFilePath);
        }

        private void comboAlpha_SelectedIndexChanged(object sender, EventArgs e)
        {
            p.Alpha = Convert.ToDouble(comboAlpha.Text);
            IniFile.IniWriteValue(p.IniSection.PictureSet.ToString(), "Alpha", p.Alpha, p.IniFilePath);
        }


        #region Vision

        public void startAnalyzePictureThread()
        {
            anyThreadIsRuning = true;
            anyThread = new Thread(new ThreadStart(anyTHread));
            anyThread.Start();
        }

        public void anyTHread()
        {
            while (anyThreadIsRuning)
            {
                anyThreadIsRuning = false;

                // Local iconic variables 

                HObject ho_Image, ho_ImageEmphasize, ho_GrayImage;
                HObject ho_Region, ho_ImageRotate, ho_Region1, ho_Rectangle1;
                HObject ho_ImageReduced2, ho_ImagePart1, ho_GsFilter1, ho_GsFilter2;
                HObject ho_Filter, ho_ImageInvert, ho_ImageFFT, ho_ImageConvol;
                HObject ho_ImageFiltered, ho_Rectangle, ho_ROI, ho_ImageMedian;
                HObject ho_ImageSmooth, ho_ConnectedRegions, ho_SelectedRegions;
                HObject ho_Contours;

                // Local control variables 

                HTuple hv_WindowHandle = new HTuple(), hv_Phi = new HTuple();
                HTuple hv_Row12 = new HTuple(), hv_Column12 = new HTuple();
                HTuple hv_Row22 = new HTuple(), hv_Column22 = new HTuple();
                HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
                HTuple hv_Sigma1 = new HTuple(), hv_Sigma2 = new HTuple();
                HTuple hv_Number = new HTuple();
                // Initialize local and output iconic variables 
                HOperatorSet.GenEmptyObj(out ho_Image);
                HOperatorSet.GenEmptyObj(out ho_ImageEmphasize);
                HOperatorSet.GenEmptyObj(out ho_GrayImage);
                HOperatorSet.GenEmptyObj(out ho_Region);
                HOperatorSet.GenEmptyObj(out ho_ImageRotate);
                HOperatorSet.GenEmptyObj(out ho_Region1);
                HOperatorSet.GenEmptyObj(out ho_Rectangle1);
                HOperatorSet.GenEmptyObj(out ho_ImageReduced2);
                HOperatorSet.GenEmptyObj(out ho_ImagePart1);
                HOperatorSet.GenEmptyObj(out ho_GsFilter1);
                HOperatorSet.GenEmptyObj(out ho_GsFilter2);
                HOperatorSet.GenEmptyObj(out ho_Filter);
                HOperatorSet.GenEmptyObj(out ho_ImageInvert);
                HOperatorSet.GenEmptyObj(out ho_ImageFFT);
                HOperatorSet.GenEmptyObj(out ho_ImageConvol);
                HOperatorSet.GenEmptyObj(out ho_ImageFiltered);
                HOperatorSet.GenEmptyObj(out ho_Rectangle);
                HOperatorSet.GenEmptyObj(out ho_ROI);
                HOperatorSet.GenEmptyObj(out ho_ImageMedian);
                HOperatorSet.GenEmptyObj(out ho_ImageSmooth);
                HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
                HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
                HOperatorSet.GenEmptyObj(out ho_Contours);
                HOperatorSet.ClearWindow(hwindow );
                ho_Image.Dispose();
                HOperatorSet.ReadImage(out ho_Image, p.AppCapFolder + @"\lineNG_white.jpg");
                hv_WindowHandle.Dispose();
                HOperatorSet.DispObj(ho_Image, hwindow);
                HOperatorSet.SetDraw(hwindow, "margin");
                HOperatorSet.SetColor(hwindow, "green");
                ho_ImageEmphasize.Dispose();
                HOperatorSet.Emphasize(ho_Image, out ho_ImageEmphasize, 7, 7, 1);
                ho_GrayImage.Dispose();
                HOperatorSet.Rgb1ToGray(ho_ImageEmphasize, out ho_GrayImage);
                HOperatorSet.DispObj(ho_GrayImage, hwindow);
                //DisplayHalconImage(ho_GrayImage);
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_GrayImage, out ho_Region, p.MinGray , p.MaxGray );
                HOperatorSet.ClearWindow(hwindow);
                HOperatorSet.DispObj(ho_Region, hwindow);
                hv_Phi.Dispose();
                HOperatorSet.OrientationRegion(ho_Region, out hv_Phi);
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_ImageRotate.Dispose();
                    HOperatorSet.RotateImage(ho_GrayImage, out ho_ImageRotate, -(hv_Phi.TupleDeg()
                        ), "constant");
                }
                HOperatorSet.SetColor(hwindow, "red");
                ho_Region1.Dispose();
                HOperatorSet.Threshold(ho_ImageRotate, out ho_Region1, p.MinGray , p.MaxGray );
                hv_Row12.Dispose(); hv_Column12.Dispose(); hv_Row22.Dispose(); hv_Column22.Dispose();
                HOperatorSet.InnerRectangle1(ho_Region1, out hv_Row12, out hv_Column12, out hv_Row22,
                    out hv_Column22);
                HOperatorSet.SetColor(hwindow , "blue");
                ho_Rectangle1.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle1, hv_Row12, hv_Column12, hv_Row22,
                    hv_Column22);
                ho_ImageReduced2.Dispose();
                HOperatorSet.ReduceDomain(ho_ImageRotate, ho_Rectangle1, out ho_ImageReduced2
                    );
                ho_ImagePart1.Dispose();
                HOperatorSet.CropDomain(ho_ImageReduced2, out ho_ImagePart1);
                //
                string roifile = p.AppFolder + @"\TEMPROI.bmp";
                if (!File.Exists(roifile))
                    File.Delete(roifile);
                
                HOperatorSet.WriteImage(ho_ImagePart1, "bmp", 0, roifile);

                ho_Image.Dispose();
                HOperatorSet.ReadImage(out ho_Image, roifile );
                //hv_WindowHandle.Dispose();
                //dev_open_window_fit_image(ho_Image, 0, 0, -1, -1, out hv_WindowHandle);
                HOperatorSet.DispObj(ho_Image, hwindow);

                HOperatorSet.SetColored(hwindow, 12);
                hv_Width.Dispose(); hv_Height.Dispose();
                HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);

                hv_Sigma1.Dispose();
                hv_Sigma1 = p.Sigma1;
                hv_Sigma2.Dispose();
                hv_Sigma2 = p.Sigma2;
                ho_GsFilter1.Dispose();
                HOperatorSet.GenGaussFilter(out ho_GsFilter1, hv_Sigma1, hv_Sigma1, 0.0, "none",
                    "rft", hv_Width, hv_Height);

                ho_GsFilter2.Dispose();
                HOperatorSet.GenGaussFilter(out ho_GsFilter2, hv_Sigma2, hv_Sigma2, 0.0, "none",
                    "rft", hv_Width, hv_Height);
                ho_Filter.Dispose();
                HOperatorSet.SubImage(ho_GsFilter1, ho_GsFilter2, out ho_Filter, p.Mult, 0);

                ho_GrayImage.Dispose();
                HOperatorSet.Rgb1ToGray(ho_Image, out ho_GrayImage);
                ho_ImageInvert.Dispose();
                HOperatorSet.InvertImage(ho_GrayImage, out ho_ImageInvert);
                ho_ImageFFT.Dispose();
                HOperatorSet.RftGeneric(ho_ImageInvert, out ho_ImageFFT, "to_freq", "sqrt", "complex",
                    hv_Width);

                ho_ImageConvol.Dispose();
                HOperatorSet.ConvolFft(ho_ImageFFT, ho_Filter, out ho_ImageConvol);

                ho_ImageFiltered.Dispose();
                HOperatorSet.RftGeneric(ho_ImageConvol, out ho_ImageFiltered, "from_freq", "n",
                    "real", hv_Width);

                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_Rectangle.Dispose();
                    //HOperatorSet.GenRectangle1(out ho_Rectangle, 10, 10, hv_Height - 10, hv_Width - 10);
                    Int32 Height = hv_Height.I - p.Bot_L;
                    Int32 Width = hv_Width.I - p.Bot_R;
                    HOperatorSet.GenRectangle1(out ho_Rectangle, p.Top_L, p.Top_R, hv_Height - 10, hv_Width - 10);
                }

                ho_ROI.Dispose();
                HOperatorSet.ReduceDomain(ho_ImageFiltered, ho_Rectangle, out ho_ROI);

                ho_ImageMedian.Dispose();
                HOperatorSet.MedianImage(ho_ROI, out ho_ImageMedian, "circle", p.Radius, "mirrored");

                ho_ImageSmooth.Dispose();
                HOperatorSet.SmoothImage(ho_ROI, out ho_ImageSmooth, "gauss", p.Alpha);


                ho_ImageSmooth.Dispose();
                HOperatorSet.Threshold(ho_ROI, out ho_ImageSmooth, p.MinGray2 , p.MaxGray2 );
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_ImageSmooth, out ho_ConnectedRegions);

                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                    "and", p.MinArea , p.MaxArea );

                ho_Contours.Dispose();
                HOperatorSet.GenContourRegionXld(ho_SelectedRegions, out ho_Contours, "border");

                hv_Number.Dispose();
                HOperatorSet.CountObj(ho_Contours, out hv_Number);

                HOperatorSet.DispObj(ho_Image, hwindow);
                HOperatorSet.DispObj(ho_Contours, hwindow);
           
                if (hv_Number > 0)
                {
                    string msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "->" + "FAIL,Find " + hv_Number + " error(s).";
                    hwindow.WriteString(msg);
                    disp_message(hwindow, msg, "window",20 , 20, "red", "false");

                }
                else
                {
                    string msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "->" + "PASS.";
                    hwindow.WriteString(msg);
                    disp_message(hwindow, msg, "window", 20, 20, "green", "false");
                }



                ho_Image.Dispose();
                ho_ImageEmphasize.Dispose();
                ho_GrayImage.Dispose();
                ho_Region.Dispose();
                ho_ImageRotate.Dispose();
                ho_Region1.Dispose();
                ho_Rectangle1.Dispose();
                ho_ImageReduced2.Dispose();
                ho_ImagePart1.Dispose();
                ho_GsFilter1.Dispose();
                ho_GsFilter2.Dispose();
                ho_Filter.Dispose();
                ho_ImageInvert.Dispose();
                ho_ImageFFT.Dispose();
                ho_ImageConvol.Dispose();
                ho_ImageFiltered.Dispose();
                ho_Rectangle.Dispose();
                ho_ROI.Dispose();
                ho_ImageMedian.Dispose();
                ho_ImageSmooth.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_Contours.Dispose();

                hv_WindowHandle.Dispose();
                hv_Phi.Dispose();
                hv_Row12.Dispose();
                hv_Column12.Dispose();
                hv_Row22.Dispose();
                hv_Column22.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_Sigma1.Dispose();
                hv_Sigma2.Dispose();
                hv_Number.Dispose();


            }
        }


        // Procedures 
        // External procedures 
        // Chapter: Develop
        // Short Description: Open a new graphics window that preserves the aspect ratio of the given image. 
        public void dev_open_window_fit_image(HObject ho_Image, HTuple hv_Row, HTuple hv_Column,
            HTuple hv_WidthLimit, HTuple hv_HeightLimit, out HTuple hv_WindowHandle)
        {




            // Local iconic variables 

            // Local control variables 

            HTuple hv_MinWidth = new HTuple(), hv_MaxWidth = new HTuple();
            HTuple hv_MinHeight = new HTuple(), hv_MaxHeight = new HTuple();
            HTuple hv_ResizeFactor = new HTuple(), hv_ImageWidth = new HTuple();
            HTuple hv_ImageHeight = new HTuple(), hv_TempWidth = new HTuple();
            HTuple hv_TempHeight = new HTuple(), hv_WindowWidth = new HTuple();
            HTuple hv_WindowHeight = new HTuple();
            // Initialize local and output iconic variables 
            hv_WindowHandle = new HTuple();
            //This procedure opens a new graphics window and adjusts the size
            //such that it fits into the limits specified by WidthLimit
            //and HeightLimit, but also maintains the correct image aspect ratio.
            //
            //If it is impossible to match the minimum and maximum extent requirements
            //at the same time (f.e. if the image is very long but narrow),
            //the maximum value gets a higher priority,
            //
            //Parse input tuple WidthLimit
            if ((int)((new HTuple((new HTuple(hv_WidthLimit.TupleLength())).TupleEqual(0))).TupleOr(
                new HTuple(hv_WidthLimit.TupleLess(0)))) != 0)
            {
                hv_MinWidth.Dispose();
                hv_MinWidth = 500;
                hv_MaxWidth.Dispose();
                hv_MaxWidth = 800;
            }
            else if ((int)(new HTuple((new HTuple(hv_WidthLimit.TupleLength())).TupleEqual(
                1))) != 0)
            {
                hv_MinWidth.Dispose();
                hv_MinWidth = 0;
                hv_MaxWidth.Dispose();
                hv_MaxWidth = new HTuple(hv_WidthLimit);
            }
            else
            {
                hv_MinWidth.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_MinWidth = hv_WidthLimit.TupleSelect(
                        0);
                }
                hv_MaxWidth.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_MaxWidth = hv_WidthLimit.TupleSelect(
                        1);
                }
            }
            //Parse input tuple HeightLimit
            if ((int)((new HTuple((new HTuple(hv_HeightLimit.TupleLength())).TupleEqual(0))).TupleOr(
                new HTuple(hv_HeightLimit.TupleLess(0)))) != 0)
            {
                hv_MinHeight.Dispose();
                hv_MinHeight = 400;
                hv_MaxHeight.Dispose();
                hv_MaxHeight = 600;
            }
            else if ((int)(new HTuple((new HTuple(hv_HeightLimit.TupleLength())).TupleEqual(
                1))) != 0)
            {
                hv_MinHeight.Dispose();
                hv_MinHeight = 0;
                hv_MaxHeight.Dispose();
                hv_MaxHeight = new HTuple(hv_HeightLimit);
            }
            else
            {
                hv_MinHeight.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_MinHeight = hv_HeightLimit.TupleSelect(
                        0);
                }
                hv_MaxHeight.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_MaxHeight = hv_HeightLimit.TupleSelect(
                        1);
                }
            }
            //
            //Test, if window size has to be changed.
            hv_ResizeFactor.Dispose();
            hv_ResizeFactor = 1;
            hv_ImageWidth.Dispose(); hv_ImageHeight.Dispose();
            HOperatorSet.GetImageSize(ho_Image, out hv_ImageWidth, out hv_ImageHeight);
            //First, expand window to the minimum extents (if necessary).
            if ((int)((new HTuple(hv_MinWidth.TupleGreater(hv_ImageWidth))).TupleOr(new HTuple(hv_MinHeight.TupleGreater(
                hv_ImageHeight)))) != 0)
            {
                hv_ResizeFactor.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ResizeFactor = (((((hv_MinWidth.TupleReal()
                        ) / hv_ImageWidth)).TupleConcat((hv_MinHeight.TupleReal()) / hv_ImageHeight))).TupleMax()
                        ;
                }
            }
            hv_TempWidth.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_TempWidth = hv_ImageWidth * hv_ResizeFactor;
            }
            hv_TempHeight.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_TempHeight = hv_ImageHeight * hv_ResizeFactor;
            }
            //Then, shrink window to maximum extents (if necessary).
            if ((int)((new HTuple(hv_MaxWidth.TupleLess(hv_TempWidth))).TupleOr(new HTuple(hv_MaxHeight.TupleLess(
                hv_TempHeight)))) != 0)
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_ResizeFactor = hv_ResizeFactor * ((((((hv_MaxWidth.TupleReal()
                            ) / hv_TempWidth)).TupleConcat((hv_MaxHeight.TupleReal()) / hv_TempHeight))).TupleMin()
                            );
                        hv_ResizeFactor.Dispose();
                        hv_ResizeFactor = ExpTmpLocalVar_ResizeFactor;
                    }
                }
            }
            hv_WindowWidth.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_WindowWidth = hv_ImageWidth * hv_ResizeFactor;
            }
            hv_WindowHeight.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_WindowHeight = hv_ImageHeight * hv_ResizeFactor;
            }
            //Resize window
            //dev_open_window(...);
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                HOperatorSet.SetPart(hwindow, 0, 0, hv_ImageHeight - 1, hv_ImageWidth - 1);
            }

            hv_MinWidth.Dispose();
            hv_MaxWidth.Dispose();
            hv_MinHeight.Dispose();
            hv_MaxHeight.Dispose();
            hv_ResizeFactor.Dispose();
            hv_ImageWidth.Dispose();
            hv_ImageHeight.Dispose();
            hv_TempWidth.Dispose();
            hv_TempHeight.Dispose();
            hv_WindowWidth.Dispose();
            hv_WindowHeight.Dispose();

            return;
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {

            string roifile = p.AppFolder + @"\TEMPROI.bmp";
            if (!File.Exists(roifile))
                return;
            btnAnalyze.Cursor = Cursors.WaitCursor;
            HOperatorSet.ClearWindow(hwindow);
            // Local iconic variables 
            HObject ho_Image, ho_GsFilter1, ho_GsFilter2;
            HObject ho_Filter, ho_GrayImage, ho_ImageInvert, ho_ImageFFT;
            HObject ho_ImageConvol, ho_ImageFiltered, ho_Rectangle;
            HObject ho_ROI, ho_ImageMedian, ho_ImageSmooth, ho_ConnectedRegions;
            HObject ho_SelectedRegions, ho_Contours;

            // Local control variables 
            HTuple hv_WindowHandle = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(), hv_Sigma1 = new HTuple();
            HTuple hv_Sigma2 = new HTuple(), hv_Number = new HTuple();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_GsFilter1);
            HOperatorSet.GenEmptyObj(out ho_GsFilter2);
            HOperatorSet.GenEmptyObj(out ho_Filter);
            HOperatorSet.GenEmptyObj(out ho_GrayImage);
            HOperatorSet.GenEmptyObj(out ho_ImageInvert);
            HOperatorSet.GenEmptyObj(out ho_ImageFFT);
            HOperatorSet.GenEmptyObj(out ho_ImageConvol);
            HOperatorSet.GenEmptyObj(out ho_ImageFiltered);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ROI);
            HOperatorSet.GenEmptyObj(out ho_ImageMedian);
            HOperatorSet.GenEmptyObj(out ho_ImageSmooth);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_Contours);
            ho_Image.Dispose();
            HOperatorSet.ReadImage(out ho_Image, roifile);

            HOperatorSet.DispObj(ho_Image, hwindow);
            HOperatorSet.SetDraw(hwindow, "margin");
            HOperatorSet.SetLineWidth(hwindow, 1);
            HOperatorSet.SetColored(hwindow, 12);


            hv_Width.Dispose(); hv_Height.Dispose();
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);


            hv_Sigma1.Dispose();
            hv_Sigma1 = Convert.ToDouble(comboSigma1.Text);// 10.0;
            hv_Sigma2.Dispose();
            hv_Sigma2 = Convert.ToDouble(comboSigma2.Text); ;//2.0;

            ho_GsFilter1.Dispose();
            HOperatorSet.GenGaussFilter(out ho_GsFilter1, hv_Sigma1, hv_Sigma1, 0.0, "none",
                "rft", hv_Width, hv_Height);

            ho_GsFilter2.Dispose();
            HOperatorSet.GenGaussFilter(out ho_GsFilter2, hv_Sigma2, hv_Sigma2, 0.0, "none",
                "rft", hv_Width, hv_Height);
            ho_Filter.Dispose();
            //HOperatorSet.SubImage(ho_GsFilter1, ho_GsFilter2, out ho_Filter, 1, 0);
            HOperatorSet.SubImage(ho_GsFilter1, ho_GsFilter2, out ho_Filter, Convert.ToDouble(comboMult.Text), 0);

            ho_GrayImage.Dispose();
            HOperatorSet.Rgb1ToGray(ho_Image, out ho_GrayImage);
            ho_ImageInvert.Dispose();
            HOperatorSet.InvertImage(ho_GrayImage, out ho_ImageInvert);

            ho_ImageFFT.Dispose();
            HOperatorSet.RftGeneric(ho_ImageInvert, out ho_ImageFFT, "to_freq", "sqrt", "complex",
                hv_Width);

            ho_ImageConvol.Dispose();
            HOperatorSet.ConvolFft(ho_ImageFFT, ho_Filter, out ho_ImageConvol);

            ho_ImageFiltered.Dispose();
            HOperatorSet.RftGeneric(ho_ImageConvol, out ho_ImageFiltered, "from_freq", "n",
                "real", hv_Width);

            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_Rectangle.Dispose();

                Int32 Height = hv_Height.I - Convert.ToInt32(txtBotL.Text);
                Int32 Width = hv_Width.I - Convert.ToInt32(txtBotR.Text);

                HOperatorSet.GenRectangle1(out ho_Rectangle, Convert.ToInt16(txtTopL.Text), Convert.ToInt16(txtTopR.Text), Height, Width);
                // HOperatorSet.GenRectangle1(out ho_Rectangle, 10,10, hv_Height-10, hv_Width-10);
            }

            ho_ROI.Dispose();
            HOperatorSet.ReduceDomain(ho_ImageFiltered, ho_Rectangle, out ho_ROI);

            ho_ImageMedian.Dispose();
            HOperatorSet.MedianImage(ho_ROI, out ho_ImageMedian, "circle", Convert.ToInt16(comboRadius.Text), "mirrored");

            ho_ImageSmooth.Dispose();
            HOperatorSet.SmoothImage(ho_ROI, out ho_ImageSmooth, "gauss", Convert.ToDouble(comboAlpha.Text));

            ho_ImageSmooth.Dispose();
            // HOperatorSet.Threshold(ho_ROI, out ho_ImageSmooth, -0.012866, -0.005549);
            double MinGray = Convert.ToDouble(txtMinGray.Text);
            double MaxGray = Convert.ToDouble(txtMaxGray.Text);

            HOperatorSet.Threshold(ho_ROI, out ho_ImageSmooth, MinGray, MaxGray);

            ho_ConnectedRegions.Dispose();
            HOperatorSet.Connection(ho_ImageSmooth, out ho_ConnectedRegions);


            ho_SelectedRegions.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                "and", Convert.ToInt64(txtMinArea.Text), Convert.ToInt64(txtMaxArea.Text));

            ho_Contours.Dispose();
            HOperatorSet.GenContourRegionXld(ho_SelectedRegions, out ho_Contours, "border");

            hv_Number.Dispose();
            HOperatorSet.CountObj(ho_Contours, out hv_Number);
            HOperatorSet.DispObj(ho_Contours, hwindow);

            ////hwindow.WriteString("HAHAHA");
            //hwindow.

            if (hv_Number > 0)
            {
                string msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "->" + "FAIL,Find " + hv_Number + " error(s).";
                hwindow.WriteString(msg);
                disp_message(hwindow, msg, "window", 12, 12, "red", "false");

            }
            else
            {
                string msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "->" + "PASS.";
                hwindow.WriteString(msg);
                disp_message(hwindow, msg, "window", 12, 12, "green", "false");
            }


            ho_Image.Dispose();
            ho_GsFilter1.Dispose();
            ho_GsFilter2.Dispose();
            ho_Filter.Dispose();
            ho_GrayImage.Dispose();
            ho_ImageInvert.Dispose();
            ho_ImageFFT.Dispose();
            ho_ImageConvol.Dispose();
            ho_ImageFiltered.Dispose();
            ho_Rectangle.Dispose();
            ho_ROI.Dispose();
            ho_ImageMedian.Dispose();
            ho_ImageSmooth.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_SelectedRegions.Dispose();
            ho_Contours.Dispose();

            hv_WindowHandle.Dispose();
            hv_Width.Dispose();
            hv_Height.Dispose();
            hv_Sigma1.Dispose();
            hv_Sigma2.Dispose();
            hv_Number.Dispose();
            btnAnalyze.Cursor = Cursors.Default;

        }


        public void disp_message(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem,
    HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_GenParamName = new HTuple(), hv_GenParamValue = new HTuple();
            HTuple hv_Color_COPY_INP_TMP = new HTuple(hv_Color);
            HTuple hv_Column_COPY_INP_TMP = new HTuple(hv_Column);
            HTuple hv_CoordSystem_COPY_INP_TMP = new HTuple(hv_CoordSystem);
            HTuple hv_Row_COPY_INP_TMP = new HTuple(hv_Row);
            // Initialize local and output iconic variables 
            //This procedure displays text in a graphics window.
            //
            //Input parameters:
            //WindowHandle: The WindowHandle of the graphics window, where
            //   the message should be displayed
            //String: A tuple of strings containing the text message to be displayed
            //CoordSystem: If set to 'window', the text position is given
            //   with respect to the window coordinate system.
            //   If set to 'image', image coordinates are used.
            //   (This may be useful in zoomed images.)
            //Row: The row coordinate of the desired text position
            //   A tuple of values is allowed to display text at different
            //   positions.
            //Column: The column coordinate of the desired text position
            //   A tuple of values is allowed to display text at different
            //   positions.
            //Color: defines the color of the text as string.
            //   If set to [], '' or 'auto' the currently set color is used.
            //   If a tuple of strings is passed, the colors are used cyclically...
            //   - if |Row| == |Column| == 1: for each new textline
            //   = else for each text position.
            //Box: If Box[0] is set to 'true', the text is written within an orange box.
            //     If set to' false', no box is displayed.
            //     If set to a color string (e.g. 'white', '#FF00CC', etc.),
            //       the text is written in a box of that color.
            //     An optional second value for Box (Box[1]) controls if a shadow is displayed:
            //       'true' -> display a shadow in a default color
            //       'false' -> display no shadow
            //       otherwise -> use given string as color string for the shadow color
            //
            //It is possible to display multiple text strings in a single call.
            //In this case, some restrictions apply:
            //- Multiple text positions can be defined by specifying a tuple
            //  with multiple Row and/or Column coordinates, i.e.:
            //  - |Row| == n, |Column| == n
            //  - |Row| == n, |Column| == 1
            //  - |Row| == 1, |Column| == n
            //- If |Row| == |Column| == 1,
            //  each element of String is display in a new textline.
            //- If multiple positions or specified, the number of Strings
            //  must match the number of positions, i.e.:
            //  - Either |String| == n (each string is displayed at the
            //                          corresponding position),
            //  - or     |String| == 1 (The string is displayed n times).
            //
            //
            //Convert the parameters for disp_text.
            if ((int)((new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(new HTuple())))) != 0)
            {

                hv_Color_COPY_INP_TMP.Dispose();
                hv_Column_COPY_INP_TMP.Dispose();
                hv_CoordSystem_COPY_INP_TMP.Dispose();
                hv_Row_COPY_INP_TMP.Dispose();
                hv_GenParamName.Dispose();
                hv_GenParamValue.Dispose();

                return;
            }
            if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Row_COPY_INP_TMP.Dispose();
                hv_Row_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Column_COPY_INP_TMP.Dispose();
                hv_Column_COPY_INP_TMP = 12;
            }
            //
            //Convert the parameter Box to generic parameters.
            hv_GenParamName.Dispose();
            hv_GenParamName = new HTuple();
            hv_GenParamValue.Dispose();
            hv_GenParamValue = new HTuple();
            if ((int)(new HTuple((new HTuple(hv_Box.TupleLength())).TupleGreater(0))) != 0)
            {
                if ((int)(new HTuple(((hv_Box.TupleSelect(0))).TupleEqual("false"))) != 0)
                {
                    //Display no box
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleConcat(
                                "box");
                            hv_GenParamName.Dispose();
                            hv_GenParamName = ExpTmpLocalVar_GenParamName;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleConcat(
                                "false");
                            hv_GenParamValue.Dispose();
                            hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
                        }
                    }
                }
                else if ((int)(new HTuple(((hv_Box.TupleSelect(0))).TupleNotEqual("true"))) != 0)
                {
                    //Set a color other than the default.
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleConcat(
                                "box_color");
                            hv_GenParamName.Dispose();
                            hv_GenParamName = ExpTmpLocalVar_GenParamName;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleConcat(
                                hv_Box.TupleSelect(0));
                            hv_GenParamValue.Dispose();
                            hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
                        }
                    }
                }
            }
            if ((int)(new HTuple((new HTuple(hv_Box.TupleLength())).TupleGreater(1))) != 0)
            {
                if ((int)(new HTuple(((hv_Box.TupleSelect(1))).TupleEqual("false"))) != 0)
                {
                    //Display no shadow.
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleConcat(
                                "shadow");
                            hv_GenParamName.Dispose();
                            hv_GenParamName = ExpTmpLocalVar_GenParamName;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleConcat(
                                "false");
                            hv_GenParamValue.Dispose();
                            hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
                        }
                    }
                }
                else if ((int)(new HTuple(((hv_Box.TupleSelect(1))).TupleNotEqual("true"))) != 0)
                {
                    //Set a shadow color other than the default.
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleConcat(
                                "shadow_color");
                            hv_GenParamName.Dispose();
                            hv_GenParamName = ExpTmpLocalVar_GenParamName;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleConcat(
                                hv_Box.TupleSelect(1));
                            hv_GenParamValue.Dispose();
                            hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
                        }
                    }
                }
            }
            //Restore default CoordSystem behavior.
            if ((int)(new HTuple(hv_CoordSystem_COPY_INP_TMP.TupleNotEqual("window"))) != 0)
            {
                hv_CoordSystem_COPY_INP_TMP.Dispose();
                hv_CoordSystem_COPY_INP_TMP = "image";
            }
            //
            if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(""))) != 0)
            {
                //disp_text does not accept an empty string for Color.
                hv_Color_COPY_INP_TMP.Dispose();
                hv_Color_COPY_INP_TMP = new HTuple();
            }
            //
            HOperatorSet.DispText(hv_WindowHandle, hv_String, hv_CoordSystem_COPY_INP_TMP,
                hv_Row_COPY_INP_TMP, hv_Column_COPY_INP_TMP, hv_Color_COPY_INP_TMP, hv_GenParamName,
                hv_GenParamValue);

            hv_Color_COPY_INP_TMP.Dispose();
            hv_Column_COPY_INP_TMP.Dispose();
            hv_CoordSystem_COPY_INP_TMP.Dispose();
            hv_Row_COPY_INP_TMP.Dispose();
            hv_GenParamName.Dispose();
            hv_GenParamValue.Dispose();

            return;
        }

        
        #endregion

        //private void tlpInspection_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        //{
        //    Pen p = new Pen(Color.Blue);
        //    e.Graphics.DrawRectangle(p, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.X + e.CellBounds.Width - 1, e.CellBounds.Y + e.CellBounds.Height - 1);
        //}

        //private void tlpInspection_Paint(object sender, PaintEventArgs e)
        //{
        //    Pen p = new Pen(Color.Blue);
        //    e.Graphics.DrawRectangle(p, e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.X + e.ClipRectangle.Width - 0, e.ClipRectangle.Y + e.ClipRectangle.Height - 0);

        //}

        #region Wsdl


        public enum MeaageType
        {
            Begin,
            Success,
            Failure,
            Warning,
            Error
        }



        private void ShowMessageInternal( MeaageType status, string message)
        {

            if (message == null)
                message = status.ToString();
            this.Invoke((EventHandler)(delegate
            {
                switch (status)
                {
                    case MeaageType.Begin:
                        this.richMessage.SelectionColor = Color.Blue;
                        this.richMessage.AppendText(DateTime.Now.ToString("HH:mm:ss") + " " + message + "\n");
                        this.richMessage.Update();
                        break;
                    case MeaageType.Success:
                        this.richMessage.SelectionColor = Color.Green;
                        this.richMessage.AppendText(DateTime.Now.ToString("HH:mm:ss") + " " + message + "\n");
                        this.richMessage.Update();
                        break;
                    case MeaageType.Failure:
                        this.richMessage.SelectionColor = Color.Red;
                        this.richMessage.AppendText(DateTime.Now.ToString("HH:mm:ss") + " " + message + "\n");
                        this.richMessage.Update();
                        break;
                    case MeaageType.Warning:
                        this.richMessage.SelectionColor = Color.DarkRed;
                        this.richMessage.AppendText(DateTime.Now.ToString("HH:mm:ss") + " " + message + "\n");
                        this.richMessage.Update();
                        break;
                    case MeaageType.Error:
                        this.richMessage.SelectionColor = Color.Red;
                        this.richMessage.AppendText(DateTime.Now.ToString("HH:mm:ss") + " " + message + "\n");
                        this.richMessage.Update();
                        break;
                    default:
                        break;
                }
                if (richMessage.TextLength > richMessage.MaxLength - 1000)
                    richMessage.Clear();
                richMessage.SelectionStart = richMessage.TextLength;
                richMessage.ScrollToCaret();
            }));
 
        }



        #endregion


        #region WebService


         /// <summary>
        /// 实例化WebServices
        /// </summary>
        /// <param name="url">WebServices地址</param>
        /// <param name="methodname">调用的方法</param>
        /// <param name="args">把webservices里需要的参数按顺序放到这个object[]里</param>
        public static object InvokeWebService(string url, string methodname, object[] args)
        {
 
            //这里的namespace是需引用的webservices的命名空间，我没有改过，也可以使用。也可以加一个参数从外面传进来。
            string @namespace = "client";
            try
            {
                //获取WSDL
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(url + "?WSDL");
                ServiceDescription sd = ServiceDescription.Read(stream);
                string classname = sd.Services[0].Name;
                ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
                sdi.AddServiceDescription(sd, "", "");
                CodeNamespace cn = new CodeNamespace(@namespace);
 
                //生成客户端代理类代码
                CodeCompileUnit ccu = new CodeCompileUnit();
                ccu.Namespaces.Add(cn);
                sdi.Import(cn, ccu);
                CSharpCodeProvider csc = new CSharpCodeProvider();
                //ICodeCompiler icc = csc.CreateCompiler();
 
                //设定编译参数
                CompilerParameters cplist = new CompilerParameters();
                cplist.GenerateExecutable = false;
                cplist.GenerateInMemory = true;
                cplist.ReferencedAssemblies.Add("System.dll");
                cplist.ReferencedAssemblies.Add("System.XML.dll");
                cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                cplist.ReferencedAssemblies.Add("System.Data.dll");
 
                //编译代理类
                CompilerResults cr = csc.CompileAssemblyFromDom(cplist, ccu);
                if (true == cr.Errors.HasErrors)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
                    {
                        sb.Append(ce.ToString());
                        sb.Append(System.Environment.NewLine);
                    }
                    throw new Exception(sb.ToString());
                }
 
                //生成代理实例，并调用方法
                System.Reflection.Assembly assembly = cr.CompiledAssembly;
                Type t = assembly.GetType(@namespace + "." + classname, true, true);
                object obj = Activator.CreateInstance(t);
                System.Reflection.MethodInfo mi = t.GetMethod(methodname);
 
                return mi.Invoke(obj, args);
            }
            catch  (Exception e)
            {
                throw e;
               // return null;
    
            }
        }

        private void txtTestSN_TextChanged(object sender, EventArgs e)
        {
            p.TestSN = txtTestSN.Text.ToUpper().Trim();
            IniFile.IniWriteValue(p.IniSection.WebSet.ToString(), "TestSN", p.TestSN, p.IniFilePath);
        }

        private void txtStage_TextChanged(object sender, EventArgs e)
        {
            p.Stage = txtStage.Text.ToUpper().Trim();
            IniFile.IniWriteValue(p.IniSection.WebSet.ToString(), "Stage",p.Stage , p.IniFilePath);
        }

        private void txtWebService_TextChanged(object sender, EventArgs e)
        {
            p.WebSite = txtWebService.Text.Trim();
            IniFile.IniWriteValue(p.IniSection.WebSet.ToString(), "WebSite", p.WebSite, p.IniFilePath);
        }

        private void chkUseWebService_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseWebService.Checked)
                p.UseWebService = "1";
            else
                p.UseWebService = "0";
            IniFile.IniWriteValue(p.IniSection.WebSet.ToString(), "UseWebService", p.UseWebService , p.IniFilePath);

        }

        private void chkTestWebService_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTestWebService.Checked)
                p.UseTestSN = "1";
            else
                p.UseTestSN = "0";
            IniFile.IniWriteValue(p.IniSection.WebSet.ToString(), "UseTestSN", p.UseTestSN , p.IniFilePath);
        }

        private void chkUseCamera_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseCamera.Checked)
                p.UseCamera = "1";
            else
                p.UseCamera = "0";
            IniFile.IniWriteValue(p.IniSection.Capture.ToString(), "UseCamera", p.UseCamera, p.IniFilePath);
        }

        private void txtSN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar ==13)
            {
                SetListText(lstSN, txtSN.Text.Trim ().ToUpper ());
                ShowMessageInternal(MeaageType.Success, "SN:" + txtSN.Text.Trim().ToUpper());
                if (p.UseWebService == "1")
                {
                    string sn = txtSN.Text.ToUpper().Trim();
                    if (!string.IsNullOrEmpty(sn) && !string.IsNullOrEmpty(p.Stage))
                        LoadInfoFromWebService(sn, p.Stage);
                }
                txtSN.Text = "";
            }
        }

        #endregion

        #region USB


        public struct  Usb
        {
            public string DeviceID;
            public String PNPDeviceID;      // 设备ID
            public String Name;             // 设备名称
            public String Description;      // 设备描述
           // public String Service;          // 服务
           // public String Status;           // 设备状态
            //public UInt16 VendorID;         // 供应商标识
            //public UInt16 ProductID;        // 产品编号 
            //public Guid ClassGuid;          // 设备安装类GUID
        }    


        public static List<Usb> GetUSBDevices()
        {
            List<Usb> devices = new List<Usb>();

            ManagementObjectCollection collection;
            string sql = "Select * From Win32_USBHub";
            sql = @"SELECT * FROM Win32_SerialPort";
            //sql = @"SELECT * FROM Win32_USBControllerDevice";
           // sql = @"SELECT * FROM Win32_PnPEntity WHERE name LIKE 'USB Serial Converter'"; //串口
            using (var searcher = new ManagementObjectSearcher(@sql))

            {
                collection = searcher.Get();
            }
            
            foreach (var device in collection)
            {
                Usb _usb = new Usb();
                _usb.DeviceID  = (string)device.GetPropertyValue("DeviceID");
                _usb.PNPDeviceID = (string)device.GetPropertyValue("PNPDeviceID");
                _usb.Description = (string) device.GetPropertyValue("Description");
                _usb.Name = (string)device.GetPropertyValue("Name");
                //devices.Add(((string) device.GetPropertyValue("DeviceID"),
                //    (string) device.GetPropertyValue("PNPDeviceID"),
                //    (string) device.GetPropertyValue("Description")));
                devices.Add(_usb);
            }

            collection.Dispose();
            return devices;

        }


        private void GetSerialPort(ComboBox cbx)
        {
            ComList = new Dictionary<string, string>();
            cbx.Items.Clear();
            RegistryKey hklm = Registry.LocalMachine;
            RegistryKey software = hklm.OpenSubKey("HARDWARE");
            RegistryKey no1 = software.OpenSubKey("DEVICEMAP");
            RegistryKey no2 = no1.OpenSubKey("SERIALCOMM");
            string[] linesplit = no2.GetValueNames();
            if (linesplit.Length > 0)
            {
                for (int i = 0; i < linesplit.Length; i++)
                {
                    ComList.Add(no2.GetValue(linesplit[i]).ToString(), linesplit[i]);
                    cbx.Items.Add(no2.GetValue(linesplit[i]));
                }
                if (cbx.Items.Count > 0)
                {
                    if (string.IsNullOrEmpty(p.ComPort))
                        cbx.SelectedIndex = 0;
                    else
                        cbx.Text = p.ComPort;
                }
                else
                    cbx.Text = p.ComPort;
            }
        }


        #endregion

        private void comboPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string com = comboPort.Text;
            string cominfo = string.Empty;
            if (!string.IsNullOrEmpty(com))
            {
                if (ComList.TryGetValue(com, out cominfo))
                    txtComDeviceInfo.Text = cominfo;
            }

            p.ComPort = com;
            IniFile.IniWriteValue(p.IniSection.ComSet.ToString(), "ComPort", p.ComPort, p.IniFilePath);

        }

        private void btnRefreshCom_Click(object sender, EventArgs e)
        {
            GetSerialPort(comboPort);
        }

        private void chkUseCom_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseCom.Checked)
                p.UseComPort = "1";
            else
                p.UseComPort = "0";
            IniFile.IniWriteValue(p.IniSection.ComSet.ToString(), "UseComPort", p.UseComPort, p.IniFilePath);
        }

        private void chkCapture1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCapture1.Checked)
                p.UseCapture1  = "1";
            else
                p.UseCapture1  = "0";
            IniFile.IniWriteValue(p.IniSection.ComSet.ToString(), "UseCapture1", p.UseComPort, p.IniFilePath);
        }

        private void chkCapture2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCapture2.Checked)
                p.UseCapture2 = "1";
            else
                p.UseCapture2 = "0";
            IniFile.IniWriteValue(p.IniSection.ComSet.ToString(), "UseCapture1", p.UseComPort, p.IniFilePath);
        }

        private void txtCapture1Signal_TextChanged(object sender, EventArgs e)
        {
            p.Capture1Signal = txtCapture1Signal.Text;
            IniFile.IniWriteValue(p.IniSection.ComSet.ToString(), "Capture1Signal", p.Capture1Signal, p.IniFilePath);
        }

        private void txtCapture2Signal_TextChanged(object sender, EventArgs e)
        {
            p.Capture2Signal = txtCapture2Signal.Text;
            IniFile.IniWriteValue(p.IniSection.ComSet.ToString(), "Capture2Signal", p.Capture2Signal, p.IniFilePath);
        }

        private void chkUseNet_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseNet.Checked)
                p.UseNet = "1";
            else
                p.UseNet = "0";
            IniFile.IniWriteValue(p.IniSection.NetSet.ToString(), "UseNet", p.UseNet, p.IniFilePath);
        }

        private void txtIP_TextChanged(object sender, EventArgs e)
        {
            p.IP = txtIP.Text.Trim();
            IniFile.IniWriteValue(p.IniSection.NetSet.ToString(), "IP", p.IP, p.IniFilePath);
        }

        private void txtPort_TextChanged(object sender, EventArgs e)
        {
            p.Port = txtPort.Text.Trim();
            IniFile.IniWriteValue(p.IniSection.NetSet.ToString(), "Port", p.Port, p.IniFilePath);
        }


        #region  Socket

        private void ReceiveData()
        {
            //listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //IPEndPoint ipEP = new IPEndPoint(IPAddress.Parse(p.IP), Convert.ToInt16(p.Port));
            ////listenSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            //listenSocket.Bind(ipEP);
            //listenSocket.Listen(100);
            while (listenThreadIsRuning)
            {
                try
                {
                   // listener.Start();
                    ShowMessageInternal(MeaageType.Begin, "Waiting for Net Message... ");
                    byte[] buffer = new byte[1024];
                    //Socket 
                    //Socket ss = listenSocket.Accept();
                    Socket ss = listener.AcceptSocket();
                    ss.Receive(buffer);
                    string str = Encoding.UTF8.GetString(buffer).Trim().Replace("\0", "");
                    // SetListText(str);
                    SetListText(lstSN, str);
                    ShowMessageInternal(MeaageType.Success, "SN:" + str);
                    this.Invoke((EventHandler)(delegate
                    {
                        txtSN.Text = str;
                        if (p.UseWebService == "1")
                        {
                            string sn = txtSN.Text.ToUpper().Trim();
                            if (!string.IsNullOrEmpty(sn) && !string.IsNullOrEmpty(p.Stage))
                                LoadInfoFromWebService(sn, p.Stage);
                        }
                    }));
                }
                catch (Exception)
                {
                    
                    //throw;
                }
            }
            
        }

        #endregion

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (serialPort1.BytesToRead == 0)
                return;
            try
            {
                string str = serialPort1.ReadExisting();
                serialPort1.DiscardInBuffer();
                str = str.Trim();
                this.Invoke((EventHandler)(delegate {

                    ShowMessageInternal(MeaageType.Begin, "MCU->PC:" + str);
                    
                    if (str == p.Capture1Signal && p.UseCapture1 == "1")
                    {
                        ShowMessageInternal(MeaageType.Begin, "Capture 1st picture...");
                        isCapture1stPicture = true;
                    }
                    if (str == p.Capture2Signal && p.UseCapture2 == "1")
                    {
                        ShowMessageInternal(MeaageType.Begin, "Capture 1st picture...");
                        isCapture2ndPicture = true;
                    }

                
                }));
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// 檢查文件是否存在,存在刪除文件
        /// </summary>
        /// <param name="filepath">文件所在的完整路徑</param>
        /// <param name="filename">文件名稱</param>
        /// <returns></returns>
        private bool CheckFileExistDeleteFile(string filepath,string filename)
        {
            if (File.Exists(filepath))
            {
                ShowMessageInternal(MeaageType.Warning, filename  + " is exist,delete the file.");
                try
                {
                    File.Delete(filepath);
                    ShowMessageInternal(MeaageType.Success, "Delete " + filename + " sucess...");
                    return true;
                }
                catch (Exception e)
                {
                    ShowMessageInternal(MeaageType.Error , "Failed to delete "  + filename);
                    ShowMessageInternal(MeaageType.Error, e.Message);
                    return false;
                }
                
            }
            return false;
        }

        private void chkAnalysisImg_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAnalysisImg.Checked)
                p.AnalysisPicture = "1";
            else
                p.AnalysisPicture = "0";
            IniFile.IniWriteValue(p.IniSection.Capture.ToString(), "AnalysisPicture", p.AnalysisPicture, p.IniFilePath);
        }


    }
}
