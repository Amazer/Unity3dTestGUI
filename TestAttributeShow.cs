using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Reflection;
namespace Pal
{
	public class TestAttributeShow  {
        private UnityEngine.Object _obj;
        private List<FieldInfo> infoslist=new List<FieldInfo>();
        public TestAttributeShow(UnityEngine.Object obj)
        {
            _obj=obj;
            FieldInfo[] infos = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            for(int i=0,imax=infos.Length;i<imax;++i)
            {
                FieldInfo info = infos[i];
                if(info.IsDefined(typeof(TestDisplayAttribute),true))
                {
                    infoslist.Add(info);
                }
            }
        }
        public void Show()
        {
            for(int i=0,imax=infoslist.Count;i<imax;++i)
            {
                FieldInfo info = infoslist[i];
                if(info.IsDefined(typeof(TestDisplayAttribute),true))
                {
                    object infoValue = info.GetValue(_obj);
                    Debug.RedLog("desplayAttr:"+info.Name);
                    Debug.RedLog("value:"+GetValueStr(infoValue));
                }
            }
        }
        public string GetValueStr(object obj)
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
