using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Data;
using System.IO;
using System.Windows.Forms;
namespace Maticsoft.CmConfig
{
	#region ����Դ��¼��Ϣ
	/// <summary>
	/// DbInfo ��ժҪ˵����
	/// </summary>
    [Serializable]
	public class DbSettings
	{
		public DbSettings()
		{}
                
		#region 

		private string _dbtype;
		private string _server;
        private string _connectstr;        
		private string _dbname;
        private bool _connectSimple=true;
        private int _tabloadtype=0;
        private string _tabloadkeyword;

        private string _procprefix = "";//�洢����ǰ׺
        private string _projectname = "";//��Ŀ����
        private string _namespace = "Maticsoft";
        private string _folder = "";
        private string _appframe = "s3";//������(s3)����������(f3)���Զ���(custom)
        private string _daltype = "";//ֱ��дsql���(sql)�����ǵ��ô洢����(Proc);
        private string _blltype = "";//ֱ��дsql���(sql)�����ǵ��ô洢����(Proc);
        private string _webtype = "";
        private string _editfont = "������";
        private float _editfontsize = 9;
        private string _dbHelperName = "DbHelperSQL";
        private string modelPrefix = "";
        private string modelSuffix = "";
        private string bllPrefix = "";
        private string bllSuffix = "";
        private string dalPrefix = "";
        private string dalSuffix = "";
        private string tabnameRule = "same";
        private string _webTemplatepath = "";
        private string _replaceoldstr="";
        private string _replacenewstr="";

        


		/// <summary>
		/// ����Դ���� 
		/// </summary>
		[XmlElement]
		public string DbType
		{
			set{ _dbtype=value; }
			get{ return _dbtype; }
		}

		/// <summary>
		/// ������
		/// </summary>
		[XmlElement]
		public string Server
		{
			set{ _server=value; }
			get{ return _server; }
		}
        /// <summary>
        /// �����ַ���
        /// </summary>
        [XmlElement]
        public string ConnectStr
        {
            set { _connectstr = value; }
            get { return _connectstr; }
        }
       
		/// <summary>
        /// ���ݿ�//����һ���Ƿ񵥿��ֶΡ�
		/// </summary>
		[XmlElement]
		public string DbName
		{
            set { _dbname = value; }
            get { return _dbname; }
		}

        /// <summary>
        /// �������ģʽ��ֻ�г����������ֶ���Ϣ��
        /// </summary>
        [XmlElement]
        public bool ConnectSimple
        {
            set { _connectSimple = value; }
            get { return _connectSimple; }
        }
       
        
        /// <summary>
        /// ���������
        /// </summary>
        [XmlElement]
        public int TabLoadtype
        {
            set { _tabloadtype = value; }
            get { return _tabloadtype; }
        }

        /// <summary>
        /// ����˹ؼ���
        /// </summary>
        [XmlElement]
        public string TabLoadKeyword
        {
            set { _tabloadkeyword = value; }
            get { return _tabloadkeyword; }
        }

        #region
        /// <summary>
        /// �洢����ǰ׺ 
        /// </summary>
        [XmlElement]
        public string ProcPrefix
        {
            set { _procprefix = value; }
            get { return _procprefix; }
        }
        /// <summary>
        /// ��Ŀ���� 
        /// </summary>
        [XmlElement]
        public string ProjectName
        {
            set { _projectname = value; }
            get { return _projectname; }
        }
        /// <summary>
        /// Ĭ�϶��������ռ���
        /// </summary>
        [XmlElement]
        public string Namepace
        {
            set { _namespace = value; }
            get { return _namespace; }
        }
        /// <summary>
        /// Ĭ��ҵ���߼����ڵ��ļ���
        /// </summary>
        [XmlElement]
        public string Folder
        {
            set { _folder = value; }
            get { return _folder; }
        }
        /// <summary>
        /// ���õļܹ�
        /// </summary>
        [XmlElement]
        public string AppFrame
        {
            set { _appframe = value; }
            get { return _appframe; }
        }
        /// <summary>
        /// ���ݲ����� 
        /// </summary>
        [XmlElement]
        public string DALType
        {
            set { _daltype = value; }
            get { return _daltype; }
        }
        /// <summary>
        /// ҵ������� 
        /// </summary>
        [XmlElement]
        public string BLLType
        {
            set { _blltype = value; }
            get { return _blltype; }
        }
        /// <summary>
        /// ��ʾ������ 
        /// </summary>
        [XmlElement]
        public string WebType
        {
            set { _webtype = value; }
            get { return _webtype; }
        }

