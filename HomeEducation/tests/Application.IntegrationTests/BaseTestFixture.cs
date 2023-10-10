using NUnit.Framework;

using static HomeEducation.Application.IntegrationTests.Testing;

namespace HomeEducation.Application.IntegrationTests;
[TestFixture]
public abstract class BaseTestFixture
{
    [SetUp]
    public async Task TestSetUp()
    {
        await ResetState();
    }
}
