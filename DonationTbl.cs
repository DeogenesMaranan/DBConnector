using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DBConnector
{
    internal class DonationTbl : Base
    {
        internal DonationTbl() : base() { }

        internal bool Add(int userid, int itemid, string status = "Pending")
        {
            string query = "INSERT INTO DonationTbl (DonorUserID, DonatedItemID, DonationDate, DonationStatus) VALUES (@UserID, @ItemID, NOW(), @Status)";
            return ExecuteNonQuery(query,
                new MySqlParameter("@UserID", userid),
                new MySqlParameter("@ItemID", itemid),
                new MySqlParameter("@Status", status)
            );
        }

        internal bool UpdateStatus(int donationid, string status)
        {
            string query = "UPDATE DonationTbl SET DonationStatus = @Status WHERE DonationID = @DonationID";
            return ExecuteNonQuery(query,
                new MySqlParameter("@Status", status),
                new MySqlParameter("@DonationID", donationid)
            );
        }

        internal List<Dictionary<string, object>> GetAllDonation()
        {
            string query = "SELECT * FROM RequestTbl";

            Func<MySqlDataReader, Dictionary<string, object>> mapFunction = (reader) =>
            {
                var donation = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string columnName = reader.GetName(i);
                    object columnValue = reader.GetValue(i);
                    donation.Add(columnName, columnValue);
                }

                return donation;
            };

            return ExecuteQuery(query, mapFunction);
        }

    }
}
