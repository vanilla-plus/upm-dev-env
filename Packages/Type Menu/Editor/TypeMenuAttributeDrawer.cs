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

		DrawTypeButton(property: property,
		           position: position,
		           fieldInfo: fieldInfo,
		           filters: GetAllBuiltInTypeRestrictions(fieldInfo: fieldInfo));

		DrawCutButton(property: property,
		              fieldInfo: fieldInfo,
		              position: position);
		
		DrawCopyButton(property: property,
		               fieldInfo: fieldInfo,
		               position: position);
		
		DrawPasteButton(property: property,
		                fieldInfo: fieldInfo,
		                position: position);

		EditorGUI.PropertyField(position: position,
		                        property: property,
		                        label: GUIContent.none,
		                        includeChildren: true);

		EditorGUI.EndProperty();
	}

	// ----------------------------------------------------------------------------------------------------------------------------------- Button //

	private static void DrawTypeButton(SerializedProperty property,
	                               Rect position,
	                               FieldInfo fieldInfo,
	IEnumerable<Func<Type, bool>> filters = null)
	{
		var optionsWidth = CutButtonWidth + CopyButtonWidth + PasteButtonWidth + EditorGUIUtility.standardVerticalSpacing * 5;
		
		position.x      += EditorGUIUtility.labelWidth + EditorGUIUtility.standardVerticalSpacing;
//		position.width -= EditorGUIUtility.labelWidth - 1 * EditorGUIUtility.standardVerticalSpacing;
		position.width  -= EditorGUIUtility.labelWidth + optionsWidth;
		position.height =  EditorGUIUtility.singleLineHeight;

		var colorCache   = GUI.backgroundColor;

		var colorAttribute = fieldInfo.GetCustomAttribute(typeof(TypeMenuAttribute)) as TypeMenuAttribute;

		GUI.backgroundColor = colorAttribute.color;

		var typeSplitString = property.managedReferenceFullTypename.Split(char.Parse(" "));

		var className    = typeSplitString.Length == 1 ? "-" : typeSplitString[1].Split('.').Last();
		var assemblyName = typeSplitString[0];

		if (GUI.Button(position: position,
		               content: new GUIContent(text: className,
		                                       tooltip: $"From [{assemblyName}]")))
		{
//			DrawTypeMenu(property: property,
//			             filters: filters);
			
			var appropriateTypes = GetAppropriateTypes(property: property,
			                                           filters: filters);

			SearchableMenuWindow.ShowWindow("Type Menu",
			                                property: property,
			                                types: appropriateTypes);
		}


		GUI.backgroundColor   = colorCache;
	}

	private const int CutButtonWidth = 48;

	private static void DrawCutButton(SerializedProperty property,
	                                  FieldInfo fieldInfo,
	                                  Rect position)
	{
		position.x      = EditorGUIUtility.currentViewWidth - CutButtonWidth - CopyButtonWidth - PasteButtonWidth - EditorGUIUtility.standardVerticalSpacing * 3;
		position.width  = CutButtonWidth;
		position.height = EditorGUIUtility.singleLineHeight;

		var colorCache  = GUI.backgroundColor;

		var colorAttribute = fieldInfo.GetCustomAttribute(typeof(TypeMenuAttribute)) as TypeMenuAttribute;

		GUI.backgroundColor = colorAttribute.color;

		if (GUI.Button(position: position,
		               content: new GUIContent(text: "Cut",
		                                       tooltip: "Cut")))
		{
			clipboardInstance = DeepClone(property.managedReferenceValue);
			                                                                   
			property.serializedObject.Update();
			property.managedReferenceValue = null; // nullify current reference - is anything else holding onto these refs?
			property.serializedObject.ApplyModifiedProperties();
		}

		GUI.backgroundColor   = colorCache;
	}
	
	private const int CopyButtonWidth = 48;

	private static void DrawCopyButton(SerializedProperty property,
	                                   FieldInfo fieldInfo,
	                                   Rect position)
	{
		position.x      = EditorGUIUtility.currentViewWidth - CopyButtonWidth - PasteButtonWidth - EditorGUIUtility.standardVerticalSpacing * 2;
		position.width  = CopyButtonWidth;
		position.height = EditorGUIUtility.singleLineHeight;

		var colorCache  = GUI.backgroundColor;

		var colorAttribute = fieldInfo.GetCustomAttribute(typeof(TypeMenuAttribute)) as TypeMenuAttribute;

		GUI.backgroundColor = colorAttribute.color;

		if (GUI.Button(position: position,
		               content: new GUIContent(text: "Copy",
		                                       tooltip: "Copy")))
		{
			clipboardInstance = DeepClone(property.managedReferenceValue);
		}

		GUI.backgroundColor   = colorCache;
	}


	private const int PasteButtonWidth = 48;
	
	private static void DrawPasteButton(SerializedProperty property,
	                                    FieldInfo fieldInfo,
	                                    Rect position)
	{
		position.x      = EditorGUIUtility.currentViewWidth - PasteButtonWidth - EditorGUIUtility.standardVerticalSpacing;
		position.width  = PasteButtonWidth;
		position.height = EditorGUIUtility.singleLineHeight;

		var colorCache  = GUI.backgroundColor;

		var colorAttribute = fieldInfo.GetCustomAttribute(typeof(TypeMenuAttribute)) as TypeMenuAttribute;

		GUI.backgroundColor = colorAttribute.color;

		if (GUI.Button(position: position,
		               content: new GUIContent(text: "Paste",
		                                       tooltip: "Paste")))
		{
			if (clipboardInstance != null)
			{
				property.serializedObject.Update();
				property.managedReferenceValue = DeepClone(clipboardInstance); // Clone again to avoid reference issues
				property.serializedObject.ApplyModifiedProperties();
			}
		}

		GUI.backgroundColor   = colorCache;
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
//	
//	private static string GetClipboardSlotDescriptor(int slot, object instance)
//	{
//		if (instance == null) return $"{slot} - Null";
//
//		Type         type      = instance.GetType();
//		
//		const string fieldName = "_Name";
//
//		while (type != null &&
//		       type != typeof(object))
//		{
//			var fieldInfo = type.GetField(name: fieldName,
//			                              bindingAttr: BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
//
//			if (fieldInfo           != null &&
//			    fieldInfo.FieldType == typeof(string))
//			{
//				return $"{slot} - {(string) fieldInfo.GetValue(instance)}";
//			}
//
//			type = type.BaseType; // Move to the base type
//		}
//
//		return $"{slot} - {instance.GetType().Name}"; // Return the type name if the field is not found
//	}
	
	private static string GetClipboardSlotDescriptor(object instance)
	{
		if (instance == null) return string.Empty;

		Type type = instance.GetType();
		
		const string fieldName = "_Name";

		while (type != null &&
		       type != typeof(object))
		{
			var fieldInfo = type.GetField(name: fieldName,
			                              bindingAttr: BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

			if (fieldInfo           != null &&
			    fieldInfo.FieldType == typeof(string))
			{
				return $" - {(string) fieldInfo.GetValue(instance)}";
			}

			type = type.BaseType; // Move to the base type
		}

		return $" - {instance.GetType().Name}"; // Return the type name if the field is not found
	}
//
//	private static void DrawTypeMenu(SerializedProperty property,
//	                             IEnumerable<Func<Type, bool>> filters = null)
//	{
//		var mainMenu = new GenericMenu();
//
////		mainMenu.AddItem(content: new GUIContent($"Clipboard/Copy/{GetClipboardSlotDescriptor(slot: 1, instance: clipboardInstance1)}"), @on: false, func: () => clipboardInstance1 = DeepClone(property.managedReferenceValue));
////		mainMenu.AddItem(content: new GUIContent($"Clipboard/Copy/{GetClipboardSlotDescriptor(slot: 2, instance: clipboardInstance2)}"), @on: false, func: () => clipboardInstance2 = DeepClone(property.managedReferenceValue));
////		mainMenu.AddItem(content: new GUIContent($"Clipboard/Copy/{GetClipboardSlotDescriptor(slot: 3, instance: clipboardInstance3)}"), @on: false, func: () => clipboardInstance3 = DeepClone(property.managedReferenceValue));
////
////		mainMenu.AddItem(content: new GUIContent($"Clipboard/Paste/{GetClipboardSlotDescriptor(slot: 1, instance: clipboardInstance1)}"),
////		                 @on: false,
////		                 func: () =>
////		                       {
////			                       property.serializedObject.Update();
////			                       property.managedReferenceValue = DeepClone(clipboardInstance1); // Clone again to avoid reference issues
////			                       property.serializedObject.ApplyModifiedProperties();
////		                       });
////
////		mainMenu.AddItem(content: new GUIContent($"Clipboard/Paste/{GetClipboardSlotDescriptor(slot: 2, instance: clipboardInstance2)}"),
////		                 @on: false,
////		                 func: () =>
////		                       {
////			                       property.serializedObject.Update();
////			                       property.managedReferenceValue = DeepClone(clipboardInstance2); // Clone again to avoid reference issues
////			                       property.serializedObject.ApplyModifiedProperties();
////		                       });
////
////		mainMenu.AddItem(content: new GUIContent($"Clipboard/Paste/{GetClipboardSlotDescriptor(slot: 3, instance: clipboardInstance3)}"),
////		                 @on: false,
////		                 func: () =>
////		                       {
////			                       property.serializedObject.Update();
////			                       property.managedReferenceValue = DeepClone(clipboardInstance3); // Clone again to avoid reference issues
////			                       property.serializedObject.ApplyModifiedProperties();
////		                       });
//
////		var fieldInfo = GetFieldInfoFromProperty(property);
////		return fieldInfo.GetCustomAttributes(typeof(Chainable), true).Length > 0;
//
//		mainMenu.AddItem(content: new GUIContent("Replace"),
//		                 @on: false,
//		                 func: () =>
//		                       {
//			                       var appropriateTypes = GetAppropriateTypes(property: property,
//			                                                                  filters: filters);
//
//			                       SearchableMenuWindow.ShowWindow("Type Menu",
//			                                                       property: property,
//			                                                       types: appropriateTypes);
//		                       });
//
//		mainMenu.AddItem(content: new GUIContent("Cut"), @on: false, func: () =>
//		                                                                   {
//			                                                                   clipboardInstance = DeepClone(property.managedReferenceValue);
//			                                                                   
//			                                                                   property.serializedObject.Update();
//			                                                                   property.managedReferenceValue = null; // nullify current reference - is anything else holding onto these refs?
//			                                                                   property.serializedObject.ApplyModifiedProperties();
//		                                                                   });
//
//		mainMenu.AddItem(content: new GUIContent("Copy"), @on: false, func: () => clipboardInstance = DeepClone(property.managedReferenceValue));
//
//		if (clipboardInstance != null)
//		{
//			mainMenu.AddItem(content: new GUIContent($"Paste{GetClipboardSlotDescriptor(instance: clipboardInstance)}"),
//			                 @on: false,
//			                 func: () =>
//			                       {
//				                       property.serializedObject.Update();
//				                       property.managedReferenceValue = DeepClone(clipboardInstance); // Clone again to avoid reference issues
//				                       property.serializedObject.ApplyModifiedProperties();
//			                       });
//		}
////		
////		mainMenu.AddItem(new GUIContent("Replace With/Search..."), false, () =>
////		                                                                  {
////			                                                                  var appropriateTypes = GetAppropriateTypes(property, filters);
////			                                                                  SearchableMenuWindow.ShowWindow(property, appropriateTypes);
////		                                                                  });
////
////		mainMenu.AddItem(content: new GUIContent(text: "Replace/Null"),
////		                 @on: false,
////		                 func: () =>
////		                       {
////			                       property.serializedObject.Update();
////			                       property.managedReferenceValue = null;
////			                       property.serializedObject.ApplyModifiedProperties();
////		                       });
////
////		var typeSplitString = property.managedReferenceFieldTypename.Split(' ');
////		var realType        = Type.GetType($"{typeSplitString[1]}, {typeSplitString[0]}");
////
////		if (realType == null)
////		{
////			Debug.LogError($"Can not get field type of managed reference : {property.managedReferenceFieldTypename}");
////
////			return;
////		}
////
////		var appropriateTypes = (
////			                       from type in TypeCache.GetTypesDerivedFrom(parentType: realType)
////			                       where IsValidType(type: type,
////			                                         filters: filters)
////			                       select type).OrderBy(t => t.Name)
////			                                   .ToList();
////
////		var namespaces = appropriateTypes.Select(t => t.Namespace).Distinct().OrderBy(ns => ns);
////
////		foreach (var ns in namespaces)
////		{
////			var typesInNamespace = appropriateTypes.Where(t => t.Namespace == ns).OrderBy(t => t.Name);
////
////			foreach (var type in typesInNamespace)
////			{
////				var menuItemPath = $"Replace/{ns}/{type.Name.Replace(oldChar: '_', newChar: ' ')}";
////
////				AddTypeMenuItem(menu: mainMenu,
////				                path: menuItemPath,
////				                type: type,
////				                property: property);
////			}
////		}
////
//		mainMenu.ShowAsContext();
//	}

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