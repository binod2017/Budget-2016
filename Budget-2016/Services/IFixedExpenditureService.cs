using Budget_2016.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Budget_2016
{
    interface IFixedExpenditureService
    {
        bool AddFixedexpenditure(FixedExpenditureModel expenditure);

        bool UpdateFixedexpenditure(FixedExpenditureModel expenditure);

        int GetMonthlyFixedExpenditure(string month, int year);

        int CheckDuplicates(string month, int year);

        DataTable GetDetailFixedExpenditure(string month, int year);
    }
}
