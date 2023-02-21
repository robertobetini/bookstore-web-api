using Core.Exceptions;
using Core.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace Core.Services;

public class SHA256HashService : IHashService
{
    private const string ALGORITHM_NAME = SecurityAlgorithms.Sha256;

    private readonly HashAlgorithm _hashAlgorithm;

    public SHA256HashService()
    {
        _hashAlgorithm = HashAlgorithm.Create(ALGORITHM_NAME);

        if (_hashAlgorithm is null)
        {
            throw new InvalidHashAlgorithmException(ALGORITHM_NAME);
        }
    }

    public string GenerateHash(string key)
    {
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var hashBytes = _hashAlgorithm.ComputeHash(keyBytes);

        return Convert.ToBase64String(hashBytes);
    }
}
