using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using Maticsoft.Utility;
using Maticsoft.IDBO;
using Maticsoft.CodeHelper;
namespace Maticsoft.BuilderDALTranMParam
{
    /// <summary>
    /// �������������ɣ�Parameter��ʽ��
    /// </summary>
    public class BuilderDAL : Maticsoft.IBuilder.IBuilderDALMTran
    {

        #region ˽�б���
        protected string _key = "ID";//��ʶ�У��������ֶ�		
        protected string _keyType = "int";//��ʶ�У��������ֶ�����        
        #endregion

        #region ��������
        IDbObject dbobj;
        private string _dbname;                
        private string _namespace; //���������ռ���
        private string _folder; //�����ļ���               
        private string _modelpath;      
        private string _dalpath;   
        private string _idalpath;
        private string _iclass;
        private string _dbhelperName;//���ݿ��������    

        private List<ModelTran> _modelTranList;


        public IDbObject DbObject
        {
            set { dbobj = value; }
            get { return dbobj; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string DbName
        {
            set { _dbname = value; }
            get { return _dbname; }
        }
        
        /// <summary>
        /// ���������ռ���
        /// </summary>
        public string NameSpace
        {
            set { _namespace = value; }
            get { return _namespace; }
        }
        /// <summary>
        /// �����ļ���
        /// </summary>
        public string Folder
        {
            set { _folder = value; }
            get { return _folder; }
        }

        /// <summary>
        /// ʵ����������ռ�
        /// </summary>
        public string Modelpath
        {
            set { _modelpath = value; }
            get { return _modelpath; }
        }
       
        
        /// <summary>
        /// ���ݲ�������ռ�
        /// </summary>
        public string DALpath
        {
            set { _dalpath = value; }
            get
            {
                return _dalpath;
            }
        }
       

        /// <summary>
        /// �ӿڵ������ռ�
        /// </summary>
        public string IDALpath
        {
            set { _idalpath = value; }
            get
            {
                return _idalpath;
            }
        }
        /// <summary>
        /// �ӿ�����
        /// </summary>
        public string IClass
        {
            set { _iclass = value; }
            get
            {
                return _iclass;
            }
        }
        /// <summary>
        /// ���ݿ��������
        /// </summary>
        public string DbHelperName
        {
            set { _dbhelperName = value; }
            get { return _dbhelperName; }
        }
        
        public List<ModelTran> ModelTranList
        {
            set { _modelTranList = value; }
            get { return _modelTranList; }
        }
        
        //�����ļ�
        public Hashtable Languagelist
        {
            get
            {
                return Maticsoft.CodeHelper.Language.LoadFromCfg("BuilderDALTranParam.lan");
            }
        }

        #endregion

        #region ��������

       
        /// <summary>
        /// ��ͬ���ݿ����ǰ׺
        /// </summary>
        public string DbParaHead
        {
            get
            {
                return CodeCommon.DbParaHead(dbobj.DbType);
            }

        }
        /// <summary>
        ///  ��ͬ���ݿ��ֶ�����
        /// </summary>
        public string DbParaDbType
        {
            get
            {
                return CodeCommon.DbParaDbType(dbobj.DbType);
            }
        }

        /// <summary>
        /// �洢���̲��� ���÷���@
        /// </summary>
        public string preParameter
        {
            get
            {
                return CodeCommon.preParameter(dbobj.DbType);
            }
        }

        /// <summary>
        /// ʵ��������������ռ� + ������������ Modelpath+ModelName
        /// </summary>
        public string ModelSpace(string ModelName)
        {
            return Modelpath + "." + ModelName;
        }


        #endregion

        #region ���캯��

        public BuilderDAL()
        {
        }
        public BuilderDAL(IDbObject idbobj)
        {
            dbobj = idbobj;
        }

        public BuilderDAL(IDbObject idbobj, string dbname, string tablename, string modelname,
            List<ColumnInfo> fieldlist, List<ColumnInfo> keys, string namepace,
            string folder, string dbherlpername, string modelpath,
            string dalpath, string idalpath, string iclass)
        {
            dbobj = idbobj;
            _dbname = dbname;           
            _namespace = namepace;
            _folder = folder;
            _dbhelperName = dbherlpername;
            _modelpath = modelpath;
            _dalpath = dalpath;
            _idalpath = idalpath;
            _iclass = iclass;
           
            
        }

        #endregion


        #region  ��������Ϣ �õ��������б�

       

        /// <summary>
        /// ����sql����еĲ����б�(���磺����Add  Exists  Update Delete  GetModel �Ĳ�������)
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public string GetPreParameter(List<ColumnInfo> keys, string numPara)
        {
            StringPlus strclass = new StringPlus();
            StringPlus strclass2 = new StringPlus();
            strclass.AppendSpaceLine(3, "" + DbParaHead + "Parameter[] parameters" + numPara + " = {");
            int n = 0;
            foreach (ColumnInfo key in keys)
            {
                strclass.AppendSpaceLine(5, "new " + DbParaHead + "Parameter(\"" + preParameter + "" + key.ColumnName + "\", " + DbParaDbType + "." + CodeCommon.DbTypeLength(dbobj.DbType, key.TypeName, "") + "),");
                strclass2.AppendSpaceLine(3, "parameters" + numPara + "[" + n.ToString() + "].Value = " + key.ColumnName + ";");
                n++;
            }
            strclass.DelLastComma();
            strclass.AppendLine("};");
            strclass.Append(strclass2.Value);
            return strclass.Value;
        }

        #endregion



        #region ���ݲ�(������)
        /// <summary>
        /// �õ�������Ĵ���
        /// </summary>     
        public string GetDALCode()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendLine("using System;");
            strclass.AppendLine("using System.Data;");
            strclass.AppendLine("using System.Text;");
            strclass.AppendLine("using System.Collections.Generic;");
            switch (dbobj.DbType)
            {
                case "SQL2005":
                case "SQL2008":
                case "SQL2012":
                    strclass.AppendLine("using System.Data.SqlClient;");
                    break;
                case "SQL2000":
                    strclass.AppendLine("using System.Data.SqlClient;");
                    break;
                case "Oracle":
                    strclass.AppendLine("using System.Data.OracleClient;");
                    break;
                case "OleDb":
                    strclass.AppendLine("using System.Data.OleDb;");
                    break;
                case "SQLite":
                    strclass.AppendLine("using System.Data.SQLite;");
                    break;
            }
            if (IDALpath != "")
            {
                strclass.AppendLine("using " + IDALpath + ";");
            }
            strclass.AppendLine("using Maticsoft.DBUtility;//Please add references");
            strclass.AppendLine("namespace " + DALpath);
            strclass.AppendLine("{");
            strclass.AppendSpaceLine(1, "/// <summary>");
            strclass.AppendSpaceLine(1, "/// " + Languagelist["summary"].ToString() );
            strclass.AppendSpaceLine(1, "/// </summary>");
            strclass.AppendSpace(1, "public partial class Class1");//+ DALNameParent
            if (IClass != "")
            {
                strclass.Append(":" + IClass);
            }
            strclass.AppendLine("");
            strclass.AppendSpaceLine(1, "{");
                      
            #region  ��������

            if (ModelTranList.Count == 0)
                return "";


            //ѭ��ÿ����,�õ�����
            StringPlus mlist = new StringPlus();
            foreach (ModelTran mt in ModelTranList)
            {
                if (mt.Action.ToLower() == "delete")
                {
                    foreach (ColumnInfo key in mt.Keys)
                    {
                        if (key.IsPrimaryKey || !key.IsIdentity)
                        {
                            mlist.Append(CodeCommon.DbTypeToCS(key.TypeName) + " " + key.ColumnName + ",");
                        }
                    }                    
                }
                else
                {
                    mlist.Append(ModelSpace(mt.ModelName) + " model" + mt.ModelName + ",");
                }
            }
            mlist.DelLastComma();


            strclass.AppendSpaceLine(2, "public void TranMethod(" + mlist.Value + " )");            
            strclass.AppendSpaceLine(2, "{");
            strclass.AppendSpaceLine(3, "List<CommandInfo> cmdlist = new List<CommandInfo>();");
            strclass.AppendLine();
            int n = 1;
            //ѭ��ÿ���������
            foreach (ModelTran mt in ModelTranList)
            {
                switch(mt.Action.ToLower())
                {
                    case "insert":
                        {
                            strclass.Append(CreatAdd(mt.TableName,mt.ModelName, mt.Fieldlist, n));
                            strclass.AppendSpaceLine(3, "CommandInfo cmd" + n + " = new CommandInfo(strSql" + n + ".ToString(), parameters" + n + ");");
                            strclass.AppendSpaceLine(3, "cmdlist.Add(cmd" + n + ");");
                            strclass.AppendLine();

                        }
                        break;
                    case "update":
                        {
                            strclass.Append(CreatUpdate(mt.TableName, mt.ModelName, mt.Fieldlist, mt.Keys, n));
                            strclass.AppendSpaceLine(3, "CommandInfo cmd" + n + " = new CommandInfo(strSql" + n + ".ToString(), parameters" + n + ");");
                            strclass.AppendSpaceLine(3, "cmdlist.Add(cmd" + n + ");");
                            strclass.AppendLine();
                        }
                        break;
                    case "delete":
                        {
                            strclass.Append(CreatDelete(mt.TableName, mt.ModelName, mt.Fieldlist, mt.Keys, n));
                            strclass.AppendSpaceLine(3, "CommandInfo cmd" + n + " = new CommandInfo(strSql" + n + ".ToString(), parameters" + n + ");");
                            strclass.AppendSpaceLine(3, "cmdlist.Add(cmd" + n + ");");
                            strclass.AppendLine();
                        }
                        break;
                }
                n++;
            }
            strclass.AppendSpaceLine(3, "DbHelperSQL.ExecuteSqlTran(cmdlist);");
            strclass.AppendSpaceLine(2, "}");
            
            #endregion
                        
            strclass.AppendSpaceLine(1, "}");
            strclass.AppendLine("}");
            strclass.AppendLine("");
            return strclass.ToString();
        }

        #endregion

        #region ���ݲ�(ʹ��Parameterʵ��)

        

        /// <summary>
        /// �õ�Add()�Ĵ���
        /// </summary>        
        public string CreatAdd(string tabName,string ModelName, List<ColumnInfo> Fieldlist,int num)
        {            
            StringPlus strclass = new StringPlus();
            StringPlus strclass1 = new StringPlus();
            StringPlus strclass2 = new StringPlus();
            StringPlus strclass3 = new StringPlus();
            StringPlus strclass4 = new StringPlus();
           
            strclass.AppendSpaceLine(3, "StringBuilder strSql" + num + "=new StringBuilder();");
            strclass.AppendSpaceLine(3, "strSql" + num + ".Append(\"insert into " + tabName + "(\");");
            strclass1.AppendSpace(3, "strSql" + num + ".Append(\"");
            int n = 0;
            foreach (ColumnInfo field in Fieldlist)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                bool IsIdentity = field.IsIdentity;
                string Length = field.Length;
                bool nullable = field.Nullable;

                if (field.IsIdentity)
                {
                    continue;
                }
                strclass3.AppendSpaceLine(5, "new " + DbParaHead + "Parameter(\"" + preParameter + columnName + "\", " + DbParaDbType + "." + CodeCommon.DbTypeLength(dbobj.DbType, columnType, Length) + "),");
                strclass1.Append(columnName + ",");
                strclass2.Append(preParameter + columnName + ",");
                if ("uniqueidentifier" == columnType.ToLower())
                {
                    strclass4.AppendSpaceLine(3, "parameters" + num + "[" + n + "].Value = Guid.NewGuid();");
                }
                else
                {
                    strclass4.AppendSpaceLine(3, "parameters" + num + "[" + n + "].Value = model" + ModelName + "." + columnName + ";");
                }
                n++;
            }

            //ȥ�����Ķ���
            strclass1.DelLastComma();
            strclass2.DelLastComma();
            strclass3.DelLastComma();
            strclass1.AppendLine(")\");");
            strclass.Append(strclass1.ToString());
            strclass.AppendSpaceLine(3, "strSql.Append(\" values (\");");
            strclass.AppendSpaceLine(3, "strSql.Append(\"" + strclass2.ToString() + ")\");");
            strclass.AppendSpaceLine(3, "" + DbParaHead + "Parameter[] parameters" + num + " = {");
            strclass.Append(strclass3.Value);
            strclass.AppendLine("};");
            strclass.AppendLine(strclass4.Value);
                        
            return strclass.ToString();
        }

