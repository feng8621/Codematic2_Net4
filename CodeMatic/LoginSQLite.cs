﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;
namespace Codematic
{
    public partial class LoginSQLite : Form
    {
        Maticsoft.CmConfig.DbSettings dbobj = new Maticsoft.CmConfig.DbSettings();

        public LoginSQLite()
        {
            InitializeComponent();
        }

        private void btn_SelDb_Click(object sender, EventArgs e)
        {
            OpenFileDialog sqlfiledlg = new OpenFileDialog();
            sqlfiledlg.Title = "选择数据库文件";
            sqlfiledlg.Filter = "SQLite files (*.sdb;*.s3db)|*.sdb;*.s3db|所有文件 (*.*)|*.*";
            DialogResult result = sqlfiledlg.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                this.txtServer.Text = sqlfiledlg.FileName;
                GetConstr();                
            }
        }

        #region 构建连接字符串
        private void GetConstr()
        {
            FileInfo file = new FileInfo(txtServer.Text);
            string ext = file.Extension;
            switch (ext.ToLower().Trim())
            {
                case ".sdb":
                    txtConstr.Text = @"Data Source=" + txtServer.Text ;
                    if (txtPass.Text.Trim() != "")
                    {
                        txtConstr.Text += ";Password=" + txtPass.Text.Trim();
                    }

                    break;
                case ".s3db":
                    txtConstr.Text = @"Data Source=" + txtServer.Text;
                    if (txtPass.Text.Trim() != "")
                    {
                        txtConstr.Text += ";Password=" + txtPass.Text.Trim();
                    }
                    break;
                default:
                    txtConstr.Text = @"Data Source=" + txtServer.Text;
                    if (txtPass.Text.Trim() != "")
                    {
                        txtConstr.Text += ";Password=" + txtPass.Text.Trim();
                    }
                    break;
            }
        }

        private void txtServer_TextChanged(object sender, System.EventArgs e)
        {
            GetConstr();
        }      
        private void txtPass_TextChanged(object sender, System.EventArgs e)
        {
            GetConstr();
        }
        #endregion

        #region  控件选择
        private void radBtn_DB_Click(object sender, System.EventArgs e)
        {
            this.radBtn_Constr.Checked = false;         
            this.txtServer.Enabled = true;
            this.txtPass.Enabled = true;          
            this.txtConstr.Enabled = false;
        }

        private void radBtn_Constr_Click(object sender, System.EventArgs e)
        {
            this.radBtn_DB.Checked = false;           
            this.txtServer.Enabled = false;
            this.txtPass.Enabled = false;           
            this.txtConstr.Enabled = true;
        }       

        #endregion


        private void btn_Ok_Click(object sender, EventArgs e)
        {
            try
            {
                string server = this.txtServer.Text.Trim();              
                string pass = this.txtPass.Text.Trim();
                if (this.radBtn_DB.Checked)
                {
                    GetConstr();
                    if (server == "")
                    {
                        MessageBox.Show(this, "数据库不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                if (this.radBtn_Constr.Checked)
                {
                    if (txtConstr.Text == "")
                    {
                        MessageBox.Show(this, "数据库不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                string constr = this.txtConstr.Text;

                //测试连接
                SQLiteConnection myCn = new SQLiteConnection(constr);
                try
                {
                    this.Text = "正在连接数据库，请稍候...";
                    myCn.Open();
                }
                catch (Exception ex)
                {
                    this.Text = "连接数据库失败！";
                    LogInfo.WriteLog(ex);
                    MessageBox.Show(this, "连接数据库失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                myCn.Close();
                this.Text = "连接数据库成功！";

                if (dbobj == null)
                    dbobj = new Maticsoft.CmConfig.DbSettings();

                //将当前配置写入配置文件
                dbobj.DbType = "SQLite";
                dbobj.Server = server;
                dbobj.ConnectStr = constr;
                dbobj.DbHelperName = "DbHelperSQLite";
                dbobj.DbName = "";
                int result = Maticsoft.CmConfig.DbConfig.AddSettings(dbobj);
                switch (result)
                {
                    case 0:
                        MessageBox.Show(this, "添加服务器配置失败，请检查是否有写入权限或文件是否存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    case 1:
                        break;
                    case 2:
                        {
                            DialogResult dr = MessageBox.Show(this, "该服务器信息已经存在！你确认是否覆盖当前数据库配置？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (dr == DialogResult.Yes)
                            {
                                Maticsoft.CmConfig.DbConfig.DelSetting(dbobj.DbType, dbobj.Server, dbobj.DbName);
                                result = Maticsoft.CmConfig.DbConfig.AddSettings(dbobj);
                                if (result != 1)
                                {
                                    MessageBox.Show(this, "建议卸载当前版本，并删除安装目录后重新安装最新版本！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(this, ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LogInfo.WriteLog(ex);
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
