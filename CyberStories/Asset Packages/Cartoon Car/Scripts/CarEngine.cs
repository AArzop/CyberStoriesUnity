using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CartoonCar
{
    public class CarEngine : MonoBehaviour
    {

        public Transform path;
        public float maxSteerAngle = 45f;
        public WheelCollider wheelFL;
        public WheelCollider wheelFR;
        public float maxMotorTorque = 80f;
        public float currentSpeed;
        public float maxSpeed = 100f;
        public Vector3 centerOfMass;

        private List<Transform> nodes;
        private int currectNode = 0;

        [Min(0)]
        public float Delay = 0f;
        private bool isCouroutineExecuting = false;

        private void Start()
        {
            GetComponent<Rigidbody>().centerOfMass = centerOfMass;

            Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
            nodes = new List<Transform>();

            for (int i = 0; i < pathTransforms.Length; i++)
            {
                if (pathTransforms[i] != path.transform)
                    nodes.Add(pathTransforms[i]);
            }

            if (Delay > 0)
                StartCoroutine(DelayObject());
        }

        private IEnumerator DelayObject()
        {
            isCouroutineExecuting = true;
            yield return new WaitForSeconds(Delay);
            isCouroutineExecuting = false;
        }

        private void FixedUpdate()
        {
            if (isCouroutineExecuting)
                return;
            ApplySteer();
            Drive();
            CheckWaypointDistance();
        }

        private void ApplySteer()
        {
            Vector3 relativeVector = transform.InverseTransformPoint(nodes[currectNode].position);
            float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
            wheelFL.steerAngle = newSteer;
            wheelFR.steerAngle = newSteer;
        }

        private void Drive()
        {
            currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;
            if (currentSpeed < maxSpeed)
            {
                wheelFL.motorTorque = maxMotorTorque;
                wheelFR.motorTorque = maxMotorTorque;
            }
            else
            {
                wheelFL.motorTorque = 0;
                wheelFR.motorTorque = 0;
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