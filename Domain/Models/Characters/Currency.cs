namespace Domain.Models.Characters;

public class Currency
{
    public int Copper { get; set; }
    public int Silver { get; set; }
    public int Electrum { get; set; }
    public int Gold { get; set; }
    public int Platinum { get; set; }

    public int TotalInCopper()
    {
        return Copper + (Silver * 10) + (Electrum * 50) + (Gold * 100) + (Platinum * 1000);
    }
    
    public void Add(Currency other)
    {
        Copper += other.Copper;
        Silver += other.Silver;
        Electrum += other.Electrum;
        Gold += other.Gold;
        Platinum += other.Platinum;
    }
    
    public bool Subtract(Currency other)
    {
        var totalNeeded = other.TotalInCopper();
        var totalHave = TotalInCopper();
        
        if (totalHave < totalNeeded) return false;
        
        var remaining = totalHave - totalNeeded;
        ConvertFromCopper(remaining);
        
        return true;
    }
    
    private void ConvertFromCopper(int copperAmount)
    {
        Platinum = copperAmount / 1000;
        copperAmount %= 1000;
        
        Gold = copperAmount / 100;
        copperAmount %= 100;
        
        Electrum = copperAmount / 50;
        copperAmount %= 50;
        
        Silver = copperAmount / 10;
        copperAmount %= 10;
        
        Copper = copperAmount;
    }
}