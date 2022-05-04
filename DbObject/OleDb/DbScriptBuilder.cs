using System;
using System.Data;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Maticsoft.Utility;
using Maticsoft.IDBO;
using Maticsoft.CodeHelper;
namespace Maticsoft.DbObjects.OleDb
{
	/// <summary>
	/// ���ݿ�ű������ࡣScript
	/// </summary>
	public class DbScriptBuilder:IDbScriptBuilder
	{
        #region ˽�б���
        protected string _key = "ID";//��ʶ�У��������ֶ�		
        protected string _keyType = "int";//��ʶ�У��������ֶ�����        
        #endregion

		#region ����
		private string _dbconnectStr;
		private string _dbname;
		private string _tablename;				
        private string _procprefix;
        private string _projectname;
        private List<ColumnInfo> _fieldlist;
        private List<ColumnInfo> _keys; //�����������ֶ��б� 

		public string DbConnectStr
		{
			set{_dbconnectStr=value;}
			get{return _dbconnectStr;}
		}
		public string DbName
		{
			set{ _dbname=value;}
			get{return _dbname;}
		}
		public string TableName
		{
			set{ _tablename=value;}
			get{return _tablename;}
		}		
        /// <summary>
        /// �洢����ǰ׺ 
        /// </summary>       
        public string ProcPrefix
        {
            set { _procprefix = value; }
            get { return _procprefix; }
        }
        /// <summary>
        /// ��Ŀ���� 
        /// </summary>        
        public string ProjectName
        {
            set { _projectname = value; }
            get { return _projectname; }
        }       
        /// <summary>
        /// ѡ����ֶμ���
        /// </summary>
        public List<ColumnInfo> Fieldlist
        {
            set { _fieldlist = value; }
            get { return _fieldlist; }
        }       
        public List<ColumnInfo> Keys
        {
            set { _keys = value; }
            get { return _keys; }
        }

		#endregion

        #region ��������
        /// <summary>
        /// ѡ����ֶμ��ϵ�-�ַ���
        /// </summary>
        public string Fields
        {
            get
            {
                StringPlus _fields = new StringPlus();
                foreach (ColumnInfo obj in Fieldlist)
                {
                    _fields.Append("'" + obj.ColumnName + "',");
                }
                _fields.DelLastComma();
                return _fields.Value;
            }
        }
        /// <summary>
        /// �ֶε� select * �б�
        /// </summary>
        public string Fieldstrlist
        {
            get
            {
                StringPlus _fields = new StringPlus();
                foreach (ColumnInfo obj in Fieldlist)
                {
                    _fields.Append(obj.ColumnName + ",");
                }
                _fields.DelLastComma();
                return _fields.Value;
            }
        }
        /// <summary>
        /// �����Ƿ��б�ʶ��
        /// </summary>
        public bool IsHasIdentity
        {
            get
            {
                bool isid = false;
                if (_keys.Count > 0)
                {
                    foreach (ColumnInfo key in _keys)
                    {
                        if (key.IsIdentity)
                        {
                            isid = true;
                        }
                    }
                }
                return isid;
            }
        }
        #endregion


		DbObject dbobj=new DbObject();

		public DbScriptBuilder()
		{			
		}

        #region ��������

        /// <summary>
        /// �õ���������������б� (���磺����Exists  Delete  GetModel �Ĳ�������)
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public string GetInParameter(List<ColumnInfo> keys, bool output)
        {
            StringPlus strclass = new StringPlus();

            foreach (ColumnInfo field in Keys)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                bool IsIdentity = field.IsIdentity;
                bool IsPK = field.IsPrimaryKey;
                string Length = field.Length;
                string Preci = field.Precision;
                string Scale = field.Scale;

                switch (columnType.ToLower())
                {
                    case "decimal":
                    case "numeric":
                        strclass.AppendLine("@" + columnName + " " + columnType + "(" + Preci + "," + Scale + ")");
                        break;
                    case "varchar":
                    case "nvarchar":
                    case "char":
                    case "nchar":
                        strclass.AppendLine("@" + columnName + " " + columnType + "(" + Length + ")");
                        break;
                    default:
                        strclass.AppendLine("@" + columnName + " " + columnType);
                        break;
                }
                if ((IsIdentity) && (output))
                {
                    strclass.AppendLine(" output,");
                    continue;
                }
                strclass.AppendLine(",");
            }
            strclass.DelLastComma();
            return strclass.Value;
        }

