using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DBConnector
{
    internal class DBInventory : Base
    {
        internal DBInventory() : base() { }

        internal int Add(string name, int quantity, DateTime expiration, string category, int ishalal) // will be used either donation or request
        {
            string query = "INSERT INTO InventoryTbl (ItemName, AvailableQuantity, ItemExpiration, ItemCategory, IsItemHalal) VALUES (@Name, @Quantity, @Expiration, @Category, @IsHalal); SELECT LAST_INSERT_ID();";
            return ExecuteScalar<int>(query,
                new MySqlParameter("@Name", name),
                new MySqlParameter("@Quantity", quantity),
                new MySqlParameter("@Expiration", expiration),
                new MySqlParameter("@Category", category),
                new MySqlParameter("@IsHalal", ishalal)
            );
        }

        internal bool UpdateQuantity(int deduction, int itemid)
        {
            string query = "UPDATE InventoryTbl SET AvailableQuantity = AvailableQuantity - @Deduction WHERE ItemID = @ItemID";
            return ExecuteNonQuery(query,
                new MySqlParameter("@Deduction", deduction),
                new MySqlParameter("@ItemID", itemid)
            );
        }

        internal List<Dictionary<string, object>> GetAvailableItems()
        {
            string query = "SELECT * FROM InventoryTbl WHERE ItemExpiration > NOW() AND AvailableQuantity > 0";

            Func<MySqlDataReader, Dictionary<string, object>> mapFunction = (reader) =>
            {
                var itemDetails = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string columnName = reader.GetName(i);
                    object columnValue = reader.GetValue(i);
                    itemDetails.Add(columnName, columnValue);
                }

                return itemDetails;
            };

            return ExecuteQuery(query, mapFunction);
        }

    }
}
