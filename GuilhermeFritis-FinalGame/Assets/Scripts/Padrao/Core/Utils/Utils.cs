using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{  
    public static class Utils
    {
        #if UNITY_EDITOR
        [UnityEditor.MenuItem("Ebac/TEST %g")]
        public static void Test(){
            Debug.Log("Test");
        }
        #endif

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

    }
}
