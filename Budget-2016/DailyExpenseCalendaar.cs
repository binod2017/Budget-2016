using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess;
using SqlQueries;
using DbConnectionStrings;
using DBHelpers;
using Budget_2016.Services;

namespace Budget_2016
{
    public partial class DailyExpenseCalendaar : Form
    {
        #region Variables
        string[] pics = { "1.jpg", "2.jpg", "3.jpg", "4.jpg", "5.jpg", "6.jpg", "7.jpg", "8.jpg",
                        "9.jpg", "10.jpg", "11.jpg", "12.jpg", "13.jpg", "14.jpg", "15.jpg", "16.jpg",
                        "17.jpg", "18.jpg", "19.jpg", "20.jpg", "21.jpg", "22.jpg", "23.jpg", "24.jpg",
                        "25.jpg", "26.jpg", "27.jpg", "28.jpg", "29.jpg", "30.jpg"};
        //Variables for Moon program
        private double ip;
        private double ag;
        ArrayList p = new ArrayList(42);
        ArrayList d = new ArrayList(42);
        int i = 0;
        string path = ("../../Images/");
        int Aday, Amonth, Ayear;
        private IEarningService earningService;
        private IDeductionService deductionService;
        private IFixedExpenditureService fixedexpenditureService;
        #endregion
        public DailyExpenseCalendaar()
        {
            InitializeComponent();
            this.Text = "Daily Expense Calendar";
            earningService = new EarningService();
            deductionService = new DeductionService();
            fixedexpenditureService = new FixedexpenditureService();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Timer tick event
            i++;
            if (i < 0)
            {
                i = pics.Length;
            }
            if (pics.Length == i)
            {
                i = 0;
            }
            //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Images\1.gif");
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            String Root = Directory.GetCurrentDirectory();
            pictureBox1.Image = Image.FromFile(path + pics[i]);
        }
        private void Calender_Load(object sender, EventArgs e)
        {
           UpdateDatabase();
            ShowMoon();
            LoadYear();
            Checking();

            //For Slide Show
            timer1.Enabled = true;
            timer1.Interval = 500;
            i = 0;

        }
        private void UpdateDatabase()
        {
            string filename = "Events.accdb";
            string sourcepath = @"D:\2010\DesktopApps\DesktopApps\bin\Release"; //C:\Users\Admin\Google Drive
            string targetpath = @"D:\2013\Budget-2016\Budget-2016\bin\Release"; //App Path
            string targetpath1 = @"D:\2013\Budget-2016\Budget-2016\bin\Debug"; ///App Path

            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.IO.Path.Combine(sourcepath, filename);
            string destFile = System.IO.Path.Combine(targetpath, filename);
            string destFile1 = System.IO.Path.Combine(targetpath1, filename);

            // To copy a file to another location and 
            // overwrite the destination file if it already exists.
            System.IO.File.Copy(sourceFile, destFile, true);
            System.IO.File.Copy(sourceFile, destFile1, true);
        }
        private void GetMonthlyDetails(int Amonth, int Ayear)
        {
            int monthlyincome = GetMonthlyIncome(GetMonthString(Amonth), Ayear);
            int monthlyfixedexpenditure = GetMonthlyFixedExpenditure(GetMonthString(Amonth), Ayear);
            int monthlspent = GetMonthlyExpenditure(GetMonthString(Amonth), Ayear);

            txtMonthlyIncome.Text = monthlyincome.ToString();
            txtFixedMonthlyExpenditure.Text = monthlyfixedexpenditure.ToString();
            txtTotalMonthlySpent.Text = monthlspent.ToString();
            txtMonthlySavings.Text = (monthlyincome - (monthlyfixedexpenditure + monthlspent)).ToString();
        }

        private int GetMonthlyExpenditure(string month, int year)
        {
            ExpenseAccess expenseaccess = new ExpenseAccess();
            DataTable dr = expenseaccess.SearchExpensebyMonth(month, year);
            int total1 = 0;
            int item = 0;
            if (dr != null)
            {
                for (int i = 0; i < dr.Rows.Count; i++)
                {
                    item = int.Parse(dr.Rows[i]["Price"].ToString());
                    total1 = total1 + item;
                }
            }
            else { }
            if (total1 != 0)
            {
                return total1;
            }
            else { return 0; }
        }

