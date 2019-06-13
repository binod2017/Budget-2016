using DbConnectionStrings;
using SqlQueries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget_2016.Access
{
    public class EarningAccess : ConnectionAccess,IEarningAccess
    {       
        public bool AddEarning(EarningModel earning)
        {
            using (OleDbCommand oleDbCommand = new OleDbCommand())
            {
                // Set the command object properties
                oleDbCommand.Connection = new OleDbConnection(this.IncomeConnectionString);
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = Budget_2016.Scripts.Scripts.sqlInsertEarning;

                // Add the input parameters to the parameter collection
                oleDbCommand.Parameters.AddWithValue("@basic", earning.Basic);
                oleDbCommand.Parameters.AddWithValue("@hra", earning.Hra);
                oleDbCommand.Parameters.AddWithValue("@conveyance", earning.Conveyance);
                oleDbCommand.Parameters.AddWithValue("@medical", earning.Medical);
                oleDbCommand.Parameters.AddWithValue("@telephone", earning.Telephone);
                oleDbCommand.Parameters.AddWithValue("@fuel", earning.Fuel);
                oleDbCommand.Parameters.AddWithValue("@driver", earning.Driver);
                oleDbCommand.Parameters.AddWithValue("@child", earning.Child);
                oleDbCommand.Parameters.AddWithValue("@food", earning.Food);
                oleDbCommand.Parameters.AddWithValue("@special", earning.Special);
                oleDbCommand.Parameters.AddWithValue("@city", earning.City);
                oleDbCommand.Parameters.AddWithValue("@bonus", earning.Bonus);
                oleDbCommand.Parameters.AddWithValue("@totalincome", earning.TotalIncome);
                oleDbCommand.Parameters.AddWithValue("@month", earning.Months);
                oleDbCommand.Parameters.AddWithValue("@year", earning.Years);


                // Open the connection, execute the query and close the connection
                oleDbCommand.Connection.Open();
                var rowsAffected = oleDbCommand.ExecuteNonQuery();
                oleDbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }

        public bool UpdateEarning(EarningModel earning)
        {
            using (OleDbCommand oleDbCommand = new OleDbCommand())
            {
                // Set the command object properties
                oleDbCommand.Connection = new OleDbConnection(this.IncomeConnectionString);
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = Budget_2016.Scripts.Scripts.sqlUpdateEarning;

                // Add the input parameters to the parameter collection
                oleDbCommand.Parameters.AddWithValue("@basic", earning.Basic);
                oleDbCommand.Parameters.AddWithValue("@hra", earning.Hra);
                oleDbCommand.Parameters.AddWithValue("@conveyance", earning.Conveyance);
                oleDbCommand.Parameters.AddWithValue("@medical", earning.Medical);
                oleDbCommand.Parameters.AddWithValue("@telephone", earning.Telephone);
                oleDbCommand.Parameters.AddWithValue("@fuel", earning.Fuel);
                oleDbCommand.Parameters.AddWithValue("@driver", earning.Driver);
                oleDbCommand.Parameters.AddWithValue("@child", earning.Child);
                oleDbCommand.Parameters.AddWithValue("@food", earning.Food);
                oleDbCommand.Parameters.AddWithValue("@special", earning.Special);
                oleDbCommand.Parameters.AddWithValue("@city", earning.City);
                oleDbCommand.Parameters.AddWithValue("@bonus", earning.Bonus);
                oleDbCommand.Parameters.AddWithValue("@totalincome", earning.TotalIncome);
                oleDbCommand.Parameters.AddWithValue("@month", earning.Months);
                oleDbCommand.Parameters.AddWithValue("@year", earning.Years);
                //oleDbCommand.Parameters.AddWithValue("@id", earning.Id);

                // Open the connection, execute the query and close the connection
                oleDbCommand.Connection.Open();
                var rowsAffected = oleDbCommand.ExecuteNonQuery();
                oleDbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }
                
        public bool DeleteEarning(int id)
        {
            using (OleDbCommand dbCommand = new OleDbCommand())
            {
                // Set the command object properties
                dbCommand.Connection = new OleDbConnection(this.IncomeConnectionString);
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = Budget_2016.Scripts.Scripts.sqlDeleteEarning;

                // Add the input parameter to the parameter collection
                dbCommand.Parameters.AddWithValue("@Id", id);

                // Open the connection, execute the query and close the connection
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }


        public int CheckDuplicates(string month, int year)
        {
            using (OleDbCommand dbCommand = new OleDbCommand())
            {
                // Set the command object properties
                dbCommand.Connection = new OleDbConnection(this.IncomeConnectionString);
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = Budget_2016.Scripts.Scripts.sqlDuplicateEarning;

                // Add the input parameter to the parameter collection
                dbCommand.Parameters.AddWithValue("@month", month);
                dbCommand.Parameters.AddWithValue("@year", year);


                // Open the connection, execute the query and close the connection
                dbCommand.Connection.Open();                
                var rowsAffected = dbCommand.ExecuteScalar();
                dbCommand.Connection.Close();

                return (int)rowsAffected;// < 0;
            }
        }


        public int GetMonthlyEarning(string month, int year)
        {
            using (OleDbCommand dbCommand = new OleDbCommand())
            {
                // Set the command object properties
                dbCommand.Connection = new OleDbConnection(this.IncomeConnectionString);
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = Budget_2016.Scripts.Scripts.sqlGetMonthlyEarning;

                // Add the input parameter to the parameter collection
                dbCommand.Parameters.AddWithValue("@month", month);
                dbCommand.Parameters.AddWithValue("@year", year);


                // Open the connection, execute the query and close the connection
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteScalar();
                dbCommand.Connection.Close();
                if (rowsAffected == null)
                {
                    return 0;// < 0;
                }
                else
                {
                    return (int)rowsAffected;// < 0;
                }
            }
        }
    }
}
