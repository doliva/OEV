using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Excepciones;

namespace Utils.BackupRestoreBD
{
    public class BackupRestoreBLL
    {
        /// <summary>
        /// Genera el formato de nombre de archivo para backup de base de datos y envía a realizar el backup en la ruta indicada.
        /// </summary>
        /// <param name="rutaDestino"></param>
        /// <returns>String</returns>
        public static String crearBackup(String rutaDestino)
        {
            String nombreCopia = null;
            try
            {
                nombreCopia = "F" + System.DateTime.Now.Day.ToString() +
                                 System.DateTime.Now.Month.ToString() +
                                 System.DateTime.Now.Year.ToString() + "_H" +
                                 System.DateTime.Now.Hour.ToString() + "." +
                                 System.DateTime.Now.Minute.ToString() + "." +
                                 System.DateTime.Now.Second.ToString() + "." +
                                 System.DateTime.Now.Millisecond.ToString() + "_" +
                                 Constantes.NOMBRE_BD +
                                 ".bak";
                BackupRestoreBDDAL.backupBD(rutaDestino + "\\" + nombreCopia);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_BACKUP, dalE.InnerException);
            }
            return nombreCopia;
        }

        /// <summary>
        /// Restaura la base de datos mediante la ruta del archivo indicada por parámetro.
        /// </summary>
        /// <param name="rutaOrigen"></param>
        public static void obtenerRestore(String rutaOrigen)
        {
            try
            {
                BackupRestoreBDDAL.restoreBD(rutaOrigen);
            }
            catch (Excepcion dalE)
            {
                throw new Excepcion(Constantes.EXCEPCION_BLL_RESTORE, dalE.InnerException);
            }
        }
    }
}
