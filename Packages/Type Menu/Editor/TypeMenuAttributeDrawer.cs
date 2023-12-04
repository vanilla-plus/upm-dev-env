using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using UnityEditor; 

using UnityEngine;

using Vanilla.TypeMenu;

[CustomPropertyDrawer(type: typeof(TypeMenuAttribute))]
public class TypeMenuAttributeDrawer : PropertyDrawer
{

	// ----------------------------------------------------------------------------------------------------------------------------------- Layout //

	
	
	public override float GetPropertyHeight(SerializedProperty property,
	                                        GUIContent label) => EditorGUI.GetPropertyHeight(property: property,
	                                                                                         includeChildren: true);

	// ----------------------------------------------------------------------------------------------------------------------------------- Drawer //

	public override void OnGUI(Rect position,
	                           SerializedProperty property,
	                           GUIContent label)
	{

		EditorGUI.BeginProperty(totalPosition: position,
		                        label: label,
		                        property: property);

		EditorGUI.LabelField(position: new Rect(x: position.x,
		                                        y: position.y,
		                                        width: position.width,
		                                        height: EditorGUIUtility.singleLineHeight),
		                     label: label);
		
		var colorCache = GUI.backgroundColor;

		var colorAttribute = fieldInfo.GetCustomAttribute(typeof(TypeMenuAttribute)) as TypeMenuAttribute;

		GUI.backgroundColor = colorAttribute.color;

		DrawTypeButton(property: property,
		           position: position,
		           filters: GetAllBuiltInTypeRestrictions(fieldInfo: fieldInfo));

		DrawCutButton(property: property,
		              position: position);
		
		DrawCopyButton(property: property,
		               position: position);
		
		DrawPasteButton(property: property,
		                position: position);

		GUI.backgroundColor = colorCache;

		EditorGUI.PropertyField(position: position,
		                        property: property,
		                        label: GUIContent.none,
		                        includeChildren: true);
		
		EditorGUI.EndProperty();
	}

	// ----------------------------------------------------------------------------------------------------------------------------------- Button //


	private static void DrawTypeButton(SerializedProperty property,
	                                   Rect position,
	                                   IEnumerable<Func<Type, bool>> filters = null)
	{
		var optionsWidth = CutButtonWidth + CopyButtonWidth + PasteButtonWidth + EditorGUIUtility.standardVerticalSpacing * 5;

		position.x += EditorGUIUtility.labelWidth + EditorGUIUtility.standardVerticalSpacing;

//		position.width -= EditorGUIUtility.labelWidth - 1 * EditorGUIUtility.standardVerticalSpacing;
		position.width  -= EditorGUIUtility.labelWidth + optionsWidth;
		position.height =  EditorGUIUtility.singleLineHeight;

		var typeSplitString = property.managedReferenceFullTypename.Split(char.Parse(" "));

		var className = typeSplitString.Length == 1 ?
			                "-" :
			                typeSplitString[1].Split('.').Last();

		var assemblyName = typeSplitString[0];

		if (!GUI.Button(position: position,
		                content: new GUIContent(text: className,
		                                        tooltip: $"From [{assemblyName}]"))) return;

		var appropriateTypes = GetAppropriateTypes(property: property,
		                                           filters: filters);

		SearchableMenuWindow.ShowWindow(title: "Type Menu",
		                                property: property,
		                                types: appropriateTypes);
	}


	private const int CutButtonWidth = 48;

	private static void DrawCutButton(SerializedProperty property,
	                                  Rect position)
	{
		position.x      = EditorGUIUtility.currentViewWidth - CutButtonWidth - CopyButtonWidth - PasteButtonWidth - EditorGUIUtility.standardVerticalSpacing * 3;
		position.width  = CutButtonWidth;
		position.height = EditorGUIUtility.singleLineHeight;

		if (!GUI.Button(position: position,
		                content: new GUIContent(text: "Cut",
		                                        tooltip: "Cut"))) return;

		clipboardInstance = DeepClone(property.managedReferenceValue);
			                                                                   
		property.serializedObject.Update();
		property.managedReferenceValue = null; // nullify current reference - is anything else holding onto these refs?
		property.serializedObject.ApplyModifiedProperties();
	}
	
	private const int CopyButtonWidth = 48;

