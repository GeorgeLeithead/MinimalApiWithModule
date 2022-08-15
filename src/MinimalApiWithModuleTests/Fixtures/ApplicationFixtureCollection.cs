namespace MinimalApiWithModuleTests.Fixtures;

using Xunit;

/// <summary>Application Fixture Collection.</summary>
/// <remarks>This class has no code, and is never created. Its purpose is simply to be the place to apply [CollectionDefinition] and all the ICollectionFixture<> interfaces.</remarks>
[CollectionDefinition("Context Collection")]
public class ApplicationFixtureCollection : ICollectionFixture<ApplicationFixture>
{
}