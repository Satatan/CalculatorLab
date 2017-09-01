using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPE200Lab1
{
    public partial class MainForm : Form
    {
        private bool containsDot;
        private bool isAllowBack;
        private bool isAfterOperater;
        private bool isAfterEqual;
        private bool isfirstOperater;
        private string firstOperand;
        private string operate;
        private CalculatorEngine engine;
        private string memory = "0";
        private bool isPersen;

        private void resetAll()
        {
            lblDisplay.Text = "0";
            isAllowBack = true;
            containsDot = false;
            isAfterOperater = false;
            isAfterEqual = false;
            isfirstOperater = false;
            isPersen = false;
        }

        public MainForm()
        {
            InitializeComponent();
            engine = new CalculatorEngine();
            resetAll();
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (isAfterOperater)
            {
                lblDisplay.Text = "0";
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            isAllowBack = true;
            string digit = ((Button)sender).Text;
            if (lblDisplay.Text is "0")
            {
                lblDisplay.Text = "";
            }
            lblDisplay.Text += digit;
            isAfterOperater = false;
        }

        private void btnOperator_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterOperater)
            {
                return;
            }
           
            if (((Button)sender).Text == "%")
                {
                    lblDisplay.Text =  (Convert.ToDouble(firstOperand) * Convert.ToDouble(lblDisplay.Text) / 100).ToString();
                    isPersen = true;

            }
            if (isfirstOperater)
            {
                if (lblDisplay.Text is "Error")
                {
                    return;
                }
                string secondOperand = lblDisplay.Text;
                
                string result = engine.calculate(operate, firstOperand, secondOperand);
                if (result is "E" || result.Length > 8)
                {
                    lblDisplay.Text = "Error";
                }
                else 
                {
                    if (!isPersen)
                    {
                        lblDisplay.Text = result;
                    }
                }
            }

            if (((Button)sender).Text == "%")
            {

            }
            else
            {
                operate = ((Button)sender).Text;

                switch (operate)
                {
                    case "+":
                    case "-":
                    case "X":
                    case "÷":
                        firstOperand = lblDisplay.Text;
                        isAfterOperater = true;
                        break;
                }
            }
            isAllowBack = false;
            isfirstOperater = true;
            containsDot = false;
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
                isPersen = false;          
                if (lblDisplay.Text is "Error")
                {
                    return;
                }
                string secondOperand = lblDisplay.Text;
                string result = engine.calculate(operate, firstOperand, secondOperand);
                if (result is "E" || result.Length > 8)
                {
                    lblDisplay.Text = "Error";
                }
                else
                {
                    lblDisplay.Text = result;
                }
            isfirstOperater = false;          
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (!containsDot)
            {
                lblDisplay.Text += ".";
                containsDot = true;
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            // already contain negative sign
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (lblDisplay.Text[0] is '-')
            {
                lblDisplay.Text = lblDisplay.Text.Substring(1, lblDisplay.Text.Length - 1);
            }
            else
            {
                lblDisplay.Text = "-" + lblDisplay.Text;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            resetAll();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                return;
            }
            if (!isAllowBack)
            {
                return;
            }
            if (lblDisplay.Text != "0")
            {
                string current = lblDisplay.Text;
                char rightMost = current[current.Length - 1];
                if (rightMost is '.')
                {
                    containsDot = false;
                }
                lblDisplay.Text = current.Substring(0, current.Length - 1);
                if (lblDisplay.Text is "" || lblDisplay.Text is "-")
                {
                    lblDisplay.Text = "0";
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void lblDisplay_Click(object sender, EventArgs e)
        {

        }

        private void backSection_Click(object sender, EventArgs e)
        {
            
            double result;
            string[] parts;
            int remainLength;
            if (lblDisplay.Text == "0")
            {
                lblDisplay.Text = "Error";
            }
            else
            {
                result = (1 / Convert.ToDouble(lblDisplay.Text));
                // split between integer part and fractional part
                parts = result.ToString().Split('.');
                // if integer part length is already break max output, return error

                // calculate remaining space for fractional part.
                remainLength = 8 - parts[0].Length - 1;
                // trim the fractional part gracefully. =

                lblDisplay.Text = result.ToString("N" + remainLength);
                containsDot = false;
            }
        }

        private void sqr_Click(object sender, EventArgs e)
        {
            double result;
            string[] parts;
            int remainLength;

            result = Math.Sqrt( Convert.ToDouble(lblDisplay.Text) );
            // split between integer part and fractional part
            parts = result.ToString().Split('.');
            // if integer part length is already break max output, return error

            // calculate remaining space for fractional part.
            remainLength = 8 - parts[0].Length - 1;
            // trim the fractional part gracefully. =
            lblDisplay.Text = result.ToString("N" + remainLength);
            containsDot = false;
        }

        private void memory_Click(object sender, EventArgs e)
        {
            operate = ((Button)sender).Text;

            switch (operate)
            {
                case "MS":
                    memory = lblDisplay.Text;
                    break;
                case "MR":
                    lblDisplay.Text = memory;
                    break;
                case "MC":
                    memory = "0";
                    break;
                case "M+":
                    memory = (Convert.ToDouble(memory) + Convert.ToDouble(lblDisplay.Text)).ToString();
                    break;
                case "M-":
                    memory = (Convert.ToDouble(memory) - Convert.ToDouble(lblDisplay.Text)).ToString();
                    break;

            }
            
        }

        private void btnPercent_Click(object sender, EventArgs e)
        {

        }
    }
}