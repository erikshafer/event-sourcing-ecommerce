using FluentAssertions;
using Xunit;

namespace Ecommerce.Core.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1_Will_Be_Removed()
    {
        const bool value = true;
        value.Should().BeTrue();
    }
}