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

        /*public static bool CompareArrays(byte[] a, byte[] b)
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
        }*/
        public static bool CompareArrays(string hexStringA, string hexStringB)
        {
            // Convertir las cadenas hexadecimales a arrays de bytes
            byte[] a = HexStringToByteArray(hexStringA);
            byte[] b = HexStringToByteArray(hexStringB);

            // Verificar si los arrays de bytes son de la misma longitud
            if (a.Length != b.Length)
            {
                return false;
            }

            // Comparar los arrays de bytes elemento por elemento
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false; // Si se encuentra un byte diferente, retornar false
                }
            }

            return true; // Si los arrays son idénticos, retornar true
        }

        // Método auxiliar para convertir una cadena hexadecimal en un array de bytes
        private static byte[] HexStringToByteArray(string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException("La cadena hexadecimal debe tener una longitud par.", nameof(hexString));
            }

            byte[] byteArray = new byte[hexString.Length / 2];

            for (int i = 0; i < byteArray.Length; i++)
            {
                byteArray[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return byteArray;
        }


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
