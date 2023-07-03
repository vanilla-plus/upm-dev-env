#if DEBUG_VANILLA && DATA_ASSETS
#define debug
#endif

#if UNITY_EDITOR
using UnityEditor;
#endif

using System;

using UnityEngine;

using Vanilla.TypeMenu;

using static UnityEngine.Debug;

namespace Vanilla.DataAssets.Three
{

	[Serializable]
	public abstract class GenericAsset<TType, TSource> : ScriptableObject
		where TSource : GenericSource<TType>
	{

		#if debug
	    [SerializeField]
	    private bool _enabled;
	    public bool enabled
	    {
		    get => _enabled;
		    set
		    {
			    if (_enabled == value) return;

			    _enabled = value;

			    Log($"[{name}] [{(_enabled ? "enabled" : "disabled")}]");
		    }
	    }
		#endif

		[SerializeReference]
		[TypeMenu]
		public TSource source;

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

			AssetDatabase.RenameAsset(pathName: path,
			                          newName: $"[{typeof(TType).Name.Replace(oldValue: "Asset", newValue: string.Empty)}] {currentName}");

			AssetDatabase.SaveAssets();
			#endif
		}


		protected virtual void OnValidate()
		{
			#if UNITY_EDITOR
			#if debug
		    Log($"[{name}] OnValidate");
			#endif

		    if (!Application.isPlaying) return;
			
		    source.Validate();
			#endif
		}


		public virtual async void Awake()
		{
			#if debug
		    Log($"[{name}] Awake");
			#endif
		}


		public virtual async void OnDestroy()
		{
			#if debug
		    Log($"[{name}] OnDestroy");
			#endif
		}


		protected virtual async void OnEnable()
		{
			#if debug
	        Log($"[{name}] OnEnable start");
			#endif

			#if debug
	        enabled = true;
			#endif

			if (source != null)
			{
				await source.Initialize();
			}

			#if debug
	        Log($"[{name}] OnEnable end");
			#endif
		}


		protected virtual async void OnDisable()
		{
			#if debug
	        Log($"[{name}] OnDisable start");
			#endif

			#if debug
	        enabled = false;
			#endif

			if (source != null)
			{
				await source.DeInitialize();
			}

			#if debug
	        Log($"[{name}] OnDisable end");
			#endif
		}

	}

}