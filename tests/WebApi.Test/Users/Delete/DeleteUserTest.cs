using System.Net;
using FluentAssertions;

namespace WebApi.Test.Users.Delete;
public class DeleteUserTest : FinanceManagerClassFixture
{
    private const string Method = "api/user";
    private readonly string _token;

    public DeleteUserTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.UserTeamMember.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoDelete(Method, _token);

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}

