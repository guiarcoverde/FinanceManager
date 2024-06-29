using Common.TestUtilities.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace WebApi.Test.Users.Register;

public class RegisterUserTest : IClassFixture<WebApplicationFactory<Program>>
{
    private const string Method = "api/user";

    private readonly HttpClient _httpClient;

    public RegisterUserTest(CustomWebApplicationFactory webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
  
        var result = await _httpClient.PostAsJsonAsync(Method, request);

        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}
