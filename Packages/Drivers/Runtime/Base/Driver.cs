using System;
using System.Collections.Generic;

using UnityEngine;

using Vanilla.TypeMenu;

using Vanilla.Drivers.Snrubs;

namespace Vanilla.Drivers
{

    [Serializable]
    public abstract class Module<T>
    {

//        public abstract IEnumerable<SnrubBase<T>> Snrubs
//        {
//            get;
//        }
        public abstract void OnValidate(DriverSocket<T> socket);

//        public virtual void OnValidate(DriverSocket<T> socket)
//        {
//            var value = socket.Asset.Delta.Value;

//            foreach (var m in Snrubs) m?.OnValidate(value);
//        }

        public abstract void Init(DriverSocket<T> socket);

//        public virtual void Init(DriverSocket<T> socket)
//        {
//            foreach (var m in Snrubs) m?.Init(socket);

//            socket.Asset.Delta.OnValueChanged += HandleValueChange;

//            HandleValueChange(socket.Asset.Delta.Value);
//        }

	    public abstract void DeInit(DriverSocket<T> driverSocket);

//        public virtual void DeInit(DriverSocket<T> driverSocket) => driverSocket.Asset.Delta.OnValueChanged -= HandleValueChange;

        public abstract void HandleValueChange(T value);
        
//        public virtual void HandleValueChange(T value)
//        {
//            foreach (var m in Snrubs) m?.HandleValueChange(value);
//        }

    }
    
//
//    public interface INormalDriver : IDriver<float>
//    {
//
//        void Init(NormalDriverSocket normalDriverSocket);
//
//        void DeInit(NormalDriverSocket normalDriverSocket);
//
//        void OnValidate(NormalDriverSocket normalDriverSocket);
//
//    }

    [Serializable]
    public abstract class Vec1Driver : Module<float>
    {

