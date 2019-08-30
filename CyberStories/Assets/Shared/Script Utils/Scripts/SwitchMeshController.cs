using UnityEngine;

namespace CyberStories.Office.Controllers
{
    public class SwitchMeshController : MonoBehaviour
    {
        public Mesh OnMeshRenderer;
        public Mesh OffMeshRenderer;

        public bool IsOn;

        public GameObject TargetGameObject;

        private MeshFilter currentMeshFilter;

        private void Start()
        {
            currentMeshFilter = TargetGameObject.GetComponent<MeshFilter>();
            currentMeshFilter.mesh = IsOn ? OnMeshRenderer : OffMeshRenderer;
        }

        public void SwitchMeshRenderer()
        {
            currentMeshFilter.mesh = IsOn ? OffMeshRenderer : OnMeshRenderer;
            IsOn = !IsOn;
        }
    }
}