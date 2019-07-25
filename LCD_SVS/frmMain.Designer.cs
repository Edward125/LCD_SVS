﻿namespace LCD_SVS
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonDiscover = new System.Windows.Forms.Button();
            this.CamSelectComboBox = new System.Windows.Forms.ComboBox();
            this.textBox_Result = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonStop = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.panelAcquisition = new System.Windows.Forms.Panel();
            this.display = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonQuit = new System.Windows.Forms.Button();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabCamera = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lstCapMsg = new System.Windows.Forms.ListBox();
            this.btnCapture = new System.Windows.Forms.Button();
            this.tabCapturePicture = new System.Windows.Forms.TabPage();
            this.panelCapture = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.comboSizeMode = new System.Windows.Forms.ComboBox();
            this.txtImgFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.picCapturePicture = new System.Windows.Forms.PictureBox();
            this.tabVision = new System.Windows.Forms.TabPage();
            this.hSmartWindowControl1 = new HalconDotNet.HSmartWindowControl();
            this.tabSetting = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNGImgFolder = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOKImgFolder = new System.Windows.Forms.TextBox();
            this.chkTestNGSavePictures = new System.Windows.Forms.CheckBox();
            this.chkTestOKSavePictures = new System.Windows.Forms.CheckBox();
            this.txtVisionImgFile = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnMeanThreshold = new System.Windows.Forms.Button();
            this.btnStartDebug = new System.Windows.Forms.Button();
            this.btnReadImage = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboMinGray = new System.Windows.Forms.ComboBox();
            this.comboMaxGray = new System.Windows.Forms.ComboBox();
            this.btnGetROI = new System.Windows.Forms.Button();
            this.panelAcquisition.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabCapturePicture.SuspendLayout();
            this.panelCapture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCapturePicture)).BeginInit();
            this.tabVision.SuspendLayout();
            this.tabSetting.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonDiscover
            // 
            this.buttonDiscover.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDiscover.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buttonDiscover.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDiscover.Location = new System.Drawing.Point(9, 6);
            this.buttonDiscover.Name = "buttonDiscover";
            this.buttonDiscover.Size = new System.Drawing.Size(276, 31);
            this.buttonDiscover.TabIndex = 12;
            this.buttonDiscover.Text = "Discover Cameras ";
            this.buttonDiscover.UseVisualStyleBackColor = false;
            this.buttonDiscover.Click += new System.EventHandler(this.buttonDiscover_Click);
            // 
            // CamSelectComboBox
            // 
            this.CamSelectComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CamSelectComboBox.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.CamSelectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CamSelectComboBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CamSelectComboBox.FormattingEnabled = true;
            this.CamSelectComboBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.CamSelectComboBox.Location = new System.Drawing.Point(9, 75);
            this.CamSelectComboBox.Name = "CamSelectComboBox";
            this.CamSelectComboBox.Size = new System.Drawing.Size(276, 23);
            this.CamSelectComboBox.TabIndex = 11;
            this.CamSelectComboBox.SelectedIndexChanged += new System.EventHandler(this.CamSelectComboBox_SelectedIndexChanged);
            // 
            // textBox_Result
            // 
            this.textBox_Result.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Result.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox_Result.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Result.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBox_Result.Location = new System.Drawing.Point(9, 43);
            this.textBox_Result.Name = "textBox_Result";
            this.textBox_Result.ReadOnly = true;
            this.textBox_Result.Size = new System.Drawing.Size(276, 27);
            this.textBox_Result.TabIndex = 13;
            this.textBox_Result.Text = " ";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonStop
            // 
            this.buttonStop.BackColor = System.Drawing.Color.LightGray;
            this.buttonStop.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStop.Location = new System.Drawing.Point(77, 105);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(65, 42);
            this.buttonStop.TabIndex = 20;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = false;
            this.buttonStop.Visible = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(150, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 14);
            this.label6.TabIndex = 24;
            this.label6.Text = "image ID";
            // 
            // buttonStart
            // 
            this.buttonStart.BackColor = System.Drawing.Color.LightGray;
            this.buttonStart.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStart.Location = new System.Drawing.Point(9, 105);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(65, 42);
            this.buttonStart.TabIndex = 11;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // panelAcquisition
            // 
            this.panelAcquisition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAcquisition.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panelAcquisition.Controls.Add(this.label6);
            this.panelAcquisition.Controls.Add(this.display);
            this.panelAcquisition.Controls.Add(this.label1);
            this.panelAcquisition.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelAcquisition.Location = new System.Drawing.Point(3, 6);
            this.panelAcquisition.Name = "panelAcquisition";
            this.panelAcquisition.Size = new System.Drawing.Size(651, 612);
            this.panelAcquisition.TabIndex = 23;
            // 
            // display
            // 
            this.display.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.display.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.display.AutoScroll = true;
            this.display.BackColor = System.Drawing.SystemColors.WindowText;
            this.display.Enabled = false;
            this.display.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.display.Location = new System.Drawing.Point(8, 34);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(640, 575);
            this.display.TabIndex = 23;
            this.display.Resize += new System.EventHandler(this.display_Resize);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "Acquisition";
            // 
            // buttonQuit
            // 
            this.buttonQuit.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonQuit.Location = new System.Drawing.Point(228, 107);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(57, 40);
            this.buttonQuit.TabIndex = 21;
            this.buttonQuit.Text = "Quit";
            this.buttonQuit.UseVisualStyleBackColor = true;
            this.buttonQuit.Click += new System.EventHandler(this.buttonQuit_Click);
            // 
            // tabMain
            // 
            this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMain.Controls.Add(this.tabCamera);
            this.tabMain.Controls.Add(this.tabCapturePicture);
            this.tabMain.Controls.Add(this.tabVision);
            this.tabMain.Controls.Add(this.tabSetting);
            this.tabMain.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabMain.Location = new System.Drawing.Point(12, 12);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(974, 651);
            this.tabMain.TabIndex = 24;
            // 
            // tabCamera
            // 
            this.tabCamera.Controls.Add(this.splitContainer1);
            this.tabCamera.Location = new System.Drawing.Point(4, 23);
            this.tabCamera.Name = "tabCamera";
            this.tabCamera.Padding = new System.Windows.Forms.Padding(3);
            this.tabCamera.Size = new System.Drawing.Size(966, 624);
            this.tabCamera.TabIndex = 0;
            this.tabCamera.Text = "Camera";
            this.tabCamera.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelAcquisition);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lstCapMsg);
            this.splitContainer1.Panel2.Controls.Add(this.btnCapture);
            this.splitContainer1.Panel2.Controls.Add(this.buttonDiscover);
            this.splitContainer1.Panel2.Controls.Add(this.buttonQuit);
            this.splitContainer1.Panel2.Controls.Add(this.buttonStop);
            this.splitContainer1.Panel2.Controls.Add(this.textBox_Result);
            this.splitContainer1.Panel2.Controls.Add(this.CamSelectComboBox);
            this.splitContainer1.Panel2.Controls.Add(this.buttonStart);
            this.splitContainer1.Size = new System.Drawing.Size(960, 618);
            this.splitContainer1.SplitterDistance = 657;
            this.splitContainer1.TabIndex = 0;
            // 
            // lstCapMsg
            // 
            this.lstCapMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCapMsg.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstCapMsg.FormattingEnabled = true;
            this.lstCapMsg.HorizontalScrollbar = true;
            this.lstCapMsg.ItemHeight = 14;
            this.lstCapMsg.Location = new System.Drawing.Point(3, 161);
            this.lstCapMsg.Name = "lstCapMsg";
            this.lstCapMsg.Size = new System.Drawing.Size(293, 438);
            this.lstCapMsg.TabIndex = 23;
            // 
            // btnCapture
            // 
            this.btnCapture.BackColor = System.Drawing.Color.LightGray;
            this.btnCapture.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapture.Location = new System.Drawing.Point(148, 105);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(74, 42);
            this.btnCapture.TabIndex = 22;
            this.btnCapture.Text = "Capture";
            this.btnCapture.UseVisualStyleBackColor = false;
            this.btnCapture.Visible = false;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // tabCapturePicture
            // 
            this.tabCapturePicture.Controls.Add(this.panelCapture);
            this.tabCapturePicture.Location = new System.Drawing.Point(4, 23);
            this.tabCapturePicture.Name = "tabCapturePicture";
            this.tabCapturePicture.Padding = new System.Windows.Forms.Padding(3);
            this.tabCapturePicture.Size = new System.Drawing.Size(966, 624);
            this.tabCapturePicture.TabIndex = 1;
            this.tabCapturePicture.Text = "Capture";
            this.tabCapturePicture.UseVisualStyleBackColor = true;
            // 
            // panelCapture
            // 
            this.panelCapture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCapture.AutoScroll = true;
            this.panelCapture.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panelCapture.Controls.Add(this.label3);
            this.panelCapture.Controls.Add(this.comboSizeMode);
            this.panelCapture.Controls.Add(this.txtImgFile);
            this.panelCapture.Controls.Add(this.label2);
            this.panelCapture.Controls.Add(this.picCapturePicture);
            this.panelCapture.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelCapture.Location = new System.Drawing.Point(3, 3);
            this.panelCapture.Name = "panelCapture";
            this.panelCapture.Size = new System.Drawing.Size(960, 618);
            this.panelCapture.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "Image Display Mode:";
            // 
            // comboSizeMode
            // 
            this.comboSizeMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSizeMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboSizeMode.FormattingEnabled = true;
            this.comboSizeMode.Items.AddRange(new object[] {
            "Normal",
            "StretchImage",
            "AutoSize",
            "CenterImage",
            "Zoom"});
            this.comboSizeMode.Location = new System.Drawing.Point(131, 10);
            this.comboSizeMode.Name = "comboSizeMode";
            this.comboSizeMode.Size = new System.Drawing.Size(144, 22);
            this.comboSizeMode.TabIndex = 3;
            this.comboSizeMode.SelectedIndexChanged += new System.EventHandler(this.comboSizeMode_SelectedIndexChanged);
            // 
            // txtImgFile
            // 
            this.txtImgFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImgFile.Location = new System.Drawing.Point(359, 10);
            this.txtImgFile.Name = "txtImgFile";
            this.txtImgFile.Size = new System.Drawing.Size(596, 22);
            this.txtImgFile.TabIndex = 2;
            this.txtImgFile.DoubleClick += new System.EventHandler(this.txtImgFile_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(287, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Image File:";
            // 
            // picCapturePicture
            // 
            this.picCapturePicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picCapturePicture.BackColor = System.Drawing.Color.Black;
            this.picCapturePicture.Location = new System.Drawing.Point(3, 38);
            this.picCapturePicture.Name = "picCapturePicture";
            this.picCapturePicture.Size = new System.Drawing.Size(954, 578);
            this.picCapturePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picCapturePicture.TabIndex = 0;
            this.picCapturePicture.TabStop = false;
            // 
            // tabVision
            // 
            this.tabVision.Controls.Add(this.btnGetROI);
            this.tabVision.Controls.Add(this.comboMaxGray);
            this.tabVision.Controls.Add(this.comboMinGray);
            this.tabVision.Controls.Add(this.label9);
            this.tabVision.Controls.Add(this.label8);
            this.tabVision.Controls.Add(this.btnReadImage);
            this.tabVision.Controls.Add(this.btnStartDebug);
            this.tabVision.Controls.Add(this.btnMeanThreshold);
            this.tabVision.Controls.Add(this.label7);
            this.tabVision.Controls.Add(this.txtVisionImgFile);
            this.tabVision.Controls.Add(this.hSmartWindowControl1);
            this.tabVision.Location = new System.Drawing.Point(4, 23);
            this.tabVision.Name = "tabVision";
            this.tabVision.Size = new System.Drawing.Size(966, 624);
            this.tabVision.TabIndex = 3;
            this.tabVision.Text = "Vision";
            this.tabVision.UseVisualStyleBackColor = true;
            // 
            // hSmartWindowControl1
            // 
            this.hSmartWindowControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hSmartWindowControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.hSmartWindowControl1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.hSmartWindowControl1.HDoubleClickToFitContent = true;
            this.hSmartWindowControl1.HDrawingObjectsModifier = HalconDotNet.HSmartWindowControl.DrawingObjectsModifier.None;
            this.hSmartWindowControl1.HImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hSmartWindowControl1.HKeepAspectRatio = true;
            this.hSmartWindowControl1.HMoveContent = true;
            this.hSmartWindowControl1.HZoomContent = HalconDotNet.HSmartWindowControl.ZoomContent.WheelForwardZoomsIn;
            this.hSmartWindowControl1.Location = new System.Drawing.Point(0, 28);
            this.hSmartWindowControl1.Margin = new System.Windows.Forms.Padding(0);
            this.hSmartWindowControl1.Name = "hSmartWindowControl1";
            this.hSmartWindowControl1.Size = new System.Drawing.Size(798, 596);
            this.hSmartWindowControl1.TabIndex = 0;
            this.hSmartWindowControl1.WindowSize = new System.Drawing.Size(798, 596);
            this.hSmartWindowControl1.Resize += new System.EventHandler(this.hSmartWindowControl1_Resize);
            // 
            // tabSetting
            // 
            this.tabSetting.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tabSetting.Controls.Add(this.groupBox1);
            this.tabSetting.Location = new System.Drawing.Point(4, 23);
            this.tabSetting.Name = "tabSetting";
            this.tabSetting.Size = new System.Drawing.Size(966, 624);
            this.tabSetting.TabIndex = 2;
            this.tabSetting.Text = "Setting";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtNGImgFolder);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtOKImgFolder);
            this.groupBox1.Controls.Add(this.chkTestNGSavePictures);
            this.groupBox1.Controls.Add(this.chkTestOKSavePictures);
            this.groupBox1.Location = new System.Drawing.Point(3, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(469, 111);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Capture Setting";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 14);
            this.label5.TabIndex = 5;
            this.label5.Text = "NG Img Folder:";
            // 
            // txtNGImgFolder
            // 
            this.txtNGImgFolder.Location = new System.Drawing.Point(99, 73);
            this.txtNGImgFolder.Name = "txtNGImgFolder";
            this.txtNGImgFolder.Size = new System.Drawing.Size(352, 22);
            this.txtNGImgFolder.TabIndex = 4;
            this.txtNGImgFolder.TextChanged += new System.EventHandler(this.txtNGImgFolder_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "OK Img Folder:";
            // 
            // txtOKImgFolder
            // 
            this.txtOKImgFolder.Location = new System.Drawing.Point(99, 45);
            this.txtOKImgFolder.Name = "txtOKImgFolder";
            this.txtOKImgFolder.Size = new System.Drawing.Size(352, 22);
            this.txtOKImgFolder.TabIndex = 2;
            this.txtOKImgFolder.TextChanged += new System.EventHandler(this.txtOKImgFolder_TextChanged);
            // 
            // chkTestNGSavePictures
            // 
            this.chkTestNGSavePictures.AutoSize = true;
            this.chkTestNGSavePictures.Location = new System.Drawing.Point(172, 21);
            this.chkTestNGSavePictures.Name = "chkTestNGSavePictures";
            this.chkTestNGSavePictures.Size = new System.Drawing.Size(141, 18);
            this.chkTestNGSavePictures.TabIndex = 1;
            this.chkTestNGSavePictures.Text = "Test NG Save Pictures";
            this.chkTestNGSavePictures.UseVisualStyleBackColor = true;
            this.chkTestNGSavePictures.CheckedChanged += new System.EventHandler(this.chkTestNGSavePictures_CheckedChanged);
            // 
            // chkTestOKSavePictures
            // 
            this.chkTestOKSavePictures.AutoSize = true;
            this.chkTestOKSavePictures.Location = new System.Drawing.Point(11, 21);
            this.chkTestOKSavePictures.Name = "chkTestOKSavePictures";
            this.chkTestOKSavePictures.Size = new System.Drawing.Size(139, 18);
            this.chkTestOKSavePictures.TabIndex = 0;
            this.chkTestOKSavePictures.Text = "Test OK Save Pictures";
            this.chkTestOKSavePictures.UseVisualStyleBackColor = true;
            this.chkTestOKSavePictures.CheckedChanged += new System.EventHandler(this.chkTestOKSavePictures_CheckedChanged);
            // 
            // txtVisionImgFile
            // 
            this.txtVisionImgFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVisionImgFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtVisionImgFile.Location = new System.Drawing.Point(60, 3);
            this.txtVisionImgFile.Name = "txtVisionImgFile";
            this.txtVisionImgFile.Size = new System.Drawing.Size(738, 22);
            this.txtVisionImgFile.TabIndex = 1;
            this.txtVisionImgFile.DoubleClick += new System.EventHandler(this.txtVisionImgFile_DoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 14);
            this.label7.TabIndex = 2;
            this.label7.Text = "Img File:";
            // 
            // btnMeanThreshold
            // 
            this.btnMeanThreshold.BackColor = System.Drawing.Color.LightGray;
            this.btnMeanThreshold.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMeanThreshold.Location = new System.Drawing.Point(816, 165);
            this.btnMeanThreshold.Name = "btnMeanThreshold";
            this.btnMeanThreshold.Size = new System.Drawing.Size(125, 32);
            this.btnMeanThreshold.TabIndex = 13;
            this.btnMeanThreshold.Text = "Threshold";
            this.btnMeanThreshold.UseVisualStyleBackColor = false;
            this.btnMeanThreshold.Visible = false;
            this.btnMeanThreshold.Click += new System.EventHandler(this.btnMeanThreshold_Click);
            // 
            // btnStartDebug
            // 
            this.btnStartDebug.BackColor = System.Drawing.Color.LightGray;
            this.btnStartDebug.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartDebug.Location = new System.Drawing.Point(816, 28);
            this.btnStartDebug.Name = "btnStartDebug";
            this.btnStartDebug.Size = new System.Drawing.Size(125, 32);
            this.btnStartDebug.TabIndex = 14;
            this.btnStartDebug.Text = "Start Debug";
            this.btnStartDebug.UseVisualStyleBackColor = false;
            this.btnStartDebug.Click += new System.EventHandler(this.btnStartDebug_Click);
            // 
            // btnReadImage
            // 
            this.btnReadImage.BackColor = System.Drawing.Color.LightGray;
            this.btnReadImage.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReadImage.Location = new System.Drawing.Point(816, 66);
            this.btnReadImage.Name = "btnReadImage";
            this.btnReadImage.Size = new System.Drawing.Size(125, 32);
            this.btnReadImage.TabIndex = 15;
            this.btnReadImage.Text = "Read Image";
            this.btnReadImage.UseVisualStyleBackColor = false;
            this.btnReadImage.Visible = false;
            this.btnReadImage.Click += new System.EventHandler(this.btnReadImage_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(814, 107);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 14);
            this.label8.TabIndex = 16;
            this.label8.Text = "Min Gray:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(813, 135);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 14);
            this.label9.TabIndex = 17;
            this.label9.Text = "Max Gray:";
            // 
            // comboMinGray
            // 
            this.comboMinGray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMinGray.FormattingEnabled = true;
            this.comboMinGray.Location = new System.Drawing.Point(875, 104);
            this.comboMinGray.Name = "comboMinGray";
            this.comboMinGray.Size = new System.Drawing.Size(66, 22);
            this.comboMinGray.TabIndex = 18;
            // 
            // comboMaxGray
            // 
            this.comboMaxGray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMaxGray.FormattingEnabled = true;
            this.comboMaxGray.Location = new System.Drawing.Point(875, 132);
            this.comboMaxGray.Name = "comboMaxGray";
            this.comboMaxGray.Size = new System.Drawing.Size(66, 22);
            this.comboMaxGray.TabIndex = 19;
            // 
            // btnGetROI
            // 
            this.btnGetROI.BackColor = System.Drawing.Color.LightGray;
            this.btnGetROI.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetROI.Location = new System.Drawing.Point(817, 202);
            this.btnGetROI.Name = "btnGetROI";
            this.btnGetROI.Size = new System.Drawing.Size(125, 32);
            this.btnGetROI.TabIndex = 20;
            this.btnGetROI.Text = "Get ROI";
            this.btnGetROI.UseVisualStyleBackColor = false;
            this.btnGetROI.Click += new System.EventHandler(this.btnGetROI_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 675);
            this.Controls.Add(this.tabMain);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panelAcquisition.ResumeLayout(false);
            this.panelAcquisition.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabCamera.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabCapturePicture.ResumeLayout(false);
            this.panelCapture.ResumeLayout(false);
            this.panelCapture.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCapturePicture)).EndInit();
            this.tabVision.ResumeLayout(false);
            this.tabVision.PerformLayout();
            this.tabSetting.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonDiscover;
        private System.Windows.Forms.ComboBox CamSelectComboBox;
        private System.Windows.Forms.TextBox textBox_Result;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Panel panelAcquisition;
        private System.Windows.Forms.Button buttonQuit;
        private System.Windows.Forms.Panel display;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabCamera;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabPage tabCapturePicture;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.ListBox lstCapMsg;
        private System.Windows.Forms.Panel panelCapture;
        private System.Windows.Forms.PictureBox picCapturePicture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboSizeMode;
        private System.Windows.Forms.TextBox txtImgFile;
        private System.Windows.Forms.TabPage tabSetting;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkTestOKSavePictures;
        private System.Windows.Forms.CheckBox chkTestNGSavePictures;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOKImgFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNGImgFolder;
        private System.Windows.Forms.TabPage tabVision;
        private HalconDotNet.HSmartWindowControl hSmartWindowControl1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtVisionImgFile;
        private System.Windows.Forms.Button btnMeanThreshold;
        private System.Windows.Forms.Button btnStartDebug;
        private System.Windows.Forms.Button btnReadImage;
        private System.Windows.Forms.ComboBox comboMinGray;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboMaxGray;
        private System.Windows.Forms.Button btnGetROI;
    }
}

