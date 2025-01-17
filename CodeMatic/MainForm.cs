using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using Crownwood.Magic.Common;
using Crownwood.Magic.Docking;
using Codematic.UpServer;
using System.Net;
using System.Collections.Specialized;
using Maticsoft.CodeHelper;
using Maticsoft.CodeBuild;
namespace Codematic
{
    public partial class MainForm : Form
    {
        Thread threadUpdate;        
        public Mutex mutex;
        public static Maticsoft.CmConfig.ModuleSettings setting = new Maticsoft.CmConfig.ModuleSettings();
        
        Maticsoft.CmConfig.AppSettings appsettings;
        string cmcfgfile = Application.StartupPath + @"\cmcfg.ini";
        Maticsoft.Utility.INIFile cfgfile;

        FrmSearch frmSearch;

        private object[] persistedSearchItems;
        delegate void SetStatusCallback(string text);
        delegate void AddNewTabPageCallback(Control control, string Title);


        #region 定义Docking Manager对象

        private DockingManager dockManager;//右侧
        //Content solutionExplorerContent;
        //Content classViewContent;

        private DockingManager DBdockManager;        
        Content DbViewContent;
        Content tempViewContent;

        #endregion

        //语言文件
        Hashtable Languagelist = Maticsoft.CodeHelper.Language.LoadFromCfg("SystemMenu.lan");
        public MainForm()
        {
            InitializeComponent();
            SetLanguage();
            mutex = new Mutex(false, "SINGLE_INSTANCE_MUTEX");
            if (!mutex.WaitOne(0, false))
            {
                mutex.Close();
                mutex = null;
            }
            string softname = "动软代码生成器";
            if (Languagelist["SoftName"]!=null)
            {
                softname = Languagelist["SoftName"].ToString();
            }            
            this.Text = softname+"  V" + Application.ProductVersion;
            webBrowser1.Url = new System.Uri("http://www.maticsoft.com/codematic/count.htm?v=" + Application.ProductVersion, System.UriKind.Absolute);



            #region  右侧视图浮动窗口    
            /*
            dockManager = new DockingManager(this, VisualStyle.IDE);

            //DockingManager的数据成员OuterControl，InnerControl
            //用来决定DockingManager所在的窗口上哪些区域不受到DockingManager停靠窗口的影响 
            //Docking Manager不会影响在OuterControl对象以后加入主窗口的对象的窗口区域 
            //Docking Manager也不会影响在InnerControl对象以前加入主窗口的对象的窗口区域 


            //对象OuterControl，Docking Manager不会关注该对象以后生成的对象的窗口区域
            //对象InnerControl，Docking Manager不会关注在该对象生成以前的对象的窗口区域
            dockManager.OuterControl = statusBar;
            dockManager.InnerControl = tabControlMain;

            //dockManager.
            tabControlMain.IDEPixelBorder = true;
            tabControlMain.IDEPixelArea = true;

            //生成Conten对象，该对象就是DockingManager管理的浮动窗口
            //设置浮动窗口的属性，title是窗口收缩以后的标题  //FullTitle是窗口显示时的标题

            //解决方案资源管理器
            solutionExplorerContent = new Content(dockManager);
            solutionExplorerContent.Control = new SolutionExplorer();
            Size solutionExplorerSize = solutionExplorerContent.Control.Size;
            solutionExplorerContent.Title = "解决方案资源管理器";
            solutionExplorerContent.FullTitle = "解决方案资源管理器";
            solutionExplorerContent.AutoHideSize = solutionExplorerSize;
            solutionExplorerContent.DisplaySize = solutionExplorerSize;
            solutionExplorerContent.ImageList = viewImgs;
            solutionExplorerContent.ImageIndex = 0;
            solutionExplorerContent.PropertyChanged += new Content.PropChangeHandler(PropChange);

            //类视图
            classViewContent = new Content(dockManager);
            classViewContent.Control = new ClassView();
            Size classViewSize = classViewContent.Control.Size;
            classViewContent.Title = "类视图";
            classViewContent.FullTitle = "类视图";
            classViewContent.AutoHideSize = classViewSize;
            classViewContent.DisplaySize = classViewSize;
            classViewContent.ImageList = viewImgs;
            classViewContent.ImageIndex = 1;

            //将浮动窗口和具体在浮动窗口中被包含的面板联系起来
            dockManager.Contents.Add(solutionExplorerContent);
            WindowContent wc = dockManager.AddContentWithState(solutionExplorerContent, State.DockRight);

            dockManager.Contents.Add(classViewContent);
            dockManager.AddContentToWindowContent(classViewContent, wc);

            //dockManager.AddContentWithState(classViewContent,State.DockRight);
            //dockManager.HideAllContents();
                        
            */
            #endregion

            #region 右侧模板
            dockManager = new DockingManager(this, VisualStyle.IDE);

            //DockingManager的数据成员OuterControl，InnerControl
            //用来决定DockingManager所在的窗口上哪些区域不受到DockingManager停靠窗口的影响 
            //Docking Manager不会影响在OuterControl对象以后加入主窗口的对象的窗口区域 
            //Docking Manager也不会影响在InnerControl对象以前加入主窗口的对象的窗口区域 


            //对象OuterControl，Docking Manager不会关注该对象以后生成的对象的窗口区域
            //对象InnerControl，Docking Manager不会关注在该对象生成以前的对象的窗口区域
            dockManager.OuterControl = statusBar;
            dockManager.InnerControl = tabControlMain;

            //dockManager.
            tabControlMain.IDEPixelBorder = true;
            tabControlMain.IDEPixelArea = true;

            //生成Conten对象，该对象就是DockingManager管理的浮动窗口
            //设置浮动窗口的属性，title是窗口收缩以后的标题  //FullTitle是窗口显示时的标题
                        
            //模版视图
            tempViewContent = new Content(dockManager);
            tempViewContent.Control = new TempView(this);
            Size tempViewSize = tempViewContent.Control.Size;
            string tempview = "模版管理";
            if (Languagelist["TempView"] != null)
            {
                tempview = Languagelist["TempView"].ToString();
            }
            tempViewContent.Title = tempview;
            tempViewContent.FullTitle = tempview;
            tempViewContent.AutoHideSize = tempViewSize;
            tempViewContent.DisplaySize = tempViewSize;
            tempViewContent.ImageList = leftViewImgs;
            tempViewContent.ImageIndex = 1;

           

            //将浮动窗口和具体在浮动窗口中被包含的面板联系起来
            dockManager.Contents.Add(tempViewContent);
            WindowContent wc = dockManager.AddContentWithState(tempViewContent, State.DockRight);

            
            #endregion


            #region 左侧视图

            DBdockManager = new DockingManager(this, VisualStyle.IDE);
            
            //定义对象OuterControl，Docking Manager不会关注该对象以后生成的对象的窗口区域
            //对象InnerControl，Docking Manager不会关注在该对象生成以前的对象的窗口区域
            DBdockManager.OuterControl = statusBar;
            DBdockManager.InnerControl = tabControlMain;


            //数据库视图
            DbViewContent = new Content(DBdockManager);
            DbViewContent.Control = new DbView(this);
            Size DbViewSize = DbViewContent.Control.Size;
            string dbview = "数据库视图";
            if (Languagelist["DBView"] != null)
            {
                dbview = Languagelist["DBView"].ToString();
            }
            DbViewContent.Title = dbview;
            DbViewContent.FullTitle = dbview;
            DbViewContent.AutoHideSize = DbViewSize;
            DbViewContent.DisplaySize = DbViewSize;
            DbViewContent.ImageList = leftViewImgs;
            DbViewContent.ImageIndex = 0;


            ////模版视图
            //tempViewContent = new Content(DBdockManager);
            //tempViewContent.Control = new TempView(this);
            //Size tempViewSize = tempViewContent.Control.Size;
            //string tempview = "模版管理";
            //if (Languagelist["TempView"] != null)
            //{
            //    tempview = Languagelist["TempView"].ToString();
            //}
            //tempViewContent.Title = tempview;
            //tempViewContent.FullTitle = tempview;
            //tempViewContent.AutoHideSize = tempViewSize;
            //tempViewContent.DisplaySize = tempViewSize;
            //tempViewContent.ImageList = leftViewImgs;
            //tempViewContent.ImageIndex = 1;

            //将浮动窗口和具体在浮动窗口中被包含的面板联系起来
            DBdockManager.Contents.Add(DbViewContent);
            WindowContent wcdb = DBdockManager.AddContentWithState(DbViewContent, State.DockLeft);


            //DBdockManager.Contents.Add(tempViewContent);
            //DBdockManager.AddContentToWindowContent(tempViewContent, wcdb);

            #endregion

            #region 起始页
            appsettings = Maticsoft.CmConfig.AppConfig.GetSettings();
            
            //启动起始页
            try
            {
                LoadStartPage();
            }
            catch (System.Exception ex)
            {
                LogInfo.WriteLog(ex);
            }
            

            //switch (appsettings.AppStart)
            //{
            //    case "startuppage"://显示起始页
            //        {
            //            #region //启动起始页
            //            try
            //            {                            
            //                LoadStartPage();
            //            }
            //            catch(System.Exception ex)
            //            {
            //                LogInfo.WriteLog(ex);                            
            //            }                        
            //            #endregion
            //        }
            //        break;
            //    case "blank"://显示空环境
            //        {
            //        }
            //        break;
            //    case "homepage": //打开主页
            //        {
            //            #region 
            //            string selstr = "首页";
            //            string link = "http://www.maticsoft.com";
            //            if (appsettings.HomePage != null && appsettings.HomePage != "")
            //            {
            //                link = appsettings.HomePage;
            //            }
            //            //起始页
            //            Crownwood.Magic.Controls.TabPage page = new Crownwood.Magic.Controls.TabPage();
            //            page.Title = selstr;
            //            page.Control = new IEView(this, link);
            //            tabControlMain.TabPages.Add(page);
            //            tabControlMain.SelectedTab = page;

            //            #endregion
            //        }
            //        break;
            //}


            #endregion

            this.tabControlMain.MouseUp += new MouseEventHandler(OnMouseUpTabPage);

            #region 启动升级程序

            if (!IsHasChecked())
            {
                try
                {
                    threadUpdate = new Thread(new ThreadStart(ProcUpdate));
                    threadUpdate.Start();
                    //ProcUpdate();
                }
                catch (System.Exception ex)
                {
                    LogInfo.WriteLog(ex);
                    MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            #endregion

            #region 发送安装信息
            //try
            //{
            //    bool issetup = appsettings.Setup;
            //    if (!issetup)
            //    {
            //        threadSetup = new Thread(new ThreadStart(SendSetup));
            //        threadSetup.Start();
            //    }
            //}
            //catch
            //{
            //}
            #endregion

            #region 装载插件

            #endregion

        }
        
        #region 设置多语言
        private void SetLanguage()
        {
            
            this.menuCommand1.Description = Languagelist["File"].ToString();
            this.menuCommand1.Text = Languagelist["File"].ToString() + " (&F)";             
            this.menuCommand2.Description = "MenuItem";
            this.menuCommand2.Text = Languagelist["Edit"].ToString() + " (&E)";            
            this.menuCommand3.Description = "MenuItem";
            this.menuCommand3.Text = Languagelist["Project"].ToString() + " (&P)";
            this.toolStripMenuItem1.Text = Languagelist["File"].ToString() + " (&F)";
            this.新建ToolStripMenuItem.Text = Languagelist["New"].ToString() + "(&N)";
            this.项目PToolStripMenuItem.Text = Languagelist["Project"].ToString() + " (&P)";
            this.文件FToolStripMenuItem.Text = Languagelist["File"].ToString() + " (&F)";
            this.数据库连接SToolStripMenuItem.Text = Languagelist["DBConnect"].ToString() + " (&S)";
            this.打开ToolStripMenuItem.Text = Languagelist["Open"].ToString() + " (&O)";
            this.关闭CToolStripMenuItem.Text = Languagelist["Close"].ToString() + " (&C)";
            this.保存为ToolStripMenuItem.Text = Languagelist["SaveAS"].ToString() + " (&S)...";
            this.退出.Text = Languagelist["Exit"].ToString() + " (&X)";
            this.toolStripMenuItem2.Text = Languagelist["Edit"].ToString() + "(&E)";
            this.恢复ZToolStripMenuItem.Text = Languagelist["Restore"].ToString() + " (&Z)";
            this.编辑ToolStripMenuItem.Text = Languagelist["Cut"].ToString() + " (&T)";
            this.复制ToolStripMenuItem.Text = Languagelist["Copy"].ToString() + " (&C)";
            this.粘贴ToolStripMenuItem.Text = Languagelist["Paste"].ToString() + " (&P)";
            this.恢复ToolStripMenuItem.Text = Languagelist["Delete"].ToString() + " (&D)";
            this.全选AToolStripMenuItem.Text = Languagelist["SelectAll"].ToString() + " (&A)";
            this.查找ToolStripMenuItem.Text = Languagelist["Find"].ToString() + " ...";
            this.查找下一个ToolStripMenuItem.Text = Languagelist["FindNext"].ToString() + "";
            this.替换ToolStripMenuItem.Text = Languagelist["Replace"].ToString() + "";
            this.转到行ToolStripMenuItem.Text = Languagelist["GotoLine"].ToString() + "...";
            this.视图VToolStripMenuItem.Text = Languagelist["View"].ToString() + "(&V)";
            this.服务器资源管理器SToolStripMenuItem.Text = Languagelist["ServerManager"].ToString() + "(&S)";
            this.模版管理器TToolStripMenuItem.Text = Languagelist["TempManager"].ToString() + "(&T)";
            this.解决方案ToolStripMenuItem.Text = Languagelist["SolutionManager"].ToString() + "(&P)";
            this.类视图ToolStripMenuItem.Text = Languagelist["ClassView"].ToString() + "(&C)";
            this.数据库摘要ToolStripMenuItem.Text = Languagelist["DBManager"].ToString();
            this.起始页GToolStripMenuItem.Text = Languagelist["StartPage"].ToString() + "(&G)";
            this.查询QToolStripMenuItem.Text = Languagelist["Query"].ToString() + " (&Q)";
            this.打开脚本ToolStripMenuItem.Text = Languagelist["OpenSql"].ToString() + "";
            this.保存脚本ToolStripMenuItem.Text = Languagelist["SaveSql"].ToString();
            this.运行当前查询ToolStripMenuItem.Text = Languagelist["RunSql"].ToString();
            this.停止查询ToolStripMenuItem.Text = Languagelist["StopSql"].ToString();



        }
        #endregion
        
        #region 发送安装信息
        void SendSetup()
        {
            try
            {
                WebClient wc = new WebClient();
                string url = "http://www.maticsoft.com/setup.aspx";

                NameValueCollection nvc = new NameValueCollection();
                nvc.Add("SoftName", "Codematic");
                nvc.Add("Version", Application.ProductVersion);
                //nvc.Add("OS", "1");
                //nvc.Add("Mac", "ee-ee-ff-ds");
                nvc.Add("SQLinfo", "ee-ee-ff-ds");
                byte[] databuffer = wc.UploadValues(url, "POST", nvc);
                string text = Encoding.Default.GetString(databuffer);
                wc.Dispose();
                appsettings.Setup = true;
                Maticsoft.CmConfig.AppConfig.SaveSettings(appsettings);
            }
            catch (System.Exception ex)
            {
                LogInfo.WriteLog(ex);
            }
        }
        #endregion

        #region 启动升级程序

        void ProcUpdate()
        {
            try
            {
                Codematic.UpServer.UpServer upser = new Codematic.UpServer.UpServer();
                decimal localVer = decimal.Parse(Application.ProductVersion);//decimal.Parse(UpdateConfig.GetSettings().Version);
                decimal newVer = 1;
                try
                {
                    newVer = decimal.Parse(upser.GetVersion());
                }
                catch (System.Exception ex)
                {                    
                    LogInfo.WriteLog("Check Version Server Error:" + ex.Message);
                }

                CheckMarker();
                if (localVer < newVer)
                {
                    string updatetip1 = "程序发现新版本 " + newVer + "，你想现在升级吗？";
                    if (Languagelist["UpdateTip1"] != null)
                    {
                        updatetip1 = Languagelist["UpdateTip1"].ToString();
                    }
                    string updatetip2 = "系统提示";
                    if (Languagelist["UpdateTip2"] != null)
                    {
                        updatetip2 = Languagelist["UpdateTip2"].ToString();
                    }
                    DialogResult dia = MessageBox.Show(this, updatetip1, updatetip2, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dia == DialogResult.Yes)
                    {
                        Process.Start(Application.StartupPath + @"\UpdateApp.exe");
                        Close();
                        Application.Exit();
                    }
                    else
                    {                        
                        Process.Start("IExplore.exe", "http://www.maticsoft.com/download.aspx");
                    }
                }
            }
            catch (System.Exception ex)
            {
                string err = ex.Message;
                LogInfo.WriteLog("Check Version Error:" + err);
            }
        }

        #endregion

        #region  引导起始页
        void LoadStartPage()
        {
            string RssPath = appsettings.StartUpPage;
            SetStatusText("正在加载起始页...");
            AddSinglePage(new StartPageForm(this, RssPath), Languagelist["StartPage"].ToString());
            SetStatusText(Languagelist["Finish"].ToString());
        }

        #endregion

        #region  tabControlMain窗口中央的多页面板

        /// <summary>
        /// 在TabControl的右键中加入菜单
        /// </summary>
        protected void OnMouseUpTabPage(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (this.tabControlMain.TabPages.Count > 0 && e.Button == MouseButtons.Right && this.tabControlMain.SelectedTab.Selected)
            {
                Crownwood.Magic.Menus.MenuControl muMenu = new Crownwood.Magic.Menus.MenuControl();
                Crownwood.Magic.Menus.MenuCommand menu1 = new Crownwood.Magic.Menus.MenuCommand(Languagelist["Save"].ToString()+"(&S)", new EventHandler(OnSaveSelected));
                Crownwood.Magic.Menus.MenuCommand menu2 = new Crownwood.Magic.Menus.MenuCommand(Languagelist["Close"].ToString() + "(&C)", new EventHandler(OnColseSelected));
                Crownwood.Magic.Menus.MenuCommand menu3 = new Crownwood.Magic.Menus.MenuCommand(Languagelist["CloseAll"].ToString() + "(&A)", new EventHandler(OnColseUnSelected));

                Crownwood.Magic.Menus.PopupMenu pm = new Crownwood.Magic.Menus.PopupMenu();
                switch (this.tabControlMain.SelectedTab.Control.Name)
                {
                    case "DbQuery":
                    case "CodeEditor":
                    case "HtmlEditor":                                           
                        {
                            pm.MenuCommands.AddRange(new Crownwood.Magic.Menus.MenuCommand[] { menu1, menu2, menu3 });
                        }
                        break;
                    default:
                        pm.MenuCommands.AddRange(new Crownwood.Magic.Menus.MenuCommand[] {  menu2, menu3 });
                        break;
                }
                

                pm.TrackPopup(this.tabControlMain.PointToScreen(new Point(e.X, e.Y)));
            }
            if (this.tabControlMain.TabPages.Count > 0 && e.Button == MouseButtons.Left && this.tabControlMain.SelectedTab.Selected)
            {
                #region 工具栏按钮切换
                toolBtn_SQLExe.Visible = false;
                toolBtn_Run.Visible = false;
                查询QToolStripMenuItem.Visible = false;

                switch (this.tabControlMain.SelectedTab.Control.Name)
                {
                    case "DbQuery":
                        {
                            toolBtn_SQLExe.Visible = true;
                            查询QToolStripMenuItem.Visible = true;
                        }
                        break;
                    case "DbBrowser":
                        {
                        }
                        break;
                    case "StartPageForm":
                        {
                        }
                        break;
                    case "CodeMaker":
                        {
                        }
                        break;
                    case "CodeTemplate":
                        {
                            toolBtn_Run.Visible = true;                            
                        }
                        break;
                    default:
                        break;
                }
                #endregion
            }
        }

        /// <summary>
        /// 关闭TabControl的已选择TabPage
        /// </summary>
        protected void OnColseSelected(object sender, EventArgs e)
        {
            if (tabControlMain.TabPages.Count > 0)
            {
                OnCloseTabPage(tabControlMain.SelectedTab);
                tabControlMain.TabPages.Remove(tabControlMain.SelectedTab);
                if (tabControlMain.TabPages.Count == 0)
                {
                    tabControlMain.Visible = false;
                }
            }
        }

        /// <summary>
        /// 保存TabControl的已选择TabPage
        /// </summary>
        protected void OnSaveSelected(object sender, EventArgs e)
        {
            //tabControlMain.SelectedTab.Controls; 
            switch (tabControlMain.SelectedTab.Control.Name)
            {
                case "DbQuery":
                    { 
                    }
                    break;
                case "CodeEditor":
                    {
                        CodeEditor ce = (CodeEditor)tabControlMain.SelectedTab.Control;
                        ce.Save();
                    }
                    break;
                case "HtmlEditor":
                    {                        
                    }
                    break;
                default:
                    
                    break;
            }
        }
        /// <summary>
        /// 关闭TabControl的未选择的所有TabPage
        /// </summary>
        protected void OnColseUnSelected(object sender, EventArgs e)
        {
            if (tabControlMain.TabPages.Count > 0)
            {
                ArrayList pagelist = new ArrayList();
                foreach (Crownwood.Magic.Controls.TabPage tabpage in tabControlMain.TabPages)
                {
                    if (tabpage != tabControlMain.SelectedTab)
                    {
                        pagelist.Add(tabpage);
                    }
                }
                foreach (Crownwood.Magic.Controls.TabPage tabpage in pagelist)
                {
                    tabControlMain.TabPages.Remove(tabpage);
                }
                if (tabControlMain.TabPages.Count == 0)
                {
                    tabControlMain.Visible = false;
                }
            }
        }

        //关闭某页时作的处理
        private void OnCloseTabPage(Crownwood.Magic.Controls.TabPage page)
        {
            switch (page.Control.Name)
            {
                case "DbQuery":
                    {
                        toolBtn_SQLExe.Visible = false;
                        查询QToolStripMenuItem.Visible = false;
                    }
                    break;
                case "DbBrowser":
                    {
                    }
                    break;
                case "StartPageForm":
                    {
                    }
                    break;
                case "CodeMaker":
                    {
                    }
                    break;
                case "CodeTemplate":
                    {
                        toolBtn_Run.Visible = false;                        
                    }
                    break;
                default:
                    break;
            }
            page.Control.Dispose();
        }

        public void PropChange(Content obj, Crownwood.Magic.Docking.Content.Property prop)
        {
            //MessageBox.Show(obj.Title + "  " + prop.ToString());
        }

        #endregion

        #region 菜单

        #region 文件
        private void 数据库连接SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DbView dbview = new DbView(this);
            dbview.backgroundWorkerReg.RunWorkerAsync();
            
        }
        private void 项目PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProject newpro = new NewProject();
            newpro.ShowDialog(this);

        }
        private void 文件FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFile newfile = new NewFile(this);
            newfile.ShowDialog(this);

