using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.DebugCam
{
    public class DebugCamControls : MonoBehaviour
    {

        private Transform _t;
        private Camera _cam;
        
        [Header(header: "State")]
        [SerializeField]
        private bool _active = false;
        public bool active
        {
            get => _active;
            set
            {
                if (_active == value) return;

                _active = value;

                Toggle();
            }
        }

        [Header("Input Lock")]
        public KeyCode inputLock = KeyCode.Mouse1;
        
        [Header(header: "Look Input")]
        public float xSensitivity = 2.0f;
        public float ySensitivity = 2.0f;
        
        [Header(header: "Move Input")]
        public KeyCode toggle = KeyCode.F1;

        public KeyCode forward = KeyCode.W;
        public KeyCode back = KeyCode.S;
        public KeyCode left = KeyCode.A;
        public KeyCode right = KeyCode.D;

        public KeyCode boost = KeyCode.LeftShift;

        public float speed      = 0.1f;
        public float boostSpeed = 0.25f;

        private Vector3 positionTarget;
        private Vector3 positionVelocity;

        private Vector3    eulerTarget;
        private Quaternion rotationVelocity;

        private MeshRenderer[] meshes = new MeshRenderer[0];

        public bool movementSmoothing = true;
        
        private void OnValidate()
        {
            #if UNITY_EDITOR
            _t     = transform;
            _cam   = GetComponentInChildren<Camera>();
            meshes = GetComponentsInChildren<MeshRenderer>();

            Toggle();
            #endif
        }

        void Awake()
        {
            #if !DEBUG
            Destroy(gameObject);
            #endif

            meshes = GetComponentsInChildren<MeshRenderer>();
        }


        void OnEnable() => positionTarget = transform.position;


        void Toggle()
        {
            _cam.enabled = _active;
            _cam.depth   = _active ? 100.0f : -100.0f;
                
            foreach (var m in meshes)
            {
                m.enabled = _active;
            }
        }

        void Update()
        {
            #if UNITY_EDITOR
            if (Input.GetKeyDown(key: toggle))
            {
                active = !_active;
            }

            if (!active) return;

            if (movementSmoothing)
            {
                if (Input.GetKey(key: inputLock))
                {
                    // Look

                    eulerTarget.x -= Input.GetAxis(axisName: "Mouse Y") * ySensitivity;
                    eulerTarget.y += Input.GetAxis(axisName: "Mouse X") * xSensitivity;

                    // Movement

                    var s = Input.GetKey(key: boost) ? boostSpeed : speed;

                    if (Input.GetKey(key: forward)) positionTarget += _t.forward * s;
                    if (Input.GetKey(key: back)) positionTarget    -= _t.forward * s;
                    if (Input.GetKey(key: left)) positionTarget    -= _t.right   * s;
                    if (Input.GetKey(key: right)) positionTarget   += _t.right   * s;

                    // FOV Zoom

                    _cam.fieldOfView -= Input.GetAxis(axisName: "Mouse ScrollWheel") * 10.0f;
                }

                _t.position = Vector3.SmoothDamp(current: transform.position,
                                                 target: positionTarget,
                                                 currentVelocity: ref positionVelocity,
                                                 smoothTime: 0.075f);

                _t.rotation = Quaternion.Lerp(a: transform.rotation,
                                                   b: Quaternion.Euler(euler: eulerTarget),
                                                   t: Time.deltaTime * 20.0f);
            }
            else
            {
                if (!Input.GetKey(key: inputLock)) return;

                // Look
                
                _t.Rotate(xAngle: -Input.GetAxis(axisName: "Mouse Y") * ySensitivity,
                          yAngle: Input.GetAxis(axisName: "Mouse X")  * xSensitivity,
                          zAngle: 0,
                          relativeTo: Space.Self);

                var e = _t.eulerAngles;

                e.z = 0.0f;

                _t.eulerAngles = e;
                
                // Movement
                
                var s = Input.GetKey(key: boost) ? boostSpeed : speed;

                var p = _t.position;
                
                if (Input.GetKey(key: forward)) p += _t.forward * s;
                if (Input.GetKey(key: back)) p    -= _t.forward * s;
                if (Input.GetKey(key: left)) p    -= _t.right   * s;
                if (Input.GetKey(key: right)) p   += _t.right   * s;

                transform.position = p;

                // FOV Zoom
                
                _cam.fieldOfView -= Input.GetAxis(axisName: "Mouse ScrollWheel") * 10.0f;

            }

            #endif
        }

    }
}
