using System;
using System.Security.Cryptography;
using System.Text;
namespace MansionArroz.Net.Utility
{
    public static class Seguridad
    {
        public static string Encriptar(string Contrasena)
        {
            byte[] byt = System.Text.Encoding.UTF8.GetBytes(Contrasena);
            return Convert.ToBase64String(byt);
        }
        public static string Desencriptar(string Contrasena)
        {
            byte[] b = Convert.FromBase64String(Contrasena);
            return System.Text.Encoding.UTF8.GetString(b);
        }

        public static string GenerarContraseña()
        {
            var BaseCaracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-#!$%^&*()_+|~=`{}:";
            var Contrasena = new char[16];
            var random = new Random();
            for (int i = 0; i < Contrasena.Length; i++)
            {
                Contrasena[i] = BaseCaracteres[random.Next(BaseCaracteres.Length)];
            }
            var ContrasenaFinal = new String(Contrasena);
            return ContrasenaFinal;
        }
    }
}
