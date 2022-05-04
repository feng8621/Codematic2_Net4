using System;
using System.Collections.Generic;
using System.Text;
using Maticsoft.IDBO;
using Maticsoft.CodeHelper;
namespace Maticsoft.IBuilder
{
    /// <summary>
    ///Web����빹�����ӿ�
    /// </summary>
    public interface IBuilderWeb
    {

        #region ��������
        
        /// <summary>
        /// ���������ռ��� 
        /// </summary>
        string NameSpace
        {
            set;
            get;
        }
        string Folder
        {
            set;
            get;
        }        
        /// <summary>
        /// model����
        /// </summary>
        string ModelName
        {
            set;
            get;
        }
        string BLLName
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
        /// <summary>
        /// �����������ֶ��б� 
        /// </summary>
        List<ColumnInfo> Keys
        {
            set;
            get;
        }
        #endregion

        #region Aspxҳ��html

        /// <summary>
        /// �õ���ʾ�����Ӵ����html����
        /// </summary>      
        string GetAddAspx();        
        /// <summary>
        /// �õ���ʾ�����Ӵ����html����
        /// </summary>      
        string GetUpdateAspx();
        /// <summary>
        /// �õ���ʾ����ʾ�����html����
        /// </summary>     
        string GetShowAspx();
        /// <summary>
        /// �õ���ʾ���б����html����
        /// </summary>     
        string GetListAspx();
        /// <summary>
        /// ��ɾ��3��ҳ�����
        /// </summary>      
        string GetWebHtmlCode(bool ExistsKey, bool AddForm, bool UpdateForm, bool ShowForm, bool SearchForm);
       
        #endregion

        #region ��ʾ�� CS
        /// <summary>
        /// �õ���ʾ�����Ӵ���Ĵ���
        /// </summary>      
        string GetAddAspxCs();
        /// <summary>
        /// �õ��޸Ĵ���Ĵ���
        /// </summary>      
        string GetUpdateAspxCs();        
        /// <summary>
        /// �õ��޸Ĵ���Ĵ���
        /// </summary>       
        string GetUpdateShowAspxCs();
        /// <summary>
        /// �õ���ʾ����ʾ����Ĵ���
        /// </summary>       
        string GetShowAspxCs();
        /// <summary>
        /// �õ���ʾ��ɾ��ҳ��Ĵ���
        /// </summary>       
        string GetDeleteAspxCs();
        /// <summary>
        /// �õ���ʾ����ʾ����Ĵ���
        /// </summary>       
        string GetListAspxCs();
        /// <summary>
        /// ɾ��ҳ��
        /// </summary>
        /// <returns></returns>
        //string CreatDeleteForm();        
        string CreatSearchForm();        
        string GetWebCode(bool ExistsKey, bool AddForm, bool UpdateForm, bool ShowForm, bool SearchForm);
       
        #endregion//��ʾ��

        #region  ����aspx.designer.cs
        /// <summary>
        /// �õ���ʾ�����Ӵ����html����
        /// </summary>      
        string GetAddDesigner(); 
        /// <summary>
        /// �õ���ʾ�����Ӵ����html����
        /// </summary>      
        string GetUpdateDesigner(); 
        /// <summary>
        /// �õ���ʾ����ʾ�����html����
        /// </summary>     
        string GetShowDesigner();
        /// <summary>
        /// �õ���ʾ���б����html����
        /// </summary>     
        string GetListDesigner();   
        #endregion
    }
}
