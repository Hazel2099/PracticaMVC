using PruebaPracticaApi.Model;
using Microsoft.Data.SqlClient;
using Dapper;


namespace PruebaPracticaApi.Data
{
    public class UsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public async Task<UsuarioLogin?> ValidarUsuario(string usuario, string password)
        {
            using var connection = new SqlConnection(_connectionString);

            var result = await connection.QueryFirstOrDefaultAsync<UsuarioLogin>(
                "ValidarUsuario",
                new { Usuario = usuario, Password = password },
                commandType: System.Data.CommandType.StoredProcedure
            );

            return result;
        }
    }
}

