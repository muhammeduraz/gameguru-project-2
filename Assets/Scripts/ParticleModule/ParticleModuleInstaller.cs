using Zenject;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.ParticleModule.Particles;

namespace Assets.Scripts.ParticleModule
{
    public class ParticleModuleInstaller : MonoInstaller<ParticleModuleInstaller>
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private Vector3 _positionOffset;

        [Header("References")]
        [SerializeField] private Transform _particleParent;
        [SerializeField] private List<BaseParticle> _particlePrefabList;
        
        #endregion Variables

        #region Functions

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ParticlePool>().AsSingle();
            Container.BindInterfacesAndSelfTo<ParticleFactory>().AsSingle().WithArguments(_particleParent, _particlePrefabList);
            Container.BindInterfacesAndSelfTo<ParticleProvider>().AsSingle().WithArguments(_positionOffset);
        }

        #endregion Functions
    }
}