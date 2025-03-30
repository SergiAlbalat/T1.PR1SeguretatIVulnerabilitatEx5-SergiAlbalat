using System.Security.Cryptography;
using System.Text;
using T1.PR1SeguretatIVulnerabilitatEx5_SergiAlbalat;
namespace T1Pr1
{
    public class Program
    {
        public static void Main()
        {
            const string Menu = "1- A\n2- B\n3- C\n4- Exit";
            const string NotOptionMsg = "This option doesn't exist";
            const string FormatExceptionMsg = "The format is incorrect";
            bool exit = false;
            int option;
            User? user = null;
            try
            {
                while (!exit)
                {
                    Console.WriteLine(Menu);
                    option = Convert.ToInt32(Console.ReadLine());
                    switch (option)
                    {
                        case 1:
                            user = CreateUser();
                            Console.WriteLine(user);
                            break;
                        case 2:
                            if (user != null)
                            {
                                Console.WriteLine(LogIn(user) ? "Logged" : "Not Logged");
                            }
                            else
                            {
                                Console.WriteLine("No user created");
                            }
                            break;
                        case 3:
                            RSAEncriptation();
                            break;
                        case 4:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine(NotOptionMsg);
                            break;
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine(FormatExceptionMsg);
            }
        }
        private static User CreateUser()
        {
            Console.WriteLine("Username:");
            string username = Console.ReadLine();
            Console.WriteLine("Password:");
            string password = Console.ReadLine();
            return new User(username, password);
        }
        private static bool LogIn(User user)
        {
            Console.WriteLine("Username:");
            string username = Console.ReadLine();
            Console.WriteLine("Password:");
            string password = Console.ReadLine();
            return user.VerifyUser(username, password);
        }
        private static void RSAEncriptation()
        {
            Console.WriteLine("Write a text");
            string text = Console.ReadLine();
            using (RSA rsa = RSA.Create())
            {
                string publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
                string privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
                byte[] dataToEncrypt = Encoding.UTF8.GetBytes(text);
                byte[] encryptedData = rsa.Encrypt(dataToEncrypt, RSAEncryptionPadding.Pkcs1);
                Console.WriteLine("Texto cifrado: " + Convert.ToBase64String(encryptedData));
                byte[] decryptedData = rsa.Decrypt(encryptedData, RSAEncryptionPadding.Pkcs1);
                Console.WriteLine("Texto descifrado: " + Encoding.UTF8.GetString(decryptedData));
            }
        }
    }
}