            ////空白文件
            //if (tabControlMain.Visible == false)
            //{
            //    tabControlMain.Visible = true;
            //}
            //Crownwood.Magic.Controls.TabPage page = new Crownwood.Magic.Controls.TabPage();
            //page.Title = "Exam.cs";
            //page.Control = new CodeEditor();
            //tabControlMain.TabPages.Add(page);
            //tabControlMain.SelectedTab = page;

        }
        private void 保存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region sql查询
            if (ActiveDbQuery != null)
            {
                SaveFileDialog sqlsavedlg = new SaveFileDialog();
                sqlsavedlg.Title = Languagelist["SaveSQL"].ToString();
                sqlsavedlg.Filter = "sql files (*.sql)|*.sql|All files (*.*)|*.*";
                DialogResult dlgresult = sqlsavedlg.ShowDialog(this);
                if (dlgresult == DialogResult.OK)
                {
                    string filename = sqlsavedlg.FileName;
                    string text = ActiveDbQuery.txtContent.Text;

                    StreamWriter sw = new StreamWriter(filename, false, Encoding.Default);//,false);
                    sw.Write(text);
                    sw.Flush();//从缓冲区写入基础流（文件）
                    sw.Close();
                }
            }
            #endregion

            #region 代码生成器
            if (ActiveCodeMaker != null)
            {
                CodeMaker cm = ActiveCodeMaker;
                SaveFileDialog sqlsavedlg = new SaveFileDialog();
                sqlsavedlg.Title = Languagelist["SaveCode"].ToString();
                string text = "";
                if (cm.codeview.txtContent_CS.Visible)
                {                    
                    sqlsavedlg.Filter = "C# files (*.cs)|*.cs|All files (*.*)|*.*";
                    text = cm.codeview.txtContent_CS.Text;
                }
                if (cm.codeview.txtContent_SQL.Visible)
                {                 
                    sqlsavedlg.Filter = "SQL files (*.sql)|*.cs|All files (*.*)|*.*";
                    text = cm.codeview.txtContent_SQL.Text;
                }
                if (cm.codeview.txtContent_Web.Visible)
                {                 
                    sqlsavedlg.Filter = "Aspx files (*.aspx)|*.cs|All files (*.*)|*.*";
                    text = cm.codeview.txtContent_Web.Text;
                }                
                DialogResult dlgresult = sqlsavedlg.ShowDialog(this);
                if (dlgresult == DialogResult.OK)
                {
                    string filename = sqlsavedlg.FileName;
                    
                    StreamWriter sw = new StreamWriter(filename, false, Encoding.Default);//,false);
                    sw.Write(text);
                    sw.Flush();//从缓冲区写入基础流（文件）
                    sw.Close();
                }
            }
            #endregion

