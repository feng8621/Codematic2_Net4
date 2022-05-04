using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Maticsoft.IDBO;
using Maticsoft.CodeHelper;
namespace Maticsoft.IBuilder
{
    /// <summary>
    /// ���빹�����ӿ�(��������������)
    /// </summary>
    public interface IBuilderDALMTran
    { 

        #region ��������
        IDbObject DbObject
        {
            set;
            get;
        }
        /// <summary>
        /// ����
        /// </summary>
        string DbName
        {
            set;
            get;
        }
        
        List<ModelTran> ModelTranList
        {
            set;
            get;
        }
                        
        /// <summary>
        /// ���������ռ���
        /// </summary>
        string NameSpace
        {
            set;
            get;
        }
        /// <summary>
        /// �����ļ���
        /// </summary>
        string Folder
        {
            set;
            get;
        }

        /// <summary>
        /// ʵ����������ռ�
        /// </summary>
        string Modelpath
        {
            set;
            get;
        }
        
               
        /// <summary>
        /// ���ݲ�������ռ�
        /// </summary>
        string DALpath
        {
            set;
            get;
        }
        

        /// <summary>
        /// �ӿڵ������ռ�
        /// </summary>
        string IDALpath
        {
            set;
            get;
        }
        /// <summary>
        /// �ӿ�����
        /// </summary>
        string IClass
        {
            set;
            get;
        }

        /// <summary>
        /// ���ݿ��������
        /// </summary>
        string DbHelperName
        {
            set;
            get;
        }
       
        #endregion

        string GetDALCode();
              

    }
}