	private static void DrawCopyButton(SerializedProperty property,
	                                   Rect position)
	{
		position.x      = EditorGUIUtility.currentViewWidth - CopyButtonWidth - PasteButtonWidth - EditorGUIUtility.standardVerticalSpacing * 2;
		position.width  = CopyButtonWidth;
		position.height = EditorGUIUtility.singleLineHeight;

		if (!GUI.Button(position: position,
		                content: new GUIContent(text: "Copy",
		                                        tooltip: "Copy"))) return;

		clipboardInstance = DeepClone(property.managedReferenceValue);
	}


	private const int PasteButtonWidth = 48;
	
	private static void DrawPasteButton(SerializedProperty property,
	                                    Rect position)
	{
		position.x      = EditorGUIUtility.currentViewWidth - PasteButtonWidth - EditorGUIUtility.standardVerticalSpacing;
		position.width  = PasteButtonWidth;
		position.height = EditorGUIUtility.singleLineHeight;

		if (!GUI.Button(position: position,
		                content: new GUIContent(text: "Paste",
		                                        tooltip: "Paste"))) return;

		if (clipboardInstance == null) return;

		property.serializedObject.Update();
		property.managedReferenceValue = DeepClone(clipboardInstance); // Clone again to avoid reference issues
		property.serializedObject.ApplyModifiedProperties();
	}

	// ------------------------------------------------------------------------------------------------------------------------------------- Menu //

	private static object clipboardInstance;
	private static object clipboardInstance2;
	private static object clipboardInstance3;

	private static object DeepClone(object obj)
	{
		if (obj == null)
			return null;

		var serialized = JsonUtility.ToJson(obj);
		
		return JsonUtility.FromJson(json: serialized, type: obj.GetType());
	}

	private static List<Type> GetAppropriateTypes(SerializedProperty property, IEnumerable<Func<Type, bool>> filters)
	{
		var typeSplitString = property.managedReferenceFieldTypename.Split(' ');
		var realType        = Type.GetType($"{typeSplitString[1]}, {typeSplitString[0]}");
		if (realType == null)
		{
			Debug.LogError($"Can not get field type of managed reference : {property.managedReferenceFieldTypename}");
			return new List<Type>();
		}

		return (from type in TypeCache.GetTypesDerivedFrom(realType)
		        where IsValidType(type: type, filters: filters)
		        select type).OrderBy(t => t.Name).ToList();
	}

	private static bool IsValidType(Type type,
	                                IEnumerable<Func<Type, bool>> filters) => !type.IsSubclassOf(typeof(UnityEngine.Object))                  &&
	                                                                          !type.IsAbstract                                                &&
	                                                                          !type.ContainsGenericParameters                                 &&
	                                                                          (!type.IsClass || type.GetConstructor(Type.EmptyTypes) != null) &&
	                                                                          (filters ?? Array.Empty<Func<Type, bool>>()).All(f => f == null || f.Invoke(type));

// ------------------------------------------------------------------------------------------------------------------------------------- Data //

	private readonly struct AssignInstanceArgs
	{

		public AssignInstanceArgs(Type type,
		                          SerializedProperty property)
		{
			this.type     = type;
			this.property = property;
		}


		public readonly SerializedProperty property;
		public readonly Type               type;

	}

	// --------------------------------------------------------------------------------------------------------------------- Type Filter Creation //


	private static IEnumerable<Func<Type, bool>> GetAllBuiltInTypeRestrictions(FieldInfo fieldInfo)
	{
		var result = new List<Func<Type, bool>>();

		foreach (var a in fieldInfo.GetCustomAttributes(inherit: false))
		{
			switch (a)
			{
				case Only includeTypes:
					result.Add(item: TypeIsSubclassOrEqualOrHasInterface(types: includeTypes.Types));
					continue;

				case Except excludeTypes:
					result.Add(item: type => !TypeIsSubclassOrEqualOrHasInterface(types: excludeTypes.Types).Invoke(type));
					continue;
			}
		}

		return result;
	}

	private static Func<Type, bool> TypeIsSubclassOrEqualOrHasInterface(IEnumerable<Type> types) 
		=> type 
			   => types.Any(c => c.IsInterface ?
				                     type.GetInterface(name: c.ToString()) != null :
				                     type.IsSubclassOf(c: c) || type == c);

}