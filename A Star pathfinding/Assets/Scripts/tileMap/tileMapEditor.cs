/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(tileData))]
public class tileMapEditor : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PrefixLabel(position, label);

        Rect newPos = position;

        newPos.y += 18f;

        SerializedProperty rows = property.FindPropertyRelative("myRows");

        if (rows.arraySize != 10)
        {
            rows.arraySize = 10;
        }

        for (int i = 0; i < 10; i++)
        {

            SerializedProperty row = rows.GetArrayElementAtIndex(i).FindPropertyRelative("row");

            newPos.height = 20;

            if (row.arraySize != 10)
            {
                row.arraySize = 10;
            }

            newPos.width = 70;

            for (int j = 0; j < 10; j++)
            {
                EditorGUI.PropertyField(newPos, row.GetArrayElementAtIndex(j), GUIContent.none);
                newPos.x += newPos.width;
            }

            newPos.x = position.x;

            newPos.y += 20;
        }

    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 20 * 12;
    }

}
*/