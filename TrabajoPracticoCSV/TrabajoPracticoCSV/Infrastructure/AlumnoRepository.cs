using Npgsql;
using TrabajoPracticoCSV.Domain;
using System.Text;

namespace TrabajoPracticoCSV.Infrastructure
{
    public class AlumnoRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public AlumnoRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public virtual async Task BulkInsertAsync(IEnumerable<Alumno> alumnos, int batchSize = 8000)
        {
            var alumnosList = alumnos.ToList();
            using var connection = _connectionFactory.CreateConnection() as NpgsqlConnection;
            await connection.OpenAsync();
            using var transaction = await connection.BeginTransactionAsync();

            for (int i = 0; i < alumnosList.Count; i += batchSize)
            {
                var batch = alumnosList.Skip(i).Take(batchSize).ToList();
                var sb = new StringBuilder();
                sb.Append("INSERT INTO alumnos (apellido, nombre, nro_documento, tipo_documento, fecha_nacimiento, sexo, nro_legajo, fecha_ingreso) VALUES ");

                var parameters = new List<NpgsqlParameter>();
                for (int j = 0; j < batch.Count; j++)
                {
                    if (j > 0) sb.Append(",");
                    sb.Append($"(@a{j},@b{j},@c{j},@d{j},@e{j},@f{j},@g{j},@h{j})");
                    parameters.Add(new NpgsqlParameter($"a{j}", batch[j].Apellido ?? (object)DBNull.Value));
                    parameters.Add(new NpgsqlParameter($"b{j}", batch[j].Nombre ?? (object)DBNull.Value));
                    parameters.Add(new NpgsqlParameter($"c{j}", batch[j].NroDocumento ?? (object)DBNull.Value));
                    parameters.Add(new NpgsqlParameter($"d{j}", batch[j].TipoDocumento ?? (object)DBNull.Value));
                    parameters.Add(new NpgsqlParameter($"e{j}", batch[j].FechaNacimiento));
                    parameters.Add(new NpgsqlParameter($"f{j}", batch[j].Sexo ?? (object)DBNull.Value));
                    parameters.Add(new NpgsqlParameter($"g{j}", batch[j].NroLegajo));
                    parameters.Add(new NpgsqlParameter($"h{j}", batch[j].FechaIngreso));
                }
                sb.Append(";");

                using var cmd = new NpgsqlCommand(sb.ToString(), connection, transaction);
                cmd.Parameters.AddRange(parameters.ToArray());
                await cmd.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();
        }
    }
}