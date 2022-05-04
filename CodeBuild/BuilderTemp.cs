using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;
using System.Data;
using System.IO;
using System.Xml.Xsl;
using Maticsoft.IDBO;
using Maticsoft.Utility;
using Maticsoft.CodeHelper;
using Maticsoft.CodeEngine;

namespace Maticsoft.CodeBuild
{
    /// <summary>
    /// ģ���������
    /// </summary>
    public class BuilderTemp
    {
        protected IDbObject dbobj;
        protected Maticsoft.CmConfig.DbSettings dbset;

        #region ����
        private string _dbname;
        private string _tablename;
        private string _tabledescription = "";
        private string TemplateFile = "";        
        private List<ColumnInfo> _keys; //�����������ֶ��б�
        private List<ColumnInfo> _fkeys; //�����������ֶ��б�
        private List<ColumnInfo> _fieldlist;
        private string _objtype;

        public string DbName
        {
            set { _dbname = value; }
            get { return _dbname; }
        }
        public string TableName
        {
            set { _tablename = value; }
            get { return _tablename; }
        }
        /// <summary>
        /// ���������Ϣ
        /// </summary>
        public string TableDescription
        {
            set { _tabledescription = value; }
            get { return _tabledescription; }
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
        public List<ColumnInfo> FKeys
        {
            set { _fkeys = value; }
            get { return _fkeys; }
        }
        /// <summary>
        /// ѡ����ֶμ����ַ���
        /// </summary>
        public string Fields
        {
            get
            {
                StringPlus _fields = new StringPlus();
                foreach (object obj in Fieldlist)
                {
                    _fields.Append("'" + obj.ToString() + "',");
                }
                _fields.DelLastComma();
                return _fields.Value;
            }
        }

        /// <summary>
        /// �������ͣ�table,view,proc
        /// </summary>
        public string ObjectType
        {
            set { _objtype = value; }
            get { return _objtype; }
        }

        #endregion

        public BuilderTemp(IDbObject idbobj, string dbName, string tableName, string tableDescription,
            List<ColumnInfo> fieldlist, List<ColumnInfo> keys, List<ColumnInfo> fkeys, string templateFile,
            Maticsoft.CmConfig.DbSettings dbSet,string objectype)
        {
            dbobj = idbobj;
            DbName = dbName;
            TableName = tableName;
            TableDescription = tableDescription;            
            TemplateFile = templateFile;
            Fieldlist = fieldlist;
            Keys = keys;
            FKeys = fkeys;
            dbset = dbSet;
            _objtype = objectype;
            
        }

        #region  ����ģ��õ����ɵĴ���GetCode

        /// <summary>
        /// ����ģ��õ����ɵĴ���
        /// </summary>
        /// <returns></returns>
        public CodeInfo GetCode()
        {
            CodeInfo codeinfo = new CodeInfo();
            if ((TemplateFile == null) || (!File.Exists(TemplateFile)))
            {
                codeinfo.ErrorMsg= "ģ���ļ������ڣ�";
                return codeinfo;
            }            
                        
            string TemplateContent = File.ReadAllText(TemplateFile);
            CodeGenerator codegenerat = new CodeGenerator();
            if (ObjectType == "proc")
            {
                ProcedureHost host = new ProcedureHost();
                host.TableList = dbobj.GetTablesInfo(DbName);
                host.ViewList = dbobj.GetVIEWsInfo(DbName);
                host.ProcedureList = dbobj.GetProcInfo(DbName);
                host.TemplateFile = TemplateFile;
                host.DbName = DbName;
                host.TableName = TableName;
                host.TableDescription = TableDescription;
                host.NameSpace = dbset.Namepace;
                host.Folder = dbset.Folder;
                host.DbHelperName = dbset.DbHelperName;
                host.Fieldlist = Fieldlist;
                host.Keys = Keys;
                host.FKeys = FKeys;

                //��������           
                host.DbSet = dbset;
                host.BLLPrefix = dbset.BLLPrefix;
                host.BLLSuffix = dbset.BLLSuffix;
                host.DALPrefix = dbset.DALPrefix;
                host.DALSuffix = dbset.DALSuffix;
                host.DbType = dbset.DbType;
                host.ModelPrefix = dbset.ModelPrefix;
                host.ModelSuffix = dbset.ModelSuffix;
                host.ProcPrefix = dbset.ProcPrefix;
                host.ProjectName = dbset.ProjectName;
                host.TabNameRule = dbset.TabNameRule;
                codeinfo = codegenerat.GenerateCode(TemplateContent, host);
            }
            else
            {
                TableHost host = new TableHost();
                host.TableList = dbobj.GetTablesInfo(DbName);
                host.ViewList = dbobj.GetVIEWsInfo(DbName);
                host.ProcedureList = dbobj.GetProcInfo(DbName);
                host.TemplateFile = TemplateFile;
                host.DbName = DbName;
                host.TableName = TableName;
                host.TableDescription = TableDescription;
                host.NameSpace = dbset.Namepace;
                host.Folder = dbset.Folder;
                host.DbHelperName = dbset.DbHelperName;
                host.Fieldlist = Fieldlist;
                host.Keys = Keys;
                host.FKeys = FKeys;

                //��������        
                host.DbSet = dbset;
                host.BLLPrefix = dbset.BLLPrefix;
                host.BLLSuffix = dbset.BLLSuffix;
                host.DALPrefix = dbset.DALPrefix;
                host.DALSuffix = dbset.DALSuffix;
                host.DbType = dbset.DbType;
                host.ModelPrefix = dbset.ModelPrefix;
                host.ModelSuffix = dbset.ModelSuffix;
                host.ProcPrefix = dbset.ProcPrefix;
                host.ProjectName = dbset.ProjectName;
                host.TabNameRule = dbset.TabNameRule;
                codeinfo = codegenerat.GenerateCode(TemplateContent, host);
            }
            return codeinfo;
        }
        
        ///// <summary>
        ///// ����ģ��õ����ɵĴ���
        ///// </summary>
        ///// <returns></returns>
        //public string GetCode()
        //{            
        //    System.IO.StringWriter stringWriter = new System.IO.StringWriter();
        //    if (Fieldlist.Count>0)
        //    {                                 
        //        //XslCompiledTransform xslt = new XslCompiledTransform();
        //        XslTransform xslt = new XslTransform();

        //        //StreamReader srFile = new StreamReader(strXslt, Encoding.Default);
        //        //XmlTextReader x = new XmlTextReader(srFile);
        //        //xslt.Load(x);

        //        xslt.Load(strTemplate);
                
        //        //XmlTextReader xtr = new XmlTextReader(GetXml(dtrows));
        //        //XmlTextWriter xtw = new XmlTextWriter();
        //        xslt.Transform(GetXml2(), null, stringWriter, null);
        //    }            
        //    return stringWriter.ToString();
        //}


        ///// <summary>
        ///// ����ģ��õ����ɵĴ���
        ///// </summary>
        ///// <returns></returns>
        //public string GetCode()
        //{
        //    DataTable dt = dbobj.GetColumnInfoList(DbName, TableName);
        //    System.IO.StringWriter stringWriter = new System.IO.StringWriter();
        //    if (dt != null)
        //    {
        //        DataRow[] dtrows;
        //        if (Fieldlist.Count > 0)
        //        {
        //            dtrows = dt.Select("ColumnName in (" + Fields + ")", "colorder asc");
        //        }
        //        else
        //        {
        //            dtrows = dt.Select();
        //        }

        //        //XslCompiledTransform xslt = new XslCompiledTransform();
        //        XslTransform xslt = new XslTransform();

        //        //StreamReader srFile = new StreamReader(strXslt, Encoding.Default);
        //        //XmlTextReader x = new XmlTextReader(srFile);
        //        //xslt.Load(x);

        //        xslt.Load(strXslt);

        //        //XmlTextReader xtr = new XmlTextReader(GetXml(dtrows));
        //        //XmlTextWriter xtw = new XmlTextWriter();
        //        xslt.Transform(GetXml2(dtrows), null, stringWriter, null);
        //    }
        //    return stringWriter.ToString();
        //}


        #endregion

        #region �õ�XML ����

        /// <summary>
        /// �õ����ݵ�xml
        /// </summary>
        /// <returns></returns>
        private XmlDocument GetXml(DataRow[] dtrows)
        {
            //string xmlDoc = "temp.xml";
            Stream w = new System.IO.MemoryStream(); 

            XmlTextWriter writer = new XmlTextWriter(w, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument(true);
            writer.WriteStartElement("Schema");

            writer.WriteStartElement("TableName");
            writer.WriteAttributeString("value", "Authors");
            writer.WriteEndElement();

            writer.WriteStartElement("FIELDS");

            foreach (DataRow row in dtrows)
            {
                string columnName = row["ColumnName"].ToString();
                string columnType = row["TypeName"].ToString();
                //string IsIdentity = row["IsIdentity"].ToString();

                writer.WriteStartElement("FIELD");
                writer.WriteAttributeString("Name", columnName);
                writer.WriteAttributeString("Type", columnType);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Flush();
            writer.Close();

            //return w;           
            TextReader stringReader = new StringReader(writer.ToString());
            XmlDocument xml = new XmlDocument();
            xml.Load(stringReader);
            return xml;


        }
        /// <summary>
        /// �õ����ݵ�xml
        /// </summary>
        /// <returns></returns>
        private XmlDocument GetXml2()
        {
            string xmlDoc = @"Template\temp.xml"; //��ʱ�����ļ�temp.xml

            XmlTextWriter writer = new XmlTextWriter(xmlDoc, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument(true);
            writer.WriteStartElement("Schema");

            writer.WriteStartElement("TableName");
            writer.WriteAttributeString("value", TableName);
            writer.WriteEndElement();

            //�ֶ�����
            writer.WriteStartElement("FIELDS");
            foreach (ColumnInfo field in Fieldlist)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                string Length = field.Length;
                bool IsIdentity = field.IsIdentity;
                bool isPK = field.IsPrimaryKey;
                string deText = field.Description;
                string defaultVal = field.DefaultVal;

                writer.WriteStartElement("FIELD");                
                writer.WriteAttributeString("Name", columnName);
                writer.WriteAttributeString("Type", CodeCommon.DbTypeToCS(columnType));
                writer.WriteAttributeString("Desc", deText);
                writer.WriteAttributeString("defaultVal", defaultVal);                
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            //�����ֶ�
            writer.WriteStartElement("PrimaryKeys");
            foreach (ColumnInfo field in Keys)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                string Length = field.Length;
                bool IsIdentity = field.IsIdentity;
                bool isPK = field.IsPrimaryKey;
                string deText = field.Description;
                string defaultVal = field.DefaultVal;

                writer.WriteStartElement("FIELD");                
                writer.WriteAttributeString("Name", columnName);
                writer.WriteAttributeString("Type", CodeCommon.DbTypeToCS(columnType));
                writer.WriteAttributeString("Desc", deText);
                writer.WriteAttributeString("defaultVal", defaultVal);                
                writer.WriteEndElement();
            }           
            writer.WriteEndElement();
            

            writer.WriteEndElement();
            writer.Flush();
            writer.Close();
                      
           
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlDoc);
            return xml;


        }

        #endregion


    }
}
