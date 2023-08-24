using Microsoft.Data.SqlClient;
using SqlConnectionExample;

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
        var sql = "SELECT * FROM Customers ORDER BY Name;";
        var cmd = new SqlCommand(sql, conn);
        var reader = cmd.ExecuteReader();
        var customers = new List<Customer>();
        while(reader.Read()) {//reader.read returns true if reader has more data to process
            var cust = new Customer();
            cust.ID = (int)reader["Id"];//reader returns data as generic objects, a cast works, but using Convert.To would work better
            cust.Name = (string)reader["Name"];
            cust.Sales = reader["sales"].Equals(DBNull.Value) ? null : Convert.ToDecimal(reader["Sales"]);//pulling a nullable value
            cust.City = (string)reader["City"];
            cust.State = (string)reader["State"];
            cust.Active = (bool)reader["Active"];
            customers.Add(cust);
        }
        reader.Close();//only one reader can be open at a time, remember to close
        conn.Close();//open connections are resource intensive, remember to close
    }
}