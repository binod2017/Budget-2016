using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget_2016.Model
{
    public class FixedExpenditureModel
    {        
        public int Id { get; set; }
        public int Rent { get; set; }
        public int Electricity { get; set; }
        public int Water { get; set; }
        public int Gas { get; set; }
        public int Telephone { get; set; }
        public int Mobile { get; set; }
        public int Cable { get; set; }
        public int Internet { get; set; }
        public int Buspass { get; set; }
        public int Petrol { get; set; }
        public int EMI { get; set; }
        public int Others { get; set; }
        public int TotalFixedExpenditure { get; set; }
		public string Months { get; set; }
		public int Years { get; set; }
    }
}
