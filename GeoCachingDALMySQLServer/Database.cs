﻿using System;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using Swk5.GeoCaching.DAL.Common;

namespace Swk5.GeoCaching.DAL.MySQLServer {
    public class Database : IDatabase {
        private readonly string connectionString;
        private readonly string localImageDirectory;

        public Database(string connectionString, string localImageDirectory) {
            this.connectionString = connectionString;
            this.localImageDirectory = localImageDirectory;
        }

        public IDbCommand CreateCommand(string sql) {
            return new MySqlCommand(sql);
        }

        public void DefineParameter(IDbCommand cmd, string name, DbType type, object value) {
            if (cmd.Parameters.Contains(name)) {
                throw new ArgumentException(String.Format("Parameter {0} already declared.", name));
            }

            // constructor with type as parameter MUST NOT be used, because converting DbType to MySqlDbType
            // does not function for double/decimal --> will be taken as int           
            cmd.Parameters.Add(new MySqlParameter {DbType = type, ParameterName = name, Value = value});
        }

        public IDataReader ExecuteReader(IDbCommand cmd) {
            DbConnection conn = null;

            try {
                conn = GetOpenConnection();
                cmd.Connection = conn;

                // let connection manager decide which behavior is required
                CommandBehavior cmdBehavior = ConnectionUtils.ShouldCloseConnection()
                    ? CommandBehavior.CloseConnection
                    : CommandBehavior.Default;

                return cmd.ExecuteReader(cmdBehavior);
            }
            catch {
                // make sure connection will be released/closed
                ConnectionUtils.ReleaseConnection(conn);

                // rethrow current exception
                throw;
            }
        }

        public int ExecuteNonQuery(IDbCommand cmd) {
            DbConnection conn = null;

            try {
                conn = GetOpenConnection();
                cmd.Connection = conn;
                return cmd.ExecuteNonQuery();
            }
            finally {
                ConnectionUtils.ReleaseConnection(conn);
            }
        }

        public T ExecuteScalarQuery<T>(IDbCommand cmd) {
            DbConnection conn = null;

            try {
                conn = GetOpenConnection();
                cmd.Connection = conn;
                return ( T ) cmd.ExecuteScalar();
            }
            finally {
                ConnectionUtils.ReleaseConnection(conn);
            }
        }

        public double ExecuteScalarDoubleQuery(IDbCommand cmd) {
            DbConnection conn = null;

            try {
                conn = GetOpenConnection();
                cmd.Connection = conn;
                return Double.Parse(cmd.ExecuteScalar().ToString());
            }
            finally {
                ConnectionUtils.ReleaseConnection(conn);
            }
        }

        public string LocalImageDirectory {
            get { return localImageDirectory; }
        }

        protected DbConnection GetOpenConnection() {
            // uses connection utils to manage connections
            return ConnectionUtils.GetOpenConnection(connectionString);
        }
    }
}