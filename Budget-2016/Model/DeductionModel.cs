using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget_2016
{
    public class DeductionModel
    {        
        public int Id { get; set; }
        public int Leave { get; set; }
        public int PF { get; set; }
        public int ProfTax { get; set; }
        public int ESI { get; set; }
        public int TDS { get; set; }
        public int Others { get; set; }
        public int TotalDeduction { get; set; }
        public string Months { get; set; }
        public int Years { get; set; }
    }
}
