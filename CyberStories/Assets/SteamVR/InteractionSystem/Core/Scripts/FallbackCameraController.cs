//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Controls for the non-VR debug camera
//
//=============================================================================

using UnityEngine;
using System.Collections;
using System;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Camera ) )]
	public class FallbackCameraController : MonoBehaviour
	{
		public float speed = 4.0f;
		public float shiftSpeed = 16.0f;
		public bool showInstructions = true;

        public float JoystickCameraSpeed = 40.0f;

		private Vector3 startEulerAngles;
		private Vector3 startMousePosition;
		private float realTime;
        private readonly float DeltaJoystick = 0.1f;

        //-------------------------------------------------
        void OnEnable()
		{
			realTime = Time.realtimeSinceStartup;
		}


		//-------------------------------------------------
		void Update()
		{
            float forward = Input.GetAxis("Vertical");
            float right = Input.GetAxis("Horizontal");

			float currentSpeed = speed;
            // Acceleration
			if ( Input.GetKey( KeyCode.LeftShift ) || Input.GetKey( KeyCode.RightShift ) )
				currentSpeed = shiftSpeed;

			float realTimeNow = Time.realtimeSinceStartup;
			float deltaRealTime = realTimeNow - realTime;
			realTime = realTimeNow;

			Vector3 delta = new Vector3( right, 0.0f, forward ) * currentSpeed * deltaRealTime;

			transform.position += transform.TransformDirection( delta );

            /* Mouse */
			Vector3 mousePosition = Input.mousePosition;

			if ( Input.GetMouseButtonDown( 1 ) /* right mouse */)
			{
				startMousePosition = mousePosition;
				startEulerAngles = transform.localEulerAngles;
			}

			if ( Input.GetMouseButton( 1 ) /* right mouse */)
			{
				Vector3 offset = mousePosition - startMousePosition;
				transform.localEulerAngles = startEulerAngles + new Vector3( -offset.y * 360.0f / Screen.height, offset.x * 360.0f / Screen.width, 0.0f );
			}
        }


		//-------------------------------------------------
		void OnGUI()
		{
			if ( showInstructions )
			{
				GUI.Label( new Rect( 10.0f, 10.0f, 600.0f, 400.0f ),
					"ZQSD/Arrow Keys to translate the camera\n" +
					"Right mouse click to rotate the camera\n" +
					"Left mouse click for standard interactions.\n" );
			}
		}
	}
}
