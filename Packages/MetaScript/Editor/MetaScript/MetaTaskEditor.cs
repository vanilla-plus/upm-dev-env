using UnityEngine;
using UnityEditor;

namespace Vanilla.MetaScript.Editor
{

	[CustomEditor(typeof(MetaTask))]
	public class MetaTaskEditor : UnityEditor.Editor
	{

		private SerializedProperty _validProperty;
//		private SerializedProperty myPropertyProperty;

		private void OnEnable()
		{
			_validProperty    = serializedObject.FindProperty("_valid");
//			myPropertyProperty = serializedObject.FindProperty("myProperty");
		}

		public override void OnInspectorGUI()
		{
			// Update the serializedObject
			serializedObject.Update();

			// Draw the default inspector fields
			DrawDefaultInspector();

			// Draw additional custom fields or modify existing fields
			_validProperty.stringValue = EditorGUILayout.TextField("Valid?", _validProperty.stringValue);
//			myPropertyProperty.intValue = EditorGUILayout.IntField("My Property", myPropertyProperty.intValue);

			// Apply changes if the script is modified
			if (GUI.changed)
			{
				serializedObject.ApplyModifiedProperties();
			}
		}

	}

}