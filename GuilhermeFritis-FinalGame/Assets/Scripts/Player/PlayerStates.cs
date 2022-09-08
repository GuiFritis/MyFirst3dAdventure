using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Padrao.StateMachine;

public class PlayerIdle : StateBase{

    public override void OnStateEnter(params object[] objs)
    {
        
    }
}

public class PlayerWalking : StateBase{

    public override void OnStateEnter(params object[] objs)
    {
        GameManager.Instance.player.animator.SetBool(GameManager.Instance.player.animWalk, true);
    }

    public override void OnStateExit()
    {        
        GameManager.Instance.player.animator.SetBool(GameManager.Instance.player.animWalk, false);
    }
}

public class PlayerRunning : StateBase{

    public override void OnStateEnter(params object[] objs)
    {
        GameManager.Instance.player.animator.SetBool(GameManager.Instance.player.animWalk, true);
        GameManager.Instance.player.animator.speed = GameManager.Instance.player.speedMultiplier; 
    }

    public override void OnStateExit()
    {        
        GameManager.Instance.player.animator.speed = 1f; 
        GameManager.Instance.player.animator.SetBool(GameManager.Instance.player.animWalk, false);
    }
}

public class PlayerJumping : StateBase{

    public override void OnStateEnter(params object[] objs)
    {
        GameManager.Instance.player.Jump();
    }
}
