using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BT1maytinh
{
    public partial class Calculator : Form
    {
        public Calculator()
        {
            InitializeComponent();
        }
        float data1, data2;
        string pheptinh;

        private void button15_Click(object sender, EventArgs e)
        {
            hienthi.Text = hienthi.Text + "1";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            hienthi.Text = hienthi.Text + "2";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            hienthi.Text = hienthi.Text + "3";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            hienthi.Text = hienthi.Text + "4";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            hienthi.Text = hienthi.Text + "5";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            hienthi.Text = hienthi.Text + "6";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hienthi.Text = hienthi.Text + "7";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hienthi.Text = hienthi.Text + "8";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            hienthi.Text = hienthi.Text + "9";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            hienthi.Clear();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            hienthi.Text = hienthi.Text + ".";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (hienthi.Text.Length > 0)
            {
                hienthi.Text = hienthi.Text.Substring(0, hienthi.Text.Length - 1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pheptinh = "chia";
            data1 = float.Parse(hienthi.Text);
            hienthi.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            pheptinh = "nhan";
            data1 = float.Parse(hienthi.Text);
            hienthi.Clear();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            pheptinh = "tru";
            data1 = float.Parse(hienthi.Text);
            hienthi.Clear();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            pheptinh = "cong";
            data1 = float.Parse(hienthi.Text);
            hienthi.Clear();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            pheptinh = "chiadu";
            data1 = float.Parse(hienthi.Text);
            hienthi.Clear();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (pheptinh == "cong")
            {
                data2 = data1 + float.Parse(hienthi.Text);
                hienthi.Text = data2.ToString();
            }
            if (pheptinh == "tru")
            {
                data2 = data1 - float.Parse(hienthi.Text);
                hienthi.Text = data2.ToString();
            }
            if (pheptinh == "nhan")
            {
                data2 = data1 * float.Parse(hienthi.Text);
                hienthi.Text = data2.ToString();
            }
            if (pheptinh == "chia")
            {
                data2 = data1 / float.Parse(hienthi.Text);
                hienthi.Text = data2.ToString();
            }
            if (pheptinh == "chiadu")
            {
                data2 = data1 % float.Parse(hienthi.Text);
                hienthi.Text = data2.ToString();
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            hienthi.Text = hienthi.Text + "0";
        }
    }
}
