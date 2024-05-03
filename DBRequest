using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DBConnector
{
    internal class RequestTbl : Base
    {
        internal RequestTbl() : base() { }

        internal bool Add(int userid, int itemid, int quantity, string status = "Pending")
        {
            string query = "INSERT INTO RequestTbl (RequesterUserID, RequestedItemID, RequestDate, RequestStatus) VALUES (@UserID, @ItemID, @Quantity, NOW(), @Status)";
            return ExecuteNonQuery(query,
                new MySqlParameter("@UserID", userid),
                new MySqlParameter("@ItemID", itemid),
                new MySqlParameter("@Quantity", quantity),
                new MySqlParameter("@Status", status)
            );
        }

        internal List<Dictionary<string, object>> GetAllRequest()
        {
            string query = "SELECT * FROM RequestTbl";

            Func<MySqlDataReader, Dictionary<string, object>> mapFunction = (reader) =>
            {
                var request = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string columnName = reader.GetName(i);
                    object columnValue = reader.GetValue(i);
                    request.Add(columnName, columnValue);
                }

                return request;
            };

            return ExecuteQuery(query, mapFunction);
        }

    }
}
