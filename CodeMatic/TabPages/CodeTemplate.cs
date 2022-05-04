using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Collections;
using Maticsoft.CodeBuild;
using System.Threading;
using Maticsoft.CodeHelper;
using Maticsoft.Utility;
using Codematic.UserControls;
namespace Codematic
{
    /// <summary>
    /// ģ���������
    /// </summary>
    public partial class CodeTemplate : Form
    {
        MainForm mainfrm;
        Maticsoft.CmConfig.DbSettings dbset;
        Maticsoft.IDBO.IDbObject dbobj;
        string Templatefilename;//ģ���ļ���
        string servername;
        string dbname;
        string tablename;
        string objtype = "table";
        string tableDescription="";
        private Thread thread;        
        delegate void SetListCallback();
        public UcCodeView codeview;

        public CodeTemplate()
        {
            InitializeComponent();            
        }
        public CodeTemplate(Form mdiParentForm)
        {
            InitializeComponent();

            codeview = new UcCodeView();
            tabPage2.Controls.Add(codeview);

            mainfrm = (MainForm)mdiParentForm;            
            CreatView();
            mainfrm.toolBtn_Run.Visible = true;

            DbView dbviewfrm = (DbView)Application.OpenForms["DbView"];
            if (dbviewfrm != null)
            {
                SetListView(dbviewfrm);
                //try
                //{
                //    this.thread = new Thread(new ThreadStart(Showlistview));
                //    this.thread.Start();
                //}
                //catch
                //{
                //}
            }
        }


        #region ����listview

        void Showlistview()
        {
            DbView dbviewfrm = (DbView)Application.OpenForms["DbView"];
            //SetListView(dbviewfrm);
            if (dbviewfrm.treeView1.InvokeRequired)
            {
                SetListCallback d = new SetListCallback(Showlistview);
                dbviewfrm.Invoke(d, null);
            }
            else
            {
                SetListView(dbviewfrm);
            }
        }
        public void SetListView(DbView dbviewfrm)
        {
            #region �õ����Ͷ���

            TreeNode SelNode = dbviewfrm.treeView1.SelectedNode;
            if (SelNode == null)
                return;
            objtype = SelNode.Tag.ToString();
            switch (SelNode.Tag.ToString())
            {
                case "table":
                case "view":                
                    {
                        servername = SelNode.Parent.Parent.Parent.Text;
                        dbname = SelNode.Parent.Parent.Text;
                        tablename = SelNode.Text;
                        dbobj = ObjHelper.CreatDbObj(servername);
                        BindlistViewCol(dbname, tablename);
                    }
                    break;
                case "column":
                    {
                        servername = SelNode.Parent.Parent.Parent.Parent.Text;
                        dbname = SelNode.Parent.Parent.Parent.Text;
                        tablename = SelNode.Parent.Text;
                        dbobj = ObjHelper.CreatDbObj(servername);
                        BindlistViewCol(dbname, tablename);

                    }
                    break;
                case "proc":
                    {
                        servername = SelNode.Parent.Parent.Parent.Text;
                        dbname = SelNode.Parent.Parent.Text;
                        tablename = SelNode.Text;
                        dbobj = ObjHelper.CreatDbObj(servername);
                        BindlistProcCol(dbname, tablename);
                    }
                    break;
                default:
                    {
                        this.listView1.Items.Clear();
                    }
                    break;
            }
            GetTableDesc();
            #endregion
            if (servername!=null&&servername.Length > 1)
            {
                dbset = Maticsoft.CmConfig.DbConfig.GetSetting(servername);
            }
        }

        #endregion

        #region  ������
        public void GetTableDesc()
        {
            if (dbobj != null)
            {
                DataTable dtEx = dbobj.GetTablesExProperty(dbname);
                if (dtEx != null)
                {
                    try
                    {
                        tableDescription = tablename;
                        DataRow[] drs = dtEx.Select("objname='" + tablename + "'");
                        if (drs.Length > 0)
                        {
                            if (drs[0]["value"] != null)
                            {
                                tableDescription = drs[0]["value"].ToString();
                            }
                        }
                    }
                    catch
                    { }
                }
            }
        }