        /// <summary>
        /// ��ǰ�༭��ʹ�õ�������
        /// </summary>
        [XmlElement]
        public string EditFont
        {
            set { _editfont = value; }
            get { return _editfont; }
        }
        /// <summary>
        /// ��ǰ�༭��ʹ�õ�����Ĵ�С
        /// </summary>
        [XmlElement]
        public float EditFontSize
        {
            set { _editfontsize = value; }
            get { return _editfontsize; }
        }
        /// <summary>
        /// ���ݷ������� 
        /// </summary>
        [XmlElement]
        public string DbHelperName
        {
            set { _dbHelperName = value; }
            get { return _dbHelperName; }
        }


        /// <summary>
        /// Model����ǰ׺ 
        /// </summary>
        [XmlElement]
        public string ModelPrefix
        {
            set { modelPrefix = value; }
            get { return modelPrefix; }
        }
        /// <summary>
        /// Model������׺ 
        /// </summary>
        [XmlElement]
        public string ModelSuffix
        {
            set { modelSuffix = value; }
            get { return modelSuffix; }
        }
        /// <summary>
        /// BLL����ǰ׺ 
        /// </summary>
        [XmlElement]
        public string BLLPrefix
        {
            set { bllPrefix = value; }
            get { return bllPrefix; }
        }
        /// <summary>
        /// BLL������׺ 
        /// </summary>
        [XmlElement]
        public string BLLSuffix
        {
            set { bllSuffix = value; }
            get { return bllSuffix; }
        }

        /// <summary>
        /// DAL����ǰ׺ 
        /// </summary>
        [XmlElement]
        public string DALPrefix
        {
            set { dalPrefix = value; }
            get { return dalPrefix; }
        }
        /// <summary>
        /// DAL������׺ 
        /// </summary>
        [XmlElement]
        public string DALSuffix
        {
            set { dalSuffix = value; }
            get { return dalSuffix; }
        }
        /// <summary>
        /// ������Сд����: same(����ԭ��)  lower��ȫ��Сд��  upper��ȫ����д��
        /// </summary>
        [XmlElement]
        public string TabNameRule
        {
            set { tabnameRule = value; }
            get { return tabnameRule; }
        }

        /// <summary>
        /// web ҳ��ģ���·����������Ĭ�ϵ�ǰ·���µ�Ĭ��ģ��
        /// </summary>
        [XmlElement]
        public string WebTemplatePath
        {
            set { _webTemplatepath = value; }
            get { return _webTemplatepath; }
        }

        /// <summary>
        /// Ҫ�滻�������ַ�
        /// </summary>
        [XmlElement]
        public string ReplacedOldStr
        {
            set { _replaceoldstr = value; }
            get { return _replaceoldstr; }
        }

        /// <summary>
        /// �滻��ı����ַ�
        /// </summary>
        [XmlElement]
        public string ReplacedNewStr
        {
            set { _replacenewstr = value; }
            get { return _replacenewstr; }
        }
        #endregion


		#endregion

	}
	#endregion 


	public class DbConfig
	{        
        static string fileName = Application.StartupPath + "\\DbSetting.config";

		#region �õ� ���е�½��������Դ����
		/// <summary>
		/// �õ����е�½��������Դ����
		/// </summary>
		/// <returns></returns>
		public static DbSettings[] GetSettings()
		{				
			try
			{				
				DataSet ds=new DataSet();
                ArrayList DbList = new ArrayList();
				if(File.Exists(fileName))
				{
                    DbSettings dbset;
					ds.ReadXml(fileName);//XmlReadMode.ReadSchema
					if(ds.Tables.Count>0)
					{
						foreach(DataRow dr in ds.Tables[0].Rows)
						{
                            dbset = TranDbSettings(ds, dr);
                            DbList.Add(dbset);
						}						
					}					
				}
                DbSettings[] dbList = (DbSettings[])DbList.ToArray(typeof(DbSettings));
                return dbList;			
			}
			catch
			{
				return null;
			}
            
		
		}

