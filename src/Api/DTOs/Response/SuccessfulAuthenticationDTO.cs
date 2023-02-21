namespace Api.DTOs.Response;

public class SuccessfulAuthenticationDTO
{
    public string Token { get; init; }

	public SuccessfulAuthenticationDTO(string token)
	{
		Token = token;
	}
}
