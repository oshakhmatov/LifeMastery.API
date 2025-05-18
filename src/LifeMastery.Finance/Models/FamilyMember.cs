namespace LifeMastery.Finance.Models;

public class FamilyMember
{
    public int Id { get; private set; }
    public string Name { get; set; }

    public FamilyMember(string name)
    {
        Name = name;
    }

    protected FamilyMember() { }
}
