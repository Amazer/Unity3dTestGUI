using UnityEngine;
using System.Collections;
namespace Pal
{
    public enum TestEnum { type1,type2}
	public class TestTDAttribute : MonoBehaviourEx {
        [TestDisplay]
        private float _testFloat ;
        [TestDisplay]
        [SerializeField]
        private int _testInt ;
        [TestDisplay]
        private bool _testbool ;
        [TestDisplay]
        private TestEnum _testEnum ;
        [TestDisplay]
        private string  _testStr ;
        TestAttributeShow show;
		public override void Start () {
            base.Start();
            show = new TestAttributeShow(this);
        }
        [ContextMenu("show")]
        public void Show()
        {
            show.Show();
        }
    }
}