            #region 模板代码
            if (ActiveCodeTemplate != null)
            {
                CodeTemplate ct = ActiveCodeTemplate;
                SaveFileDialog sqlsavedlg = new SaveFileDialog();
                sqlsavedlg.Title = Languagelist["SaveCode"].ToString();
                string text = "";
                if (ct.codeview.txtContent_CS.Visible)
                {
                    sqlsavedlg.Filter = "C# files (*.cs)|*.cs|All files (*.*)|*.*";
                    text = ct.codeview.txtContent_CS.Text;
                }
                if (ct.codeview.txtContent_SQL.Visible)
                {
                    sqlsavedlg.Filter = "SQL files (*.sql)|*.cs|All files (*.*)|*.*";
                    text = ct.codeview.txtContent_SQL.Text;
                }
                if (ct.codeview.txtContent_Web.Visible)
                {
                    sqlsavedlg.Filter = "Aspx files (*.aspx)|*.cs|All files (*.*)|*.*";
                    text = ct.codeview.txtContent_Web.Text;
                }
                DialogResult dlgresult = sqlsavedlg.ShowDialog(this);
                if (dlgresult == DialogResult.OK)
                {
                    string filename = sqlsavedlg.FileName;
                    StreamWriter sw = new StreamWriter(filename, false, Encoding.Default);//,false);
                    sw.Write(text);
                    sw.Flush();//从缓冲区写入基础流（文件）
                    sw.Close();
                }
            }
            #endregion

