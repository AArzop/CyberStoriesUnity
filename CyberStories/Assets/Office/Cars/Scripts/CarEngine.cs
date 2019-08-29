using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CartoonCar
{
    public class CarEngine : MonoBehaviour
    {
        public Transform path;
        public float maxSteerAngle = 45f;
        [FormerlySerializedAs("wheelFL")] public WheelCollider wheelFl;
        [FormerlySerializedAs("wheelFR")] public WheelCollider wheelFr;
        public float maxMotorTorque = 80f;
        public float currentSpeed;
        public float maxSpeed = 100f;
        public Vector3 centerOfMass;

        private List<Transform> nodes;
        private int currectNode = 0;

        [FormerlySerializedAs("Delay")] [Min(0)]
        public float delay = 0f;

        private bool isCoroutineExecuting = false;

        private void Start()
        {
            GetComponent<Rigidbody>().centerOfMass = centerOfMass;

            Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
            nodes = new List<Transform>();

            foreach (var pathTransform in pathTransforms)
            {
                if (pathTransform != path.transform)
                    nodes.Add(pathTransform);
            }

            if (delay > 0)
                StartCoroutine(DelayObject());
        }

        private IEnumerator DelayObject()
        {
            isCoroutineExecuting = true;
            yield return new WaitForSeconds(delay);
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
            float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
            wheelFl.steerAngle = newSteer;
            wheelFr.steerAngle = newSteer;
        }

        private void Drive()
        {
            currentSpeed = 2 * Mathf.PI * wheelFl.radius * wheelFl.rpm * 60 / 1000;
            if (currentSpeed < maxSpeed)
            {
                wheelFl.motorTorque = maxMotorTorque;
                wheelFr.motorTorque = maxMotorTorque;
            }
            else
            {
                wheelFl.motorTorque = 0;
                wheelFr.motorTorque = 0;
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