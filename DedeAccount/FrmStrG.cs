using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DedeAccount
{
    public partial class FrmStrG : Form
    {
        public FrmStrG()
        {
            InitializeComponent();
        }
        string numStr = "1234567890";
        string uStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string lStr = "abcdefghijklmnopqrstuvwxyz";
        string tStr = "!@#$%^&*";


        private void btnG_Click(object sender, EventArgs e)
        {
            string zz = "";
            if (cbxUStr.Checked) {
                zz += uStr;
            }
            if (cbxLStr.Checked)
            {
                zz += lStr;
            }
            if (cbxTStr.Checked)
            {
                zz += tStr;
            }
            if (cbxNumStr.Checked)
            {
                zz += numStr;
            }
            Random r = new Random();
            //生成
            var len = int.Parse(txtStrLength.Text);
            string result = "";
            if (cbxFirstY.Checked)
            {
                var zz1 = "";
                if (cbxUStr.Checked)
                {
                    zz1 += uStr;
                }
                if (cbxLStr.Checked)
                {
                    zz1 += lStr;
                }
                result = zz1[r.Next(zz1.Length)].ToString();
                for (int i = 1; i < len; i++)
                {
                    result += zz[r.Next(zz.Length)].ToString();
                }

            }
            else {
               
                for (int i = 0; i < len; i++)
                {
                    result += zz[r.Next(zz.Length)].ToString();
                }
            }
            txtContent.Text = result;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(txtContent.Text);
        }
    }
}
