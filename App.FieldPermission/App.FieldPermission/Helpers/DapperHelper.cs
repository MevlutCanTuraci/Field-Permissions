using System.Data;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Microsoft.Data.SqlClient;

namespace App.FieldPermission.Helpers;

public class DapperHelper
{
    private IConfiguration _configuration;

    public DapperHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<T> ActionAsync<T>(Func<IDbConnection, Task<T>> Action)
    {
        using (var db = new SqlConnection(
                   _configuration.GetConnectionString("SqlServer")
               ))
        {
            return await Action.Invoke(db);
        }
    }
    
    public async Task<T> ActionLinqAsync<T, E>(
        Func<IDbConnection, DapperRepository<E>, Task<T>> Action
    ) where E : class
    {
        using (var db = new SqlConnection(_configuration.GetConnectionString("Default")))
        using (DapperRepository<E> dre = new DapperRepository<E>(db, new SqlGenerator<E>()))
        {
            return await Action.Invoke(db, dre);
        }
    }
}