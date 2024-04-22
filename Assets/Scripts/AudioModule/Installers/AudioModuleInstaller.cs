using Zenject;
using UnityEngine;
using Assets.Scripts.AudioModule.Data;

namespace Assets.Scripts.AudioModule
{
    public class AudioModuleInstaller : MonoInstaller<AudioModuleInstaller>
    {
        #region Variables

        [Header("Components")]
        [SerializeField] private AudioSource _audioSource;

        [Header("References")]
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private AudioPlayerDataSO _audioPlayerDataSO;

        #endregion Variables

        #region Functions

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AudioSourceManager>().AsSingle().WithArguments(_audioSource);
            Container.BindInterfacesAndSelfTo<CubePlacementAudioPlayer>().AsSingle().WithArguments(_audioClip, _audioPlayerDataSO);
        }

        #endregion Functions
    }
}