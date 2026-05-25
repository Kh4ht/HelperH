using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class HierarchyColorHeader
{
    private const string KEY = "[Header]";

    static HierarchyColorHeader()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
    }

    private static void OnHierarchyGUI(int instanceID, Rect selectionRect)
    {
        GameObject obj = EditorUtility.EntityIdToObject(instanceID) as GameObject;
        if (obj == null) return;

        if (obj.name.StartsWith(KEY))
        {
            // Mark as editor-only
            if (!obj.CompareTag("EditorOnly"))
            {
                obj.tag = "EditorOnly";
                EditorUtility.SetDirty(obj); // Save change in scene
            }
            if (!obj.isStatic)
            {
                obj.isStatic = true;
                EditorUtility.SetDirty(obj);
            }

            // Draw background color
            EditorGUI.DrawRect(selectionRect, new Color(0.2196078f, 0.2196078f, 0.2196078f, 1f));

            // Create GUIStyle for text
            GUIStyle style = new(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.MiddleCenter, // center text
                fontSize = 16, // bigger font
            };

            style.normal.textColor = new Color(0.5019608f, 0.8392157f, 0.9921569f, 1f); // text color

            // Create GUIStyle for text
            GUIStyle style2 = new(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.UpperCenter, // center text
                fontSize = 16, // bigger font
            };

            style2.normal.textColor = new Color(0f, 0f, 0f, 1f); // text color


            // Draw label
            EditorGUI.LabelField(selectionRect, obj.name.Replace(KEY, ""), style2);
            EditorGUI.LabelField(selectionRect, obj.name.Replace(KEY, ""), style);
        }
    }
}
