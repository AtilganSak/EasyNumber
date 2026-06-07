using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EasyNumber))]
public class EasyNumberDrawer : PropertyDrawer
{
    const float LINE = 18f;
    const float PAD = 2f;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (!property.isExpanded)
            return LINE;

        SerializedProperty steps = property.FindPropertyRelative("steps");
        int count = steps != null ? steps.arraySize : 0;

        // foldout + size field + decimals field + elements + separator + preview
        return (4 + count) * (LINE + PAD) + 1f;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        Rect row = new Rect(position.x, position.y, position.width, LINE);
        property.isExpanded = EditorGUI.Foldout(row, property.isExpanded, label, true);

        if (property.isExpanded)
        {
            EditorGUI.indentLevel++;
            SerializedProperty steps = property.FindPropertyRelative("steps");
            SerializedProperty valueProperty = property.FindPropertyRelative("_value");
            SerializedProperty decimalsProperty = property.FindPropertyRelative("_decimals");

            row.y += LINE + PAD;

            // Size field
            int newSize = Mathf.Max(0, EditorGUI.IntField(row, "Size", steps.arraySize));
            if (newSize != steps.arraySize)
                steps.arraySize = newSize;

            // Decimals field
            row.y += LINE + PAD;
            decimalsProperty.intValue = Mathf.Clamp(EditorGUI.IntField(row, "Decimals", decimalsProperty.intValue), 0, 5);

            // Step fields with named labels
            for (int i = 0; i < steps.arraySize; i++)
            {
                row.y += LINE + PAD;
                string stepLabel = i < Necessary.ScoreNames.Length ? Necessary.ScoreNames[i] : i.ToString();
                if (string.IsNullOrEmpty(stepLabel)) stepLabel = "x1";
                EditorGUI.PropertyField(row, steps.GetArrayElementAtIndex(i), new GUIContent(stepLabel));
            }

            // Separator
            row.y += LINE + PAD;
            Rect separatorRect = new Rect(row.x + EditorGUI.indentLevel * 15f, row.y, row.width - EditorGUI.indentLevel * 15f, 1f);
            EditorGUI.DrawRect(separatorRect, new Color(0.5f, 0.5f, 0.5f, 0.5f));
            row.y += 1f + PAD;

            // Preview
            double combined = 0;
            if (steps != null && steps.arraySize > 0)
            {
                for (int i = 0; i < steps.arraySize; i++)
                    combined += steps.GetArrayElementAtIndex(i).doubleValue * Math.Pow(1000, i);
            }
            else if (valueProperty != null)
            {
                combined = valueProperty.doubleValue;
            }

            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.TextField(row, "Value", Necessary.Convert(combined, decimalsProperty.intValue));
            EditorGUI.EndDisabledGroup();

            EditorGUI.indentLevel--;
        }

        EditorGUI.EndProperty();
    }
}