        #endregion

        #region ����ģ���ı���
        public void SettxtTemplate(string Filename)
        {
            try
            {
                Templatefilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Filename);//
                StreamReader srFile = new StreamReader(Templatefilename, Encoding.Default);
                string strContents = srFile.ReadToEnd();
                srFile.Close();
                txtTemplate.Text = strContents;
                groupBox1.Text = Templatefilename;
                
            }
            catch(Exception ex)
            {
                txtTemplate.Text = "����ģ��ʧ��:"+ex.Message;
            }
        }
        #endregion

        #region  ΪlistView� �� ����

        private void CreatView()
        {
            //�����б�
            this.listView1.Columns.Clear();
            this.listView1.Items.Clear();
            this.listView1.LargeImageList = imglistView;
            this.listView1.SmallImageList = imglistView;
            this.listView1.View = View.Details;
            this.listView1.GridLines = true;
            this.listView1.CheckBoxes = true;
            this.listView1.FullRowSelect = true;

            listView1.Columns.Add("���", 60, HorizontalAlignment.Center);
            listView1.Columns.Add("����", 110, HorizontalAlignment.Left);
            listView1.Columns.Add("��������", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("����", 40, HorizontalAlignment.Left);
            listView1.Columns.Add("С��", 40, HorizontalAlignment.Left);
            listView1.Columns.Add("��ʶ", 40, HorizontalAlignment.Center);
            listView1.Columns.Add("����", 40, HorizontalAlignment.Center);
            listView1.Columns.Add("�����", 60, HorizontalAlignment.Center);
            listView1.Columns.Add("Ĭ��ֵ", 100, HorizontalAlignment.Left);
            //listView1.Columns.Add("�ֶ�˵��", 100, HorizontalAlignment.Left);
        }

        private void BindlistViewCol(string Dbname, string TableName)
        {
            List<ColumnInfo> collist = dbobj.GetColumnInfoList(Dbname, TableName);
            if ((collist!=null)&&(collist.Count > 0))
            {
                listView1.Items.Clear();
                list_KeyField.Items.Clear();
                foreach (ColumnInfo col in collist)
                {
                    string order = col.ColumnOrder;
                    string columnName = col.ColumnName;
                    string columnType = col.TypeName;        
                    string Length = col.Length;
                    string Preci = col.Precision;
                    string Scale = col.Scale;        
                    string defaultVal = col.DefaultVal;
                    string description = col.Description;
                    string IsIdentity = (col.IsIdentity) ? "��" : "";
                    string ispk = (col.IsPrimaryKey) ? "��" : "";
                    string isnull = (col.Nullable) ? "��" : "";

                    ListViewItem item1 = new ListViewItem(order, 0);
                    item1.ImageIndex = 4;
                    item1.SubItems.Add(columnName);
                    item1.SubItems.Add(columnType);
                    item1.SubItems.Add(Length);
                    item1.SubItems.Add(Scale);
                    item1.SubItems.Add(IsIdentity);
                    if ((ispk == "��") && (isnull.Trim() == ""))//���������ǿ�
                    {
                        this.list_KeyField.Items.Add(columnName);
                        //this.list_KeyField.Items.Add(columnName + "(" + columnType + ")");
                    }
                    else
                    {
                        ispk = "";
                    }
                    item1.SubItems.Add(ispk);
                    item1.SubItems.Add(isnull);
                    item1.SubItems.Add(defaultVal);

                    listView1.Items.AddRange(new ListViewItem[] { item1 });

                }
            }
            btn_SelAll_Click(null, null);            
        }

        private void BindlistProcCol(string Dbname, string TableName)
        {
            List<ColumnInfo> collist = dbobj.GetColumnList(Dbname, TableName);
            if ((collist != null) && (collist.Count > 0))
            {
                listView1.Items.Clear();
                list_KeyField.Items.Clear();
                foreach (ColumnInfo col in collist)
                {
                    string order = col.ColumnOrder;
                    string columnName = col.ColumnName;
                    string columnType = col.TypeName;
                    string Length = col.Length;
                    string Preci = col.Precision;
                    string Scale = col.Scale;
                    string defaultVal = col.DefaultVal;
                    string description = col.Description;
                    string IsIdentity = (col.IsIdentity) ? "��" : "";
                    string ispk = (col.IsPrimaryKey) ? "��" : "";
                    string isnull = (col.Nullable) ? "��" : "";

                    ListViewItem item1 = new ListViewItem(order, 0);
                    item1.ImageIndex = 4;
                    item1.SubItems.Add(columnName);
                    item1.SubItems.Add(columnType);
                    item1.SubItems.Add(Length);
                    item1.SubItems.Add(Scale);
                    item1.SubItems.Add(IsIdentity);
                    if ((ispk == "��") && (isnull.Trim() == ""))//���������ǿ�
                    {
                        this.list_KeyField.Items.Add(columnName);
                        //this.list_KeyField.Items.Add(columnName + "(" + columnType + ")");
                    }
                    else
                    {
                        ispk = "";
                    }
                    item1.SubItems.Add(ispk);
                    item1.SubItems.Add(isnull);
                    item1.SubItems.Add(description);

                    listView1.Items.AddRange(new ListViewItem[] { item1 });

                }
            }
            btn_SelAll_Click(null, null);
        }
        #endregion

        #region ѡ���ֶΰ�ť
        private void btn_SelAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                if (!item.Checked)
                {
                    item.Checked = true;
                }
            }
        }

        private void btn_SelI_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                item.Checked = !item.Checked;
            }
        }               
        #endregion

        #region  ��������

        //�趨������listbox
        private void btn_SetKey_Click(object sender, EventArgs e)
        {
            this.list_KeyField.Items.Clear();
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Checked)
                {
                    this.list_KeyField.Items.Add(item.SubItems[1].Text);
                    //this.list_KeyField.Items.Add(item.SubItems[1].Text + "(" + item.SubItems[2].Text + ")");
                }
            }

        }
        /// <summary>
        /// ���ô���ؼ�����
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="strContent"></param>
        private void SettxtContent(string Type, string strContent)
        {
            codeview.SettxtContent(Type, strContent);
            this.tabControl1.SelectedIndex = 1;

        }

        /// <summary>
        /// �õ������Ķ�����Ϣ
        /// </summary>
        /// <returns></returns>
        private List<ColumnInfo> GetKeyFields()
        {           
            List<ColumnInfo> collist = dbobj.GetColumnInfoList(dbname, tablename);
            DataTable dt = Maticsoft.CodeHelper.CodeCommon.GetColumnInfoDt(collist);
            StringPlus Fields = new StringPlus();
            foreach (object obj in list_KeyField.Items)
            {
                Fields.Append("'" + obj.ToString() + "',");
            }
            Fields.DelLastComma();
            if (dt != null)
            {
                DataRow[] dtrows;
                if (Fields.Value.Length > 0)
                {
                    dtrows = dt.Select("ColumnName in (" + Fields.Value + ")", "colorder asc");
                }
                else
                {
                    dtrows = dt.Select();
                }
                List<ColumnInfo> keys = new List<ColumnInfo>();
                ColumnInfo key;
                foreach (DataRow row in dtrows)
                {
                    string Colorder = row["Colorder"].ToString();
                    string ColumnName = row["ColumnName"].ToString();
                    string TypeName = row["TypeName"].ToString();
                    string isIdentity = row["IsIdentity"].ToString();
                    string IsPK = row["IsPK"].ToString();
                    string Length = row["Length"].ToString();
                    string Preci = row["Preci"].ToString();
                    string Scale = row["Scale"].ToString();
                    string cisNull = row["cisNull"].ToString();
                    string DefaultVal = row["DefaultVal"].ToString();
                    string DeText = row["DeText"].ToString();

                    key = new ColumnInfo();
                    key.ColumnOrder = Colorder;
                    key.ColumnName = ColumnName;
                    key.TypeName = TypeName;
                    key.IsIdentity = (isIdentity == "��") ? true : false;
                    key.IsPrimaryKey = (IsPK == "��") ? true : false;
                    key.Length = Length;
                    key.Precision = Preci;
                    key.Scale = Scale;
                    key.Nullable = (cisNull == "��") ? true : false;
                    key.DefaultVal = DefaultVal;
                    key.Description = DeText;
                    keys.Add(key);

                }
                return keys;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// �õ�����Ķ�����Ϣ
        /// </summary>
        /// <returns></returns>
        private List<ColumnInfo> GetFKeyFields()
        {
            List<ColumnInfo> collist = dbobj.GetFKeyList(dbname, tablename);
            return collist;
        }


        /// <summary>
        /// �õ�ѡ����ֶμ���
        /// </summary>
        /// <returns></returns>
        private List<ColumnInfo> GetFieldlist()
        {
            List<ColumnInfo> collist ;
            if (objtype == "proc")
            {
                collist = dbobj.GetColumnList(dbname, tablename);
            }
            else
            {
                collist = dbobj.GetColumnInfoList(dbname, tablename);
            }
            DataTable dt = Maticsoft.CodeHelper.CodeCommon.GetColumnInfoDt(collist);
            StringPlus Fields = new StringPlus();
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Checked)
                {
                    Fields.Append("'" + item.SubItems[1].Text + "',");
                }
            }
            Fields.DelLastComma();
            if (dt != null)
            {
                DataRow[] dtrows;
                if (Fields.Value.Length > 0)
                {
                    dtrows = dt.Select("ColumnName in (" + Fields.Value + ")", "colorder asc");
                }
                else
                {
                    dtrows = dt.Select();
                }
                List<ColumnInfo> keys = new List<ColumnInfo>();
                ColumnInfo key;
                foreach (DataRow row in dtrows)
                {
                    string Colorder = row["Colorder"].ToString();
                    string ColumnName = row["ColumnName"].ToString();
                    string TypeName = row["TypeName"].ToString();
                    string isIdentity = row["IsIdentity"].ToString();
                    string IsPK = row["IsPK"].ToString();
                    string Length = row["Length"].ToString();
                    string Preci = row["Preci"].ToString();
                    string Scale = row["Scale"].ToString();
                    string cisNull = row["cisNull"].ToString();
                    string DefaultVal = row["DefaultVal"].ToString();
                    string DeText = row["DeText"].ToString();

                    key = new ColumnInfo();
                    key.ColumnOrder = Colorder;
                    key.ColumnName = ColumnName;
                    key.TypeName = TypeName;
                    key.IsIdentity = (isIdentity == "��") ? true : false;
                    key.IsPrimaryKey = (IsPK == "��") ? true : false;
                    key.Length = Length;
                    key.Precision = Preci;
                    key.Scale = Scale;
                    key.Nullable = (cisNull == "��") ? true : false;
                    key.DefaultVal = DefaultVal;
                    key.Description = DeText;
                    keys.Add(key);

                }
                return keys;
            }
            else
            {
                return null;
            }
        }


        #endregion

        #region ���ɴ���

        private void btn_Run_Click(object sender, EventArgs e)
        {
            try
            {
                Run();
                //threadCode = new Thread(new ThreadStart(Run));
                //threadCode.Start();
            }
            catch
            {
            }
        }

        public void Run()
        {
            StatusLabel1.Text = "��������...";            
            try
            {
                string strContent = txtTemplate.Text;
                if (strContent.Trim() == "")
                {
                    MessageBox.Show("ģ������Ϊ�գ�������ģ���������ѡ��ģ�棡", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    StatusLabel1.ForeColor = Color.Black;
                    StatusLabel1.Text = "����";
                    return;
                }
                if (Templatefilename == null || Templatefilename.Length == 0)
                {
                    Templatefilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template\\TemplateFile\\temp~.cmt");
                }
                File.WriteAllText(Templatefilename, strContent, Encoding.UTF8);
            }
            catch (System.Exception ex)
            {
                StatusLabel1.ForeColor = Color.Red;
                StatusLabel1.Text = "ģ���ʽ����" + ex.Message;
                return;
            }

            string strcode = "";
            CodeInfo codeinfo = new CodeInfo();
            try
            {
                BuilderTemp bt = new BuilderTemp(dbobj, dbname, tablename, tableDescription, GetFieldlist(), GetKeyFields(),GetFKeyFields(),
                    Templatefilename, dbset, objtype);
                codeinfo = bt.GetCode();
                if (codeinfo.ErrorMsg != null && codeinfo.ErrorMsg.Length > 0)
                {
                    strcode = codeinfo.Code + System.Environment.NewLine + "/*------ ��������ʱ���ִ���: ------" +
                        System.Environment.NewLine + codeinfo.ErrorMsg + "*/";
                }
                else
                {
                    strcode = codeinfo.Code;
                }
            }
            catch (System.Exception ex)
            {
                StatusLabel1.ForeColor = Color.Red;
                StatusLabel1.Text = "����ת��ʧ�ܣ�" + ex.Message;
                return;
            }
            SettxtContent(codeinfo.FileExtension.Replace(".",""), strcode);
            this.tabControl1.SelectedIndex = 1;
            if (codeinfo.ErrorMsg!=null&&codeinfo.ErrorMsg.Length > 0)
            {
                StatusLabel1.ForeColor = Color.Red;
                StatusLabel1.Text = "��������ʧ��!";
            }
            else
            {
                StatusLabel1.ForeColor = Color.Green;
                StatusLabel1.Text = "�������ɳɹ���";
            }
        }

        #endregion

        private void txtTemplate_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                string s = sender.ToString();
            }
        }

        private void btnHidelist_Click(object sender, EventArgs e)
        {
            if (listView1.Visible)
            {
                listView1.Visible = false;
                btnHidelist.Text = "��ʾ���";
            }
            else
            {
                listView1.Visible = true;
                btnHidelist.Text = "���ر��";
            }
        }

        #region Menu
        private void menu_Copy_Click(object sender, EventArgs e)
        {
            string text = txtTemplate.ActiveTextAreaControl.SelectionManager.SelectedText;            
            Clipboard.SetDataObject(text);

        }
        private void menu_Save_Click(object sender, EventArgs e)
        {
            try
            {
                string strContent = txtTemplate.Text;
                File.WriteAllText(Templatefilename, strContent, Encoding.UTF8);
                MessageBox.Show("����ɹ�!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("����ʧ��!"+ex.Message);
            }
        }
        private void menu_SaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sqlsavedlg = new SaveFileDialog();
                sqlsavedlg.Title = "ģ�����Ϊ";
                string strContent = txtTemplate.Text;
                DialogResult dlgresult = sqlsavedlg.ShowDialog(this);
                if (dlgresult == DialogResult.OK)
                {
                    string filename = sqlsavedlg.FileName;
                    //StreamWriter sw = new StreamWriter(filename, false, Encoding.Default);//,false);
                    //sw.Write(text);
                    //sw.Flush();//�ӻ�����д����������ļ���
                    //sw.Close();
                    File.WriteAllText(filename, strContent, Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("����ʧ��!" + ex.Message);
            }

        }
        #endregion


    }
}