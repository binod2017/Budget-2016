using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget_2016.Services
{
    public interface IEarningService
    {
        bool AddEarning(EarningModel earning);
        bool UpdateEarning(EarningModel earning);
        bool DeleteEarning(int id);
        int CheckDuplicates(string month, int year);

        int GetMonthlyEarning(string month, int year);
    }
}
