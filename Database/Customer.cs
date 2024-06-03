namespace Database;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public int Adress_Id { get; set; }
    
    public Customer(int id, string name,int adress_Id)
    {
        Id = id;
        Name = name;
        Adress_Id = adress_Id;
    }
    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Adress_ID {Adress_Id}";
    }
}