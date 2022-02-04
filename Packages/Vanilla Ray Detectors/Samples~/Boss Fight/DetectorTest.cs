using System;

using UnityEngine;

namespace Vanilla.RayDetectors.Samples
{
    
    [Serializable]
    public class DetectorTestModule : GenericDetector<DetectorTestModule, DetectableTest> {  }
    
    public class DetectorTest : MonoBehaviour
    {

        public GenericDetector<DetectorTestModule, DetectableTest> detector;


        void Start() => Cursor.lockState = CursorLockMode.Locked;


        void Update()
        {
	        detector.Detect(rayOrigin: transform.position,
	                   rayDirection: transform.forward);

	        if (Input.GetMouseButtonDown(0))
	        {
		        if (detector.componentDetected)
		        {
			        detector.currentComponent.TakeDamage();
		        }
	        }

	        if (Input.GetKeyDown(KeyCode.Escape))
	        {
		        Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
	        }
        }

    }
}
