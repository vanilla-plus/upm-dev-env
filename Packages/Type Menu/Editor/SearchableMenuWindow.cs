using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using UnityEditor;

public class SearchableMenuWindow : EditorWindow
{

	private static GameObject         selectedGameObject;
	private        string             searchQuery = string.Empty;
	private        Vector2            scrollPosition;
	private        SerializedProperty property;
	private        IEnumerable<Type>         types;
	private        List<Type>         filteredTypes    = new List<Type>();
	private        int                focusedItemIndex = 0; // Index of the currently focused item
	private        GUIStyle           selectedStyle;
	private        GUIStyle           unselectedStyle;
	
	// Call this method to open the window, passing the necessary parameters
	public static void ShowWindow(string title,
	                              SerializedProperty property,
	                              IEnumerable<Type> types)
	{
		var window = GetWindow<SearchableMenuWindow>(utility: true,
		                                             title: title);

		window.property = property;
		window.types    = types;

//		window.unselectedStyle = new GUIStyle
//		                         {
//			                         border = new RectOffset(2,
//			                                                 0,
//			                                                 0,
//			                                                 0),
//			                         fontStyle = FontStyle.Normal,
//			                         alignment = TextAnchor.MiddleLeft
//		                         };
//
//
//		window.selectedStyle = new GUIStyle
//		                       {
//			                       border = new RectOffset(8,
//			                                               0,
//			                                               0,
//			                                               0),
//			                       fontStyle = FontStyle.Bold,
//			                       alignment = TextAnchor.MiddleLeft,
//		                       };

		window.unselectedStyle           = EditorStyles.toolbarButton;
		window.unselectedStyle.alignment = TextAnchor.MiddleLeft;
		window.selectedStyle             = EditorStyles.miniButton;
		window.selectedStyle.alignment   = TextAnchor.MiddleLeft;
		
		selectedGameObject = Selection.activeGameObject;
	}



void OnGUI()
{
    GUI.FocusControl("SearchField");
    EditorGUI.FocusTextInControl("SearchField");
    GUI.SetNextControlName("SearchField");
    
    searchQuery                    = EditorGUILayout.TextField(searchQuery);
    
//    var unselectedStyle         = GUI.skin.button;
    
//    var selectedStyle = EditorStyles.miniButton;
//	var selectedStyle = EditorStyles.toolbarButton;
	
	
//    
//    selectedStyle.fontStyle = FontStyle.Bold;
    
	PopulateFilteredTypes();

    HandleKeyboardNavigation();

    // Filter and display results
    scrollPosition = GUILayout.BeginScrollView(scrollPosition);
    
    for (var i = 0; i < filteredTypes.Count; i++)
    {
        var type  = filteredTypes[i];
        var style = i == focusedItemIndex ? selectedStyle : unselectedStyle;

        if (!GUILayout.Button(text: type == null ?
	                                    "-" :
	                                    type.Name.Replace("_", " "),
                              style: style)) continue;

        HandleTypeSelection(type);
        
        Close(); // Close the window after a selection is made
    }
    
    GUILayout.EndScrollView();
}


private void HandleKeyboardNavigation()
{
	if (Event.current.type is not (EventType.KeyDown or EventType.KeyUp)) return;

	switch (Event.current.keyCode)
	{
		case KeyCode.DownArrow:
			focusedItemIndex = Mathf.Min(a: focusedItemIndex    + 1,
			                             b: filteredTypes.Count - 1);

			Event.current.Use(); // Mark the event as used

			break;

		case KeyCode.UpArrow:
			focusedItemIndex = Mathf.Max(a: focusedItemIndex - 1,
			                             b: 0);

			Event.current.Use();

			break;

		case KeyCode.Return when focusedItemIndex >= 0:
		{
			var selectedType = filteredTypes[focusedItemIndex];
			
			HandleTypeSelection(selectedType);
			
			Close();
			
			GUIUtility.ExitGUI();

			break;
		}
	}
}


// Filter types based on search query
	private void PopulateFilteredTypes()
	{
		filteredTypes.Clear();

		filteredTypes.AddRange(types.Where(type => type.Name.Contains(value: searchQuery.Replace(oldValue: " ",
		                                                                                         newValue: "_"),
		                                                              comparisonType: StringComparison.OrdinalIgnoreCase)));
		
		filteredTypes.Add(null);
	}


	void Update()
	{
		// Close the Window automatically if the selectedGameObject has changed (cached from when the Window was first opened)
		if (Selection.activeGameObject != selectedGameObject) Close();
	}


	private void HandleTypeSelection(Type selectedType)
	{
		// Implementation for type selection
		// Similar to what you have in your menu item selection callback
		if (selectedType != null)
		{
			property.serializedObject.Update();
			var instance = Activator.CreateInstance(selectedType);
			property.managedReferenceValue = instance;
			property.serializedObject.ApplyModifiedProperties();
		}
		else
		{
			property.serializedObject.Update();
			property.managedReferenceValue = null;
			property.serializedObject.ApplyModifiedProperties();
		}
	}


}