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


namespace LCD_SVS
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            gpanel = display.CreateGraphics();
        }

        public Thread acqThread;
        bool acqThreadIsRuning = false;
        bool acqIsCapturePicture = false;

        
        public string CurrentID = "";
        private Graphics gpanel;
        delegate void SetStatusCallBack();
        delegate void SetdisplayCallBack();
        delegate void treeUpdateCallBack();
        treeUpdateCallBack treeUpdate = null;
        string feature_info = null;
        private Rectangle outRectangle;
        Camera sv_cam = null;
        public Bitmap[] display_img_rgb = new Bitmap[4];
        public Bitmap[] display_img_mono = new Bitmap[4];
        private bool newsize = false;


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
                text.AppendFormat(" Select camera!");
                this.textBox_Result.Text = text.ToString();
                this.textBox_Result.ForeColor = Color.Green;

            }
            else
            {
                this.textBox_Result.Text = "No cameras found!";
                SetListText("No cameras found!");
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
            if (sv_cam == null)
            {
                SetListText("Select Camera first please.");
                return;
            }
            buttonStart.Cursor = Cursors.WaitCursor;

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

            buttonStop.Visible = true;
            buttonStart.Visible = false;
            buttonStart.Cursor = Cursors.Default;
           // updateViewTree();
            buttonStart.Cursor = Cursors.Default;
            btnCapture.Visible = true;
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
                    }
                }

                this.sv_cam.destdataIndex++;
                if (this.sv_cam.destdataIndex == 4)
                    this.sv_cam.destdataIndex = 0;
            }

        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (sv_cam == null)
                return;

            if (!acqThreadIsRuning)
                return;

            buttonStop.Cursor = Cursors.WaitCursor;
            clearControl();
            sv_cam.threadIsRuning = false;
            acqThreadIsRuning = false;
            acqThread.Join();
            sv_cam.acquisitionStop();
            buttonStop.Visible = false;
            buttonStart.Visible = true;
            buttonStop.Cursor = Cursors.Default;
            acqThread.Abort();
            Array.Clear(sv_cam.imagebufferMono, 0, 4);
            Array.Clear(sv_cam.imagebufferRGB, 0, 4);
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
        private void SetListText(String text)
        {
            if (this.lstCapMsg .InvokeRequired)
            {
                this.lstCapMsg.BeginInvoke(new Action<String>((msg) =>
                {
                    this.lstCapMsg.Items.Add( DateTime.Now.ToString ("HH:mm:ss") +"->" + msg);
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
                this.lstCapMsg.Items.Add(DateTime.Now.ToString("HH:mm:ss") + "->" + text);
                if (lstCapMsg.Items.Count > 0)
                    lstCapMsg.SelectedIndex = lstCapMsg.Items.Count - 1;
                if (lstCapMsg.Items.Count > 600)
                    lstCapMsg.Items.RemoveAt(0);
                Graphics g = lstCapMsg.CreateGraphics();
                int hzSize = (int)g.MeasureString(lstCapMsg.Items[lstCapMsg.Items.Count - 1].ToString(), lstCapMsg.Font).Width;
                lstCapMsg.HorizontalExtent = hzSize;
            }
        }

        private void txtImgFile_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
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











    }
}
