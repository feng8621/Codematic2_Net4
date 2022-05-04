using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Codematic.UserControls;
namespace Codematic
{
    public partial class OptionFrm : Form
    {
        private UcOptionsEnviroments optionsEnviroments ;
        private UcOptionEditor optionEditor ;
        private UcOptionsQuerySettings optionsQuerySettings ;
        private UcOptionStartUp optionStartUp ;
        private UcLanguage optionLanguage;
        public UcCodeSet uccdset;        
        private MainForm mainForm;
        UcAddInManage ucAddin;        
        UcDatatypeMap ucDatatype;
        UcSysManage ucSysmanage;

        Maticsoft.CmConfig.AppSettings appsettings;
                

        public OptionFrm(MainForm mainform)
        {
            InitializeComponent();
            mainForm = mainform;
            appsettings = Maticsoft.CmConfig.AppConfig.GetSettings();
            
            optionsEnviroments = new UcOptionsEnviroments();
            optionEditor = new UcOptionEditor();
            optionsQuerySettings = new UcOptionsQuerySettings();
            optionStartUp = new UcOptionStartUp(appsettings);
            optionLanguage = new UcLanguage(appsettings);
            uccdset = new UcCodeSet();            
            ucAddin = new UcAddInManage();
            ucDatatype = new UcDatatypeMap();
            ucSysmanage = new UcSysManage();

        }
        private void OptionFrm_Load(object sender, EventArgs e)
        {
            InitTreeView();            
        }

        #region ��ʼ��ѡ��
        private void InitTreeView()
        {
            // Top
            TreeNode tnEnviroment = new TreeNode("����", 0, 1);
            tnEnviroment.Tag = "tnEnviroment";
            TreeNode tnCodeSet = new TreeNode("������������", 0, 1);
            tnCodeSet.Tag = "tnCodeSet";
            TreeNode tnAddIn = new TreeNode("�������", 0, 1);
            tnAddIn.Tag = "tnAddIn";
            TreeNode tnDbo = new TreeNode("ϵͳ����", 0, 1);
            tnDbo.Tag = "tnDbo";

            #region ����
            
            TreeNode StartPage = new TreeNode("����", 2, 3);
            StartPage.Tag = "StartPage";

            TreeNode LanguagePage = new TreeNode("����", 2, 3);
            LanguagePage.Tag = "LanguagePage";
            
            //tnEnviroment.Nodes.Add(StartPage);
            tnEnviroment.Nodes.Add(LanguagePage);
            //tnEnviroment.Nodes.Add(tnEditor);
            //tnEnviroment.Nodes.Add(tnQuerySettings);
            #endregion

            #region  �������
            TreeNode tnDB = new TreeNode("���ݿ�ű�", 2, 3);
            tnDB.Tag = "tnDB";
            TreeNode tnCS = new TreeNode("��������", 2, 3);
            tnCS.Tag = "tnCS";
            //TreeNode tnNameRule = new TreeNode("����������", 2, 3);
            //tnNameRule.Tag = "tnNameRule";
            TreeNode tnWeb = new TreeNode("Webҳ��", 2, 3);
            tnWeb.Tag = "tnWeb";
            TreeNode tnDatatype = new TreeNode("�ֶ�����ӳ��", 2, 3);
            tnDatatype.Tag = "tnDatatype";
            //tnCodeSet.Nodes.Add(tnDB);
            tnCodeSet.Nodes.Add(tnCS);
            //tnCodeSet.Nodes.Add(tnNameRule);
            //tnCodeSet.Nodes.Add(tnWeb);
            tnCodeSet.Nodes.Add(tnDatatype);
            #endregion

            #region  �������
            //TreeNode tnaddin = new TreeNode("DAL�������", 2, 3);
            //TreeNode tnaddinbll = new TreeNode("BLL�������", 2, 3);
            //TreeNode tnproc = new TreeNode("�洢���̴�����", 2, 3);            
            
            //tnAddIn.Nodes.Add(tnaddin);
            //tnAddIn.Nodes.Add(tnaddinbll);
            #endregion

            this.treeView1.Nodes.Add(tnEnviroment);
            this.treeView1.Nodes.Add(tnCodeSet);
            this.treeView1.Nodes.Add(tnAddIn);
            this.treeView1.Nodes.Add(tnDbo);
            tnEnviroment.Expand();
            tnCodeSet.Expand();

            this.UserControlContainer.Controls.Add(optionsEnviroments);//����
            this.UserControlContainer.Controls.Add(optionEditor);//�༭
            this.UserControlContainer.Controls.Add(optionsQuerySettings);//����
            this.UserControlContainer.Controls.Add(optionStartUp);//����
            this.UserControlContainer.Controls.Add(optionLanguage);//����
            this.UserControlContainer.Controls.Add(uccdset);
            //this.UserControlContainer.Controls.Add(uccsset);//�������ɻ�������
            //this.UserControlContainer.Controls.Add(ucnameset);//�������ɻ�������
            this.UserControlContainer.Controls.Add(ucAddin);//�������
            this.UserControlContainer.Controls.Add(ucDatatype);//�ֶ�����ӳ��
            this.UserControlContainer.Controls.Add(ucSysmanage);//ϵͳ����

            ActivateOptionControl(optionsEnviroments);

        }
        private void ActivateOptionControl(System.Windows.Forms.UserControl optionControl)
        {
            foreach (UserControl uc in this.UserControlContainer.Controls)
                uc.Hide();
            optionControl.Show();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = this.treeView1.SelectedNode;
            if (selectedNode != null)
            {
                switch (selectedNode.Tag.ToString())
                {
                    case "tnEnviroment":
                        ActivateOptionControl(optionsEnviroments);
                        break;
                    case "tnEditor":
                        ActivateOptionControl(optionEditor);
                        break;
                    case "tnQuerySettings":
                        ActivateOptionControl(optionsQuerySettings);
                        break;
                    case "StartPage":
                        ActivateOptionControl(optionStartUp);
                        break;
                    case "LanguagePage":
                        ActivateOptionControl(optionLanguage);
                        break;
                    //case "tnDB":
                    //    ActivateOptionControl(ucdbset);
                    //    break;
                    case "tnCodeSet":
                    case "tnCS":
                        ActivateOptionControl(uccdset);
                        break;
                    //case "tnNameRule":
                    //    ActivateOptionControl(ucnameset);
                    //    break;
                    case "tnDatatype":
                        ActivateOptionControl(ucDatatype);
                        break;
                    case "tnAddIn":                    
                        ActivateOptionControl(ucAddin);
                        break;
                    case "tnDbo":
                        ActivateOptionControl(ucSysmanage);
                        break;
                }
            }
        }
        #endregion


