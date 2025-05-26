using Dapper;
using Microsoft.Data.SqlClient;
using PruebaPracticaApi.Model;
using System.Data;

namespace PruebaPracticaApi.Data
{
    public class EmpleadoRepository
    {
        private readonly string _connectionString;    
        public EmpleadoRepository(IConfiguration config) 
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(_connectionString))
                throw new ArgumentException("Connection string 'DefaultConnection' no encontrada en configuración.");
        }

        public async Task<IEnumerable<Empleado>> ObtenerEmpleados()
        {
            using var db = new SqlConnection(_connectionString);
            return await db.QueryAsync<Empleado>("sp_ObtenerEmpleados", commandType: CommandType.StoredProcedure);
        }

        public async Task<Empleado> ObtenerEmpleadoPorId(int id)
        {
            using var db = new SqlConnection(_connectionString);
            return await db.QueryFirstOrDefaultAsync<Empleado>("sp_ObtenerEmpleadoPorId", new { Id = id }, commandType: CommandType.StoredProcedure);
        }


        public async Task AgregarEmpleado(Empleado empleado)
        {
            using var db = new SqlConnection(_connectionString);

            var parametros = new
            {
                Nombre = empleado.Nombre,
                Cargo = empleado.Cargo,
                Email = empleado.Email
            };

            await db.ExecuteAsync("sp_AgregarEmpleados", parametros, commandType: CommandType.StoredProcedure);
        }


        public async Task ActualizarEmpleado(Empleado empleado)
        {
            using var db = new SqlConnection(_connectionString);
            await db.ExecuteAsync("sp_ActualizarEmpleados", empleado, commandType: CommandType.StoredProcedure);
        }
        public async Task<Empleado> EliminarEmpleado(int id)
        {
            using var db = new SqlConnection(_connectionString);          
            return await db.QueryFirstOrDefaultAsync<Empleado>("sp_EliminarEmpleado", new { Id = id }, commandType: CommandType.StoredProcedure);
        }
    }
}
