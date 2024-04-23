namespace Assets.Scripts.CanvasModule
{
    public interface IPanel
    {
        #region Functions

        public void Appear();
        public void Disappear();

        public void AppearAsync();
        public void DisappearAsync();

        #endregion Functions
    }
}