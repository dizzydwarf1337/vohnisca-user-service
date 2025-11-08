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
}