using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget_2016
{
    public class EarningModel
    {        
        public int Id { get; set; }
        public int Basic { get; set; }
        public int Hra { get; set; }
        public int Conveyance { get; set; }
        public int Medical { get; set; }
        public int Telephone { get; set; }
        public int Fuel { get; set; }
        public int Driver { get; set; }
        public int Child { get; set; }
        public int Food { get; set; }
        public int Special { get; set; }
        public int City { get; set; }
        public int Bonus { get; set; }
        public int TotalIncome { get; set; }
        public string Months { get; set; }
        public int Years { get; set; }
    }
}
