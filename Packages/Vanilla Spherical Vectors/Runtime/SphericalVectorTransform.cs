using System;

using UnityEngine;

namespace Vanilla.SphericalVectors.Samples
{

	public class SphericalVectorTransform : MonoBehaviour
	{

		[Header(header: "Target")]
		[SerializeField]
		public Transform target;

		[SerializeField]
		public Vector3 destination = new Vector3(x: 0,
		                                         y: 0,
		                                         z: 3.0f);

		[Header(header: "Input")]
		[SerializeField]
		public float rotateSpeed = 500f;
		[SerializeField]
		public float scrollSpeed = 15000f;

		[Header("Clamp Longitude")]
		[SerializeField]
		public bool clampLongitude = false;

		[SerializeField]
		public float longMin = -179.9f;
		[SerializeField]
		public float longMax = 179.9f;
		
		[Header("Clamp Latitude")]
		[SerializeField]
		public bool clampLatitude = true;

		[SerializeField]
		public float latMin = -89.9f;
		[SerializeField]
		public float latMax = 89.9f;

		[Header(header: "Clamp Radius")]
		[SerializeField]
		public bool clampRadius = true;

		[SerializeField]
		public float radiusMin = 3.0f;
		[SerializeField]
		public float radiusMax = 10.0f;


		[Header(header: "Smoothing")]
		[SerializeField]
		public bool doSmoothing = true;

		[SerializeField]
		public float smoothTime = 0.0666f;

		[NonSerialized]
		public Vector3 delta;

		[NonSerialized]
		public Vector3 smoothDeltaVelocity;

		private void Start()
		{
			destination = transform.position.ToSpherical();

			delta = destination;
		}


//		void Update() => destination.x = Wrap(destination.x,
//		                                   -180.0f,
//		                                   180.0f);
//
//
//		public float Wrap(float value, float min, float max) {
//			float range = max - min;
//			while (value < min) {
//				value += range;
//			}
//			while (value >= max) {
//				value -= range;
//			}
//			return value;
//		}

		void LateUpdate()
		{

			// Input

			if (Input.GetMouseButton(button: 1))
			{
				destination.x -= Input.GetAxis(axisName: "Mouse X") * rotateSpeed * Time.deltaTime;
				destination.y  -= Input.GetAxis(axisName: "Mouse Y") * rotateSpeed * Time.deltaTime;
			}

			destination.z -= Input.GetAxis(axisName: "Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;

			// Clamp

			if (clampLongitude)
			{
				destination.x = Mathf.Clamp(value: destination.x,
				                            min: longMin,
				                            max: longMax);
			}

			if (clampLatitude)
			{
				destination.y = Mathf.Clamp(value: destination.y,
				                            min: latMin,
				                            max: latMax);
			}
			
			if (clampRadius)
			{
				destination.z = Mathf.Clamp(value: destination.z,
				                            min: radiusMin,
				                            max: radiusMax);
			}

			// Smooth

			if (doSmoothing)
			{

				delta = Vector3.SmoothDamp(current: delta,
				                           target: destination,
				                           currentVelocity: ref smoothDeltaVelocity,
				                           smoothTime: smoothTime);
				
				// Apply

				// If you get strange results here, be mindful that the scale of a target transform will also scale its TransformPoint result!
				
				if (target)
				{
					transform.position = target.TransformPoint(position: delta.ToCartesian());

					transform.LookAt(worldPosition: target.position);
				}
				else
				{
					transform.position = delta.ToCartesian();

					transform.LookAt(worldPosition: Vector3.zero);
				}

			}
			else
			{

				// Apply

				if (target)
				{
					transform.position = target.TransformPoint(position: destination.ToCartesian());

					transform.LookAt(worldPosition: target.position);
				}
				else
				{
					transform.position = destination.ToCartesian();

					transform.LookAt(worldPosition: Vector3.zero);
				}
			}


		}

	}

}