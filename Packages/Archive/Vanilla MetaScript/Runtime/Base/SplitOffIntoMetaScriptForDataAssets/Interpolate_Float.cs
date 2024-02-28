//using System.Collections.Generic;
//
//using Cysharp.Threading.Tasks;
//
//using UnityEngine;
//
//using Vanilla.UnityExtensions;
//using Vanilla.MetaScript.DataAssets;
//
//namespace Vanilla.MetaScript
//{
//
//    public class Interpolate_Float : TaskBase
//    {
//
////        public FloatSocket    data;
//        public FloatDataAsset data;
//        public float          target;
//        public float          duration;
//
//
//        public override async UniTask Run()
//        {
//            var i     = 0.0f;
//            var rate  = 1.0f / duration;
//            var start = data.value;
//
//            while (i < 1.0f)
//            {
//                i += Time.deltaTime * rate;
//
//                data.value = Mathf.Lerp(a: start,
//                                        b: target,
//                                        t: i);
//
//                await UniTask.Yield();
//            }
//
//            data.value = target;
//        }
//
//
//        public override string GetDescription() => $"Interpolate [{data.name}] to [{target}] over [{duration}] seconds";
//
//    }
//
//}