        /// <summary>
        /// �õ�Where������� - Parameter��ʽ (���磺����Exists  Delete  GetModel ��where)
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public string GetWhereExpression(List<ColumnInfo> keys)
        {
            StringPlus strclass = new StringPlus();
            foreach (ColumnInfo key in keys)
            {
                strclass.Append(key.ColumnName + "=@" + key.ColumnName + " and ");
            }
            strclass.DelLastChar("and");
            return strclass.Value;
        }


        #endregion

		#region �������ݿ�����ű�

        /// <summary>
        /// �������ݿ����б�Ĵ����ű�
        /// </summary>
        /// <returns></returns>
        public string CreateDBTabScript(string dbname)
        {
            dbobj.DbConnectStr = this.DbConnectStr;
            StringPlus strclass = new StringPlus();
            List<string> tabNames = dbobj.GetTables(dbname);
            if (tabNames.Count > 0)
            {
                foreach (string tabname in tabNames)
                {
                    strclass.AppendLine(CreateTabScript(dbname, tabname));
                }
            }
            return strclass.Value;
        }


		/// <summary>
		/// �������ݿ�����ű�
		/// </summary>
		/// <returns></returns>
		public string CreateTabScript(string dbname,string tablename)
		{
			dbobj.DbConnectStr=_dbconnectStr;
            List<ColumnInfo> collist = dbobj.GetColumnInfoList(dbname, tablename);
            DataTable dt = Maticsoft.CodeHelper.CodeCommon.GetColumnInfoDt(collist);
			StringPlus strclass=new StringPlus();
			
			string PKfild="";//�����ֶ�
			StringPlus ColdefaVal=new StringPlus();//�ֶε�Ĭ��ֵ�б�			
			
			Hashtable FildtabList=new Hashtable();//�ֶ��б�
			StringPlus FildList=new StringPlus();//�ֶ��б�
			//��ʼ������
			strclass.AppendLine("");
			strclass.AppendLine("CREATE TABLE ["+tablename+"] (");
			if(dt!=null)
			{				
				foreach(DataRow row in dt.Rows)
				{					
					string columnName=row["ColumnName"].ToString();	
					string columnType=row["TypeName"].ToString();				
					string Length=row["Length"].ToString();
					string Preci=row["Preci"].ToString();
					string Scale=row["Scale"].ToString();
					string ispk=row["isPK"].ToString();	
					string isnull=row["cisNull"].ToString();
					string defaultVal=row["defaultVal"].ToString();

					strclass.Append("["+columnName+"] ["+columnType+"] ");
					switch(columnType.Trim())
					{	
						case "CHAR":					
						case "VARCHAR2":
						case "NCHAR":
						case "NVARCHAR2":						
							strclass.Append(" ("+Length+")");
							break;
						case "NUMBER":		
							strclass.Append(" ("+Preci+","+Scale+")");
							break;						
					}				
					if(isnull=="��")
					{
						strclass.Append(" NULL");
					}
					else
					{
						strclass.Append(" NOT NULL");
					}
					if(defaultVal!="")
					{
						strclass.Append(" DEFAULT "+defaultVal);
					}
					strclass.AppendLine(",");

					FildtabList.Add(columnName,columnType);
					FildList.Append("["+columnName+"],");
					
//					if(defaultVal!="")
//					{
//						ColdefaVal.Append("CONSTRAINT [DF_"+tablename+"_"+columnName+"] DEFAULT "+defaultVal+" FOR ["+columnName+"],");
//					}

					if(PKfild=="")
					{						
						PKfild=columnName;//�õ�����
					}
				}
			}
			strclass.DelLastComma();
			FildList.DelLastComma();
			strclass.AppendLine(")");
			strclass.AppendLine("");

			//��ȡ����
			DataTable dtdata=dbobj.GetTabData(dbname,tablename);
			if(dtdata!=null)
			{				
				foreach(DataRow row in dtdata.Rows)//ѭ��������
				{	
					StringPlus strfild=new StringPlus();
					StringPlus strdata=new StringPlus();
					string [] split= FildList.Value.Split(new Char [] { ','});

					foreach(string fild in split)//ѭ��һ�����ݵĸ����ֶ�
					{
						string colname=fild.Substring(1,fild.Length-2);
						string coltype="";
						foreach (DictionaryEntry myDE in FildtabList)
						{
							if(myDE.Key.ToString()==colname)
							{
								coltype=myDE.Value.ToString();
							}
						}	
						string strval="";
						switch(coltype)
						{
							case "BLOB":
							{
								byte[] bys=(byte[])row[colname];
                                strval = Maticsoft.CodeHelper.CodeCommon.ToHexString(bys);
							}
								break;
							case "bit":
							{
								strval=(row[colname].ToString().ToLower()=="true")?"1":"0";
							}
								break;
							default:
								strval=row[colname].ToString().Trim();
								break;
						}
						if(strval!="")
						{
                            if (Maticsoft.CodeHelper.CodeCommon.IsAddMark(coltype))
							{
                                //strdata.Append("'"+strval+"',");
                                strdata.Append("'" + strval.Replace("'", "''") + "',");
							}
							else
							{
								strdata.Append(strval+",");
							}	
							strfild.Append("["+colname+"],");
						}

					}					
					strfild.DelLastComma();
					strdata.DelLastComma();
					//��������INSERT���
					strclass.Append("INSERT ["+tablename+"] (");
					strclass.Append(strfild.Value);
					strclass.Append(") VALUES ( ");
					strclass.Append(strdata.Value);//����ֵ
					strclass.AppendLine(")");
				}
			}


			return strclass.Value;

		}