        /// <summary>
        /// �õ�Update�����Ĵ���
        /// </summary>   
        public string CreatUpdate(string tabName, string ModelName, List<ColumnInfo> Fieldlist, List<ColumnInfo> Keys, int num)
        {           
            StringPlus strclass = new StringPlus();
            StringPlus strclass1 = new StringPlus();
            StringPlus strclass2 = new StringPlus();
            StringPlus strclass3 = new StringPlus();
            
            strclass.AppendSpaceLine(3, "StringBuilder strSql" + num + "=new StringBuilder();");
            strclass.AppendSpaceLine(3, "strSql" + num + ".Append(\"update " + tabName + " set \");");
            int n = 0;

            if (Fieldlist.Count == 0)
            {
                Fieldlist = Keys;
            }

            //�������ֶ���䣬��ʱ����
            List<ColumnInfo> fieldpk = new List<ColumnInfo>();

            foreach (ColumnInfo field in Fieldlist)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                string Length = field.Length;
                bool IsIdentity = field.IsIdentity;
                bool isPK = field.IsPrimaryKey;

                if (field.IsIdentity || field.IsPrimaryKey || (Keys.Contains(field)))
                {
                    fieldpk.Add(field);
                    continue;
                }
                if (columnType.ToLower() == "timestamp")
                {
                    continue;
                }
                strclass1.AppendSpaceLine(5, "new " + DbParaHead + "Parameter(\"" + preParameter + columnName + "\", " + DbParaDbType + "." + CodeCommon.DbTypeLength(dbobj.DbType, columnType, Length) + "),");
                strclass2.AppendSpaceLine(3, "parameters" + num + "[" + n + "].Value = model." + columnName + ";");
                n++;

                strclass3.AppendSpaceLine(3, "strSql" + num + ".Append(\"" + columnName + "=" + preParameter + columnName + ",\");");
            }
            foreach (ColumnInfo field in fieldpk)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                string Length = field.Length;
                bool IsIdentity = field.IsIdentity;
                bool isPK = field.IsPrimaryKey;

