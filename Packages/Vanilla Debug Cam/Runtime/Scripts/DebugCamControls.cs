using System;
using System.IO;
using System.Threading.Tasks;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

using Vanilla.DataSources;

namespace Vanilla.DebugCam
{
    
    [Serializable]
    public class DebugCamControls : MonoBehaviour
    {

        public static GameObject debugCam;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Reset()
        {
            #if UNITY_EDITOR
            if (debugCam != null) Destroy(debugCam);

            debugCam = null;
            #endif
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static async void Init()
        {
            #if UNITY_EDITOR
            while (debugCam == null)
            {
                if (Input.GetKeyDown(KeyCode.F5))
                {
                    Spawn();
                    
                    break;
                }

                await Task.Yield();
            }
            #endif
        }


        public static void Spawn()
        {
            #if UNITY_EDITOR
            var path = Path.Combine("Packages",
                                    "vanilla.debug-cam",
                                    "Runtime",
                                    "Prefabs",
                                    "Debug Cam.prefab");

            var asset = (GameObject) AssetDatabase.LoadAssetAtPath(assetPath: path,
                                                                   type: typeof(GameObject));

            if (asset == null)
            {
                Debug.LogWarning(message: $"We were unable to find the chosen prefab. It was expected to be found at the following path:\n\n{path}");

                return;
            }

            debugCam = PrefabUtility.InstantiatePrefab(assetComponentOrGameObject: asset) as GameObject;
            #endif
        }


        private Transform _t;
        private Camera _cam;


        [Header(header: "State")]
        [SerializeField]
        public ProtectedBoolSource Active = new()
                                            {
                                                Name  = "Debug Cam Active",
                                                Value = true
                                            };

        [Header("Input Lock")]
        public KeyCode inputLock = KeyCode.Mouse1;
        
        [Header(header: "Look Input")]
        public float xSensitivity = 4.0f;
        public float ySensitivity = 4.0f;
        public float zSensitivity = 4.0f;
        
        [Header(header: "Move Input")]
        public KeyCode toggle = KeyCode.F5;

        public KeyCode forward = KeyCode.W;
        public KeyCode back    = KeyCode.S;
        public KeyCode left    = KeyCode.A;
        public KeyCode right   = KeyCode.D;
        public KeyCode up   = KeyCode.E;
        public KeyCode down   = KeyCode.Q;

        public KeyCode boost = KeyCode.LeftShift;

        public float speed      = 0.1f;
        public float boostSpeed = 0.25f;

        private Vector3 positionTarget;
        private Vector3 positionVelocity;

        private Vector3    eulerTarget;
        private Quaternion rotationVelocity;

        private MeshRenderer[] meshes = Array.Empty<MeshRenderer>();

        public bool movementSmoothing = true;
        
        private void OnValidate()
        {
            #if UNITY_EDITOR
            _t     = transform;
            
            meshes = GetComponentsInChildren<MeshRenderer>();
            #endif
        }

        void Awake()
        {
            #if !UNITY_EDITOR
            Destroy(gameObject);
            
            return;
            #endif

            // We do this because URP and HDRP attach their own "Additional Camera Data" components
            // which can result in missing GUID references if the right pipeline isn't installed.
            // We can't assume which pipeline this tool will be used with - so let's just add the camera
            // at runtime and let the engine handle the rest.
            _cam       = gameObject.AddComponent<Camera>(); 
            _cam.depth = Active ? 100.0f : -100.0f;

            Active.OnSet += HandleActiveChange;

            if (debugCam != null)
            {
                Destroy(debugCam);
            }
            
            debugCam = gameObject;
            DontDestroyOnLoad(debugCam);

            meshes = GetComponentsInChildren<MeshRenderer>();
        }


        private void HandleActiveChange(bool incoming)
        {
            _cam.enabled = incoming;
            _cam.depth   = incoming ? 100.0f : -100.0f;
            
            foreach (var m in meshes) m.enabled = incoming;
        }


        void OnEnable() => positionTarget = transform.position;

        void Update()
        {
            #if UNITY_EDITOR
            if (Input.GetKeyDown(key: toggle)) Active.Flip();

            if (!Active) return;

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
                    if (Input.GetKey(key: up)) positionTarget      += _t.up      * s;
                    if (Input.GetKey(key: down)) positionTarget    -= _t.up      * s;

                    // FOV Zoom

                    _cam.fieldOfView -= Input.GetAxis(axisName: "Mouse ScrollWheel") * zSensitivity;
                }

                _t.position = Vector3.SmoothDamp(current: transform.position,
                                                 target: positionTarget,
                                                 currentVelocity: ref positionVelocity,
                                                 smoothTime: 0.075f,
                                                 maxSpeed: 100.0f, 
                                                 deltaTime: Time.unscaledDeltaTime);

                _t.rotation = Quaternion.Slerp(a: transform.rotation,
                                               b: Quaternion.Euler(euler: eulerTarget),
                                               t: Time.unscaledDeltaTime * 20.0f);
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