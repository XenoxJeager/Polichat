namespace Polichat_Backend.LIB;

public class JwtService
{
    public string TryLogin(string username, string password)
    {
        if (!CheckCredentials(username, password))
            return null;
        return "";
    }

    private bool CheckCredentials(string username, string password)
    {
        return true;
    }
}
