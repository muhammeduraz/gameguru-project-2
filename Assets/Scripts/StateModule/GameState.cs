using Assets.Scripts.CubeModule;
using Assets.Scripts.InputModule;

namespace Assets.Scripts.StateModule
{
    public class GameState : BaseState
    {
        #region Variables

        private CubePlacer _cubePlacer;
        private CustomInput _customInput;

        #endregion Variables

        #region Functions

        public GameState(CustomInput customInput, CubePlacer cubePlacer) : base()
        {
            _cubePlacer = cubePlacer;
            _customInput = customInput;
        }

        public override void Dispose()
        {
            base.Dispose();

            _cubePlacer = null;
            _customInput = null;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            _cubePlacer.Reinitialize();
            _cubePlacer.Enable();

            _customInput.Enable();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        #endregion Functions
    }
}