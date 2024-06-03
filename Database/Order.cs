using System.Data;
using System.Data.SqlClient;

namespace Database;

public class Order
{
    public int Id { get; set; }
    public string OrderDate { get; set; }
    public int CustomerId { get; set; }
    public int ProduktId { get; set; }
    public int OrderDetailId { get; set; }
    
    public Order(string orderDate, int customerId, int produktId, int orderDetailId)
    {
        OrderDate = orderDate;
        CustomerId = customerId;
        ProduktId = produktId;
        OrderDetailId = orderDetailId;
    }
    public override string ToString()
    {
        return $"ID: {Id}, OrderDate: {OrderDate}, CustomerId: {CustomerId}, ProduktId: {ProduktId}, OrderDetailId: {OrderDetailId}";
    }
    
    public void VlozitDoDatabase(SqlConnection connection)
    {
      
        string query = "INSERT INTO [Order](OrderDate, CustomerId, ProductID, OrderDetail_Id) VALUES (@orderdate,@customerid,@productid,@orderdetailid)";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.Add(new SqlParameter("@orderdate", this.OrderDate));
        command.Parameters.Add(new SqlParameter("@customerid", this.CustomerId));
        command.Parameters.Add(new SqlParameter("@productid", this.ProduktId));
        command.Parameters.Add(new SqlParameter("@orderdetailid", this.OrderDetailId));
        command.ExecuteNonQuery();
    }
    public static void UpdateVDatabase(SqlConnection connection, int orderId, string date, int customerID, int produktID, int orderDetailID)
    {
        

        string query = $"UPDATE [Order] SET OrderDate = @orderdate, CustomerId = @customerid, ProductID = @productid, OrderDetail_Id = @orderdetailid " +
                       $"WHERE id = {orderId}";

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.Add(new SqlParameter("@orderdate", date));
        command.Parameters.Add(new SqlParameter("@customerid", customerID));
        command.Parameters.Add(new SqlParameter("@productid", produktID));
        command.Parameters.Add(new SqlParameter("@orderdetailid", orderDetailID));
        command.ExecuteNonQuery();
    }
    public static void DeleteVDatabase(SqlConnection connection, int orderId)
    {
        
        string query = $"DELETE FROM [Order] WHERE ID = {orderId}";

        SqlCommand command = new SqlCommand(query, connection);
        command.ExecuteNonQuery();
    }
    public static void WriteOutDatabaseOrder(SqlConnection connection)
    {
        string query = $"SELECT * FROM [Order]";

        SqlCommand command = new SqlCommand(query, connection);
        try
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                
                int fieldCount = reader.FieldCount;
                string[] columnNames = new string[fieldCount];
                for (int i = 0; i < fieldCount; i++)
                {
                    columnNames[i] = reader.GetName(i);
                }

                
                Console.WriteLine(string.Join(", ", columnNames));

                
                while (reader.Read())
                {
                    object[] rowValues = new object[fieldCount];
                    reader.GetValues(rowValues);

                    
                    Console.WriteLine(string.Join(", ", rowValues.Select(v => v.ToString())));
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Error reading table: {ex.Message}");
        }
    }
    public static void WriteOutDatabaseCustomer(SqlConnection connection)
    {
        string query = $"SELECT * FROM Customer";

        SqlCommand command = new SqlCommand(query, connection);
        try
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                
                int fieldCount = reader.FieldCount;
                string[] columnNames = new string[fieldCount];
                for (int i = 0; i < fieldCount; i++)
                {
                    columnNames[i] = reader.GetName(i);
                }

                
                Console.WriteLine(string.Join(", ", columnNames));

                
                while (reader.Read())
                {
                    object[] rowValues = new object[fieldCount];
                    reader.GetValues(rowValues);

                    
                    Console.WriteLine(string.Join(", ", rowValues.Select(v => v.ToString())));
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Error reading table: {ex.Message}");
        }
    }
    public static void WriteOutDatabaseOrderDetail(SqlConnection connection)
    {
        string query = $"SELECT * FROM OrderDetail";

        SqlCommand command = new SqlCommand(query, connection);
        try
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                
                int fieldCount = reader.FieldCount;
                string[] columnNames = new string[fieldCount];
                for (int i = 0; i < fieldCount; i++)
                {
                    columnNames[i] = reader.GetName(i);
                }

                
                Console.WriteLine(string.Join(", ", columnNames));

                
                while (reader.Read())
                {
                    object[] rowValues = new object[fieldCount];
                    reader.GetValues(rowValues);

                    
                    Console.WriteLine(string.Join(", ", rowValues.Select(v => v.ToString())));
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Error reading table: {ex.Message}");
        }
    }
    public static void ImportFromCsv(string filePath, SqlConnection connection)
    {
        try
        {
            
            string[] lines = File.ReadAllLines(filePath);

            int startRowIndex = 0;
            
            
            for (int i = startRowIndex; i < lines.Length; i++)
            {
                
                string[] fields = lines[i].Split(',');
                
                if (fields.Length == 4)
                {
                    
                    Order order = new Order(fields[0],int.Parse(fields[1]),int.Parse(fields[2]),int.Parse(fields[3]));

                    // Insert the order into the database
                    order.VlozitDoDatabase(connection);
                }
                else
                {
                    Console.WriteLine($"Skipping invalid line at index {i + 1}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while importing data: {ex.Message}");
        }
    }
    
}