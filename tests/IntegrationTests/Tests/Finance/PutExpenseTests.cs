using AutoFixture;
using LifeMastery.Core.Modules.Finance.Commands.Expenses;
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
        var request = fixture.Create<PutExpenseRequest>();

        //Act
        var response = await _client.PutAsync("/api/finance/expenses", JsonContent.Create(request));

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
