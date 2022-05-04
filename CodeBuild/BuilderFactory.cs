using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using Maticsoft.AddInManager;
using Maticsoft.Utility;
using System.Data;
namespace Maticsoft.CodeBuild
{
    /// <summary>
    /// �������ɶ��� ����
    /// </summary>
    public class BuilderFactory
    {
        private static Cache cache = new Cache();

        #region ���򼯷���

        private static object CreateObject(string path, string TypeName)
        {
            object obj = cache.GetObject(TypeName);
            if (obj == null)
            {
                try
                {
                    obj = Assembly.Load(path).CreateInstance(TypeName,true);
                    cache.SaveCache(TypeName, obj);// д�뻺��
                }
                catch (System.Exception ex)
                {
                    string str = ex.Message;// ��¼������־
                }
            }
            return obj;
        }
        #endregion

        #region �������ݷ��ʲ� �������ɶ���

        /// <summary>
        /// �������ݷ��ʲ� �������ɶ���
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Maticsoft.IBuilder.IBuilderDAL CreateDALObj(string AssemblyGuid)
        {
            try
            {
                if (AssemblyGuid == "")
                {
                    return null;
                }
                AddIn addin = new AddIn(AssemblyGuid);                
                string Assembly = addin.Assembly;
                string Classname = addin.Classname;

                object objType = CreateObject(Assembly, Classname);
                return (Maticsoft.IBuilder.IBuilderDAL)objType;
            }
            catch (SystemException ex)
            {
                string err = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// �������ݷ��ʲ� �������ɶ���
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Maticsoft.IBuilder.IBuilderDALTran CreateDALTranObj(string AssemblyGuid)
        {
            try
            {
                if (AssemblyGuid == "")
                {
                    return null;
                }
                AddIn addin = new AddIn(AssemblyGuid);
                string Assembly = addin.Assembly;
                string Classname = addin.Classname;

                object objType = CreateObject(Assembly, Classname);
                return (Maticsoft.IBuilder.IBuilderDALTran)objType;
            }
            catch (SystemException ex)
            {
                string err = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// �������ݷ��ʲ� �������ɶ���
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Maticsoft.IBuilder.IBuilderDALMTran CreateDALMTranObj(string AssemblyGuid)
        {
            try
            {
                if (AssemblyGuid == "")
                {
                    return null;
                }
                AddIn addin = new AddIn(AssemblyGuid);
                string Assembly = addin.Assembly;
                string Classname = addin.Classname;

                object objType = CreateObject(Assembly, Classname);
                return (Maticsoft.IBuilder.IBuilderDALMTran)objType;
            }
            catch (SystemException ex)
            {
                string err = ex.Message;
                return null;
            }
        }
        #endregion

        #region ���ؽӿڲ� �������ɶ���

        /// <summary>
        /// �����ӿڲ� �������ɶ���
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Maticsoft.IBuilder.IBuilderIDAL CreateIDALObj()
        {
            try
            {
                object objType = CreateObject("Maticsoft.BuilderIDAL", "Maticsoft.BuilderIDAL.BuilderIDAL");
                return (Maticsoft.IBuilder.IBuilderIDAL)objType;
            }
            catch (SystemException ex)
            {
                string err = ex.Message;
                return null;
            }
        }


        #endregion

        #region ����ҵ��� �������ɶ���

        /// <summary>
        /// ����ҵ��� �������ɶ���
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Maticsoft.IBuilder.IBuilderBLL CreateBLLObj(string AssemblyGuid)
        {
            try
            {
                if (AssemblyGuid == "")
                {
                    return null;
                }
                AddIn addin = new AddIn(AssemblyGuid);
                string Assembly = addin.Assembly;
                string Classname = addin.Classname;

                object objType = CreateObject(Assembly, Classname);
                return (Maticsoft.IBuilder.IBuilderBLL)objType;
            }
            catch (SystemException ex)
            {
                string err = ex.Message;
                return null;
            }
        }
  

        #endregion

        #region ����Model�� �������ɶ���

        /// <summary>
        /// ����ҵ��� �������ɶ���
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Maticsoft.IBuilder.IBuilderModel CreateModelObj(string AssemblyGuid)
        {
            try
            {
                if (AssemblyGuid == "")
                {
                    return null;
                }
                AddIn addin = new AddIn(AssemblyGuid);
                string Assembly = addin.Assembly;
                string Classname = addin.Classname;

                object objType = CreateObject(Assembly, Classname);
                return (Maticsoft.IBuilder.IBuilderModel)objType;
            }
            catch (SystemException ex)
            {
                string err = ex.Message;
                return null;
            }
        }


        #endregion

        #region ����WEB�� �������ɶ���

        /// <summary>
        /// ����ҵ��� �������ɶ���
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Maticsoft.IBuilder.IBuilderWeb CreateWebObj(string AssemblyGuid)
        {
            try
            {
                if (AssemblyGuid == "")
                {
                    return null;
                }
                AddIn addin = new AddIn(AssemblyGuid);
                string Assembly = addin.Assembly;
                string Classname = addin.Classname;

                object objType = CreateObject(Assembly, Classname);
                return (Maticsoft.IBuilder.IBuilderWeb)objType;
            }
            catch (SystemException ex)
            {
                string err = ex.Message;
                return null;
            }
        }


        #endregion


    }
}
