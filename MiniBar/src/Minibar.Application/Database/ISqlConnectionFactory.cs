using System.Data;

namespace Minibar.Application.Database
{
    public interface ISqlConnectionFactory
    {
        IDbConnection Create();
    }
}