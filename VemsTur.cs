public class VemsTur
{
    public bool ÄrSpelare1sTur { get; private set; }

    public VemsTur(bool ÄrSpelare1sTur)
    {
        this.ÄrSpelare1sTur = ÄrSpelare1sTur; 
    }

    public void BytTur()
    {
        ÄrSpelare1sTur = !ÄrSpelare1sTur; 
    }
}
