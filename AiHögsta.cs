public class AIHögsta : IAIDrag
{
    public int VäljGrop(string[] namn, bool ärSpelare1, Spelplan spelplan)
    {
        int maxKulor = 0;
        int valdGrop = 7;  
        for (int i = 7; i <= 12; i++)
        {
            if (spelplan.Gropar[i].AntalKulor > maxKulor)
            {
                maxKulor = spelplan.Gropar[i].AntalKulor;
                valdGrop = i;
            }
        }
        return valdGrop;
    }
}
