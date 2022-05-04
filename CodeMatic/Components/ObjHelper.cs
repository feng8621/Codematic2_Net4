using System;
using System.Collections.Generic;
using System.Text;

namespace Codematic
{
    /// <summary>
    /// ����ӿڴ���������
    /// </summary>
    class ObjHelper
    {

        public ObjHelper()
        {
        }

        //�������ݿ���Ϣ��ӿ�
        public static Maticsoft.IDBO.IDbObject CreatDbObj(string longservername)
        {
            Maticsoft.CmConfig.DbSettings dbset = Maticsoft.CmConfig.DbConfig.GetSetting(longservername);
            Maticsoft.IDBO.IDbObject dbobj = Maticsoft.DBFactory.DBOMaker.CreateDbObj(dbset.DbType);
            dbobj.DbConnectStr = dbset.ConnectStr;            
            return dbobj;
        }
    
        //�����ű��ӿ�
        public static Maticsoft.IDBO.IDbScriptBuilder CreatDsb(string longservername)
        {
            Maticsoft.CmConfig.DbSettings dbset = Maticsoft.CmConfig.DbConfig.GetSetting(longservername);
            Maticsoft.IDBO.IDbScriptBuilder dsb = Maticsoft.DBFactory.DBOMaker.CreateScript(dbset.DbType);
            dsb.DbConnectStr = dbset.ConnectStr;
            return dsb;
        }

        //��������������ӿ�
        public static Maticsoft.CodeBuild.CodeBuilders CreatCB(string longservername)
        {
            //Maticsoft.CmConfig.DbSettings dbset = Maticsoft.CmConfig.DbConfig.GetSetting(longservername);
            Maticsoft.CodeBuild.CodeBuilders cb = new Maticsoft.CodeBuild.CodeBuilders(CreatDbObj(longservername));// Maticsoft.CodeBuild.CodeBuilders(dbset.DbType);
            //cb.DbConnectStr = dbset.ConnectStr;
            return cb;
        }
    }

     
}
