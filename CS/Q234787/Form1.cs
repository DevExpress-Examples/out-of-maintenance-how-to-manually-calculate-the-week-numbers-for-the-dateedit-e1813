using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Q234787 {
    public partial class Form1 : XtraForm {
        public Form1() {
            InitializeComponent();
        }  

        private void dateEdit1_Properties_CustomWeekNumber(object sender, DevExpress.XtraEditors.Repository.CustomWeekNumberEventArgs e) {
            DateTime date = e.Date;
            DateTime baseDate = date.Month < 10 ? new DateTime(date.Year - 1, 10, 1) : new DateTime(date.Year, 10, 1);
            e.WeekNumber = (int)Math.Floor((double)(new DateTime(date.Ticks - baseDate.Ticks).DayOfYear / 7));
        }
    }
}