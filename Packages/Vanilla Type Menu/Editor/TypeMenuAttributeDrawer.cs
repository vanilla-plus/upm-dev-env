//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//
//using UnityEditor; 
//
//using UnityEngine;
//
//using Vanilla.Interfaces;
//using Vanilla.TypeMenu;
//
//[CustomPropertyDrawer(type: typeof(TypeMenuAttribute))]
//public class TypeMenuAttributeDrawer : PropertyDrawer
//{
//
//	// ----------------------------------------------------------------------------------------------------------------------------------- Layout //
//
//	
//	
//	public override float GetPropertyHeight(SerializedProperty property,
//	                                        GUIContent label) => EditorGUI.GetPropertyHeight(property: property,
//	                                                                                         includeChildren: true);
//
//	// ----------------------------------------------------------------------------------------------------------------------------------- Drawer //
//
//	public override void OnGUI(Rect position,
//	                           SerializedProperty property,
//	                           GUIContent label)
//	{
//
//		EditorGUI.BeginProperty(totalPosition: position,
//		                        label: label,
//		                        property: property);
//
//		EditorGUI.LabelField(position: new Rect(x: position.x,
//		                                        y: position.y,
//		                                        width: position.width,
//		                                        height: EditorGUIUtility.singleLineHeight),
//		                     label: label);
//
//		DrawButton(property: property,
//		           position: position,
//		           filters: GetAllBuiltInTypeRestrictions(fieldInfo: fieldInfo));
//		
//		EditorGUI.PropertyField(position: position,
//		                        property: property,
//		                        label: GUIContent.none,
//		                        includeChildren: true);
//
//		EditorGUI.EndProperty();
//	}
//
//	// ----------------------------------------------------------------------------------------------------------------------------------- Button //
//
//	private static void DrawButton(SerializedProperty property,
//	                               Rect position,
//	                               IEnumerable<Func<Type, bool>> filters = null)
//	{
//
//		position.x      += EditorGUIUtility.labelWidth + 1 * EditorGUIUtility.standardVerticalSpacing;
//		position.width  -= EditorGUIUtility.labelWidth - 1 * EditorGUIUtility.standardVerticalSpacing;
//		position.height =  EditorGUIUtility.singleLineHeight;
//
//		var indentCache = EditorGUI.indentLevel;
//		var colorCache   = GUI.backgroundColor;
//
//		EditorGUI.indentLevel = 0;
//
//		GUI.backgroundColor = new Color(r: 0.666f, g: 0.666f, b: 0.75f, a: 1.0f);
//
//		var typeSplitString = property.managedReferenceFullTypename.Split(char.Parse(" "));
//
//		var className    = typeSplitString.Length == 1 ? "-" : typeSplitString[1].Split('.').Last();
//		var assemblyName = typeSplitString[0];
//
//		if (GUI.Button(position: position,
//		               content: new GUIContent(text: className,
//		                                       tooltip: $"From [{assemblyName}]")))
//		{
//			DrawMenu(property: property,
//			         filters: filters);
//		}
//
//		GUI.backgroundColor   = colorCache;
//		EditorGUI.indentLevel = indentCache;
//	}
//
//	// ------------------------------------------------------------------------------------------------------------------------------------- Menu //
//
//
//	private static void DrawMenu(SerializedProperty property,
//	                             IEnumerable<Func<Type, bool>> filters = null)
//	{
//		var menu = new GenericMenu();
//
//		menu.AddItem(content: new GUIContent("Default"),
//		             @on: false,
//		             func: default);
//
//		// This is the '-' entry up top, which sets the reference to null if chosen
//		menu.AddItem(content: new GUIContent("    ▪ -"),
//		             @on: false,
//		             func: () =>
//		                   {
//			                   property.serializedObject.Update();
//			                   property.managedReferenceValue = null;
//			                   property.serializedObject.ApplyModifiedProperties();
//		                   });
//
//		var typeSplitString = property.managedReferenceFieldTypename.Split(char.Parse(" "));
//
//		//  Which type is this?
//		var realType = Type.GetType($"{typeSplitString[1]}, {typeSplitString[0]}");
//
//		if (realType == null)
//		{
//			Debug.LogError($"Can not get field type of managed reference : {property.managedReferenceFieldTypename}");
//
//			return;
//		}
//
//		var appropriateTypes = (
//			                       from type in TypeCache.GetTypesDerivedFrom(parentType: realType)
//			                       where !type.IsSubclassOf(c: typeof(UnityEngine.Object))
//			                       where !type.IsAbstract
//			                       where !type.ContainsGenericParameters
//			                       where !type.IsClass || type.GetConstructor(types: Type.EmptyTypes) != null
//			                       where (filters ?? Array.Empty<Func<Type, bool>>()).All(f => f == null || f.Invoke(type))
//			                       select type).ToList();
//
//		// Coalesce all the assemblies so we have no doubles 
//		var assemblies = new HashSet<Assembly>();
//
//		foreach (var t in appropriateTypes)
//		{
//			assemblies.Add(item: t.Assembly);
//		}
//
//		// Iterate through each assembly, adding a non-selectable label followed by the valid types from that assembly
//		foreach (var a in assemblies)
//		{
//			// Assembly label
//			menu.AddItem(content: new GUIContent(a.ToString().Split('(',
//			                                                        ',')[0]),
//			             @on: false,
//			             func: default);
//
//			// Selectable menu options
//			foreach (var t in appropriateTypes.Where(t => t.Assembly == a))
//			{
//				menu.AddItem(content: new GUIContent(text: "    ▪ " +
//				                                           t.Name.Replace(oldChar: '_',
//				                                                          newChar: ' ')),
//				             on: false,
//				             func: obj =>
//				                   {
//					                   var args = (AssignInstanceArgs) obj;
//
//					                   var instance = Activator.CreateInstance(args.type);
//
//					                   var p = args.property;
//
//					                   p.serializedObject.Update();
//					                   p.managedReferenceValue = instance;
//					                   p.serializedObject.ApplyModifiedProperties();
//				                   },
//				             userData: new AssignInstanceArgs(type: t,
//				                                              property: property));
//			}
//		}
//
//		menu.ShowAsContext();
//
//		var target = property.serializedObject.targetObject as IValidatable;
//
////		Debug.Log(property.serializedObject.targetObject.ToString());
//
////		Debug.Log(target?.ToString());
//		
//		target?.Validate();
//		
////		
////		
////		var typeMenuExchangable  = property.serializedObject.targetObject as ITypeMenuItem;
////		
////		typeMenuExchangable.Exchange();
//	}
//
//
//	// ------------------------------------------------------------------------------------------------------------------------------------- Data //
//
//	private readonly struct AssignInstanceArgs
//	{
//
//		public AssignInstanceArgs(Type type,
//		                          SerializedProperty property)
//		{
//			this.type     = type;
//			this.property = property;
//		}
//
//
//		public readonly SerializedProperty property;
//		public readonly Type               type;
//
//	}
//
//	// --------------------------------------------------------------------------------------------------------------------- Type Filter Creation //
//
//
//	private static IEnumerable<Func<Type, bool>> GetAllBuiltInTypeRestrictions(FieldInfo fieldInfo)
//	{
//		var result = new List<Func<Type, bool>>();
//
//		foreach (var a in fieldInfo.GetCustomAttributes(inherit: false))
//		{
//			switch (a)
//			{
//				case Only includeTypes:
//					result.Add(item: TypeIsSubclassOrEqualOrHasInterface(types: includeTypes.Types));
//					continue;
//
//				case Except excludeTypes:
//					result.Add(item: type => !TypeIsSubclassOrEqualOrHasInterface(types: excludeTypes.Types).Invoke(type));
//					continue;
//			}
//		}
//
//		return result;
//	}
//
//	private static Func<Type, bool> TypeIsSubclassOrEqualOrHasInterface(IEnumerable<Type> types) 
//		=> type 
//			   => types.Any(c => c.IsInterface ?
//				                     type.GetInterface(name: c.ToString()) != null :
//				                     type.IsSubclassOf(c: c) || type == c);
//
//}

