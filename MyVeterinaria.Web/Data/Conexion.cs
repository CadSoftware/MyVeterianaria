using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVeterinaria.Web.Data
{
    public static class CadenaConexion
    {
        private const string Servidor = "10.1.254.126";
        private const string BaseDatos = "VeterinariaDb";
        private const string Usuario = "sa";
        private const string Password = "Desarroll0";

        public static string ObtenerCadenaConexion => $"Data Source={Servidor}; " +
                                                      $"Initial Catalog={BaseDatos}; " +
                                                      $"User Id={Usuario}; " +
                                                      $"Password={Password};";
    }
}
