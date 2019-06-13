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
    public partial class TaxCalculator : Form
    {
        public TaxCalculator()
        {
            InitializeComponent();
            this.Text = "Tax Calculation";
        }
        #region Variables
        int jant1, jant2, jant, febt1, febt2, febt, mart1, mart2, mart, aprt1, aprt2, aprt;
        int mayt1, mayt2, mayt, junt1, junt2, junt, jult1, jult2, jult, augt1, augt2, augt;
        int sept1, sept2, sept, octt1, octt2, octt, novt1, novt2, novt, dect1, dect2, dect;
        int tctc, thra, tother, tproftax, tnet, ttaxable, tbenefits, tincome, tsurcharge;
        int tcess, tliability, tmonthly, tratio;
        #endregion
        private void TaxCalculator_Load(object sender, EventArgs e)
        {
            Calculate(); EstimateTax();
        }
        private void Jan1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                //MessageBox.Show(e.KeyChar.ToString());
            }
            else { e.Handled = true; Jan1.Focus(); }
        }

        private void Jan1_KeyUp(object sender, KeyEventArgs e)
        {
            Calculate(); EstimateTax();
        }

        private void Jan1_Leave(object sender, EventArgs e)
        {
            Calculate(); EstimateTax();
        }

        private void Calculate()
        {
            jant1 = int.Parse(Jan1.Text) + int.Parse(Jan2.Text) + int.Parse(Jan3.Text) + int.Parse(Jan4.Text) + int.Parse(Jan5.Text) + int.Parse(Jan6.Text) + int.Parse(Jan7.Text);
            jant2 = int.Parse(Jan8.Text) + int.Parse(Jan9.Text) + int.Parse(Jan10.Text) + int.Parse(Jan11.Text) + int.Parse(Jan12.Text) + int.Parse(Jan13.Text);

            febt1 = int.Parse(Feb1.Text) + int.Parse(Feb2.Text) + int.Parse(Feb3.Text) + int.Parse(Feb4.Text) + int.Parse(Feb5.Text) + int.Parse(Feb6.Text) + int.Parse(Feb7.Text);
            febt2 = int.Parse(Feb8.Text) + int.Parse(Feb9.Text) + int.Parse(Feb10.Text) + int.Parse(Feb11.Text) + int.Parse(Feb12.Text) + int.Parse(Feb13.Text);

            mart1 = int.Parse(Mar1.Text) + int.Parse(Mar2.Text) + int.Parse(Mar3.Text) + int.Parse(Mar4.Text) + int.Parse(Mar5.Text) + int.Parse(Mar6.Text) + int.Parse(Mar7.Text);
            mart2 = int.Parse(Mar8.Text) + int.Parse(Mar9.Text) + int.Parse(Mar10.Text) + int.Parse(Mar11.Text) + int.Parse(Mar12.Text) + int.Parse(Mar13.Text);

            aprt1 = int.Parse(Apr1.Text) + int.Parse(Apr2.Text) + int.Parse(Apr3.Text) + int.Parse(Apr4.Text) + int.Parse(Apr5.Text) + int.Parse(Apr6.Text) + int.Parse(Apr7.Text);
            aprt2 = int.Parse(Apr8.Text) + int.Parse(Apr9.Text) + int.Parse(Apr10.Text) + int.Parse(Apr11.Text) + int.Parse(Apr12.Text) + int.Parse(Apr13.Text);

            mayt1 = int.Parse(May1.Text) + int.Parse(May2.Text) + int.Parse(May3.Text) + int.Parse(May4.Text) + int.Parse(May5.Text) + int.Parse(May6.Text) + int.Parse(May7.Text);
            mayt2 = int.Parse(May8.Text) + int.Parse(May9.Text) + int.Parse(May10.Text) + int.Parse(May11.Text) + int.Parse(May12.Text) + int.Parse(May13.Text);

            junt1 = int.Parse(Jun1.Text) + int.Parse(Jun2.Text) + int.Parse(Jun3.Text) + int.Parse(Jun4.Text) + int.Parse(Jun5.Text) + int.Parse(Jun6.Text) + int.Parse(Jun7.Text);
            junt2 = int.Parse(Jun8.Text) + int.Parse(Jun9.Text) + int.Parse(Jun10.Text) + int.Parse(Jun11.Text) + int.Parse(Jun12.Text) + int.Parse(Jun13.Text);

            jult1 = int.Parse(Jul1.Text) + int.Parse(Jul2.Text) + int.Parse(Jul3.Text) + int.Parse(Jul4.Text) + int.Parse(Jul5.Text) + int.Parse(Jul6.Text) + int.Parse(Jul7.Text);
            jult2 = int.Parse(Jul8.Text) + int.Parse(Jul9.Text) + int.Parse(Jul10.Text) + int.Parse(Jul11.Text) + int.Parse(Jul12.Text) + int.Parse(Jul13.Text);

            augt1 = int.Parse(Aug1.Text) + int.Parse(Aug2.Text) + int.Parse(Aug3.Text) + int.Parse(Aug4.Text) + int.Parse(Aug5.Text) + int.Parse(Aug6.Text) + int.Parse(Aug7.Text);
            augt2 = int.Parse(Aug8.Text) + int.Parse(Aug9.Text) + int.Parse(Aug10.Text) + int.Parse(Aug11.Text) + int.Parse(Aug12.Text) + int.Parse(Aug13.Text);

            sept1 = int.Parse(Sep1.Text) + int.Parse(Sep2.Text) + int.Parse(Sep3.Text) + int.Parse(Sep4.Text) + int.Parse(Sep5.Text) + int.Parse(Sep6.Text) + int.Parse(Sep7.Text);
            sept2 = int.Parse(Sep8.Text) + int.Parse(Sep9.Text) + int.Parse(Sep10.Text) + int.Parse(Sep11.Text) + int.Parse(Sep12.Text) + int.Parse(Sep13.Text);

            octt1 = int.Parse(Oct1.Text) + int.Parse(Oct2.Text) + int.Parse(Oct3.Text) + int.Parse(Oct4.Text) + int.Parse(Oct5.Text) + int.Parse(Oct6.Text) + int.Parse(Oct7.Text);
            octt2 = int.Parse(Oct8.Text) + int.Parse(Oct9.Text) + int.Parse(Oct10.Text) + int.Parse(Oct11.Text) + int.Parse(Oct12.Text) + int.Parse(Oct13.Text);

            novt1 = int.Parse(Nov1.Text) + int.Parse(Nov2.Text) + int.Parse(Nov3.Text) + int.Parse(Nov4.Text) + int.Parse(Nov5.Text) + int.Parse(Nov6.Text) + int.Parse(Nov7.Text);
            novt2 = int.Parse(Nov8.Text) + int.Parse(Nov9.Text) + int.Parse(Nov10.Text) + int.Parse(Nov11.Text) + int.Parse(Nov12.Text) + int.Parse(Nov13.Text);

            dect1 = int.Parse(Dec1.Text) + int.Parse(Dec2.Text) + int.Parse(Dec3.Text) + int.Parse(Dec4.Text) + int.Parse(Dec5.Text) + int.Parse(Dec6.Text) + int.Parse(Dec7.Text);
            dect2 = int.Parse(Dec8.Text) + int.Parse(Dec9.Text) + int.Parse(Dec10.Text) + int.Parse(Dec11.Text) + int.Parse(Dec12.Text) + int.Parse(Dec13.Text);

            jant = jant1 - jant2;
            JanT1.Text = jant1.ToString();
            JanT2.Text = jant2.ToString();
            JanT.Text = jant.ToString();

            febt = febt1 - febt2;
            FebT1.Text = febt1.ToString();
            FebT2.Text = febt2.ToString();
            FebT.Text = febt.ToString();

            mart = mart1 - mart2;
            MarT1.Text = mart1.ToString();
            MarT2.Text = mart2.ToString();
            MarT.Text = mart.ToString();

            aprt = aprt1 - aprt2;
            AprT1.Text = aprt1.ToString();
            AprT2.Text = aprt2.ToString();
            AprT.Text = aprt.ToString();

            mayt = mayt1 - mayt2;
            MayT1.Text = mayt1.ToString();
            MayT2.Text = mayt2.ToString();
            MayT.Text = mayt.ToString();

            junt = junt1 - junt2;
            JunT1.Text = junt1.ToString();
            JunT2.Text = junt2.ToString();
            JunT.Text = junt.ToString();

            jult = jult1 - jult2;
            JulT1.Text = jult1.ToString();
            JulT2.Text = jult2.ToString();
            JulT.Text = jult.ToString();

            augt = augt1 - augt2;
            AugT1.Text = augt1.ToString();
            AugT2.Text = augt2.ToString();
            AugT.Text = augt.ToString();

            sept = sept1 - sept2;
            SepT1.Text = sept1.ToString();
            SepT2.Text = sept2.ToString();
            SepT.Text = sept.ToString();

            octt = octt1 - octt2;
            OctT1.Text = octt1.ToString();
            OctT2.Text = octt2.ToString();
            OctT.Text = octt.ToString();

            novt = novt1 - novt2;
            NovT1.Text = novt1.ToString();
            NovT2.Text = novt2.ToString();
            NovT.Text = novt.ToString();

            dect = dect1 - dect2;
            DecT1.Text = dect1.ToString();
            DecT2.Text = dect2.ToString();
            DecT.Text = dect.ToString();

            Tctc.Text = (jant + febt + mart + aprt + mayt + junt + jult + augt + sept + octt + novt + dect).ToString();
        }
        int tded, tcredit, tincome0, tincome1, tincome2, tincome3, tincome4;
        private void EstimateTax()
        {
            tctc = int.Parse(Tctc.Text);
            thra = int.Parse(Thra.Text);
            tother = int.Parse(Tother.Text);
            tproftax = int.Parse(Tproftax.Text);
            tbenefits = int.Parse(Tbenefits.Text);

            tnet = tctc - (thra + tother + tproftax);
            Tnet.Text = tnet.ToString();
            ttaxable = tnet - tbenefits;
            Ttaxable.Text = ttaxable.ToString();

            if (ttaxable <= 250000)
            { }
            else
            { //0%
                tincome0 = tded;
            }

            if (ttaxable >= 250001 && ttaxable <= 500000)
            {
                tincome1 = (ttaxable - 250000) * 10 / 100;
            }
            else if (ttaxable > 500000)
            { //10%
                tincome1 = Math.Min(ttaxable, (500000 - 250000)) * 10 / 100;
            }

            if (ttaxable >= 500001 && ttaxable <= 1000000)
            { //20%
                tincome2 = (ttaxable - 500000) * 20 / 100;
            }
            else if (ttaxable > 1000000)
            {
                tincome2 = Math.Min(ttaxable, (1000000 - 500000)) * 20 / 100;
            }

            if (ttaxable >= 10000001)
            { //30%
                tincome3 = (ttaxable - 10000000) * 30 / 100;
            }
            else { tincome3 = 0; }

            if (ttaxable > 10000000)
            {
                tincome4 = tincome3 * 12 / 100;
            }
            tincome = tincome0 + tincome1 + tincome2 + tincome3 + tincome4;
            Tincome.Text = tincome.ToString();

            tcess = tincome + (tincome * 3 / 100);
            Tcess.Text = tcess.ToString();
            if (ttaxable <= 500000)
            {
                tcredit = Math.Min(2000, tincome1);
            }

            tliability = tcess - tcredit;
            Tliability.Text = tliability.ToString();

            Tmonthly.Text = (tliability / 12).ToString();
            if (tctc != 0)
            {
                Tratio.Text = (tliability / tctc).ToString() + " %";
            }

        }

        private void TaxCalculator_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Thra_Click(object sender, EventArgs e)
        {
            HRAExemption hraexemption = new HRAExemption();
            hraexemption.hraUpdated += new HRAExemption.HraUpdateHandler(Thra1_Click);
            hraexemption.ShowDialog();
        }

        private void Thra1_Click(object sender, HraUpdateEventArgs e)
        {
            Thra.Text = e.Hraexemption.ToString();
            this.Show();
        }

        private void Tbenefits_Click(object sender, EventArgs e)
        {
            TaxBenefits taxbenefit = new TaxBenefits();
            taxbenefit.benefitUpdated += new TaxBenefits.BenefitUpdateHandler(Tbenefits1_Click);
            taxbenefit.ShowDialog();
        }

        private void Tbenefits1_Click(object sender, BenefitUpdateEventArgs e)
        {
            Tbenefits.Text = e.Benefitexemption.ToString();
            this.Show();
        }

    }
}
