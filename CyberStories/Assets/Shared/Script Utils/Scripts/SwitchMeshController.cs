using UnityEngine;
using UnityEngine.Serialization;

namespace CyberStories.Office.Controllers
{
    public class SwitchMeshController : MonoBehaviour
    {
        [FormerlySerializedAs("OnMeshRenderer")] public Mesh onMeshRenderer;
        [FormerlySerializedAs("OffMeshRenderer")] public Mesh offMeshRenderer;

        [FormerlySerializedAs("IsOn")] public bool isOn;

        [FormerlySerializedAs("TargetGameObject")] public GameObject targetGameObject;

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