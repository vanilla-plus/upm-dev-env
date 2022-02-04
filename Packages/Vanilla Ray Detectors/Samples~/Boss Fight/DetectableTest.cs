using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.RayDetectors.Samples
{
    public class DetectableTest : MonoBehaviour, IDetectable<DetectorTestModule, DetectableTest>
    {

        private MeshRenderer _rend;
        public  MeshRenderer rend => _rend ??= GetComponent<MeshRenderer>();

        private Rigidbody _rb;
        public  Rigidbody rb => _rb ??= GetComponent<Rigidbody>();
        
        public float health = 1.0f;

        private const float blowUpForce = 10.0f;
        
        public Gradient colorGradient;
        
	    void Start() => SetColor();

        public void OnDetectedBegin(DetectorTestModule rayDetector) { }

        public void OnDetectedEnd(DetectorTestModule rayDetector) { }

        public void TakeDamage()
        {
	        health = Mathf.Clamp01(health - 0.25f);

	        if (health < Mathf.Epsilon)
	        {
		        BlowUp();
		        
		        return;
	        }

	        SetColor();
        }
        
        private void SetColor() => rend.material.color = colorGradient.Evaluate(health);


        private void BlowUp()
        {
	        foreach (var c in GetComponentsInChildren<DetectableTest>())
	        {
		        if (c == this) continue;

		        c.BlowUp();
	        }

	        var t      = transform;
	        var p      = t.position;
	        var parent = t.parent;

	        var forcePoint = parent ?
		                         Vector3.Lerp(a: p,
		                                      b: parent.position,
		                                      t: 0.5f) :
		                         p;

	        var forceDir = parent ? p - parent.position : Vector3.up;


	        transform.SetParent(null);

	        rb.isKinematic = false;

	        rb.AddForceAtPosition(force: forceDir * blowUpForce,
	                              position: forcePoint,
	                              mode: ForceMode.Impulse);

	        rb.AddTorque(torque: forceDir * blowUpForce,
	                     mode: ForceMode.Impulse);

	        Destroy(this);

        }

    }
}
