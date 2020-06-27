using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        Calculator calc;
        public Form1()
        {
            InitializeComponent();
            calc = new Calculator();
            calc.didUpdateValue += CalculatorDidUpdateValue;
            calc.InputError += CalculatorInputError;
            calc.DivError += CalculatorInputError;
            calc.clear();
        }

        private void CalculatorInputError(Calculator sender, string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void CalculatorDidUpdateValue(Calculator sender, double value, int precision)
        {
            if (precision > 0)
                label1.Text = value.ToString("F" + precision.ToString());
            else
                label1.Text = value.ToString();
        }

        private void button_Click(object sender, EventArgs e)
        {
            int digit = -1;
            Button b = sender as Button;
            if (int.TryParse(b.Text, out digit))
                calc.AddDigit(digit);
            else
            {
                switch (b.Tag)
                {
                    case "div":
                        calc.AddOperation(CalculatorOperation.Div);
                        break;
                    case "mul":
                        calc.AddOperation(CalculatorOperation.Mul);
                        break;
                    case "min":
                        calc.AddOperation(CalculatorOperation.Min);
                        break;
                    case "pls":
                        calc.AddOperation(CalculatorOperation.Pls);
                        break;
                    case "clear":
                        calc.clear();
                        break;
                    case "clearall":
                        calc.clearAll();
                        break;
                    case "compute":
                        calc.Compute();
                        break;
                    case "dot":
                        calc.AddPoint();
                        break;
                    case "negate":
                        calc.TransformInput(CalculatorTransformation.Negate);
                        break;
                    case "percent":
                        calc.TransformInput(CalculatorTransformation.Percent);
                        break;
                    case "sqr":
                        calc.TransformInput(CalculatorTransformation.Sqr);
                        break;
                    case "sqrt":
                        calc.TransformInput(CalculatorTransformation.Sqrt);
                        break;
                    case "inverse":
                        calc.TransformInput(CalculatorTransformation.Inverse);
                        break;
                    case "del":
                        calc.RemoveDigit();
                        break;
                }
            }
        }
    }
}
