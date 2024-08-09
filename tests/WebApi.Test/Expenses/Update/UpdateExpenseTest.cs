using System.Globalization;
using System.Net;
using System.Text.Json;
using Common.TestUtilities.Requests;
using FinanceManager.Exceptions;
using FluentAssertions;
using WebApi.Test.InlineData;

namespace WebApi.Test.Expenses.Update;

public class UpdateExpenseTest : FinanceManagerClassFixture
{
    private const string Method = "api/expenses"; 
    private readonly string _token;
    private readonly long _expenseId;
    
    public UpdateExpenseTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.UserTeamMember.GetToken();
        _expenseId = webApplicationFactory.Expense.GetId();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestExpenseJsonBuilder.Build();
        var result = 
            await DoPut(requestUri: $"{Method}/{_expenseId}", request: request, token: _token);
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task TitleEmptyError(string culture)
    {
        var request = RequestExpenseJsonBuilder.Build();
        request.Title = string.Empty;
        var result = 
            await DoPut(requestUri: $"{Method}/{_expenseId}", request: request, token: _token, culture: culture);

        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();
        var expectedMessage =
            ResourceErrorMessage.ResourceManager.GetString("TITLE_REQUIRED", new CultureInfo(culture));

        errors
            .Should()
            .HaveCount(1)
            .And
            .Contain(error => error.GetString()!.Equals(expectedMessage));
    }
    
    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task ExpenseNotFoundError(string culture)
    {
        var request = RequestExpenseJsonBuilder.Build();
        var result = 
            await DoPut(requestUri: $"{Method}/1000", request: request, token: _token, culture: culture);

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();
        var expectedMessage =
            ResourceErrorMessage.ResourceManager.GetString("EXPENSE_NOT_FOUND", new CultureInfo(culture));

        errors
            .Should()
            .HaveCount(1)
            .And
            .Contain(error => error.GetString()!.Equals(expectedMessage));
    }
}
