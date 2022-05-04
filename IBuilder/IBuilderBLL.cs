using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Maticsoft.IDBO;
using Maticsoft.CodeHelper;
namespace Maticsoft.IBuilder
{
    /// <summary>
    /// BLL���빹�����ӿ�
    /// </summary>
    public interface IBuilderBLL
    {
        #region ��������
        /// <summary>
        /// ѡ����ֶμ���
        /// </summary>
        List<ColumnInfo> Fieldlist
        {
            set;
            get;
        }  
        /// <summary>
        /// �����������ֶ��б� 
        /// </summary>
        List<ColumnInfo> Keys
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
        /// �����ļ��У����������ռ���
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
        /// Model����
        /// </summary>
        string ModelName
        {
            set;
            get;
        }
        string TableDescription
        {
            set;
            get;
        }
        /// <summary>
        /// ҵ���߼���������ռ�
        /// </summary>
        string BLLpath
        {
            set;
            get;
        }
        /// <summary>
        /// BLL����
        /// </summary>
        string BLLName
        {
            set;
            get;
        }

        /// <summary>
        /// ������������ռ�
        /// </summary>
        string Factorypath
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
        /// �ӿ���
        /// </summary>
        string IClass
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
        /// DAL����
        /// </summary>
        string DALName
        {
            set;
            get;
        }
        /// <summary>
        /// �Ƿ����Զ�������ʶ��
        /// </summary>
        bool IsHasIdentity
        {
            set;
            get;
        }
        /// <summary>
        /// ���ݿ�����
        /// </summary>
        string DbType
        {
            set;
            get;
        }
        
        #endregion

        string GetBLLCode(bool Maxid, bool Exists, bool Add, bool Update, bool Delete, bool GetModel,bool GetModelByCache, bool List);

        /// <summary>
        /// �õ�GetMaxID()�ķ�������
        /// </summary>
        string CreatBLLGetMaxID();

        /// <summary>
        /// �õ�Exists()�����Ĵ���
        /// </summary>
        string CreatBLLExists();

        /// <summary>
        /// �õ�Add()�Ĵ���
        /// </summary>
        string CreatBLLADD();

        /// <summary>
        /// �õ�Update()�Ĵ���
        /// </summary>        
        string CreatBLLUpdate();

        /// <summary>
        /// �õ�Delete()�Ĵ���
        /// </summary>
        string CreatBLLDelete();

        /// <summary>
        /// �õ�GetModel()�Ĵ���
        /// </summary>
        string CreatBLLGetModel();

        /// <summary>
        /// �õ�GetList()�Ĵ���
        /// </summary> 
        string CreatBLLGetList();
    }
}
