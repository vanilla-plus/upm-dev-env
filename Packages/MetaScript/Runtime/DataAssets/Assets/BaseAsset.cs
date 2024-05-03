using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataAssets
{
	
	[Serializable]
    public abstract class BaseAsset : ScriptableObject,
                                      ISerializationCallbackReceiver
    {

	    #if UNITY_EDITOR
	    [TextArea(8, 20)] public string Description = "Describe this asset here:\n\n• What does this data do?\n• What scene is it used in?\n• Why is this default value used?";
	    #endif


	    public virtual void Reset()
	    {
		    #if debug
		    Debug.Log($"[{Time.frameCount}] {name}\t=> Reset");
		    #endif
	    }

	    protected virtual void OnValidate()
		{
			#if debug
			Debug.Log($"[{Time.frameCount}] {name}\t=> OnValidate");
			#endif
		}

		protected virtual void Awake()
		{
			#if debug
			Debug.Log($"[{Time.frameCount}] {name}\t=> Awake");
			#endif
		}


		protected virtual void OnEnable()
		{
			#if debug
			Debug.Log($"[{Time.frameCount}] {name}\t=> OnEnable");
			#endif
		}


		protected virtual void OnDisable()
		{
			#if debug
			Debug.Log($"[{Time.frameCount}] {name}\t=> OnDisable");
			#endif
		}
		
		protected virtual void OnDestroy()
		{
			#if debug
			Debug.Log($"[{Time.frameCount}] {name}\t=> OnDestroy");
			#endif
		}


		public virtual void OnBeforeSerialize()
		{
//			#if debug
//			Debug.Log($"[{Time.frameCount}] {name}\t=> OnBeforeSerialize");
//			#endif
		}


		public virtual void OnAfterDeserialize()
		{
//			#if debug
//			Debug.Log($"OnAfterDeserialize");
//			#endif
		}
    }
}
