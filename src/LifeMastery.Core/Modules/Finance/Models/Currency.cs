namespace LifeMastery.Core.Modules.Finance.Models;

public class Currency
{
    public int Id { get; set; }
    public string Name { get; set; }

    protected Currency() { }
    public Currency(string name)
    {
        Name = name;
    }
}
