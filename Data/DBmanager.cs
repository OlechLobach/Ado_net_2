using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Ado_net_2
{
    public static class DBManager
    {
        private static string connectionString = @"Server=DESKTOP-GNKQNCJ\SQLEXPRESS;Database=Warehouse;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public static void CreateTables()
        {
            using (SqlConnection connection = GetConnection())
            {
                string createProductsTableQuery = @"
                    CREATE TABLE Products (
                        Id INT PRIMARY KEY IDENTITY,
                        Name NVARCHAR(100) NOT NULL,
                        Type NVARCHAR(50) NOT NULL,
                        SupplierId INT,
                        Quantity INT NOT NULL,
                        FOREIGN KEY (SupplierId) REFERENCES Suppliers(Id)
                    )";

                string createSuppliersTableQuery = @"
                    CREATE TABLE Suppliers (
                        Id INT PRIMARY KEY IDENTITY,
                        Name NVARCHAR(100) NOT NULL
                    )";

                string createProductTypesTableQuery = @"
                    CREATE TABLE ProductTypes (
                        Id INT PRIMARY KEY IDENTITY,
                        TypeName NVARCHAR(50) NOT NULL
                    )";

                SqlCommand command = new SqlCommand(createProductsTableQuery, connection);
                command.ExecuteNonQuery();

                command = new SqlCommand(createSuppliersTableQuery, connection);
                command.ExecuteNonQuery();

                command = new SqlCommand(createProductTypesTableQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public static void InsertProduct(string name, string type, int supplierId, int quantity)
        {
            using (SqlConnection connection = GetConnection())
            {
                string insertProductQuery = @"
                    INSERT INTO Products (Name, Type, SupplierId, Quantity)
                    VALUES (@Name, @Type, @SupplierId, @Quantity)";

                SqlCommand command = new SqlCommand(insertProductQuery, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Type", type);
                command.Parameters.AddWithValue("@SupplierId", supplierId);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.ExecuteNonQuery();
            }
        }

        public static void UpdateProduct(int id, string name, string type, int supplierId, int quantity)
        {
            using (SqlConnection connection = GetConnection())
            {
                string updateProductQuery = @"
                    UPDATE Products
                    SET Name = @Name, Type = @Type, SupplierId = @SupplierId, Quantity = @Quantity
                    WHERE Id = @Id";

                SqlCommand command = new SqlCommand(updateProductQuery, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Type", type);
                command.Parameters.AddWithValue("@SupplierId", supplierId);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteProduct(int id)
        {
            using (SqlConnection connection = GetConnection())
            {
                string deleteProductQuery = "DELETE FROM Products WHERE Id = @Id";

                SqlCommand command = new SqlCommand(deleteProductQuery, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        public static string GetSupplierWithMostProducts()
        {
            using (SqlConnection connection = GetConnection())
            {
                string query = @"
                    SELECT TOP 1 Suppliers.Name, COUNT(Products.Id) AS ProductCount
                    FROM Suppliers
                    JOIN Products ON Suppliers.Id = Products.SupplierId
                    GROUP BY Suppliers.Name
                    ORDER BY ProductCount DESC";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return $"{reader["Name"]} ({reader["ProductCount"]} products)";
                }
                return "No supplier found.";
            }
        }

        public static DataTable GetAllProducts()
        {
            DataTable table = new DataTable();
            using (SqlConnection connection = GetConnection())
            {
                string query = "SELECT * FROM Products";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(table);
            }
            return table;
        }
    }
}