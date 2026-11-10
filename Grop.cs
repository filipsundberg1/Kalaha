public class Grop
{
    public int AntalKulor { get; private set; }

    public Grop(int antalKulor)
    {
        this.AntalKulor = antalKulor;
    }

    public int TaKulor()
    {
        int kulor = AntalKulor;
        AntalKulor = 0;
        return kulor;
    }

    public void LäggTillKula()
    {
        AntalKulor++;
    }

    public void LäggTillKulor(int antal)
    {
        AntalKulor += antal;
    }
}
