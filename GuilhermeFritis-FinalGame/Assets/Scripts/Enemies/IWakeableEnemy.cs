using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWakeableEnemy
{
    void WakeUp(GameObject player);
    void Sleep();
}
