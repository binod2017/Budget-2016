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
    public partial class AddOthers : Form
    {
        // add a delegate
        public delegate void OthersUpdateHandler(object sender, OthersUpdateEventArgs e);
        // add an event of the delegate type
        public event OthersUpdateHandler othersUpdated;
        public AddOthers()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            othersexpenditure = int.Parse(txtSpentFor.Text);
            // instance the event args and pass it each value
            OthersUpdateEventArgs args = new OthersUpdateEventArgs(othersexpenditure);
            // raise the event with the updated arguments
            othersUpdated(this, args);
            this.Close();
        }

        public int othersexpenditure { get; set; }
    }
    #region Events
    public class OthersUpdateEventArgs : System.EventArgs
    {
        // add local member variable to hold text
        private int _othersexpenditure;

        // class constructor
        public OthersUpdateEventArgs(int othersexpenditure)
        {
            this._othersexpenditure = othersexpenditure;
        }
        // Properties - Accessible by the listener
        public int Othersexpenditure
        {
            get
            {
                return _othersexpenditure;
            }
        }
    }
    #endregion
}
