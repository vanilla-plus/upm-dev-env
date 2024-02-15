using System.Collections.Generic;

using Newtonsoft.Json.Linq;

using Unity.Services.RemoteConfig;

using UnityEditor;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.JNode
{
    
    [CreateAssetMenu(menuName = "Vanilla/JNode/JNode Asset", fileName = "New JNode Asset")]
    public class JNodeAsset : ScriptableObject
    {
        
        [Header("Settings")]
        public bool parseOnValidate = false;
        public bool parseInputToData       = true;
        public bool parseDataToOutput      = true;
        public bool prettyPrint            = true;
        public bool autoNameAsset = true;
        [Tooltip("If enabled, ")]
        public bool autoUpdateRemoteConfig = false;
        
        [SerializeField]
        public string nameProperty = "Name";
        
        [Space(20.0f)]
        [Header("Input")]
        [TextArea(10,
                  40)]
        public string input;

        [Space(20.0f)]
        [Header("Data Class")]
        [SerializeReference]
        [TypeMenu("green")]
        public JNode data = default;

        [Space(20.0f)]
        [Header("Output")]
        [TextArea(10,
                  40)]
        public string stringOutput;


        protected virtual void OnValidate()
        {
            if (!parseOnValidate) return;

			Parse();
        }

        [ContextMenu("Parse")]
        private void Parse()
        {
	        if (parseInputToData && !string.IsNullOrEmpty(value: input))
	        {
		        data.FromJson(json: input);
	        }
            
	        data.OnValidate();

	        var dataType = data.GetType();

	        if (parseDataToOutput)
	        {
		        stringOutput = data.ToJson(prettyPrint: prettyPrint);
	        }
	        
	        var keyPropInfo = dataType.GetProperty(nameProperty);

	        if (keyPropInfo == null)
	        {
		        Debug.LogError($"Parsing failed - no property with the name [{nameProperty}] was found in the data class.");
		        
		        return;
	        }
	        
	        var keyString = keyPropInfo.GetValue(data).ToString();		// Use me for naming stuff
	        var keyLower  = keyString.ToLower();							// Use me as a key

	        #if UNITY_EDITOR
	        if (autoNameAsset)
	        {
		        var assetPath = AssetDatabase.GetAssetPath(GetInstanceID());

		        AssetDatabase.RenameAsset(pathName: assetPath,
		                                  newName: $"[JNodeAsset] - [{dataType.Name}] - {keyString}");

		        AssetDatabase.SaveAssets();
	        }
	        #endif

//	        if (autoUpdateRemoteConfig)
//	        {
//		        var temp = new JObject(ConfigManager.appConfig.config);
//
//		        var oldEntry = temp.GetValue(keyValue.ToString().ToLower());
//
//		        Debug.Log(oldEntry);
//		        
////                Debug.Log(data.ToJson(prettyPrint));
//
//		        Debug.Log(stringOutput);
//	        }
	        
	        if (autoUpdateRemoteConfig)
	        {
//		        var temp = new JObject(ConfigManager.appConfig.config);
		        var temp = new JObject(RemoteConfigService.Instance.appConfig.config);

		        Debug.Log(temp);

		        temp.Remove(keyLower);

		        Debug.Log(temp);

//		        var newEntry = new JObject(stringOutput);
		        
//		        temp.Add(newEntry);

		        temp.TryAdd(keyLower,
		                    stringOutput);
		        
		        Debug.Log(temp);

		        // No set :( only get
		        
//		        ConfigManager.appConfig.config = temp;

//		        var oldEntry = temp.GetValue(keyValue.ToString().ToLower());

//		        Debug.Log(oldEntry);

//                Debug.Log(data.ToJson(prettyPrint));

//		        Debug.Log(stringOutput);
	        }
        }

    }
}