        [SerializeReference]
        [TypeMenu("blue")]
        private IEnumerable<Vec1Snrub> _snrubs;
        public override IEnumerable<SnrubBase<float>> Snrubs => _snrubs;

//        public void OnValidate(DriverSocket<float> driverSocket)
//        {
//            var value = driverSocket.Asset.Delta.Value;
//
//            foreach (var m in Snrubs) m?.OnValidate(value);
//        }
//
//
//        public virtual void Init(DriverSocket<float> driverSocket)
//        {
//            foreach (var m in Snrubs) m?.Init(driverSocket);
//
//            driverSocket.Asset.Delta.OnValueChanged += HandleValueChange;
//
//            HandleValueChange(driverSocket.Asset.Delta.Value);
//        }
//
//
//        public void DeInit(DriverSocket<float> driverSocket) => driverSocket.Asset.Delta.OnValueChanged -= HandleValueChange;
//        
//        
//        public void HandleValueChange(float value)
//        {
//            foreach (var m in Snrubs) m?.HandleValueChange(value);
//        }

    }

//    [Serializable]
//    public abstract class NormalDriver : Driver<float>
//    {
//        
//        [SerializeReference]
//        [TypeMenu("blue")]
//        private IEnumerable<NormalSnrub> _snrubs;
//        public override IEnumerable<SnrubBase<float>> Snrubs => _snrubs;
//
//        public override void OnValidate(DriverSocket<float> driverSocket)
//        {
//            var value = GetInterpolate(driverSocket.Asset.Delta.Value);
//
//            foreach (var m in Snrubs) m?.OnValidate(value);
//        }
//
//
//        public virtual void Init(Vec1DriverSocket driverSocket)
//        {
//            foreach (var m in Snrubs) m?.Init(driverSocket);
//
//            driverSocket.Asset.Delta.OnValueChanged += HandleValueChange;
//
//            HandleValueChange(driverSocket.Asset.Delta.Value);
//        }
//
//
//        public void DeInit(Vec1DriverSocket driverSocket) => driverSocket.Asset.Delta.OnValueChanged -= HandleValueChange;
//
//
//        public void HandleValueChange(float normal)
//        {
//            var value = GetInterpolate(normal);
//
//            foreach (var m in Snrubs) m?.HandleValueChange(value);
//        }
//
//
//        protected abstract T GetInterpolate(float normal);
//
//        public void OnValidate(DriverSocket<float> socket) { }
//
//        public void Init(DriverSocket<float> socket) { }
//
//        public void DeInit(DriverSocket<float> socket) { }
//
//    }
//
//    [Serializable]
//    public class Normal_Vector1_Driver : NormalDriver<float>
//    {
//
//        [SerializeField]
//        public float Min = 0.0f;
//
//        [SerializeField]
//        public float Max = 1.0f;
//
//        [SerializeReference]
//        [TypeMenu("blue")]
//        public Vec1Snrub[] snrubs = Array.Empty<Vec1Snrub>();
//        public override SnrubBase<float>[] Snrubs
//        {
//            get => snrubs;
//            set => snrubs = value as Vec1Snrub[];
//        }
//        
//        protected override float GetInterpolate(float normal) => Mathf.Lerp(a: Min,
//                                                                            b: Max,
//                                                                            t: normal);
//
//    }
//
//    [Serializable]
//    public class Vector2 : NormalDriver<UnityEngine.Vector2>
//    {
//
//        [SerializeField]
//        public UnityEngine.Vector2 Min = UnityEngine.Vector2.zero;
//
//        [SerializeField]
//        public UnityEngine.Vector2 Max = UnityEngine.Vector2.one;
//
//        [SerializeReference]
//        [TypeMenu("blue")]
//        public Vec2Snrub[] snrubs = Array.Empty<Vec2Snrub>();
//        public override SnrubBase<UnityEngine.Vector2>[] Snrubs
//        {
//            get => snrubs;
//            set => snrubs = value as Vec2Snrub[];
//        }
//
//        protected override UnityEngine.Vector2 GetInterpolate(float normal) => UnityEngine.Vector2.Lerp(a: Min,
//                                                                                                        b: Max,
//                                                                                                        t: normal);
//
//    }
//
//    [Serializable]
//    public class Vector3 : NormalDriver<UnityEngine.Vector3>
//    {
//
//        [SerializeField]
//        public UnityEngine.Vector3 Min = UnityEngine.Vector3.zero;
//
//        [SerializeField]
//        public UnityEngine.Vector3 Max = UnityEngine.Vector3.one;
//
//        [SerializeReference]
//        [TypeMenu("blue")]
//        public Vec3Snrub[] snrubs = Array.Empty<Vec3Snrub>();
//        public override SnrubBase<UnityEngine.Vector3>[] Snrubs
//        {
//            get => snrubs;
//            set => snrubs = value as Vec3Snrub[];
//        }
//
//        protected override UnityEngine.Vector3 GetInterpolate(float normal) => UnityEngine.Vector3.Lerp(a: Min,
//                                                                                                        b: Max,
//                                                                                                        t: normal);
//
//    }
//
//    [Serializable]
//    public class Color : NormalDriver<UnityEngine.Color>
//    {
//
//        [SerializeField]
//        public UnityEngine.Color From = UnityEngine.Color.black;
//
//        [SerializeField]
//        public UnityEngine.Color To = UnityEngine.Color.white;
//
//        [SerializeReference]
//        [TypeMenu("blue")]
//        public ColorSnrub[] snrubs = Array.Empty<ColorSnrub>();
//        public override SnrubBase<UnityEngine.Color>[] Snrubs
//        {
//            get => snrubs;
//            set => snrubs = value as ColorSnrub[];
//        }
//
//        protected override UnityEngine.Color GetInterpolate(float normal) => UnityEngine.Color.Lerp(a: From,
//                                                                                                    b: To,
//                                                                                                    t: normal);
//
//    }
//
//    [Serializable]
//    public class Gradient : NormalDriver<UnityEngine.Color>
//    {
//
//        [SerializeField]
//        public UnityEngine.Gradient[] gradients = Array.Empty<UnityEngine.Gradient>();
//
//        [SerializeReference]
//        [TypeMenu("blue")]
//        public ColorSnrub[] snrubs = Array.Empty<ColorSnrub>();
//        public override SnrubBase<UnityEngine.Color>[] Snrubs
//        {
//            get => snrubs;
//            set => snrubs = value as ColorSnrub[];
//        }
//
//        protected override UnityEngine.Color GetInterpolate(float normal)
//        {
//            var gradientCount = gradients.Length;
//
//            if (gradientCount == 0) return UnityEngine.Color.white;
//
//            var gradientBracket = (int) Mathf.Clamp(value: Mathf.Floor(f: normal * gradientCount),
//                                                    min: 0,
//                                                    max: gradientCount - 1);
//
//            var output = normal * gradientCount - gradientBracket;
//
//            return gradients[gradientBracket].Evaluate(time: output);
//        }
//
//    }

}