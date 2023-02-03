using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static void Load(int i){
        SceneManager.LoadScene(i);
    }

    public static void Load(string s){
        SceneManager.LoadScene(s);
    }
}
