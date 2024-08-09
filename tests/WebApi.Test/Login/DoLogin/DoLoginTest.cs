using System.Globalization;
using System.Net;
using System.Text.Json;
using Common.TestUtilities.Requests;
using FinanceManager.Communication.Requests;
using FinanceManager.Exceptions;
using FluentAssertions;
using WebApi.Test.InlineData;

namespace WebApi.Test.Login.DoLogin;

public class DoLoginTest : FinanceManagerClassFixture
{
    private const string Method = "api/login";
    private readonly string _email;
    private readonly string _password;
    private readonly string _name;
    
    public DoLoginTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _name = webApplicationFactory.UserTeamMember.GetName();
        _password = webApplicationFactory.UserTeamMember.GetPassword();
        _email = webApplicationFactory.UserTeamMember.GetEmail();
        _name = webApplicationFactory.UserTeamMember.GetName();

    }
    
    [Fact]
    public async Task Success()
    {
        var request = new RequestLoginJson
        { 
            Email = _email,
            Password = _password
        };

        var response = await DoPost(requestUri: Method, request: request);

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
        
        var response = await DoPost(Method, request: request, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMessage.ResourceManager.GetString("INVALID_USERNAME_AND_PASSWORD", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(c => c.GetString()!.Equals(expectedMessage));
    }
}
