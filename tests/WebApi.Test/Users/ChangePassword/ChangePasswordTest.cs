using System.Globalization;
using System.Net;
using System.Text.Json;
using Common.TestUtilities.Requests;
using FinanceManager.Communication.Requests;
using FinanceManager.Exceptions;
using FluentAssertions;
using WebApi.Test.InlineData;

namespace WebApi.Test.Users.ChangePassword;

public class ChangePasswordTest : FinanceManagerClassFixture
{
    private const string Method = "api/user/change-password";
    private readonly string _token;
    private readonly string _password;
    private readonly string _email;
    
    public ChangePasswordTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.UserTeamMember.GetToken();
        _password = webApplicationFactory.UserTeamMember.GetPassword();
        _email = webApplicationFactory.UserTeamMember.GetEmail();
    }
    
    [Fact]
    public async Task Success()
    {
        var request = RequestChangePasswordJsonBuilder.Build();
        request.Password = _password;

        var response = await DoPut(Method, request, _token);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var loginRequest = new RequestLoginJson
        {
            Email = _email,
            Password = _password
        };

        response = await DoPost("api/login", loginRequest, _token);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        loginRequest.Password = request.NewPassword;

        response = await DoPost("api/login", loginRequest, _token);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task SamePasswordError(string culture)
    {
        var request = RequestChangePasswordJsonBuilder.Build();

        var response = await DoPut(Method, request, _token, culture);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);
        var errors = responseData.RootElement.GetProperty("errorMessages").EnumerateArray();
        var expectedMessage =
            ResourceErrorMessage.ResourceManager.GetString("PASSWORD_DIFFERENT_CURRENT_PASSWORD",
                new CultureInfo(culture));

        errors.Should()
            .HaveCount(1)
            .And
            .Contain(m => m.GetString()!.Equals(expectedMessage));

    }
}