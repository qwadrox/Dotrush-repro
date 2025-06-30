namespace Project.Test;

public class UnitTest1
{
    [Theory]
    [InlineData("11720049-21455883-00000000", new[] { "11720049-21455883-00000000", "1172004921455883" }, 2)]
    [InlineData("10300002-20391940-00003285", new[] { "10300002-20391940-00003285" }, 1)]
    [InlineData("1172004921455883", new[] { "1172004921455883", "117200492145588300000000" }, 2)]
    [InlineData("109180010000009309780008", new[] { "109180010000009309780008" }, 1)]
    [InlineData("11 72 00 49 21 45 58 83", new[] { "11 72 00 49 21 45 58 83", "117200492145588300000000" }, 2)]
    [InlineData("10 91 80 01 00 00 00 93 09 78 00 08", new[] { "10 91 80 01 00 00 00 93 09 78 00 08" }, 1)]
    [InlineData("1091 8001 0000 0093 0978 0008", new[] { "1091 8001 0000 0093 0978 0008" }, 1)]
    [InlineData("1172 0049 2145 5883", new[] { "1172 0049 2145 5883", "117200492145588300000000" }, 2)]
    public void GenerateBankAccountVariations_TheoryTests(
        string bankAccount,
        string[] expectedVariations,
        int expectedCount
    )
    {
        // Act
        // Assert
    }
}
