﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class Constantes
    {
        public static string EXCEPCION_DAL_RESTORE = "Error de DAL al intentar obtener restore";
        public static string EXCEPCION_DAL_BACKUP = "Error de DAL al intentar crear backup";

        public static string NOMBRE_BD = "BD_OEV";

        public static string EXCEPCION_BLL_BACKUP = "Error de BLL al intentar crear backup";
        public static string EXCEPCION_BLL_RESTORE = "Error de BLL al intentar obtener restore";

        public static string EXCEPCION_BLL_INS = "Error de BLL al intentar insert: ";
        public static String EXCEPCION_BLL_UPD = "Error de BLL al intentar update: ";
        public static String EXCEPCION_BLL_DEL = "Error de BLL al intentar delete: ";
        public static string EXCEPCION_BLL_SEL = "Error de BLL al intentar selectBy: ";
        public static String EXCEPCION_BLL_ALL = "Error de BLL al intentar selectAll:";

        public static String EXCEPCION_DAL_INS = "Error de DAL intentar insert: ";
        public static String EXCEPCION_DAL_UPD = "Error de DAL intentar update: ";
        public static String EXCEPCION_DAL_DEL = "Error de DAL intentar delete: ";
        public static String EXCEPCION_DAL_SEL = "Error de DAL intentar selectBy: ";
        public static String EXCEPCION_DAL_ALL = "Error de DAL al intentar selectAll:";

        public static String ROL_ADMINISTRADOR = "ADMINISTRADOR";

        public static String BAJA_USUARIO = "Baja de usuario";

        public static String BACKUP_BD = "Backup de BD";
        public static String RESTORE_BD = "Restore de BD";

        public static String LOGIN = "Login de usuario";
        public static String LOGOUT = "Logout de usuario";



        public static string DVH = "Digito Verificador Horizontal";
        public static string DVV = "Digito Verificador Vertical";
    }
}
