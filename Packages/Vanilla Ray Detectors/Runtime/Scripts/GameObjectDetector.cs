using System;

using UnityEngine;

namespace Vanilla.RayDetectors
{

    [Serializable]
    public class GameObjectDetector
    {

        public float rayLength = 10.0f;

        public LayerMask layerMask;

        public bool colliderDetected = false;

        [SerializeField]
        private Collider _currentCollider;
        public Collider currentCollider
        {
            get => _currentCollider;
            set
            {
                if (ReferenceEquals(objA: _currentCollider,
                                    objB: value)) return;

                ColliderChangeDetected(outgoing: _currentCollider,
                                       incoming: value);
                
                _currentCollider = value;
            }
        }

        public Action<Collider, Collider> onColliderChangeDetected;


        public virtual void Detect(Vector3 rayOrigin,
                                   Vector3 rayDirection)
        {
            Physics.Raycast(origin: rayOrigin,
                            direction: rayDirection,
                            hitInfo: out var hit,
                            maxDistance: rayLength,
                            layerMask: layerMask);

            currentCollider = hit.collider;

            #if DEBUG_RAY_DETECTOR
            Debug.DrawRay(start: rayOrigin,
                          dir: rayDirection * rayLength,
                          color: colliderDetected ?
                                     Color.green :
                                     Color.red);
            #endif
        }


        protected virtual void ColliderChangeDetected(Collider outgoing,
                                                      Collider incoming)
        {
            #if DEBUG_RAY_DETECTORS
            Debug.Log(message: $"GameObjectDetector collider result changed from [{outgoing}] to [{incoming}]");
            #endif
            
            colliderDetected = !ReferenceEquals(objA: incoming,
                                                objB: null);

            onColliderChangeDetected?.Invoke(arg1: outgoing,
                                             arg2: incoming);
        }

    }

}