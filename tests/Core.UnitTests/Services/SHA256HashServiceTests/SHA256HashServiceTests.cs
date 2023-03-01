using Core.Services;

namespace Core.UnitTests.Services.SHA256HashServiceTests;

public abstract class SHA256HashServiceTests
{
	protected readonly SHA256HashService _sut;

    public SHA256HashServiceTests()
	{
		_sut = new SHA256HashService();
	}
}
