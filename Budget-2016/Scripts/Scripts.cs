using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Budget_2016.Scripts
{
    public static class Scripts
    {
        #region Earning
        /// <summary>
        /// sql to insert a Earning details
        /// </summary>
        public static readonly string sqlInsertEarning = "Insert Into" +
            " Earnings(Basic, Hra, Conveyance, Medical, Telephone, Fuel, Driver, Child, Food, Special, City, Bonus, TotalIncome, Months, Years)" +
            " Values(@basic, @hra, @conveyance, @medical, @telephone, @fuel, @driver, @child, @food, @special, @city, @bonus, @totalincome, @month, @year)";

        /// <summary>
        /// sql to update Earning details
        /// </summary>
        public static readonly string sqlUpdateEarning = "Update Earnings " +
            " Set [Basic] = @basic, [Hra] = @hra, [Conveyance] = @conveyance, [Medical] = @medical, " +
            "[Telephone] = @telephone, [Fuel] = @fuel, [Driver] = @driver, [Child] = @child, [Food] = @food, " +
            "[Special] = @special, [City] = @city, [Bonus] = @bonus, [TotalIncome] = @totalincome " +
            "where([Months] = @month and [Years] = @year)";// Where ([Id] = @id)";

        /// <summary>
        /// sql to delete a Earning record
        /// </summary>
        public static readonly string sqlDeleteEarning = "Delete From Earnings Where (Id = @id)";

        public static readonly string sqlInsertDeduction = "Insert Into" +
            " Deductions(Leave, PF, ProfTax, ESI, TDS, Others, TotalDeduction, Months, Years)" +
            " Values(@leave, @pf, @proftax, @esi, @tds, @others, @totaldeduction, @month, @year)";

        public static readonly string sqlUpdateDeduction = "Update Deductions " +
            " Set [Leave] = @leave, [PF] = @pf, [ProfTax] = @proftax, [ESI] = @esi, [TDS] = @tds, [Others] = @others, [TotalDeduction] = @totaldeduction Where([Months] = @month and [Years] = @year)";// Where ([Id] = @id)";

        public static readonly string sqlDeleteDeduction = "Delete From Deductions Where (Id = @id)";
        #endregion

        public static readonly string sqlInsertFixedExpenditure = "Insert Into" +
            " FixedExpenditures(Rent, Electricity, Water, Gas, Telephone, Mobile, Cable, Internet, Buspass, Petrol, EMI, Others, TotalFixedExpenditure, Months, Years)" +
            " Values(@rent, @electricity, @water, @gas, @telephone, @mobile, @cable, @internet, @buspass, @petrol, @emi, @others, @total, @month, @year)";

        public static readonly string sqlUpdateFixedExpenditure = "Update FixedExpenditures " +
            " Set [Rent] = @rent, [Electricity] = @electricity , [Water] = @water , [Gas] = @gas , [Telephone] = @telephone, [Mobile] = @mobile, [Cable] = @cable , [Internet] = @internet, [Buspass] = @buspass, [Petrol] = @petrol, [EMI] = @emi, [Others] = @others, [TotalFixedExpenditure] = @total where([Months] = @month and [Years] = @year)";//  Where ([Id] = @id)";

        public static readonly string sqlDeleteFixedExpenditure = "Delete From FixedExpenditures Where (Id = @id)";

        public static string sqlDuplicateDeduction = "Select COUNT(*) from Deductions where (Months=@month and Years=@year)";

        public static string sqlDuplicateEarning = "Select COUNT(*) from Earnings where (Months=@month and Years=@year)";

        public static string sqlGetMonthlyDeduction = "Select TotalDeduction as Deduction from Deductions where (Months=@month and Years=@year)";

        public static string sqlGetMonthlyEarning = "Select TotalIncome as Earning from Earnings where (Months=@month and Years=@year)";

        public static string sqlGetMonthlyFixedExpenditure = "Select TotalFixedExpenditure as FixedExpenditure from FixedExpenditures where (Months=@month and Years=@year)";

        public static string sqlDuplicateFixedExpenditure = "Select COUNT(*) from FixedExpenditures where (Months=@month and Years=@year)";

        public static string sqlGetDetailFixedExpenditure = "Select * from FixedExpenditures where (Months=@month and Years=@year)";
    }
}