        /// <summary>
        /// ����SQL��ѯ��� �������ݴ����ű�
        /// </summary>
        /// <returns></returns>
        public string CreateTabScriptBySQL(string dbname, string strSQL)
        {
            dbobj.DbConnectStr = _dbconnectStr;
            //string PKfild = "";//�����ֶ�
            bool IsIden = false;//�Ƿ��Ǳ�ʶ�ֶ�
            string tablename = "TableName";
            StringPlus strclass = new StringPlus();

            #region ��ѯ����
            int ns = strSQL.IndexOf(" from ");
            if (ns > 0)
            {
                string sqltemp = strSQL.Substring(ns + 5).Trim();
                int ns2 = sqltemp.IndexOf(" ");
                if (sqltemp.Length > 0)
                {
                    if (ns2 > 0)
                    {
                        tablename = sqltemp.Substring(0, ns2).Trim();
                    }
                    else
                    {
                        tablename = sqltemp.Substring(0).Trim();
                    }
                }
            }
            tablename = tablename.Replace("[", "").Replace("]", "");

            #endregion

            #region ���ݽű�

            //��ȡ����
            DataTable dtdata = dbobj.GetTabDataBySQL(dbname, strSQL);
            if (dtdata != null)
            {
                DataColumnCollection dtcols = dtdata.Columns;
                foreach (DataRow row in dtdata.Rows)//ѭ��������
                {
                    StringPlus strfild = new StringPlus();
                    StringPlus strdata = new StringPlus();

                    foreach (DataColumn col in dtcols)//ѭ��һ�����ݵĸ����ֶ�
                    {
                        string colname = col.ColumnName;
                        string coltype = col.DataType.Name;
                        if (col.AutoIncrement)
                        {
                            IsIden = true;
                        }
                        string strval = "";
                        switch (coltype.ToLower())
                        {
                            case "binary":
                            case "byte[]":
                            case "blob":
                                {
                                    byte[] bys = (byte[])row[colname];
                                    strval = Maticsoft.CodeHelper.CodeCommon.ToHexString(bys);
                                }
                                break;
                            case "bit":
                            case "boolean":
                                {
                                    strval = (row[colname].ToString().ToLower() == "true") ? "1" : "0";
                                }
                                break;
                            default:
                                strval = row[colname].ToString().Trim();
                                break;
                        }
                        if (strval != "")
                        {
                            if (Maticsoft.CodeHelper.CodeCommon.IsAddMark(coltype))
                            {
                                //strdata.Append("'" + strval + "',");
                                strdata.Append("'" + strval.Replace("'", "''") + "',");
                            }
                            else
                            {
                                strdata.Append(strval + ",");
                            }
                            strfild.Append("" + colname + ",");
                        }

                    }
                    strfild.DelLastComma();
                    strdata.DelLastComma();
                    //��������INSERT���
                    strclass.Append("INSERT " + tablename + " (");
                    strclass.Append(strfild.Value);
                    strclass.Append(") VALUES ( ");
                    strclass.Append(strdata.Value);//����ֵ
                    strclass.AppendLine(")");
                }
            }
            //StringPlus strclass0 = new StringPlus();
            //if (IsIden)
            //{
            //    strclass0.AppendLine("SET IDENTITY_INSERT [" + tablename + "] ON");
            //    strclass0.AppendLine("");
            //}
            //strclass0.AppendLine(strclass.Value);
            //if (IsIden)
            //{
            //    strclass0.AppendLine("");
            //    strclass0.AppendLine("SET IDENTITY_INSERT [" + tablename + "] OFF");
            //}
            #endregion
            return strclass.Value;

        }

	
		/// <summary>
		/// �������ݿ�����ű����ļ�
		/// </summary>
		/// <returns></returns>
		public void CreateTabScript(string dbname,string tablename,string filename,System.Windows.Forms.ProgressBar progressBar)
		{
			StreamWriter sw=new StreamWriter(filename,true,Encoding.Default);//,false);

			dbobj.DbConnectStr=_dbconnectStr;
            List<ColumnInfo> collist = dbobj.GetColumnInfoList(dbname, tablename);
			StringPlus strclass=new StringPlus();
			
			string PKfild="";//�����ֶ�
			StringPlus ColdefaVal=new StringPlus();//�ֶε�Ĭ��ֵ�б�
			
			
			Hashtable FildtabList=new Hashtable();//�ֶ��б�
			StringPlus FildList=new StringPlus();//�ֶ��б�
			
			#region �����Ľű�

			//��ʼ������
			strclass.AppendLine("");
			strclass.AppendLine("CREATE TABLE ["+tablename+"] (");
            if ((collist!=null)&&(collist.Count > 0))
            {
                foreach (ColumnInfo col in collist)
                {
                    string columnName = col.ColumnName;
                    string columnType = col.TypeName;
                    bool IsIdentity = col.IsIdentity;
                    string Length = col.Length;
                    string Preci = col.Precision;
                    string Scale = col.Scale;
                    bool ispk = col.IsPrimaryKey;
                    bool isnull = col.Nullable;
                    string defaultVal = col.DefaultVal;


					strclass.Append("["+columnName+"] ["+columnType+"] ");					
					switch(columnType.Trim())
					{	
						case "char":					
						case "varchar":
						case "nchar":
						case "nvarchar":						
							strclass.Append(" ("+Length+")");
							break;
						case "float":		
							strclass.Append(" ("+Preci+","+Scale+")");
							break;							
					}				
					if(isnull)
					{
						strclass.Append(" NULL");
					}
					else
					{
						strclass.Append(" NOT NULL");
					}
					if(defaultVal!="")
					{
						strclass.Append(" DEFAULT "+defaultVal);
					}
					strclass.AppendLine(",");

					FildtabList.Add(columnName,columnType);
					FildList.Append("["+columnName+"],");
					
//					if(defaultVal!="")
//					{
//						ColdefaVal.Append("CONSTRAINT [DF_"+tablename+"_"+columnName+"] DEFAULT "+defaultVal+" FOR ["+columnName+"],");
//					}

					if(PKfild=="")
					{						
						PKfild=columnName;//�õ�����
					}
				}
			}
			strclass.DelLastComma();
			FildList.DelLastComma();
			strclass.AppendLine(")");
			strclass.AppendLine("");
			
			if(PKfild!="")
			{
				strclass.Append("ALTER TABLE ["+tablename+"] WITH NOCHECK ADD  CONSTRAINT [PK_"+tablename+"] PRIMARY KEY  NONCLUSTERED ( ["+PKfild+"] )");
			}

			#endregion			

			#region
			//��������
			//			if((PKfild!="")||(ColdefaVal.Value!=""))
			//			{				
			//				strclass.AppendLine("ALTER TABLE ["+tablename+"] WITH NOCHECK ADD ");
			//				if(ColdefaVal.Value!="")
			//				{
			//					strclass.Append(ColdefaVal.Value);
			//				}
			//				if(PKfild!="")
			//				{
			//					strclass.Append(" CONSTRAINT [PK_"+tablename+"] PRIMARY KEY  NONCLUSTERED ( ["+PKfild+"] )");
			//				}
			//				else
			//				{
			//					strclass.DelLastComma();
			//				}
			//			}
			#endregion

			sw.Write(strclass.Value);

			#region �������ݽű�

			//��ȡ����
			DataTable dtdata=dbobj.GetTabData(dbname,tablename);
			if(dtdata!=null)
			{		
				int i=0;				
				progressBar.Maximum=dtdata.Rows.Count;				
				foreach(DataRow row in dtdata.Rows)//ѭ��������
				{						
					progressBar.Value=i;
					i++;
					StringPlus rowdata=new StringPlus();

					StringPlus strfild=new StringPlus();
					StringPlus strdata=new StringPlus();
					string [] split= FildList.Value.Split(new Char [] { ','});

					foreach(string fild in split)//ѭ��һ�����ݵĸ����ֶ�
					{
						string colname=fild.Substring(1,fild.Length-2);
						string coltype="";
						foreach (DictionaryEntry myDE in FildtabList)
						{
							if(myDE.Key.ToString()==colname)
							{
								coltype=myDE.Value.ToString();
							}
						}	
						string strval="";
						switch(coltype)
						{
							case "binary":
							{
								byte[] bys=(byte[])row[colname];
                                strval = Maticsoft.CodeHelper.CodeCommon.ToHexString(bys);
							}
								break;
							case "bit":
							case "bool":
							{
								strval=(row[colname].ToString().ToLower()=="true")?"1":"0";
							}
								break;
							default:
								strval=row[colname].ToString().Trim();
								break;
						}
						if(strval!="")
						{
                            if (Maticsoft.CodeHelper.CodeCommon.IsAddMark(coltype))
							{
                                //strdata.Append("'"+strval+"',");
                                strdata.Append("'" + strval.Replace("'", "''") + "',");
							}
							else
							{
								strdata.Append(strval+",");
							}	
							strfild.Append("["+colname+"],");
						}

					}					
					strfild.DelLastComma();
					strdata.DelLastComma();

					//��������INSERT���
					rowdata.Append("INSERT ["+tablename+"] (");
					rowdata.Append(strfild.Value);
					rowdata.Append(") VALUES ( ");
					rowdata.Append(strdata.Value);//����ֵ
					rowdata.AppendLine(")");

					sw.Write(rowdata.Value);
				}
			}

			#endregion

			sw.Flush();
			sw.Close();

		}


		
		#endregion

