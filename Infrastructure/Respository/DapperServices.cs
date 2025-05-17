using Dapper;
using LabManagement.Infrastructure.IRespository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace LabManagement.Resposities.Respository
{
    public class DapperServices : IDapperServices
    {
        private readonly IConfiguration _configuration;
        private string connectString = "AppConn";        
        public DapperServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string? DateFormat => _configuration.GetConnectionString("DateFormat");

        public string? DATAAREAID()
        {
            return _configuration.GetConnectionString("DATAAREAID");
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int ExcuteScaler<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            IDbConnection db = new SqlConnection(_configuration.GetConnectionString(connectString));
            return db.Execute(sp, parms, commandType: commandType);
        }

        public object ExcuteScalerObject<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            IDbConnection db = new SqlConnection(_configuration.GetConnectionString(connectString));
            return db.ExecuteScalar(sp, parms, commandType: commandType);
        }

        public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            IDbConnection db = new SqlConnection(_configuration.GetConnectionString(connectString));
            return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
        }

        public List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            IDbConnection db = new SqlConnection(_configuration.GetConnectionString(connectString));
            return db.Query<T>(sp, parms, commandType: commandType).ToList();
        }
    }
}
