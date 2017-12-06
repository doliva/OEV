using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utils
{
    public static class Validacion
    {
        /// <summary>
        /// Verifica formato de email válido
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Boolean</returns>
        public static Boolean esEmailValido(String email)
        {
            if (String.IsNullOrEmpty(email))
                return true;
            String expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Calcula el dígito verificador dado un CUIT completo o sin él.
        /// </summary>
        /// <param name="cuit">El CUIT como String sin guiones</param>
        /// <returns>El valor del dígito verificador calculado.</returns>
        private static int CalcularDigitoCuit(string cuit)
        {
            int[] mult = new[] { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            char[] nums = cuit.ToCharArray();
            int total = 0;
            for (int i = 0; i < mult.Length; i++)
            {
                total += int.Parse(nums[i].ToString()) * mult[i];
            }
            var resto = total % 11;
            return resto == 0 ? 0 : resto == 1 ? 9 : 11 - resto;
        }



        /// <summary>
        /// Valida el CUIT ingresado.
        /// </summary>
        /// <param name="cuit" />Número de CUIT como string con o sin guiones
        /// <returns>True si el CUIT es válido y False si no.</returns>
        public static bool ValidaCuit(string cuit)
        {
            if (cuit == null)
            {
                return false;
            }
            //Quito los guiones, el cuit resultante debe tener 11 caracteres.
            cuit = cuit.Replace("-", string.Empty);
            if (cuit.Length != 11)
            {
                return false;
            }
            else
            {
                int calculado = CalcularDigitoCuit(cuit);
                int digito = int.Parse(cuit.Substring(10));
                return calculado == digito;
            }
        }

        /// <summary>
        /// Verifica que sea un formato numérico.
        /// </summary>
        /// <param name="valor"></param>
        /// <returns>Boolean</returns>
        public static Boolean esNumeroValido(String valor)
        {
            try
            {
                Convert.ToInt32(valor);
            }
            catch (FormatException fe)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Verifica que la edad sea de 18 años en adelante.
        /// </summary>
        /// <param name="fecNac"></param>
        /// <returns>Boolean</returns>
        public static Boolean esFecNacValido(DateTime fecNac)
        {
            int edad = DateTime.Now.Year - fecNac.Year;
            if (edad > 17)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Verifica que el nombre de archivo seleccionado para restaurar la base de datos tenga un formato válido
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns>Boolean</returns>
        public static Boolean esBackupValido(String nombre)
        {

            if (nombre.Contains("F") && nombre.Contains("_H") && nombre.Contains(Constantes.NOMBRE_BD))
                return true;
            return false;
        }

    }
}
