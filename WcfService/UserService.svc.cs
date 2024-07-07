using BLToolkit.Data;
using BLToolkit.Data.Linq;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService
{
    public class UserService : IUserService
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT \"id\", \"firstname\", \"lastname\", \"patronymic\", \"identificationnumber\", \"email\", \"contactphone\", \"creationdate\", \"lastmodifieddate\" FROM \"users\"";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Patronymic = reader.GetString(3),
                                IdentificationNumber = reader.GetString(4),
                                Email = reader.GetString(5),
                                ContactPhone = reader.GetString(6),
                                CreationDate = reader.GetDateTime(7),
                                LastModifiedDate = reader.GetDateTime(8)
                            };
                            users.Add(user);
                        }
                    }
                }
            }
            return users;
        }
        public void UpdateUser(User updatedUser)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string sql = "UPDATE \"users\" SET \"firstname\" = @FirstName, \"lastname\" = @LastName, \"patronymic\" = @Patronymic, " +
                             "\"identificationnumber\" = @IdentificationNumber, \"email\" = @Email, \"contactphone\" = @ContactPhone, " +
                             "\"lastmodifieddate\" = @LastModifiedDate WHERE \"id\" = @Id";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", updatedUser.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", updatedUser.LastName);
                    cmd.Parameters.AddWithValue("@Patronymic", updatedUser.Patronymic);
                    cmd.Parameters.AddWithValue("@IdentificationNumber", updatedUser.IdentificationNumber);
                    cmd.Parameters.AddWithValue("@Email", updatedUser.Email);
                    cmd.Parameters.AddWithValue("@ContactPhone", updatedUser.ContactPhone);
                    cmd.Parameters.AddWithValue("@LastModifiedDate", updatedUser.LastModifiedDate);
                    cmd.Parameters.AddWithValue("@Id", updatedUser.Id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception($"User with ID {updatedUser.Id} not found.");
                    }
                }
            }
        }

        public void AddUser(User newUser)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO \"users\"(\"firstname\", \"lastname\", \"patronymic\", \"identificationnumber\", \"email\", \"contactphone\", \"creationdate\", \"lastmodifieddate\") " +
                             "VALUES (@FirstName, @LastName, @Patronymic, @IdentificationNumber, @Email, @ContactPhone, @CreationDate, @LastModifiedDate)";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", newUser.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", newUser.LastName);
                    cmd.Parameters.AddWithValue("@Patronymic", newUser.Patronymic);
                    cmd.Parameters.AddWithValue("@IdentificationNumber", newUser.IdentificationNumber);
                    cmd.Parameters.AddWithValue("@Email", newUser.Email);
                    cmd.Parameters.AddWithValue("@ContactPhone", newUser.ContactPhone);
                    cmd.Parameters.AddWithValue("@CreationDate", newUser.CreationDate);
                    cmd.Parameters.AddWithValue("@LastModifiedDate", newUser.LastModifiedDate);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("Failed to add new user.");
                    }
                }
            }
        }
    }
}
