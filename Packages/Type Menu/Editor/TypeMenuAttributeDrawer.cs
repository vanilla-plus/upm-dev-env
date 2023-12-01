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

		DrawButton(property: property,
		           position: position,
		           filters: GetAllBuiltInTypeRestrictions(fieldInfo: fieldInfo));
		
		EditorGUI.PropertyField(position: position,
		                        property: property,
		                        label: GUIContent.none,
		                        includeChildren: true);

		EditorGUI.EndProperty();
	}

	// ----------------------------------------------------------------------------------------------------------------------------------- Button //

	private static void DrawButton(SerializedProperty property,
	                               Rect position,
	                               IEnumerable<Func<Type, bool>> filters = null)
	{

		position.x      += EditorGUIUtility.labelWidth + 1 * EditorGUIUtility.standardVerticalSpacing;
		position.width  -= EditorGUIUtility.labelWidth - 1 * EditorGUIUtility.standardVerticalSpacing;
		position.height =  EditorGUIUtility.singleLineHeight;

		var indentCache = EditorGUI.indentLevel;
		var colorCache   = GUI.backgroundColor;

		EditorGUI.indentLevel = 0;

		GUI.backgroundColor = new Color(r: 0f,
		                                g: 0.69f,
		                                b: 1f);

		var typeSplitString = property.managedReferenceFullTypename.Split(char.Parse(" "));

		var className    = typeSplitString.Length == 1 ? "-" : typeSplitString[1].Split('.').Last();
		var assemblyName = typeSplitString[0];

		if (GUI.Button(position: position,
		               content: new GUIContent(text: className,
		                                       tooltip: $"From [{assemblyName}]")))
		{
//			Debug.Log(property.FindPropertyRelative("_initialized").boolValue);

			DrawMenu(property: property,
			         filters: filters);
		}

		GUI.backgroundColor   = colorCache;
		EditorGUI.indentLevel = indentCache;
	}

	
	// ------------------------------------------------------------------------------------------------------------------------------------- Menu //
	
	private static object clonedInstance1;
	private static object clonedInstance2;
	private static object clonedInstance3;
	
	private static object DeepClone(object obj)
	{
		if (obj == null)
			return null;

		var serialized = JsonUtility.ToJson(obj);
		
		return JsonUtility.FromJson(json: serialized, type: obj.GetType());
	}
	
	private static void DrawMenu(SerializedProperty property,
	                             IEnumerable<Func<Type, bool>> filters = null)
	{
		var mainMenu = new GenericMenu();

		mainMenu.AddItem(content: new GUIContent("Clipboard/Copy/1"), @on: false, func: () => clonedInstance1 = DeepClone(property.managedReferenceValue));
		mainMenu.AddItem(content: new GUIContent("Clipboard/Copy/2"), @on: false, func: () => clonedInstance2 = DeepClone(property.managedReferenceValue));
		mainMenu.AddItem(content: new GUIContent("Clipboard/Copy/3"), @on: false, func: () => clonedInstance3 = DeepClone(property.managedReferenceValue));

		mainMenu.AddItem(content: new GUIContent("Clipboard/Paste/1"),
		                 @on: false,
		                 func: () =>
		                       {
			                       if (clonedInstance1 == null)
			                       {
				                       Debug.LogWarning("Clipboard 1 is null");
			                       }
			                       else
			                       {
				                       property.serializedObject.Update();
				                       property.managedReferenceValue = DeepClone(clonedInstance1); // Clone again to avoid reference issues
				                       property.serializedObject.ApplyModifiedProperties();
			                       }
		                       });

		mainMenu.AddItem(content: new GUIContent("Clipboard/Paste/2"),
		                 @on: false,
		                 func: () =>
		                       {
			                       if (clonedInstance2 == null)
			                       {
				                       Debug.LogWarning("Clipboard 2 is null");
			                       }

			                       {
				                       property.serializedObject.Update();
				                       property.managedReferenceValue = DeepClone(clonedInstance2); // Clone again to avoid reference issues
				                       property.serializedObject.ApplyModifiedProperties();
			                       }
		                       });

		mainMenu.AddItem(content: new GUIContent("Clipboard/Paste/3"),
		                 @on: false,
		                 func: () =>
		                       {
			                       if (clonedInstance3 != null)
			                       {
				                       Debug.LogWarning("Clipboard 3 is null");
			                       }
			                       else
			                       {
				                       property.serializedObject.Update();
				                       property.managedReferenceValue = DeepClone(clonedInstance3); // Clone again to avoid reference issues
				                       property.serializedObject.ApplyModifiedProperties();
			                       }
		                       });

		mainMenu.AddItem(content: new GUIContent(text: "Replace/Null"),
		                 @on: false,
		                 func: () =>
		                       {
			                       property.serializedObject.Update();
			                       property.managedReferenceValue = null;
			                       property.serializedObject.ApplyModifiedProperties();
		                       });

		var typeSplitString = property.managedReferenceFieldTypename.Split(' ');
		var realType        = Type.GetType($"{typeSplitString[1]}, {typeSplitString[0]}");

		if (realType == null)
		{
			Debug.LogError($"Can not get field type of managed reference : {property.managedReferenceFieldTypename}");

			return;
		}

		var appropriateTypes = (
			                       from type in TypeCache.GetTypesDerivedFrom(parentType: realType)
			                       where IsValidType(type: type,
			                                         filters: filters)
			                       select type).OrderBy(t => t.Name)
			                                   .ToList();

		var namespaces = appropriateTypes.Select(t => t.Namespace).Distinct().OrderBy(ns => ns);

		foreach (var ns in namespaces)
		{
			var typesInNamespace = appropriateTypes.Where(t => t.Namespace == ns).OrderBy(t => t.Name);

			foreach (var type in typesInNamespace)
			{
				var menuItemPath = $"Replace/{ns}/{type.Name.Replace(oldChar: '_', newChar: ' ')}";

				AddTypeMenuItem(menu: mainMenu,
				                path: menuItemPath,
				                type: type,
				                property: property);
			}
		}

		mainMenu.ShowAsContext();
	}


	private static bool IsValidType(Type type,
	                                IEnumerable<Func<Type, bool>> filters) => !type.IsSubclassOf(typeof(UnityEngine.Object))                  &&
	                                                                          !type.IsAbstract                                                &&
	                                                                          !type.ContainsGenericParameters                                 &&
	                                                                          (!type.IsClass || type.GetConstructor(Type.EmptyTypes) != null) &&
	                                                                          (filters ?? Array.Empty<Func<Type, bool>>()).All(f => f == null || f.Invoke(type));


private static void AddTypeMenuItem(GenericMenu menu,
                                    string path,
                                    Type type,
                                    SerializedProperty property) => menu.AddItem(content: new GUIContent(text: path.Replace(oldChar: '.',
                                                                                                                            newChar: '/')),
                                                                                 @on: false,
                                                                                 func: obj =>
                                                                                       {
	                                                                                       var args     = (AssignInstanceArgs) obj;
	                                                                                       var instance = Activator.CreateInstance(args.type);
	                                                                                       var p        = args.property;
	                                                                                       p.serializedObject.Update();
	                                                                                       p.managedReferenceValue = instance;
	                                                                                       p.serializedObject.ApplyModifiedProperties();
                                                                                       },
                                                                                 userData: new AssignInstanceArgs(type: type,
                                                                                                                  property: property));

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