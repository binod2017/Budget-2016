using Budget_2016.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Budget_2016
{
    public partial class FixedExpenditure : Form
    {
        // add a delegate
        public delegate void ExpenditureUpdateHandler(object sender, ExpenditureUpdateEventArgs e);
        // add an event of the delegate type
        public event ExpenditureUpdateHandler expenditureUpdated;
        public FixedExpenditureModel expenditure;
        private IFixedExpenditureService fixedexpenditureService;
        public FixedExpenditure()
        {
            InitializeComponent();
            this.Text = "Fixed Expenses";
            fixedexpenditureService = new FixedexpenditureService();
        }

        #region Variables
        int id, rent, electricity, water, gas, telephone, mobile, cable, internet, buspass, petrol, emi, others, fixedexpenditure;
        #endregion
        int year;
        string month;
        private void FixedExpenditure_Load(object sender, EventArgs e)
        {
            GetMonthlyExpenditure(month, year);
        }

        private void GetMonthlyExpenditure(string month, int year)
        {
            DataTable dt = fixedexpenditureService.GetDetailFixedExpenditure(month, year);
            if (dt.Rows.Count > 0)
            {
                txtRent.Text = dt.Rows[0]["Rent"].ToString();
                txtElectricity.Text = dt.Rows[0]["Electricity"].ToString();
                txtWater.Text = dt.Rows[0]["Water"].ToString();
                txtGas.Text = dt.Rows[0]["Gas"].ToString();
                txtTelephone.Text = dt.Rows[0]["Telephone"].ToString();
                txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                txtCable.Text = dt.Rows[0]["Cable"].ToString();
                txtInternet.Text = dt.Rows[0]["Internet"].ToString();
                txtBuspass.Text = dt.Rows[0]["Buspass"].ToString();
                txtPetrol.Text = dt.Rows[0]["Petrol"].ToString();
                txtEMI.Text = dt.Rows[0]["EMI"].ToString();
                txtOthers1.Text = dt.Rows[0]["Others"].ToString();
                txtTotalFixedExpenditure.Text = dt.Rows[0]["TotalFixedExpenditure"].ToString();
                txtMonth.Text = dt.Rows[0]["Months"].ToString();
                txtYear.Text = dt.Rows[0]["Years"].ToString();
            }
            Calculate();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            expenditure = new FixedExpenditureModel()
            {
                Id = 0,
                Rent = rent,
                Electricity = electricity,
                Water = water,
                Gas = gas,
                Telephone = telephone,
                Mobile = mobile,
                Cable = cable,
                Internet = internet,
                Buspass = buspass,
                Petrol = petrol,
                EMI = emi,
                Others = others,
                TotalFixedExpenditure = fixedexpenditure,
                Months = month,
                Years = year
            };
            //Save the Expenditures
            int checkfixedexpenditure = fixedexpenditureService.CheckDuplicates(month, year);
            if (checkfixedexpenditure == 0)
            {
                bool success = fixedexpenditureService.AddFixedexpenditure(expenditure);
                if (success)
                {
                    MessageBox.Show("Added Successfully");
                }
            }
            else { MessageBox.Show("Already Exist"); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            expenditure = new FixedExpenditureModel()
            {
                Id = 0,
                Rent = rent,
                Electricity = electricity,
                Water = water,
                Gas = gas,
                Telephone = telephone,
                Mobile = mobile,
                Cable = cable,
                Internet = internet,
                Buspass = buspass,
                Petrol = petrol,
                EMI = emi,
                Others = others,
                TotalFixedExpenditure = fixedexpenditure,
                Months = month,
                Years = year
            };
            bool success = fixedexpenditureService.UpdateFixedexpenditure(expenditure);
            if (success)
            {
                MessageBox.Show("Updated Successfully");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            GetMonthlyExpenditure(month, year);
            int fixedexpenditure = rent + electricity + water + gas + telephone + mobile + cable + internet + buspass + petrol + emi + others;
            txtTotalFixedExpenditure.Text = fixedexpenditure.ToString();
            // instance the event args and pass it each value
            ExpenditureUpdateEventArgs args = new ExpenditureUpdateEventArgs(fixedexpenditure);
            // raise the event with the updated arguments
            expenditureUpdated(this, args);
            //this.Dispose();
            this.Close();
        }

        private void txtOthers1_Click(object sender, EventArgs e)
        {
            AddOthers addothers = new AddOthers();
            addothers.othersUpdated += new AddOthers.OthersUpdateHandler(Others_Click);
            addothers.ShowDialog();
            this.ShowInTaskbar = false;
        }

        private void Others_Click(object sender, OthersUpdateEventArgs e)
        {
            txtOthers1.Text = e.Othersexpenditure.ToString();
            this.Show();
        }

        internal void Show(string Amonth, int Ayear)
        {
            month = Amonth;
            year = Ayear;
        }

        private void txtTotalFixedExpenditure_Leave(object sender, EventArgs e)
        {
            txtTotalFixedExpenditure.Text = "";
            Calculate();
        }

        private void Calculate()
        {
            if (txtRent.Text != "") { rent = int.Parse(txtRent.Text); } else { rent = 4500; txtRent.Text = "4500"; }
            if (txtElectricity.Text != "") { electricity = int.Parse(txtElectricity.Text); } else { electricity = 0; txtElectricity.Text = "0"; }
            if (txtWater.Text != "") { water = int.Parse(txtWater.Text); } else { water = 500; txtWater.Text = "500"; }
            if (txtGas.Text != "") { gas = int.Parse(txtGas.Text); } else { gas = 0; txtGas.Text = "0"; }
            if (txtTelephone.Text != "") { telephone = int.Parse(txtTelephone.Text); } else { telephone = 500; txtTelephone.Text = "500"; }
            if (txtMobile.Text != "") { mobile = int.Parse(txtMobile.Text); } else { mobile = 500; txtMobile.Text = "500"; }
            if (txtCable.Text != "") { cable = int.Parse(txtCable.Text); } else { cable = 0; txtCable.Text = "0"; }
            if (txtInternet.Text != "") { internet = int.Parse(txtInternet.Text); } else { internet = 500; txtInternet.Text = "500"; }
            if (txtBuspass.Text != "") { buspass = int.Parse(txtBuspass.Text); } else { buspass = 3000; txtBuspass.Text = "3000"; }
            if (txtPetrol.Text != "") { petrol = int.Parse(txtPetrol.Text); } else { petrol = 0; txtPetrol.Text = "0"; }
            if (txtEMI.Text != "") { emi = int.Parse(txtEMI.Text); } else { emi = 0; txtEMI.Text = "0"; }
            if (txtOthers1.Text != "") { others = int.Parse(txtOthers1.Text); } else { others = 0; txtOthers1.Text = "0"; }
            fixedexpenditure = rent + electricity + water + gas + telephone + mobile + cable + internet + buspass + petrol + emi + others;
            txtTotalFixedExpenditure.Text = (fixedexpenditure).ToString();
        }

        private void txtTotalFixedExpenditure_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                //MessageBox.Show(e.KeyChar.ToString());
            }
            else { e.Handled = true; txtTotalFixedExpenditure.Focus(); }
        }
    }
    #region Events
    public class ExpenditureUpdateEventArgs : System.EventArgs
    {
        // add local member variable to hold text
        private int _fixedexpenditure;

        // class constructor
        public ExpenditureUpdateEventArgs(int fixedexpenditure)
        {
            this._fixedexpenditure = fixedexpenditure;
        }
        // Properties - Accessible by the listener
        public int Fixedexpenditure
        {
            get
            {
                return _fixedexpenditure;
            }
        }
    }
    #endregion
}
