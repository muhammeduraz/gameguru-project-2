using System;
using Zenject;
using System.Collections.Generic;
using Assets.Scripts.CanvasModule;
using Assets.Scripts.FinishModule;

namespace Assets.Scripts.StateModule
{
    public class StateHandler : IInitializable, IDisposable
    {
        #region Variables

        private SignalBus _signalBus;

        private BaseState _currentState;

        private WinState _winState;
        private FailState _failState;
        private GameState _gameState;
        private StartState _startState;

        private List<BaseState> _stateList;

        #endregion Variables

        #region Functions

        public StateHandler(SignalBus signalBus, WinState winState, FailState failState, GameState gameState, StartState startState)
        {
            _signalBus = signalBus;

            _winState = winState;
            _failState = failState;
            _gameState = gameState;
            _startState = startState;

            _stateList = new List<BaseState>()
            {
                _winState,
                _failState,
                _gameState,
                _startState,
            };
        }

        public void Initialize()
        {
            ChangeState(typeof(StartState));

            _signalBus.Subscribe<GameFailSignal>(OnGameFailSignalFired);
            _signalBus.Subscribe<GameWinSignal>(OnFinishInteractSignalFired);
            _signalBus.Subscribe<StartButtonClickedSignal>(OnStartButtonClickedSignalFired);
            _signalBus.Subscribe<RetryButtonClickedSignal>(OnRetryButtonClickedSignalFired);
            _signalBus.Subscribe<ContinueButtonClickedSignal>(OnContinueButtonClickedSignalFired);
        }

        public void Dispose()
        {
            _currentState = null;

            _winState = null;
            _failState = null;
            _gameState = null;
            _startState = null;

            _stateList = null;

            _signalBus.Unsubscribe<GameFailSignal>(OnGameFailSignalFired);
            _signalBus.Unsubscribe<GameWinSignal>(OnFinishInteractSignalFired);
            _signalBus.Unsubscribe<StartButtonClickedSignal>(OnStartButtonClickedSignalFired);
            _signalBus.Unsubscribe<RetryButtonClickedSignal>(OnRetryButtonClickedSignalFired);
            _signalBus.Unsubscribe<ContinueButtonClickedSignal>(OnContinueButtonClickedSignalFired);
            _signalBus = null;
        }

        public void ChangeState(Type stateType)
        {
            _currentState?.OnStateExit();
            _currentState = GetState(stateType);
            _currentState?.OnStateEnter();
        }

        private BaseState GetState(Type stateType)
        {
            return _stateList.Find(state => state.GetType() == stateType);
        }

        private void OnGameFailSignalFired()
        {
            ChangeState(typeof(FailState));
        }

        private void OnFinishInteractSignalFired()
        {
            ChangeState(typeof(WinState));
        }

        private void OnStartButtonClickedSignalFired()
        {
            ChangeState(typeof(GameState));
        }

        private void OnRetryButtonClickedSignalFired()
        {
            ChangeState(typeof(StartState));
        }

        private void OnContinueButtonClickedSignalFired()
        {
            ChangeState(typeof(StartState));
        }

        #endregion Functions
    }
}