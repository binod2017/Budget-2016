using DbConnectionStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Budget_2016
{
    public class FixedexpenditureAccess : ConnectionAccess, IFixedExpenditureAccess
    {
        public bool AddFixedexpenditure(Model.FixedExpenditureModel expenditure)
        {
            using (OleDbCommand oleDbCommand = new OleDbCommand())
            {
                // Set the command object properties
                oleDbCommand.Connection = new OleDbConnection(this.IncomeConnectionString);
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = Budget_2016.Scripts.Scripts.sqlInsertFixedExpenditure;

                // Add the input parameters to the parameter collection
                oleDbCommand.Parameters.AddWithValue("@rent", expenditure.Rent);
                oleDbCommand.Parameters.AddWithValue("@electricity", expenditure.Electricity);
                oleDbCommand.Parameters.AddWithValue("@water", expenditure.Water);
                oleDbCommand.Parameters.AddWithValue("@gas", expenditure.Gas);
                oleDbCommand.Parameters.AddWithValue("@telephone", expenditure.Telephone);
                oleDbCommand.Parameters.AddWithValue("@mobile", expenditure.Mobile);
                oleDbCommand.Parameters.AddWithValue("@cable", expenditure.Cable);
                oleDbCommand.Parameters.AddWithValue("@internet", expenditure.Internet);
                oleDbCommand.Parameters.AddWithValue("@buspass", expenditure.Buspass);
                oleDbCommand.Parameters.AddWithValue("@petrol", expenditure.Petrol);
                oleDbCommand.Parameters.AddWithValue("@emi", expenditure.EMI);
                oleDbCommand.Parameters.AddWithValue("@others", expenditure.Others);
                oleDbCommand.Parameters.AddWithValue("@total", expenditure.TotalFixedExpenditure);
                oleDbCommand.Parameters.AddWithValue("@month", expenditure.Months);
                oleDbCommand.Parameters.AddWithValue("@year", expenditure.Years);


                // Open the connection, execute the query and close the connection
                oleDbCommand.Connection.Open();
                var rowsAffected = oleDbCommand.ExecuteNonQuery();
                oleDbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }


        public bool UpdateFixedexpenditure(Model.FixedExpenditureModel expenditure)
        {
            using (OleDbCommand oleDbCommand = new OleDbCommand())
            {
                // Set the command object properties
                oleDbCommand.Connection = new OleDbConnection(this.IncomeConnectionString);
                oleDbCommand.CommandType = CommandType.Text;
                oleDbCommand.CommandText = Budget_2016.Scripts.Scripts.sqlUpdateFixedExpenditure;

                // Add the input parameters to the parameter collection
                oleDbCommand.Parameters.AddWithValue("@rent", expenditure.Rent);
                oleDbCommand.Parameters.AddWithValue("@electricity", expenditure.Electricity);
                oleDbCommand.Parameters.AddWithValue("@water", expenditure.Water);
                oleDbCommand.Parameters.AddWithValue("@gas", expenditure.Gas);
                oleDbCommand.Parameters.AddWithValue("@telephone", expenditure.Telephone);
                oleDbCommand.Parameters.AddWithValue("@mobile", expenditure.Mobile);
                oleDbCommand.Parameters.AddWithValue("@cable", expenditure.Cable);
                oleDbCommand.Parameters.AddWithValue("@internet", expenditure.Internet);
                oleDbCommand.Parameters.AddWithValue("@buspass", expenditure.Buspass);
                oleDbCommand.Parameters.AddWithValue("@petrol", expenditure.Petrol);
                oleDbCommand.Parameters.AddWithValue("@emi", expenditure.EMI);
                oleDbCommand.Parameters.AddWithValue("@others", expenditure.Others);
                oleDbCommand.Parameters.AddWithValue("@total", expenditure.TotalFixedExpenditure);
                oleDbCommand.Parameters.AddWithValue("@month", expenditure.Months);
                oleDbCommand.Parameters.AddWithValue("@year", expenditure.Years);
                oleDbCommand.Parameters.AddWithValue("@id", expenditure.Id);


                // Open the connection, execute the query and close the connection
                oleDbCommand.Connection.Open();
                var rowsAffected = oleDbCommand.ExecuteNonQuery();
                oleDbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }


        public int GetMonthlyFixedExpenditure(string month, int year)
        {
            using (OleDbCommand dbCommand = new OleDbCommand())
            {
                // Set the command object properties
                dbCommand.Connection = new OleDbConnection(this.IncomeConnectionString);
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = Budget_2016.Scripts.Scripts.sqlGetMonthlyFixedExpenditure;

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


        public int CheckDuplicates(string month, int year)
        {
            using (OleDbCommand dbCommand = new OleDbCommand())
            {
                // Set the command object properties
                dbCommand.Connection = new OleDbConnection(this.IncomeConnectionString);
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = Budget_2016.Scripts.Scripts.sqlDuplicateFixedExpenditure;

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


        public DataTable GetDetailFixedExpenditure(string month, int year)
        {
            DataTable dataTable = new DataTable();
            using (OleDbDataAdapter DataAdapter = new OleDbDataAdapter())
            {
                // Create the command and set its properties
                DataAdapter.SelectCommand = new OleDbCommand();
                DataAdapter.SelectCommand.Connection = new OleDbConnection(this.IncomeConnectionString);
                DataAdapter.SelectCommand.CommandType = CommandType.Text;
                DataAdapter.SelectCommand.CommandText = Budget_2016.Scripts.Scripts.sqlGetDetailFixedExpenditure;

                // Add the input parameters to the parameter collection
                DataAdapter.SelectCommand.Parameters.AddWithValue("@month", month);
                DataAdapter.SelectCommand.Parameters.AddWithValue("@year", year);

                // Fill the table from adapter
                DataAdapter.Fill(dataTable);
            }
            return dataTable;
        }
    }
}
