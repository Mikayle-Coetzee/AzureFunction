using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AzureFunction1
{
    public class ConnectToDatabaseClass
    {
        /// <summary>
        /// SQL DB Connection String
        /// </summary>
        private readonly string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Visual Studio Apps\\AzureFunction1\\StudentDB.mdf\";Integrated Security=True";

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method will insert the student data into the database 
        /// </summary>
        /// <param name="StudentNumber"></param>
        /// <param name="Name"></param>
        /// <param name="Age"></param>
        /// <returns></returns>
        public bool SaveCustomer(string StudentNumber, string Name, int Age)
        {
            //SqlCommand command = new SqlCommand(ConnectionString);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                // Insert data into the database 
                string insertQuery = "INSERT INTO Student (StudentNumber, Name, Age) " +
                                     "VALUES (@StudentNumber, @Name, @Age)";

                using (SqlCommand command = new SqlCommand(insertQuery, conn))
                {
                    //command.Parameters.AddWithValue("@StudentNumber", StudentNumber);
                    //command.Parameters.AddWithValue("@Name", Name);
                    //command.Parameters.AddWithValue("@Age", Age);

                    command.ExecuteNonQuery();
                }

                conn.Close();
            }

            return true;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
