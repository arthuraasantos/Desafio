using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Tools.Utils
{
    public static class Identificador
    {
        public static string GerarIdentificadorAutomatico(string tipo, string data, decimal valor, string fitId)
        {
            string resultado = Criptografar(tipo.Substring(0, 3) + data + valor + fitId);
            
            return resultado;
        }

        public static bool CompararIdentificador(string tipo, string data, decimal valor, string fitId, string Identificador)
        {
            bool retorno = false;
            string novo = Criptografar(tipo.Substring(0, 3) + data + valor + fitId);
            string old = Identificador;
            if (novo == old)
            {
                retorno = true;
            }

            return retorno;

        }

        public static string Criptografar(string valor)
        {
            StringBuilder x = new StringBuilder();
            MD5 md = new MD5CryptoServiceProvider();
            md.ComputeHash(ASCIIEncoding.ASCII.GetBytes(valor));
            byte[] hash = md.Hash;

            for (int i = 0; i < hash.Length; i++)
            {
                x.Append(hash[i].ToString());
            }

            return x.ToString();
        }
    }
}
