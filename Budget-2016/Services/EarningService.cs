using Budget_2016.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget_2016.Services
{
    public class EarningService : IEarningService
    {
        public IEarningAccess earningAccess;
        public EarningService()
        {
            earningAccess = new EarningAccess();
        }
        public bool AddEarning(EarningModel earning)
        {
            return this.earningAccess.AddEarning(earning);
        }
        public bool UpdateEarning(EarningModel earning)
        {
            return this.earningAccess.UpdateEarning(earning);
        }
        public bool DeleteEarning(int id)
        {
            return this.earningAccess.DeleteEarning(id);
        }
        public int CheckDuplicates(string month, int year)
        {
            return this.earningAccess.CheckDuplicates(month,year); 
        }
        public int GetMonthlyEarning(string month, int year)
        {
            return this.earningAccess.GetMonthlyEarning(month, year); 
        }
    }
}
