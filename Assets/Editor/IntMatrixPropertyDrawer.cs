using UnityEditor;
using UnityEngine;

namespace OpenCVMarkerBasedAR
{
    [CustomPropertyDrawer (typeof(IntMatrix))]
    public class IntMatrixPropertyDrawer : PropertyDrawer
    {
        public bool showPosition = true;

        public override void OnGUI (UnityEngine.Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty (position, label, property);
        
            showPosition = EditorGUI.Foldout (new UnityEngine.Rect (position.x, position.y, position.width - 6, 18), showPosition, label);
        
            if (showPosition) {
                int oldIndentLevel = EditorGUI.indentLevel;
                EditorGUI.indentLevel = EditorGUI.indentLevel + 1;
            
                position = EditorGUI.IndentedRect (position);
                EditorGUI.indentLevel = 0;

                UnityEngine.Rect newposition = position;
                newposition.y += 18f;
                
                SerializedProperty element = property.FindPropertyRelative ("elementType");
                EditorGUI.PropertyField (new UnityEngine.Rect (position.x, position.y + 18, position.width, 18), element);

                if (element.enumValueIndex == 2)
                {
                    newposition.y += 20f;

                    SerializedProperty data = property.FindPropertyRelative("vectorData");
                    data.arraySize = 3;
                    
                    newposition.width = 18f;
                    newposition.height = 18f;

                    for (int j = 0; j < 3; j++)
                    {

                        newposition.x = position.x + (position.width - (newposition.width * 3)) / 2;
                        newposition.y += 18f;
                        
                        EditorGUI.PropertyField(newposition, data.GetArrayElementAtIndex(j), GUIContent.none);
                        
                        newposition.x += newposition.width;
                    }
                }
                else if (element.enumValueIndex == 3)
                {
                    SerializedProperty matrixSize = property.FindPropertyRelative("matrixSize");
                    EditorGUI.PropertyField(new UnityEngine.Rect(position.x, position.y + 35, position.width, 18),
                        matrixSize);

                    if (matrixSize.intValue <= 0)
                        matrixSize.intValue = 1;

                    newposition.y += 20f;

                    SerializedProperty data = property.FindPropertyRelative("matrixData");
                    data.arraySize = matrixSize.intValue * matrixSize.intValue;

                    newposition.width = 18f;
                    newposition.height = 18f;

                    for (int j = 0; j < matrixSize.intValue; j++)
                    {

                        newposition.x = position.x + (position.width - (newposition.width * matrixSize.intValue)) / 2;
                        newposition.y += 18f;

                        for (int i = 0; i < matrixSize.intValue; i++)
                        {
                            EditorGUI.PropertyField(newposition,
                                data.GetArrayElementAtIndex(j * matrixSize.intValue + i), GUIContent.none);
                            newposition.x += newposition.width;
                        }
                    }
                }
                else if (element.enumValueIndex == 4)
                {
                    SerializedProperty operation = property.FindPropertyRelative("operation");

                    EditorGUI.PropertyField(new UnityEngine.Rect(position.x, position.y + 35, position.width, 18), operation);
                }

                EditorGUI.indentLevel = oldIndentLevel;
            }
            EditorGUI.EndProperty ();
        }

        public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
        {        
            if (showPosition) {
            
                SerializedProperty matrixSize = property.FindPropertyRelative ("matrixSize");
            
                return 18f * (matrixSize.intValue + 3);
            } else {
                return 18f;
            }
        }
    }
}