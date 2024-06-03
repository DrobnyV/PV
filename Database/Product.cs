using System.Data;
using System.Data.SqlClient;

namespace Database;

public class Product
{
    
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    
    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
    public void vlozitDoDatabase(SqlConnection connection)
    {
        
        string query = "insert into Product(name,price) values (@name, @price)";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.Add(new SqlParameter("@name", this.Name));
        command.Parameters.Add(new SqlParameter("@price", this.Price));
        command.ExecuteNonQuery();
       
       
        
    }
    public static void UpdateVDatabase(SqlConnection connection, int productId, string name, decimal price)
    {
      
        string query = $"UPDATE Product SET name = @name, price = @price WHERE id = {productId}";

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.Add(new SqlParameter("@name", name));
        command.Parameters.Add(new SqlParameter("@price", price));
        command.ExecuteNonQuery();
    }

    public static void DeleteVDatabase(SqlConnection connection, int productId)
    {
        
        string query = $"DELETE FROM Product WHERE ID = {productId}";

        SqlCommand command = new SqlCommand(query, connection);
        command.ExecuteNonQuery();
    }

    public static void WriteOutDatabaseProduct(SqlConnection connection)
    {
        string query = $"SELECT * FROM Product";

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
                if (fields.Length == 2)
                {
                    // Parse the fields and create a new Product instance
                    Product product = new Product(fields[0], decimal.Parse(fields[1]));
                    product.vlozitDoDatabase(connection);
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