		#region �����洢����
        /// <summary>
        /// �õ�һ���������б�Ĵ洢����
        /// </summary>
        /// <param name="DbName"></param>
        /// <returns></returns>
        public string GetPROCCode(string dbname)
        {
            return "";
        }

        /// <summary>
        /// �õ�ĳ����Ĵ洢����
        /// </summary>
        /// <param name="dbname">����</param>
        /// <param name="tablename">����</param>
        /// <returns></returns>
        public string GetPROCCode(string dbname, string tablename)
        {
            return "";
        }
        /// <summary>
        /// �õ�ĳ����Ĵ洢���̣�ѡ�����ɵķ�����
        /// </summary>   
        public string GetPROCCode(bool Maxid, bool Ishas, bool Add, bool Update, bool Delete, bool GetModel, bool List)
        {
            return "";
        }

        public string CreatPROCGetMaxID()
        {
            return "";
        }
        public string CreatPROCIsHas()
        {
            return "";
        }
        public string CreatPROCADD()
        {
            return "";
        }
        public string CreatPROCUpdate()
        {
            return "";
        }
        public string CreatPROCDelete()
        {
            return "";

        }
        public string CreatPROCGetModel()
        {
            return "";

        }
        public string CreatPROCGetList()
        {
            return "";

        }		
		
