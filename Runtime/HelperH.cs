using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HelperH
{
    public enum CursorHotspot
    {
        TopLeft,
        TopCenter,
        TopRight,
        CenterLeft,
        Center,
        CenterRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    public enum InfMoveSpeed
    {
        NORMAL,
        SLOWEST,
        SLOW,
        FAST,
        FASTEST,
    }

    public enum BulletMoveSpeed
    {
        SLOWEST,
        SLOW,
        NORMAL,
        FAST,
        FASTEST,
    }

    public enum EnemyName
    {
        Slime,
        Skeleton,
    }

    public enum TowerName
    {
        Barbarian,
        Bow,
        Bomb,
        Tesla,
    }

    // XML Colors for HDebug Console Masseges
    public enum XMLC
    {
        White,
        Green,
        Red,
        Yellow,
    }

    // █████████████████████████████████████████████████████████████████████████ //

    public static class UIH
    {
        private const string __ = "––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––";
        public const string Line = __ + __ + __ + __;
    }

    // █████████████████████████████████████████████████████████████████████████ //

    public static class H
    {
        public const float SLOWEST = 0.15f;
        public const float SLOW = 0.3f;
        public const float NORMAL = 0.45f;
        public const float FAST = 0.6f;
        public const float FASTEST = 0.75f;


        public static readonly int INFANTRY_COLOR = Shader.PropertyToID("_color");

        /// <returns>The world position if successful, otherwise <see cref="Vector3.negativeInfinity"/>.</returns>

        static Vector2 GetHotspotPosition(Texture2D texture, CursorHotspot position)
        {
            float width = texture.width;
            float height = texture.height;

            return position switch
            {
                CursorHotspot.TopLeft => new Vector2(0, 0),
                CursorHotspot.TopCenter => new Vector2(width / 2f, 0),
                CursorHotspot.TopRight => new Vector2(width, 0),

                CursorHotspot.CenterLeft => new Vector2(0, height / 2f),
                CursorHotspot.Center => new Vector2(width / 2f, height / 2f),
                CursorHotspot.CenterRight => new Vector2(width, height / 2f),

                CursorHotspot.BottomLeft => new Vector2(0, height),
                CursorHotspot.BottomCenter => new Vector2(width / 2f, height),
                CursorHotspot.BottomRight => new Vector2(width, height),

                _ => new Vector2(0, height), // default fallback
            };
        }

        // █████████████████████████████████████████████████████████████████████████ //

        #region ————————————« Universal Functions »—————————————————————————————

        /// <summary>Changes the <see cref="Cursor"/> texture to a custom <paramref name="texture2D"/> with a specified hotspot alignment.</summary>
        /// <param name="hotspotPosition">The position on the texture to use as the cursor's hotspot.</param>
        public static void ChangeCursor(Texture2D texture2D, CursorHotspot hotspotPosition = CursorHotspot.TopLeft)
        {
            Vector2 hotSpot = GetHotspotPosition(texture2D, hotspotPosition);
            Cursor.SetCursor(texture2D, hotSpot, CursorMode.Auto);
        }

        /// <returns>The current mouse position.</returns>
        public static Vector2 GetMousePos()
            => Camera.main.ScreenToWorldPoint(Input.mousePosition);

        /// <returns>True if the mouse pointer is currently over any UI element.</returns>
        public static bool IsMouseOverUI()
            => EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();

        /// <returns>The angle (in degrees) from the given source to the <paramref name="target"/>'s position.</returns>
        public static float GetAngle(Vector2 from, Vector2 target) =>
            Mathf.Atan2(target.y - from.y, target.x - from.x) * Mathf.Rad2Deg - 90f;

        /// <summary>Plays an <paramref name="audioClip"/> if the <paramref name="audioSource"/> is not already playing.</summary>
        public static void HPlaySFX(this AudioSource audioSource, AudioClip audioClip,
            [CallerMemberName] string c = "", [CallerFilePath] string f = "", [CallerLineNumber] int l = 0)
        {
            if (audioSource == null || audioClip == null)
            {
                HDebug.Log($"[PlaySFX] Missing audioSource or clip.", XMLC.White, c, f, l);
                return;
            }

            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(audioClip);
        }
        #endregion —————————————————————————————————————————————————————————————

        // █████████████████████████████████████████████████████████████████████████ //

        #region ————————————————————« Generic »—————————————————————————————————

        /// <returns>True if the <paramref name="list"/> is null or empty.</returns>
        public static bool HIsNullOrEmpty<T>(this IList<T> list)
            => list == null || list.Count == 0;


        /// <returns>True if the <paramref name="array"/> is null or empty.</returns>
        public static bool HIsNullOrEmpty<T>(this T[] array)
            => array == null || array.Length == 0;


        /// <returns>A random item from a <paramref name="list"/>.</returns>
        public static T HPickRandom<T>(this IList<T> list)
            => list.HIsNullOrEmpty() ? default : list[Random.Range(0, list.Count)];


        /// <returns>A random element from an <paramref name="array"/>.</returns>
        public static T HPickRandom<T>(this T[] array)
            => array.HIsNullOrEmpty() ? default : array[Random.Range(0, array.Length)];

#if UNITY_EDITOR
        public static void AutoFillList<T>(List<T> list) where T : ScriptableObject
        {
            if (list == null) return;
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
#endif

        public static void MatchListCount<T>(List<T> list, int targetCount)
        {
            if (list == null) return;

            while (list.Count < targetCount)
                list.Add(default);

            while (list.Count > targetCount)
                list.RemoveAt(list.Count - 1);
        }

        #endregion —————————————————————————————————————————————————————————————

        // █████████████████████████████████████████████████████████████████████████ //

        #region ————————————————————« Vector3 »—————————————————————————————————

        /// <returns>The SQUARED distance between <paramref name="a"/> and <paramref name="b"/> for performance optimization.</returns>
        public static float HDistance(Vector3 a, Vector3 b)
        {
            // Check for special invalid values (not null since Vector3 is a struct)
            if (a == Vector3.negativeInfinity || b == Vector3.negativeInfinity)
            {
                HDebug.LogWarning($"[HDistance] Invalid world position (Vector3.negativeInfinity)." +
                                  $"a: {a}, b: {b}");
                return 0f;
            }

            return (a - b).sqrMagnitude;
        }

        /// <returns>True if the squared distance between <paramref name="a"/> and <paramref name="b"/>
        /// is less than the <paramref name="threshold"/>.</returns>
        public static bool HIsPosReached(this Vector3 a, Vector3 b, float threshold)
            => (a - b).sqrMagnitude < threshold * threshold;

        /// <returns>
        /// A <see cref="Vector2"/> representing the direction from <paramref name="fromPos"/> to <paramref name="target"/>.
        /// </returns>
        public static Vector2 HGetDirTo(this Vector3 fromPos, Vector3 target, bool normalizeDir = true)
            => (Vector2)(target - fromPos) == Vector2.zero ? Vector2.zero : normalizeDir ? (target - fromPos).normalized : (target - fromPos);

        public static float HGetAngle(this Vector2 dir)
            => Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        public static float HGetAngle(this Vector3 dir)
            => Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        #endregion —————————————————————————————————————————————————————————————

        // █████████████████████████████████████████████████████████████████████████ //

        #region ————————————————————« Collider2D »——————————————————————————————

        /// <returns>
        /// "True" if the <paramref name="point"/> is within the collider bounds of the given <paramref name="coll"/>; otherwise, "False".
        /// Logs a warning and returns "False" if the object is null, unsupported, or lacks a <see cref="Collider2D"/>.
        /// </returns>
        public static bool HContainsPoint2D(this Collider2D coll, Vector3 point)
        {
            if (coll == null)
            {
                HDebug.LogWarning($"[HIsInColliderBounds] Null object reference.");
                return false;
            }

            return coll.OverlapPoint(point);
        }

        #endregion —————————————————————————————————————————————————————————————

        // █████████████████████████████████████████████████████████████████████████ //

        #region ————————————————————« RigidBody2D »—————————————————————————————

        /// <returns>
        /// 'True' if the velocity magnitude is less than or equal to <paramref name="threshold"/> ; otherwise, 'False'.
        /// </returns>
        public static bool HIsVelocityBelow(this Rigidbody2D rb, float threshold)
        {
            if (rb == null)
            {
                HDebug.LogWarning("Rigidbody2D is Null");
                return false;
            }

            return rb.linearVelocity.sqrMagnitude <= threshold * threshold;
        }

        /// <returns>The angle (in degrees) of the <see cref="Rigidbody2D"/>'s velocity direction.</returns>
        public static float HGetVelocityAngle(this Rigidbody2D rb)
        {
            if (rb == null)
            {
                HDebug.LogWarning($"rb is null");
                return 0f;
            }

            return Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
        }

        /// <returns>Direction of the object based on velocity.</returns>
        public static Vector2 HGetVelocityDir(this Rigidbody2D rb, bool isNormalized = true)
        {
            if (rb == null)
            {
                HDebug.LogWarning($"rb is null");
                return Vector2.zero;
            }

            return isNormalized ? rb.linearVelocity.normalized : rb.linearVelocity;
        }

        #endregion —————————————————————————————————————————————————————————————

        // █████████████████████████████████████████████████████████████████████████ //

        #region ————————————————————« SpriteRenderer »——————————————————————————

        /// <summary>
        /// Fades a material property on the <see cref="SpriteRenderer"/> to the specified <paramref name="value"/> over the given <paramref name="duration"/>.
        /// <para>Supports float, int, and Color types.</para>
        /// </summary>
        public static void HFadeMaterialProperty<T>(this SpriteRenderer sr, int propertyID, T value, float duration = 0)
            => sr.HFadeMaterialProperty(propertyID, value, duration, value, 0);


        /// <summary>
        /// Fades a material property on the <see cref="SpriteRenderer"/> by tweening it from a <paramref name="startValue"/> over <paramref name="startDuration"/>,
        /// to an <paramref name="endValue"/> over <paramref name="endDuration"/>.
        /// <para>Supports float, int, and Color types.</para>
        /// </summary>
        public static void HFadeMaterialProperty<T>(this SpriteRenderer sr, int propertyID, T startValue, float startDuration, T endValue, float endDuration)
        {
            Material mat = sr.material;

            if (sr == null || mat == null || !mat.HasProperty(propertyID))
            {
                HDebug.LogWarning($"[FadeMaterialProperty] Invalid SpriteRenderer or material or property ID.");
                return;
            }

            if (startValue?.GetType() != endValue?.GetType())
            {
                HDebug.LogWarning($"endValue type must be the same as startValue of type {typeof(T)}.");
                return;
            }

            // 🟩 EARLY EXIT IF start == end == current
            if (startValue is float sF && endValue is float eF)
            {
                float current = mat.GetFloat(propertyID);
                if (Mathf.Approximately(current, sF) && Mathf.Approximately(sF, eF))
                    return;
            }
            else if (startValue is int sI && endValue is int eI)
            {
                int current = mat.GetInteger(propertyID);
                if (current == sI && sI == eI)
                    return;
            }
            else if (startValue is Color sC && endValue is Color eC)
            {
                UnityEngine.Color current = mat.GetColor(propertyID);
                if (current == sC && sC == eC)
                    return;
            }

            mat.DOKill();

            if (startValue is float startF)
            {
                mat.DOFloat(startF, propertyID, startDuration)
                    .OnComplete(() =>
                    {
                        if (endValue is float endF && !Mathf.Approximately(mat.GetFloat(propertyID), endF))
                            mat.DOFloat(endF, propertyID, endDuration);
                    });
            }

            else if (startValue is int startI)
            {
                mat.DOFloat(startI, propertyID, startDuration)
                    .OnComplete(() =>
                    {
                        if (endValue is int endI && mat.GetInteger(propertyID) != endI)
                            mat.DOFloat(endI, propertyID, endDuration);
                    });
            }

            else if (startValue is Color startC)
            {
                mat.DOColor(startC, propertyID, startDuration)
                    .OnComplete(() =>
                    {
                        if (endValue is Color endC && mat.GetColor(propertyID) != endC)
                            mat.DOColor(endC, propertyID, endDuration);
                    });
            }

            else HDebug.LogWarning($"[FadeMaterialProperty] Unsupported type {typeof(T)}.");
        }

        #endregion —————————————————————————————————————————————————————————————

        // █████████████████████████████████████████████████████████████████████████ //

        #region ————————————————————« Custom Class »————————————————————————————


        #endregion —————————————————————————————————————————————————————————————
    }
}