using MySql.Data.MySqlClient;

namespace DBConnector
{
    internal class Account : Base
    {
        internal Account() : base() { }

        internal int Register(string username, string email, string pass, string type, string address, string cpnumber, int familysize)
        {
            string query = "INSERT INTO UserTbl (Username, UserEmail, UserPass, UserType, UserAddress, UserCPNumber, UserFamilySize) VALUES (@Username, @Email, @Pass, @Type, @Address, @CPNumber, @FamilySize); SELECT LAST_INSERT_ID();";

            return ExecuteScalar<int>(query,
                new MySqlParameter("@Username", username),
                new MySqlParameter("@Email", email),
                new MySqlParameter("@Pass", pass),
                new MySqlParameter("@Type", type),
                new MySqlParameter("@Address", address),
                new MySqlParameter("@CPNumber", cpnumber),
                new MySqlParameter("@FamilySize", familysize)
            );
        }


        internal int Login(string Username, string Password)
        {
            string query = "SELECT UserID FROM UserTbl WHERE Username = @Username AND UserPass = @Password";
            return ExecuteScalar<int>(query,
                new MySqlParameter("@Username", Username),
                new MySqlParameter("@Password", Password)
            );
        }

        internal bool UpdateAddress(string address, int userid)
        {
            string query = "UPDATE UserTbl SET UserAddress = @Address where UserID = @UserID";
            return ExecuteNonQuery(query,
                new MySqlParameter("@Address", address),
                new MySqlParameter("@UserID", userid)
            );
        }

        internal bool UpdateCPNumber(string cpnumber, int userid)
        {
            string query = "UPDATE UserTbl SET UserCPNumber = @CPNumber where UserID = @UserID";
            return ExecuteNonQuery(query,
                new MySqlParameter("@CPNumber", cpnumber),
                new MySqlParameter("@UserID", userid)
            );
        }
    }
}
