using UnityEngine;
using UnityEngine.Serialization;

namespace CyberStories.Office.Controllers
{
    public class SwitchMeshController : MonoBehaviour
    {
        [FormerlySerializedAs("onMeshRenderer")] public Mesh OnMeshRenderer;
        [FormerlySerializedAs("offMeshRenderer")] public Mesh OffMeshRenderer;

        [FormerlySerializedAs("isOn")] public bool IsOn;

        [FormerlySerializedAs("targetGameObject")] public GameObject TargetGameObject;

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