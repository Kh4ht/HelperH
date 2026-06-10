using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class EditorUtilityTools
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PreventListDuplications
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public static void PreventListDuplications<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            for (int j = i + 1; j < list.Count; j++)
            {
                if (EqualityComparer<T>.Default.Equals(list[i], list[j]))
                    list[j] = default;
            }
        }
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region MatchCount
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public static void MatchCount<T>(this List<T> list, int count)
    {
        if (list == null) return;

        while (list.Count < count)
            list.Add(default);

        while (list.Count > count)
            list.RemoveAt(list.Count - 1);
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region AutoFillDataBase
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public static void AutoFillDataBase<T>(this List<T> list) where T : ScriptableObject
    {
        if (list == null)
            return;

        list.Clear();

        string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            T asset = AssetDatabase.LoadAssetAtPath<T>(path);
            if (asset != null)
                list.Add(asset);
        }
    }

    #endregion
}
