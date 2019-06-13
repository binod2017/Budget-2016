using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Budget_2016.Services
{
    public interface IDeductionService
    {
        bool AddDeduction(DeductionModel deduction);
        bool UpdateDeduction(DeductionModel deduction);
        bool DeleteDeduction(int id);
        int CheckDuplicates(string month, int year);

        int GetMonthlyDeduction(string month, int year);
    }
}
