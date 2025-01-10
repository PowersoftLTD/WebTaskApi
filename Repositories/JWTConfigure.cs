using Dapper;
using System.Data;
using System.Drawing;
using TaskManagement.API.DapperDbConnections;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;

namespace TaskManagement.API.Repositories
{
    public class JWTConfigure : IJWTConfigure
    {
        public IDapperDbConnection _dapperDbConnection;
        public JWTConfigure(IDapperDbConnection dapperDbConnection)
        {
            _dapperDbConnection = dapperDbConnection;
        }
        async Task<IEnumerable<ConfigDetail>> IJWTConfigure.JWTToken()
        {
            using (IDbConnection db = _dapperDbConnection.CreateConnection())
            {
                try
                {
                    // Log the SQL query execution
                    var tokens = await db.QueryAsync<ConfigDetail>("select App,[Key],Issuer,Audience,ClientID,ClientSecret " +
                        "from Config_Details_Hdr_V");

                    // Log or inspect the result
                    if (tokens == null || !tokens.Any())
                    {
                        // Log that no tokens were found
                        Console.WriteLine("No tokens found in the database.");
                    }
                    return tokens;
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return Enumerable.Empty<ConfigDetail>(); // Return an empty list in case of error
                }
            }
        }
    }
}
