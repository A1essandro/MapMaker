namespace Structure.Impl
{
    public class OverlayFloatSum : IOverlay<float>
    {
        public float Overlay(float cell1, float cell2)
        {
            return cell1 + cell2;
        }
    }
}
