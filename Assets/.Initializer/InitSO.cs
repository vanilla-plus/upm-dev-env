//using System.Collections;
//using System.Collections.Generic;
//
//using UnityEngine;
//
//using static UnityEngine.Debug;
//
//namespace MagicalProject
//{
//
//    [CreateAssetMenu(fileName = "What",
//                     menuName = "Yes")]
//    public class InitSO : ScriptableObject
//    {
//
//        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
//        public static void Before() => Log("Before?");
//
//
//        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
//        public static void After() => Log("After?");
//
//    }
//
//}