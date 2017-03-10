using UnityEngine;
using System.Collections.Generic;
namespace Pal
{
	public class TestDataGUI : MonoBehaviourEx {

        private List<TestAttributeShow> _list = new List<TestAttributeShow>();
        private GUIStyle _style = new GUIStyle();
		public override void Start () {
				base.Start();
            _style.fontSize = 20;
		
		}
        public override void OnEnable()
        {
            _list.Clear();
            MonoBehaviour[] mono = GetComponents<MonoBehaviour>();
            for(int i=0,imax=mono.Length;i<imax;++i)
            {
                if(mono[i].isActiveAndEnabled)
                {
                    TestAttributeShow show = new TestAttributeShow(mono[i]);
                    if (show.propertyCount > 0)
                    {
                        _list.Add(show);
                    }
                }
            }
        }
        public override void OnDisable()
        {
            _list.Clear();
        }
        public void OnGUI()
        {
            for(int i=0,imax=_list.Count;i<imax;++i)
            {

                _list[i].DrawPropertyOnGUI(_style);
            }
        }
    }
}
