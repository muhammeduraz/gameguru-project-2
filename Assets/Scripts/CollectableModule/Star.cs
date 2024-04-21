using Assets.Scripts.ParticleModule.Signals;
using Assets.Scripts.ParticleModule.Particles;

namespace Assets.Scripts.Collectables
{
    public class Star : BaseCollectable
    {
        #region Functions

        public override void OnInteract()
        {
            Disable();
            signalBus.Fire(new ParticleRequestSignal(typeof(StarParticle), transform.position));
        }

        #endregion Functions
    }
}