        #region ����
        private void btn_Ok_Click(object sender, EventArgs e)
        {
            #region ����
            //switch (optionStartUp.cmb_StartUpItem.SelectedIndex)
            //{
            //    case 0:
            //        appsettings.AppStart = "startuppage";
            //        appsettings.StartUpPage = optionStartUp.txtStartUpPage.Text;
            //        break;
            //    case 1:
            //        appsettings.AppStart = "blank";
            //        break;
            //    case 2:
            //        appsettings.AppStart = "homepage";
            //        appsettings.HomePage = optionStartUp.txtStartUpPage.Text;
            //        break;
            //}

            if (optionLanguage.radioButton2.Checked)
            {
                appsettings.TemplateFolder = optionStartUp.txtTempPath.Text;
            }
            else
            {
                appsettings.TemplateFolder = "Template";
            }
            appsettings.Language = optionLanguage.Language;
            Maticsoft.CmConfig.AppConfig.SaveSettings(appsettings);
            #endregion

            #region ������������
            Maticsoft.CmConfig.DbSettings dbset = uccdset.GetCurrentDbSetting();
            if (dbset != null)
            {
                if (uccdset.radbtn_Frame_One.Checked)
                {
                    dbset.AppFrame = "One";
                }
                if (uccdset.radbtn_Frame_S3.Checked)
                {
                    dbset.AppFrame = "S3";
                }
                if (uccdset.radbtn_Frame_F3.Checked)
                {
                    dbset.AppFrame = "F3";
                }

                dbset.DALType = uccdset.GetDALType();
                dbset.BLLType = uccdset.GetBLLType();
                dbset.WebType = uccdset.GetWebType();

                dbset.Namepace = uccdset.txtNamepace.Text.Trim();
                dbset.DbHelperName = uccdset.txtDbHelperName.Text.Trim();
                dbset.ProjectName = uccdset.txtProjectName.Text.Trim();
                dbset.ProcPrefix = uccdset.txtProcPrefix.Text.Trim();

                dbset.BLLPrefix = uccdset.txtBLL_Prefix.Text.Trim();
                dbset.BLLSuffix = uccdset.txtBLL_Suffix.Text.Trim();
                dbset.DALPrefix = uccdset.txtDAL_Prefix.Text.Trim();
                dbset.DALSuffix = uccdset.txtDAL_Suffix.Text.Trim();
                dbset.ModelPrefix = uccdset.txtModel_Prefix.Text.Trim();
                dbset.ModelSuffix = uccdset.txtModel_Suffix.Text.Trim();
                dbset.ReplacedOldStr = uccdset.txtOldStr.Text.Trim();
                dbset.ReplacedNewStr = uccdset.txtNewStr.Text.Trim();

                #region ����������
                if (uccdset.radbtn_Same.Checked)
                {
                    dbset.TabNameRule = "same";
                }
                if (uccdset.radbtn_Lower.Checked)
                {
                    dbset.TabNameRule = "lower";
                }
                if (uccdset.radbtn_Upper.Checked)
                {
                    dbset.TabNameRule = "upper";
                }
                if (uccdset.radbtn_firstUpper.Checked)
                {
                    dbset.TabNameRule = "firstupper";
                }
                #endregion
                Maticsoft.CmConfig.DbConfig.UpdateSettings(dbset);
            }

            #endregion

            #region �ֶ�����ӳ��


            //�����ֶ�ӳ�������ļ�
            ucDatatype.SaveData();
            ucSysmanage.SaveDBO();
            #endregion
      


            Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


    }
}