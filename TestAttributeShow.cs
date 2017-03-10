using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Reflection;
namespace Pal
{
         
    public class NameValueStr
    {
        private UnityEngine.Object _dataObj;
        private FieldInfo _fieldInfo;
        private string _name;
        private string _strValue;
        public string Name
        {
            get
            {
                return _name;
            }
        }
        public string ValueStr
        {
            get
            {
                Refresh();
                return _strValue;
            }
        }
        public NameValueStr(FieldInfo info,UnityEngine.Object dataObj,string fieldName)
        {
            _dataObj = dataObj;
            _fieldInfo = info;
            _name = fieldName;
        }
        public void Refresh()
        {
            object infoValue = _fieldInfo.GetValue(_dataObj);
            _strValue=TestAttributeShow.GetValueStr(infoValue);
        }
    }
	public class TestAttributeShow  {
        private UnityEngine.Object _obj;
        private List<FieldInfo> _infoslist=new List<FieldInfo>();
        private List<NameValueStr> _nameValStrList = new List<NameValueStr>();
        public int propertyCount
        {
            get
            {
                return _nameValStrList.Count;
            }
        }
        public TestAttributeShow(UnityEngine.Object obj)
        {
            _obj=obj;
            FieldInfo[] infos = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            for(int i=0,imax=infos.Length;i<imax;++i)
            {
                FieldInfo info = infos[i];
                if(info.IsDefined(typeof(TestDisplayAttribute),true))
                {
                    _infoslist.Add(info);
                    NameValueStr nvs = new NameValueStr(info, _obj, info.Name);
                    _nameValStrList.Add(nvs);
                }
            }
        }
        public void DrawPropertyOnGUI(GUIStyle style)
        {
            GUILayout.BeginScrollView(Vector2.zero);
            for(int i=0,imax=_nameValStrList.Count; i<imax;++i)
            {
                NameValueStr str = _nameValStrList[i];
                if(style!=null)
                {
                    GUILayout.Label(str.Name + ":" + str.ValueStr, style);
                }
                else
                {
                    GUILayout.Label(str.Name + ":" + str.ValueStr);
                }
            }
            GUILayout.EndScrollView();
        }
        public void ShowToString()
        {
            for(int i=0,imax=_infoslist.Count;i<imax;++i)
            {
                FieldInfo info = _infoslist[i];
                if(info.IsDefined(typeof(TestDisplayAttribute),true))
                {
                    object infoValue = info.GetValue(_obj);
                    Debug.RedLog("desplayAttr:"+info.Name);
                    Debug.RedLog("value:"+GetValueStr(infoValue));
                }
            }
        }
        public static string GetValueStr(object obj)
        {
            string str = string.Empty;
            if(obj==null)
            {
                str = "null";
            }
            else if(obj is float)
            {
                float v=(float)obj;
                str = v.ToString();
            }
            else if(obj is int)
            {
                int v=(int)obj;
                str = v.ToString();
            }
            else if(obj is bool)
            {
                bool v = (bool)obj;
                str = v.ToString();
            }
            else if(obj is string)
            {
                str = (string)obj;
            }
            else if(obj.GetType().IsEnum)
            {
                str = obj.GetType().Name;
            }
            return str;
        }
	}
}
