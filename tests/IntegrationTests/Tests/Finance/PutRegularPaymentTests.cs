using AutoFixture;
using LifeMastery.Core.Modules.Finance.Commands;
using LifeMastery.Core.Modules.Finance.Enums;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTests.Tests.Finance;

public sealed class PutRegularPaymentTests : TestBase
{
    private readonly HttpClient _client;

    public PutRegularPaymentTests(WebAppFactory factory) : base(factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task RegularPaymentNotExists_AddsNew()
    {
        // Arrange
        var command = new PutRegularPaymentRequest
        {
            DeadlineDay = fixture.Create<int>(),
            DeadlineMonth = fixture.Create<int>(),
            Name = fixture.Create<string>(),
            Period = fixture.Create<Period>()
        };

        //Act
        var response = await _client.PutAsync("/api/finance/regular-payments", JsonContent.Create(command));

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