		#endregion


        #region ����SQL��ѯ���

        /// <summary>
        /// ����Select��ѯ���
        /// </summary>
        /// <param name="dbname">����</param>
        /// <param name="tablename">����</param>
        /// <returns></returns>
        public string GetSQLSelect(string dbname, string tablename)
        {
            dbobj.DbConnectStr = _dbconnectStr;
            List<ColumnInfo> collist = dbobj.GetColumnList(dbname, tablename);
            this.DbName = dbname;
            this.TableName = tablename;
            StringPlus strsql = new StringPlus();
            strsql.Append("select ");
            if ((collist!=null)&&(collist.Count > 0))
            {
                foreach (ColumnInfo col in collist)
                {
                    string columnName = col.ColumnName;
                    strsql.Append("[" + columnName + "],");
                }
                strsql.DelLastComma();
            }
            strsql.Append(" from " + tablename);
            return strsql.Value;
        }

        /// <summary>
        /// ����update��ѯ���
        /// </summary>
        /// <param name="dbname">����</param>
        /// <param name="tablename">����</param>
        /// <returns></returns>
        public string GetSQLUpdate(string dbname, string tablename)
        {
            dbobj.DbConnectStr = _dbconnectStr;
            List<ColumnInfo> collist = dbobj.GetColumnList(dbname, tablename);
            this.DbName = dbname;
            this.TableName = tablename;
            StringPlus strsql = new StringPlus();
            strsql.AppendLine("update " + tablename + " set ");
            if ((collist!=null)&&(collist.Count > 0))
            {
                foreach (ColumnInfo col in collist)
                {
                    string columnName = col.ColumnName;
                    strsql.AppendLine("[" + columnName + "] = <" + columnName + ">,");
                }
                strsql.DelLastComma();
            }
            strsql.Append(" where <��������>");
            return strsql.Value;
        }
        /// <summary>
        /// ����update��ѯ���
        /// </summary>
        /// <param name="dbname">����</param>
        /// <param name="tablename">����</param>
        /// <returns></returns>
        public string GetSQLDelete(string dbname, string tablename)
        {
            dbobj.DbConnectStr = _dbconnectStr;
            //DataTable dt = dbobj.GetColumnList(dbname, tablename);
            this.DbName = dbname;
            this.TableName = tablename;
            StringPlus strsql = new StringPlus();
            strsql.AppendLine("delete from " + tablename);
            strsql.Append(" where  <��������>");
            return strsql.Value;
        }
        /// <summary>
        /// ����INSERT��ѯ���
        /// </summary>
        /// <param name="dbname">����</param>
        /// <param name="tablename">����</param>
        /// <returns></returns>
        public string GetSQLInsert(string dbname, string tablename)
        {
            dbobj.DbConnectStr = _dbconnectStr;
            List<ColumnInfo> collist = dbobj.GetColumnList(dbname, tablename);
            this.DbName = dbname;
            this.TableName = tablename;
            StringPlus strsql = new StringPlus();
            StringPlus strsql2 = new StringPlus();
            strsql.AppendLine("INSERT INTO " + tablename + " ( ");

            if ((collist!=null)&&(collist.Count > 0))
            {
                foreach (ColumnInfo col in collist)
                {
                    string columnName = col.ColumnName;
                    string columnType = col.TypeName;

                    strsql.AppendLine("[" + columnName + "] ,");
                    if (Maticsoft.CodeHelper.CodeCommon.IsAddMark(columnType))
                    {
                        strsql2.Append("'" + columnName + "',");
                    }
                    else
                    {
                        strsql2.Append(columnName + ",");
                    }

                }
                strsql.DelLastComma();
                strsql2.DelLastComma();
            }
            strsql.Append(") VALUES (" + strsql2.Value + ")");
            return strsql.Value;
        }
        #endregion

        
    }
}
