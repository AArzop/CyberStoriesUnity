using UnityEngine;
using UnityEngine.AI;

namespace CyberStories.Shared.Player
{
    public class PlayerIA : MonoBehaviour
    {
        public Transform[] points;
        private int newDestPoint = 0;
        private NavMeshAgent agent;
        private readonly float rotationSpeed = 2.5f;

        public GameObject teleportArea;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            teleportArea.SetActive(false);
            GotoNextPoint();
        }

        private void LateUpdate()
        {
            if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
            {
                Quaternion lookRotation = Quaternion.LookRotation(agent.velocity.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            }

        }


        void GotoNextPoint()
        {
            // Returns if no points have been set up
            if (points.Length == 0)
                return;

            // Set the agent to go to the currently selected destination.
            agent.destination = points[newDestPoint].position;
            newDestPoint = ++newDestPoint;
        }


        void Update()
        {
            // Choose the next destination point when the agent gets
            // close to the current one.
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                if (newDestPoint != points.Length)
                    GotoNextPoint();
                else
                    teleportArea.SetActive(true);
            }
        }
    }
}