        public static DbSettings TranDbSettings(DataSet ds, DataRow dr)
        {
            DbSettings dbset = new DbSettings();
            dbset.DbType = dr["DbType"].ToString();
            dbset.Server = dr["Server"].ToString();
            dbset.ConnectStr = dr["ConnectStr"].ToString();
            dbset.DbName = dr["DbName"].ToString();
            if (ds.Tables[0].Columns.Contains("ConnectSimple"))
            {
                if ((dr["ConnectSimple"] != null) && (dr["ConnectSimple"].ToString().Length > 0))
                {
                    dbset.ConnectSimple = bool.Parse(dr["ConnectSimple"].ToString());
                }
            }
            if (ds.Tables[0].Columns.Contains("TabLoadtype"))
            {
                if ((dr["TabLoadtype"] != null) && (dr["TabLoadtype"].ToString().Length > 0))
                {
                    dbset.TabLoadtype = int.Parse(dr["TabLoadtype"].ToString());
                }
            }
            if (ds.Tables[0].Columns.Contains("TabLoadKeyword"))
            {
                if ((dr["TabLoadKeyword"] != null) && (dr["TabLoadKeyword"].ToString().Length > 0))
                {
                    dbset.TabLoadKeyword = dr["TabLoadKeyword"].ToString();
                }
            }

            if (ds.Tables[0].Columns.Contains("ProcPrefix"))
            {
                if ((dr["ProcPrefix"] != null) )
                {
                    dbset.ProcPrefix = dr["ProcPrefix"].ToString();
                }
            }
            if (ds.Tables[0].Columns.Contains("ProjectName"))
            {
                if ((dr["ProjectName"] != null) )
                {
                    dbset.ProjectName = dr["ProjectName"].ToString();
                }
            }
            if (ds.Tables[0].Columns.Contains("Namepace"))
            {
                if ((dr["Namepace"] != null) && (dr["Namepace"].ToString().Length > 0))
                {
                    dbset.Namepace = dr["Namepace"].ToString();
                }
            }
            if (ds.Tables[0].Columns.Contains("Folder"))
            {
                if ((dr["Folder"] != null) )
                {
                    dbset.Folder = dr["Folder"].ToString();
                }
            }
            if (ds.Tables[0].Columns.Contains("AppFrame"))
            {
                if ((dr["AppFrame"] != null) && (dr["AppFrame"].ToString().Length > 0))
                {
                    dbset.AppFrame = dr["AppFrame"].ToString();
                }
            }

            if (ds.Tables[0].Columns.Contains("DALType"))
            {
                if ((dr["DALType"] != null) && (dr["DALType"].ToString().Length > 0))
                {
                    dbset.DALType = dr["DALType"].ToString();
                }
            }
            if (ds.Tables[0].Columns.Contains("BLLType"))
            {
                if ((dr["BLLType"] != null) && (dr["BLLType"].ToString().Length > 0))
                {
                    dbset.BLLType = dr["BLLType"].ToString();
                }
            }
            if (ds.Tables[0].Columns.Contains("WebType"))
            {
                if ((dr["WebType"] != null) && (dr["WebType"].ToString().Length > 0))
                {
                    dbset.WebType = dr["WebType"].ToString();
                }
            }
            if (ds.Tables[0].Columns.Contains("EditFont"))
            {
                if ((dr["EditFont"] != null) && (dr["EditFont"].ToString().Length > 0))
                {
                    dbset.EditFont = dr["EditFont"].ToString();
                }
            }
            if (ds.Tables[0].Columns.Contains("EditFontSize"))
            {
                if ((dr["EditFontSize"] != null) && (dr["EditFontSize"].ToString().Length > 0))
                {
                    dbset.EditFontSize = float.Parse(dr["EditFontSize"].ToString());
                }
            }
            if (ds.Tables[0].Columns.Contains("DbHelperName"))
            {
                if ((dr["DbHelperName"] != null) && (dr["DbHelperName"].ToString().Length > 0))
                {
                    dbset.DbHelperName = dr["DbHelperName"].ToString();
                }
            }
            if (ds.Tables[0].Columns.Contains("ModelPrefix"))
            {
                if ((dr["ModelPrefix"] != null) )
                {
                    dbset.ModelPrefix = dr["ModelPrefix"].ToString();
                }
            }

            if (ds.Tables[0].Columns.Contains("ModelSuffix"))
            {
                if ((dr["ModelSuffix"] != null) )
                {
                    dbset.ModelSuffix = dr["ModelSuffix"].ToString();
                }
            }
            if (ds.Tables[0].Columns.Contains("BLLPrefix"))
            {
                if ((dr["BLLPrefix"] != null) )
                {
                    dbset.BLLPrefix = dr["BLLPrefix"].ToString();
                }
            }
            if (ds.Tables[0].Columns.Contains("BLLSuffix"))
            {
                if ((dr["BLLSuffix"] != null) )
                {
                    dbset.BLLSuffix = dr["BLLSuffix"].ToString();
                }
            }
            if (ds.Tables[0].Columns.Contains("DALPrefix"))
            {
                if ((dr["DALPrefix"] != null) )
                {
                    dbset.DALPrefix = dr["DALPrefix"].ToString();
                }
            }
            if (ds.Tables[0].Columns.Contains("DALSuffix"))
            {
                if ((dr["DALSuffix"] != null))
                {
                    dbset.DALSuffix = dr["DALSuffix"].ToString();
                }
            }
            if (ds.Tables[0].Columns.Contains("TabNameRule"))
            {
                if ((dr["TabNameRule"] != null) && (dr["TabNameRule"].ToString().Length > 0))
                {
                    dbset.TabNameRule = dr["TabNameRule"].ToString();
                }
            }
            if (ds.Tables[0].Columns.Contains("WebTemplatePath"))
            {
                if ((dr["WebTemplatePath"] != null) && (dr["WebTemplatePath"].ToString().Length > 0))
                {
                    dbset.WebTemplatePath = dr["WebTemplatePath"].ToString();
                }
            }
            if (ds.Tables[0].Columns.Contains("ReplacedOldStr"))
            {
                if ((dr["ReplacedOldStr"] != null) && (dr["ReplacedOldStr"].ToString().Length > 0))
                {
                    dbset.ReplacedOldStr = dr["ReplacedOldStr"].ToString();
                }
            }
            if (ds.Tables[0].Columns.Contains("ReplacedNewStr"))
            {
                if ((dr["ReplacedNewStr"] != null) && (dr["ReplacedNewStr"].ToString().Length > 0))
                {
                    dbset.ReplacedNewStr = dr["ReplacedNewStr"].ToString();
                }
            }
            return dbset;
        }

