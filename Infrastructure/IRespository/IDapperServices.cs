using Dapper;
using System.Data;

namespace LabManagement.Infrastructure.IRespository
{
    public interface IDapperServices : IDisposable
    {
        string? DATAAREAID();
        T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        object ExcuteScalerObject<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        int ExcuteScaler<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
    }

}
