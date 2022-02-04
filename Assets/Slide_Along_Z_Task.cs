//using System;
//
//using Cysharp.Threading.Tasks;
//
//using UnityEngine;
//
//using Vanilla.DataAssets;
//using Vanilla.Easing;
//using Vanilla.MetaScript;
//
//namespace MagicalProject
//{
//
//    [Serializable]
//    public class Slide_Along_Z_Task : TaskBase
//    {
//
//        [SerializeField]
//        public RefSocket<Transform, TransformAsset> target;
//
//        [SerializeField]
//        public ValueSocket<float, FloatAsset> from;
//        
//        [SerializeField]
//        public ValueSocket<float, FloatAsset> to;
//        
//        [SerializeField]
//        public ValueSocket<float, FloatAsset> seconds;
//        
//        public override string GetDescription() => $"Moves [{target.GetAssetName()}] target along the z axis";
//
//        public override async UniTask Run()
//        {
//            var i    = 0.0f;
//            var rate = 1.0f / seconds.Get();
//            var t    = target.Get();
//            var a    = from.Get();
//            var b    = to.Get();
//            var p    = t.localPosition;
//
//            while (i < 1.0f)
//            {
//                i += Time.deltaTime * rate;
//
//                p.z = Mathf.Lerp(a: a,
//                                 b: b,
//                                 t: i);
//
//                await UniTask.Yield();
//            }
//
//            p.z = b;
//
//            t.localPosition = p;
//        }
//
//    }
//}
