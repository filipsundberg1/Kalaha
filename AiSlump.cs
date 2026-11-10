public class AISlump : IAIDrag
{
    private static readonly Random random = new Random();
        
    public int VäljGrop(string[] namn, bool ärSpelare1, Spelplan spelplan)
    {
        int[] möjligaGropar = { 7, 8, 9, 10, 11, 12 }; 
        int grop;

        do
        {
            grop = möjligaGropar[random.Next(möjligaGropar.Length)];
        }

        while (spelplan.Gropar[grop].AntalKulor == 0);
        return grop;
    }
    
}
