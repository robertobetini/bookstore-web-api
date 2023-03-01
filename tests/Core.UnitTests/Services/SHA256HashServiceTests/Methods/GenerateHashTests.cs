using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace Core.UnitTests.Services.SHA256HashServiceTests.Methods;

public class GenerateHashTests : SHA256HashServiceTests
{
    private const string ALGORITHM_NAME = SecurityAlgorithms.Sha256;

    [Theory]
    [InlineData("Arbitrary data.")]
    [InlineData("Some special characters: àóÍîüãÒ^`´´Çç")]
    public void When_Invoked_Then_ReturnsBase64EncodedHashString(string data)
    {
        #region Arrange
        var hashAlgorithm = HashAlgorithm.Create(ALGORITHM_NAME);
        var keyBytes = Encoding.UTF8.GetBytes(data);
        var hashBytes = hashAlgorithm.ComputeHash(keyBytes);
        var expected = Convert.ToBase64String(hashBytes);
        #endregion

        // Act
        var result = _sut.GenerateHash(data);

        // Assert
        Assert.Equal(expected, result);
    }
}
