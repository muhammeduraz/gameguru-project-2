using System;

namespace Assets.Scripts.StateModule
{
    public class BaseState : IDisposable
    {
        #region Functions

        public BaseState() { }

        public virtual void Dispose() { }

        public virtual void OnStateEnter() { }

        public virtual void OnStateExit() { }

        #endregion Functions
    }
}