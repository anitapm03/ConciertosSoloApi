using System.Security.Cryptography;
using System.Text;

namespace ConciertosSoloApi.Helpers
{
    public class HelperCryptography
    {
        public static string GenerateSalt()
        {
            Random random = new Random();
            string salt = "";
            for (int i = 1; i <= 50; i++)
            {
                int aleat = random.Next(1, 255);
                char letra = Convert.ToChar(aleat);
                salt += letra;
            }
            return salt;
        }

        public static bool CompareArrays(byte[] a, byte[] b)
        {
            bool iguales = true;
            if (a.Length != b.Length)
            {
                iguales = false;
            }
            else
            {
                for (int i = 0; i < a.Length; i++)
                {
                    //PREGUNTAMOS SI EL CONTENIDO DE CADA BYTE ES DISTINTO
                    if (a[i].Equals(b[i]) == false)
                    {
                        iguales = false;
                        break;
                    }
                }
            }
            return iguales;
        }

        //TENDREMOS UN METODO PARA CIFRAR EL PASSWORD
        //VAMOS A RECIBIR EL PASSWORD (STRING) Y EL SALT (STRING)
        //Y DEVOLVEREMOS EL ARRAY DE BYTES[] DEL RESULTADO CIFRADO
        /*public static byte[] EncryptPassword(string password, string salt)
        {
            string contenido = password + salt;
            SHA512 sha = SHA512.Create();
            //CONVERTIMOS contenido A BYTES[]
            byte[] salida = Encoding.UTF8.GetBytes(contenido);
            //CREAMOS LAS ITERACIONES
            for (int i = 1; i <= 114; i++)
            {
                salida = sha.ComputeHash(salida);
            }
            sha.Clear();
            return salida;
        }*/
        public static string EncryptPassword(string password, string salt)
        {
            string contenido = password + salt;
            SHA512 sha = SHA512.Create();

            // Convertir contenido a bytes[]
            byte[] salida = Encoding.UTF8.GetBytes(contenido);

            // Aplicar las iteraciones
            for (int i = 1; i <= 114; i++)
            {
                salida = sha.ComputeHash(salida);
            }

            // Convertir el hash resultante en un string legible
            StringBuilder builder = new StringBuilder();
            foreach (byte b in salida)
            {
                builder.Append(b.ToString("x2")); // Convierte byte a hexadecimal
            }

            sha.Clear();
            return builder.ToString(); // Devuelve el hash como string
        }
    }
}
