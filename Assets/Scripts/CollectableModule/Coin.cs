using Assets.Scripts.ParticleModule.Signals;
using Assets.Scripts.ParticleModule.Particles;

namespace Assets.Scripts.Collectables
{
    public class Coin : BaseCollectable
    {
        #region Functions

        public override void OnInteract()
        {
            Disable();
            signalBus.Fire(new ParticleRequestSignal(typeof(CoinParticle), transform.position));
        }

        #endregion Functions
    }
}