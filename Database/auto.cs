using System.Data.SqlClient;

public class Auto
{
    private int id;
    private string znacka;
    private string typ;
    private int maxRychlost;

    // Constructor
    public Auto(int id, string znacka, string typ, int maxRychlost)
    {
        this.id = id;
        this.znacka = znacka;
        this.typ = typ;
        this.maxRychlost = maxRychlost;
    }

    // Properties
    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Znacka
    {
        get { return znacka; }
        set { znacka = value; }
    }

    public string Typ
    {
        get { return typ; }
        set { typ = value; }
    }

    public int MaxRychlost
    {
        get { return maxRychlost; }
        set { maxRychlost = value; }
    }

    // ToString method
    public override string ToString()
    {
        return $"Auto [Id={Id}, Znacka={Znacka}, Typ={Typ}, MaxRychlost={MaxRychlost}]";
    }

    public void vlozitDoDatabase(SqlConnection connection)
    {
        
        string query = "insert into auto(znacka,typ,maxRychlost) values (@znacka,@typ,@maxRychlost)";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.Add(new SqlParameter("@znacka", this.znacka));
        command.Parameters.Add(new SqlParameter("@typ", this.typ));
        command.Parameters.Add(new SqlParameter("@maxRychlost", this.maxRychlost));
        command.ExecuteNonQuery();
       
       
        
    }
    }
