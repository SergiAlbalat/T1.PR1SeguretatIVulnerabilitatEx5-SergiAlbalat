using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
namespace T1.PR1SeguretatIVulnerabilitatEx5_SergiAlbalat
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public User(string username, string password)
        {
            Username = username;
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(bytes);
                StringBuilder hashString = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashString.Append(b.ToString("x2"));
                }
                Password = hashString.ToString();
            }
        }
        public bool VerifyUser(string username,  string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(bytes);
                StringBuilder hashString = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashString.Append(b.ToString("x2"));
                }
                password = hashString.ToString();
            }
            return Username == username && Password == password;
        }
        public override string ToString()
        {
            return $"Username: {Username}, Password: {Password}";
        }
    }
}