        /// <summary>
        /// �õ���ǰ���ݿ������������Ϣ
        /// </summary>
        /// <returns></returns>
        public static DataSet GetSettingDs()
        {
            try
            {
                DataSet ds = new DataSet();               
                if (File.Exists(fileName))
                {                   
                    ds.ReadXml(fileName);//,XmlReadMode.ReadSchema                    
                }
                return ds;
            }
            catch
            {
                return null;
            }
        }
		#endregion

        #region ���� ���������� �õ�����������
        /// <summary>
        /// ���ݳ����������õ�����������
        /// </summary>
        /// <param name="servername"></param>
        public static DbSettings GetSetting(string loneServername)
        {
            //127.0.0.1(dbtype)(dbname)  
            //(local)(dbtype)(dbname) 
            
            string dbtype = "SQL2008";
            string server = "";
            string dbname = "";
            if (loneServername.StartsWith("(local)"))
            {
                int s = 7;
                server = "(local)";
                int e = loneServername.IndexOf(")", s);
                dbtype = loneServername.Substring(s + 1, e - s - 1);
                if (loneServername.Length > e + 1)
                {
                    dbname = loneServername.Substring(e + 2).Replace(")", "");
                }
            }
            else
            {
                int s = loneServername.IndexOf("(");
                server = loneServername.Substring(0, s);
                int e = loneServername.IndexOf(")", s);
                dbtype = loneServername.Substring(s + 1, e - s - 1);
                
                if (loneServername.Length > e + 1)
                {
                    dbname = loneServername.Substring(e + 2).Replace(")", "");
                }
            }
            return GetSetting(dbtype, server, dbname);            
        }
        #endregion

