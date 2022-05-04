using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
namespace Codematic
{
	/// <summary>
	/// LoginForm ��ժҪ˵����
	/// </summary>
	public class LoginOledb : System.Windows.Forms.Form
	{
		public System.Windows.Forms.GroupBox groupBox1;
		public System.Windows.Forms.Label label2;
		public System.Windows.Forms.Label label3;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ToolTip toolTip1;
		public System.Windows.Forms.TextBox txtServer;
		public System.Windows.Forms.TextBox txtUser;
		public System.Windows.Forms.TextBox txtPass;
		private System.ComponentModel.IContainer components;
		private WiB.Pinkie.Controls.ButtonXP btn_Ok;
		private WiB.Pinkie.Controls.ButtonXP btn_Cancel;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private WiB.Pinkie.Controls.ButtonXP btn_SelDb;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.RadioButton radBtn_DB;
		private System.Windows.Forms.RadioButton radBtn_Constr;
		public System.Windows.Forms.TextBox txtConstr;
        private Button btnBrowse;
        public OpenFileDialog openFileDialog1;

		Maticsoft.CmConfig.DbSettings dbobj=new Maticsoft.CmConfig.DbSettings();
		

		public LoginOledb()
		{			
			InitializeComponent();			
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		public void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginOledb));
            this.txtServer = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtConstr = new System.Windows.Forms.TextBox();
            this.btn_Ok = new WiB.Pinkie.Controls.ButtonXP();
            this.btn_Cancel = new WiB.Pinkie.Controls.ButtonXP();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.radBtn_DB = new System.Windows.Forms.RadioButton();
            this.btn_SelDb = new WiB.Pinkie.Controls.ButtonXP();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radBtn_Constr = new System.Windows.Forms.RadioButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(133, 31);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(250, 25);
            this.txtServer.TabIndex = 1;
            this.toolTip1.SetToolTip(this.txtServer, "����д���ݿ��ļ�������·����ַ��");
            this.txtServer.TextChanged += new System.EventHandler(this.txtServer_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtPass);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(32, 103);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(444, 136);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "��¼Ȩ�ޣ���ѡ����";
            // 
            // checkBox1
            // 
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(96, 99);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(117, 31);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "�հ�����";
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(181, 31);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(220, 25);
            this.txtUser.TabIndex = 3;
            this.txtUser.TextChanged += new System.EventHandler(this.txtUser_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(79, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "��¼��(&L)��";
            // 
            // txtPass
            // 
            this.txtPass.Enabled = false;
            this.txtPass.Location = new System.Drawing.Point(181, 62);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(220, 25);
            this.txtPass.TabIndex = 3;
            this.txtPass.TextChanged += new System.EventHandler(this.txtPass_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(96, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "����(&P)��";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(21, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 35);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // txtConstr
            // 
            this.txtConstr.Enabled = false;
            this.txtConstr.Location = new System.Drawing.Point(139, 15);
            this.txtConstr.Name = "txtConstr";
            this.txtConstr.Size = new System.Drawing.Size(424, 25);
            this.txtConstr.TabIndex = 0;
            this.toolTip1.SetToolTip(this.txtConstr, "����д���ݿ��ADO.NET�����ַ�����");
            // 
            // btn_Ok
            // 
            this.btn_Ok._Image = null;
            this.btn_Ok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btn_Ok.DefaultScheme = false;
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_Ok.Image = null;
            this.btn_Ok.Location = new System.Drawing.Point(217, 309);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
            this.btn_Ok.Size = new System.Drawing.Size(100, 30);
            this.btn_Ok.TabIndex = 19;
            this.btn_Ok.Text = "ȷ��(&O):";
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel._Image = null;
            this.btn_Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btn_Cancel.DefaultScheme = false;
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Image = null;
            this.btn_Cancel.Location = new System.Drawing.Point(377, 309);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
            this.btn_Cancel.Size = new System.Drawing.Size(100, 30);
            this.btn_Cancel.TabIndex = 20;
            this.btn_Cancel.Text = "ȡ��(&C):";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnBrowse);
            this.groupBox2.Controls.Add(this.txtServer);
            this.groupBox2.Controls.Add(this.radBtn_DB);
            this.groupBox2.Controls.Add(this.btn_SelDb);
            this.groupBox2.Location = new System.Drawing.Point(32, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(444, 78);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ѡ�����ݿ�";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(388, 31);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(48, 25);
            this.btnBrowse.TabIndex = 21;
            this.btnBrowse.Text = "ѡ��";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Visible = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // radBtn_DB
            // 
            this.radBtn_DB.Checked = true;
            this.radBtn_DB.Location = new System.Drawing.Point(21, 28);
            this.radBtn_DB.Name = "radBtn_DB";
            this.radBtn_DB.Size = new System.Drawing.Size(139, 31);
            this.radBtn_DB.TabIndex = 20;
            this.radBtn_DB.TabStop = true;
            this.radBtn_DB.Text = "���ļ���ַ��";
            this.radBtn_DB.Click += new System.EventHandler(this.radBtn_DB_Click);
            // 
            // btn_SelDb
            // 
            this.btn_SelDb._Image = null;
            this.btn_SelDb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btn_SelDb.DefaultScheme = false;
            this.btn_SelDb.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_SelDb.Image = null;
            this.btn_SelDb.Location = new System.Drawing.Point(509, 28);
            this.btn_SelDb.Name = "btn_SelDb";
            this.btn_SelDb.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
            this.btn_SelDb.Size = new System.Drawing.Size(54, 31);
            this.btn_SelDb.TabIndex = 19;
            this.btn_SelDb.Text = "...";
            this.btn_SelDb.Visible = false;
            this.btn_SelDb.Click += new System.EventHandler(this.btn_SelDb_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.txtConstr);
            this.groupBox3.Controls.Add(this.radBtn_Constr);
            this.groupBox3.Location = new System.Drawing.Point(32, 247);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(444, 51);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            // 
            // radBtn_Constr
            // 
            this.radBtn_Constr.Location = new System.Drawing.Point(16, 13);
            this.radBtn_Constr.Name = "radBtn_Constr";
            this.radBtn_Constr.Size = new System.Drawing.Size(139, 31);
            this.radBtn_Constr.TabIndex = 1;
            this.radBtn_Constr.Text = "�����ַ�����";
            this.radBtn_Constr.Click += new System.EventHandler(this.radBtn_Constr_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // LoginOledb
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(511, 354);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginOledb";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "�������ݿ�";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

		}
//		protected override void OnClosing(CancelEventArgs e)
//		{				
//			if(this.DialogResult==DialogResult.Cancel)
//			{					
//				this.Close();
//			}	
//			else
//			{
//				e.Cancel = true;
//			}
//			// otherwise, let the framework close the app
//		}


		#endregion

		private void LoginForm_Load(object sender, System.EventArgs e)
		{			
			this.toolTip1.SetToolTip(this.txtUser,"�뱣֤���û�����ÿ�����ݿ�ķ���Ȩ��");
            //try
            //{
            //    dbobj=Maticsoft.CmConfig.DbConfig.GetSetting("OleDb");
            //    if(dbobj!=null)
            //    {
            //        txtServer.Text=dbobj.Server;
            //        txtUser.Text=dbobj.Uid;
            //        txtPass.Text=dbobj.Password;					
            //    }

            //}
            //catch
            //{
            //    MessageBox.Show("��ȡ�����ļ�ʧ��!");
            //}
		}


		#region ��¼

		private void btn_Ok_Click(object sender, System.EventArgs e)
		{
			try
			{							
				string server=this.txtServer.Text.Trim();
				string user=this.txtUser.Text.Trim();
				string pass=this.txtPass.Text.Trim();
								
				if(this.radBtn_DB.Checked)
				{
                    GetConstr();
					if(server=="")
					{
						MessageBox.Show(this,"���ݿⲻ��Ϊ�գ�","����",MessageBoxButtons.OK,MessageBoxIcon.Information);
						return;
					}
				}

				if(this.radBtn_Constr.Checked)
				{
                    if (txtConstr.Text == "")
					{
						MessageBox.Show(this,"���ݿⲻ��Ϊ�գ�","����",MessageBoxButtons.OK,MessageBoxIcon.Information);
						return;
					}
				}
                string constr = this.txtConstr.Text;

                //��������
				OleDbConnection myCn = new OleDbConnection(constr);
				try
				{
                    this.Text = "�����������ݿ⣬���Ժ�...";
					myCn.Open();
				}
				catch(Exception ex)
				{
                    LogInfo.WriteLog(ex);
                    this.Text = "�������ݿ�ʧ�ܣ�";
                    MessageBox.Show(this, "�������ݿ�ʧ�ܣ�" + ex.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);					
					return;					
				}				
				myCn.Close();
                this.Text = "�������ݿ�ɹ���";

				if(dbobj==null)
					dbobj=new Maticsoft.CmConfig.DbSettings();

				//����ǰ����д�������ļ�
				dbobj.DbType="OleDb";
				dbobj.Server=server;
                dbobj.ConnectStr = constr;
                dbobj.DbName = "";
                dbobj.DbHelperName = "DbHelperOleDb";
                int result = Maticsoft.CmConfig.DbConfig.AddSettings(dbobj);
                switch (result)
                {
                    case 0:
                        MessageBox.Show(this, "��ӷ���������ʧ�ܣ������Ƿ���д��Ȩ�޻��ļ��Ƿ���ڣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    case 1:
                        break;
                    case 2:
                        {
                            DialogResult dr = MessageBox.Show(this, "�÷�������Ϣ�Ѿ����ڣ���ȷ���Ƿ񸲸ǵ�ǰ���ݿ����ã�", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (dr == DialogResult.Yes)
                            {
                                Maticsoft.CmConfig.DbConfig.DelSetting(dbobj.DbType, dbobj.Server, dbobj.DbName);
                                result = Maticsoft.CmConfig.DbConfig.AddSettings(dbobj);
                                if (result != 1)
                                {
                                    MessageBox.Show(this, "����ж�ص�ǰ�汾����ɾ����װĿ¼�����°�װ���°汾��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                        break;
                }
								              
				this.DialogResult=DialogResult.OK;
				this.Close();	
				
			}
			catch(System.Exception ex)
			{				
				MessageBox.Show(this,ex.Message,"����",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                LogInfo.WriteLog(ex);
			}
			
		}
		#endregion
		
		private void btn_Cancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
        }


        #region ѡ���ļ�
        private void btn_SelDb_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog sqlfiledlg=new OpenFileDialog();
			sqlfiledlg.Title="ѡ�����ݿ��ļ�";
            sqlfiledlg.Filter = "Access files (*.mdb;*.accdb)|*.mdb;*.accdb|�����ļ� (*.*)|*.*";
			DialogResult result=sqlfiledlg.ShowDialog(this);			
			if(result==DialogResult.OK)
			{
				this.txtServer.Text=sqlfiledlg.FileName;
                GetConstr();
                //this.txtConstr.Text=@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+txtServer.Text+";Persist Security Info=False";	
			}
        }

        #endregion

        #region ���������ַ���
        private void GetConstr()
		{
            if (txtServer.Text.Trim().Length < 2)
            {
                return;
            }
            FileInfo file = new FileInfo(txtServer.Text);
            string ext = file.Extension;
            switch (ext.ToLower().Trim())
            {
                case ".mdb":
                    txtConstr.Text = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtServer.Text + ";Persist Security Info=False";
                    break;
                case ".accdb":
                    txtConstr.Text = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtServer.Text + ";Persist Security Info=False";
                    break;
                default:
                    txtConstr.Text = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtServer.Text + ";Persist Security Info=False";		
                    break;
            }				
		}

		private void txtServer_TextChanged(object sender, System.EventArgs e)
		{
			GetConstr();			
		}
		private void txtUser_TextChanged(object sender, System.EventArgs e)
		{
			GetConstr();
		}
		private void txtPass_TextChanged(object sender, System.EventArgs e)
		{
			GetConstr();
        }
        #endregion

        #region  �ؼ�ѡ��
        private void radBtn_DB_Click(object sender, System.EventArgs e)
		{
			this.radBtn_Constr.Checked=false;
			this.checkBox1.Enabled=true;
			this.txtServer.Enabled=true;
			this.txtPass.Enabled=true;
			this.txtUser.Enabled=true;
			this.txtConstr.Enabled=false;		
		}

		private void radBtn_Constr_Click(object sender, System.EventArgs e)
		{
			this.radBtn_DB.Checked=false;
			this.checkBox1.Enabled=false;
			this.txtServer.Enabled=false;
			this.txtPass.Enabled=false;
			this.txtUser.Enabled=false;
			this.txtConstr.Enabled=true;
        }
        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                this.txtPass.Text = "";
                this.txtPass.Enabled = false;
            }
            else
            {
                this.txtPass.Enabled = true;
            }
        }

        #endregion

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Title = "ѡ�����ݿ��ļ�";
                openFileDialog1.Filter = "Access files (*.mdb;*.accdb)|*.mdb;*.accdb|�����ļ� (*.*)|*.*";
                DialogResult result = openFileDialog1.ShowDialog(this);
                if (DialogResult.OK == result)
                {
                    this.txtServer.Text = openFileDialog1.FileName;
                    GetConstr();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("ѡ�����ݿ��ļ�ʧ�ܣ�"+ex.Message);
            }
        }


    }
}
