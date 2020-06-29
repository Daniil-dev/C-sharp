using System;
using System.Windows;
using System.Windows.Controls;
using WindowsFormsApp1;
namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Calculator calc;
        public MainWindow()
        {
            InitializeComponent();
            calc = new Calculator();
            calc.DidUpdateValue += Calc_DidUpdateValue;
        }

        private void Calc_DidUpdateValue(Calculator sender, double value, int precision)
        {
            output.Text = value.ToString();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            int digit = -1;



            if (int.TryParse(button.Content.ToString(), out digit))
            {
                calc.AddDigit(digit);
            }
            else
            {
                switch (button.Content)
                {
                    case "/":
                        calc.AddOperation(CalculatorOperation.Div);
                        break;
                    case "*":
                        calc.AddOperation(CalculatorOperation.Mul);
                        break;
                    case "-":
                        calc.AddOperation(CalculatorOperation.Sub);
                        break;
                    case "+":
                        calc.AddOperation(CalculatorOperation.Add);
                        break;
                    case "C":
                        calc.Clear();
                        break;
                    case "CE":
                        calc.clearAll();
                        break;
                    case "=":
                        calc.Compute();
                        break;
                    case ".":
                        calc.AddPoint();
                        break;
                    case "+/-":
                        calc.TransformInput(CalculatorTransformation.Negate);
                        break;
                    case "%":
                        calc.TransformInput(CalculatorTransformation.Percent);
                        break;
                    case "sqr":
                        calc.TransformInput(CalculatorTransformation.Sqr);
                        break;
                    case "sqrt":
                        calc.TransformInput(CalculatorTransformation.Sqrt);
                        break;
                    case "1/x":
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
