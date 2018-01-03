using UnityEngine;
using UnityEditor;
using VariableObjects;

/*
public class RefEditor<Types, VarType> : PropertyDrawer 
	where VarType: Variable<Types> {
*/
[CustomPropertyDrawer(typeof(LayerMaskConstReference))]
[CustomPropertyDrawer(typeof(LayerMaskReference))]
public class RefEditor : PropertyDrawer {

	override public void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

		string valuePropertyName;

		if(property.FindPropertyRelative("_useInternal").boolValue == true) {
			valuePropertyName = "internalValue";
		}
		else {
			valuePropertyName = "variable";
		}

		EditorGUI.BeginProperty(position, label, property);


		position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);


		Rect checkBoxRect = new Rect(position.x, position.y, 10, position.height);
		Rect fieldRect = new Rect(position.x+15, position.y, position.width - 15, position.height);

		EditorGUI.PropertyField(checkBoxRect, property.FindPropertyRelative("_useInternal"), GUIContent.none);
		EditorGUI.PropertyField(fieldRect, property.FindPropertyRelative(valuePropertyName), GUIContent.none);

		EditorGUI.EndProperty();
	}
}