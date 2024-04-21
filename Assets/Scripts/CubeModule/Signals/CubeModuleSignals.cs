namespace Assets.Scripts.CubeModule.Signals
{
    #region Signals

    public struct CubePlacedSignal
    {
        private Cube _cube;
        public Cube Cube { get => _cube; set => _cube = value; }
        
        public CubePlacedSignal(Cube cube)
        {
            _cube = cube;
        }
    }

    #endregion Signals
}