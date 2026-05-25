#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MatchSpriteSize))]
public class MatchSpriteSizeEditor : Editor
{
    private MatchSpriteSize ms;

    private void OnEnable()
    {
        ms = (MatchSpriteSize)target;
        TryApplySize();
    }

    private void TryApplySize()
    {
        if (!ms || !ms.img || !ms.rt || !ms.img.sprite)
            return;

        Vector2 spriteSize = ms.img.sprite.rect.size;

        // Auto-set preserveAspect = true
        if (!ms.img.preserveAspect)
        {
            ms.img.preserveAspect = true;
            EditorUtility.SetDirty(ms.img);
        }

        // Apply rect transform size if needed
        if (ms.rt.sizeDelta != spriteSize)
        {
            ms.rt.sizeDelta = spriteSize;
            EditorUtility.SetDirty(ms.rt);
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (ms.img && ms.img.sprite)
            EditorGUILayout.LabelField("Auto-resizing & preserveAspect enforced.");
        else
            EditorGUILayout.HelpBox("Assign a sprite to auto-resize.", MessageType.Info);

        if (GUILayout.Button("Apply Sprite Size Manually"))
        {
            TryApplySize();
        }
    }

    private void OnSceneGUI()
    {
        TryApplySize();
    }
}
#endif
