﻿using System.Net;
using System.Text.Json;
using FluentAssertions;

namespace WebApi.Test.Expenses.GetAll;

public class GetAllExpensesTest : FinanceManagerClassFixture
{
    private const string Method = "api/expenses";
    private readonly string _token;

    public GetAllExpensesTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.UserTeamMember.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoGet(requestUri: Method, token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("expenses").EnumerateArray().Should().NotBeNullOrEmpty();

    }
}