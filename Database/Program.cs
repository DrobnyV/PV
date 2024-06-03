using System.Data.SqlClient;
using Database;
using Microsoft.Extensions.Configuration;


IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath("C:\\Users\\dimin\\RiderProjects\\Database\\Database")
    .AddJsonFile("appsettings.json")
    .Build();

SqlConnectionStringBuilder consStringBuilder = new SqlConnectionStringBuilder();
consStringBuilder.UserID = configuration["LogInf:UserID"]; //prihlasovaci jmeno
consStringBuilder.Password = configuration["LogInf:Password"]; //heslo
consStringBuilder.InitialCatalog = configuration["LogInf:InitialCatalog"]; //jmeno databaze
consStringBuilder.DataSource = configuration["LogInf:DataSource"]; //XXX je cislo vaseho PC ve skole
consStringBuilder.ConnectTimeout = Convert.ToInt32(configuration["LogInf:ConnectTimeout"]);


while (true)
        {
            
            Console.WriteLine("Vyberte akci:");
            Console.WriteLine("1. Přidat produkt");
            Console.WriteLine("2. Aktualizovat produkt");
            Console.WriteLine("3. Smazat produkt");
            Console.WriteLine("4. Přidat objednávku");
            Console.WriteLine("5. Aktualizovat objednávku");
            Console.WriteLine("6. Smazat objednávku");
            Console.WriteLine("7. Importovat data z CSV produkty");
            Console.WriteLine("8. Importovat data z CSV objednávky");
            Console.WriteLine("9. Ukončit program");

            
            
            try
            {
                using (SqlConnection connection = new SqlConnection(consStringBuilder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Pripojeno");
                    string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.WriteLine("Zadejte název produktu:");
                    string productName = Console.ReadLine();
                    Console.WriteLine("Zadejte cenu produktu:");
                    decimal productPrice = Convert.ToDecimal(Console.ReadLine());
                    Product product = new Product(productName, productPrice);
                    product.vlozitDoDatabase(connection);
                    break;
                case "2":
                    Product.WriteOutDatabaseProduct(connection);
                    Console.WriteLine("Zadejte ID produktu, který chcete aktualizovat:");
                    int productIdToUpdate = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Zadejte nový název produktu:");
                    string updatedProductName = Console.ReadLine();
                    Console.WriteLine("Zadejte novou cenu produktu:");
                    decimal updatedProductPrice = Convert.ToDecimal(Console.ReadLine());
                    Product.UpdateVDatabase(connection, productIdToUpdate, updatedProductName, updatedProductPrice);
                    Console.WriteLine("Produkt byl úspěšně aktualizován.");
                    break;
                case "3":
                    Console.WriteLine("Produkty:");
                    Product.WriteOutDatabaseProduct(connection);
                    Console.WriteLine("Zadejte ID produktu, který chcete smazat:");
                    int productIdToDelete = Convert.ToInt32(Console.ReadLine());
                    Product.DeleteVDatabase(connection,productIdToDelete);
                    Console.WriteLine("Produkt byl úspěšně smazán.");
                    break;
                case "4":
                    Console.WriteLine("Zadejte datum (YYYY.MM.DD):");
                    string orderDate = Console.ReadLine();
                    Console.WriteLine("Zakaznici:");
                    Order.WriteOutDatabaseCustomer(connection);
                    Console.WriteLine("Zadejte CustomerID:");
                    int CustomerID = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Produkty:");
                    Product.WriteOutDatabaseProduct(connection);
                    Console.WriteLine("Zadejte ProduktID:");
                    int ProduktID = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Order details:");
                    Order.WriteOutDatabaseOrderDetail(connection);
                    Console.WriteLine("Zadejte OrderDetalID:");
                    int OrderDetailID = Convert.ToInt32(Console.ReadLine());
                    Order order = new Order(orderDate,CustomerID,ProduktID,OrderDetailID);
                    order.VlozitDoDatabase(connection);
                    break;
                case "5":
                    Order.WriteOutDatabaseOrder(connection);
                    Console.WriteLine("Zadejte ID orderu, který chcete aktualizovat:");
                    int orderIdToUpdate = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Zadejte nový datum (YYYY.MM.DD):");
                    string updatedOrderDate = Console.ReadLine();
                    Console.WriteLine("Zakaznici:");
                    Order.WriteOutDatabaseCustomer(connection);
                    Console.WriteLine("Zadejte nové customer id:");
                    int updatedCustomerID = Convert.ToInt32((Console.ReadLine()));
                    Console.WriteLine("Produkty:");
                    Product.WriteOutDatabaseProduct(connection);
                    Console.WriteLine("Zadejte nové produkt id:");
                    int updatedProduktID = Convert.ToInt32((Console.ReadLine()));
                    Console.WriteLine("Order details:");
                    Order.WriteOutDatabaseOrderDetail(connection);
                    Console.WriteLine("Zadejte nové order detail id:");
                    int updatedOrderDetailID = Convert.ToInt32((Console.ReadLine()));
                    Order.UpdateVDatabase(connection, orderIdToUpdate, updatedOrderDate, updatedCustomerID,updatedProduktID,updatedOrderDetailID);
                    Console.WriteLine("Order byla úspěšně aktualizována.");
                    break;
                case "6":
                    Console.WriteLine("Objednávky:");
                    Order.WriteOutDatabaseOrder(connection);
                    Console.WriteLine("Zadejte ID objednávky, kterou chcete smazat:");
                    int orderIdToDelete = Convert.ToInt32(Console.ReadLine());
                    Order.DeleteVDatabase(connection,orderIdToDelete);
                    Console.WriteLine("Order byla úspěšně smazána.");
                    break;
                    
                case "7":
                    Console.WriteLine("Zadejte cestu k souboru CSV:");
                    string csvFilePath = Console.ReadLine();
                    Product.ImportFromCsv(csvFilePath, connection);
                    Console.WriteLine("Data byla úspěšně importována z CSV.");
                    break;
                case "8":
                    Console.WriteLine("Zadejte cestu k souboru CSV:");
                    string csvFilePath2 = Console.ReadLine();
                    Order.ImportFromCsv(csvFilePath2, connection);
                    Console.WriteLine("Data byla úspěšně importována z CSV.");
                    break;
                case "9":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Neplatná volba, zkuste to znovu.");
                    break;
            }
                }
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }
            
            
        }