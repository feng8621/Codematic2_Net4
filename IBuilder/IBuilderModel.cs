using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Maticsoft.IDBO;
using Maticsoft.CodeHelper;
namespace Maticsoft.IBuilder
{
    /// <summary>
    /// Model���빹�����ӿ�
    /// </summary>
    public interface IBuilderModel
    {
        #region ��������   
        /// <summary>
        /// model����
        /// </summary>
        string ModelName
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
        /// ʵ����������ռ�
        /// </summary>
        string Modelpath
        {
            set;
            get;
        }
        /// <summary>
        /// ѡ����ֶμ���
        /// </summary>
        List<ColumnInfo> Fieldlist
        {
            set;
            get;
        }

        ///// <summary>
        ///// ��������Դ�б��뽫�ļ�����ָ�����ļ���
        ///// </summary>
        //Hashtable Languagelist
        //{            
        //    get;
        //}

        #endregion
        
        #region ������������Model��
        /// <summary>
        /// ������������Model��
        /// </summary>		
        string CreatModel();       
        #endregion

        #region ����Model���Բ���
        /// <summary>
        /// ����ʵ���������
        /// </summary>
        /// <returns></returns>
        string CreatModelMethod();      
        #endregion
    }
}
