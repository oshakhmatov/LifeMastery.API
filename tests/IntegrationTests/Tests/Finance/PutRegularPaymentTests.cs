using AutoFixture;
using LifeMastery.Core.Modules.Finance.Commands.RegularPayments;
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
        var request = fixture.Create<PutRegularPaymentRequest>();
        request.Id = null;

        //Act
        var response = await _client.PutAsync("/api/finance/regular-payments", JsonContent.Create(request));

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