        #region �õ� ָ������Դ �� IP �ķ�����������Ϣ
        /// <summary>
		/// �õ�ָ������Դ��������Ϣ
		/// </summary>
		/// <returns></returns>
        public static DbSettings GetSetting(string DbType, string Serverip,string DbName)
		{				
			try
			{
                DbSettings dbset = null;
				DataSet ds=new DataSet();	
				if(File.Exists(fileName))
				{					
					ds.ReadXml(fileName);//,XmlReadMode.ReadSchema
					if(ds.Tables.Count>0)
					{
                        string strwhere = "DbType='" + DbType + "' and Server='" + Serverip + "'";
                        if (DbName.Trim() != "")
                        {
                            strwhere += " and DbName='" + DbName + "'";
                        }
                        DataRow[] drs = ds.Tables[0].Select(strwhere);
						if(drs.Length>0)
						{
                            DataRow dr = drs[0];                            
                            dbset=TranDbSettings(ds, dr);
                           
						}						
					}					
				}
                return dbset;				
			}
			catch
			{
				return null;
			}			
		}
		#endregion

		#region ���浱ǰ����Դ����

		public static DataTable CreateDataTable()
		{			
			DataTable table=new DataTable("DBServer");
			DataColumn col;
			
			col=new DataColumn();
			col.DataType=Type.GetType("System.String");
			col.ColumnName="DbType";
			table.Columns.Add(col);

			col=new DataColumn();
			col.DataType=Type.GetType("System.String");
			col.ColumnName="Server";
			table.Columns.Add(col);

			col=new DataColumn();
			col.DataType=Type.GetType("System.String");
            col.ColumnName = "ConnectStr";
			table.Columns.Add(col); 

			col=new DataColumn();
			col.DataType=Type.GetType("System.String");
            col.ColumnName = "DbName";
			table.Columns.Add(col);
            
            col = new DataColumn();
            col.DataType = Type.GetType("System.Boolean");
            col.ColumnName = "ConnectSimple";
            table.Columns.Add(col);

            col = new DataColumn();
            col.DataType = Type.GetType("System.Int32");
            col.ColumnName = "TabLoadtype";
            table.Columns.Add(col);

            
            col = new DataColumn();
            col.DataType = Type.GetType("System.String");
            col.ColumnName = "TabLoadKeyword";
            table.Columns.Add(col);

            col = new DataColumn();
            col.DataType = Type.GetType("System.String");
            col.ColumnName = "ProcPrefix";
            table.Columns.Add(col);

            col = new DataColumn();
            col.DataType = Type.GetType("System.String");
            col.ColumnName = "ProjectName";
            table.Columns.Add(col);

            col = new DataColumn();
            col.DataType = Type.GetType("System.String");
            col.ColumnName = "Namepace";
            table.Columns.Add(col);

            col = new DataColumn();
            col.DataType = Type.GetType("System.String");
            col.ColumnName = "Folder";
            table.Columns.Add(col);

            col = new DataColumn();
            col.DataType = Type.GetType("System.String");
            col.ColumnName = "AppFrame";
            table.Columns.Add(col);

            col = new DataColumn();
            col.DataType = Type.GetType("System.String");
            col.ColumnName = "DALType";
            table.Columns.Add(col);

            col = new DataColumn();
            col.DataType = Type.GetType("System.String");
            col.ColumnName = "BLLType";
            table.Columns.Add(col);

            col = new DataColumn();
            col.DataType = Type.GetType("System.String");
            col.ColumnName = "WebType";
            table.Columns.Add(col);

            col = new DataColumn();
            col.DataType = Type.GetType("System.String");
            col.ColumnName = "EditFont";
            table.Columns.Add(col);

            
            col = new DataColumn();
            col.DataType = Type.GetType("System.Double");
            col.ColumnName = "EditFontSize";
            table.Columns.Add(col);

            col = new DataColumn();
            col.DataType = Type.GetType("System.String");
            col.ColumnName = "DbHelperName";
            table.Columns.Add(col);

            col = new DataColumn();
            col.DataType = Type.GetType("System.String");
            col.ColumnName = "ModelPrefix";
            table.Columns.Add(col);

            col = new DataColumn();
            col.DataType = Type.GetType("System.String");
            col.ColumnName = "ModelSuffix";
            table.Columns.Add(col);

            col = new DataColumn();
            col.DataType = Type.GetType("System.String");
            col.ColumnName = "BLLPrefix";
            table.Columns.Add(col);

            col = new DataColumn();
            col.DataType = Type.GetType("System.String");
            col.ColumnName = "BLLSuffix";
            table.Columns.Add(col);

            col = new DataColumn();
            col.DataType = Type.GetType("System.String");
            col.ColumnName = "DALPrefix";
            table.Columns.Add(col);

            col = new DataColumn();
            col.DataType = Type.GetType("System.String");
            col.ColumnName = "DALSuffix";
            table.Columns.Add(col);
            
            col = new DataColumn();
            col.DataType = Type.GetType("System.String");
            col.ColumnName = "TabNameRule";
            table.Columns.Add(col);

            col = new DataColumn();
            col.DataType = Type.GetType("System.String");
            col.ColumnName = "WebTemplatePath";
            table.Columns.Add(col);

            col = new DataColumn();
            col.DataType = Type.GetType("System.String");
            col.ColumnName = "ReplacedOldStr";
            table.Columns.Add(col);

            col = new DataColumn();
            col.DataType = Type.GetType("System.String");
            col.ColumnName = "ReplacedNewStr";
            table.Columns.Add(col);
			return table;
		}

