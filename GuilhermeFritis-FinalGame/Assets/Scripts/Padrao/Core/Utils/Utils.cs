using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Padrao.Core.Utils
{  
    public static class Utils
    {
        public static void Scale(this Transform t, float size=1.2f){
            t.localScale = Vector3.one * size;
        }

        public static T GetRandom<T>(this List<T> list){
            return list[Random.Range(0, list.Count)];
        }

        public static T GetRandom<T>(this List<T> list, T key){
            if(list.Count == 1){
                return key;
            }
            var value = key;
            while(value.Equals(key)){
                value = list[Random.Range(0, list.Count)];
            }
            return value;
        }

        public static T GetRandom<T>(this T[] arr){
            return arr[Random.Range(0, arr.Length)];
        }

        public static Vector3 RandowPositionAround(this Vector3 position)
        {
            position.x += Random.Range(-1.5f, 1.5f);
            position.y += Random.Range(-1.5f, 1.5f);
            return position;
        }

        public static void AddNew<T>(this List<T> list, T obj)
        {
            if(!list.Contains(obj))
            {
                list.Add(obj);
            }
        }

        public static int IndexOf<T>(this T[] array, T obj)
        {
            for(int i = 0; i < array.Length; i++)
            {
                if(array[i].Equals(obj))
                {
                    return i;
                }
            }
            return -1;
        }

        public static bool Contains<T>(this T[] array, T obj)
        {
            foreach (T item in array)
            {
                if(item.Equals(obj))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
