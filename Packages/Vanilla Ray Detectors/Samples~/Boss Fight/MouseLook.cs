using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.RayDetectors.Samples
{
    public class MouseLook : MonoBehaviour
    {

        void Start() => transform.eulerAngles = Vector3.zero;


        void Update()
        {
            var e = transform.eulerAngles;

            e.x -= Input.GetAxis("Mouse Y") * 2.0f;
            e.y += Input.GetAxis("Mouse X") * 2.0f;

            transform.eulerAngles = e;
        }
    }
}
