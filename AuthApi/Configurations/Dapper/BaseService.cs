using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;
using static Dapper.SqlMapper;


namespace AuthApi.Configurations.Dapper
{
    public abstract class BaseService
    {
        private readonly IConfiguration configuration;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes the new instance of <see cref="BaseService"/>.
        /// </summary>
        /// <param name="configuration">The instance of <see cref="IConfiguration"/>.</param>
        /// <param name="logger">The instance of <see cref="ILogger"/>.</param>
        protected BaseService(IConfiguration configuration, ILogger logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        protected IDbConnection Connection => new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

        /// <summary>
        /// Executes the query to get the results.
        /// </summary>
        /// <typeparam name="T">The type of result data.</typeparam>
        /// <param name="sQuery">The sql query.</param>
        /// <param name="parameters">The paramters.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A sequence of data of T; if a basic type (int, string, etc) is queried then the data from the first column in assumed,
        /// otherwise an instance is created per row,
        /// and a direct column-name===member-name mapping is assumed (case insensitive).</returns>
        protected async Task<List<T>> GetQueryResultAsync<T>(string sQuery, object parameters = null, IDbTransaction transaction = null)
        {
            logger.LogDebug($"QUERY GetQueryResultAsync COMMAND | {sQuery}");
            logger.LogDebug($"QUERY GetQueryResultAsync PARAMETERS | {parameters}");

            var command = new CommandDefinition(sQuery, parameters, transaction);

            var result = await Connection.QueryAsync<T>(command);

            logger.LogDebug($"QUERY GetQueryResultAsync EXECUTED");

            return result.ToList();

        }

        /// <summary>
        /// Executes the query to get the results.
        /// </summary>
        /// <typeparam name="T">The type of result data.</typeparam>
        /// <param name="sQuery">The sql query.</param>
        /// <param name="parameters">The paramters.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>A sequence of data of T; if a basic type (int, string, etc) is queried then the data from the first column in assumed,
        /// otherwise an instance is created per row,
        /// and a direct column-name===member-name mapping is assumed (case insensitive).</returns>
        protected async Task<T> GetQueryFirstOrDefaultResultAsync<T>(string sQuery, object parameters, IDbTransaction transaction = null)
        {
            logger.LogDebug($"QUERY GetQueryFirstOrDefaultResultAsync COMMAND | {sQuery}");
            logger.LogDebug($"QUERY GetQueryFirstOrDefaultResultAsync PARAMETERS | {parameters}");

            var command = new CommandDefinition(sQuery, parameters, transaction);

            var result = await Connection.QueryFirstOrDefaultAsync<T>(command);

            logger.LogDebug($"QUERY GetQueryFirstOrDefaultResultAsync EXECUTED");

            return result;
        }

        /// <summary>
        /// Executes the query
        /// </summary>
        /// <param name="sQuery">The sql query.</param>
        /// <param name="parameters">The query parameters</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>The affected number of rows.</returns>
        protected async Task<int> ExecuteAsync(string sQuery, object parameters, IDbTransaction transaction = null)
        {
            logger.LogDebug($"QUERY ExecuteAsync COMMAND | {sQuery}");
            logger.LogDebug($"QUERY ExecuteAsync PARAMETERS | {parameters}");

            var command = new CommandDefinition(sQuery, parameters, transaction);

            var rowsAffected = await Connection.ExecuteAsync(command);

            logger.LogDebug($"QUERY ExecuteAsync EXECUTED");

            return rowsAffected;
        }
        /// <summary>
        /// Reads from multiple query.
        /// </summary>
        /// <param name="sQuery">The sql query</param>
        /// <param name="parameters">THe parameters.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>The grid reader.</returns>
        protected async Task<GridReader> GetFromMultipleQuery(string sQuery, object parameters, IDbTransaction transaction = null)
        {
            logger.LogDebug($"QUERY InsertAndGetId COMMAND | {sQuery}");
            logger.LogDebug($"QUERY InsertAndGetId PARAMETERS | {parameters}");

            var command = new CommandDefinition(sQuery, parameters, transaction);

            var result = await Connection.QueryMultipleAsync(command);

            logger.LogDebug($"QUERY GetQueryResultCount EXECUTED");

            return result;
        }

        /// <summary>
        /// Execute Sql Query for datatable
        /// </summary>
        /// <param name="sQuery"></param>
        /// <returns></returns>
        protected async Task<DataTable> Execute(string sQuery)
        {
            var connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var dt = new DataTable();
                var cmd = new SqlCommand(sQuery, connection) { CommandType = CommandType.Text };
                cmd.CommandTimeout = 0;
                var dr = await cmd.ExecuteReaderAsync();
                dt.Load(dr);
                cmd.Dispose();
                connection.Close();
                return dt;
            }
        }
        /// <summary>
        /// for dataset
        /// </summary>
        /// <param name="sQuery"></param>
        /// <returns></returns>
        protected async Task<DataSet> ExecuteQuery(string sQuery)
        {
            var connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var cmd = new SqlCommand();
                var ds = new DataSet();
                cmd.CommandText = sQuery;
                cmd.Connection = connection;
                var da = new SqlDataAdapter(cmd.CommandText, cmd.Connection);
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                await Task.Run(() => da.Fill(ds));
                cmd.Dispose();
                da.Dispose();
                return ds;
            }
        }
    }
}
