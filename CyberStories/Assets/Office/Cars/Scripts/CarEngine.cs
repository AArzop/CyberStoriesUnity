using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CartoonCar
{
    public class CarEngine : MonoBehaviour
    {
        [FormerlySerializedAs("path")] public Transform Path;
        [FormerlySerializedAs("maxSteerAngle")] public float MaxSteerAngle = 45f;
        [FormerlySerializedAs("wheelFl")] public WheelCollider WheelFl;
        [FormerlySerializedAs("wheelFr")] public WheelCollider WheelFr;
        [FormerlySerializedAs("maxMotorTorque")] public float MaxMotorTorque = 80f;
        [FormerlySerializedAs("currentSpeed")] public float CurrentSpeed;
        [FormerlySerializedAs("maxSpeed")] public float MaxSpeed = 100f;
        [FormerlySerializedAs("centerOfMass")] public Vector3 CenterOfMass;

        private List<Transform> nodes;
        private int currectNode = 0;

        [FormerlySerializedAs("delay")] [Min(0)]
        public float Delay = 0f;

        private bool isCoroutineExecuting = false;

        private void Start()
        {
            GetComponent<Rigidbody>().centerOfMass = CenterOfMass;

            Transform[] pathTransforms = Path.GetComponentsInChildren<Transform>();
            nodes = new List<Transform>();

            foreach (var pathTransform in pathTransforms)
            {
                if (pathTransform != Path.transform)
                    nodes.Add(pathTransform);
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