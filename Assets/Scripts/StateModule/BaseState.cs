using System;

namespace Assets.Scripts.StateModule
{
    public class BaseState : IDisposable
    {
	    #region Variables

        

        #endregion Variables

	    #region Properties

        

        #endregion Properties

        #region Functions

        public BaseState()
        {

        }

        public virtual void Dispose()
        {
            
        }

        public virtual void OnStateEnter()
        {

        }

        public virtual void OnStateExit()
        {

        }

        #endregion Functions
    }
}