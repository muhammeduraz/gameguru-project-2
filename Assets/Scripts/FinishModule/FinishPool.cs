using Zenject;
using System.Collections.Generic;

namespace Assets.Scripts.FinishModule
{
    public class FinishPool : MonoMemoryPool<Finish>
    {
        #region Variables

        private int _maxAliveFinishCount = 2;

        private Finish _cacheFinish;
        private Queue<Finish> _finishQueue;

        #endregion Variables

        #region Functions

        public FinishPool()
        {
            _finishQueue = new Queue<Finish>();
        }

        protected override void OnSpawned(Finish finish)
        {
            base.OnSpawned(finish);

            _finishQueue.Enqueue(finish);

            DespawnIfPossible();
        }

        private void DespawnIfPossible()
        {
            if (_finishQueue.Count > _maxAliveFinishCount)
            {
                _cacheFinish = _finishQueue.Dequeue();
                Despawn(_cacheFinish);
            }
        }

        #endregion Functions
    }
}