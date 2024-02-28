//using System.Collections;
//using System.Collections.Generic;
//
//using Cysharp.Threading.Tasks;
//
//using Unity.RemoteConfig;
//
//using Vanilla.MetaScript.DataAssets;
//
//namespace Vanilla.MetaScript
//{
//    public class Set_Float_From_Remote_Config : TaskBase
//    {
//
//        public FloatDataAsset data;
//        public string         keyword;
//        
//        public override string GetDescription() => $"Set [{data.name}] from Remote Config using [{keyword}] keyword";
//
//        public override async UniTask Run() =>
//	        data.value = ConfigManager.appConfig.GetFloat(key: keyword,
//	                                                      defaultValue: data.value);
//
//    }
//}