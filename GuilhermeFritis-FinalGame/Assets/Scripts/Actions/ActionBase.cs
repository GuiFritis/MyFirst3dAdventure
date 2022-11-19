using UnityEngine;
using UnityEngine.InputSystem;

namespace Actions{
    public class ActionBase : MonoBehaviour
    {
        public InputActionReference inputAction;

        void Start()
        {
            Init();
        }

        protected virtual void Init()
        {            
            inputAction.asset.Enable();
            inputAction.action.started += ctx => CallAction();
        }

        public virtual void CallAction() {}
    }
}