            #region  代码浏览
            if (ActiveCodeEditor != null)
            {
                SaveFileDialog sqlsavedlg = new SaveFileDialog();
                sqlsavedlg.Title = Languagelist["SaveCode"].ToString();
                sqlsavedlg.Filter = "C# files (*.cs)|*.cs|All files (*.*)|*.*";
                DialogResult dlgresult = sqlsavedlg.ShowDialog(this);
                if (dlgresult == DialogResult.OK)
                {
                    string filename = sqlsavedlg.FileName;
                    string text = ActiveCodeEditor.txtContent.Text;

                    StreamWriter sw = new StreamWriter(filename, false, Encoding.Default);//,false);
                    sw.Write(text);
                    sw.Flush();//从缓冲区写入基础流（文件）
                    sw.Close();
                }
            }
            #endregion


        }

        private void 关闭CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControlMain.TabPages.Count > 0)
            {
                OnCloseTabPage(tabControlMain.SelectedTab);
                tabControlMain.TabPages.Remove(tabControlMain.SelectedTab);
                if (tabControlMain.TabPages.Count == 0)
                {
                    tabControlMain.Visible = false;
                }
            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region 编辑

        /// <summary>
        /// 当前焦点的SQL查询窗体
        /// </summary>
        private DbQuery ActiveDbQuery
        {
            get
            {
                foreach (Crownwood.Magic.Controls.TabPage page in tabControlMain.TabPages)
                {                    
                    if (page.Selected)
                    {
                        if (page.Control.Name == "DbQuery") 
                        {
                            foreach (Control ctr in page.Control.Controls)
                            {
                                if ((ctr.ProductName == "LTPTextEditor") && (ctr.Name == "txtContent"))
                                {
                                    return (DbQuery)page.Control;
                                }
                            }
                        }                        
                    }
                }
                return null;
            }
            set
            {
                ActiveDbQuery = value;
            }
        }
        /// <summary>
        /// 当前焦点的代码生成窗体
        /// </summary>
        private CodeMaker ActiveCodeMaker
        {
            get
            {
                foreach (Crownwood.Magic.Controls.TabPage page in tabControlMain.TabPages)
                {
                    if (page.Selected)
                    {
                        if (page.Control.Name == "CodeMaker")
                        {
                            foreach (Control ctr in page.Control.Controls)
                            {
                                if (ctr.Name == "tabControl1")
                                {
                                    return ((CodeMaker)page.Control);
                                }
                            }
                        }                       
                    }
                }
                return null;
            }
            //set
            //{
            //    ActiveCodeMaker = value;
            //}
        }
        /// <summary>
        /// 当前焦点的模板代码生成窗体
        /// </summary>
        private CodeTemplate ActiveCodeTemplate
        {
            get
            {
                foreach (Crownwood.Magic.Controls.TabPage page in tabControlMain.TabPages)
                {
                    if (page.Selected)
                    {
                        if (page.Control.Name == "CodeTemplate")
                        {
                            foreach (Control ctr in page.Control.Controls)
                            {
                                if (ctr.Name == "tabControl1")
                                {
                                    return ((CodeTemplate)page.Control);
                                }
                            }
                        }
                    }
                }
                return null;
            }
            //set
            //{
            //    ActiveCodeMaker = value;
            //}
        }

        
        /// <summary>
        /// 当前代码编辑器窗体
        /// </summary>
        private CodeEditor ActiveCodeEditor
        {
            get
            {
                foreach (Crownwood.Magic.Controls.TabPage page in tabControlMain.TabPages)
                {
                    if (page.Selected)
                    {
                        if (page.Control.Name == "CodeEditor")
                        {
                            foreach (Control ctr in page.Control.Controls)
                            {
                                if ((ctr.ProductName == "Maticsoft.TextEditor") && (ctr.Name == "txtContent"))
                                {
                                    return (CodeEditor)page.Control;
                                }
                            }
                        }
                    }
                }
                return null;
            }
            set
            {
                ActiveCodeEditor = value;
            }
        }

        private void 查找ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDbQuery != null)
            {
                frmSearch = new FrmSearch(ActiveDbQuery);
                frmSearch.Closing += new CancelEventHandler(frmSearch_Closing);
                frmSearch.SearchItems = persistedSearchItems;
                frmSearch.TopMost = true;
                frmSearch.Show();//Dialog(ActiveQueryForm);
                frmSearch.Focus();
            }
        }
        private void frmSearch_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                persistedSearchItems = frmSearch.SearchItems;
            }
            catch { return; }
        }

        private void 查找下一个ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveDbQuery.FindNext();
        }

        private void 替换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDbQuery != null)
            {
                FrmSearch frmSearch = new FrmSearch(ActiveDbQuery, true);
                frmSearch.Closing += new CancelEventHandler(frmSearch_Closing);
                frmSearch.SearchItems = persistedSearchItems;
                frmSearch.Show();
                frmSearch.Focus();
            }
        }

        private void 转到行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDbQuery != null)
            {
                ActiveDbQuery.GoToLine();
            }
        }

        private void 全选AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Crownwood.Magic.Controls.TabPage page in tabControlMain.TabPages)
            {                
                if (page.Selected)
                {
                    if (page.Control.Name == "DbQuery") 
                    {
                        foreach (Control ctr in page.Control.Controls)
                        {
                            if ((ctr.ProductName == "LTPTextEditor") && (ctr.Name == "txtContent"))
                            {
                                LTPTextEditor.Editor.TextEditorControlWrapper txtContent = (LTPTextEditor.Editor.TextEditorControlWrapper)ctr;
                                txtContent.Select(0, txtContent.Text.Length);
                            }                           
                        }
                    }
                    //if (page.Control.Name == "CodeMaker")
                    //{
                    //    foreach (Control ctr in page.Control.Controls)
                    //    {
                    //        if ((ctr.ProductName == "Maticsoft.TextEditor") && (ctr.Name == "txtContent"))
                    //        {
                    //            LTPTextEditor.Editor.TextEditorControlWrapper txtContent = (LTPTextEditor.Editor.TextEditorControlWrapper)ctr;
                    //            txtContent.Select(0, txtContent.Text.Length);
                    //        }
                    //    }
                    //}
                    //if (page.Control.Name == "CodeEditor")
                    //{
                    //    foreach (Control ctr in page.Control.Controls)
                    //    {
                    //        if ((ctr.ProductName == "Maticsoft.TextEditor") && (ctr.Name == "txtContent"))
                    //        {
                    //            Maticsoft.TextEditor.TextEditorControl txtContent = (Maticsoft.TextEditor.TextEditorControl)ctr;
                    //            txtContent.Select(0, txtContent.Text.Length);
                    //        }
                    //    }
                    //}
                }
            }
        }

        private void 恢复ZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDbQuery != null)
            {
                ActiveDbQuery.Undo();
            }
        }
        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDbQuery != null)
            {
                ActiveDbQuery.Paste();
            }
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDbQuery != null)
            {
                ActiveDbQuery.Copy();
            }
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDbQuery != null)
            {
                ActiveDbQuery.Cut();
            }

        }
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDbQuery != null)
            {
                ActiveDbQuery.Cut();
            }
        }

        #endregion

        #region 视图
        private void 服务器资源管理器SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string dbview = "数据库视图";
                if (Languagelist["DBView"] != null)
                {
                    dbview = Languagelist["DBView"].ToString();
                }
                Content content = DBdockManager.Contents[dbview];
                if (服务器资源管理器SToolStripMenuItem.Checked)
                {
                    DBdockManager.HideContent(content);
                    服务器资源管理器SToolStripMenuItem.Checked = false;
                }
                else
                {
                    DBdockManager.ShowContent(content);
                    服务器资源管理器SToolStripMenuItem.Checked = true;
                }
            }
            catch
            { }
        }
        private void 模版管理器TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string tempview = "模版管理";
                if (Languagelist["TempView"] != null)
                {
                    tempview = Languagelist["TempView"].ToString();
                }
                Content content = dockManager.Contents[tempview];
                if (模版管理器TToolStripMenuItem.Checked)
                {
                    dockManager.HideContent(content);
                    模版管理器TToolStripMenuItem.Checked = false;
                }
                else
                {
                    dockManager.ShowContent(content);
                    模版管理器TToolStripMenuItem.Checked = true;
                }
            }
            catch
            { }
        }

        private void 解决方案ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Content content = dockManager.Contents["解决方案资源管理器"];
                if (解决方案ToolStripMenuItem.Checked)
                {
                    //dockManager.HideContent(content);
                    解决方案ToolStripMenuItem.Checked = false;
                }
                else
                {
                    ////dockManager.ShowContent(content);
                    解决方案ToolStripMenuItem.Checked = true;
                }
            }
            catch { }
        }

        private void 类视图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Content content = dockManager.Contents["类视图"];
                if (类视图ToolStripMenuItem.Checked)
                {
                    dockManager.HideContent(content);
                    类视图ToolStripMenuItem.Checked = false;
                }
                else
                {
                    //dockManager.ShowContent(content);
                    类视图ToolStripMenuItem.Checked = true;
                }
            }
            catch
            { }
        }

        private void 数据库摘要ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSinglePage(new DbBrowser(), Languagelist["Summary"].ToString());
        }

        private void 起始页GToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AddSinglePage(new StartPageForm(this), "起始页");
            LoadStartPage();
        }
        #endregion

        #region 生成
        private void 生成数据脚本ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 生成存储过程ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 存储过程ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 数据脚本ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 表数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 对象定义ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region 窗口
        private void 窗口WToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void StatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void 显示结果窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void 重置窗口布局ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ////解决方案资源管理器            
            //DBdockManager.ShowContent(solutionExplorerContent);

            ////类视图            
            //DBdockManager.ShowContent(classViewContent);
                        
            //数据库视图            
            DBdockManager.ShowContent(DbViewContent);
            
            //模版管理视图            
            DBdockManager.ShowContent(tempViewContent);
        }
        private void 关闭所有文档LToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControlMain.TabPages.Count > 0)
            {
                ArrayList pagelist = new ArrayList();
                foreach (Crownwood.Magic.Controls.TabPage tabpage in tabControlMain.TabPages)
                {
                    pagelist.Add(tabpage);
                }
                foreach (Crownwood.Magic.Controls.TabPage tabpage in pagelist)
                {
                    tabControlMain.TabPages.Remove(tabpage);
                }
                if (tabControlMain.TabPages.Count == 0)
                {
                    tabControlMain.Visible = false;
                }
            }

        }
        #endregion

        #region 查询
        private void 打开脚本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDbQuery != null)
            {
                OpenFileDialog sqlfiledlg = new OpenFileDialog();
                sqlfiledlg.Title = "打开sql脚本文件";
                sqlfiledlg.Filter = "sql files (*.sql)|*.sql|All files (*.*)|*.*";
                DialogResult result = sqlfiledlg.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    string filename = sqlfiledlg.FileName;

                    StreamReader srFile = new StreamReader(filename, Encoding.Default);
                    string Contents = srFile.ReadToEnd();
                    srFile.Close();
                    ActiveDbQuery.txtContent.Text = Contents;

                    //ActiveDbQuery.txtContent.LoadFile(filename, true);
                }

            }
        }

        private void 保存脚本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDbQuery != null)
            {
                SaveFileDialog sqlsavedlg = new SaveFileDialog();
                sqlsavedlg.Title = "保存当前查询";
                sqlsavedlg.Filter = "sql files (*.sql)|*.sql|All files (*.*)|*.*";
                DialogResult dlgresult = sqlsavedlg.ShowDialog(this);
                if (dlgresult == DialogResult.OK)
                {
                    string filename = sqlsavedlg.FileName;
                    string text = ActiveDbQuery.txtContent.Text;

                    StreamWriter sw = new StreamWriter(filename, false, Encoding.Default);//,false);
                    sw.Write(text);
                    sw.Flush();//从缓冲区写入基础流（文件）
                    sw.Close();
                }
            }

        }

        private void 运行当前查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDbQuery != null)
            {
                ActiveDbQuery.RunCurrentQuery();
            }
        }

        private void 停止查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 验证当前查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDbQuery != null)
            {
                ActiveDbQuery.miValidateCurrentQuery_Click(sender, e);
            }

        }

        private void 脚本片断管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddToSnippet frm = new FrmAddToSnippet("");
            frm.ShowDialog(this);
        }

        private void 转到定义ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDbQuery != null)
            {
                ActiveDbQuery.GoToDefenition();
            }
        }

        private void 转到对象引用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDbQuery != null)
            {
                ActiveDbQuery.GoToReferenceObject();
            }
        }

        #endregion


        #region 工具
        private void 数据库管理器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSinglePage(new DbBrowser(), "摘要");
        }

        private void 查询分析器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSinglePage(new DbQuery(this, ""), "查询分析器");
            this.toolBtn_SQLExe.Visible = true;
        }
        //搜索表
        private void SearchTablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string longservername = FormCommon.GetDbViewSelServer();
            if (longservername == "")
            {
                MessageBox.Show("没有可用的数据库连接，请先连接数据库服务器。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SearchTables st = new SearchTables(this, longservername);            
            st.ShowDialog(this);
        }

        private void 代码生成器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string longservername = FormCommon.GetDbViewSelServer();
            if (longservername == "")
            {
                MessageBox.Show("没有可用的数据库连接，请先连接数据库服务器。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            AddSinglePage(new CodeMaker(), "代码生成");
        }
        private void dB脚本生成器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string longservername = FormCommon.GetDbViewSelServer();
            if (longservername == "")
            {
                MessageBox.Show("没有可用的数据库连接，请先连接数据库服务器。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DbToScript ce = new DbToScript(longservername);
            ce.ShowDialog(this);
        }
        private void 模版代码生成器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string longservername = FormCommon.GetDbViewSelServer();
            if (longservername == "")
            {
                MessageBox.Show("没有可用的数据库连接，请先连接数据库服务器。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            AddSinglePage(new CodeTemplate(this), "模版代码生成器");
        }

        private void 代码自动输出器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string longservername = FormCommon.GetDbViewSelServer();
            if (longservername == "")
            {
                MessageBox.Show("没有可用的数据库连接，请先连接数据库服务器。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CodeExport ce = new CodeExport(longservername);
            ce.ShowDialog(this);
        }

        private void 模板代码批量生成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string longservername = FormCommon.GetDbViewSelServer();
            if (longservername == "")
            {
                MessageBox.Show("没有可用的数据库连接，请先连接数据库服务器。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            TemplateBatch tb = new TemplateBatch(longservername,false);
            tb.ShowDialog(this);
        }



        private void 生成数据库文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string longservername = FormCommon.GetDbViewSelServer();
            if (longservername == "")
            {
                MessageBox.Show("没有可用的数据库连接，请先连接数据库服务器。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }           
            try
            {
                //DbToWord dbtoword = new DbToWord(longservername);
                //dbtoword.Show();

                MessageBox.Show("该功能尚未改造完成。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                DialogResult dr = MessageBox.Show("启动文档生成器失败，请检查是否安装了Office软件，并确保左侧连接了数据库。\r\n 但你可以选择生成网页格式的文档，你要生成吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    DbToWeb dbtoweb = new DbToWeb(longservername);
                    dbtoweb.Show();
                }
            }
        }

        private void c代码转换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConverteCS2VB csvb = new ConverteCS2VB();
            csvb.Show();
        }
        private void wEB项目发布ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectExp pro = new ProjectExp();
            pro.Show();
        }

        private void 选项OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionFrm of = new OptionFrm(this);
            of.Show();
        }
        #endregion

        #region 帮助

        private void 主题ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    Process proc = new Process();
            //    proc.StartInfo.FileName = "help.chm";
            //    proc.StartInfo.Arguments = "";
            //    proc.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            //    proc.Start();
            //}
            //catch
            //{
            //    MessageBox.Show("请访问：http://ltp.cnblogs.com", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            try
            {
                Process proc = new Process();
                Process.Start("IExplore.exe", "http://help.maticsoft.com");
            }
            catch
            {
                MessageBox.Show("请访问：http://www.maticsoft.com", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void 访问Maticsoft站点NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process proc = new Process();
                Process.Start("IExplore.exe", "http://www.maticsoft.com");
            }
            catch
            {
                MessageBox.Show("请访问：http://www.maticsoft.com", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void 我要反馈ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process proc = new Process();
                Process.Start("IExplore.exe", "http://www.maticsoft.com/bug.aspx");
            }
            catch
            {
                MessageBox.Show("请访问：http://www.maticsoft.com", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void 论坛交流ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process proc = new Process();
                Process.Start("IExplore.exe", "http://bbs.maticsoft.com");
            }
            catch
            {
                MessageBox.Show("请访问：http://bbs.maticsoft.com", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void 关于CodematicAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout fa = new FormAbout();
            fa.ShowDialog(this);
        }
        #endregion

        #endregion

        #region 公用方法

        public void SetStatusText(string text)
        {
            //if (this.StatusLabel1.InvokeRequired)
            //{
            //    SetStatusCallback d = new SetStatusCallback(SetStatusText);
            //    this.Invoke(d, new object[] { text });
            //}
            //else
            //{
            this.StatusLabel1.Text = text;

            //}
        }


        //得到当前数据库浏览器选中的服务器名称
        //private string GetDbViewSelServer()
        //{
        //    DbView dbviewfrm1 = (DbView)Application.OpenForms["DbView"];
        //    TreeNode SelNode = dbviewfrm1.treeView1.SelectedNode;
        //    if (SelNode == null)
        //        return "";
        //    string longservername = "";
        //    switch (SelNode.Tag.ToString())
        //    {
        //        case "serverlist":
        //            return "";
        //        case "server":
        //            {
        //                longservername = SelNode.Text;
        //            }
        //            break;
        //        case "db":
        //            {
        //                longservername = SelNode.Parent.Text;
        //            }
        //            break;
        //        case "tableroot":
        //        case "viewroot":
        //            {
        //                longservername = SelNode.Parent.Parent.Text;
        //            }
        //            break;
        //        case "table":
        //        case "view":
        //            {
        //                longservername = SelNode.Parent.Parent.Parent.Text;
        //            }
        //            break;
        //        case "column":
        //            longservername = SelNode.Parent.Parent.Parent.Parent.Text;
        //            break;
        //    }

        //    return longservername;
        //}

        /// <summary>
        /// 检查数据库服务器连接
        /// </summary>
        
        public void CheckDbServer()
        {
            string longservername = FormCommon.GetDbViewSelServer();
            if (longservername == "")
            {
                MessageBox.Show("没有可用的数据库连接，请先连接数据库服务器。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        //是否已经存在该窗体页
        private bool ExistPage(string CtrName)
        {
            bool Exist = false;
            if (tabControlMain.Visible == false)
            {
                tabControlMain.Visible = true;
            }
            foreach (Crownwood.Magic.Controls.TabPage page in tabControlMain.TabPages)
            {
                string str = page.Control.Name;
                if (page.Control.Name == CtrName)
                {
                    Exist = true;
                }
                //if (page.Title == "摘要")
                //{
                //    showed = true;
                //}
            }
            return Exist;
        }

        //创建新的窗体页
        //private void AddNewTabPage(Control control, string Title)
        //{
        //    Crownwood.Magic.Controls.TabPage page = new Crownwood.Magic.Controls.TabPage();
        //    page.Title = Title;
        //    page.Control = control;
        //    tabControlMain.TabPages.Add(page);
        //    tabControlMain.SelectedTab = page;
        //}

        public void AddNewTabPage(Control control, string Title)
        {
            if (this.tabControlMain.InvokeRequired)
            {
                AddNewTabPageCallback d = new AddNewTabPageCallback(AddNewTabPage);
                this.Invoke(d, new object[] { control, Title });
            }
            else
            {
                Crownwood.Magic.Controls.TabPage page = new Crownwood.Magic.Controls.TabPage();
                page.Title = Title;
                page.Control = control;
                tabControlMain.TabPages.Add(page);
                tabControlMain.SelectedTab = page;
            }
        }

        // 增加TabPage
        public void AddTabPage(string pageTitle, Control ctrForm)
        {
            if (tabControlMain.Visible == false)
            {
                tabControlMain.Visible = true;
            }
            Crownwood.Magic.Controls.TabPage page = new Crownwood.Magic.Controls.TabPage();
            page.Title = pageTitle;
            page.Control = ctrForm;
            tabControlMain.TabPages.Add(page);
            tabControlMain.SelectedTab = page;
        }


        // 创建新的唯一窗体页（不允许重复的）
        public void AddSinglePage(Control control, string Title)
        {
            if (tabControlMain.Visible == false)
            {
                tabControlMain.Visible = true;
            }
            bool showed = false;
            Crownwood.Magic.Controls.TabPage currPage = null;
            foreach (Crownwood.Magic.Controls.TabPage page in tabControlMain.TabPages)
            {
                if (page.Control.Name == control.Name)
                {
                    showed = true;
                    currPage = page;
                }
            }
            if (!showed)//不存在
            {
                AddNewTabPage(control, Title);
            }
            else
            {
                tabControlMain.SelectedTab = currPage;
            }
        }
        /// <summary>
        /// 是否已经检查过最新版本
        /// </summary>
        /// <returns></returns>
        private bool IsHasChecked()
        {
            if (File.Exists(cmcfgfile))
            {
                cfgfile = new Maticsoft.Utility.INIFile(cmcfgfile);

                string autoupdate = cfgfile.IniReadValue("update", "autoupdate");
                if (string.IsNullOrEmpty(autoupdate) || autoupdate == "0")
                {
                    return false;
                }

                string today = cfgfile.IniReadValue("update", "today");
                if (today == DateTime.Today.ToString("yyyyMMdd"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 标记今天已经做了版本检测
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="loginfo"></param>
        private void CheckMarker()
        {            
            cfgfile.IniWriteValue("update", "today", DateTime.Today.ToString("yyyyMMdd"));           
        }

        #endregion

        #region 工具栏

        //数据库浏览器
        private void toolBtn_DbView_Click(object sender, EventArgs e)
        {
            AddSinglePage(new DbBrowser(), "摘要");
        }
        //查询分析器
        private void toolBtn_SQL_Click(object sender, EventArgs e)
        {
            AddSinglePage(new DbQuery(this, ""), "查询分析器");
            this.toolBtn_SQLExe.Visible = true;
        }
        //代码生成器
        private void toolBtn_CreatCode_Click(object sender, EventArgs e)
        {
            string longservername = FormCommon.GetDbViewSelServer();
            if (longservername == "")
            {
                MessageBox.Show("没有可用的数据库连接，请先连接数据库服务器。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            AddSinglePage(new CodeMaker(), "代码生成");
        }
        //模版代码生成
        private void toolBtn_CreatTempCode_Click(object sender, EventArgs e)
        {
            string longservername = FormCommon.GetDbViewSelServer();
            if (longservername == "")
            {
                MessageBox.Show("没有可用的数据库连接，请先连接数据库服务器。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            AddSinglePage(new CodeTemplate(this), "模版代码生成器");

        }

        //自动输出代码
        private void toolBtn_OutCode_Click(object sender, EventArgs e)
        {
            string longservername = FormCommon.GetDbViewSelServer();
            if (longservername == "")
            {
                MessageBox.Show("没有可用的数据库连接，请先连接数据库服务器。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CodeExport ce = new CodeExport(longservername);
            ce.ShowDialog(this);

        }

        private void toolBtn_SQLExe_Click(object sender, EventArgs e)
        {
            if (tabControlMain.SelectedTab.Control.Name == "DbQuery")
            {
                DbQuery dqfrm = (DbQuery)tabControlMain.SelectedTab.Control;
                dqfrm.RunCurrentQuery();

            }
        }

        private void toolBtn_Run_Click(object sender, EventArgs e)
        {
            if (tabControlMain.SelectedTab.Control.Name == "CodeTemplate")
            {
                CodeTemplate ctfrm = (CodeTemplate)tabControlMain.SelectedTab.Control;
                ctfrm.Run();
            }
            if (ActiveCodeEditor != null)
            {
                #region
                //try
                //{
                //    string strContent = ActiveCodeEditor.txtContent.Text;
                //    string Templatefilename = ActiveCodeEditor.fileName;
                //    if (strContent.Trim() == "")
                //    {
                //        MessageBox.Show("模版内容为空，请先在模版管理器里选择模版！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        StatusLabel1.ForeColor = Color.Black;
                //        StatusLabel1.Text = "就绪";
                //        return;
                //    }
                //    if (Templatefilename == null || Templatefilename.Length == 0)
                //    {
                //        Templatefilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template\\TemplateFile\\temp~.cmt");
                //    }
                //    File.WriteAllText(Templatefilename, strContent, Encoding.UTF8);
                //}
                //catch (System.Exception ex)
                //{
                //    StatusLabel1.ForeColor = Color.Red;
                //    StatusLabel1.Text = "模版格式有误：" + ex.Message;
                //    return;
                //}

                //string strcode = "";
                //CodeInfo codeinfo = new CodeInfo();
                //try
                //{
                //    string longservername = FormCommon.GetDbViewSelServer();
                //     dbobj = ObjHelper.CreatDbObj(servername);
                //    BuilderTemp bt = new BuilderTemp(dbobj, dbname, tablename, tablename, GetFieldlist(), GetKeyFields(),
                //        Templatefilename, dbset);
                //    codeinfo = bt.GetCode();
                //    if (codeinfo.ErrorMsg != null && codeinfo.ErrorMsg.Length > 0)
                //    {
                //        strcode = codeinfo.Code + System.Environment.NewLine + "/*------ 代码生成时出现错误: ------" +
                //            System.Environment.NewLine + codeinfo.ErrorMsg + "*/";
                //    }
                //    else
                //    {
                //        strcode = codeinfo.Code;
                //    }
                //}
                //catch (System.Exception ex)
                //{
                //    StatusLabel1.ForeColor = Color.Red;
                //    StatusLabel1.Text = "代码转换失败！" + ex.Message;
                //    return;
                //}
                //SettxtContent(codeinfo.FileExtension.Replace(".", ""), strcode);
                //this.tabControl1.SelectedIndex = 1;
                //if (codeinfo.ErrorMsg != null && codeinfo.ErrorMsg.Length > 0)
                //{
                //    StatusLabel1.ForeColor = Color.Red;
                //    StatusLabel1.Text = "代码生成失败!";
                //}
                //else
                //{
                //    StatusLabel1.ForeColor = Color.Green;
                //    StatusLabel1.Text = "代码生成成功。";
                //}
#endregion
            }
        }


        //数据库文档
        private void toolBtn_Word_Click(object sender, EventArgs e)
        {
            string longservername = FormCommon.GetDbViewSelServer();
            if (longservername == "")
            {
                MessageBox.Show("没有可用的数据库连接，请先连接数据库服务器。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                //DbToWord dbtoword = new DbToWord(longservername);
                //dbtoword.Show();

                MessageBox.Show("该功能尚未改造完成。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                DialogResult dr = MessageBox.Show("启动文档生成器失败，请检查是否安装了Office软件，并确保左侧连接了数据库。\r\n 但你可以选择生成网页格式的文档，你要生成吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    DbToWeb dbtoweb = new DbToWeb(longservername);
                    dbtoweb.Show();
                }
            }

            

        }

        //web项目发布
        private void toolBtn_Web_Click(object sender, EventArgs e)
        {
            ProjectExp pro = new ProjectExp();
            pro.Show();
        }

        //退出
        private void toolBtn_Exit_Click(object sender, EventArgs e)
        {
            //this.notifyIcon1.Visible = false;
            Application.Exit();
            Environment.Exit(0);

        }

        // 数据库选择列表
        private void toolComboBox_DB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string longservername = FormCommon.GetDbViewSelServer();
            if (string.IsNullOrEmpty(longservername))
            {
                MessageBox.Show("没有可用的数据库连接，请先连接数据库服务器。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Maticsoft.IDBO.IDbObject dbobj = ObjHelper.CreatDbObj(longservername);
            string dbname = toolComboBox_DB.Text;
            DataTable dt = dbobj.GetTabViews(dbname);
            toolComboBox_Table.Items.Clear();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string tablename = row["name"].ToString();
                    this.toolComboBox_Table.Items.Add(tablename);
                }
                if (toolComboBox_Table.Items.Count > 0)
                {
                    this.toolComboBox_Table.SelectedIndex = 0;
                }
            }
            this.StatusLabel3.Text = "当前库:" + dbname;
        }


        //表
        private void toolComboBox_Table_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion

       
        

        

        

        




    }
}