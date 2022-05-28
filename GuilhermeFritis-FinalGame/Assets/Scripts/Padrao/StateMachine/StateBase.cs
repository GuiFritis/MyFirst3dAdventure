
namespace Padrao.StateMachine{
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
}
