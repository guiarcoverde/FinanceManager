﻿using System.Net;
using System.Text.Json;
using FluentAssertions;

namespace WebApi.Test.Users.Profile;

public class GetUserProfileTest : FinanceManagerClassFixture
{
    private const string Method = "api/user";
    private readonly string _token;
    private readonly string _userName;
    private readonly string _userEmail;

    public GetUserProfileTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.UserTeamMember.GetToken();
        _userName = webApplicationFactory.UserTeamMember.GetName();
        _userEmail = webApplicationFactory.UserTeamMember.GetEmail();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoGet(Method, token: _token);
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("name").GetString().Should().Be(_userName);
        response.RootElement.GetProperty("email").GetString().Should().Be(_userEmail);

    }
}