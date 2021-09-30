using System.Security.Cryptography;
using System.Text;

namespace DigitalOceanCertTool.Helpers {
    public static class RandomHelper {
        const string SecureRandomStringCharset = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        public static string GetSecureRandomString(int length) {
            StringBuilder s = new StringBuilder();
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider()) {
                while (s.Length != length) {
                    byte[] oneByte = new byte[1];
                    provider.GetBytes(oneByte);
                    char character = (char)oneByte[0];
                    if (SecureRandomStringCharset.Contains(character)) {
                        s.Append(character);
                    }
                }
            }
            return s.ToString();
        }
    }
}