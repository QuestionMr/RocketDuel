using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// IngredientDrawerUIE
// [CustomPropertyDrawer(typeof(IItemSelect))]
// // public class ItemSelectPD : PropertyDrawer
// // {
//     // public override VisualElement CreatePropertyGUI(SerializedProperty property)
//     // {
//     //     // Create property container element.
//     //     var container = new VisualElement();
//     //     Debug.Log("YES");
//     //     return container;
//     // }
    
//     // // Draw the property inside the given rect
//     // public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//     // {
//     //     EditorGUI.BeginProperty(position, label, property);

//     //     position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

//     //     var indent = EditorGUI.indentLevel;
//     //     EditorGUI.indentLevel = 0;

//     //     var amountRect = new Rect(position.x, position.y, 30, position.height);

//     //     // Draw fields - pass GUIContent.none to each so they are drawn without labels
//     //     //EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);

//     //     // Set indent back to what it was
//     //     EditorGUI.indentLevel = indent;

//     //     EditorGUI.EndProperty();
//     // }
//      public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//     {
//         Debug.Log("AGHHH");
//         EditorGUI.BeginProperty(position, label, property);
//         if (IsValid(property)){
//             label.tooltip = "Serialize + interface";
//             CheckProperty(property);

//             // if (position.Contains(Event.current.mousePosition) == true)
//             // {
//             //     if (DragAndDrop.objectReferences.Length > 0)
//             //     {
//             //         if (TryGetInterfaceFromObject(DragAndDrop.objectReferences[0]) == null)
//             //             DragAndDrop.visualMode = DragAndDropVisualMode.Rejected;
//             //     }
//             // }
//         }
//         EditorGUI.PropertyField(position, property, label);
        
//     }

//     private Object TryGetInterfaceFromObject(Object targetObject)
//     {
//         if (targetObject is IItemSelect) return targetObject;
//         return null;
//     }

//     private bool IsValid(SerializedProperty property)
//     {
//         return true;
//         //return property.type == "IItemSelect";
//     }

//     private void CheckProperty(SerializedProperty property)
//     {
//         Debug.Log(property.name);
//         if (property.objectReferenceValue == null) return;

//         property.objectReferenceValue = TryGetInterfaceFromObject(property.objectReferenceValue);
//     }
// }