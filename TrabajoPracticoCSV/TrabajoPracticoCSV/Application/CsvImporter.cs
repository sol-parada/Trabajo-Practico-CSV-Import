using CsvHelper;
using System.Globalization;
using TrabajoPracticoCSV.Domain;
using TrabajoPracticoCSV.Infrastructure;

namespace TrabajoPracticoCSV.Application
{
    public class CsvImporter
    {
        private readonly AlumnoRepository _repository;

        public CsvImporter(AlumnoRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> ImportAsync(string csvPath)
        {
            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var alumnos = new List<Alumno>();
            int total = 0;
            const int batchSize = 8000;

            await foreach (var alumno in csv.GetRecordsAsync<Alumno>())
            {
                alumnos.Add(alumno);
                if (alumnos.Count == batchSize)
                {
                    await _repository.BulkInsertAsync(alumnos);
                    total += alumnos.Count;
                    alumnos.Clear();
                }
            }

            if (alumnos.Count > 0)
            {
                await _repository.BulkInsertAsync(alumnos);
                total += alumnos.Count;
            }

            return total;
        }
    }
}