        private int GetMonthlyFixedExpenditure(string month, int year)
        {
            int fixedexpenditure = fixedexpenditureService.GetMonthlyFixedExpenditure(month, year);
            return fixedexpenditure;
        }

        private int GetMonthlyIncome(string month, int year)
        {
            int earning = earningService.GetMonthlyEarning(month, year);
            int deduction = deductionService.GetMonthlyDeduction(month, year);
            int income = earning - deduction;
            return income;
        }
        private void btnGet_Click(object sender, EventArgs e)
        {
            AlertForm f1 = new AlertForm();
            f1.Show();
            this.LoadImages();
            GetMonthlyDetails(Amonth, Ayear);
            f1.Hide();
        }
        private static int Findmonth(string month)
        {
            int i = 0;

            switch (month)
            {
                case "January":
                    i = 1;
                    break;
                case "February":
                    i = 2;
                    break;
                case "March":
                    i = 3;
                    break;
                case "April":
                    i = 4;
                    break;
                case "May":
                    i = 5;
                    break;
                case "June":
                    i = 6;
                    break;
                case "July":
                    i = 7;
                    break;
                case "August":
                    i = 8;
                    break;
                case "September":
                    i = 9;
                    break;
                case "October":
                    i = 10;
                    break;
                case "November":
                    i = 11;
                    break;
                default:
                    i = 12;
                    break;
            }
            return i;
        }
        //Find Julian Date
        private int JulianDate(int d, int m, int y)
        {
            int mm, yy;// month and year
            int k1, k2, k3;// constansts
            int j;//julian date

            yy = y - (int)((12 - m) / 10);
            mm = m + 9;
            if (mm >= 12)
            {
                mm = mm - 12;
            }
            k1 = (int)(365.25 * (yy + 4712));
            k2 = (int)(30.6001 * mm + 0.5);
            k3 = (int)((int)((yy / 100) + 49) * 0.75) - 38;
            // 'j' for dates in Julian calendar:
            j = k1 + k2 + d + 59;
            if (j > 2299160)
            {
                // For Gregorian calendar:
                j = j - k3;  // 'j' is the Julian date at 12h UT (Universal Time)
            }
            return j;
        }
        //Find Moon Age
        public double MoonAge(int d, int m, int y)
        {
            int j = JulianDate(d, m, y);
            //Calculate the approximate phase of the moon
            ip = (j + 4.867) / 29.53059;
            ip = ip - Math.Floor(ip);
            //After several trials I've seen to add the following lines, 
            //which gave the result was not bad
            if (ip < 0.5)
                ag = ip * 29.53059 + 29.53059 / 2;
            else
                ag = ip * 29.53059 - 29.53059 / 2;
            // Moon's age in days
            ag = Math.Floor(ag) + 1;
            return ag;
        }
        public void PrintAge()
        {
            string theAge = "";

            theAge = theAge + ag.ToString();

            if (ag == 1)
                theAge = theAge + " " + "day";
            else
                theAge = theAge + " " + "days";

            this.lblAge.Text = theAge;
        }
        public void ClearDraw()
        {
            if (pb11.Image != null)
            {
                pb11.Image = null;
            }
        }
        //Draw Moon
        private void DrawMoon()
        {
            int Xpos, Ypos, Rpos;
            int Xpos1, Xpos2;
            double Phase;

            Phase = ip;

            // Width of 'ImageToDraw' Object = Width of 'PicMoon' control
            int PageWidth = pb11.Width;
            // Height of 'ImageToDraw' Object = Height of 'PicMoon' control
            int PageHeight = pb11.Height;
            // Initiate 'ImageToDraw' Object with size = size of control 'PicMoon' control
            Bitmap ImageToDraw = new Bitmap(PageWidth, PageHeight);
            // Create graphics object for alteration.
            Graphics newGraphics = Graphics.FromImage(ImageToDraw);

            Pen PenB = new Pen(Color.Black); // For darkness part of the moon
            Pen PenW = new Pen(Color.White); // For the lighted part of the moon

            for (Ypos = 0; Ypos <= 45; Ypos++)
            {
                Xpos = (int)(Math.Sqrt(45 * 45 - Ypos * Ypos));
                // Draw darkness part of the moon
                Point pB1 = new Point(90 - Xpos, Ypos + 90);
                Point pB2 = new Point(Xpos + 90, Ypos + 90);
                Point pB3 = new Point(90 - Xpos, 90 - Ypos);
                Point pB4 = new Point(Xpos + 90, 90 - Ypos);
                newGraphics.DrawLine(PenB, pB1, pB2);
                newGraphics.DrawLine(PenB, pB3, pB4);
                // Determine the edges of the lighted part of the moon
                Rpos = 2 * Xpos;
                if (Phase < 0.5)
                {
                    Xpos1 = -Xpos;
                    Xpos2 = (int)(Rpos - 2 * Phase * Rpos - Xpos);
                }
                else
                {
                    Xpos1 = Xpos;
                    Xpos2 = (int)(Xpos - 2 * Phase * Rpos + Rpos);
                }
                // Draw the lighted part of the moon
                Point pW1 = new Point(Xpos1 + 90, 90 - Ypos);
                Point pW2 = new Point(Xpos2 + 90, 90 - Ypos);
                Point pW3 = new Point(Xpos1 + 90, Ypos + 90);
                Point pW4 = new Point(Xpos2 + 90, Ypos + 90);
                newGraphics.DrawLine(PenW, pW1, pW2);
                newGraphics.DrawLine(PenW, pW3, pW4);
            }
            // Display the bitmap in the picture box.
            pb11.Image = ImageToDraw;
            // Release graphics object
            PenB.Dispose();
            PenW.Dispose();
            newGraphics.Dispose();
            ImageToDraw = null;
        }
        private void YourChoice()
        {
            //user select date from MonthCalendar control
            int Aday, Amonth, Ayear;
            //Aday = this.MyCalendar.SelectionStart.Day;
            //Amonth = this.MyCalendar.SelectionStart.Month;
            //Ayear = this.MyCalendar.SelectionStart.Year;
            Aday = DateTime.Now.Day;
            Amonth = DateTime.Now.Month;
            Ayear = DateTime.Now.Year;
            this.MoonAge(Aday, Amonth, Ayear);
        }
        //Show Moon
        public void ShowMoon()
        {
            //draw moon and print age in selected days
            this.YourChoice(); //select date
            //this.ClearDraw(); //clear PicMoon PictureBox
            //this.DrawMoon(); //draw the moon
            this.PrintAge(); //print age of moon in days
        }
        //Load Images
        private void LoadImages()
        {
            //Aday = this.MyCalendar.SelectionStart.Day;
            //Amonth = this.MyCalendar.SelectionStart.Month;
            //Ayear = this.MyCalendar.SelectionStart.Year;
            Aday = 1;
            Amonth = Findmonth(cmbMonth.SelectedItem.ToString());
            Ayear = int.Parse(cmbYear.SelectedItem.ToString());

            int daysinMonth = DateTime.DaysInMonth(Ayear, Amonth);
            DateTime dt = new DateTime(Ayear, Amonth, Aday);
            string firstdayofMonth = dt.DayOfWeek.ToString();

            ArrayList k = new ArrayList();
            for (int i = 0; i < daysinMonth; i++)
            {
                k.Insert(i, int.Parse(MoonAge(i + 1, Amonth, Ayear).ToString()));
            }
            ArrayList k1 = new ArrayList();
            for (int i = 0; i < daysinMonth; i++)
            {
                k1.Insert(i, i + 1);
            }

            if (firstdayofMonth == "Sunday")
            {
                //Load image from p1 to days of month
                if (daysinMonth == 28)
                {
                    //Load image from p1 to p28
                    Load28();
                }
                else if (daysinMonth == 29)
                {
                    //Load image from p1 to p29
                    Load29();
                }
                else if (daysinMonth == 30)
                {
                    //Load image from p1 to p30
                    Load30();
                }
                else if (daysinMonth == 31)
                {
                    //Load image from p1 to p31
                    Load31();
                }
                p.InsertRange(0, k);
                d.InsertRange(0, k1);
            }
            else if (firstdayofMonth == "Monday")
            {
                //Load image from p2 to days of month
                if (daysinMonth == 28)
                {
                    //Load image from p2 to p29
                    Load28();
                }
                else if (daysinMonth == 29)
                {
                    //Load image from p2 to p30
                    Load29();
                }
                else if (daysinMonth == 30)
                {
                    //Load image from p2 to p31
                    Load30();
                }
                else if (daysinMonth == 31)
                {
                    //Load image from p2 to p32
                    Load31();
                }
                p.InsertRange(1, k);
                d.InsertRange(1, k1);
            }
            else if (firstdayofMonth == "Tuesday")
            {
                //Load image from p3 to days of month
                if (daysinMonth == 28)
                {
                    //Load image from p3 to p30
                    Load28();
                }
                else if (daysinMonth == 29)
                {
                    //Load image from p3 to p31
                    Load29();
                }
                else if (daysinMonth == 30)
                {
                    //Load image from p3 to p32
                    Load30();
                }
                else if (daysinMonth == 31)
                {
                    //Load image from p3 to p33
                    Load31();
                }
                p.InsertRange(2, k);
                d.InsertRange(2, k1);
            }
            else if (firstdayofMonth == "Wednesday")
            {
                //Load image from p4 to days of month
                if (daysinMonth == 28)
                {
                    //Load image from p4 to p31
                    Load28();
                }
                else if (daysinMonth == 29)
                {
                    //Load image from p4 to p32
                    Load29();
                }
                else if (daysinMonth == 30)
                {
                    //Load image from p4 to p33
                    Load30();
                }
                else if (daysinMonth == 31)
                {
                    //Load image from p4 to p34
                    Load31();
                }
                p.InsertRange(3, k);
                d.InsertRange(3, k1);
            }
            else if (firstdayofMonth == "Thursday")
            {
                //Load image from p5 to days of month
                if (daysinMonth == 28)
                {
                    //Load image from p5 to p32
                    Load28();
                }
                else if (daysinMonth == 29)
                {
                    //Load image from p5 to p33
                    Load29();
                }
                else if (daysinMonth == 30)
                {
                    //Load image from p5 to p34
                    Load30();
                }
                else if (daysinMonth == 31)
                {
                    //Load image from p5 to p35
                    Load31();
                }
                p.InsertRange(4, k);
                d.InsertRange(4, k1);
            }
            else if (firstdayofMonth == "Friday")
            {
                //Load image from p6 to days of month
                if (daysinMonth == 28)
                {
                    //Load image from p6 to p33
                    Load28();
                }
                else if (daysinMonth == 29)
                {
                    //Load image from p6 to p34
                    Load29();
                }
                else if (daysinMonth == 30)
                {
                    //Load image from p6 to p35
                    Load30();
                }
                else if (daysinMonth == 31)
                {
                    //Load image from p6 to p36
                    Load31();
                }
                p.InsertRange(5, k);
                d.InsertRange(5, k1);
            }
            else if (firstdayofMonth == "Saturday")
            {
                //Load image from p7 to days of month
                if (daysinMonth == 28)
                {
                    //Load image from p7 to p34
                    Load28();
                }
                else if (daysinMonth == 29)
                {
                    //Load image from p7 to p35
                    Load29();
                }
                else if (daysinMonth == 30)
                {
                    //Load image from p7 to p36
                    Load30();
                }
                else if (daysinMonth == 31)
                {
                    //Load image from p7 to p37
                    Load31();
                }
                p.InsertRange(6, k);
                d.InsertRange(6, k1);
            }
            object date;
            //pb1.Image = Image.FromFile(path + p[0].ToString() + ".jpg");    //1st
            lblM1.Text = d[0].ToString();
            //MessageBox.Show(d[0].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM1.Text != "")
            {
                date = d[0].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM1.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb2.Image = Image.FromFile(path + p[1].ToString() + ".jpg");    //2nd
            lblM2.Text = d[1].ToString();
            //MessageBox.Show(d[1].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM2.Text != "")
            {
                date = d[1].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM2.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb3.Image = Image.FromFile(path + p[2].ToString() + ".jpg");    //3rd
            lblM3.Text = d[2].ToString();
            //MessageBox.Show(d[2].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM3.Text != "")
            {
                date = d[2].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM3.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb4.Image = Image.FromFile(path + p[3].ToString() + ".jpg");    //4th
            lblM4.Text = d[3].ToString();
            //MessageBox.Show(d[3].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM4.Text != "")
            {
                date = d[3].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM4.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb5.Image = Image.FromFile(path + p[4].ToString() + ".jpg");    //5th
            lblM5.Text = d[4].ToString();
            //MessageBox.Show(d[4].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM5.Text != "")
            {
                date = d[4].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM5.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb6.Image = Image.FromFile(path + p[5].ToString() + ".jpg");    //6th
            lblM6.Text = d[5].ToString();
            //MessageBox.Show(d[5].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM6.Text != "")
            {
                date = d[5].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM6.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb7.Image = Image.FromFile(path + p[6].ToString() + ".jpg");    //7th
            lblM7.Text = d[6].ToString();
            //MessageBox.Show(d[6].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM7.Text != "")
            {
                date = d[6].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM7.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb8.Image = Image.FromFile(path + p[7].ToString() + ".jpg");    //8th
            lblM8.Text = d[7].ToString();
            //MessageBox.Show(d[7].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM8.Text != "")
            {
                date = d[7].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM8.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb9.Image = Image.FromFile(path + p[8].ToString() + ".jpg");    //9th
            lblM9.Text = d[8].ToString();
            //MessageBox.Show(d[8].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM9.Text != "")
            {
                date = d[8].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM9.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb10.Image = Image.FromFile(path + p[9].ToString() + ".jpg");    //10th
            lblM10.Text = d[9].ToString();
            //MessageBox.Show(d[9].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM10.Text != "")
            {
                date = d[9].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM10.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb11.Image = Image.FromFile(path + p[10].ToString() + ".jpg");    //11th
            lblM11.Text = d[10].ToString();
            //MessageBox.Show(d[10].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM11.Text != "")
            {
                date = d[10].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM11.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb12.Image = Image.FromFile(path + p[11].ToString() + ".jpg");    //12th
            lblM12.Text = d[11].ToString();
            //MessageBox.Show(d[11].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM12.Text != "")
            {
                date = d[11].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM12.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb13.Image = Image.FromFile(path + p[12].ToString() + ".jpg");    //13th
            lblM13.Text = d[12].ToString();
            //MessageBox.Show(d[12].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM13.Text != "")
            {
                date = d[12].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM13.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb14.Image = Image.FromFile(path + p[13].ToString() + ".jpg");    //14th
            lblM14.Text = d[13].ToString();
            //MessageBox.Show(d[13].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM14.Text != "")
            {
                date = d[13].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM14.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb15.Image = Image.FromFile(path + p[14].ToString() + ".jpg");    //15th
            lblM15.Text = d[14].ToString();
            //MessageBox.Show(d[14].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM15.Text != "")
            {
                date = d[14].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM15.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb16.Image = Image.FromFile(path + p[15].ToString() + ".jpg");     //16th
            lblM16.Text = d[15].ToString();
            //MessageBox.Show(d[15].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM16.Text != "")
            {
                date = d[15].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM16.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb17.Image = Image.FromFile(path + p[16].ToString() + ".jpg");    //17th
            lblM17.Text = d[16].ToString();
            //MessageBox.Show(d[16].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM17.Text != "")
            {
                date = d[16].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM17.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb18.Image = Image.FromFile(path + p[17].ToString() + ".jpg");    //18th
            lblM18.Text = d[17].ToString();
            //MessageBox.Show(d[17].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM18.Text != "")
            {
                date = d[17].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM18.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb19.Image = Image.FromFile(path + p[18].ToString() + ".jpg");    //19th
            lblM19.Text = d[18].ToString();
            //MessageBox.Show(d[18].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM19.Text != "")
            {
                date = d[18].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM19.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb20.Image = Image.FromFile(path + p[19].ToString() + ".jpg");    //20th
            lblM20.Text = d[19].ToString();
            //MessageBox.Show(d[19].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM20.Text != "")
            {
                date = d[19].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM20.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb21.Image = Image.FromFile(path + p[20].ToString() + ".jpg");    //21st
            lblM21.Text = d[20].ToString();
            //MessageBox.Show(d[20].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM21.Text != "")
            {
                date = d[20].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM21.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb22.Image = Image.FromFile(path + p[21].ToString() + ".jpg");    //22nd
            lblM22.Text = d[21].ToString();
            //MessageBox.Show(d[21].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM22.Text != "")
            {
                date = d[21].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM22.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb23.Image = Image.FromFile(path + p[22].ToString() + ".jpg");    //23rd
            lblM23.Text = d[22].ToString();
            //MessageBox.Show(d[22].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM23.Text != "")
            {
                date = d[22].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM23.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb24.Image = Image.FromFile(path + p[23].ToString() + ".jpg");    //24th
            lblM24.Text = d[23].ToString();
            //MessageBox.Show(d[23].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM24.Text != "")
            {
                date = d[23].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM24.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb25.Image = Image.FromFile(path + p[24].ToString() + ".jpg");    //25th
            lblM25.Text = d[24].ToString();
            //MessageBox.Show(d[24].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM25.Text != "")
            {
                date = d[24].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM25.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb26.Image = Image.FromFile(path + p[25].ToString() + ".jpg");    //26th
            lblM26.Text = d[25].ToString();
            //MessageBox.Show(d[25].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM26.Text != "")
            {
                date = d[25].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM26.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb27.Image = Image.FromFile(path + p[26].ToString() + ".jpg");    //27th
            lblM27.Text = d[26].ToString();
            //MessageBox.Show(d[26].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM27.Text != "")
            {
                date = d[26].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM27.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb28.Image = Image.FromFile(path + p[27].ToString() + ".jpg");    //28th
            lblM28.Text = d[27].ToString();
            //MessageBox.Show(d[27].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM28.Text != "")
            {
                date = d[27].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM28.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb29.Image = Image.FromFile(path + p[28].ToString() + ".jpg");    //29th
            lblM29.Text = d[28].ToString();
            //MessageBox.Show(d[28].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM29.Text != "")
            {
                date = d[28].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM29.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb30.Image = Image.FromFile(path + p[29].ToString() + ".jpg");    //30th
            lblM30.Text = d[29].ToString();
            //MessageBox.Show(d[29].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM30.Text != "")
            {
                date = d[29].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM30.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb31.Image = Image.FromFile(path + p[30].ToString() + ".jpg");    //31st
            lblM31.Text = d[30].ToString();
            //MessageBox.Show(d[30].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM31.Text != "")
            {
                date = d[30].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM31.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb32.Image = Image.FromFile(path + p[31].ToString() + ".jpg");
            lblM32.Text = d[31].ToString();
            //MessageBox.Show(d[31].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM32.Text != "")
            {
                date = d[31].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM32.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb33.Image = Image.FromFile(path + p[32].ToString() + ".jpg");
            lblM33.Text = d[32].ToString();
            //MessageBox.Show(d[32].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM33.Text != "")
            {
                date = d[32].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM33.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb34.Image = Image.FromFile(path + p[33].ToString() + ".jpg");
            lblM34.Text = d[33].ToString();
            //MessageBox.Show(d[33].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM34.Text != "")
            {
                date = d[33].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM34.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb35.Image = Image.FromFile(path + p[34].ToString() + ".jpg");
            lblM35.Text = d[34].ToString();
            //MessageBox.Show(d[34].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM35.Text != "")
            {
                date = d[34].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM35.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb36.Image = Image.FromFile(path + p[35].ToString() + ".jpg");
            lblM36.Text = d[35].ToString();
            //MessageBox.Show(d[35].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM36.Text != "")
            {
                date = d[35].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM36.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb37.Image = Image.FromFile(path + p[36].ToString() + ".jpg");
            lblM37.Text = d[36].ToString();
            //MessageBox.Show(d[36].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM37.Text != "")
            {
                date = d[36].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM37.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb38.Image = Image.FromFile(path + p[37].ToString() + ".jpg");
            lblM38.Text = d[37].ToString();
            //MessageBox.Show(d[37].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM38.Text != "")
            {
                date = d[37].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM38.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb39.Image = Image.FromFile(path + p[38].ToString() + ".jpg");
            lblM39.Text = d[38].ToString();
            //MessageBox.Show(d[38].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM39.Text != "")
            {
                date = d[38].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM39.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb40.Image = Image.FromFile(path + p[39].ToString() + ".jpg");
            lblM40.Text = d[39].ToString();
            //MessageBox.Show(d[39].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM40.Text != "")
            {
                date = d[39].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM40.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb41.Image = Image.FromFile(path + p[40].ToString() + ".jpg");
            lblM41.Text = d[40].ToString();
            //MessageBox.Show(d[40].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM41.Text != "")
            {
                date = d[40].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM41.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //pb42.Image = Image.FromFile(path + p[41].ToString() + ".jpg");
            lblM42.Text = d[41].ToString();
            //MessageBox.Show(d[41].ToString() +"-"+ Amonth +"-"+ Ayear);
            if (lblM42.Text != "")
            {
                date = d[41].ToString() + "-" + GetMonthString(Amonth) + "-" + Ayear;
                lblM42.Text += Environment.NewLine + Getdatafromdatabase(date);
            }

            //Conditions for changing the window size
            if (p[28].ToString() == "0")
            {
                //Change Size the window to 498,426
                this.ClientSize = new System.Drawing.Size(895, 383);
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            }
            else if (p[35].ToString() == "0")
            {
                //Change Size the window to 498,490
                this.ClientSize = new System.Drawing.Size(895, 456);
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            }
            else
            {
                //Change Size the window to 498,560
                this.ClientSize = new System.Drawing.Size(895, 520);
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            }
            //lbl1.Text = (int.Parse(lblM1.Text.TrimEnd('-')) + int.Parse(lblM2.Text.TrimEnd('-')) + int.Parse(lblM3.Text.TrimEnd('-')) + int.Parse(lblM4.Text.TrimEnd('-')) + int.Parse(lblM5.Text.TrimEnd('-')) + int.Parse(lblM6.Text.TrimEnd('-')) + int.Parse(lblM7.Text.TrimEnd('-'))).ToString();
            //lbl2.Text = (int.Parse(lblM8.Text.Substring(0,9).Trim()) + int.Parse(lblM9.Text.Substring(0,9).Trim()) + int.Parse(lblM10.Text.Substring(0,9).Trim()) + int.Parse(lblM11.Text.Substring(0,9).Trim()) + int.Parse(lblM12.Text.Substring(0,9).Trim()) + int.Parse(lblM13.Text.Substring(0,9).Trim()) + int.Parse(lblM14.Text.Substring(0,9).Trim())).ToString();
            //lbl3.Text = (int.Parse(lblM15.Text.Substring(0,9).Trim()) + int.Parse(lblM16.Text.Substring(0,9).Trim()) + int.Parse(lblM17.Text.Substring(0,9).Trim()) + int.Parse(lblM18.Text.Substring(0,9).Trim()) + int.Parse(lblM19.Text.Substring(0,9).Trim()) + int.Parse(lblM20.Text.Substring(0,9).Trim()) + int.Parse(lblM21.Text.Substring(0,9).Trim())).ToString();
            //lbl4.Text = (int.Parse(lblM22.Text.Substring(0,9).Trim()) + int.Parse(lblM23.Text.Substring(0,9).Trim()) + int.Parse(lblM24.Text.Substring(0,9).Trim()) + int.Parse(lblM25.Text.Substring(0,9).Trim()) + int.Parse(lblM26.Text.Substring(0,9).Trim()) + int.Parse(lblM27.Text.Substring(0,9).Trim()) + int.Parse(lblM28.Text.Substring(0,9).Trim())).ToString();
            //lbl5.Text = (int.Parse(lblM29.Text.Substring(0,9).Trim()) + int.Parse(lblM30.Text.Substring(0,9).Trim()) + int.Parse(lblM31.Text.Substring(0,9).Trim()) + int.Parse(lblM32.Text.Substring(0,9).Trim()) + int.Parse(lblM33.Text.Substring(0,9).Trim()) + int.Parse(lblM34.Text.Substring(0,9).Trim()) + int.Parse(lblM35.Text.Substring(0,9).Trim())).ToString();
            //lbl6.Text = (int.Parse(lblM36.Text.Substring(0,9).Trim()) + int.Parse(lblM37.Text.Substring(0,9).Trim()) + int.Parse(lblM38.Text.Substring(0,9).Trim()) + int.Parse(lblM39.Text.Substring(0,9).Trim()) + int.Parse(lblM40.Text.Substring(0,9).Trim()) + int.Parse(lblM41.Text.Substring(0,9).Trim()) + int.Parse(lblM42.Text.Substring(0,9).Trim())).ToString();

        }
        private string GetMonthString(int Amonth)
        {
            string mth = "";
            switch (Amonth)
            {
                case 1:
                    mth = "Jan";
                    break;
                case 2:
                    mth = "Feb";
                    break;
                case 3:
                    mth = "Mar";
                    break;
                case 4:
                    mth = "Apr";
                    break;
                case 5:
                    mth = "May";
                    break;
                case 6:
                    mth = "Jun";
                    break;
                case 7:
                    mth = "Jul";
                    break;
                case 8:
                    mth = "Aug";
                    break;
                case 9:
                    mth = "Sep";
                    break;
                case 10:
                    mth = "Oct";
                    break;
                case 11:
                    mth = "Nov";
                    break;
                case 12:
                    mth = "Dec";
                    break;
            }
            return mth;
        }
        private string Getdatafromdatabase(object date)
        {
            ExpenseAccess expenseaccess = new ExpenseAccess();
            DataRow dr = expenseaccess.GetAllExpensebyDate(date, date);
            int total = 0;
            int item = 0;
            if (dr != null)
            {
                for (int i = 0; i < dr.Table.Rows.Count; i++)
                {
                    item = int.Parse(dr.Table.Rows[i]["Price"].ToString());
                    total = total + item;
                }
            }
            else { }
            if (total != 0)
            {
                return "Spent :- " + total.ToString();
            }
            else { return ""; }
        }
        private void Load31()
        {
            for (int i = 0; i < (42 - 31); i++)
            {
                p.Insert(i, 0);
                d.Insert(i, "");
            }
        }
        private void Load30()
        {
            for (int i = 0; i < (42 - 30); i++)
            {
                p.Insert(i, 0);
                d.Insert(i, "");
            }
        }
        private void Load29()
        {
            for (int i = 0; i < (42 - 29); i++)
            {
                p.Insert(i, 0);
                d.Insert(i, "");
            }
        }
        private void Load28()
        {
            for (int i = 0; i < (42 - 28); i++)
            {
                p.Insert(i, 0);
                d.Insert(i, "");
            }
        }
        private void frmMoon_Load(object sender, System.EventArgs e)
        {
            this.ShowMoon();
            this.LoadYear();
        }
        //Load Year
        private void LoadYear()
        {
            int present = DateTime.Today.Year;
            int past = present - 1;
            int future = present + 10;

            for (int i = past; i < future; i++)
            {
                cmbYear.Items.Add(i);
            }
        }
        //Check
        private void Checking()
        {
            if (cmbMonth.SelectedIndex == -1 || cmbYear.SelectedIndex == -1)
            {
                btnGet.Enabled = false;
            }
            else { btnGet.Enabled = true; }
        }
        private void MyCalendar_DateChanged(object sender, System.Windows.Forms.DateRangeEventArgs e)
        {
            this.ShowMoon();
        }
        private void btnToDay_Click(object sender, System.EventArgs e)
        {
            //set the date of today
            //this.MyCalendar.SetDate(this.MyCalendar.TodayDate.Date);
        }
        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Checking();
            btnGet.PerformClick();
        }
        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Checking();
            btnGet.PerformClick();
        }
        private void MonthlyReports_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void txtFixedMonthlyExpenditure_Click(object sender, EventArgs e)
        {
            FixedExpenditure fixedexpenditure = new FixedExpenditure();
            fixedexpenditure.expenditureUpdated += new FixedExpenditure.ExpenditureUpdateHandler(FixedMonthlyExpenditure_Click);
            fixedexpenditure.Show(GetMonthString(Amonth), Ayear);
            fixedexpenditure.Show();
            //this.ShowInTaskbar = false;
        }
        private void FixedMonthlyExpenditure_Click(object sender, ExpenditureUpdateEventArgs e)
        {
            // update the forms values from the event args
            txtFixedMonthlyExpenditure.Text = e.Fixedexpenditure.ToString();
            GetMonthlyDetails(Amonth, Ayear);
            this.Show();
            //this.ShowInTaskbar = true;
        }
    }
}
