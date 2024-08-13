﻿using System.Globalization;
using System.Net;
using System.Text.Json;
using Common.TestUtilities.Requests;
using FinanceManager.Exceptions;
using FluentAssertions;
using WebApi.Test.InlineData;

namespace WebApi.Test.Users.Update;

public class UpdateUserTest : FinanceManagerClassFixture
{
    private const string Method = "api/user";
    private readonly string _token;

    public UpdateUserTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.UserTeamMember.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestUpdateUserJsonBuilder.Build();

        var response = await DoPut(Method, request, _token);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task EmptyNameError(string culture)
    {
        var request = RequestUpdateUserJsonBuilder.Build();
        request.Name = string.Empty;

        var response = await DoPut(Method, request, _token, culture);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errorMessages").EnumerateArray();
        var expectedMessage = ResourceErrorMessage.ResourceManager.GetString("NAME_EMPTY", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(c => c.GetString()!.Equals(expectedMessage));

    }
}