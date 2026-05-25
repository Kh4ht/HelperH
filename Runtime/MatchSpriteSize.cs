using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(RectTransform))]
[DisallowMultipleComponent]
[AddComponentMenu("HelperKH/UI/" + nameof(MatchSpriteSize))]
public class MatchSpriteSize : MonoBehaviour
{
    #region FIELDS █████████████████████████████████████████████████████████████████████████████████████████████

    [HideInInspector] public Image img;
    [HideInInspector] public RectTransform rt;

    #region SERIALIZABLE FIELDS █ █ █



    #endregion
    #endregion
    #region UNITY EVENT FUNCTIONS ██████████████████████████████████████████████████████████████████████████████

    private void Reset()
    {
        CacheReferences();
    }

    private void OnValidate()
    {
        CacheReferences();
    }

    #endregion
    #region PRIVATE METHODS ████████████████████████████████████████████████████████████████████████████████████



    #endregion
    #region PUBLIC METHODS █████████████████████████████████████████████████████████████████████████████████████

    public void CacheReferences()
    {
        if (!img) img = GetComponent<Image>();
        if (!rt) rt = GetComponent<RectTransform>();
    }

    #endregion
}
