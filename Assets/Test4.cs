using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.DotNetExtensions;
using Vanilla.UnityExtensions;

namespace MagicalProject
{
    public class Test4 : MonoBehaviour
    {

	    public Transform target;

	    public Vector3 vel;
	    
	    public float factor = 10.0f;

	    public float power = 2.0f;
	    
        void Update()
        {
//	        transform.position = Vector3Extensions.SmoothByDistance(a: transform.position,
//	                                                                  b: target.position,
//	                                                                  factor: factor);

//	        target.position = Vector3.SmoothDamp(target.position,
//	                                             transform.position,
//	                                             ref vel,
//	                                             factor);


//	        vel = factor.DegreesToDirection().X_Y(0.0f);
        }

    }
}
