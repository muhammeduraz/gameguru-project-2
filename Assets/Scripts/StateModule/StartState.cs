using UnityEngine;
using Assets.Scripts.InputModule;
using Assets.Scripts.CanvasModule;
using Assets.Scripts.FinishModule;
using Assets.Scripts.PlayerModule;

namespace Assets.Scripts.StateModule
{
    public class StartState : BaseState
    {
        #region Variables

        private float _finishLineDistance;

        private Player _player;
        private CustomInput _customInput;
        private CanvasManager _canvasManager;
        private FinishManager _finishManager;

        #endregion Variables

        #region Functions

        public StartState(Player player, CustomInput customInput, CanvasManager canvasManager, FinishManager finishManager) : base()
        {
            _player = player;
            _customInput = customInput;
            _canvasManager = canvasManager;
            _finishManager = finishManager;
        }

        public override void Dispose()
        {
            base.Dispose();

            _customInput = null;
            _canvasManager = null;
            _finishManager = null;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            _finishManager.PlaceNewFinishLine(_player.Position + _finishLineDistance * Vector3.forward);
            _customInput.Disable();
            _canvasManager.Appear(typeof(CanvasStartPanel));
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        #endregion Functions
    }
}