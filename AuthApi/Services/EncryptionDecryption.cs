using System.Text;

namespace AuthApi.Services
{
    public class EncryptionDecryption
    {
        public static string DecodeString(string encoded)
        {
            return Encoding.ASCII.GetString(Convert.FromBase64String(encoded));
        }

        public static string EncodeString(string decoded)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(decoded));
        }
    }
}
