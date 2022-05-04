using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Xml;

namespace Codematic
{
    /// <summary>
    /// FormLogin ��ժҪ˵����
    /// </summary>
    public class LoginOra : System.Windows.Forms.Form
    {
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private WiB.Pinkie.Controls.ButtonXP BtnOk;
        private WiB.Pinkie.Controls.ButtonXP BtnCancle;
        public System.Windows.Forms.TextBox txtUser;
        public System.Windows.Forms.TextBox txtPass;
        public System.Windows.Forms.TextBox txtServer;

        Maticsoft.CmConfig.DbSettings dbobj = new Maticsoft.CmConfig.DbSettings();
        public string constr = "";
        public string dbname = "";
        public CheckBox chk_Simple;
        private CheckBox chkboxConnectStr;
        private TextBox txtConnectStr;

        #region system
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.Container components = null;

        public LoginOra()
        {
            //
            // Windows ���������֧���������
            //
            InitializeComponent();

            //
            // TODO: �� InitializeComponent ���ú�����κι��캯������
            //
        }

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

        #region Windows ������������ɵĴ���
        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginOra));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.BtnOk = new WiB.Pinkie.Controls.ButtonXP();
            this.BtnCancle = new WiB.Pinkie.Controls.ButtonXP();
            this.chk_Simple = new System.Windows.Forms.CheckBox();
            this.chkboxConnectStr = new System.Windows.Forms.CheckBox();
            this.txtConnectStr = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(166, 276);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.label1.Location = new System.Drawing.Point(204, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(400, 1);
            this.label1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(197, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 29);
            this.label2.TabIndex = 2;
            this.label2.Text = " ��¼�����ݿ�";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(217, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "�û�����";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(208, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "����(P&)��";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(232, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "����";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(218, 276);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(374, 29);
            this.label6.TabIndex = 6;
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtUser
            // 
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUser.Location = new System.Drawing.Point(306, 73);
            this.txtUser.MaxLength = 30;
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(224, 25);
            this.txtUser.TabIndex = 7;
            // 
            // txtPass
            // 
            this.txtPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPass.Location = new System.Drawing.Point(306, 106);
            this.txtPass.MaxLength = 25;
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(224, 25);
            this.txtPass.TabIndex = 7;
            // 
            // txtServer
            // 
            this.txtServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServer.Location = new System.Drawing.Point(306, 139);
            this.txtServer.MaxLength = 30;
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(224, 25);
            this.txtServer.TabIndex = 7;
            // 
            // BtnOk
            // 
            this.BtnOk._Image = null;
            this.BtnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.BtnOk.DefaultScheme = true;
            this.BtnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOk.Image = null;
            this.BtnOk.Location = new System.Drawing.Point(234, 239);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
            this.BtnOk.Size = new System.Drawing.Size(94, 31);
            this.BtnOk.TabIndex = 8;
            this.BtnOk.Text = "ȷ ��";
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancle
            // 
            this.BtnCancle._Image = null;
            this.BtnCancle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.BtnCancle.DefaultScheme = true;
            this.BtnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancle.Image = null;
            this.BtnCancle.Location = new System.Drawing.Point(362, 239);
            this.BtnCancle.Name = "BtnCancle";
            this.BtnCancle.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
            this.BtnCancle.Size = new System.Drawing.Size(94, 31);
            this.BtnCancle.TabIndex = 9;
            this.BtnCancle.Text = "ȡ ��";
            this.BtnCancle.Click += new System.EventHandler(this.BtnCancle_Click);
            // 
            // chk_Simple
            // 
            this.chk_Simple.AutoSize = true;
            this.chk_Simple.Checked = true;
            this.chk_Simple.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Simple.Location = new System.Drawing.Point(306, 180);
            this.chk_Simple.Name = "chk_Simple";
            this.chk_Simple.Size = new System.Drawing.Size(119, 19);
            this.chk_Simple.TabIndex = 37;
            this.chk_Simple.Text = "��Ч����ģʽ";
            this.chk_Simple.UseVisualStyleBackColor = true;
            // 
            // chkboxConnectStr
            // 
            this.chkboxConnectStr.AutoSize = true;
            this.chkboxConnectStr.Location = new System.Drawing.Point(446, 180);
            this.chkboxConnectStr.Name = "chkboxConnectStr";
            this.chkboxConnectStr.Size = new System.Drawing.Size(149, 19);
            this.chkboxConnectStr.TabIndex = 38;
            this.chkboxConnectStr.Text = "�Զ��������ַ���";
            this.chkboxConnectStr.UseVisualStyleBackColor = true;
            this.chkboxConnectStr.CheckedChanged += new System.EventHandler(this.chkboxConnectStr_CheckedChanged);
            // 
            // txtConnectStr
            // 
            this.txtConnectStr.Location = new System.Drawing.Point(306, 208);
            this.txtConnectStr.Name = "txtConnectStr";
            this.txtConnectStr.Size = new System.Drawing.Size(333, 25);
            this.txtConnectStr.TabIndex = 39;
            this.txtConnectStr.Visible = false;
            // 
            // LoginOra
            // 
            this.AcceptButton = this.BtnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.ClientSize = new System.Drawing.Size(651, 323);
            this.Controls.Add(this.txtConnectStr);
            this.Controls.Add(this.chkboxConnectStr);
            this.Controls.Add(this.chk_Simple);
            this.Controls.Add(this.BtnCancle);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtServer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginOra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "��¼";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion




        #endregion


        private void FormLogin_Load(object sender, System.EventArgs e)
        {
            //try
            //{
            //    dbobj=Maticsoft.CmConfig.DbConfig.GetSetting("Oracle");
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

        private void BtnCancle_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void BtnOk_Click(object sender, System.EventArgs e)
        {
            try
            {
                string server = "";
                if (chkboxConnectStr.Checked)
                {
                    if (txtConnectStr.Text.Length == 0)
                    {
                        MessageBox.Show(this, "�����ַ�������Ϊ�գ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    constr = txtConnectStr.Text;
                }
                else
                {
                    string user = this.txtUser.Text.Trim();
                    string pass = this.txtPass.Text.Trim();
                    server = this.txtServer.Text.Trim();

                    if ((user.Trim() == "") || (server.Trim() == ""))
                    {
                        MessageBox.Show(this, "�û��������벻��Ϊ�գ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    constr = "Data Source=" + server + "; user id=" + user + ";password=" + pass;
                }
                if (constr.Length < 5)
                {
                    return;
                }

                //��������
                OracleConnection myCn = new OracleConnection(constr);
                try
                {
                    this.Text = "�������ӷ����������Ժ�...";
                    myCn.Open();
                }
                catch (Exception ex)
                {                   
                    this.Text = "���ӷ�����ʧ�ܣ�";
                    myCn.Close();
                    LogInfo.WriteLog(ex);
                    MessageBox.Show(this, "���ӷ�����ʧ�ܣ�" + ex.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                myCn.Close();
                this.Text = "���ӷ������ɹ���";


                if (dbobj == null)
                    dbobj = new Maticsoft.CmConfig.DbSettings();

                //����ǰ����д�������ļ�
                dbobj.DbType = "Oracle";
                dbobj.Server = txtServer.Text.Trim(); 
                dbobj.ConnectStr = constr;
                dbobj.DbName = "";
                dbobj.DbHelperName = "DbHelperOra";
                dbobj.ConnectSimple = chk_Simple.Checked;
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
                                
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(this, ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LogInfo.WriteLog(ex);
            }
        }

        private void chkboxConnectStr_CheckedChanged(object sender, EventArgs e)
        {
            if (chkboxConnectStr.Checked)
            {
                txtConnectStr.Visible = true;
                txtPass.Enabled = false;
                txtServer.Enabled = false;
                txtUser.Enabled = false;
            }
            else
            {
                txtConnectStr.Visible = false;
                txtPass.Enabled = true;
                txtServer.Enabled = true;
                txtUser.Enabled = true;
            }
        }


    }
}
