using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DbConnectionStrings;
using DBHelpers;
using DataAccess;
using Budget_2016.Services;

namespace Budget_2016
{
    public partial class Earning : Form
    {
        private IEarningService earningService;
        private IDeductionService deductionService;
        public Earning()
        {
            InitializeComponent();
            this.Text = "Income Details";
            groupBox2.Visible = false;
            this.earningService = new EarningService();
            this.deductionService = new DeductionService();
        }

        /// <summary>
        /// Resize the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAddSpouse_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAddSpouse.Checked)
            {
                this.ClientSize = new System.Drawing.Size(868, 475);
                groupBox2.Visible = true;
            }
            else { this.ClientSize = new System.Drawing.Size(435, 475); groupBox2.Visible = false; }
        }

        /// <summary>
        /// Save the salary details in database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            string month = txtMth.Text.Substring(0, 3);
            int year = int.Parse(txtYr.Text);
            EarningModel earning = new EarningModel()
            {
                Basic = selfbasic,
                Hra = selfhra,
                Conveyance = selfconveyance,
                Medical = selfmedical,
                Telephone = selftelephone,
                Fuel = selffuel,
                Driver = selfdriver,
                Child = selfchild,
                Food = selffood,
                Special = selfspecial,
                City = selfcity,
                Bonus = selfbonus,
                TotalIncome = selfincome,
                Months = month,
                Years = year
            };

