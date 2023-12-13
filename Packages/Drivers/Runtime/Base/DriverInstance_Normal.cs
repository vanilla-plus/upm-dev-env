using System;
using System.Collections.Generic;

using UnityEngine;

using Vanilla.DataAssets;
using Vanilla.Drivers.Snrubs;
using Vanilla.TypeMenu;

namespace Vanilla.Drivers.Normal
{

	[Serializable]
	public class DriverInstance_Normal : DriverInstance<float>
	{

		[SerializeField]
		public Socket[] _sockets = Array.Empty<Socket>();
		public override DriverSocket<float>[] Sockets => _sockets;
		
	}
	
	
	[Serializable]
	public class Socket : DriverSocket<float>
	{

		[SerializeField]
		private NormalAsset _asset;
		public override DataAsset<float> Asset => _asset;

		[SerializeReference]
		[TypeMenu("blue")]
		private NormalDriver[] _drivers;
		public override Module<float>[] Modules => _drivers;
		
	}
	
	
    [Serializable]
    public abstract class NormalDriver : Module<float>
    {
        
        [SerializeReference]
        [TypeMenu("blue")]
        private IEnumerable<NormalSnrub> _snrubs;
        public override IEnumerable<SnrubBase<float>> Snrubs => _snrubs;

        public override void OnValidate(DriverSocket<float> driverSocket)
        {
            var value = GetInterpolate(normal: driverSocket.Asset.Delta.Value);

            foreach (var m in Snrubs) m?.OnValidate(value);
        }


        public override void Init(DriverSocket<float> socket)
        {
            foreach (var m in Snrubs) m?.Init(socket: socket);

            socket.Asset.Delta.OnValueChanged += HandleValueChange;

            HandleValueChange(normal: socket.Asset.Delta.Value);
        }


        public override void DeInit(DriverSocket<float> socket)
        {
	        socket.Asset.Delta.OnValueChanged -= HandleValueChange;
	        
	        foreach (var m in Snrubs) m?.DeInit(socket: socket);
        }


        public override void HandleValueChange(float normal)
        {
            var value = GetInterpolate(normal: normal);

            foreach (var m in Snrubs) m?.HandleValueChange(value);
        }


        protected abstract float GetInterpolate(float normal);

    }
    
    [Serializable] public abstract class NormalSnrub : SnrubBase<float> { }

    [Serializable] public abstract class Vec2Snrub : NormalSnrub { }

    [Serializable] public abstract class Vec3Snrub : NormalSnrub { }

    [Serializable] public abstract class Vec4Snrub : NormalSnrub { }

    [Serializable] public abstract class ColorSnrub : NormalSnrub { }
    
//
//	[Serializable]
//    public class Vec1NormalDriver : NormalDriver
//    {
//
//        [SerializeField]
//        public float Min = 0.0f;
//
//        [SerializeField]
//        public float Max = 1.0f;
//
//        protected override float GetInterpolate(float normal) => Mathf.Lerp(a: Min,
//                                                                            b: Max,
//                                                                            t: normal);
//
//    }
//    
    
//
//    [Serializable]
//    public class Vec2NormalDriver : NormalDriver
//    {
//
//        [SerializeField]
//        public Vector2 Min = Vector2.zero;
//
//        [SerializeField]
//        public Vector2 Max = Vector2.one;
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