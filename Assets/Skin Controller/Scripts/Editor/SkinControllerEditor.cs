using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(SkinController))]
public class SkinControllerEditor : Editor {

	SerializedProperty path;
	SerializedProperty spriteSheet;
	SerializedProperty type;
	SerializedProperty enableWarnings;

	/// <summary>
	/// Sets up all needed SerialiozedProperty for multiple object editing
	/// </summary>
	void OnEnable() {
		path = serializedObject.FindProperty ("path");
		spriteSheet = serializedObject.FindProperty ("spriteSheet");
		type = serializedObject.FindProperty ("type");
		enableWarnings = serializedObject.FindProperty ("enableWarnings");
	}

	/// <summary>
	/// Displays controls and gets inputs on the Inspector window
	/// </summary>
	override public void OnInspectorGUI()
	{
		serializedObject.Update ();

		var script = target as SkinController;

		path.stringValue = EditorGUILayout.TextField (new GUIContent ("Path to Folder", "(Must be in a Resources folder) The path to the folder that contains the sprite sheet."), script.path);
		spriteSheet.stringValue = EditorGUILayout.TextField (new GUIContent ("Sprite Sheet Name", "(Must be in a Resources folder) The name of the sprite sheet."), script.spriteSheet);

		EditorGUILayout.PropertyField (type, new GUIContent ("Swap Type", "'Tag' swaps based on the same sprite name in the sheet [shirt] > [shirt]. 'Number' swaps based on its numeric termination [grass_1] > [snow_1]."));
		enableWarnings.boolValue = EditorGUILayout.Toggle (new GUIContent ("Enable Warnings", "If true warnings will be thrown when a sprite isn't found with the current settings."), script.enableWarnings);

		serializedObject.ApplyModifiedProperties ();
	}
}