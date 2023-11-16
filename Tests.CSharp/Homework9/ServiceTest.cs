using System.Linq.Expressions;
using Hw9;
using Hw9.ErrorMessages;
using Hw9.Services.ExpressionTree;
using Hw9.Services.MathCalculator;
using Hw9.Services.ParserAndValidator;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
namespace Tests.CSharp.Homework9;

public class ServicesTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ServicesTest(WebApplicationFactory<Program> fixture)
    {
        _client = fixture.CreateClient();
    }

    [Theory]
    [InlineData("-2 - 7", "-2 7 - ")]
    [InlineData("(-2) - (-7)", "-2 -7 - ")]
    [InlineData("(-2 + 2)", "-2 2 + ")]
    [InlineData("-4 - (-4)", "-4 -4 - ")]
    [InlineData("1 - 1 - 2", "1 1 - 2 - ")]
    public async Task ParseToPosrfixForm(string expression, string result)
    {
        var response = Parser.ConvertToPostfixForm(expression);

        Assert.Equal(result, response);
    }

    [Theory]
    [InlineData(ExpressionType.And,1,1 )]
    [InlineData(ExpressionType.Block,1,1 )]
    async Task ExpressionVisitorCalculateTest(ExpressionType expressionType, double first,double second)
    {
        var response = () => (object)ExpressionTreeVisitor.Calculate(expressionType,first,second);
        var exception = Assert.Throws<Exception>(response);
        Assert.Equal(MathErrorMessager.UnknownCharacter, exception.Message);
    }
}
