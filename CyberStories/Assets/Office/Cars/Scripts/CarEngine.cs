using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CartoonCar
{
    // TODO: fix bug: car engine don't follow the path accurately and sometimes crashes in walls 
    public class CarEngine : MonoBehaviour
    {
        public Transform Path;
        public float MaxSteerAngle = 45f;
        public WheelCollider WheelFl;
        public WheelCollider WheelFr;
        public float MaxMotorTorque = 80f;
        public float CurrentSpeed;
        public float MaxSpeed = 100f;    
        public Vector3 CenterOfMass;

        private List<Transform> nodes;
        private int currectNode = 0;

        [Min(0)]
        public float Delay = 0f;

        private bool isCoroutineExecuting = false;

        private void Start()
        {
            GetComponent<Rigidbody>().centerOfMass = CenterOfMass;

            Transform[] pathTransforms = Path.GetComponentsInChildren<Transform>();
            nodes = new List<Transform>();

            for (int i = 0; i< pathTransforms.Length; i++)
            {
                if (pathTransforms[i] != Path.transform)
                    nodes.Add(pathTransforms[i]);
            }

            if (Delay > 0)
                StartCoroutine(DelayObject());
        }

        private IEnumerator DelayObject()
        {
            isCoroutineExecuting = true;
            yield return new WaitForSeconds(Delay);
            isCoroutineExecuting = false;
        }

        private void FixedUpdate()
        {
            if (isCoroutineExecuting)
                return;
            ApplySteer();
            Drive();
            CheckWaypointDistance();
        }

        private void ApplySteer()
        {
            Vector3 relativeVector = transform.InverseTransformPoint(nodes[currectNode].position);
            float newSteer = (relativeVector.x / relativeVector.magnitude) * MaxSteerAngle;
            WheelFl.steerAngle = newSteer;
            WheelFr.steerAngle = newSteer;
        }

        private void Drive()
        {
            CurrentSpeed = 2 * Mathf.PI * WheelFl.radius * WheelFl.rpm * 60 / 1000;
            if (CurrentSpeed < MaxSpeed)
            {
                WheelFl.motorTorque = MaxMotorTorque;
                WheelFr.motorTorque = MaxMotorTorque;
            }
            else
            {
                WheelFl.motorTorque = 0;
                WheelFr.motorTorque = 0;
            }
        }

        private void CheckWaypointDistance()
        {
            if (Vector3.Distance(transform.position, nodes[currectNode].position) < 0.5f)
            {
                if (currectNode == nodes.Count - 1)
                    currectNode = 0;
                else
                    ++currectNode;
            }
        }
    }
}