using Budget_2016.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Budget_2016
{
    public class FixedexpenditureService : IFixedExpenditureService
    {
        private IFixedExpenditureAccess fixedexpenditureAccess;
        public FixedexpenditureService()
        {
            fixedexpenditureAccess = new FixedexpenditureAccess();
        }
        public bool AddFixedexpenditure(FixedExpenditureModel expenditure)
        {
            return fixedexpenditureAccess.AddFixedexpenditure(expenditure);
        }
        public bool UpdateFixedexpenditure(FixedExpenditureModel expenditure)
        {
            return fixedexpenditureAccess.UpdateFixedexpenditure(expenditure);
        }
        public int GetMonthlyFixedExpenditure(string month, int year)
        {
            return fixedexpenditureAccess.GetMonthlyFixedExpenditure(month,year);
        }


        public int CheckDuplicates(string month, int year)
        {
            return fixedexpenditureAccess.CheckDuplicates(month, year);
        }


        public DataTable GetDetailFixedExpenditure(string month, int year)
        {
            return fixedexpenditureAccess.GetDetailFixedExpenditure(month, year);
        }
    }
}
