using I18N.West;
using MySql.Data.MySqlClient;

namespace LoaderServer.MySql;

public class MySqlManager
{
    private readonly string _host;
    private readonly string _database;
    private readonly string _username;
    private readonly string _password;
    private readonly string _port;

    public MySqlManager(string host, string database, string username, string password, string port)
    {
        _host = host;
        _database = database;
        _username = username;
        _password = password;
        _port = port;

        new CP1250();
    }

    public async Task<object> ExecuteQuery(string query, bool isScalar, Dictionary<string, object> parameters = null)
    {
        using (MySqlConnection mySqlConnection = CreateConnection())
        {
            await mySqlConnection.OpenAsync();

            using (MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection))
            {
                if (parameters != null)
                {
                    foreach (var parameter in parameters) 
                    {
                        mySqlCommand.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }
                }

                if (isScalar)
                    return await mySqlCommand.ExecuteScalarAsync();
                    
                return await mySqlCommand.ExecuteNonQueryAsync();
            }
        }
    }

    /*public async Task<List<Product>> GetProducts(string username, string password)
    {
        var products = new List<Product>();

        try
        {
            using (MySqlConnection mySqlConnection = CreateConnection())
            {
                await mySqlConnection.OpenAsync();

                using (MySqlCommand mySqlCommand = new MySqlCommand($"SELECT * FROM `products` WHERE ownerusername='{username}' AND ownerpassword='{password}'", mySqlConnection))
                {
                    var reader = await mySqlCommand.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                            products.Add(new Product(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                    }
                }
            }

            return products;
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return products;
        }
    }*/
        
    private MySqlConnection CreateConnection() => new
    (
        $"SERVER={_host};" +
        $"DATABASE={_database};" +
        $"UID={_username};" +
        $"PASSWORD={_password};" +
        $"PORT={_port};"
    );
}