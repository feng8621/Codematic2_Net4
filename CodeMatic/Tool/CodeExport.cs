using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using System.IO;
using System.Text;
using System.Data;
using Maticsoft.CodeBuild;
using Maticsoft.Utility;
using Maticsoft.CodeHelper;
using Maticsoft.AddInManager;
using Codematic.UserControls;
namespace Codematic
{
    /// <summary>
    /// ������������
    /// </summary>
    public partial class CodeExport : Form
    {
        #region
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelNum;
        private System.Windows.Forms.Button btn_Addlist;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_Del;
        private System.Windows.Forms.Button btn_Dellist;
        private System.Windows.Forms.ListBox listTable2;
        private System.Windows.Forms.ListBox listTable1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbDB;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private WiB.Pinkie.Controls.ButtonXP btn_Cancle;
        private WiB.Pinkie.Controls.ButtonXP btn_Export;
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private WiB.Pinkie.Controls.ButtonXP btn_TargetFold;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.TextBox txtTargetFolder;
        private System.Windows.Forms.GroupBox groupBox5;
        private GroupBox groupBox6;
        private RadioButton radBtn_One;
        private RadioButton radBtn_F3;
        private RadioButton radBtn_S3;
        private TextBox txtDbHelper;
        private Label label6;
        private PictureBox pictureBox1;
        private TextBox txtTabNamepre;
        private Label label7;
        private Label label8;
        #endregion

        DALTypeAddIn cm_daltype;
        DALTypeAddIn cm_blltype;
        DALTypeAddIn cm_webtype;

        Thread mythread;
        string cmcfgfile = Application.StartupPath + @"\cmcfg.ini";
        string copyrightfile = Application.StartupPath + @"\copyright.txt";
        string copyrightstr = "";
        Maticsoft.Utility.INIFile cfgfile;
        Maticsoft.IDBO.IDbObject dbobj;//���ݿ����
        Maticsoft.CodeBuild.CodeBuilders cb;//�������ɶ���
        Maticsoft.CmConfig.DbSettings dbset;//����������                
        
        delegate void SetBtnEnableCallback();
        delegate void SetBtnDisableCallback();
        delegate void SetlblStatuCallback(string text);
        delegate void SetProBar1MaxCallback(int val);
        delegate void SetProBar1ValCallback(int val);
        string dbname = "";
        private Button btnInputTxt;


        VSProject vsp = new VSProject();

        #region ���캯��

        public CodeExport(string longservername)
        {
            InitializeComponent();
            dbset = Maticsoft.CmConfig.DbConfig.GetSetting(longservername);
            dbobj = Maticsoft.DBFactory.DBOMaker.CreateDbObj(dbset.DbType);
            dbobj.DbConnectStr = dbset.ConnectStr;
            cb = new CodeBuilders(dbobj);
            this.lblServer.Text = dbset.Server;
        }
        #endregion

