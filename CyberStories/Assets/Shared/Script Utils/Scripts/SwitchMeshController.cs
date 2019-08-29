using UnityEngine;
using UnityEngine.Serialization;

namespace CyberStories.Office.Controllers
{
    public class SwitchMeshController : MonoBehaviour
    {
        public Mesh onMeshRenderer;
        public Mesh offMeshRenderer;

        public bool isOn;

        public GameObject targetGameObject;

        private MeshFilter currentMeshFilter;

        private void Start()
        {
            currentMeshFilter = targetGameObject.GetComponent<MeshFilter>();
            currentMeshFilter.mesh = isOn ? onMeshRenderer : offMeshRenderer;
        }

        public void SwitchMeshRenderer()
        {
            currentMeshFilter.mesh = isOn ? offMeshRenderer : onMeshRenderer;
            isOn = !isOn;
        }
    }
}