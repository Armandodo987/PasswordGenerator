using PasswordGenerator.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace PasswordGenerator.Services
{
    public class PasswordService : IPasswordService
    {
        public string GenerateOTP(int userId, long timeStamp)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            int binaryCode = BitConverter.ToInt32(encoder.GetBytes(Hash($"{userId}_{timeStamp + 30}")), 0);
            int otp = binaryCode % (int)Math.Pow(10, 6);
            return otp.ToString().PadLeft(6, '0');
        }

        private string Hash(string input)
        {
            using var sha1 = SHA1.Create();
            return Convert.ToHexString(sha1.ComputeHash(Encoding.UTF8.GetBytes(input)));
        }
    }
}
