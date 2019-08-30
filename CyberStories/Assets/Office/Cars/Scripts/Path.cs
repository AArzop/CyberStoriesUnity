using System.Collections.Generic;
using UnityEngine;

namespace CartoonCar
{
    public class Path : MonoBehaviour
    {
        public Color LineColor;

        private List<Transform> nodes = new List<Transform>();

        void OnDrawGizmosSelected()
        {
            Gizmos.color = LineColor;

            Transform[] pathTransforms = GetComponentsInChildren<Transform>();
            nodes = new List<Transform>();

            foreach (Transform pathTransform in pathTransforms)
            {
                if (pathTransform != transform)
                {
                    nodes.Add(pathTransform);
                }
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                Vector3 currentNode = nodes[i].position;
                Vector3 previousNode = Vector3.zero;

                if (i > 0)
                {
                    previousNode = nodes[i - 1].position;
                }
                else if (i == 0 && nodes.Count > 1)
                {
                    previousNode = nodes[nodes.Count - 1].position;
                }

                Gizmos.DrawLine(previousNode, currentNode);
                Gizmos.DrawWireSphere(currentNode, 0.3f);
            }
        }
    }
}