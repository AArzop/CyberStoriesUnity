using UnityEngine;
using UnityEngine.AI;

namespace CyberStories.CyberstoriesNpc.Controllers
{
    public class Patrol : MonoBehaviour
    {

        public Transform[] points;
        private int destPoint = 0;
        private NavMeshAgent agent;

        private Animator animator;

        public bool mustGoBack;
        private bool isGoingBack = false;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            animator.SetFloat("Vertical", 1f);
            // /*** ***/ agent.updateRotation = false;
            GotoNextPoint();
        }

        /*
        private void LateUpdate()
        {
            // Direct rotation
            if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
            {
                transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
            }
            
        }
        */

        void GotoNextPoint()
        {
            // Returns if no points have been set up
            if (points.Length == 0)
                return;

            // Set the agent to go to the currently selected destination.
            agent.destination = points[destPoint].position;

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            if (!mustGoBack)
            {
                destPoint = (destPoint + 1) % points.Length;
            } else
            {
                if (destPoint == 0)
                    isGoingBack = false;
                if (destPoint + 1 == points.Length)
                    isGoingBack = true;
                destPoint = destPoint + (isGoingBack ? -1 : 1);
            }
        }


        void Update()
        {
            // Choose the next destination point when the agent gets
            // close to the current one.
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }
    }
}