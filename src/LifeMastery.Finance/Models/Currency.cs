namespace LifeMastery.Finance.Models;

public class Currency
{
    public int Id { get; private set; }
    public string Name { get; set; }

    public Currency(string name)
    {
        Name = name;
    }

    protected Currency() { }
}
