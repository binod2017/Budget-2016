using Budget_2016.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Budget_2016
{
    public class DeductionService : IDeductionService
    {
        private IDeductionAccess deductionAccess;
        public DeductionService()
        {
            deductionAccess = new DeductionAccess();
        }
        public bool AddDeduction(DeductionModel deduction)
        {
            return deductionAccess.AddDeduction(deduction);
        }

        public bool UpdateDeduction(DeductionModel deduction)
        {
            return deductionAccess.UpdateDeduction(deduction);
        }

        public bool DeleteDeduction(int id)
        {
            return deductionAccess.DeleteDeduction(id);
        }


        public int CheckDuplicates(string month, int year)
        {
            return deductionAccess.CheckDuplicates(month,year);
        }


        public int GetMonthlyDeduction(string month, int year)
        {
            return deductionAccess.GetMonthlyDeduction(month, year);
        }
    }
}
