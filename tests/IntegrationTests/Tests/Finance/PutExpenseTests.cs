using AutoFixture;
using LifeMastery.Core.Modules.Finance.Commands;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTests.Tests.Finance;

public sealed class PutExpenseTests : TestBase
{
    private readonly HttpClient _client;

    public PutExpenseTests(WebAppFactory factory) : base(factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ExpenseDoesNotExist_AddsNewExpense()
    {
        // Arrange
        var command = new PutExpenseRequest()
        {
            Amount = fixture.Create<decimal>()
        };

        //Act
        var response = await _client.PutAsync("/api/finance/expenses", JsonContent.Create(command));

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
