using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.Geocodes
{

	public class MojitenMapCamera : MonoBehaviour
	{

		public Canvas canvas;
		
		public RectTransform canvasRectTransform;
		public RectTransform mapRectTransform;

		public float   minOrthographicSize = 5f;
		public float   maxOrthographicSize = 15f;

		private Vector3 lastMousePosition;

		public float scrollSensitivity = 10.0f;

		void Update()
		{
			// Handle Scroll-to-zoom
			
			float scrollInput = Input.GetAxis("Mouse ScrollWheel");

			Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - scrollInput * scrollSensitivity,
			                                           minOrthographicSize,
			                                           maxOrthographicSize);

			// Handle Middle-click-to-drag
			
			var p = transform.position;

			if (Input.GetMouseButtonDown(2))
			{
                lastMousePosition = Input.mousePosition;
			}
			
			if (Input.GetMouseButton(2))
			{
				var mousePos   = Input.mousePosition;
				var mouseDelta = mousePos - lastMousePosition;

				var viewportHeight = Camera.main.orthographicSize * 2;
				var viewportWidth  = viewportHeight               * Camera.main.aspect;

				mouseDelta.x /= Screen.width  / viewportWidth;
				mouseDelta.y /= Screen.height / viewportHeight;

				p -= new Vector3(mouseDelta.x,
				                 mouseDelta.y,
				                 0);

				lastMousePosition = mousePos;
			}

			var canvasBounds = canvas.renderingDisplaySize * 0.5f;
			
			var camHalfHeight = Camera.main.orthographicSize;
			var camHalfWidth  = camHalfHeight * Camera.main.aspect;

			// Clamp the camera position based on canvasBounds and cameras orthographic size * 0.5f
			
			p.x = Mathf.Clamp(p.x,
			                  -canvasBounds.x + camHalfWidth,
			                  canvasBounds.x  - camHalfWidth);

			p.y = Mathf.Clamp(p.y,
			                  -canvasBounds.y + camHalfHeight,
			                  canvasBounds.y - camHalfHeight);

			transform.position = p;
		}

	}

}