        #region Windows ������������ɵĴ���
        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }


        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeExport));
            this.btn_Export = new WiB.Pinkie.Controls.ButtonXP();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnInputTxt = new System.Windows.Forms.Button();
            this.btn_Addlist = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_Del = new System.Windows.Forms.Button();
            this.btn_Dellist = new System.Windows.Forms.Button();
            this.listTable2 = new System.Windows.Forms.ListBox();
            this.listTable1 = new System.Windows.Forms.ListBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelNum = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbDB = new System.Windows.Forms.ComboBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Cancle = new WiB.Pinkie.Controls.ButtonXP();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtTargetFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_TargetFold = new WiB.Pinkie.Controls.ButtonXP();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTabNamepre = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDbHelper = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.radBtn_One = new System.Windows.Forms.RadioButton();
            this.radBtn_F3 = new System.Windows.Forms.RadioButton();
            this.radBtn_S3 = new System.Windows.Forms.RadioButton();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Export
            // 
            this.btn_Export._Image = null;
            this.btn_Export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Export.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btn_Export.DefaultScheme = false;
            this.btn_Export.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_Export.Image = null;
            this.btn_Export.Location = new System.Drawing.Point(491, 619);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
            this.btn_Export.Size = new System.Drawing.Size(100, 33);
            this.btn_Export.TabIndex = 46;
            this.btn_Export.Text = "����";
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnInputTxt);
            this.groupBox2.Controls.Add(this.btn_Addlist);
            this.groupBox2.Controls.Add(this.btn_Add);
            this.groupBox2.Controls.Add(this.btn_Del);
            this.groupBox2.Controls.Add(this.btn_Dellist);
            this.groupBox2.Controls.Add(this.listTable2);
            this.groupBox2.Controls.Add(this.listTable1);
            this.groupBox2.Location = new System.Drawing.Point(11, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(789, 179);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ѡ���";
            // 
            // btnInputTxt
            // 
            this.btnInputTxt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnInputTxt.Location = new System.Drawing.Point(447, 134);
            this.btnInputTxt.Name = "btnInputTxt";
            this.btnInputTxt.Size = new System.Drawing.Size(25, 29);
            this.btnInputTxt.TabIndex = 9;
            this.btnInputTxt.Text = "Txt";
            this.btnInputTxt.UseVisualStyleBackColor = true;
            this.btnInputTxt.Click += new System.EventHandler(this.btnInputTxt_Click);
            // 
            // btn_Addlist
            // 
            this.btn_Addlist.Enabled = false;
            this.btn_Addlist.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Addlist.Location = new System.Drawing.Point(367, 26);
            this.btn_Addlist.Name = "btn_Addlist";
            this.btn_Addlist.Size = new System.Drawing.Size(53, 29);
            this.btn_Addlist.TabIndex = 7;
            this.btn_Addlist.Text = ">>";
            this.btn_Addlist.Click += new System.EventHandler(this.btn_Addlist_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Enabled = false;
            this.btn_Add.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Add.Location = new System.Drawing.Point(367, 62);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(53, 29);
            this.btn_Add.TabIndex = 8;
            this.btn_Add.Text = ">";
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_Del
            // 
            this.btn_Del.Enabled = false;
            this.btn_Del.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Del.Location = new System.Drawing.Point(367, 98);
            this.btn_Del.Name = "btn_Del";
            this.btn_Del.Size = new System.Drawing.Size(53, 29);
            this.btn_Del.TabIndex = 5;
            this.btn_Del.Text = "<";
            this.btn_Del.Click += new System.EventHandler(this.btn_Del_Click);
            // 
            // btn_Dellist
            // 
            this.btn_Dellist.Enabled = false;
            this.btn_Dellist.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Dellist.Location = new System.Drawing.Point(367, 134);
            this.btn_Dellist.Name = "btn_Dellist";
            this.btn_Dellist.Size = new System.Drawing.Size(53, 29);
            this.btn_Dellist.TabIndex = 6;
            this.btn_Dellist.Text = "<<";
            this.btn_Dellist.Click += new System.EventHandler(this.btn_Dellist_Click);
            // 
            // listTable2
            // 
            this.listTable2.ItemHeight = 15;
            this.listTable2.Location = new System.Drawing.Point(475, 26);
            this.listTable2.Name = "listTable2";
            this.listTable2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listTable2.Size = new System.Drawing.Size(290, 139);
            this.listTable2.TabIndex = 1;
            this.listTable2.DoubleClick += new System.EventHandler(this.listTable2_DoubleClick);
            // 
            // listTable1
            // 
            this.listTable1.ItemHeight = 15;
            this.listTable1.Location = new System.Drawing.Point(21, 26);
            this.listTable1.Name = "listTable1";
            this.listTable1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listTable1.Size = new System.Drawing.Size(291, 139);
            this.listTable1.TabIndex = 0;
            this.listTable1.DoubleClick += new System.EventHandler(this.listTable1_DoubleClick);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(11, 661);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(785, 24);
            this.progressBar1.TabIndex = 10;
            // 
            // labelNum
            // 
            this.labelNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelNum.Location = new System.Drawing.Point(32, 628);
            this.labelNum.Name = "labelNum";
            this.labelNum.Size = new System.Drawing.Size(120, 28);
            this.labelNum.TabIndex = 9;
            this.labelNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbDB);
            this.groupBox1.Controls.Add(this.lblServer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(11, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(789, 60);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ѡ�����ݿ�";
            // 
            // cmbDB
            // 
            this.cmbDB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDB.Location = new System.Drawing.Point(521, 26);
            this.cmbDB.Name = "cmbDB";
            this.cmbDB.Size = new System.Drawing.Size(203, 23);
            this.cmbDB.TabIndex = 2;
            this.cmbDB.SelectedIndexChanged += new System.EventHandler(this.cmbDB_SelectedIndexChanged);
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(139, 28);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(55, 15);
            this.lblServer.TabIndex = 1;
            this.lblServer.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "��ǰ��������";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(415, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "ѡ�����ݿ⣺";
            // 
            // btn_Cancle
            // 
            this.btn_Cancle._Image = null;
            this.btn_Cancle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btn_Cancle.DefaultScheme = false;
            this.btn_Cancle.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_Cancle.Image = null;
            this.btn_Cancle.Location = new System.Drawing.Point(629, 619);
            this.btn_Cancle.Name = "btn_Cancle";
            this.btn_Cancle.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
            this.btn_Cancle.Size = new System.Drawing.Size(100, 33);
            this.btn_Cancle.TabIndex = 45;
            this.btn_Cancle.Text = "�ر�";
            this.btn_Cancle.Click += new System.EventHandler(this.btn_Cancle_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Controls.Add(this.txtTargetFolder);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.btn_TargetFold);
            this.groupBox3.Location = new System.Drawing.Point(11, 544);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(789, 64);
            this.groupBox3.TabIndex = 47;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "����λ��";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Codematic.Properties.Resources.Control;
            this.pictureBox1.Location = new System.Drawing.Point(13, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 36);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 53;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // txtTargetFolder
            // 
            this.txtTargetFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTargetFolder.Location = new System.Drawing.Point(139, 23);
            this.txtTargetFolder.Name = "txtTargetFolder";
            this.txtTargetFolder.Size = new System.Drawing.Size(526, 25);
            this.txtTargetFolder.TabIndex = 45;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 44;
            this.label2.Text = "���Ŀ¼��";
            // 
            // btn_TargetFold
            // 
            this.btn_TargetFold._Image = null;
            this.btn_TargetFold.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btn_TargetFold.DefaultScheme = false;
            this.btn_TargetFold.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_TargetFold.Image = null;
            this.btn_TargetFold.Location = new System.Drawing.Point(673, 22);
            this.btn_TargetFold.Name = "btn_TargetFold";
            this.btn_TargetFold.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
            this.btn_TargetFold.Size = new System.Drawing.Size(76, 29);
            this.btn_TargetFold.TabIndex = 46;
            this.btn_TargetFold.Text = "ѡ��...";
            this.btn_TargetFold.Click += new System.EventHandler(this.btn_TargetFold_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.txtTabNamepre);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.txtDbHelper);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.txtNamespace);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.txtFolder);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(16, 256);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(789, 99);
            this.groupBox4.TabIndex = 48;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "�����趨";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(431, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 15);
            this.label8.TabIndex = 4;
            this.label8.Text = "(���ձ�ʾ����)";
            // 
            // txtTabNamepre
            // 
            this.txtTabNamepre.Location = new System.Drawing.Point(297, 63);
            this.txtTabNamepre.Name = "txtTabNamepre";
            this.txtTabNamepre.Size = new System.Drawing.Size(134, 25);
            this.txtTabNamepre.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(262, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "����ֱ�ӱ�Ϊ������ȥ������ǰ׺��";
            // 
            // txtDbHelper
            // 
            this.txtDbHelper.Location = new System.Drawing.Point(641, 27);
            this.txtDbHelper.Name = "txtDbHelper";
            this.txtDbHelper.Size = new System.Drawing.Size(134, 25);
            this.txtDbHelper.TabIndex = 1;
            this.txtDbHelper.Text = "DbHelperSQL";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(523, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "���ݷ���������";
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(99, 28);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(114, 25);
            this.txtNamespace.TabIndex = 1;
            this.txtNamespace.Text = "CodematicDemo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "�����ռ䣺";
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(337, 28);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(178, 25);
            this.txtFolder.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(237, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "���ļ�������";
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(11, 419);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(789, 117);
            this.groupBox5.TabIndex = 49;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "����ģ���������";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.radBtn_One);
            this.groupBox6.Controls.Add(this.radBtn_F3);
            this.groupBox6.Controls.Add(this.radBtn_S3);
            this.groupBox6.Location = new System.Drawing.Point(16, 359);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(784, 56);
            this.groupBox6.TabIndex = 50;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "�ܹ�";
            // 
            // radBtn_One
            // 
            this.radBtn_One.AutoSize = true;
            this.radBtn_One.Location = new System.Drawing.Point(84, 23);
            this.radBtn_One.Name = "radBtn_One";
            this.radBtn_One.Size = new System.Drawing.Size(88, 19);
            this.radBtn_One.TabIndex = 0;
            this.radBtn_One.Text = "����ṹ";
            this.radBtn_One.UseVisualStyleBackColor = true;
            // 
            // radBtn_F3
            // 
            this.radBtn_F3.AutoSize = true;
            this.radBtn_F3.Checked = true;
            this.radBtn_F3.Location = new System.Drawing.Point(400, 23);
            this.radBtn_F3.Name = "radBtn_F3";
            this.radBtn_F3.Size = new System.Drawing.Size(118, 19);
            this.radBtn_F3.TabIndex = 0;
            this.radBtn_F3.TabStop = true;
            this.radBtn_F3.Text = "����ģʽ����";
            this.radBtn_F3.UseVisualStyleBackColor = true;
            // 
            // radBtn_S3
            // 
            this.radBtn_S3.AutoSize = true;
            this.radBtn_S3.Location = new System.Drawing.Point(240, 23);
            this.radBtn_S3.Name = "radBtn_S3";
            this.radBtn_S3.Size = new System.Drawing.Size(88, 19);
            this.radBtn_S3.TabIndex = 0;
            this.radBtn_S3.Text = "������";
            this.radBtn_S3.UseVisualStyleBackColor = true;
            // 
            // CodeExport
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
            this.ClientSize = new System.Drawing.Size(812, 688);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btn_Export);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Cancle);
            this.Controls.Add(this.labelNum);
            this.Controls.Add(this.progressBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CodeExport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "������������";
            this.Load += new System.EventHandler(this.CodeExport_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        #region ��ʼ��
        private void CodeExport_Load(object sender, System.EventArgs e)
        {
            string mastedb = "master";
            switch (dbobj.DbType)
            {
                case "SQL2000":
                case "SQL2005":
                case "SQL2008":
                case "SQL2012":
                    mastedb = "master";
                    break;
                case "Oracle":
                case "OleDb":
                    {
                        mastedb = "";
                        label3.Visible = false;
                        cmbDB.Visible = false;
                        break;
                    }
                case "MySQL":
                    mastedb = "mysql";
                    break;
                case "SQLite":
                    mastedb = "sqlite_master";
                    break;
            }
            if ((dbset.DbName == "") || (dbset.DbName == mastedb))
            {
                List<string> dblist = dbobj.GetDBList();
                if (dblist != null)
                {
                    if (dblist.Count > 0)
                    {
                        foreach (string dbname in dblist)
                        {
                            this.cmbDB.Items.Add(dbname);
                        }
                    }
                }
            }
            else
            {
                this.cmbDB.Items.Add(dbset.DbName);
            }

            if (this.cmbDB.Items.Count > 0)
            {
                DbView dbviewfrm = (DbView)Application.OpenForms["DbView"];
                if (dbviewfrm != null)
                {
                    string dbname = dbviewfrm.GetSelectedDBName();
                    cmbDB.Text = dbname;
                }
                else
                {
                    this.cmbDB.SelectedIndex = 0;
                }                
            }
            else
            {
                List<string> tabNames = dbobj.GetTableViews("");
                this.listTable1.Items.Clear();
                this.listTable2.Items.Clear();
                if (tabNames.Count > 0)
                {
                    foreach (string tabname in tabNames)
                    {
                        listTable1.Items.Add(tabname);
                    }
                }
            }


            IsHasItem();


            this.btn_Export.Enabled = false;            
            switch (dbset.AppFrame)
            {
                case "One":
                    this.radBtn_One.Checked = true;
                    break;
                case "S3":
                    this.radBtn_S3.Checked = true;
                    break;
                case "F3":
                    this.radBtn_F3.Checked = true;
                    break;
            }

            #region ���ز��

            //cm_modeltype = new DALTypeAddIn("Maticsoft.IBuilder.IBuilderModel");
            //cm_modeltype.Title = "Model";
            //groupBox5.Controls.Add(cm_modeltype);
            //cm_modeltype.Location = new System.Drawing.Point(30, 16);
            //cm_modeltype.SetSelectedDALType(setting.BLLType.Trim());  

            cm_daltype = new DALTypeAddIn("Maticsoft.IBuilder.IBuilderDAL");
            cm_daltype.Title = "DAL";
            groupBox5.Controls.Add(cm_daltype);
            cm_daltype.Location = new System.Drawing.Point(30, 16);
            cm_daltype.SetSelectedDALType(dbset.DALType.Trim());

            cm_blltype = new DALTypeAddIn("Maticsoft.IBuilder.IBuilderBLL");
            cm_blltype.Title = "BLL";
            groupBox5.Controls.Add(cm_blltype);
            cm_blltype.Location = new System.Drawing.Point(30, 40);
            cm_blltype.SetSelectedDALType(dbset.BLLType.Trim());

            cm_webtype = new DALTypeAddIn("Maticsoft.IBuilder.IBuilderWeb");
            cm_webtype.Title = "Web";
            groupBox5.Controls.Add(cm_webtype);
            cm_webtype.Location = new System.Drawing.Point(30, 64);
            cm_webtype.SetSelectedDALType(dbset.WebType.Trim());

            #endregion

            txtDbHelper.Text = dbset.DbHelperName;
            if (dbset.DbHelperName == "DbHelperSQL")
            {
                switch (dbobj.DbType)
                {
                    case "SQL2000":
                    case "SQL2005":
                    case "SQL2008":
                    case "SQL2012":
                        txtDbHelper.Text = "DbHelperSQL";
                        break;
                    case "Oracle":
                        txtDbHelper.Text = "DbHelperOra";
                        break;
                    case "MySQL":
                        txtDbHelper.Text = "DbHelperMySQL";
                        break;
                    case "OleDb":
                        txtDbHelper.Text = "DbHelperOleDb";
                        break;
                    case "SQLite":
                        txtDbHelper.Text = "DbHelperSQLite";
                        break;
                }
            }
            txtFolder.Text = dbset.Folder;
            txtNamespace.Text = dbset.Namepace;

            if (File.Exists(cmcfgfile))
            {
                cfgfile = new Maticsoft.Utility.INIFile(cmcfgfile);
                string lastpath = cfgfile.IniReadValue("Project", "lastpath");
                if (lastpath.Trim() != "")
                {
                    txtTargetFolder.Text = lastpath;
                }
            }
            //��İ�Ȩ��Ϣ
            if (File.Exists(copyrightfile))
            {
                copyrightstr=File.ReadAllText(copyrightfile);
            }
        }

        private void cmbDB_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string dbname = cmbDB.Text;
            List<string> tabNames = dbobj.GetTableViews(dbname);
            this.listTable1.Items.Clear();
            this.listTable2.Items.Clear();
            if (tabNames.Count > 0)
            {
                foreach (string tabname in tabNames)
                {
                    listTable1.Items.Add(tabname);
                }
            }

            IsHasItem();

        }


        #endregion

        #region listbox ����

        private void btn_Addlist_Click(object sender, System.EventArgs e)
        {
            int c = this.listTable1.Items.Count;
            for (int i = 0; i < c; i++)
            {
                this.listTable2.Items.Add(this.listTable1.Items[i]);
            }
            this.listTable1.Items.Clear();

            IsHasItem();
        }

        private void btn_Add_Click(object sender, System.EventArgs e)
        {
            int c = this.listTable1.SelectedItems.Count;
            ListBox.SelectedObjectCollection objs = this.listTable1.SelectedItems;
            for (int i = 0; i < c; i++)
            {
                this.listTable2.Items.Add(listTable1.SelectedItems[i]);

            }
            for (int i = 0; i < c; i++)
            {
                if (this.listTable1.SelectedItems.Count > 0)
                {
                    this.listTable1.Items.Remove(listTable1.SelectedItems[0]);
                }
            }
            IsHasItem();
        }

        private void btn_Del_Click(object sender, System.EventArgs e)
        {
            int c = this.listTable2.SelectedItems.Count;
            ListBox.SelectedObjectCollection objs = this.listTable2.SelectedItems;
            for (int i = 0; i < c; i++)
            {
                this.listTable1.Items.Add(listTable2.SelectedItems[i]);

            }
            for (int i = 0; i < c; i++)
            {
                if (this.listTable2.SelectedItems.Count > 0)
                {
                    this.listTable2.Items.Remove(listTable2.SelectedItems[0]);
                }
            }

            IsHasItem();
        }

        private void btn_Dellist_Click(object sender, System.EventArgs e)
        {
            int c = this.listTable2.Items.Count;
            for (int i = 0; i < c; i++)
            {
                this.listTable1.Items.Add(this.listTable2.Items[i]);
            }
            this.listTable2.Items.Clear();
            IsHasItem();
        }

        private void listTable1_DoubleClick(object sender, System.EventArgs e)
        {
            if (this.listTable1.SelectedItem != null)
            {
                this.listTable2.Items.Add(listTable1.SelectedItem);
                this.listTable1.Items.Remove(this.listTable1.SelectedItem);
                IsHasItem();
            }
        }

        private void listTable2_DoubleClick(object sender, System.EventArgs e)
        {
            if (this.listTable2.SelectedItem != null)
            {
                this.listTable1.Items.Add(listTable2.SelectedItem);
                this.listTable2.Items.Remove(this.listTable2.SelectedItem);
                IsHasItem();
            }
        }
        /// <summary>
        /// �ж�listbox��û����Ŀ
        /// </summary>
        private void IsHasItem()
        {
            if (this.listTable1.Items.Count > 0)
            {
                this.btn_Add.Enabled = true;
                this.btn_Addlist.Enabled = true;
            }
            else
            {
                this.btn_Add.Enabled = false;
                this.btn_Addlist.Enabled = false;
            }
            if (this.listTable2.Items.Count > 0)
            {
                this.btn_Del.Enabled = true;
                this.btn_Dellist.Enabled = true;
                this.btn_Export.Enabled = true;
                SetlblStatuText("��ǰѡ��" + listTable2.Items.Count.ToString());
            }
            else
            {
                this.btn_Del.Enabled = false;
                this.btn_Dellist.Enabled = false;
                this.btn_Export.Enabled = false;
            }
        }
        #endregion

        #region �첽�ؼ�����
        public void SetBtnEnable()
        {
            if (this.btn_Export.InvokeRequired)
            {
                SetBtnEnableCallback d = new SetBtnEnableCallback(SetBtnEnable);
                this.Invoke(d, null);
            }
            else
            {
                this.btn_Export.Enabled = true;
                this.btn_Cancle.Enabled = true;
            }
        }
        public void SetBtnDisable()
        {
            if (this.btn_Export.InvokeRequired)
            {
                SetBtnDisableCallback d = new SetBtnDisableCallback(SetBtnDisable);
                this.Invoke(d, null);
            }
            else
            {
                this.btn_Export.Enabled = false;
                this.btn_Cancle.Enabled = false;
            }
        }
        public void SetlblStatuText(string text)
        {
            if (this.labelNum.InvokeRequired)
            {
                SetlblStatuCallback d = new SetlblStatuCallback(SetlblStatuText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelNum.Text = text;
            }
        }
        /// <summary>
        /// ѭ����ַ�������ֵ
        /// </summary>
        /// <param name="val"></param>
        public void SetprogressBar1Max(int val)
        {
            if (this.progressBar1.InvokeRequired)
            {
                SetProBar1MaxCallback d = new SetProBar1MaxCallback(SetprogressBar1Max);
                this.Invoke(d, new object[] { val });
            }
            else
            {
                this.progressBar1.Maximum = val;

            }
        }
        /// <summary>
        /// ѭ����ַ����
        /// </summary>
        /// <param name="val"></param>
        public void SetprogressBar1Val(int val)
        {
            if (this.progressBar1.InvokeRequired)
            {
                SetProBar1ValCallback d = new SetProBar1ValCallback(SetprogressBar1Val);
                this.Invoke(d, new object[] { val });
            }
            else
            {
                this.progressBar1.Value = val;

            }
        }
        #endregion


        #region ��ť
        private void btn_Cancle_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btn_Export_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (this.txtTargetFolder.Text.Trim() == "")
                {
                    MessageBox.Show("Ŀ���ļ���Ϊ�գ�");
                    return;
                }
                cfgfile.IniWriteValue("Project", "lastpath", txtTargetFolder.Text.Trim());
                dbname = this.cmbDB.Text;
                pictureBox1.Visible = true;
                mythread = new Thread(new ThreadStart(ThreadWork));
                mythread.Start();
                //ThreadWork();
            }
            catch (System.Exception ex)
            {
                LogInfo.WriteLog(ex);
                MessageBox.Show(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }
        private void btn_TargetFold_Click(object sender, System.EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            DialogResult result = folder.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                this.txtTargetFolder.Text = folder.SelectedPath;
            }
        }
        #endregion

        void ThreadWork()
        {
            try
            {
                SetBtnDisable();
                string strnamespace = this.txtNamespace.Text.Trim();
                string strfolder = this.txtFolder.Text.Trim();
                int tblCount = this.listTable2.Items.Count;

                SetprogressBar1Max(tblCount);
                SetprogressBar1Val(1);
                SetlblStatuText("0");

                cb.DbName = dbname;
                if (strnamespace != "")
                {
                    cb.NameSpace = strnamespace;
                    dbset.Namepace = strnamespace;
                }
                cb.Folder = strfolder;
                dbset.Folder = strfolder;
                cb.DbHelperName = txtDbHelper.Text.Trim();
                cb.ProcPrefix = dbset.ProcPrefix;
                dbset.DbHelperName = txtDbHelper.Text.Trim();

                Maticsoft.CmConfig.DbConfig.UpdateSettings(dbset);
                //Maticsoft.CmConfig.ModuleConfig.SaveSettings(setting);

                #region �����չ����
                DataTable dtEx = dbobj.GetTablesExProperty(dbname);

                #endregion


                #region ѭ��ÿ����

                for (int i = 0; i < tblCount; i++)
                {
                    string tablename = this.listTable2.Items[i].ToString();
                    cb.TableName = tablename;
                    cb.ModelName = tablename;
                    if (dtEx != null)
                    {
                        try
                        {
                            DataRow[] drs = dtEx.Select("objname='" + tablename + "'");
                            if (drs.Length > 0)
                            {
                                if (drs[0]["value"] != null)
                                {
                                    cb.TableDescription = drs[0]["value"].ToString();
                                }
                            }
                        }
                        catch
                        { }
                    }

                    string tabpre = txtTabNamepre.Text.Trim();
                    if (tabpre != "")
                    {
                        if (tablename.StartsWith(tabpre))
                        {
                            cb.ModelName = tablename.Substring(tabpre.Length);
                        }
                    }
                    string strmodelname = cb.ModelName;
                    //����������
                    cb.ModelName = NameRule.GetModelClass(strmodelname, dbset);
                    cb.BLLName = NameRule.GetBLLClass(strmodelname, dbset);
                    cb.DALName = NameRule.GetDALClass(strmodelname, dbset);

                    DataTable dtkey = dbobj.GetKeyName(dbname, tablename);
                    List<ColumnInfo> collist = dbobj.GetColumnInfoList(dbname, tablename);
                    cb.Fieldlist = collist;
                    cb.Keys = CodeCommon.GetColumnInfos(dtkey);
                    CreatCS();

                    SetprogressBar1Val(i + 1);
                    SetlblStatuText((i + 1).ToString());
                }

                #endregion


                SetBtnEnable();
                MessageBox.Show(this, "�ĵ����ɳɹ���", "���", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception er)
            {
                LogInfo.WriteLog(er);
                MessageBox.Show(er.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }   
        }

        #region ���� C#����

        //�õ����ݲ�����
        private string GetDALType()
        {
            string daltype = "";
            daltype = cm_daltype.AppGuid;
            if ((daltype == "") || (daltype == "System.Data.DataRowView"))
            {
                MessageBox.Show("��ѡ�����ݲ��������ͣ���رպ����ԣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "";
            }
            return daltype;

        }
        //�õ�bll������
        private string GetBLLType()
        {
            string blltype = "";
            blltype = cm_blltype.AppGuid;
            if ((blltype == "") || (blltype == "System.Data.DataRowView"))
            {
                MessageBox.Show("��ѡ��ҵ����������ͣ���رպ����ԣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "";
            }
            return blltype;
        }

        //�õ����ݲ�����
        public string GetWebType()
        {
            string webtype = "";
            webtype = cm_webtype.AppGuid;
            if ((webtype == "") || (webtype == "System.Data.DataRowView"))
            {
                MessageBox.Show("��ѡ���ʾ���������ͣ���رպ����ԣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "";
            }
            return webtype;
        }

        ////�õ�Model������
        //private string GetModelType()
        //{
        //    string blltype = "";
        //    blltype = cm_blltype.AppGuid;
        //    if ((blltype == "") && (blltype == "System.Data.DataRowView"))
        //    {
        //        MessageBox.Show("ѡ������ݲ�����������رպ����ԣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return "";
        //    }
        //    return blltype;
        //}

        //�ܹ�ѡ��
        private void CreatCS()
        {
            if (this.radBtn_One.Checked)
            {
                CreatCsOne();
            }
            if (this.radBtn_S3.Checked)
            {
                CreatCsS3();
            }
            if (this.radBtn_F3.Checked)
            {
                CreatCsF3();
            }

        }

        //�Ѵ���д��ָ���ļ�
        private void WriteFile(string Filename, string strCode)
        {
            StreamWriter sw = new StreamWriter(Filename, false, Encoding.UTF8);//,false);
            sw.Write(strCode);
            sw.Flush();
            sw.Close();
        }

        #region ����ṹ
        private void CreatCsOne()
        {
            string strnamespace = this.txtNamespace.Text.Trim();
            string strfolder = this.txtFolder.Text.Trim();
            if (strfolder.Trim() != "")
            {
                cb.NameSpace = strnamespace + "." + strfolder;
                cb.Folder = strfolder;
            }
            string strCode = cb.GetCodeFrameOne(GetDALType(), true, true, true, true, true, true, true);

            string TargetFolder = this.txtTargetFolder.Text;
            FolderCheck(TargetFolder);

            string classFolder = TargetFolder + "\\Class";
            FolderCheck(classFolder);
            string filename = classFolder + "\\" + cb.ModelName + ".cs";
            WriteFile(filename, strCode);
        }
        #endregion

        #region ������
        private void CreatCsS3()
        {
            string TargetFolder = this.txtTargetFolder.Text;
            FolderCheck(TargetFolder);

            #region Model����
            string modelFolder = TargetFolder + "\\Model";
            if (cb.Folder != "")
            {
                modelFolder = modelFolder + "\\" + cb.Folder;
            }
            FolderCheck(modelFolder);

            string filemodel = modelFolder + "\\" + cb.ModelName + ".cs";
            string strmodel = cb.GetCodeFrameS3Model();

            string copyrightmodel = copyrightstr.Replace("<$$modelname$$>", cb.ModelName).Replace("<$$datetime$$>",DateTime.Now.ToString()) + "\r\n";

            WriteFile(filemodel, copyrightmodel + strmodel);
            AddClassFile(modelFolder + "\\Model.csproj", cb.ModelName + ".cs", "");
            #endregion

            #region DAL����
            string dalFolder = TargetFolder + "\\DAL";
            if (cb.Folder != "")
            {
                dalFolder = dalFolder + "\\" + cb.Folder;
            }
            FolderCheck(dalFolder);
            string filedal = dalFolder + "\\" + cb.DALName + ".cs";
            string strdal = cb.GetCodeFrameS3DAL(GetDALType(), true, true, true, true, true, true, true);

            string copyrightdal = copyrightstr.Replace("<$$modelname$$>", cb.DALName).Replace("<$$datetime$$>", DateTime.Now.ToString()) + "\r\n";

            WriteFile(filedal, copyrightdal + strdal);
            AddClassFile(dalFolder + "\\DAL.csproj", cb.DALName + ".cs", "");
            #endregion

            #region BLL����
            string bllFolder = TargetFolder + "\\BLL";
            if (cb.Folder != "")
            {
                bllFolder = bllFolder + "\\" + cb.Folder;
            }
            FolderCheck(bllFolder);
            string filebll = bllFolder + "\\" + cb.BLLName + ".cs";
            string blltype = GetBLLType();
            string strbll = cb.GetCodeFrameS3BLL(blltype, true, true, true, true, true, true, true, true);

            string copyrightbll = copyrightstr.Replace("<$$modelname$$>", cb.BLLName).Replace("<$$datetime$$>", DateTime.Now.ToString()) + "\r\n";

            WriteFile(filebll, copyrightbll + strbll);
            AddClassFile(bllFolder + "\\BLL.csproj", cb.BLLName + ".cs", "");
            #endregion


            #region web����
            string webtype = GetWebType();
            cb.CreatBuilderWeb(webtype);

            //���ɵ�Ŀ��·��
            string webFolder = TargetFolder + "\\Web";
            if (cb.Folder != "")
            {
                webFolder = webFolder + "\\" + cb.Folder;
            }
            FolderCheck(webFolder);
            FolderCheck(webFolder + "\\" + cb.ModelName);
            string tempstr = "";

            //ģ��ҳ���·��
            string webTemplatePath = Application.StartupPath + @"\Template\web";
            if (dbset.WebTemplatePath != null && dbset.WebTemplatePath.Length > 0)
            {
                webTemplatePath = dbset.WebTemplatePath;
            }

            //����Ŀ¼·����Ӧ�������ռ�·��
            string namespacePath = cb.ModelName;
            if (cb.Folder.Length > 0)
            {
                namespacePath = cb.Folder + "." + namespacePath;
            }
            string tabdesc = CutTableDescription(cb.TableName, cb.TableDescription);

            #region ADD
            string fileaspx = webFolder + "\\" + cb.ModelName + "\\Add.aspx";
            string fileaspxcs = webFolder + "\\" + cb.ModelName + "\\Add.aspx.cs";
            string fileaspxds = webFolder + "\\" + cb.ModelName + "\\Add.aspx.designer.cs";

            string tempaspx = webTemplatePath + @"\Add.aspx";
            string tempaspxcs = webTemplatePath + @"\Add.aspx.cs";
            string tempaspxds = webTemplatePath + @"\Add.aspx.designer.cs";

            if (File.Exists(tempaspx))
            {
                using (StreamReader sr = new StreamReader(tempaspx, Encoding.Default))
                {
                    string s = cb.GetAddAspx();
                    tempstr = sr.ReadToEnd().Replace(".Demo.Add", "." + namespacePath + ".Add").Replace("<$$AddAspx$$>", s).Replace("<$$TableDescription$$>", tabdesc);
                    sr.Close();
                }
                WriteFile(fileaspx, tempstr);
            }
            if (File.Exists(tempaspxcs))
            {
                using (StreamReader sr = new StreamReader(tempaspxcs, Encoding.Default))
                {
                    string s = cb.GetAddAspxCs();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + namespacePath).Replace("<$$AddAspxCs$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxcs, tempstr);
            }
            if (File.Exists(tempaspxds))
            {
                using (StreamReader sr = new StreamReader(tempaspxds, Encoding.Default))
                {
                    string s = cb.GetAddDesigner();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + namespacePath).Replace("<$$AddDesigner$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxds, tempstr);
            }
           
            #endregion

            #region Modify.aspx
            fileaspx = webFolder + "\\" + cb.ModelName + "\\Modify.aspx";
            fileaspxcs = webFolder + "\\" + cb.ModelName + "\\Modify.aspx.cs";
            fileaspxds = webFolder + "\\" + cb.ModelName + "\\Modify.aspx.designer.cs";

            tempaspx = webTemplatePath + @"\Modify.aspx";
            tempaspxcs = webTemplatePath + @"\Modify.aspx.cs";
            tempaspxds = webTemplatePath + @"\Modify.aspx.designer.cs";
            if (File.Exists(tempaspx))
            {
                using (StreamReader sr = new StreamReader(tempaspx, Encoding.Default))
                {
                    string s = cb.GetUpdateAspx();
                    tempstr = sr.ReadToEnd().Replace(".Demo.Modify", "." + namespacePath + ".Modify").Replace("<$$ModifyAspx$$>", s).Replace("<$$TableDescription$$>", tabdesc);
                    sr.Close();
                }
                WriteFile(fileaspx, tempstr);
            }
            if (File.Exists(tempaspxcs))
            {
                using (StreamReader sr = new StreamReader(tempaspxcs, Encoding.Default))
                {
                    string s = cb.GetUpdateAspxCs();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + namespacePath).Replace("<$$ModifyAspxCs$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxcs, tempstr);
            }
            if (File.Exists(tempaspxds))
            {
                using (StreamReader sr = new StreamReader(tempaspxds, Encoding.Default))
                {
                    string s = cb.GetUpdateDesigner();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + namespacePath).Replace("<$$ModifyDesigner$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxds, tempstr);
            }
           
            #endregion

            #region Show
            fileaspx = webFolder + "\\" + cb.ModelName + "\\Show.aspx";
            fileaspxcs = webFolder + "\\" + cb.ModelName + "\\Show.aspx.cs";
            fileaspxds = webFolder + "\\" + cb.ModelName + "\\Show.aspx.designer.cs";

            tempaspx = webTemplatePath + @"\Show.aspx";
            tempaspxcs = webTemplatePath + @"\Show.aspx.cs";
            tempaspxds = webTemplatePath + @"\Show.aspx.designer.cs";
            if (File.Exists(tempaspx))
            {
                using (StreamReader sr = new StreamReader(tempaspx, Encoding.Default))
                {
                    string s = cb.GetShowAspx();
                    tempstr = sr.ReadToEnd().Replace(".Demo.Show", "." + namespacePath + ".Show").Replace("<$$ShowAspx$$>", s).Replace("<$$TableDescription$$>", tabdesc);
                    sr.Close();
                }
                WriteFile(fileaspx, tempstr);
            }
            if (File.Exists(tempaspxcs))
            {
                using (StreamReader sr = new StreamReader(tempaspxcs, Encoding.Default))
                {
                    string s = cb.GetShowAspxCs();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + namespacePath).Replace("<$$ShowAspxCs$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxcs, tempstr);
            }
            if (File.Exists(tempaspxds))
            {
                using (StreamReader sr = new StreamReader(tempaspxds, Encoding.Default))
                {
                    string s = cb.GetShowDesigner();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + namespacePath).Replace("<$$ShowDesigner$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxds, tempstr);
            }

            #endregion


            #region List
            fileaspx = webFolder + "\\" + cb.ModelName + "\\List.aspx";
            fileaspxcs = webFolder + "\\" + cb.ModelName + "\\List.aspx.cs";
            fileaspxds = webFolder + "\\" + cb.ModelName + "\\List.aspx.designer.cs";

            tempaspx = webTemplatePath + @"\List.aspx";
            tempaspxcs = webTemplatePath + @"\List.aspx.cs";
            tempaspxds = webTemplatePath + @"\List.aspx.designer.cs";

            if (File.Exists(tempaspx))
            {
                //���������ֶΣ���������������
                string keyField = "";
                string keyFieldParams = "";
                if (cb.Keys.Count == 1)
                {
                    keyField = cb.Keys[0].ColumnName;
                    keyFieldParams = "id={0}";
                }
                else
                {
                    StringPlus tempkeyfields = new StringPlus();
                    StringPlus tempkeyfieldParams = new StringPlus();
                    for (int n = 0; n < cb.Keys.Count; n++)
                    {
                        ColumnInfo field = cb.Keys[n];
                        if (field.IsIdentity)
                        {
                            keyField = field.ColumnName;
                            keyFieldParams = "id={0}";
                            break;
                        }
                        else
                        {
                            tempkeyfields.Append(field.ColumnName + ",");
                            tempkeyfieldParams.Append("id" + n.ToString() + "={" + n.ToString() + "}&");
                        }
                    }
                    if (keyField.Length == 0) //����������У����Զ������У����ö�����ѭ����
                    {
                        tempkeyfields.DelLastComma();
                        keyField = tempkeyfields.Value;
                        tempkeyfieldParams.DelLastChar("&");
                        keyFieldParams = tempkeyfieldParams.Value;
                    }
                }

                using (StreamReader sr = new StreamReader(tempaspx, Encoding.Default))
                {
                    string s = cb.GetListAspx();
                    tempstr = sr.ReadToEnd().Replace(".Demo.List", "." + namespacePath + ".List").Replace("<$$ListAspx$$>", s);
                    tempstr = tempstr.Replace("<$$KeyField$$>", keyField).Replace("<$$TableDescription$$>", tabdesc).Replace("<$$KeyFieldParams$$>", keyFieldParams);
                    sr.Close();
                }
                WriteFile(fileaspx, tempstr);
            }

            if (File.Exists(tempaspxcs))
            {
                using (StreamReader sr = new StreamReader(tempaspxcs, Encoding.Default))
                {
                    string s = cb.GetListAspxCs();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + namespacePath).Replace("<$$ListAspxCs$$>", s);
                    //����ɾ������
                    string keyField = "";
                    if (cb.Keys.Count == 1)
                    {
                        keyField = cb.Keys[0].ColumnName;
                    }
                    else
                    {
                        foreach (ColumnInfo field in cb.Keys)
                        {
                            if (field.IsIdentity)
                            {
                                keyField = field.ColumnName;
                                break;
                            }
                        }
                    }
                    if (keyField.Trim().Length == 0)
                    {
                        tempstr = tempstr.Replace("bll.DeleteList(idlist)", "#warning �������ɾ��棺����������޷���������ɾ�������ֹ��޸Ĵ���. //bll.DeleteList(idlist)");
                    }
                    sr.Close();
                }
                WriteFile(fileaspxcs, tempstr);
            }
            if (File.Exists(tempaspxds))
            {
                using (StreamReader sr = new StreamReader(tempaspxds, Encoding.Default))
                {
                    string s = cb.GetListDesigner();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + namespacePath).Replace("<$$ListDesigner$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxds, tempstr);
            }
            #endregion


            #endregion

            CheckDirectory(TargetFolder);
        }
        #endregion

        #region ����ģʽ����

        private void CreatCsF3()
        {
            string TargetFolder = this.txtTargetFolder.Text;
            FolderCheck(TargetFolder);

            #region Model����
            string modelFolder = TargetFolder + "\\Model";
            if (cb.Folder != "")
            {
                modelFolder = modelFolder + "\\" + cb.Folder;
            }
            FolderCheck(modelFolder);
            string filemodel = modelFolder + "\\" + cb.ModelName + ".cs";

            string copyrightmodel = copyrightstr.Replace("<$$modelname$$>", cb.ModelName).Replace("<$$datetime$$>", DateTime.Now.ToString()) + "\r\n";

            string strmodel = cb.GetCodeFrameS3Model();
            WriteFile(filemodel, copyrightmodel+strmodel);
            AddClassFile(modelFolder + "\\Model.csproj", cb.ModelName + ".cs", "");
            #endregion

            #region DAL����
            string strdbtype = dbobj.DbType;
            if (dbobj.DbType == "SQL2000" || dbobj.DbType == "SQL2005"
                || dbobj.DbType == "SQL2008" || dbobj.DbType == "SQL2012")
            {
                strdbtype = "SQLServer";
            }
            string dalFolder = TargetFolder + "\\" + strdbtype + "DAL";
            if (cb.Folder != "")
            {
                dalFolder = dalFolder + "\\" + cb.Folder;
            }
            FolderCheck(dalFolder);
            string filedal = dalFolder + "\\" + cb.DALName + ".cs";
            string strdal = cb.GetCodeFrameF3DAL(GetDALType(), true, true, true, true, true, true, true);

            string copyrightdal = copyrightstr.Replace("<$$modelname$$>", cb.DALName).Replace("<$$datetime$$>", DateTime.Now.ToString()) + "\r\n";

            WriteFile(filedal, copyrightdal+strdal);

            AddClassFile(dalFolder + "\\" + strdbtype + "DAL.csproj", cb.DALName + ".cs", "");
            #endregion

            #region  DALFactory
            string factoryFolder = TargetFolder + "\\DALFactory";
            FolderCheck(factoryFolder);
            string filedalfac = factoryFolder + "\\DataAccess.cs";
            string strdalfac = cb.GetCodeFrameF3DALFactory();
            //�Ѿ����ڣ����������ݣ���׷��
            if (File.Exists(filedalfac))
            {
                string temp = File.ReadAllText(filedalfac);
                if (temp.IndexOf("class DataAccess") > 0)
                {
                    strdalfac = cb.GetCodeFrameF3DALFactoryMethod();
                    vsp.AddMethodToClass(filedalfac, strdalfac);
                }
                else
                {
                    strdalfac = cb.GetCodeFrameF3DALFactory();
                    StreamWriter sw = new StreamWriter(filedalfac, true, Encoding.UTF8);
                    sw.Write(strdalfac);
                    sw.Flush();
                    sw.Close();
                }
            }
            else//�����½����ļ�
            {
                strdalfac = cb.GetCodeFrameF3DALFactory();
                WriteFile(filedalfac, strdalfac);
            }
            #endregion

            #region  IDAL����
            string idalFolder = TargetFolder + "\\IDAL";
            if (cb.Folder != "")
            {
                idalFolder = idalFolder + "\\" + cb.Folder;
            }
            FolderCheck(idalFolder);
            string fileidal = idalFolder + "\\I" + cb.DALName + ".cs";
            string stridal = cb.GetCodeFrameF3IDAL(true, true, true, true, true, true, true, true);

            string copyrightidal = copyrightstr.Replace("<$$modelname$$>", cb.DALName).Replace("<$$datetime$$>", DateTime.Now.ToString()) + "\r\n";

            WriteFile(fileidal,copyrightidal+ stridal);
            AddClassFile(idalFolder + "\\IDAL.csproj", "I" + cb.DALName + ".cs", "");
            #endregion

            #region BLL����
            string bllFolder = TargetFolder + "\\BLL";
            if (cb.Folder != "")
            {
                bllFolder = bllFolder + "\\" + cb.Folder;
            }
            FolderCheck(bllFolder);
            string filebll = bllFolder + "\\" + cb.BLLName + ".cs";
            string blltype = GetBLLType();
            string strbll = cb.GetCodeFrameF3BLL(blltype, true, true, true, true, true, true, true, true);

            string copyrightbll = copyrightstr.Replace("<$$modelname$$>", cb.BLLName).Replace("<$$datetime$$>", DateTime.Now.ToString()) + "\r\n";

            WriteFile(filebll, copyrightbll+strbll);
            AddClassFile(bllFolder + "\\BLL.csproj", cb.BLLName + ".cs", "");
            #endregion

            #region web����

            string webtype = GetWebType();
            cb.CreatBuilderWeb(webtype);

            //���ɵ�Ŀ��·��
            string webFolder = TargetFolder + "\\Web";
            if (cb.Folder != "")
            {
                webFolder = webFolder + "\\" + cb.Folder;
            }
            FolderCheck(webFolder);
            FolderCheck(webFolder + "\\" + cb.ModelName);
            string tempstr = "";

            //ģ��ҳ���·��
            string webTemplatePath = Application.StartupPath + @"\Template\web";
            if (dbset.WebTemplatePath != null && dbset.WebTemplatePath.Length > 0)
            {
                webTemplatePath = dbset.WebTemplatePath;
            }


            //����Ŀ¼·����Ӧ�������ռ�·��
            string namespacePath = cb.ModelName;
            if (cb.Folder.Length > 0)
            {
                namespacePath = cb.Folder + "." + namespacePath;
            }

            string tabdesc = CutTableDescription(cb.TableName, cb.TableDescription);

            #region ADD
            string fileaspx = webFolder + "\\" + cb.ModelName + "\\Add.aspx";
            string fileaspxcs = webFolder + "\\" + cb.ModelName + "\\Add.aspx.cs";
            string fileaspxds = webFolder + "\\" + cb.ModelName + "\\Add.aspx.designer.cs";

            string tempaspx = webTemplatePath + @"\Add.aspx";
            string tempaspxcs = webTemplatePath + @"\Add.aspx.cs";
            string tempaspxds = webTemplatePath + @"\Add.aspx.designer.cs";
            if (File.Exists(tempaspx))
            {
                using (StreamReader sr = new StreamReader(tempaspx, Encoding.Default))
                {
                    string s = cb.GetAddAspx();
                    tempstr = sr.ReadToEnd().Replace(".Demo.Add", "." + namespacePath + ".Add").Replace("<$$AddAspx$$>", s).Replace("<$$TableDescription$$>", tabdesc);
                    sr.Close();
                }
                WriteFile(fileaspx, tempstr);
            }
            if (File.Exists(tempaspxcs))
            {
                using (StreamReader sr = new StreamReader(tempaspxcs, Encoding.Default))
                {
                    string s = cb.GetAddAspxCs();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + namespacePath).Replace("<$$AddAspxCs$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxcs, tempstr);
            }
            if (File.Exists(tempaspxds))
            {
                using (StreamReader sr = new StreamReader(tempaspxds, Encoding.Default))
                {
                    string s = cb.GetAddDesigner();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + namespacePath).Replace("<$$AddDesigner$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxds, tempstr);
            }
            
            #endregion

            #region Modify.aspx
            fileaspx = webFolder + "\\" + cb.ModelName + "\\Modify.aspx";
            fileaspxcs = webFolder + "\\" + cb.ModelName + "\\Modify.aspx.cs";
            fileaspxds = webFolder + "\\" + cb.ModelName + "\\Modify.aspx.designer.cs";

            tempaspx = webTemplatePath + @"\Modify.aspx";
            tempaspxcs = webTemplatePath + @"\Modify.aspx.cs";
            tempaspxds = webTemplatePath + @"\Modify.aspx.designer.cs";
            if (File.Exists(tempaspx))
            {
                using (StreamReader sr = new StreamReader(tempaspx, Encoding.Default))
                {
                    string s = cb.GetUpdateAspx();
                    tempstr = sr.ReadToEnd().Replace(".Demo.Modify", "." + namespacePath + ".Modify").Replace("<$$ModifyAspx$$>", s).Replace("<$$TableDescription$$>", tabdesc);
                    sr.Close();
                }
                WriteFile(fileaspx, tempstr);
            }
            if (File.Exists(tempaspxcs))
            {
                using (StreamReader sr = new StreamReader(tempaspxcs, Encoding.Default))
                {
                    string s = cb.GetUpdateAspxCs();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + namespacePath).Replace("<$$ModifyAspxCs$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxcs, tempstr);
            }
            if (File.Exists(tempaspxds))
            {
                using (StreamReader sr = new StreamReader(tempaspxds, Encoding.Default))
                {
                    string s = cb.GetUpdateDesigner();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + namespacePath).Replace("<$$ModifyDesigner$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxds, tempstr);
            }
            //AddClassFile(webFolder + "\\Web.csproj", cb.ModelName + "\\Modify.aspx", "2005");
            //AddClassFile(webFolder + "\\Web.csproj", cb.ModelName + "\\Modify.aspx.cs", "2005");
            //AddClassFile(webFolder + "\\Web.csproj", cb.ModelName + "\\Modify.aspx.designer.cs", "2005");
            #endregion

            #region Show
            fileaspx = webFolder + "\\" + cb.ModelName + "\\Show.aspx";
            fileaspxcs = webFolder + "\\" + cb.ModelName + "\\Show.aspx.cs";
            fileaspxds = webFolder + "\\" + cb.ModelName + "\\Show.aspx.designer.cs";

            tempaspx = webTemplatePath + @"\Show.aspx";
            tempaspxcs = webTemplatePath + @"\Show.aspx.cs";
            tempaspxds = webTemplatePath + @"\Show.aspx.designer.cs";
            if (File.Exists(tempaspx))
            {
                using (StreamReader sr = new StreamReader(tempaspx, Encoding.Default))
                {
                    string s = cb.GetShowAspx();
                    tempstr = sr.ReadToEnd().Replace(".Demo.Show", "." + namespacePath + ".Show").Replace("<$$ShowAspx$$>", s).Replace("<$$TableDescription$$>", tabdesc);
                    sr.Close();
                }
                WriteFile(fileaspx, tempstr);
            }
            if (File.Exists(tempaspxcs))
            {
                using (StreamReader sr = new StreamReader(tempaspxcs, Encoding.Default))
                {
                    string s = cb.GetShowAspxCs();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + namespacePath).Replace("<$$ShowAspxCs$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxcs, tempstr);
            }

            if (File.Exists(tempaspxds))
            {
                using (StreamReader sr = new StreamReader(tempaspxds, Encoding.Default))
                {
                    string s = cb.GetShowDesigner();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + namespacePath).Replace("<$$ShowDesigner$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxds, tempstr);
            }

            #endregion

            #region delete
            fileaspx = webFolder + "\\" + cb.ModelName + "\\delete.aspx";
            fileaspxcs = webFolder + "\\" + cb.ModelName + "\\delete.aspx.cs";
            fileaspxds = webFolder + "\\" + cb.ModelName + "\\delete.aspx.designer.cs";

            tempaspx = webTemplatePath + @"\delete.aspx";
            tempaspxcs = webTemplatePath + @"\delete.aspx.cs";
            tempaspxds = webTemplatePath + @"\delete.aspx.designer.cs";
            if (File.Exists(tempaspx))
            {
                using (StreamReader sr = new StreamReader(tempaspx, Encoding.Default))
                {
                    tempstr = sr.ReadToEnd().Replace(".Demo.delete", "." + namespacePath + ".delete");
                    sr.Close();
                }
                WriteFile(fileaspx, tempstr);
            }
            if (File.Exists(tempaspxcs))
            {
                using (StreamReader sr = new StreamReader(tempaspxcs, Encoding.Default))
                {
                    string s = cb.GetDeleteAspxCs();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + namespacePath).Replace("<$$DeleteAspxCs$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxcs, tempstr);
            }

            if (File.Exists(tempaspxds))
            {
                using (StreamReader sr = new StreamReader(tempaspxds, Encoding.Default))
                {                    
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + namespacePath);
                    sr.Close();
                }
                WriteFile(fileaspxds, tempstr);
            }
            #endregion


            #region List
            fileaspx = webFolder + "\\" + cb.ModelName + "\\List.aspx";
            fileaspxcs = webFolder + "\\" + cb.ModelName + "\\List.aspx.cs";
            fileaspxds = webFolder + "\\" + cb.ModelName + "\\List.aspx.designer.cs";

            tempaspx = webTemplatePath + @"\List.aspx";
            tempaspxcs = webTemplatePath + @"\List.aspx.cs";
            tempaspxds = webTemplatePath + @"\List.aspx.designer.cs";

            if (File.Exists(tempaspx))
            {
                //���������ֶΣ���������������
                string keyField = "";
                string keyFieldParams = "";
                if (cb.Keys.Count == 1)
                {
                    keyField = cb.Keys[0].ColumnName;
                    keyFieldParams = "id={0}";
                }
                else
                {
                    StringPlus tempkeyfields = new StringPlus();
                    StringPlus tempkeyfieldParams = new StringPlus();
                    for (int n = 0; n < cb.Keys.Count; n++)
                    {
                        ColumnInfo field = cb.Keys[n];
                        if (field.IsIdentity)
                        {
                            keyField = field.ColumnName;
                            keyFieldParams = "id={0}";
                            break;
                        }
                        else
                        {
                            tempkeyfields.Append(field.ColumnName + ",");
                            tempkeyfieldParams.Append("id" + n.ToString() + "={" + n.ToString() + "}&");
                        }
                    }
                    if (keyField.Length == 0) //����������У����Զ������У����ö�����ѭ����
                    {
                        tempkeyfields.DelLastComma();
                        keyField = tempkeyfields.Value;
                        tempkeyfieldParams.DelLastChar("&");
                        keyFieldParams = tempkeyfieldParams.Value;
                    }
                }

                using (StreamReader sr = new StreamReader(tempaspx, Encoding.Default))
                {
                    string s = cb.GetListAspx();
                    tempstr = sr.ReadToEnd().Replace(".Demo.List", "." + namespacePath + ".List").Replace("<$$ListAspx$$>", s);
                    tempstr = tempstr.Replace("<$$KeyField$$>", keyField).Replace("<$$TableDescription$$>", tabdesc).Replace("<$$KeyFieldParams$$>", keyFieldParams);
                    sr.Close();
                }
                WriteFile(fileaspx, tempstr);
            }

            if (File.Exists(tempaspxcs))
            {
                using (StreamReader sr = new StreamReader(tempaspxcs, Encoding.Default))
                {
                    string s = cb.GetListAspxCs();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + namespacePath).Replace("<$$ListAspxCs$$>", s);
                    //����ɾ������
                    string keyField = "";
                    if (cb.Keys.Count == 1)
                    {
                        keyField = cb.Keys[0].ColumnName;
                    }
                    else
                    {
                        foreach (ColumnInfo field in cb.Keys)
                        {
                            if (field.IsIdentity)
                            {
                                keyField = field.ColumnName;
                                break;
                            }
                        }
                    }
                    if (keyField.Trim().Length == 0)
                    {
                        tempstr = tempstr.Replace("bll.DeleteList(idlist)", "#warning �������ɾ��棺����������޷���������ɾ�������ֹ��޸Ĵ���. //bll.DeleteList(idlist)");
                    }
                    sr.Close();
                }
                WriteFile(fileaspxcs, tempstr);
            }
            if (File.Exists(tempaspxds))
            {
                using (StreamReader sr = new StreamReader(tempaspxds, Encoding.Default))
                {
                    string s = cb.GetListDesigner();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + namespacePath).Replace("<$$ListDesigner$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxds, tempstr);
            }
            #endregion

            #endregion

            CheckDirectory(TargetFolder);

        }

        #endregion

        #endregion

        #region ��������
        private void FolderCheck(string Folder)
        {
            DirectoryInfo target = new DirectoryInfo(Folder);
            if (!target.Exists)
            {
                target.Create();
            }
        }
        /// <summary>
        ///  �޸���Ŀ�ļ�
        /// </summary>
        /// <param name="ProjectFile">��Ŀ�ļ���</param>
        /// <param name="classFileName">���ļ���</param>
        /// <param name="ProType">��Ŀ����</param>
        private void AddClassFile(string ProjectFile, string classFileName, string ProType)
        {
            if (File.Exists(ProjectFile))
            {
                switch (ProType)
                {
                    case "2003":
                        vsp.AddClass2003(ProjectFile, classFileName);
                        break;
                    case "2005":
                        vsp.AddClass2005(ProjectFile, classFileName);
                        break;
                    default:
                        vsp.AddClass(ProjectFile, classFileName);
                        break;
                }
            }
        }
        /// <summary>
        /// �����ռ������
        /// </summary>
        /// <param name="SourceDirectory"></param>
        public void CheckDirectory(string SourceDirectory)
        {
            DirectoryInfo source = new DirectoryInfo(SourceDirectory);
            if (!source.Exists)
                return;

            FileInfo[] sourceFiles = source.GetFiles();
            int filescount = sourceFiles.Length;
            for (int i = 0; i < filescount; ++i)
            {
                //if ((sourceFiles[i].Extension == ".cs") || (sourceFiles[i].Extension == ".ascx") ||
                //    (sourceFiles[i].Extension == ".aspx") || (sourceFiles[i].Extension == ".csproj"))
                //{
                //ReplaceNamespace(sourceFiles[i].FullName, txtNamespace.Text.Trim()); 
                //}
                switch (sourceFiles[i].Extension)
                {
                    case ".csproj":
                        ReplaceNamespaceProj(sourceFiles[i].FullName, txtNamespace.Text.Trim());
                        break;
                    case ".cs":
                    case ".ascx":
                    case ".aspx":
                    case ".asax":
                    case ".master":
                        ReplaceNamespace(sourceFiles[i].FullName, txtNamespace.Text.Trim());
                        break;
                    default:
                        break;
                }
            }

            DirectoryInfo[] sourceDirectories = source.GetDirectories();
            for (int j = 0; j < sourceDirectories.Length; ++j)
            {
                CheckDirectory(sourceDirectories[j].FullName);
            }
        }
        private void ReplaceNamespace(string filename, string spacename)
        {
            StreamReader sr = new StreamReader(filename, Encoding.Default);
            string text = sr.ReadToEnd();
            sr.Close();

            text = text.Replace("<$$namespace$$>", spacename);
            //text = text.Replace("namespace Maticsoft", "namespace " + spacename);
            //text = text.Replace("Inherits=\"Maticsoft", "Inherits=\"" + spacename);

            StreamWriter sw = new StreamWriter(filename, false, Encoding.UTF8);//,false);
            sw.Write(text);
            sw.Flush();//�ӻ�����д����������ļ���
            sw.Close();
        }
        private void ReplaceNamespaceProj(string filename, string spacename)
        {
            StreamReader sr = new StreamReader(filename, Encoding.Default);
            string text = sr.ReadToEnd();
            sr.Close();

            text = text.Replace("<AssemblyName>Maticsoft.", "<AssemblyName>" + spacename + ".");
            text = text.Replace("<RootNamespace>Maticsoft.", "<RootNamespace>" + spacename + ".");

            StreamWriter sw = new StreamWriter(filename, false, Encoding.UTF8);//,false);
            sw.Write(text);
            sw.Flush();//�ӻ�����д����������ļ���
            sw.Close();
        }

        /// <summary>
        /// ���������Ϣ����
        /// </summary>       
        private string CutTableDescription(string TableName, string TableDescription)
        {
            string newDeText = "";
            if (TableDescription.Trim().Length > 0)
            {

                int n = 0;
                int n1 = TableDescription.IndexOf(";");
                int n2 = TableDescription.IndexOf("��");
                int n3 = TableDescription.IndexOf(",");

                n = Math.Min(n1, n2);
                if (n < 0)
                {
                    n = Math.Max(n1, n2);
                }
                n = Math.Min(n, n3);
                if (n < 0)
                {
                    n = Math.Max(n1, n2);
                }

                if (n > 0)
                {
                    newDeText = TableDescription.Trim().Substring(0, n);
                }
                else
                {
                    if (TableDescription.Trim().Length > 10)
                    {
                        newDeText = TableDescription.Trim().Substring(0, 10);
                    }
                    else
                    {
                        newDeText = TableDescription.Trim();
                    }
                }
            }
            else
            {
                newDeText = TableName;
            }
            return newDeText;
        }

        #endregion


        /// <summary>
        /// �ֹ��������������ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInputTxt_Click(object sender, EventArgs e)
        {
            CodeExpTabInput tabForm = new CodeExpTabInput();
            DialogResult dr= tabForm.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                listTable2.Items.Clear();
                foreach(string line in tabForm.txtTableList.Lines)
                {
                    listTable2.Items.Add(line);
                }
            }
            IsHasItem();
        }
    }
}
