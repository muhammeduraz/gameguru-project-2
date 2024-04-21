using System;
using System.Collections.Generic;
using Assets.Scripts.ParticleModule.Particles;

namespace Assets.Scripts.ParticleModule
{
    public class ParticlePool : IDisposable
    {
        #region Variables

        private BaseParticle _cacheParticle;
        private Stack<BaseParticle> _cacheStack;
        private Dictionary<Type, Stack<BaseParticle>> _poolDict;

        #endregion Variables

        #region Functions

        public ParticlePool()
        {
            _poolDict = new Dictionary<Type, Stack<BaseParticle>>();
        }

        public void Dispose()
        {
            _poolDict = null;
            _cacheStack = null;
            _cacheParticle = null;
        }

        private bool HasParticle(Type type)
        {
            if (!_poolDict.ContainsKey(type)) return false;

            bool got = _poolDict.TryGetValue(type, out _cacheStack);
            if (!got || _cacheStack.Count < 1) return false;

            return true;
        }

        public void AddBackToPool(BaseParticle particle)
        {
            particle.OnTerminated -= AddBackToPool;

            bool got = _poolDict.TryGetValue(particle.GetType(), out _cacheStack);
            if (!got)
            {
                Stack<BaseParticle> newStack = new Stack<BaseParticle>();
                newStack.Push(particle);

                _poolDict.Add(particle.GetType(), newStack);
                return;
            }

            _cacheStack.Push(particle);
        }

        public BaseParticle GetParticle(Type type)
        {
            if (!HasParticle(type)) return null;

            _poolDict.TryGetValue(type, out _cacheStack);
            _cacheParticle = _cacheStack.Pop();
            _cacheParticle.OnTerminated += AddBackToPool;

            return _cacheParticle;
        }

        #endregion Functions
    }
}