using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Helpers 
{
    public static class Utilities 
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int randomIndex = Random.Range(i, list.Count);
                T value = list[i];
                list[i] = list[randomIndex];
                list[randomIndex] = value;
            }
        }

        public static void Swap<T>(this List<T> list, int pos1, int pos2)
        {
            T tmp = list[pos1];
            list[pos1] = list[pos2];
            list[pos2] = tmp;

        }

        public static float Remap(float value, float fromMin, float fromMax, float toMin, float toMax) 
        {
            float progress = Mathf.InverseLerp(fromMin, fromMax, value);
            return Mathf.Lerp(toMin, toMax, progress);
        }

        public static Vector3 WorldToUISpace(Canvas canvas, Vector3 worldPosition)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPos, canvas.worldCamera, out Vector2 localPoint);
            return canvas.transform.TransformPoint(localPoint);
        }
    }
}

