using Common.TestUtilities.Requests;
using FinanceManager.Communication.Requests;
using FinanceManager.Exceptions;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.Login.DoLogin;

public class DoLoginTest(CustomWebApplicationFactory webApplicationFactory) : IClassFixture<CustomWebApplicationFactory>
{
    private const string Method = "api/login";
    private readonly HttpClient _httpClient = webApplicationFactory.CreateClient();
    private readonly string _email = webApplicationFactory.GetEmail();
    private readonly string _name = webApplicationFactory.GetName();
    private readonly string _password = webApplicationFactory.GetPassword();

    [Fact]
    public async Task Success()
    {
        var request = new RequestLoginJson
        { 
            Email = _email,
            Password = _password
        };

        var response = await _httpClient.PostAsJsonAsync(Method, request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("name").GetString().Should().Be(_name);
        responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task ErrorLoginInvalid(string culture)
    {
        var request = RequestLoginJsonBuilder.Build();
        
        _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(culture));
        
        var response = await _httpClient.PostAsJsonAsync(Method, request);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMessage.ResourceManager.GetString("INVALID_USERNAME_AND_PASSWORD", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(c => c.GetString()!.Equals(expectedMessage));
    }
}
