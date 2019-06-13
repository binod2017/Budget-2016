using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Budget_2016
{
    public partial class AlertForm : Form
    {
        #region METHODS
        public AlertForm()
        {
            InitializeComponent();
            this.Text = "Fetching from Server.....";            
        }

        #endregion
        //int i = 0;
        //private void timer2_Tick(object sender, EventArgs e)
        //{
        //    i++;
        //    if (i >= 5)
        //    {
        //        i = i + 1;
        //        this.Text = "Fetching.....";
        //    }            
        //    if (10 == i)
        //    {
        //        i = 0;
        //        this.Text = "Loading.....";
        //    }
        //}

        //private void AlertForm_Load(object sender, EventArgs e)
        //{
        //    timer2.Start();
        //    timer2.Enabled = true;
        //    timer2.Interval = 1;
        //    i = 0;
        //}
               
    }
}
