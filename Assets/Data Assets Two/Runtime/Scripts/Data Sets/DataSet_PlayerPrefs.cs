//using System;
//using System.Collections.Generic;
//
//using UnityEngine;
//
//using static UnityEngine.Debug;
//
//namespace Vanilla.DataAssets
//{
//
//	[CreateAssetMenu(fileName = "New PlayerPrefs Data Set",
//	                 menuName = "Vanilla/Data Sets/PlayerPrefs")]
//	[Serializable]
//	public class DataSet_PlayerPrefs : DataSet_Base
//	{
//
//		public List<BoolAsset>    bools    = new List<BoolAsset>();
//		public List<FloatAsset>   floats   = new List<FloatAsset>();
//		public List<IntAsset>     ints     = new List<IntAsset>();
//		public List<StringAsset>  strings  = new List<StringAsset>();
//		public List<Vector2Asset> vector2s = new List<Vector2Asset>();
//		public List<Vector3Asset> vector3s = new List<Vector3Asset>();
//		
//		[ContextMenu(itemName: "Load")]
//		public void Load()
//		{
//
//			// Bools
//
//			foreach (var a in bools)
//			{
//				if (PlayerPrefs.HasKey(key: a.name))
//				{
//					a.value = PlayerPrefs.GetInt(key: a.name) == 1;
//				}
//				else
//				{
//					#if DEBUG_DATA_ASSETS
//					LogWarning(message: $"No saved bool value with the key [{a.name}] could be found on this device.");
//					#endif
//				}
//			}
//
//			// Floats
//
//			foreach (var a in floats)
//			{
//				if (PlayerPrefs.HasKey(key: a.name))
//				{
//					a.value = PlayerPrefs.GetFloat(key: a.name);
//				}
//				else
//				{
//					#if DEBUG_DATA_ASSETS
//					LogWarning(message: $"No saved float value with the key [{a.name}] could be found on this device.");
//					#endif
//				}
//			}
//
//			// Ints
//
//			foreach (var a in ints)
//			{
//				if (PlayerPrefs.HasKey(key: a.name))
//				{
//					a.value = PlayerPrefs.GetInt(key: a.name);
//				}
//				else
//				{
//					#if DEBUG_DATA_ASSETS
//					LogWarning(message: $"No saved int value with the key [{a.name}] could be found on this device.");
//					#endif
//				}
//			}
//
//			// Strings
//
//			foreach (var a in strings)
//			{
//				if (PlayerPrefs.HasKey(key: a.name))
//				{
//					a.value = PlayerPrefs.GetString(key: a.name);
//				}
//				else
//				{
//					#if DEBUG_DATA_ASSETS
//					LogWarning(message: $"No saved string value with the key [{a.name}] could be found on this device.");
//					#endif
//				}
//			}
//
//			// Vector2
//
//			foreach (var a in vector2s)
//			{
//
//				// X
//
//				var newVector2 = Vector2.zero;
//
//				var key = $"{a.name}{DataAssetsUtility.Component_X}";
//
//				if (PlayerPrefs.HasKey(key: key))
//				{
//					newVector2.x = PlayerPrefs.GetFloat(key: key);
//				}
//				else
//				{
//					#if DEBUG
//					LogWarning(message: $"No saved vector2 x value with the key [{key}] could be found on this device.");
//					#endif
//				}
//
//				// Y
//
//				key = $"{a.name}{DataAssetsUtility.Component_Y}";
//
//				if (PlayerPrefs.HasKey(key: key))
//				{
//					newVector2.y = PlayerPrefs.GetFloat(key: key);
//				}
//				else
//				{
//					#if DEBUG
//					LogWarning(message: $"No saved vector2 y value with the key [{key}] could be found on this device.");
//					#endif
//				}
//
//				a.value = newVector2;
//			}
//
//			// Vector3
//
//			foreach (var a in vector3s)
//			{
//
//				// X
//
//				var newVector3 = Vector3.zero;
//
//				var key = $"{a.name}{DataAssetsUtility.Component_X}";
//
//				if (PlayerPrefs.HasKey(key: key))
//				{
//					newVector3.x = PlayerPrefs.GetFloat(key: key);
//				}
//				else
//				{
//					#if DEBUG
//					LogWarning(message: $"No saved vector3 x value with the key [{key}] could be found on this device.");
//					#endif
//				}
//
//				// Y
//
//				key = $"{a.name}{DataAssetsUtility.Component_Y}";
//
//				if (PlayerPrefs.HasKey(key: key))
//				{
//					newVector3.y = PlayerPrefs.GetFloat(key: key);
//				}
//				else
//				{
//					#if DEBUG
//					LogWarning(message: $"No saved vector3 y value with the key [{key}] could be found on this device.");
//					#endif
//				}
//
//				// Z
//
//				key = $"{a.name}{DataAssetsUtility.Component_Z}";
//
//				if (PlayerPrefs.HasKey(key: key))
//				{
//					newVector3.z = PlayerPrefs.GetFloat(key: key);
//				}
//				else
//				{
//					#if DEBUG
//					LogWarning(message: $"No saved vector3 z value with the key [{key}] could be found on this device.");
//					#endif
//				}
//
//				a.value = newVector3;
//			}
//
//		}
//
//
//		[ContextMenu(itemName: "Save")]
//		public void Save()
//		{
//
//			// Bools
//
//			foreach (var a in bools)
//			{
//				PlayerPrefs.SetInt(key: a.name,
//				                   value: a.value ?
//					                          1 :
//					                          0);
//			}
//
//			// Floats
//
//			foreach (var a in floats)
//			{
//				PlayerPrefs.SetFloat(key: a.name,
//				                     value: a.value);
//			}
//
//			// Ints
//
//			foreach (var a in ints)
//			{
//				PlayerPrefs.SetInt(key: a.name,
//				                   value: a.value);
//			}
//
//			// Strings
//
//			foreach (var a in strings)
//			{
//				PlayerPrefs.SetString(key: a.name,
//				                      value: a.value);
//			}
//
//			// Vector2
//
//			foreach (var a in vector2s)
//			{
//
//				// X
//
//				PlayerPrefs.SetFloat(key: $"{a.name}{DataAssetsUtility.Component_X}",
//				                     value: a.value.x);
//
//				// Y
//
//				PlayerPrefs.SetFloat(key: $"{a.name}{DataAssetsUtility.Component_Y}",
//				                     value: a.value.y);
//			}
//
//			// Vector3
//
//			foreach (var a in vector3s)
//			{
//
//				// X
//
//				PlayerPrefs.SetFloat(key: $"{a.name}{DataAssetsUtility.Component_X}",
//				                     value: a.value.x);
//
//				// Y
//
//				PlayerPrefs.SetFloat(key: $"{a.name}{DataAssetsUtility.Component_Y}",
//				                     value: a.value.y);
//
//				// Z
//
//				PlayerPrefs.SetFloat(key: $"{a.name}{DataAssetsUtility.Component_Z}",
//				                     value: a.value.z);
//			}
//
//		}
//
//	}
//
//}