                strclass1.AppendSpaceLine(5, "new " + DbParaHead + "Parameter(\"" + preParameter + columnName + "\", " + DbParaDbType + "." + CodeCommon.DbTypeLength(dbobj.DbType, columnType, Length) + "),");
                strclass2.AppendSpaceLine(3, "parameters" + num + "[" + n + "].Value = model" + ModelName + "." + columnName + ";");
                n++;
            }


            if (strclass3.Value.Length > 0)
            {
                //ȥ�����Ķ���			
                strclass3.DelLastComma();
                strclass3.AppendLine("\");");
            }
            else
            {
                strclass3.AppendLine("#warning ϵͳ����ȱ�ٸ��µ��ֶΣ����ֹ�ȷ����˸����Ƿ���ȷ�� ");
                foreach (ColumnInfo field in Fieldlist)
                {
                    string columnName = field.ColumnName;
                    strclass3.AppendSpaceLine(3, "strSql" + num + ".Append(\"" + columnName + "=" + preParameter + columnName + ",\");");
                }
                if (Fieldlist.Count > 0)
                {
                    strclass3.DelLastComma();
                    strclass3.AppendLine("\");");
                }
            }

            strclass.Append(strclass3.Value);
            strclass.AppendSpaceLine(3, "strSql" + num + ".Append(\" where " + CodeCommon.GetWhereParameterExpression(Keys, true, dbobj.DbType) + "\");");