        public static void AddColForTable(DataTable table)
        {
            DataColumn col;
            if (!table.Columns.Contains("DbType"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "DbType";
                table.Columns.Add(col);
            }

            if (!table.Columns.Contains("Server"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "Server";
                table.Columns.Add(col);
            }
            if (!table.Columns.Contains("ConnectStr"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "ConnectStr";
                table.Columns.Add(col);
            }

            if (!table.Columns.Contains("DbName"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "DbName";
                table.Columns.Add(col);
            }

            if (!table.Columns.Contains("ConnectSimple"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.Boolean");
                col.ColumnName = "ConnectSimple";
                table.Columns.Add(col);
            }

            if (!table.Columns.Contains("TabLoadtype"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.Int32");
                col.ColumnName = "TabLoadtype";
                table.Columns.Add(col);
            }


            if (!table.Columns.Contains("TabLoadKeyword"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "TabLoadKeyword";
                table.Columns.Add(col);
            }

            if (!table.Columns.Contains("ProcPrefix"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "ProcPrefix";
                table.Columns.Add(col);
            }
            if (!table.Columns.Contains("ProjectName"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "ProjectName";
                table.Columns.Add(col);
            }

            if (!table.Columns.Contains("Namepace"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "Namepace";
                table.Columns.Add(col);
            }

            if (!table.Columns.Contains("Folder"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "Folder";
                table.Columns.Add(col);
            }

            if (!table.Columns.Contains("AppFrame"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "AppFrame";
                table.Columns.Add(col);
            }
            if (!table.Columns.Contains("DALType"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "DALType";
                table.Columns.Add(col);
            }
            if (!table.Columns.Contains("BLLType"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "BLLType";
                table.Columns.Add(col);
            }
            if (!table.Columns.Contains("WebType"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "WebType";
                table.Columns.Add(col);
            }
            if (!table.Columns.Contains("EditFont"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "EditFont";
                table.Columns.Add(col);
            }
            if (!table.Columns.Contains("EditFontSize"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.Double");
                col.ColumnName = "EditFontSize";
                table.Columns.Add(col);
            }
            if (!table.Columns.Contains("DbHelperName"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "DbHelperName";
                table.Columns.Add(col);
            }
            if (!table.Columns.Contains("ModelPrefix"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "ModelPrefix";
                table.Columns.Add(col);
            }
            if (!table.Columns.Contains("ModelSuffix"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "ModelSuffix";
                table.Columns.Add(col);
            }
            if (!table.Columns.Contains("BLLPrefix"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "BLLPrefix";
                table.Columns.Add(col);
            }
            if (!table.Columns.Contains("BLLSuffix"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "BLLSuffix";
                table.Columns.Add(col);
            }
            if (!table.Columns.Contains("DALPrefix"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "DALPrefix";
                table.Columns.Add(col);
            }
            if (!table.Columns.Contains("DALSuffix"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "DALSuffix";
                table.Columns.Add(col);
            }
            if (!table.Columns.Contains("TabNameRule"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "TabNameRule";
                table.Columns.Add(col);
            }
            if (!table.Columns.Contains("WebTemplatePath"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "WebTemplatePath";
                table.Columns.Add(col);
            }

            if (!table.Columns.Contains("ReplacedOldStr"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "ReplacedOldStr";
                table.Columns.Add(col);
            }

            if (!table.Columns.Contains("ReplacedNewStr"))
            {
                col = new DataColumn();
                col.DataType = Type.GetType("System.String");
                col.ColumnName = "ReplacedNewStr";
                table.Columns.Add(col);
            }
        }

        private static DataRow NewDataRow(DataTable dt, DbSettings dbobj)
        {
            AddColForTable(dt);
            DataRow rown = dt.NewRow();
            rown["DbType"] = dbobj.DbType;
            rown["Server"] = dbobj.Server;
            rown["ConnectStr"] = dbobj.ConnectStr;
            rown["DbName"] = dbobj.DbName;
            rown["ConnectSimple"] = dbobj.ConnectSimple;
            rown["TabLoadtype"] = dbobj.TabLoadtype;
            rown["TabLoadKeyword"] = dbobj.TabLoadKeyword;

            rown["ProcPrefix"] = dbobj.ProcPrefix;
            rown["ProjectName"] = dbobj.ProjectName;
            rown["Namepace"] = dbobj.Namepace;
            rown["Folder"] = dbobj.Folder;
            rown["AppFrame"] = dbobj.AppFrame;
            rown["DALType"] = dbobj.DALType;
            rown["BLLType"] = dbobj.BLLType;
            rown["WebType"] = dbobj.WebType;
            rown["EditFont"] = dbobj.EditFont;
            rown["EditFontSize"] = dbobj.EditFontSize;
            rown["DbHelperName"] = dbobj.DbHelperName;
            rown["ModelPrefix"] = dbobj.ModelPrefix;
            rown["ModelSuffix"] = dbobj.ModelSuffix;
            rown["BLLPrefix"] = dbobj.BLLPrefix;
            rown["BLLSuffix"] = dbobj.BLLSuffix;
            rown["DALPrefix"] = dbobj.DALPrefix;
            rown["DALSuffix"] = dbobj.DALSuffix;
            rown["TabNameRule"] = dbobj.TabNameRule;
            rown["WebTemplatePath"] = dbobj.WebTemplatePath;
            rown["ReplacedOldStr"] = dbobj.ReplacedOldStr;
            rown["ReplacedNewStr"] = dbobj.ReplacedNewStr;
          

            return rown;
        }

        /// <summary>
        /// ���ӵ�ǰ����Դ����,1:�ɹ���0:ʧ�ܣ�2:�Ѿ�����
        /// </summary>
        /// <param name="data"></param>
		public static int AddSettings(DbSettings dbobj)
		{
			try
			{				
				DataSet ds=new DataSet();			
				if(!File.Exists(fileName))
                {
                    #region ��һ�����
                    DataTable dt=CreateDataTable();	
                    DataRow rown= NewDataRow(dt, dbobj);
			
					dt.Rows.Add(rown);
			
					ds.Tables.Add(dt);
                    #endregion
                }
				else
                {
                    #region ׷��

                    ds.ReadXml(fileName);//,XmlReadMode.ReadSchema
					if(ds.Tables.Count>0)
					{
                        DataRow[] drs = ds.Tables[0].Select("DbType='" + dbobj.DbType + "' and Server='" + dbobj.Server + "' and DbName='" + dbobj.DbName + "'");
						if(drs.Length>0)
						{                            
                            return 2;
                        }
                        else
                        {                            
                            DataRow rown = NewDataRow(ds.Tables[0], dbobj);
                            ds.Tables[0].Rows.Add(rown);
						}
					}
					else
					{
						DataTable dt=CreateDataTable();                                                
                        DataRow rown = NewDataRow(dt, dbobj);
						dt.Rows.Add(rown);			
						ds.Tables.Add(dt);
                    }
                    #endregion

                }
				ds.WriteXml(fileName);
                return 1;
			}
			catch
			{
				//throw new Exception("����������Ϣʧ�ܣ�");
                return 0;
			}
		}

        /// <summary>
        /// ���µ�ǰ����Դ����
        /// </summary>
        /// <param name="data"></param>
        public static void UpdateSettings(DbSettings dbobj)
        {
            try
            {                
                DataSet ds = new DataSet();
                if (!File.Exists(fileName))
                {
                    DataTable dt = CreateDataTable();                    
                    DataRow rown = NewDataRow(dt, dbobj);
                    dt.Rows.Add(rown);

                    ds.Tables.Add(dt);
                }
                else
                {
                    ds.ReadXml(fileName);//,XmlReadMode.ReadSchema
                    if (ds.Tables.Count > 0)
                    {
                        DataRow[] drs = ds.Tables[0].Select("DbType='" + dbobj.DbType + "' and Server='" + dbobj.Server + "' and DbName='" + dbobj.DbName + "'");
                        if (drs.Length > 0)
                        {
                            AddColForTable(ds.Tables[0]);

                            DataRow rown = drs[0];
                            rown["DbType"] = dbobj.DbType;
                            rown["Server"] = dbobj.Server;
                            rown["ConnectStr"] = dbobj.ConnectStr;
                            rown["DbName"] = dbobj.DbName;
                            rown["ConnectSimple"] = dbobj.ConnectSimple;
                            rown["TabLoadtype"] = dbobj.TabLoadtype;
                            rown["TabLoadKeyword"] = dbobj.TabLoadKeyword;
                            
                            rown["ProcPrefix"] = dbobj.ProcPrefix;
                            rown["ProjectName"] = dbobj.ProjectName;
                            rown["Namepace"] = dbobj.Namepace;                            
                            rown["Folder"] = dbobj.Folder;
                            rown["AppFrame"] = dbobj.AppFrame;
                            rown["DALType"] = dbobj.DALType;
                            rown["BLLType"] = dbobj.BLLType;
                            rown["WebType"] = dbobj.WebType;
                            rown["EditFont"] = dbobj.EditFont;
                            rown["EditFontSize"] = dbobj.EditFontSize;
                            rown["DbHelperName"] = dbobj.DbHelperName;
                            rown["ModelPrefix"] = dbobj.ModelPrefix;
                            rown["ModelSuffix"] = dbobj.ModelSuffix;
                            rown["BLLPrefix"] = dbobj.BLLPrefix;
                            rown["BLLSuffix"] = dbobj.BLLSuffix;
                            rown["DALPrefix"] = dbobj.DALPrefix;
                            rown["DALSuffix"] = dbobj.DALSuffix;
                            rown["TabNameRule"] = dbobj.TabNameRule;
                            rown["WebTemplatePath"] = dbobj.WebTemplatePath;
                            rown["ReplacedOldStr"] = dbobj.ReplacedOldStr;
                            rown["ReplacedNewStr"] = dbobj.ReplacedNewStr;
                            
                        }
                        else
                        {                            
                            DataRow rown = NewDataRow(ds.Tables[0], dbobj);
                            ds.Tables[0].Rows.Add(rown);
                        }
                    }
                    else
                    {
                        DataTable dt = CreateDataTable();                        
                        DataRow rown = NewDataRow(dt, dbobj);
                        dt.Rows.Add(rown);
                        ds.Tables.Add(dt);
                    }

                }
                ds.WriteXml(fileName);
            }
            catch
            {
                throw new Exception("����������Ϣʧ�ܣ�");
            }
        }	


		#endregion

        #region ɾ�� ָ������Դ��������Ϣ
        /// <summary>
        /// ɾ��ָ������Դ��������Ϣ
        /// </summary>
        /// <returns></returns>
        public static void DelSetting(string DbType, string Serverip, string DbName)
        {
            try
            {             
                DataSet ds = new DataSet();
                if (File.Exists(fileName))
                {
                    ds.ReadXml(fileName);//,XmlReadMode.ReadSchema
                    if (ds.Tables.Count > 0)
                    {
                        string strwhere = "DbType='" + DbType + "' and Server='" + Serverip + "'";
                        if ((DbName.Trim() != "") && (DbName.Trim() != "master"))
                        {
                            strwhere += " and DbName='" + DbName + "'";
                        }
                        DataRow[] drs = ds.Tables[0].Select(strwhere);
                        if (drs.Length > 0)
                        {
                            ds.Tables[0].Rows.Remove(drs[0]);                           
                        }
                        ds.Tables[0].AcceptChanges();
                    }
                }
                ds.WriteXml(fileName);
            }
            catch
            {
                //return null;
            }
        }
        #endregion
	
	}
}
