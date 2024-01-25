namespace LifeMastery.Core.Modules.Finance.Models;

public sealed class Payment
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }

    protected Payment() { }
}
