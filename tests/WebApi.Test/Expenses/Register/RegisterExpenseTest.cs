using System.Globalization;
using System.Net;
using System.Text.Json;
using Common.TestUtilities.Requests;
using FinanceManager.Exceptions;
using FluentAssertions;
using WebApi.Test.InlineData;

namespace WebApi.Test.Expenses.Register;

public class RegisterExpenseTest : FinanceManagerClassFixture
{
    private const string Method = "api/expenses";
    private readonly string _token;
    
    public RegisterExpenseTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.GetToken();
    }
    

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();
        
        var result = await DoPost(requestUri: Method, request: request, token: _token);
        
        result.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("title").GetString().Should().Be(request.Title);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task ErrorTitleEmpty(string culture)
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = string.Empty;
        
        var result = await DoPost(requestUri: Method, request: request, token: _token, culture: culture);
        
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage =
            ResourceErrorMessage.ResourceManager.GetString("TITLE_REQUIRED", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));


    }
}