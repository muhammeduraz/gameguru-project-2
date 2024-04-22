namespace Assets.Scripts.CubeModule.Signals
{
    #region Signals

    public struct CubePlacedSignal
    {
        private bool _correctly;
        private Cube _cube;

        public bool Correctly { get => _correctly; set => _correctly = value; }
        public Cube Cube { get => _cube; set => _cube = value; }

        public CubePlacedSignal(bool correctly, Cube cube)
        {
            _correctly = correctly;
            _cube = cube;
        }
    }

    #endregion Signals
}