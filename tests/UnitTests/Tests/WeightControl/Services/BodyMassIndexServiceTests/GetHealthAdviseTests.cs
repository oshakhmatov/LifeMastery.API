using LifeMastery.Health.Services;

namespace UnitTests.Modules.WeightControl.Services.BodyMassIndexServiceTests;

public sealed class GetHealthAdviseTests
{
    [Fact]
    public void WeightIsOkAndCouldBeDecreased_ShouldReturnLightAdvise()
    {
        var cut = new BodyMassIndexService();

        var result = cut.GetHealthAdvise(minWeight: 60, maxWeight: 80, perfectWeight: 70, actualWeight: 75);

        Assert.Equal("Можно сбросить 5 кг", result);
    }

    [Fact]
    public void WeightIsOkAndCouldBeIncreased_ShouldReturnLightAdvise()
    {
        var cut = new BodyMassIndexService();

        var result = cut.GetHealthAdvise(minWeight: 60, maxWeight: 80, perfectWeight: 70, actualWeight: 65);

        Assert.Equal("Можно набрать 5 кг", result);
    }

    [Fact]
    public void WeightIsNotOkAndShouldBeDecreased_ShouldReturnStrongAdvise()
    {
        var cut = new BodyMassIndexService();

        var result = cut.GetHealthAdvise(minWeight: 60, maxWeight: 80, perfectWeight: 70, actualWeight: 85);

        Assert.Equal("Стоит сбросить 5 кг", result);
    }

    [Fact]
    public void WeightIsNotOkAndShouldBeIncreased_ShouldReturnStrongAdvise()
    {
        var cut = new BodyMassIndexService();

        var result = cut.GetHealthAdvise(minWeight: 60, maxWeight: 80, perfectWeight: 70, actualWeight: 55);

        Assert.Equal("Стоит набрать 5 кг", result);
    }

    [Fact]
    public void WeightDeltaIsFractionalNumber_ShouldReturnOneDecimalPlaceWithComma()
    {
        var cut = new BodyMassIndexService();

        var result = cut.GetHealthAdvise(minWeight: 60, maxWeight: 80, perfectWeight: 70, actualWeight: 54.9);

        Assert.Equal("Стоит набрать 5,1 кг", result);
    }
}
