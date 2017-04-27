namespace Structure
{
    public interface IOverlay<TCell>
    {

        TCell Overlay(TCell cell1, TCell cell2);

    }
}
