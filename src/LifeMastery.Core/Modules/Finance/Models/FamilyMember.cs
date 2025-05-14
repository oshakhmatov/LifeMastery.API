namespace LifeMastery.Core.Modules.Finance.Models;

public class FamilyMember
{
    public int Id { get; set; }
    public string Name { get; set; }

    public FamilyMember(string name)
    {
        Name = name;
    }

    protected FamilyMember() { }
}
