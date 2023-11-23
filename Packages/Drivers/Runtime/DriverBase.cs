using System;

using UnityEngine;
using UnityEngine.Events;

using Vanilla.TypeMenu;

namespace Vanilla.Drivers
{

    public interface IDriver
    {

        void Init(DriverSet driverSet);

        void DeInit(DriverSet driverSet);

        void HandleValueChange(float normal);

        void OnValidate(DriverInstance instance, DriverSet driverSet);

    }

    [Serializable]
    public abstract class DriverBase<T> : IDriver
    {

//        [SerializeField]
//        public string Name;

//        [SerializeField]
//        public UnityEvent<T> OnValueChange;

//        [SerializeReference]
//        [TypeMenu]
//        public ModuleBase<T>[] modules = Array.Empty<ModuleBase<T>>();

        public abstract ModuleBase<T>[] Modules
        {
            get;
            set;
        }

//        public abstract void OnValidate(DriverSet driverSet);


        public void OnValidate(DriverInstance instance, DriverSet set)
        {
//            if (instance.DebugMode)
//            {
                var value = GetInterpolate(set.normal.Value);
//            }
//            else
//            {
//                var value = 0.0f;
//            }

            foreach (var m in Modules) m?.OnValidate(value);
        }


        public virtual void Init(DriverSet set)
        {
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

//    [Serializable]
//    public abstract class EventDriver<T> : DriverBase<T>
//    {
//
//        [SerializeField]
//        public UnityEvent<T> OnValueChange;
//
//        public override void Interpolate(float normal) => OnValueChange.Invoke(Get(normal));
//
//    }



    [Serializable]
    public class Vec1Driver : DriverBase<float>
    {

        [SerializeField]
        public float Min = 0.0f;

        [SerializeField]
        public float Max = 1.0f;

        [SerializeReference]
        [TypeMenu]
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
    public class Vec2Driver : DriverBase<Vector2>
    {

        [SerializeField]
        public Vector2 Min = Vector2.zero;

        [SerializeField]
        public Vector2 Max = Vector2.one;

        [SerializeReference]
        [TypeMenu]
        public Vec2Module[] modules = Array.Empty<Vec2Module>();
        public override ModuleBase<Vector2>[] Modules
        {
            get => modules;
            set => modules = value as Vec2Module[];
        }

        protected override Vector2 GetInterpolate(float normal) => Vector2.Lerp(a: Min,
                                                                                b: Max,
                                                                                t: normal);

    }

    [Serializable]
    public class Vec3Driver : DriverBase<Vector3>
    {

        [SerializeField]
        public Vector3 Min = Vector3.zero;

        [SerializeField]
        public Vector3 Max = Vector3.one;

        [SerializeReference]
        [TypeMenu]
        public Vec3Module[] modules = Array.Empty<Vec3Module>();
        public override ModuleBase<Vector3>[] Modules
        {
            get => modules;
            set => modules = value as Vec3Module[];
        }

        protected override Vector3 GetInterpolate(float normal) => Vector3.Lerp(a: Min,
                                                                                b: Max,
                                                                                t: normal);

    }

    [Serializable]
    public class ColorDriver : DriverBase<Color>
    {

        [SerializeField]
        public Color From = Color.black;

        [SerializeField]
        public Color To = Color.white;

        [SerializeReference]
        [TypeMenu]
        public ColorModule[] modules = Array.Empty<ColorModule>();
        public override ModuleBase<Color>[] Modules
        {
            get => modules;
            set => modules = value as ColorModule[];
        }

        protected override Color GetInterpolate(float normal) => Color.Lerp(a: From,
                                                                            b: To,
                                                                            t: normal);

    }

    [Serializable]
    public class GradientDriver : DriverBase<Color>
    {

        [SerializeField]
        public Gradient[] gradients = Array.Empty<Gradient>();

        [SerializeReference]
        [TypeMenu]
        public ColorModule[] modules = Array.Empty<ColorModule>();
        public override ModuleBase<Color>[] Modules
        {
            get => modules;
            set => modules = value as ColorModule[];
        }

        protected override Color GetInterpolate(float normal)
        {
            var gradientCount = gradients.Length;

            if (gradientCount == 0) return Color.white;

            var gradientBracket = (int) Mathf.Clamp(value: Mathf.Floor(f: normal * gradientCount),
                                                    min: 0,
                                                    max: gradientCount - 1);

            var output = normal * gradientCount - gradientBracket;

            return gradients[gradientBracket].Evaluate(time: output);
        }

    }

}