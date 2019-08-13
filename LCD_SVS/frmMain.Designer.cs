namespace LCD_SVS
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
            this.txtBotR = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtBotL = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtMaxGray = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtMinGray = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtMaxArea = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtMinArea = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtTopR = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtTopL = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.lblAlpha = new System.Windows.Forms.Label();
            this.comboAlpha = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.comboRadius = new System.Windows.Forms.ComboBox();
            this.comboMult = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.comboSigma2 = new System.Windows.Forms.ComboBox();
            this.comboSigma1 = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.btnGetROI = new System.Windows.Forms.Button();
            this.comboMaxGray = new System.Windows.Forms.ComboBox();
            this.comboMinGray = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnReadImage = new System.Windows.Forms.Button();
            this.btnMeanThreshold = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtVisionImgFile = new System.Windows.Forms.TextBox();
            this.hSmartWindowControl1 = new HalconDotNet.HSmartWindowControl();
            this.tabInspection = new System.Windows.Forms.TabPage();
            this.tlpInspection = new System.Windows.Forms.TableLayoutPanel();
            this.panelInspection = new System.Windows.Forms.Panel();
            this.lstSN = new System.Windows.Forms.ListBox();
            this.richMessage = new System.Windows.Forms.RichTextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.txtInspectionInfo = new System.Windows.Forms.TextBox();
            this.hSmartWindowControl1st = new HalconDotNet.HSmartWindowControl();
            this.hSmartWindowControl2nd = new HalconDotNet.HSmartWindowControl();
            this.tabSetting = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkUseNet = new System.Windows.Forms.CheckBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnRefreshCom = new System.Windows.Forms.Button();
            this.txtComDeviceInfo = new System.Windows.Forms.TextBox();
            this.txtCapture2Signal = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.chkCapture2 = new System.Windows.Forms.CheckBox();
            this.chkCapture1 = new System.Windows.Forms.CheckBox();
            this.chkUseCom = new System.Windows.Forms.CheckBox();
            this.txtCapture1Signal = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.comboPort = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtStage = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txtTestSN = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.chkTestWebService = new System.Windows.Forms.CheckBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtWebService = new System.Windows.Forms.TextBox();
            this.chkUseWebService = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAnalysisImg = new System.Windows.Forms.CheckBox();
            this.chkUseCamera = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkTestNGSavePictures = new System.Windows.Forms.CheckBox();
            this.chkTestOKSavePictures = new System.Windows.Forms.CheckBox();
            this.txtNGImgFolder = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOKImgFolder = new System.Windows.Forms.TextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.chkTestOKUploadResult = new System.Windows.Forms.CheckBox();
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
            this.tabInspection.SuspendLayout();
            this.tlpInspection.SuspendLayout();
            this.panelInspection.SuspendLayout();
            this.tabSetting.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.CamSelectComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
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
            this.panelAcquisition.Size = new System.Drawing.Size(651, 797);
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
            this.display.Size = new System.Drawing.Size(640, 572);
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
            this.buttonQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.tabMain.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabMain.Controls.Add(this.tabCamera);
            this.tabMain.Controls.Add(this.tabCapturePicture);
            this.tabMain.Controls.Add(this.tabVision);
            this.tabMain.Controls.Add(this.tabInspection);
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
            this.tabCamera.Location = new System.Drawing.Point(4, 26);
            this.tabCamera.Name = "tabCamera";
            this.tabCamera.Padding = new System.Windows.Forms.Padding(3);
            this.tabCamera.Size = new System.Drawing.Size(966, 621);
            this.tabCamera.TabIndex = 0;
            this.tabCamera.Text = "Camera Mode";
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
            this.splitContainer1.Size = new System.Drawing.Size(960, 615);
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
            this.lstCapMsg.Location = new System.Drawing.Point(6, 153);
            this.lstCapMsg.Name = "lstCapMsg";
            this.lstCapMsg.Size = new System.Drawing.Size(293, 634);
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
            this.tabCapturePicture.Location = new System.Drawing.Point(4, 26);
            this.tabCapturePicture.Name = "tabCapturePicture";
            this.tabCapturePicture.Padding = new System.Windows.Forms.Padding(3);
            this.tabCapturePicture.Size = new System.Drawing.Size(966, 621);
            this.tabCapturePicture.TabIndex = 1;
            this.tabCapturePicture.Text = "Capture Display";
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
            this.panelCapture.Size = new System.Drawing.Size(960, 615);
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
            this.tabVision.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tabVision.Controls.Add(this.txtBotR);
            this.tabVision.Controls.Add(this.label20);
            this.tabVision.Controls.Add(this.txtBotL);
            this.tabVision.Controls.Add(this.label21);
            this.tabVision.Controls.Add(this.txtMaxGray);
            this.tabVision.Controls.Add(this.label19);
            this.tabVision.Controls.Add(this.txtMinGray);
            this.tabVision.Controls.Add(this.label18);
            this.tabVision.Controls.Add(this.txtMaxArea);
            this.tabVision.Controls.Add(this.label16);
            this.tabVision.Controls.Add(this.txtMinArea);
            this.tabVision.Controls.Add(this.label17);
            this.tabVision.Controls.Add(this.txtTopR);
            this.tabVision.Controls.Add(this.label15);
            this.tabVision.Controls.Add(this.txtTopL);
            this.tabVision.Controls.Add(this.label14);
            this.tabVision.Controls.Add(this.lblAlpha);
            this.tabVision.Controls.Add(this.comboAlpha);
            this.tabVision.Controls.Add(this.label13);
            this.tabVision.Controls.Add(this.comboRadius);
            this.tabVision.Controls.Add(this.comboMult);
            this.tabVision.Controls.Add(this.label12);
            this.tabVision.Controls.Add(this.comboSigma2);
            this.tabVision.Controls.Add(this.comboSigma1);
            this.tabVision.Controls.Add(this.label11);
            this.tabVision.Controls.Add(this.label10);
            this.tabVision.Controls.Add(this.btnAnalyze);
            this.tabVision.Controls.Add(this.btnGetROI);
            this.tabVision.Controls.Add(this.comboMaxGray);
            this.tabVision.Controls.Add(this.comboMinGray);
            this.tabVision.Controls.Add(this.label9);
            this.tabVision.Controls.Add(this.label8);
            this.tabVision.Controls.Add(this.btnReadImage);
            this.tabVision.Controls.Add(this.btnMeanThreshold);
            this.tabVision.Controls.Add(this.label7);
            this.tabVision.Controls.Add(this.txtVisionImgFile);
            this.tabVision.Controls.Add(this.hSmartWindowControl1);
            this.tabVision.Location = new System.Drawing.Point(4, 26);
            this.tabVision.Name = "tabVision";
            this.tabVision.Size = new System.Drawing.Size(966, 621);
            this.tabVision.TabIndex = 3;
            this.tabVision.Text = "Vision Debug Mode";
            // 
            // txtBotR
            // 
            this.txtBotR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBotR.Location = new System.Drawing.Point(866, 436);
            this.txtBotR.Name = "txtBotR";
            this.txtBotR.Size = new System.Drawing.Size(77, 22);
            this.txtBotR.TabIndex = 47;
            this.txtBotR.Text = "10";
            this.txtBotR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBotR.TextChanged += new System.EventHandler(this.txtBotR_TextChanged);
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(815, 440);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(41, 14);
            this.label20.TabIndex = 46;
            this.label20.Text = "Bot_R:";
            // 
            // txtBotL
            // 
            this.txtBotL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBotL.Location = new System.Drawing.Point(866, 409);
            this.txtBotL.Name = "txtBotL";
            this.txtBotL.Size = new System.Drawing.Size(77, 22);
            this.txtBotL.TabIndex = 45;
            this.txtBotL.Text = "10";
            this.txtBotL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBotL.TextChanged += new System.EventHandler(this.txtBotL_TextChanged);
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(816, 413);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(39, 14);
            this.label21.TabIndex = 44;
            this.label21.Text = "Bot_L:";
            // 
            // txtMaxGray
            // 
            this.txtMaxGray.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxGray.Location = new System.Drawing.Point(866, 328);
            this.txtMaxGray.Name = "txtMaxGray";
            this.txtMaxGray.Size = new System.Drawing.Size(77, 22);
            this.txtMaxGray.TabIndex = 43;
            this.txtMaxGray.Text = "-0.005549";
            this.txtMaxGray.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMaxGray.TextChanged += new System.EventHandler(this.txtMaxGray_TextChanged);
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(812, 332);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 14);
            this.label19.TabIndex = 42;
            this.label19.Text = "MaxGray";
            // 
            // txtMinGray
            // 
            this.txtMinGray.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMinGray.Location = new System.Drawing.Point(866, 301);
            this.txtMinGray.Name = "txtMinGray";
            this.txtMinGray.Size = new System.Drawing.Size(77, 22);
            this.txtMinGray.TabIndex = 41;
            this.txtMinGray.Text = "-0.012866";
            this.txtMinGray.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMinGray.TextChanged += new System.EventHandler(this.txtMinGray_TextChanged);
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(812, 304);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(52, 14);
            this.label18.TabIndex = 40;
            this.label18.Text = "MinGray";
            // 
            // txtMaxArea
            // 
            this.txtMaxArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxArea.Location = new System.Drawing.Point(866, 490);
            this.txtMaxArea.Name = "txtMaxArea";
            this.txtMaxArea.Size = new System.Drawing.Size(77, 22);
            this.txtMaxArea.TabIndex = 39;
            this.txtMaxArea.Text = "99999";
            this.txtMaxArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMaxArea.TextChanged += new System.EventHandler(this.txtMaxArea_TextChanged);
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(811, 493);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(54, 14);
            this.label16.TabIndex = 38;
            this.label16.Text = "MaxArea";
            // 
            // txtMinArea
            // 
            this.txtMinArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMinArea.Location = new System.Drawing.Point(866, 463);
            this.txtMinArea.Name = "txtMinArea";
            this.txtMinArea.Size = new System.Drawing.Size(77, 22);
            this.txtMinArea.TabIndex = 37;
            this.txtMinArea.Text = "800";
            this.txtMinArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMinArea.TextChanged += new System.EventHandler(this.txtMinArea_TextChanged);
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(812, 466);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 14);
            this.label17.TabIndex = 36;
            this.label17.Text = "MinArea";
            // 
            // txtTopR
            // 
            this.txtTopR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTopR.Location = new System.Drawing.Point(866, 382);
            this.txtTopR.Name = "txtTopR";
            this.txtTopR.Size = new System.Drawing.Size(77, 22);
            this.txtTopR.TabIndex = 35;
            this.txtTopR.Text = "10";
            this.txtTopR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTopR.TextChanged += new System.EventHandler(this.txtTopR_TextChanged);
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(815, 386);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(42, 14);
            this.label15.TabIndex = 34;
            this.label15.Text = "Top_R:";
            // 
            // txtTopL
            // 
            this.txtTopL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTopL.Location = new System.Drawing.Point(866, 355);
            this.txtTopL.Name = "txtTopL";
            this.txtTopL.Size = new System.Drawing.Size(77, 22);
            this.txtTopL.TabIndex = 33;
            this.txtTopL.Text = "10";
            this.txtTopL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTopL.TextChanged += new System.EventHandler(this.txtTopL_TextChanged);
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(816, 359);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(40, 14);
            this.label14.TabIndex = 32;
            this.label14.Text = "Top_L:";
            // 
            // lblAlpha
            // 
            this.lblAlpha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAlpha.AutoSize = true;
            this.lblAlpha.Location = new System.Drawing.Point(816, 276);
            this.lblAlpha.Name = "lblAlpha";
            this.lblAlpha.Size = new System.Drawing.Size(42, 14);
            this.lblAlpha.TabIndex = 31;
            this.lblAlpha.Text = "Alpha:";
            // 
            // comboAlpha
            // 
            this.comboAlpha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboAlpha.FormattingEnabled = true;
            this.comboAlpha.Items.AddRange(new object[] {
            "0.1",
            "0.2",
            "0.3",
            "0.5",
            "1.0",
            "1.5",
            "2.0",
            "2.5",
            "3.0",
            "4.0",
            "5.0",
            "7.0",
            "10.0"});
            this.comboAlpha.Location = new System.Drawing.Point(867, 274);
            this.comboAlpha.Name = "comboAlpha";
            this.comboAlpha.Size = new System.Drawing.Size(76, 22);
            this.comboAlpha.TabIndex = 30;
            this.comboAlpha.Text = "1.0";
            this.comboAlpha.SelectedIndexChanged += new System.EventHandler(this.comboAlpha_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(813, 248);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 14);
            this.label13.TabIndex = 29;
            this.label13.Text = "Radius:";
            // 
            // comboRadius
            // 
            this.comboRadius.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboRadius.FormattingEnabled = true;
            this.comboRadius.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "11",
            "15",
            "19",
            "25",
            "31",
            "39",
            "47",
            "59"});
            this.comboRadius.Location = new System.Drawing.Point(867, 247);
            this.comboRadius.Name = "comboRadius";
            this.comboRadius.Size = new System.Drawing.Size(76, 22);
            this.comboRadius.TabIndex = 28;
            this.comboRadius.Text = "3";
            this.comboRadius.SelectedIndexChanged += new System.EventHandler(this.comboRadius_SelectedIndexChanged);
            // 
            // comboMult
            // 
            this.comboMult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboMult.FormattingEnabled = true;
            this.comboMult.Items.AddRange(new object[] {
            "0.0",
            "1.0",
            "2.0",
            "3.0",
            "4.0"});
            this.comboMult.Location = new System.Drawing.Point(867, 220);
            this.comboMult.Name = "comboMult";
            this.comboMult.Size = new System.Drawing.Size(76, 22);
            this.comboMult.TabIndex = 27;
            this.comboMult.Text = "1.0";
            this.comboMult.SelectedIndexChanged += new System.EventHandler(this.comboMult_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(821, 223);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 14);
            this.label12.TabIndex = 26;
            this.label12.Text = "Mult:";
            // 
            // comboSigma2
            // 
            this.comboSigma2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboSigma2.FormattingEnabled = true;
            this.comboSigma2.Items.AddRange(new object[] {
            "0.7",
            "0.8",
            "0.9",
            "1.0",
            "1.1",
            "1.2",
            "1.5",
            "2.0",
            "3.0",
            "4.0",
            "5.0",
            "6.0",
            "7.0",
            "8.0",
            "9.0",
            "10.0"});
            this.comboSigma2.Location = new System.Drawing.Point(867, 193);
            this.comboSigma2.Name = "comboSigma2";
            this.comboSigma2.Size = new System.Drawing.Size(76, 22);
            this.comboSigma2.TabIndex = 25;
            this.comboSigma2.Text = "2.0";
            this.comboSigma2.SelectedIndexChanged += new System.EventHandler(this.comboSigma2_SelectedIndexChanged);
            // 
            // comboSigma1
            // 
            this.comboSigma1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboSigma1.FormattingEnabled = true;
            this.comboSigma1.Items.AddRange(new object[] {
            "0.7",
            "0.8",
            "0.9",
            "1.0",
            "1.1",
            "1.2",
            "1.5",
            "2.0",
            "3.0",
            "4.0",
            "5.0",
            "6.0",
            "7.0",
            "8.0",
            "9.0",
            "10.0"});
            this.comboSigma1.Location = new System.Drawing.Point(867, 166);
            this.comboSigma1.Name = "comboSigma1";
            this.comboSigma1.Size = new System.Drawing.Size(76, 22);
            this.comboSigma1.TabIndex = 24;
            this.comboSigma1.SelectedIndexChanged += new System.EventHandler(this.comboSigma1_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(816, 196);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 14);
            this.label11.TabIndex = 23;
            this.label11.Text = "Sigma2:";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(815, 169);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 14);
            this.label10.TabIndex = 22;
            this.label10.Text = "Sigma1:";
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnalyze.BackColor = System.Drawing.Color.LightGray;
            this.btnAnalyze.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnalyze.Location = new System.Drawing.Point(819, 518);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(125, 32);
            this.btnAnalyze.TabIndex = 21;
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = false;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // btnGetROI
            // 
            this.btnGetROI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetROI.BackColor = System.Drawing.Color.LightGray;
            this.btnGetROI.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetROI.Location = new System.Drawing.Point(818, 128);
            this.btnGetROI.Name = "btnGetROI";
            this.btnGetROI.Size = new System.Drawing.Size(125, 32);
            this.btnGetROI.TabIndex = 20;
            this.btnGetROI.Text = "Get ROI";
            this.btnGetROI.UseVisualStyleBackColor = false;
            this.btnGetROI.Click += new System.EventHandler(this.btnGetROI_Click);
            // 
            // comboMaxGray
            // 
            this.comboMaxGray.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboMaxGray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMaxGray.FormattingEnabled = true;
            this.comboMaxGray.Location = new System.Drawing.Point(876, 65);
            this.comboMaxGray.Name = "comboMaxGray";
            this.comboMaxGray.Size = new System.Drawing.Size(66, 22);
            this.comboMaxGray.TabIndex = 19;
            this.comboMaxGray.SelectedIndexChanged += new System.EventHandler(this.comboMaxGray_SelectedIndexChanged);
            // 
            // comboMinGray
            // 
            this.comboMinGray.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboMinGray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMinGray.FormattingEnabled = true;
            this.comboMinGray.Location = new System.Drawing.Point(876, 40);
            this.comboMinGray.Name = "comboMinGray";
            this.comboMinGray.Size = new System.Drawing.Size(66, 22);
            this.comboMinGray.TabIndex = 18;
            this.comboMinGray.SelectedIndexChanged += new System.EventHandler(this.comboMinGray_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(814, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 14);
            this.label9.TabIndex = 17;
            this.label9.Text = "Max Gray:";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(815, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 14);
            this.label8.TabIndex = 16;
            this.label8.Text = "Min Gray:";
            // 
            // btnReadImage
            // 
            this.btnReadImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadImage.BackColor = System.Drawing.Color.LightGray;
            this.btnReadImage.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReadImage.Location = new System.Drawing.Point(817, 6);
            this.btnReadImage.Name = "btnReadImage";
            this.btnReadImage.Size = new System.Drawing.Size(125, 32);
            this.btnReadImage.TabIndex = 15;
            this.btnReadImage.Text = "Read Image";
            this.btnReadImage.UseVisualStyleBackColor = false;
            this.btnReadImage.Click += new System.EventHandler(this.btnReadImage_Click);
            // 
            // btnMeanThreshold
            // 
            this.btnMeanThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMeanThreshold.BackColor = System.Drawing.Color.LightGray;
            this.btnMeanThreshold.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMeanThreshold.Location = new System.Drawing.Point(817, 91);
            this.btnMeanThreshold.Name = "btnMeanThreshold";
            this.btnMeanThreshold.Size = new System.Drawing.Size(125, 32);
            this.btnMeanThreshold.TabIndex = 13;
            this.btnMeanThreshold.Text = "Threshold";
            this.btnMeanThreshold.UseVisualStyleBackColor = false;
            this.btnMeanThreshold.Click += new System.EventHandler(this.btnMeanThreshold_Click);
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
            // hSmartWindowControl1
            // 
            this.hSmartWindowControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hSmartWindowControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.hSmartWindowControl1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.hSmartWindowControl1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hSmartWindowControl1.HDoubleClickToFitContent = true;
            this.hSmartWindowControl1.HDrawingObjectsModifier = HalconDotNet.HSmartWindowControl.DrawingObjectsModifier.None;
            this.hSmartWindowControl1.HImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hSmartWindowControl1.HKeepAspectRatio = true;
            this.hSmartWindowControl1.HMoveContent = true;
            this.hSmartWindowControl1.HZoomContent = HalconDotNet.HSmartWindowControl.ZoomContent.WheelForwardZoomsIn;
            this.hSmartWindowControl1.Location = new System.Drawing.Point(0, 28);
            this.hSmartWindowControl1.Margin = new System.Windows.Forms.Padding(0);
            this.hSmartWindowControl1.Name = "hSmartWindowControl1";
            this.hSmartWindowControl1.Size = new System.Drawing.Size(798, 593);
            this.hSmartWindowControl1.TabIndex = 0;
            this.hSmartWindowControl1.WindowSize = new System.Drawing.Size(798, 593);
            this.hSmartWindowControl1.Resize += new System.EventHandler(this.hSmartWindowControl1_Resize);
            // 
            // tabInspection
            // 
            this.tabInspection.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tabInspection.Controls.Add(this.tlpInspection);
            this.tabInspection.Location = new System.Drawing.Point(4, 26);
            this.tabInspection.Name = "tabInspection";
            this.tabInspection.Size = new System.Drawing.Size(966, 621);
            this.tabInspection.TabIndex = 4;
            this.tabInspection.Text = "Inspection Test Mode";
            // 
            // tlpInspection
            // 
            this.tlpInspection.ColumnCount = 2;
            this.tlpInspection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInspection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 328F));
            this.tlpInspection.Controls.Add(this.panelInspection, 2, 0);
            this.tlpInspection.Controls.Add(this.hSmartWindowControl1st, 0, 0);
            this.tlpInspection.Controls.Add(this.hSmartWindowControl2nd, 0, 1);
            this.tlpInspection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpInspection.Location = new System.Drawing.Point(0, 0);
            this.tlpInspection.Name = "tlpInspection";
            this.tlpInspection.RowCount = 2;
            this.tlpInspection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpInspection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpInspection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInspection.Size = new System.Drawing.Size(966, 621);
            this.tlpInspection.TabIndex = 0;
            // 
            // panelInspection
            // 
            this.panelInspection.Controls.Add(this.lstSN);
            this.panelInspection.Controls.Add(this.richMessage);
            this.panelInspection.Controls.Add(this.label22);
            this.panelInspection.Controls.Add(this.txtSN);
            this.panelInspection.Controls.Add(this.txtInspectionInfo);
            this.panelInspection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInspection.Location = new System.Drawing.Point(641, 3);
            this.panelInspection.Name = "panelInspection";
            this.tlpInspection.SetRowSpan(this.panelInspection, 2);
            this.panelInspection.Size = new System.Drawing.Size(322, 615);
            this.panelInspection.TabIndex = 0;
            // 
            // lstSN
            // 
            this.lstSN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSN.FormattingEnabled = true;
            this.lstSN.ItemHeight = 14;
            this.lstSN.Location = new System.Drawing.Point(3, 368);
            this.lstSN.Name = "lstSN";
            this.lstSN.Size = new System.Drawing.Size(316, 186);
            this.lstSN.TabIndex = 18;
            // 
            // richMessage
            // 
            this.richMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richMessage.Location = new System.Drawing.Point(3, 1);
            this.richMessage.Name = "richMessage";
            this.richMessage.ReadOnly = true;
            this.richMessage.Size = new System.Drawing.Size(316, 364);
            this.richMessage.TabIndex = 17;
            this.richMessage.Text = "";
            this.richMessage.WordWrap = false;
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(8, 561);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(60, 17);
            this.label22.TabIndex = 16;
            this.label22.Text = "Input SN:";
            // 
            // txtSN
            // 
            this.txtSN.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtSN.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtSN.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtSN.Location = new System.Drawing.Point(72, 556);
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(245, 25);
            this.txtSN.TabIndex = 15;
            this.txtSN.Text = " ";
            this.txtSN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSN_KeyPress);
            // 
            // txtInspectionInfo
            // 
            this.txtInspectionInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInspectionInfo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtInspectionInfo.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInspectionInfo.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtInspectionInfo.Location = new System.Drawing.Point(3, 583);
            this.txtInspectionInfo.Name = "txtInspectionInfo";
            this.txtInspectionInfo.ReadOnly = true;
            this.txtInspectionInfo.Size = new System.Drawing.Size(316, 27);
            this.txtInspectionInfo.TabIndex = 14;
            this.txtInspectionInfo.Text = " ";
            this.txtInspectionInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // hSmartWindowControl1st
            // 
            this.hSmartWindowControl1st.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.hSmartWindowControl1st.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.hSmartWindowControl1st.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hSmartWindowControl1st.HDoubleClickToFitContent = true;
            this.hSmartWindowControl1st.HDrawingObjectsModifier = HalconDotNet.HSmartWindowControl.DrawingObjectsModifier.None;
            this.hSmartWindowControl1st.HImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hSmartWindowControl1st.HKeepAspectRatio = true;
            this.hSmartWindowControl1st.HMoveContent = true;
            this.hSmartWindowControl1st.HZoomContent = HalconDotNet.HSmartWindowControl.ZoomContent.WheelForwardZoomsIn;
            this.hSmartWindowControl1st.Location = new System.Drawing.Point(0, 0);
            this.hSmartWindowControl1st.Margin = new System.Windows.Forms.Padding(0);
            this.hSmartWindowControl1st.Name = "hSmartWindowControl1st";
            this.hSmartWindowControl1st.Size = new System.Drawing.Size(638, 310);
            this.hSmartWindowControl1st.TabIndex = 1;
            this.hSmartWindowControl1st.WindowSize = new System.Drawing.Size(638, 310);
            // 
            // hSmartWindowControl2nd
            // 
            this.hSmartWindowControl2nd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.hSmartWindowControl2nd.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.hSmartWindowControl2nd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hSmartWindowControl2nd.HDoubleClickToFitContent = true;
            this.hSmartWindowControl2nd.HDrawingObjectsModifier = HalconDotNet.HSmartWindowControl.DrawingObjectsModifier.None;
            this.hSmartWindowControl2nd.HImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hSmartWindowControl2nd.HKeepAspectRatio = true;
            this.hSmartWindowControl2nd.HMoveContent = true;
            this.hSmartWindowControl2nd.HZoomContent = HalconDotNet.HSmartWindowControl.ZoomContent.WheelForwardZoomsIn;
            this.hSmartWindowControl2nd.Location = new System.Drawing.Point(0, 310);
            this.hSmartWindowControl2nd.Margin = new System.Windows.Forms.Padding(0);
            this.hSmartWindowControl2nd.Name = "hSmartWindowControl2nd";
            this.hSmartWindowControl2nd.Size = new System.Drawing.Size(638, 311);
            this.hSmartWindowControl2nd.TabIndex = 2;
            this.hSmartWindowControl2nd.WindowSize = new System.Drawing.Size(638, 311);
            // 
            // tabSetting
            // 
            this.tabSetting.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tabSetting.Controls.Add(this.groupBox4);
            this.tabSetting.Controls.Add(this.groupBox3);
            this.tabSetting.Controls.Add(this.groupBox2);
            this.tabSetting.Controls.Add(this.groupBox1);
            this.tabSetting.Location = new System.Drawing.Point(4, 26);
            this.tabSetting.Name = "tabSetting";
            this.tabSetting.Size = new System.Drawing.Size(966, 621);
            this.tabSetting.TabIndex = 2;
            this.tabSetting.Text = "Setting";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkUseNet);
            this.groupBox4.Controls.Add(this.txtPort);
            this.groupBox4.Controls.Add(this.label30);
            this.groupBox4.Controls.Add(this.txtIP);
            this.groupBox4.Controls.Add(this.label29);
            this.groupBox4.Location = new System.Drawing.Point(3, 373);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(543, 73);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Net Listening Setting";
            // 
            // chkUseNet
            // 
            this.chkUseNet.AutoSize = true;
            this.chkUseNet.Location = new System.Drawing.Point(11, 22);
            this.chkUseNet.Name = "chkUseNet";
            this.chkUseNet.Size = new System.Drawing.Size(131, 18);
            this.chkUseNet.TabIndex = 18;
            this.chkUseNet.Text = "Use Net Receive SN";
            this.chkUseNet.UseVisualStyleBackColor = true;
            this.chkUseNet.CheckedChanged += new System.EventHandler(this.chkUseNet_CheckedChanged);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(248, 44);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(66, 22);
            this.txtPort.TabIndex = 20;
            this.txtPort.Text = "10086";
            this.txtPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPort.TextChanged += new System.EventHandler(this.txtPort_TextChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(213, 48);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(31, 14);
            this.label30.TabIndex = 19;
            this.label30.Text = "Port:";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(76, 44);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(128, 22);
            this.txtIP.TabIndex = 18;
            this.txtIP.Text = "10.62.32.8";
            this.txtIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIP.TextChanged += new System.EventHandler(this.txtIP_TextChanged);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(6, 47);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(67, 14);
            this.label29.TabIndex = 0;
            this.label29.Text = "IP Address:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnRefreshCom);
            this.groupBox3.Controls.Add(this.txtComDeviceInfo);
            this.groupBox3.Controls.Add(this.txtCapture2Signal);
            this.groupBox3.Controls.Add(this.label28);
            this.groupBox3.Controls.Add(this.chkCapture2);
            this.groupBox3.Controls.Add(this.chkCapture1);
            this.groupBox3.Controls.Add(this.chkUseCom);
            this.groupBox3.Controls.Add(this.txtCapture1Signal);
            this.groupBox3.Controls.Add(this.label27);
            this.groupBox3.Controls.Add(this.label26);
            this.groupBox3.Controls.Add(this.comboPort);
            this.groupBox3.Location = new System.Drawing.Point(3, 242);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(543, 123);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Capture Signal Setting";
            // 
            // btnRefreshCom
            // 
            this.btnRefreshCom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnRefreshCom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefreshCom.Location = new System.Drawing.Point(410, 40);
            this.btnRefreshCom.Name = "btnRefreshCom";
            this.btnRefreshCom.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshCom.TabIndex = 17;
            this.btnRefreshCom.Text = "Refresh";
            this.btnRefreshCom.UseVisualStyleBackColor = false;
            this.btnRefreshCom.Click += new System.EventHandler(this.btnRefreshCom_Click);
            // 
            // txtComDeviceInfo
            // 
            this.txtComDeviceInfo.Location = new System.Drawing.Point(158, 40);
            this.txtComDeviceInfo.Name = "txtComDeviceInfo";
            this.txtComDeviceInfo.ReadOnly = true;
            this.txtComDeviceInfo.Size = new System.Drawing.Size(246, 22);
            this.txtComDeviceInfo.TabIndex = 7;
            this.txtComDeviceInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCapture2Signal
            // 
            this.txtCapture2Signal.Location = new System.Drawing.Point(278, 95);
            this.txtCapture2Signal.Name = "txtCapture2Signal";
            this.txtCapture2Signal.Size = new System.Drawing.Size(76, 22);
            this.txtCapture2Signal.TabIndex = 15;
            this.txtCapture2Signal.Text = "B";
            this.txtCapture2Signal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCapture2Signal.TextChanged += new System.EventHandler(this.txtCapture2Signal_TextChanged);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(164, 100);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(112, 14);
            this.label28.TabIndex = 16;
            this.label28.Text = "2nd Capture Signal:";
            // 
            // chkCapture2
            // 
            this.chkCapture2.AutoSize = true;
            this.chkCapture2.Location = new System.Drawing.Point(10, 100);
            this.chkCapture2.Name = "chkCapture2";
            this.chkCapture2.Size = new System.Drawing.Size(128, 18);
            this.chkCapture2.TabIndex = 14;
            this.chkCapture2.Text = "Capture 2nd Image";
            this.chkCapture2.UseVisualStyleBackColor = true;
            this.chkCapture2.CheckedChanged += new System.EventHandler(this.chkCapture2_CheckedChanged);
            // 
            // chkCapture1
            // 
            this.chkCapture1.AutoSize = true;
            this.chkCapture1.Location = new System.Drawing.Point(11, 72);
            this.chkCapture1.Name = "chkCapture1";
            this.chkCapture1.Size = new System.Drawing.Size(124, 18);
            this.chkCapture1.TabIndex = 13;
            this.chkCapture1.Text = "Capture 1st Image";
            this.chkCapture1.UseVisualStyleBackColor = true;
            this.chkCapture1.CheckedChanged += new System.EventHandler(this.chkCapture1_CheckedChanged);
            // 
            // chkUseCom
            // 
            this.chkUseCom.AutoSize = true;
            this.chkUseCom.Location = new System.Drawing.Point(11, 21);
            this.chkUseCom.Name = "chkUseCom";
            this.chkUseCom.Size = new System.Drawing.Size(206, 18);
            this.chkUseCom.TabIndex = 12;
            this.chkUseCom.Text = "Use Com Signal to Capture Image";
            this.chkUseCom.UseVisualStyleBackColor = true;
            this.chkUseCom.CheckedChanged += new System.EventHandler(this.chkUseCom_CheckedChanged);
            // 
            // txtCapture1Signal
            // 
            this.txtCapture1Signal.Location = new System.Drawing.Point(278, 68);
            this.txtCapture1Signal.Name = "txtCapture1Signal";
            this.txtCapture1Signal.Size = new System.Drawing.Size(76, 22);
            this.txtCapture1Signal.TabIndex = 12;
            this.txtCapture1Signal.Text = "A";
            this.txtCapture1Signal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCapture1Signal.TextChanged += new System.EventHandler(this.txtCapture1Signal_TextChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(164, 72);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(108, 14);
            this.label27.TabIndex = 12;
            this.label27.Text = "1st Capture Signal:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(13, 44);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(57, 14);
            this.label26.TabIndex = 12;
            this.label26.Text = "Com Port:";
            // 
            // comboPort
            // 
            this.comboPort.FormattingEnabled = true;
            this.comboPort.Location = new System.Drawing.Point(76, 40);
            this.comboPort.Name = "comboPort";
            this.comboPort.Size = new System.Drawing.Size(76, 22);
            this.comboPort.TabIndex = 0;
            this.comboPort.SelectedIndexChanged += new System.EventHandler(this.comboPort_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkTestOKUploadResult);
            this.groupBox2.Controls.Add(this.txtStage);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.txtTestSN);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.chkTestWebService);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.txtWebService);
            this.groupBox2.Controls.Add(this.chkUseWebService);
            this.groupBox2.Location = new System.Drawing.Point(3, 131);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(543, 105);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WebService Setting";
            // 
            // txtStage
            // 
            this.txtStage.Location = new System.Drawing.Point(477, 69);
            this.txtStage.Name = "txtStage";
            this.txtStage.Size = new System.Drawing.Size(60, 22);
            this.txtStage.TabIndex = 11;
            this.txtStage.Text = "TU";
            this.txtStage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtStage.TextChanged += new System.EventHandler(this.txtStage_TextChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(436, 74);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(40, 14);
            this.label25.TabIndex = 10;
            this.label25.Text = "Stage:";
            // 
            // txtTestSN
            // 
            this.txtTestSN.Location = new System.Drawing.Point(320, 70);
            this.txtTestSN.Name = "txtTestSN";
            this.txtTestSN.Size = new System.Drawing.Size(110, 22);
            this.txtTestSN.TabIndex = 9;
            this.txtTestSN.Text = "F3NZLT2";
            this.txtTestSN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTestSN.TextChanged += new System.EventHandler(this.txtTestSN_TextChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(265, 74);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(49, 14);
            this.label24.TabIndex = 8;
            this.label24.Text = "Test SN:";
            // 
            // chkTestWebService
            // 
            this.chkTestWebService.AutoSize = true;
            this.chkTestWebService.Location = new System.Drawing.Point(11, 73);
            this.chkTestWebService.Name = "chkTestWebService";
            this.chkTestWebService.Size = new System.Drawing.Size(239, 18);
            this.chkTestWebService.TabIndex = 7;
            this.chkTestWebService.Text = "Connect WebService Test Query Method";
            this.chkTestWebService.UseVisualStyleBackColor = true;
            this.chkTestWebService.CheckedChanged += new System.EventHandler(this.chkTestWebService_CheckedChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(14, 48);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(73, 14);
            this.label23.TabIndex = 6;
            this.label23.Text = "WebService:";
            // 
            // txtWebService
            // 
            this.txtWebService.Location = new System.Drawing.Point(99, 44);
            this.txtWebService.Name = "txtWebService";
            this.txtWebService.Size = new System.Drawing.Size(438, 22);
            this.txtWebService.TabIndex = 6;
            this.txtWebService.Text = "http://10.62.201.215/Tester.WebService/WebService.asmx";
            this.txtWebService.TextChanged += new System.EventHandler(this.txtWebService_TextChanged);
            // 
            // chkUseWebService
            // 
            this.chkUseWebService.AutoSize = true;
            this.chkUseWebService.Location = new System.Drawing.Point(11, 21);
            this.chkUseWebService.Name = "chkUseWebService";
            this.chkUseWebService.Size = new System.Drawing.Size(113, 18);
            this.chkUseWebService.TabIndex = 6;
            this.chkUseWebService.Text = "Use WebService";
            this.chkUseWebService.UseVisualStyleBackColor = true;
            this.chkUseWebService.CheckedChanged += new System.EventHandler(this.chkUseWebService_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkAnalysisImg);
            this.groupBox1.Controls.Add(this.chkUseCamera);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.chkTestNGSavePictures);
            this.groupBox1.Controls.Add(this.chkTestOKSavePictures);
            this.groupBox1.Controls.Add(this.txtNGImgFolder);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtOKImgFolder);
            this.groupBox1.Location = new System.Drawing.Point(3, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(543, 111);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Capture Setting";
            // 
            // chkAnalysisImg
            // 
            this.chkAnalysisImg.AutoSize = true;
            this.chkAnalysisImg.Location = new System.Drawing.Point(392, 21);
            this.chkAnalysisImg.Name = "chkAnalysisImg";
            this.chkAnalysisImg.Size = new System.Drawing.Size(112, 18);
            this.chkAnalysisImg.TabIndex = 7;
            this.chkAnalysisImg.Text = "Analysis Picture";
            this.chkAnalysisImg.UseVisualStyleBackColor = true;
            this.chkAnalysisImg.CheckedChanged += new System.EventHandler(this.chkAnalysisImg_CheckedChanged);
            // 
            // chkUseCamera
            // 
            this.chkUseCamera.AutoSize = true;
            this.chkUseCamera.Location = new System.Drawing.Point(11, 21);
            this.chkUseCamera.Name = "chkUseCamera";
            this.chkUseCamera.Size = new System.Drawing.Size(91, 18);
            this.chkUseCamera.TabIndex = 6;
            this.chkUseCamera.Text = "Use Camera";
            this.chkUseCamera.UseVisualStyleBackColor = true;
            this.chkUseCamera.CheckedChanged += new System.EventHandler(this.chkUseCamera_CheckedChanged);
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
            // chkTestNGSavePictures
            // 
            this.chkTestNGSavePictures.AutoSize = true;
            this.chkTestNGSavePictures.Location = new System.Drawing.Point(247, 21);
            this.chkTestNGSavePictures.Name = "chkTestNGSavePictures";
            this.chkTestNGSavePictures.Size = new System.Drawing.Size(135, 18);
            this.chkTestNGSavePictures.TabIndex = 1;
            this.chkTestNGSavePictures.Text = "Test NG Save Picture";
            this.chkTestNGSavePictures.UseVisualStyleBackColor = true;
            this.chkTestNGSavePictures.CheckedChanged += new System.EventHandler(this.chkTestNGSavePictures_CheckedChanged);
            // 
            // chkTestOKSavePictures
            // 
            this.chkTestOKSavePictures.AutoSize = true;
            this.chkTestOKSavePictures.Location = new System.Drawing.Point(105, 21);
            this.chkTestOKSavePictures.Name = "chkTestOKSavePictures";
            this.chkTestOKSavePictures.Size = new System.Drawing.Size(133, 18);
            this.chkTestOKSavePictures.TabIndex = 0;
            this.chkTestOKSavePictures.Text = "Test OK Save Picture";
            this.chkTestOKSavePictures.UseVisualStyleBackColor = true;
            this.chkTestOKSavePictures.CheckedChanged += new System.EventHandler(this.chkTestOKSavePictures_CheckedChanged);
            // 
            // txtNGImgFolder
            // 
            this.txtNGImgFolder.Location = new System.Drawing.Point(99, 73);
            this.txtNGImgFolder.Name = "txtNGImgFolder";
            this.txtNGImgFolder.Size = new System.Drawing.Size(438, 22);
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
            this.txtOKImgFolder.Size = new System.Drawing.Size(438, 22);
            this.txtOKImgFolder.TabIndex = 2;
            this.txtOKImgFolder.TextChanged += new System.EventHandler(this.txtOKImgFolder_TextChanged);
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // chkTestOKUploadResult
            // 
            this.chkTestOKUploadResult.AutoSize = true;
            this.chkTestOKUploadResult.Location = new System.Drawing.Point(167, 20);
            this.chkTestOKUploadResult.Name = "chkTestOKUploadResult";
            this.chkTestOKUploadResult.Size = new System.Drawing.Size(146, 18);
            this.chkTestOKUploadResult.TabIndex = 12;
            this.chkTestOKUploadResult.Text = "Test OK Upload Result";
            this.chkTestOKUploadResult.UseVisualStyleBackColor = true;
            this.chkTestOKUploadResult.CheckedChanged += new System.EventHandler(this.chkTestOKUploadResult_CheckedChanged);
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
            this.tabInspection.ResumeLayout(false);
            this.tlpInspection.ResumeLayout(false);
            this.panelInspection.ResumeLayout(false);
            this.panelInspection.PerformLayout();
            this.tabSetting.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.Button btnReadImage;
        private System.Windows.Forms.ComboBox comboMinGray;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboMaxGray;
        private System.Windows.Forms.Button btnGetROI;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboSigma2;
        private System.Windows.Forms.ComboBox comboSigma1;
        private System.Windows.Forms.ComboBox comboMult;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboRadius;
        private System.Windows.Forms.Label lblAlpha;
        private System.Windows.Forms.ComboBox comboAlpha;
        private System.Windows.Forms.TextBox txtTopR;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtTopL;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtMaxArea;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtMinArea;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtBotR;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtBotL;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtMaxGray;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtMinGray;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TabPage tabInspection;
        private System.Windows.Forms.TableLayoutPanel tlpInspection;
        private System.Windows.Forms.Panel panelInspection;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.TextBox txtInspectionInfo;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.RichTextBox richMessage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkUseWebService;
        private System.Windows.Forms.CheckBox chkTestWebService;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtWebService;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtTestSN;
        private System.Windows.Forms.TextBox txtStage;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.CheckBox chkUseCamera;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox comboPort;
        private System.Windows.Forms.TextBox txtCapture2Signal;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.CheckBox chkCapture2;
        private System.Windows.Forms.CheckBox chkCapture1;
        private System.Windows.Forms.CheckBox chkUseCom;
        private System.Windows.Forms.TextBox txtCapture1Signal;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtComDeviceInfo;
        private System.Windows.Forms.Button btnRefreshCom;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.CheckBox chkUseNet;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.CheckBox chkAnalysisImg;
        private HalconDotNet.HSmartWindowControl hSmartWindowControl1st;
        private HalconDotNet.HSmartWindowControl hSmartWindowControl2nd;
        private System.Windows.Forms.ListBox lstSN;
        private System.Windows.Forms.CheckBox chkTestOKUploadResult;
    }
}

