using System;

using UnityEditor;

using UnityEngine;

using static UnityEngine.Debug;

namespace Vanilla.DataAssets // ------------------------------------------------------------------------------------------------------------ Base //
{

    // ----------------------------------------------------------------------------------------------------------------------------------- Socket //
    
//    public interface ISocket<TType,TAsset,TSocket,TGetter,TSetter>
////        where TAsset 
//    {
//
//    }
    
    // ------------------------------------------------------------------------------------------------------------------------------------ Asset //

//    public interface IDataAsset<TType,TAsset,TSocket,TGetter,TSetter>
//    {
//
//        string Description
//        {
//            get;
//        }
//
//        void Broadcast();
//
//        void OnValidate();
//
//        void Awake();
//        
//        void OnEnable();
//        
//        void OnDisable();
//
//    }
    
    [Serializable]
    public abstract class BaseAsset : ScriptableObject 
    {

//        [TextArea(minLines: 1,
//                  maxLines: 50)]
        [SerializeField]
        private string _description;
        public string description => _description;

        public abstract void Broadcast();


        protected virtual void OnValidate()
        {
            #if UNITY_EDITOR
//            PrefixAssetNameWithPayloadTypeName();
            #endif
        }

        [ContextMenu("Add Type Prefix")]
        protected void PrefixAssetNameWithPayloadTypeName()
        {
            #if UNITY_EDITOR
            var currentName = name;

            var indexOfFirstClosingBracket = name.IndexOf(']');

            if (indexOfFirstClosingBracket != -1)
            {
                currentName = currentName.Substring(startIndex: indexOfFirstClosingBracket + 2);
            }
            
            var path = AssetDatabase.GetAssetPath(GetInstanceID());

            var typeName = GetType().Name;
                
            AssetDatabase.RenameAsset(pathName: path,
                                      newName: $"[{typeName.Replace(oldValue: "Asset", newValue: string.Empty)}] {currentName}");
            
            AssetDatabase.SaveAssets();
            #endif
        }
        
        private void Awake()
        {
            #if DEBUG_DATA_ASSETS
            if (Application.isPlaying)
            {
                Log($"{name} - Awake");
            }
            #endif
        }


        protected virtual void OnEnable()
        {
            #if DEBUG_DATA_ASSETS
            if (Application.isPlaying)
            {
                Log($"{name} - OnEnable");
            }
            #endif
        }


        protected virtual void OnDisable()
        {
            #if DEBUG_DATA_ASSETS
            if (Application.isPlaying && !QuitUtility.Quit.InProgress) 
                Log($"{name} - OnDisable");
            #endif
        }

    }

    [Serializable]
    public abstract class BaseAccessor // -------------------------------------------------------------------------------------------- Processors //
    {

    }
    
    // ----------------------------------------------------------------------------------------------------------------------------------- Getter //

//    public interface IGetter<TType, TAsset, TSocket, TGetter, TSetter>
//        where TAsset : IDataAsset<TType,TAsset, TSocket, TGetter,TSetter>
//        where TSocket : ISocket<TType,TAsset,TSocket,TGetter,TSetter>
//        where TGetter : IGetter<TType,TAsset,TSocket,TGetter,TSetter>
//        where TSetter : ISetter<TType,TAsset,TSocket,TGetter,TSetter>
//    {
//
//        void Get(TAsset asset);
//
//    }
//    
//    // ----------------------------------------------------------------------------------------------------------------------------------- Setter //
//
//    public interface ISetter<TType, 
//                             TAsset, 
//                             TSocket, 
//                             TGetter, 
//                             TSetter>
//        where TAsset : IDataAsset<TType,TAsset, TSocket, TGetter,TSetter>
//        where TSocket : ISocket<TType,TAsset,TSocket,TGetter,TSetter>
//        where TGetter : IGetter<TType,TAsset,TSocket,TGetter,TSetter>
//        where TSetter : ISetter<TType,TAsset,TSocket,TGetter,TSetter>
//    {
//
//        void Get(TAsset asset);
//
//    }

}