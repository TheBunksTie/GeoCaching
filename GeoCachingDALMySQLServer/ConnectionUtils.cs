using System.Collections.Generic;
using System.Data.Common;
using System.Transactions;
using MySql.Data.MySqlClient;

namespace Swk5.GeoCaching.DAL.MySQLServer {

  /// <summary>
  /// Utils for obtaining a DbConnection. Takes care of a current transaction 
  /// when using <see cref="TransactionScope"/>.
  /// Simplified implementation for demonstration purposes only.
  /// Remark: Implementation for SqlServer.
  /// </summary>
  public static class ConnectionUtils {

    // holds the connections per transaction
    // each transaction can have more than one connection (with different connection 
    // strings) but a transaction can only have one connection per unique connection 
    // string <Transaction, IDictionary<ConnectionString, DbConnection>>
    // Multiple threads may invoke methods of this class in parallel. Therefore,
    // access to this static field must be synchronized.
    private static IDictionary<Transaction, IDictionary<string, DbConnection>> 
      transactedConnections = new Dictionary<Transaction, 
                                             IDictionary<string, DbConnection>>();

    /// <summary>
    /// Returns an opened DbConnection. If a transaction is present, for
    /// each unique connection string the same connection is returned.
    /// </summary>
    /// <param name="connectionString">connection string of the connection</param>
    /// <returns>A <see cref="DbConnection"/> instance</returns>
    public static DbConnection GetOpenConnection(string connectionString) {
      DbConnection connection = null;
      Transaction currentTransaction = Transaction.Current;

      if (currentTransaction == null)
        // no transaction context -> return "ordinary" connection
        connection = CreateOpenConnection(connectionString);
      else {
        // transaction present
        IDictionary<string, DbConnection> connections = null;
        lock (transactedConnections) {
          // check if current transaction already has at least one connection assigned
          if (!transactedConnections.TryGetValue(currentTransaction, out connections)) {
            connections = new Dictionary<string, DbConnection>();
            transactedConnections[currentTransaction] = connections;
          }

          if (!connections.TryGetValue(connectionString, out connection)) {
            // no connection for this connection string in the current transaction
            connection = CreateOpenConnection(connectionString);

            connections[connectionString] = connection;
            currentTransaction.TransactionCompleted +=
              new TransactionCompletedEventHandler(OnTransactionCompleted);
          }
        }
      }

      return connection;
    }

    /// <summary>
    /// Creates a new, opened DbConnection
    /// </summary>
    /// <param name="connectionString">connection string of the connection</param>
    /// <returns>A <see cref="DbConnection"/> instance </returns>
    private static DbConnection CreateOpenConnection(string connectionString) {
      DbConnection connection = new MySqlConnection(connectionString);
      connection.Open();
      return connection;
    }

    /// <summary>
    /// Releases a connection. 
    /// (Remark: The connection is closed only when not used in a transaction)
    /// </summary>
    /// <param name="connection">connection to release</param>
    public static void ReleaseConnection(DbConnection connection) {
      if (connection == null)
        return;

      Transaction currentTransaction = Transaction.Current;
      // close connection if no transaction is active
      if (currentTransaction == null)
        connection.Close();
    }

    /// <summary>
    /// Determines, if custom connection handling code should close a connection.
    /// (Remark: Used for DataReader.CommandBehavior)
    /// </summary>
    /// <returns></returns>
    public static bool ShouldCloseConnection() {
      return Transaction.Current == null;
    }

    /// <summary>
    /// Eventhandler for TransactionScope.TransactionCompleted.
    /// Closes connections and removes transaction from observed connections.
    /// </summary>
    /// <param name="sender">sender</param>
    /// <param name="e">TransactionEventArgs</param>
    private static void OnTransactionCompleted(object sender, TransactionEventArgs e) {
      lock (transactedConnections) {
        IDictionary<string, DbConnection> connections = null;
        if (transactedConnections.TryGetValue(e.Transaction, out connections)) {
          foreach (DbConnection conn in connections.Values)
            conn.Close();
          transactedConnections.Remove(e.Transaction);
        }
      }
    }
  }
}