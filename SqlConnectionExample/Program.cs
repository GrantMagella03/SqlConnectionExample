using Microsoft.Data.SqlClient;

internal class Program {
    private static void Main(string[] args) {
        var connStr = "server=localhost\\sqlexpress;" +
            "database=SalesDb;" +
            "trusted_connection=true;" +
            "trustServerCertificate=true;";
        var conn = new SqlConnection(connStr);
        conn.Open();
        if (conn.State != System.Data.ConnectionState.Open) {
            throw new Exception("Connection didn't open");
        }
        Console.WriteLine("Success");
        var sql = "SELECT * FROM Customers WHERE id BETWEEN 10 AND 19;";
        var cmd = new SqlCommand(sql, conn);
        var reader = cmd.ExecuteReader();
        //reader.Read();//returns true if reader has more data to process
        while(reader.Read()) {
            var id = (int)reader["Id"];
            var name = (string)reader["Name"];
            Console.WriteLine($"{id} {name}");
        }
        conn.Close();
    }
}