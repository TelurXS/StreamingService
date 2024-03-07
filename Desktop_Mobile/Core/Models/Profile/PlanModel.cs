namespace Metflix.Core.Models;

public class PlanModel
{
    private int id;
    private string name;
    private string pricePerMonth;
    private string description;

    public int Id => id;

    public string Name => name;

    public string PricePerMonth => pricePerMonth;

    public string Description => description;

    public PlanModel(string name, string pricePerMonth, string description)
    {
        this.name = name;
        this.pricePerMonth = pricePerMonth;
        this.description = description;
    }
}