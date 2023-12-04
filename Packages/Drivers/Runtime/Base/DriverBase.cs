using System;

using UnityEngine;

using Vanilla.TypeMenu;

using Vanilla.Drivers.Modules;

namespace Vanilla.Drivers
{

    public interface IDriver
    {

        void Init(DriverSet driverSet);

        void DeInit(DriverSet driverSet);

        void OnValidate(DriverSet driverSet);

    }

    [Serializable]
    public abstract class DriverBase<T> : IDriver
    {

        [SerializeField]
        public string Name;

        public abstract ModuleBase<T>[] Modules
        {
            get;
            set;
        }

        public void OnValidate(DriverSet set)
        {
            var value = GetInterpolate(set.normal.Value);

            foreach (var m in Modules) m?.OnValidate(value);
        }


        public virtual void Init(DriverSet set)
        {
            foreach (var m in Modules) m?.Init(set);

            set.normal.OnValueChanged += HandleValueChange;

            HandleValueChange(set.normal.Value);
        }


        public void DeInit(DriverSet set) => set.normal.OnValueChanged -= HandleValueChange;


        public void HandleValueChange(float normal)
        {
            var value = GetInterpolate(normal);

            foreach (var m in Modules) m?.HandleValueChange(value);
        }


        protected abstract T GetInterpolate(float normal);

    }

    [Serializable]
    public class Vector1 : DriverBase<float>
    {

        [SerializeField]
        public float Min = 0.0f;

        [SerializeField]
        public float Max = 1.0f;

        [SerializeReference]
        [TypeMenu("blue")]
        public Vec1Module[] modules = Array.Empty<Vec1Module>();
        public override ModuleBase<float>[] Modules
        {
            get => modules;
            set => modules = value as Vec1Module[];
        }
        
        protected override float GetInterpolate(float normal) => Mathf.Lerp(a: Min,
                                                                            b: Max,
                                                                            t: normal);

    }

    [Serializable]
    public class Vector2 : DriverBase<UnityEngine.Vector2>
    {

        [SerializeField]
        public UnityEngine.Vector2 Min = UnityEngine.Vector2.zero;

        [SerializeField]
        public UnityEngine.Vector2 Max = UnityEngine.Vector2.one;

        [SerializeReference]
        [TypeMenu("blue")]
        public Vec2Module[] modules = Array.Empty<Vec2Module>();
        public override ModuleBase<UnityEngine.Vector2>[] Modules
        {
            get => modules;
            set => modules = value as Vec2Module[];
        }

        protected override UnityEngine.Vector2 GetInterpolate(float normal) => UnityEngine.Vector2.Lerp(a: Min,
                                                                                                        b: Max,
                                                                                                        t: normal);

    }

    [Serializable]
    public class Vector3 : DriverBase<UnityEngine.Vector3>
    {

        [SerializeField]
        public UnityEngine.Vector3 Min = UnityEngine.Vector3.zero;

        [SerializeField]
        public UnityEngine.Vector3 Max = UnityEngine.Vector3.one;

        [SerializeReference]
        [TypeMenu("blue")]
        public Vec3Module[] modules = Array.Empty<Vec3Module>();
        public override ModuleBase<UnityEngine.Vector3>[] Modules
        {
            get => modules;
            set => modules = value as Vec3Module[];
        }

        protected override UnityEngine.Vector3 GetInterpolate(float normal) => UnityEngine.Vector3.Lerp(a: Min,
                                                                                                        b: Max,
                                                                                                        t: normal);

    }

    [Serializable]
    public class Color : DriverBase<UnityEngine.Color>
    {

        [SerializeField]
        public UnityEngine.Color From = UnityEngine.Color.black;

        [SerializeField]
        public UnityEngine.Color To = UnityEngine.Color.white;

        [SerializeReference]
        [TypeMenu("blue")]
        public ColorModule[] modules = Array.Empty<ColorModule>();
        public override ModuleBase<UnityEngine.Color>[] Modules
        {
            get => modules;
            set => modules = value as ColorModule[];
        }

        protected override UnityEngine.Color GetInterpolate(float normal) => UnityEngine.Color.Lerp(a: From,
                                                                                                    b: To,
                                                                                                    t: normal);

    }

    [Serializable]
    public class Gradient : DriverBase<UnityEngine.Color>
    {

        [SerializeField]
        public UnityEngine.Gradient[] gradients = Array.Empty<UnityEngine.Gradient>();

        [SerializeReference]
        [TypeMenu("blue")]
        public ColorModule[] modules = Array.Empty<ColorModule>();
        public override ModuleBase<UnityEngine.Color>[] Modules
        {
            get => modules;
            set => modules = value as ColorModule[];
        }

        protected override UnityEngine.Color GetInterpolate(float normal)
        {
            var gradientCount = gradients.Length;

            if (gradientCount == 0) return UnityEngine.Color.white;

            var gradientBracket = (int) Mathf.Clamp(value: Mathf.Floor(f: normal * gradientCount),
                                                    min: 0,
                                                    max: gradientCount - 1);

            var output = normal * gradientCount - gradientBracket;

            return gradients[gradientBracket].Evaluate(time: output);
        }

    }

}