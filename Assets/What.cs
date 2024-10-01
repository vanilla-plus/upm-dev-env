//using UnityEngine;
//using UnityEditor;
//using System;
//using System.Linq;
//using System.Reflection;
//
//using Vanilla.MetaScript;
//
//public class CustomAddComponentMenu : MonoBehaviour
//{
//	[MenuItem("Custom/Add Component")]
//	private static void AddCustomComponentMenu()
//	{
//		// Get the currently selected GameObject in the Unity Editor
//		GameObject selectedObject = Selection.activeGameObject;
//
//		if (selectedObject == null)
//		{
//			Debug.LogWarning("No GameObject selected.");
//			return;
//		}
//
//		// Use reflection to get all types deriving from BaseType
//		var derivedTypes = Assembly.GetAssembly(typeof(MetaTask)).GetTypes()
//		                           .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(MetaTask)));
//
//		foreach (var type in derivedTypes)
//		{
//			// Create a menu item for each derived type
//			string menuItemName = "Custom/Add Component/" + type.Name;
//			int    controlId    = GUIUtility.GetControlID(FocusType.Passive);
//
//			// Lambda to handle menu item click
//			EditorApplication.delayCall += () =>
//			                               {
//				                               if (EditorGUIUtility.GetObjectPickerControlID() == controlId)
//				                               {
//					                               Undo.AddComponent(selectedObject, type);
//				                               }
//			                               };
//
//			// Add the menu item
//			Menu.AddItem(new GUIContent(menuItemName), false, () => {
//				                                                  EditorGUIUtility.ShowObjectPicker<MonoBehaviour>(null, false, "", controlId);
//			                                                  });
//		}
//	}
//}