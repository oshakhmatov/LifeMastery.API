using AutoFixture;
using AutoFixture.AutoMoq;

namespace UnitTests.Common;

public abstract class TestBase 
{ 
    private protected readonly IFixture fixture;

    public TestBase()
    {
        fixture = new Fixture().Customize(new AutoMoqCustomization());
        fixture.Customizations.Add(new DateOnlySpecimenBuilder());
    }
}
