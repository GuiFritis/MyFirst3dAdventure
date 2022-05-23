public class StateBase
{
    public virtual void OnStateEnter(object t = null)
    {
    }
    
    public virtual void OnStateStay()
    {
    }
    
    public virtual void OnStateExit()
    {
    }
}

public class StatePlaying : StateBase
{
    public override void OnStateEnter(object t = null)
    {
        
    }
}

public class StateResetingPosition : StateBase
{
    public override void OnStateEnter(object t = null)
    {
        
    }
}

public class StateEndGame : StateBase
{
    public override void OnStateEnter(object t = null)
    {

    }
}
