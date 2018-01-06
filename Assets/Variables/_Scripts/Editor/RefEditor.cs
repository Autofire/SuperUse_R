using UnityEngine;
using UnityEditor;
using VariableObjects;

// DO NOT REMOVE - Automatic insertion point
[CustomPropertyDrawer(typeof(ViveControllerAssistantReference))] [CustomPropertyDrawer(typeof(ViveControllerAssistantConstReference))]
[CustomPropertyDrawer(typeof(TransformReference))] [CustomPropertyDrawer(typeof(TransformConstReference))]
[CustomPropertyDrawer(typeof(GameObjectReference))] [CustomPropertyDrawer(typeof(GameObjectConstReference))]
[CustomPropertyDrawer(typeof(BoolReference))] [CustomPropertyDrawer(typeof(BoolConstReference))]
[CustomPropertyDrawer(typeof(LayerMaskReference))] [CustomPropertyDrawer(typeof(LayerMaskConstReference))]
[CustomPropertyDrawer(typeof(FloatReference))] [CustomPropertyDrawer(typeof(FloatConstReference))]

public class RefEditor : PropertyDrawer {

	override public void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

		const string USE_INTERNAL_TOOLTIP =
			"Checked: Use an 'internal' value; that is, one specified here.\n" +
			"Unchecked: Use a variable asset.";

		string valuePropertyName;

		if(property.FindPropertyRelative("_useInternal").boolValue == true) {
			valuePropertyName = "internalValue";
		}
		else {
			valuePropertyName = "variable";
		}

		label.tooltip = GetTooltip(fieldInfo);



		EditorGUI.BeginProperty(position, label, property);

		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID (FocusType.Passive), label);

		Rect checkBoxRect = new Rect(position.x, position.y, 10, position.height);
		Rect fieldRect = new Rect(position.x+15, position.y, position.width - 15, position.height);

		EditorGUI.PropertyField( checkBoxRect, property.FindPropertyRelative("_useInternal"), GUIContent.none );
		EditorGUI.PropertyField( fieldRect, property.FindPropertyRelative(valuePropertyName), GUIContent.none );

		EditorGUI.LabelField(checkBoxRect, new GUIContent("", USE_INTERNAL_TOOLTIP));

		EditorGUI.EndProperty();
	} // End OnGUI




	string GetTooltip(System.Reflection.FieldInfo info) {
		// Partial credit to https://answers.unity.com/answers/1421384/view.html

		string tooltip = "";

		object[] attributes = info.GetCustomAttributes(typeof(TooltipAttribute), true);

		if(attributes.Length > 0) {
			TooltipAttribute tt = attributes[0] as TooltipAttribute;
			if (tt != null) {
				tooltip = tt.tooltip;
			}
		}

		return tooltip;
	} // End GetTooltip

} // End RefEditor
