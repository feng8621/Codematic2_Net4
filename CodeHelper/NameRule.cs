using System;
using System.Collections.Generic;
using System.Text;

namespace Maticsoft.CodeHelper
{
    /// <summary>
    /// ����������
    /// </summary>
    public class NameRule
    {      

        #region 

        /// <summary>
        /// ��ϵõ�Model����
        /// </summary>
        /// <param name="TabName">����</param>
        /// <returns></returns>
        public static string GetModelClass(string TabName, Maticsoft.CmConfig.DbSettings dbset)
        {
            return dbset.ModelPrefix + TabNameRuled(TabName, dbset) + dbset.ModelSuffix;
        }
        /// <summary>
        /// ��ϵõ�BLL����
        /// </summary>
        /// <param name="TabName"></param>
        /// <returns></returns>
        public static string GetBLLClass(string TabName, Maticsoft.CmConfig.DbSettings dbset)
        {
            return dbset.BLLPrefix + TabNameRuled(TabName, dbset) + dbset.BLLSuffix;
        }

        /// <summary>
        /// ��ϵõ�DAL����
        /// </summary>
        /// <param name="TabName"></param>
        /// <returns></returns>
        public static string GetDALClass(string TabName, Maticsoft.CmConfig.DbSettings dbset)
        {
            return dbset.DALPrefix + TabNameRuled(TabName, dbset) + dbset.DALSuffix;
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="TabName"></param>
        /// <param name="dbset"></param>
        /// <returns></returns>
        private static string TabNameRuled(string TabName, Maticsoft.CmConfig.DbSettings dbset)
        {
            string newTabName = TabName;
            if (dbset.ReplacedOldStr.Length > 0)
            {
                newTabName = newTabName.Replace(dbset.ReplacedOldStr, dbset.ReplacedNewStr);
            }
            switch (dbset.TabNameRule.ToLower())
            {
                case "lower":
                    newTabName = newTabName.ToLower();
                    break;
                case "upper":
                    newTabName = newTabName.ToUpper();
                    break;
                case "firstupper":
                    {
                        string strfir = newTabName.Substring(0, 1).ToUpper();
                        newTabName = strfir + newTabName.Substring(1);
                    }
                    break;
                case "same":
                    break;
                default:
                    break;
            }
            return newTabName;
        }
        #endregion
        

        
    }
}
