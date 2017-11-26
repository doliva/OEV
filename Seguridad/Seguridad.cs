using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Base.BLL;

namespace Base
{
    public class Seguridad : ISeguridad
    {
        IUsuarioBLL usuarioBLL = new UsuarioBLL();
        
        /// <summary>
        /// Transforma el parámetro cadena en un código para generar un dígito verificador.
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns>String</returns>
        public string generarSHA(string cadena)
        {
            UnicodeEncoding codigo = new UnicodeEncoding();
            SHA256Managed SHA = new SHA256Managed();
            byte[] hash = SHA.ComputeHash(codigo.GetBytes(cadena));
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Verifica la consistencia de la tabla Usuarios
        /// </summary>
        /// <returns>Boolean</returns>
        public bool verificarConsistencia()
        {
            List<Usuario> listaUsuarios = usuarioBLL.obtenerUsuarios();
            String filasSHA = "";
            String dvhFilas = "";

            foreach (Usuario usr in listaUsuarios)
            {
                //Por fila
                String cadena = usr.estado.ToString() + usr.email + usr.clave;
                String cadenaSHA = generarSHA(cadena);
                filasSHA += cadenaSHA;
                dvhFilas += usr.digitoVerificador;
                //Verificacion por fila
                if (!cadenaSHA.Equals(usr.digitoVerificador))
                {
                    return false;
                }
            }
            if (!filasSHA.Equals(dvhFilas))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Verifica la consistencia de cada uno de los registros de la tabla Usuario,
        /// retornando una lista de los usuarios con errores.
        /// </summary>
        /// <returns>List<Usuario></returns>
        public List<Usuario> verificarHorizontal()
        {
            List<Usuario> listaUsuarios = usuarioBLL.obtenerUsuarios();
            List<Usuario> listaErrores = new List<Usuario>();
            foreach (Usuario usr in listaUsuarios)
            {
                {
                    String cadena = usr.estado.ToString() + usr.email + usr.clave;
                    String cadenaSHA = generarSHA(cadena);
                    if (!cadenaSHA.Equals(usr.digitoVerificador))
                    {
                        listaErrores.Add(usr);
                    }
                }
            }
            return listaErrores;
        }

        /// <summary>
        /// Encriptar cadena.
        /// </summary>
        /// <param name="cadenaAencriptar"></param>
        /// <returns>String</returns>
        public string Encriptar(string cadenaAencriptar)
        {
            String resultado = String.Empty;
            byte[] encriptado = System.Text.Encoding.Unicode.GetBytes(cadenaAencriptar);
            resultado = Convert.ToBase64String(encriptado);
            return resultado;
        }

        /// <summary>
        /// Desencriptar cadena.
        /// </summary>
        /// <param name="cadenaAdesencriptar"></param>
        /// <returns>String</returns>
        public string DesEncriptar(string cadenaAdesencriptar)
        {
            String resultado = String.Empty;
            if (cadenaAdesencriptar.Length == 64)
            {
                byte[] desencriptado = Convert.FromBase64String(cadenaAdesencriptar);
                resultado = System.Text.Encoding.Unicode.GetString(desencriptado);

                return resultado;
            }
            else
            {
                return "Cadena no valida!";
            }
        }
    }
}
