using UnityEngine;

namespace CyberStories.Office.Controllers
{
    public class SwitchMeshController : MonoBehaviour
    {
        public Mesh OnMeshRenderer;
        public Mesh OffMeshRenderer;

        public bool IsOn;

        public GameObject TargetGameObject;

        private MeshFilter _currentMeshFilter;

        void Start()
        {
            _currentMeshFilter = TargetGameObject.GetComponent<MeshFilter>();
            if (IsOn)
                _currentMeshFilter.mesh = OnMeshRenderer;
            else
                _currentMeshFilter.mesh = OffMeshRenderer;
        }

        public void SwitchMeshRenderer()
        {
            if (IsOn)
                _currentMeshFilter.mesh = OffMeshRenderer;
            else
                _currentMeshFilter.mesh = OnMeshRenderer;
            IsOn = !IsOn;
        }
    }
}