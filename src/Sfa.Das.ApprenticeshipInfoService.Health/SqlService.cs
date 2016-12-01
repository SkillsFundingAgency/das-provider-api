using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using Sfa.Das.ApprenticeshipInfoService.Health.Models;

namespace Sfa.Das.ApprenticeshipInfoService.Health
{
    public class SqlService : ISqlService
    {
        public Status TestConnection(string connectionString)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return connection.State == ConnectionState.Open ? Status.Green : Status.Red;
                }
            }
            catch
            {
                return Status.Red;
            }
        }
    }
}