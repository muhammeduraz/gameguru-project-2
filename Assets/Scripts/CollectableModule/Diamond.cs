using Assets.Scripts.ParticleModule.Signals;
using Assets.Scripts.ParticleModule.Particles;

namespace Assets.Scripts.Collectables
{
    public class Diamond : BaseCollectable
    {
        #region Functions

        public override void OnInteract()
        {
            Disable();
            signalBus.Fire(new ParticleRequestSignal(typeof(DiamondParticle), transform.position));
        }

        #endregion Functions
    }
}