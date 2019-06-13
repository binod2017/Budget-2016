using DbConnectionStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Budget_2016.Services
{
    public class DeductionAccess : ConnectionAccess, IDeductionAccess
    {

        public bool AddDeduction(DeductionModel deduction)
        {
            using (OleDbCommand oleDbCommand = new OleDbCommand())
            {
                // Set the command object properties
                oleDbCommand.Connection = new OleDbConnection(this.IncomeConnectionString);
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = Budget_2016.Scripts.Scripts.sqlInsertDeduction;

                // Add the input parameters to the parameter collection
                oleDbCommand.Parameters.AddWithValue("@leave", deduction.Leave);
                oleDbCommand.Parameters.AddWithValue("@pf", deduction.PF    );
                oleDbCommand.Parameters.AddWithValue("@proftax", deduction.ProfTax);
                oleDbCommand.Parameters.AddWithValue("@esi", deduction.ESI);
                oleDbCommand.Parameters.AddWithValue("@tds", deduction.TDS);
                oleDbCommand.Parameters.AddWithValue("@others", deduction.Others);
                oleDbCommand.Parameters.AddWithValue("@totaldeduction", deduction.TotalDeduction);
                oleDbCommand.Parameters.AddWithValue("@month", deduction.Months);
                oleDbCommand.Parameters.AddWithValue("@year", deduction.Years);


                // Open the connection, execute the query and close the connection
                oleDbCommand.Connection.Open();
                var rowsAffected = oleDbCommand.ExecuteNonQuery();
                oleDbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }

        public bool UpdateDeduction(DeductionModel deduction)
        {
            using (OleDbCommand oleDbCommand = new OleDbCommand())
            {
                // Set the command object properties
                oleDbCommand.Connection = new OleDbConnection(this.IncomeConnectionString);
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = Budget_2016.Scripts.Scripts.sqlUpdateDeduction;

                // Add the input parameters to the parameter collection
                oleDbCommand.Parameters.AddWithValue("@leave", deduction.Leave);
                oleDbCommand.Parameters.AddWithValue("@pf", deduction.PF);
                oleDbCommand.Parameters.AddWithValue("@proftax", deduction.ProfTax);
                oleDbCommand.Parameters.AddWithValue("@esi", deduction.ESI);
                oleDbCommand.Parameters.AddWithValue("@tds", deduction.TDS);
                oleDbCommand.Parameters.AddWithValue("@others", deduction.Others);
                oleDbCommand.Parameters.AddWithValue("@totaldeduction", deduction.TotalDeduction);
                oleDbCommand.Parameters.AddWithValue("@month", deduction.Months);
                oleDbCommand.Parameters.AddWithValue("@year", deduction.Years);
                //oleDbCommand.Parameters.AddWithValue("@id", deduction.Id);

                // Open the connection, execute the query and close the connection
                oleDbCommand.Connection.Open();
                var rowsAffected = oleDbCommand.ExecuteNonQuery();
                oleDbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }

        public bool DeleteDeduction(int id)
        {
            using (OleDbCommand dbCommand = new OleDbCommand())
            {
                // Set the command object properties
                dbCommand.Connection = new OleDbConnection(this.IncomeConnectionString);
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = Budget_2016.Scripts.Scripts.sqlDeleteDeduction;

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
                dbCommand.CommandText = Budget_2016.Scripts.Scripts.sqlDuplicateDeduction;

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


        public int GetMonthlyDeduction(string month, int year)
        {
            using (OleDbCommand dbCommand = new OleDbCommand())
            {
                // Set the command object properties
                dbCommand.Connection = new OleDbConnection(this.IncomeConnectionString);
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = Budget_2016.Scripts.Scripts.sqlGetMonthlyDeduction;

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
