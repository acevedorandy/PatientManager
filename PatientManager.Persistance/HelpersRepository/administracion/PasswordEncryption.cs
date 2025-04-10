using System.Security.Cryptography;
using System.Text;

namespace PatientManager.Persistance.HelpersRepository.administracion
{
    public class PasswordEncryption
    {
        public static string ComputeSha256Hash(string contraseña)
        {
            // Creacion de la encriptacion

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(contraseña));


                // Convertidor de bytes a string

                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    stringBuilder.Append(bytes[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }

        }
    }
}
