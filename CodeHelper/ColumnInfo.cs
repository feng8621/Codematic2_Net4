using System;
using System.Collections.Generic;
using System.Text;

namespace Maticsoft.CodeHelper
{
    /// <summary>
    /// �ֶ���Ϣ
    /// </summary>
    [Serializable]
    public class ColumnInfo
    {
        private string _colorder;
        private string _columnName;
        private string _typeName = "";
        private string _length = "";
        private string _precision = "";
        private string _scale = "";
        private bool _isIdentity;
        private bool _isprimaryKey;
        private bool _isForeignKey;
        private bool _nullable;
        private string _defaultVal = "";
        private string _description = "";

        /// <summary>
        /// ���
        /// </summary>
        public string ColumnOrder
        {
            set { _colorder = value; }
            get { return _colorder; }
        }
        /// <summary>
        /// �ֶ���
        /// </summary>
        public string ColumnName
        {
            set { _columnName = value; }
            get { return _columnName; }
        }
        /// <summary>
        /// �ֶ�����
        /// </summary>
        public string TypeName
        {
            set { _typeName = value; }
            get { return _typeName; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Length
        {
            set { _length = value; }
            get { return _length; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Precision
        {
            set { _precision = value; }
            get { return _precision; }
        }
        /// <summary>
        /// С��λ��
        /// </summary>
        public string Scale
        {
            set { _scale = value; }
            get { return _scale; }
        }
        /// <summary>
        /// �Ƿ��Ǳ�ʶ��
        /// </summary>
        public bool IsIdentity
        {
            set { _isIdentity = value; }
            get { return _isIdentity; }
        }
        /// <summary>
        /// �Ƿ�������
        /// </summary>
        public bool IsPrimaryKey
        {
            set { _isprimaryKey = value; }
            get { return _isprimaryKey; }
        }
        /// <summary>
        /// �Ƿ������
        /// </summary>
        public bool IsForeignKey
        {
            set { _isForeignKey = value; }
            get { return _isForeignKey; }
        }
        /// <summary>
        /// �Ƿ������
        /// </summary>
        public bool Nullable
        {
            set { _nullable = value; }
            get { return _nullable; }
        }
        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public string DefaultVal
        {
            set { _defaultVal = value; }
            get { return _defaultVal; }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }




    }
}
