using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Padrao.Core.Singleton;

public class PauseManager : Singleton<PauseManager>
{
    public void Pause(){
        Time.timeScale = 0;
    }

    public void Resume(){
        Time.timeScale = 1;
    }
}
