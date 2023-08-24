using Microsoft.Data.SqlClient;

internal class Program {
    private static void Main(string[] args) {
        var connStr = "server=localhost\\sqlexpress;" +
            "database=SalesDb;" +
            "trusted_connection=true;" + //this only works for local server hosting, anything else needs user/pass
            "trustServerCertificate=true;";
        var conn = new SqlConnection(connStr);
        conn.Open();
        if (conn.State != System.Data.ConnectionState.Open) {//makes sure that your connected,
                                                             //your code could be completely valid
                                                             //but if there is a problem server-side you wont connect.
            throw new Exception("Connection didn't open");
        }
        Console.WriteLine("Success");
        var sql = "SELECT * FROM Customers WHERE id BETWEEN 10 AND 19;";
        var cmd = new SqlCommand(sql, conn);
        var reader = cmd.ExecuteReader();
        while(reader.Read()) {//reader.read returns true if reader has more data to process
            var id = (int)reader["Id"];
            var name = (string)reader["Name"];
            Console.WriteLine($"{id} {name}");
        }
        conn.Close();// open connections are resource intensive, remember to close when done.
    }
}