            strclass.AppendSpaceLine(3, "" + DbParaHead + "Parameter[] parameters" + num + " = {");
            strclass1.DelLastComma();
            strclass.Append(strclass1.Value);
            strclass.AppendLine("};");
            strclass.AppendLine(strclass2.Value);
            
            return strclass.ToString();
        }
                
        /// <summary>
        /// �õ�Delete�Ĵ���
        /// </summary>       
        public string CreatDelete(string tabName, string ModelName, List<ColumnInfo> Fieldlist, List<ColumnInfo> Keys, int num)
        {
            StringPlus strclass = new StringPlus();

            #region ��ʶ�ֶ�����-ɾ��

            strclass.AppendSpaceLine(3, "StringBuilder strSql" + num + "=new StringBuilder();");
            strclass.AppendSpaceLine(3, "strSql" + num + ".Append(\"delete from " + tabName + " \");");
            strclass.AppendSpaceLine(3, "strSql" + num + ".Append(\" where " + CodeCommon.GetWhereParameterExpression(Keys, true, dbobj.DbType) + "\");");
            strclass.AppendLine(CodeCommon.GetPreParameter(Keys, true, dbobj.DbType));

            #endregion

            #region �����������ȵ�ɾ��(���б�ʶ�ֶΣ����зǱ�ʶ�����ֶ�)

            if ((Maticsoft.CodeHelper.CodeCommon.HasNoIdentityKey(Keys)) && (Maticsoft.CodeHelper.CodeCommon.GetIdentityKey(Keys) != null))
            {

                strclass.AppendSpaceLine(3, "StringBuilder strSql" + num + "=new StringBuilder();");
                strclass.AppendSpaceLine(3, "strSql" + num + ".Append(\"delete from " + tabName + " \");");
                strclass.AppendSpaceLine(3, "strSql" + num + ".Append(\" where " + CodeCommon.GetWhereParameterExpression(Keys, false, dbobj.DbType) + "\");");
                strclass.AppendLine(CodeCommon.GetPreParameter(Keys, false, dbobj.DbType));
                                                
            }

            #endregion

            #region ����ɾ������

            //string keyField = "";
            //if (Keys.Count == 1)
            //{
            //    keyField = Keys[0].ColumnName;
            //}
            //else
            //{
            //    foreach (ColumnInfo field in Keys)
            //    {
            //        if (field.IsIdentity)
            //        {
            //            keyField = field.ColumnName;
            //            break;
            //        }
            //    }
            //}
            //if (keyField.Trim().Length > 0)
            //{                                
            //    strclass.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
            //    strclass.AppendSpaceLine(3, "strSql.Append(\"delete from " + tabName + " \");");
            //    strclass.AppendSpaceLine(3, "strSql.Append(\" where " + keyField + " in (\"+" + keyField + "list + \")  \");");               

            //}

            #endregion
            
            return strclass.Value;

        }

              

        #endregion
                
    }
}
