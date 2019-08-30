using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace CyberStories.CyberstoriesNpc.Controllers
{
    public class Patrol : MonoBehaviour
    {
        [FormerlySerializedAs("points")] public Transform[] Points;
        private int destPoint = 0;
        private NavMeshAgent agent;

        [FormerlySerializedAs("rotationSpeed")] [Min(0)] public float RotationSpeed = 3f;
        private Animator animator;

        [FormerlySerializedAs("mustGoBack")] public bool MustGoBack;
        private bool isGoingBack = false;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            animator.SetFloat("Vertical", 1f);
            agent.updateRotation = false;
            GotoNextPoint();
        }


        private void LateUpdate()
        {
            // Direct rotation
            if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
            {
                //transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
                Quaternion lookRotation = Quaternion.LookRotation(agent.velocity.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, RotationSpeed * Time.deltaTime);
            }
        }


        void GotoNextPoint()
        {
            // Returns if no points have been set up
            if (Points.Length == 0)
                return;

            // Set the agent to go to the currently selected destination.
            agent.destination = Points[destPoint].position;

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            if (!MustGoBack)
            {
                destPoint = (destPoint + 1) % Points.Length;
            }
            else
            {
                if (destPoint == 0)
                    isGoingBack = false;
                if (destPoint + 1 == Points.Length)
                    isGoingBack = true;
                destPoint = destPoint + (isGoingBack ? -1 : 1);
            }
        }


        private void Update()
        {
            // Choose the next destination point when the agent gets
            // close to the current one.
            Debug.Log(agent.remainingDistance);
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }
    }
}