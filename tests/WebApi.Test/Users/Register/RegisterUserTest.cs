using System.Globalization;
using System.Net;
using System.Text.Json;
using Common.TestUtilities.Requests;
using FinanceManager.Exceptions;
using FluentAssertions;
using WebApi.Test.InlineData;

namespace WebApi.Test.Users.Register;

public class RegisterUserTest(CustomWebApplicationFactory webApplicationFactory) : FinanceManagerClassFixture(webApplicationFactory)
{
    private const string Method = "api/user";
    
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var result = await DoPost(Method, request);

        result.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("name").GetString().Should().Be(request.Name);
        response.RootElement.GetProperty("token").GetString().Should().NotBeNullOrEmpty();
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task ErrorNameEmpty(string cultureInfo)
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;
        
        var result = await DoPost(requestUri: Method, request: request, culture: cultureInfo);

        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMessage.ResourceManager.GetString("NAME_EMPTY", new CultureInfo(cultureInfo));

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
}
