using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class SingleSqlCommandConnection : IDbConnection
    {
        private readonly SqlConnection _connection;

        public SingleSqlCommandConnection(SqlConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");

            _connection = connection;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public IDbTransaction BeginTransaction()
        {
            return _connection.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return _connection.BeginTransaction(il);
        }

        public void Close()
        {
            _connection.Close();
        }

        public void ChangeDatabase(string databaseName)
        {
            _connection.ChangeDatabase(databaseName);
        }

        public IDbCommand CreateCommand()
        {
            return Command ?? (Command = _connection.CreateCommand());
        }

        protected SqlCommand Command { get; set; }

        public void Open()
        {
            _connection.Open();
        }

        public string ConnectionString { get { return _connection.ConnectionString; } set { _connection.ConnectionString = value; } }
        public int ConnectionTimeout { get { return _connection.ConnectionTimeout; } }
        public string Database { get { return _connection.Database; } }
        public ConnectionState State { get { return _connection.State; } }
    }
}