            DeductionModel deduction = new DeductionModel()
            {
                Leave = selfleave,
                PF = selfpf,
                ProfTax = selfproftax,
                ESI = selfesi,
                TDS = selftds,
                Others = selfothers,
                TotalDeduction = selfdeduction,
                Months = month,
                Years = year
            };
            //Check if the record exist(Duplicate Records) for same month and year
            int checkearning = earningService.CheckDuplicates(month, year);
            int checkdeduction = deductionService.CheckDuplicates(month, year);
            if (checkearning == 0 && checkdeduction == 0)
            {
                var success = earningService.AddEarning(earning);
                var success1 = deductionService.AddDeduction(deduction);
                if (success == true && success1 == true)
                {
                    MessageBox.Show("Added Successfully");
                }
            }
            else { MessageBox.Show("Already Exist"); }

        }
        /// <summary>
        /// Update the salary details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string month = txtMth.Text.Substring(0, 3);
            int year = int.Parse(txtYr.Text);
            EarningModel earning = new EarningModel()
            {
                Basic = selfbasic,
                Hra = selfhra,
                Conveyance = selfconveyance,
                Medical = selfmedical,
                Telephone = selftelephone,
                Fuel = selffuel,
                Driver = selfdriver,
                Child = selfchild,
                Food = selffood,
                Special = selfspecial,
                City = selfcity,
                Bonus = selfbonus,
                TotalIncome = selfincome,
                Months = month,
                Years = year
            };

            DeductionModel deduction = new DeductionModel()
            {
                Leave = selfleave,
                PF = selfpf,
                ProfTax = selfproftax,
                ESI = selfesi,
                TDS = selftds,
                Others = selfothers,
                TotalDeduction = selfdeduction,
                Months = month,
                Years = year
            };
            var success = earningService.UpdateEarning(earning);
            var success1 = deductionService.UpdateDeduction(deduction);
            if (success == true && success1 == true)
            {
                MessageBox.Show("Updated Successfully");
            }
        }
        /// <summary>
        /// Clear all the Textboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearallTextboxes();
        }

        private void ClearallTextboxes()
        {
            txtSelfBasic.Text = txtSelfBonus.Text = txtSelfCity.Text = txtSelfConveyance.Text = "0";
            txtSelfDeductiion.Text = txtSelfESI.Text = txtSelfHRA.Text = txtSelfLeave.Text = "0";
            txtSelfMedical.Text = txtSelfTelephone.Text = txtSelfFuel.Text = txtSelfDriver.Text = txtSelfChild.Text = "0";
            txtSelfFood.Text = txtSelfNet.Text = txtSelfOthers.Text = txtSelfPf.Text = "0";
            txtSelfProftax.Text = txtSelfSpecial.Text = txtSelfTDS.Text = txtSelfIncome.Text = "0";

            txtSpouseBasic.Text = txtSpouseBonus.Text = txtSpouseCity.Text = txtSpouseConveyance.Text = "0";
            txtSpouseDeductions.Text = txtSpouseESI.Text = txtSpouseHRA.Text = txtSpouseIncome.Text = "0";
            txtSpouseLeave.Text = txtSpouseMedical.Text = txtSpouseTelephone.Text = txtSpouseFuel.Text = txtSpouseDriver.Text = txtSpouseChild.Text = "0";
            txtSpouseFood.Text = txtSpouseNet.Text = txtSpouseOthers.Text = "0";
            txtSpousePF.Text = txtSpouseProftax.Text = txtSpouseSpecial.Text = txtSpouseTDS.Text = "0";
        }

        /// <summary>
        /// Close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #region Income Variables
        int selfbasic, selfhra, selfconveyance, selfmedical, selftelephone, selffuel, selfdriver, selfchild, selffood, selfspecial, selfcity, selfbonus;
        int spousebasic, spousehra, spouseconveyance, spousemedical, spousetelephone, spousefuel, spousedriver, spousechild, spousefood, spousespecial, spousecity, spousebonus;

        int selfleave, selfpf, selfproftax, selfesi, selftds, selfothers;
        int spouseleave, spousepf, spouseproftax, spouseesi, spousetds, spouseothers;

        int selfincome, selfdeduction, selfnet;
        int spouseincome, spousededuction, spousenet;

        #endregion

        private void Income_Load(object sender, EventArgs e)
        {
            txtMth.Text = DateTime.Now.ToString("MMMM");
            txtYr.Text = DateTime.Now.Year.ToString();
            txtSelfIncome.Text = txtSpouseIncome.Text = txtSelfNet.Text = txtSpouseNet.Text = "0.00";
            txtSelfDeductiion.Text = txtSpouseDeductions.Text = "0.00";
            GetData(txtMth.Text, txtYr.Text);
            //Calculate();
            txtSelfBasic.Focus();
        }

        private void GetData(string month, string year)
        {
            
        }

        private void txtSelfBasic_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                //MessageBox.Show(e.KeyChar.ToString());
            }
            else { e.Handled = true; txtSelfBasic.Focus(); }
        }

        private void Calculate()
        {
            #region Self income
            ////string test = txtSelfBasic.Text;
            ////MessageBox.Show(test);
            if (txtSelfBasic.Text.Trim() != "0") { selfbasic = int.Parse(txtSelfBasic.Text); } else { selfbasic = 46875; txtSelfBasic.Text = "46875"; }
            if (txtSelfHRA.Text.Trim() != "0") { selfhra = int.Parse(txtSelfHRA.Text); } else { selfhra = 23437; txtSelfHRA.Text = "23437"; }
            if (txtSelfConveyance.Text.Trim() != "0") { selfconveyance = int.Parse(txtSelfConveyance.Text); } else { selfconveyance = 1600; txtSelfConveyance.Text = "1600"; }
            if (txtSelfMedical.Text.Trim() != "0") { selfmedical = int.Parse(txtSelfMedical.Text); } else { selfmedical = 1250; txtSelfMedical.Text = "1250"; }
            if (txtSelfTelephone.Text.Trim() != "0") { selftelephone = int.Parse(txtSelfTelephone.Text); } else { selftelephone = 1000; txtSelfTelephone.Text = "1000"; }
            if (txtSelfFuel.Text.Trim() != "0") { selffuel = int.Parse(txtSelfFuel.Text); } else { selffuel = 2500; txtSelfFuel.Text = "2500"; }
            if (txtSelfDriver.Text.Trim() != "0") { selfdriver = int.Parse(txtSelfDriver.Text); } else { selfdriver = 900; txtSelfDriver.Text = "900"; }
            if (txtSelfChild.Text.Trim() != "0") { selfchild = int.Parse(txtSelfChild.Text); } else { selfchild = 200; txtSelfChild.Text = "200"; }
            if (txtSelfFood.Text.Trim() != "0") { selffood = int.Parse(txtSelfFood.Text); } else { selffood = 2200; txtSelfFood.Text = "2200"; }
            if (txtSelfSpecial.Text.Trim() != "0") { selfspecial = int.Parse(txtSelfSpecial.Text); } else { selfspecial = 24204; txtSelfSpecial.Text = "24204"; }
            if (txtSelfCity.Text.Trim() != "0") { selfcity = int.Parse(txtSelfCity.Text); } else { selfcity = 0; txtSelfCity.Text = "0"; }
            if (txtSelfBonus.Text.Trim() != "0") { selfbonus = int.Parse(txtSelfBonus.Text); } else { selfbonus = 0; txtSelfBonus.Text = "0"; }

            if (txtSelfLeave.Text.Trim() != "0") { selfleave = int.Parse(txtSelfLeave.Text); } else { selfleave = 0; txtSelfLeave.Text = "0"; }
            if (txtSelfPf.Text.Trim() != "0") { selfpf = int.Parse(txtSelfPf.Text); } else { selfpf = 0; txtSelfPf.Text = "0"; }
            if (txtSelfProftax.Text.Trim() != "0") { selfproftax = int.Parse(txtSelfProftax.Text); } else { selfproftax = 200; txtSelfProftax.Text = "200"; }
            if (txtSelfESI.Text.Trim() != "0") { selfesi = int.Parse(txtSelfESI.Text); } else { selfesi = 0; txtSelfESI.Text = "0"; }
            if (txtSelfTDS.Text.Trim() != "0") { selftds = int.Parse(txtSelfTDS.Text); } else { selftds = 0; txtSelfTDS.Text = "0"; }
            if (txtSelfOthers.Text.Trim() != "0") { selfothers = int.Parse(txtSelfOthers.Text); } else { selfothers = 0; txtSelfOthers.Text = "0"; }
            #endregion

            #region Spouse income
            //if (txtSpouseBasic.Text.Trim() != "0") { spousebasic = int.Parse(txtSpouseBasic.Text); } else { spousebasic = 0; txtSpouseBasic.Text = "0"; }
            //if (txtSpouseHRA.Text.Trim() != "0") { spousehra = int.Parse(txtSpouseHRA.Text); } else { spousehra = 0; txtSpouseHRA.Text = "0"; }
            //if (txtSpouseConveyance.Text.Trim() != "0") { spouseconveyance = int.Parse(txtSpouseConveyance.Text); } else { spouseconveyance = 0; txtSpouseConveyance.Text = "0"; }
            //if (txtSpouseMedical.Text.Trim() != "0") { spousemedical = int.Parse(txtSpouseMedical.Text); } else { spousemedical = 0; txtSpouseMedical.Text = "0"; }
            //if (txtSpouseTelephone.Text.Trim() != "0") { spousetelephone = int.Parse(txtSpouseTelephone.Text); } else { spousetelephone = 0; txtSpouseTelephone.Text = "0"; }
            //if (txtSpouseFuel.Text.Trim() != "0") { spousefuel = int.Parse(txtSpouseFuel.Text); } else { spousefuel = 0; txtSpouseFuel.Text = "0"; }
            //if (txtSpouseDriver.Text.Trim() != "0") { spousedriver = int.Parse(txtSpouseDriver.Text); } else { spousedriver = 0; txtSpouseDriver.Text = "0"; }
            //if (txtSpouseChild.Text.Trim() != "0") { spousechild = int.Parse(txtSpouseChild.Text); } else { spousechild = 0; txtSpouseChild.Text = "0"; }
            //if (txtSpouseFood.Text.Trim() != "0") { spousefood = int.Parse(txtSpouseFood.Text); } else { spousefood = 0; txtSpouseFood.Text = "0"; }
            //if (txtSpouseSpecial.Text.Trim() != "0") { spousespecial = int.Parse(txtSpouseSpecial.Text); } else { spousespecial = 0; txtSpouseSpecial.Text = "0"; }
            //if (txtSpouseCity.Text.Trim() != "0") { spousecity = int.Parse(txtSpouseCity.Text); } else { spousecity = 0; txtSpouseCity.Text = "0"; }
            //if (txtSpouseBonus.Text.Trim() != "0") { spousebonus = int.Parse(txtSpouseBonus.Text); } else { spousebonus = 0; txtSpouseBonus.Text = "0"; }

            //if (txtSpouseLeave.Text.Trim() != "0") { spouseleave = int.Parse(txtSpouseLeave.Text); } else { spouseleave = 0; txtSpouseLeave.Text = "0"; }
            //if (txtSpousePF.Text.Trim() != "0") { spousepf = int.Parse(txtSpousePF.Text); } else { spousepf = 0; txtSpousePF.Text = "0"; }
            //if (txtSpouseProftax.Text.Trim() != "0") { spouseproftax = int.Parse(txtSpouseProftax.Text); } else { spouseproftax = 0; txtSpouseProftax.Text = "0"; }
            //if (txtSpouseESI.Text.Trim() != "0") { spouseesi = int.Parse(txtSpouseESI.Text); } else { spouseesi = 0; txtSpouseESI.Text = "0"; }
            //if (txtSpouseTDS.Text.Trim() != "0") { spousetds = int.Parse(txtSpouseTDS.Text); } else { spousetds = 0; txtSpouseTDS.Text = "0"; }
            //if (txtSpouseOthers.Text.Trim() != "0") { spouseothers = int.Parse(txtSpouseOthers.Text); } else { spouseothers = 0; txtSpouseOthers.Text = "0"; }
            #endregion

            selfincome = selfbasic + selfhra + selfconveyance + selfmedical + selftelephone + selffuel + selfdriver + selfchild + selffood + selfspecial + selfcity + selfbonus;
            selfdeduction = selfleave + selfpf + selfproftax + selfesi + selftds + selfothers;
            selfnet = selfincome - selfdeduction;
            txtSelfIncome.Text = selfincome.ToString() + ".00";
            txtSelfDeductiion.Text = selfdeduction.ToString() + ".00";
            txtSelfNet.Text = selfnet.ToString() + ".00";

            spouseincome = spousebasic + spousehra + spouseconveyance + spousemedical + spousetelephone + spousefuel + spousedriver + spousechild + spousefood + spousespecial + spousecity + spousebonus;
            spousededuction = spouseleave + spousepf + spouseproftax + spouseesi + spousetds + spouseothers;
            spousenet = spouseincome - spousededuction;
            txtSpouseIncome.Text = spouseincome.ToString() + ".00";
            txtSpouseDeductions.Text = spousededuction.ToString() + ".00";
            txtSpouseNet.Text = spousenet.ToString() + ".00";
        }

        private void txtSelfBasic_Leave(object sender, EventArgs e)
        {
            txtSelfIncome.Text = txtSelfNet.Text = "";
            Calculate();
        }

    }
}
