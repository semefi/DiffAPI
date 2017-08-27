using System;

namespace DiffAPI.Repository.SQLServer
{
    public abstract class DbRepository
    {
        protected readonly string connectionString;

        protected DbRepository(string connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException("connectionString");
            this.connectionString = connectionString;
        }
    }
}