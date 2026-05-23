using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KH
{
    public static class Kh
    {
        // █████████████████████████████████████████████████████████████████████████████████████████████████
        #region  FIELDS
        // █████████████████████████████████████████████████████████████████████████████████████████████████

        private const string _STRIPE = "— — — — — — — — — — — — — — — — — — — — — — — — — — — — — — — — — — ";
        private const string _STRIPE_2 = "—— —— —— —— —— —— —— —— —— —— —— —— —— —— —— —— —— —— —— —— —— —— —— —— ";
        private const string _DOTTED = "•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••";
        private const string _LINE = "———————————————————————————————————————————————————————————————————————————————";
        public const string LINE = _LINE + _LINE + _LINE;
        public const string DOTTED_LINE = _DOTTED + _DOTTED + _DOTTED;
        public const string STRIPE_LINE_1 = _STRIPE + _STRIPE + _STRIPE;
        public const string STRIPE_LINE_2 = _STRIPE_2 + _STRIPE_2 + _STRIPE_2;


        #endregion
        // █████████████████████████████████████████████████████████████████████████████████████████████████
        #region  GetMousePos
        // █████████████████████████████████████████████████████████████████████████████████████████████████

        /// <summary>Changes the <see cref="Cursor"/> texture to a 
        /// custom <paramref name="texture2D"/> with a specified hotspot alignment.</summary>
        /// <param name="hotspotPosition">The position on the texture to use as the cursor's hotspot.</param>
        public static void ChangeCursor(Texture2D texture2D, CursorHotspot hotspotPosition = CursorHotspot.TopLeft)
        {
            Vector2 hotSpot = KHMethods.GetHotspotPosition(texture2D, hotspotPosition);
            Cursor.SetCursor(texture2D, hotSpot, CursorMode.Auto);
        }

        #endregion
        // █████████████████████████████████████████████████████████████████████████████████████████████████
        #region  GetMousePos
        // █████████████████████████████████████████████████████████████████████████████████████████████████

        /// <returns>The current mouse position.</returns>
        public static Vector2 GetMousePos()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        #endregion
        // █████████████████████████████████████████████████████████████████████████████████████████████████
        #region  IsMouseOverUI
        // █████████████████████████████████████████████████████████████████████████████████████████████████

        /// <returns>True if the mouse pointer is currently over any UI element.</returns>
        public static bool IsMouseOverUI()
        {
            return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        }

        #endregion
        // █████████████████████████████████████████████████████████████████████████████████████████████████
        #region  KHIsNullOrEmpty
        // █████████████████████████████████████████████████████████████████████████████████████████████████

        /// <returns>True if the <paramref name="list"/> is null or empty.</returns>
        public static bool KHIsNullOrEmpty<T>(this IList<T> list)
        {
            return list == null || list.Count == 0;
        }

        #endregion
        // █████████████████████████████████████████████████████████████████████████████████████████████████
        #region  KHPickRandom
        // █████████████████████████████████████████████████████████████████████████████████████████████████

        /// <returns>A random item from a <paramref name="list"/>.</returns>
        public static T KHPickRandom<T>(this IList<T> list)
        {
            return list.KHIsNullOrEmpty() ? default : list[Random.Range(0, list.Count)];
        }

        #endregion
        // █████████████████████████████████████████████████████████████████████████████████████████████████
        #region  KHHasType
        // █████████████████████████████████████████████████████████████████████████████████████████████████

        /// <summary>
        /// Checks if the enumerable contains an element of type T.
        /// </summary>
        public static bool KHHasType<TType>(this System.Collections.IEnumerable list)
        {
            foreach (object item in list)
                if (item is TType)
                    return true;

            return false;
        }

        #endregion
        // █████████████████████████████████████████████████████████████████████████████████████████████████
        #region  KHDestroyAllChildrenImmediate
        // █████████████████████████████████████████████████████████████████████████████████████████████████

        public static void KHDestroyAllChildrenImmediate(this Transform parent)
        {
            if (parent == null)
                return;

            for (int i = parent.childCount - 1; i >= 0; i--)
            {
                Object.DestroyImmediate(parent.GetChild(i).gameObject);
            }
        }

        #endregion
        // █████████████████████████████████████████████████████████████████████████████████████████████████
        #region  SqrDistance
        // █████████████████████████████████████████████████████████████████████████████████████████████████

        ///<summary>for performance optimization.</summary>
        /// <returns>The Squared distance between <paramref name="a"/> 
        /// and <paramref name="b"/></returns>
        public static float SqrDistance(Vector3 a, Vector3 b)
        {
            // Check for special invalid values (not null since Vector3 is a struct)
            if (a == Vector3.negativeInfinity || b == Vector3.negativeInfinity)
            {
                KHDebug.LogWarning($"[HDistance] Invalid world position (Vector3.negativeInfinity)." +
                                  $"a: {a}, b: {b}");
                return 0f;
            }

            return (a - b).sqrMagnitude;
        }

        ///<summary>for performance optimization.</summary>
        /// <returns>The Squared distance between <paramref name="a"/> 
        /// and <paramref name="b"/></returns>
        public static float SqrDistance(Transform a, Transform b)
        {
            if (a == null || b == null)
            {
                KHDebug.LogWarning($"[HDistance] Transform is null. a: {a}, b: {b}");
                return 0f;
            }
            // Check for special invalid values (not null since Vector3 is a struct)
            if (a.position == Vector3.negativeInfinity || b.position == Vector3.negativeInfinity)
            {
                KHDebug.LogWarning($"[HDistance] Invalid world position (Vector3.negativeInfinity)." +
                                  $"a: {a}, b: {b}");
                return 0f;
            }

            return (a.position - b.position).sqrMagnitude;
        }

        ///<summary>for performance optimization.</summary>
        /// <returns>The Squared distance between <paramref name="a"/> 
        /// and <paramref name="b"/></returns>
        public static float SqrDistance(MonoBehaviour a, MonoBehaviour b)
        {
            if (a == null || b == null)
            {
                KHDebug.LogWarning($"[HDistance] object is null. a: {a}, b: {b}");
                return 0f;
            }

            // Check for special invalid values (not null since Vector3 is a struct)
            if (a.transform.position == Vector3.negativeInfinity || b.transform.position == Vector3.negativeInfinity)
            {
                KHDebug.LogWarning($"[HDistance] Invalid world position (Vector3.negativeInfinity)." +
                                $"a: {a}, b: {b}");
                return 0f;
            }

            return (a.transform.position - b.transform.position).sqrMagnitude;
        }

        #endregion
        // █████████████████████████████████████████████████████████████████████████████████████████████████
        #region  SqrDistanceIsLessThan
        // █████████████████████████████████████████████████████████████████████████████████████████████████

        ///<summary>for performance optimization.</summary>
        /// <returns>True if the distance between <paramref name="a"/> and <paramref name="b"/>
        /// is less than the <paramref name="threshold"/>.</returns>
        public static bool SqrDistanceIsLessThan(Vector3 a, Vector3 b, float threshold)
        {
            return SqrDistance(a, b) < threshold * threshold;
        }

        ///<summary>for performance optimization.</summary>
        /// <returns>True if the distance between <paramref name="a"/> and <paramref name="b"/>
        /// is less than the <paramref name="threshold"/>.</returns>
        public static bool SqrDistanceIsLessThan(Transform a, Transform b, float threshold)
        {
            return SqrDistance(a, b) <= threshold * threshold;
        }

        ///<summary>for performance optimization.</summary>
        /// <returns>True if the distance between <paramref name="a"/> and <paramref name="b"/>
        /// is less than the <paramref name="threshold"/>.</returns>
        public static bool SqrDistanceIsLessThan(MonoBehaviour a, MonoBehaviour b, float threshold)
        {
            return SqrDistance(a, b) <= threshold * threshold;
        }

        #endregion
        // █████████████████████████████████████████████████████████████████████████████████████████████████
        #region KHMoveTowards
        // █████████████████████████████████████████████████████████████████████████████████████████████████

        /// <summary>
        /// Moves the <see cref="Transform"/> towards the <paramref name="targetPos"/> at a constant <paramref name="moveSpeed"/>.
        /// </summary>
        public static void KHMoveTowards(this Transform transform, Vector3 targetPos, float moveSpeed)
        {
            transform.position += (Vector3)transform.position.KHDirTo(targetPos) * moveSpeed;
        }

        #endregion
        // █████████████████████████████████████████████████████████████████████████████████████████████████
        #region KHDirTo
        // █████████████████████████████████████████████████████████████████████████████████████████████████

        /// <returns>
        /// A <see cref="Vector2"/> representing the direction from <paramref name="fromPos"/> to <paramref name="target"/>.
        /// </returns>
        public static Vector2 KHDirTo(this Vector3 fromPos, Vector3 target, bool normalizeDir = true)
        {
            return (Vector2)(target - fromPos) == Vector2.zero ? Vector2.zero : normalizeDir ? (target - fromPos).normalized : (target - fromPos);
        }

        #endregion
        // █████████████████████████████████████████████████████████████████████████████████████████████████
        #region KHGetAngle
        // █████████████████████████████████████████████████████████████████████████████████████████████████

        public static float KHGetAngle(this Vector2 dir)
        {
            return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        }

        #endregion
    }
}