// ----------------------------------------------------------------------------------------------------------------------------------------- Backup!

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

		GUI.backgroundColor = new Color(r: 0.666f, g: 0.666f, b: 0.75f, a: 1.0f);

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


	private static void DrawMenu(SerializedProperty property,
	                             IEnumerable<Func<Type, bool>> filters = null)
	{
		var menu = new GenericMenu();

		menu.AddItem(content: new GUIContent("Default"),
		             @on: false,
		             func: default);

		// This is the '-' entry up top, which sets the reference to null if chosen
		menu.AddItem(content: new GUIContent("    ▪ -"),
		             @on: false,
		             func: () =>
		                   {
			                   property.serializedObject.Update();
			                   property.managedReferenceValue = null;
			                   property.serializedObject.ApplyModifiedProperties();
		                   });

		var typeSplitString = property.managedReferenceFieldTypename.Split(char.Parse(" "));

		//  Which type is this?
		var realType = Type.GetType($"{typeSplitString[1]}, {typeSplitString[0]}");

		if (realType == null)
		{
			Debug.LogError($"Can not get field type of managed reference : {property.managedReferenceFieldTypename}");

			return;
		}

		var appropriateTypes = (
			                       from type in TypeCache.GetTypesDerivedFrom(parentType: realType)
			                       where !type.IsSubclassOf(c: typeof(UnityEngine.Object))
			                       where !type.IsAbstract
			                       where !type.ContainsGenericParameters
			                       where !type.IsClass || type.GetConstructor(types: Type.EmptyTypes) != null
			                       where (filters ?? Array.Empty<Func<Type, bool>>()).All(f => f == null || f.Invoke(type))
			                       select type).ToList();

		// Coalesce all the assemblies so we have no doubles 
		var assemblies = new HashSet<Assembly>();

		foreach (var t in appropriateTypes)
		{
			assemblies.Add(item: t.Assembly);
		}
		
		// Iterate through each assembly, adding a non-selectable label followed by the valid types from that assembly
		foreach (var a in assemblies)
		{
			// Assembly label
			menu.AddItem(content: new GUIContent(a.ToString().Split('(',
			                                                        ',')[0]),
			             @on: false,
			             func: default);

			// Selectable menu options
			foreach (var t in appropriateTypes.Where(t => t.Assembly == a))
			{
				menu.AddItem(content: new GUIContent(text: "    ▪ " +
				                                           t.Name.Replace(oldChar: '_',
				                                                          newChar: ' ')),
				             on: false,
				             func: obj =>
				                   {
					                   var args = (AssignInstanceArgs) obj;

					                   var instance = Activator.CreateInstance(args.type);

					                   var p = args.property;

					                   // I've done some dumb hacks in my time, but this has got to be one of the dumbest.
					                   // There seems to be literally no way to check if the class 'property' represents is null
					                   // property.managedReferenceValue has no get
					                   // property.objectReferenceValue throws an error if you try to get it
					                   // So who needs 'em? We can check the type string presented as such "managedReference<type-name-goes-here>"
					                   // No need to even snip the string out, just check if the following bracket isn't the closing one :)

					                   if (property.type[property.type.IndexOf('<') + 1] != '>')
					                   {
						                   // This next part allows us to enable any class marked with the interface IPortable to
						                   // have a chance at reclaiming some of the values from the instance it's replacing.
						                   // Some bits are easy with Unitys built-in support, but some (like UnityEvents) are garish.
						                   // UnityEvent support can be summoned by calling TypeMenuEditorUtility.TransferPersistentCalls

						                   var portableTarget = instance as IPortable;

						                   portableTarget?.Port(previousInstance: property);
					                   }

					                   p.serializedObject.Update();
					                   p.managedReferenceValue = instance;
					                   p.serializedObject.ApplyModifiedProperties();
				                   },
				             userData: new AssignInstanceArgs(type: t,
				                                              property: property));
			}
		}

		menu.ShowAsContext();

//		var target = property.serializedObject.targetObject as IValidatable;
//
//		target?.Validate();
		
	}


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