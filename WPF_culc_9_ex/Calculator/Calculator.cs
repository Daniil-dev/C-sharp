using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    delegate void CaclulatorUpdateOutput(Calculator sender, double value, int precision);
    delegate void CaclulatorInernalError(Calculator sender, string message);
    enum CalculatorOperation { Add, Sub, Mul, Div}
    enum CalculatorTransformation { Negate, Percent, Sqr, Sqrt, Inverse }
    class Calculator
    {
        double? input = null;
        double? result = null;

        CalculatorOperation? op = null;

        bool hasPoint;
        int fractionDigits = 0;

        public event CaclulatorUpdateOutput DidUpdateValue;
        public event CaclulatorInernalError InputError;
        public event CaclulatorInernalError ComputationError;

        public void AddDigit(int digit)
        {
            if (hasPoint == false)
            {
                if (input.HasValue && Math.Log10(input.Value) > 9)
                {
                    InputError?.Invoke(this, "Value is too long for this calculator!");
                    return;
                }

                input = (input ?? 0) * 10 + digit;                
            }
            else
            {
                if (fractionDigits > 8)
                {
                    InputError?.Invoke(this, "Value is too long for this calculator!");
                    return;
                }
                fractionDigits++;
                input = (input ?? 0) + digit * Math.Pow(10, -fractionDigits);
            }
            DidUpdateValue?.Invoke(this, input.Value, fractionDigits);
        }

        public void RemoveDigit()
        {
            if (input == null || input.ToString().Length == 0)
                return;
            if (!hasPoint)
            {
                long i = (long)input / 10;
                input = i;
            }
            else 
            {
                if (input.ToString().Substring(input.ToString().Length) == ",")
                {
                    hasPoint = false;
                }
                String s = input.ToString().Remove(input.ToString().Length - 1, 1);
                try
                {
                    input = Double.Parse(s);
                }
                catch
                {
                    input = 0;
                }
                fractionDigits--;
            }
            DidUpdateValue?.Invoke(this, input.Value, fractionDigits);
        }

        public void AddPoint()
        {
            hasPoint = true;
        }

        public void TransformInput(CalculatorTransformation t)
        {
            input = input ?? 0;
            if (input == null)
                return;

            switch (t)
            {
                case CalculatorTransformation.Negate:
                    input = -input;
                    break;
                case CalculatorTransformation.Percent:
                    if (input.HasValue && input != 0)
                    {
                        input = result * (input / 100);
                        try
                        {
                            DidUpdateValue?.Invoke(this, result.Value, 0);
                        }
                        catch (Exception) { }
                    }
                    break;
                case CalculatorTransformation.Sqr:
                    input = Math.Pow(input.Value, 2);
                    try
                    {
                        DidUpdateValue?.Invoke(this, result.Value, 0);
                    }
                    catch (Exception) { }
                    break;
                case CalculatorTransformation.Sqrt:
                    input = Math.Sqrt(input.Value);
                    try
                    {
                        DidUpdateValue?.Invoke(this, result.Value, 0);
                    }
                    catch (Exception) { }
                    break;
                case CalculatorTransformation.Inverse:
                    if (input.HasValue && input != 0)
                    {
                        input = 1 / input;
                        try
                        {
                            DidUpdateValue?.Invoke(this, result.Value, 0);
                        }
                        catch (Exception) { }
                    }
                    break;
            }
            try
            {
                DidUpdateValue?.Invoke(this, input.Value, fractionDigits);
            }
            catch (Exception) { }
            
        }

        public void AddOperation(CalculatorOperation op)
        {
            if (this.op.HasValue)
                Compute();

            this.op = op;
            if (input.HasValue)
            {
                result = input;
                this.Clear();
            }
        }

        public void Compute()
        {
            switch(this.op)
            {
                case CalculatorOperation.Add:
                    result = result + input;
                    try
                    {
                        DidUpdateValue?.Invoke(this, result.Value, 0);
                        ResetInput();
                    }
                    catch (Exception) { }
                    break;
                case CalculatorOperation.Sub:
                    result -= input;
                    try
                    {
                        DidUpdateValue?.Invoke(this, result.Value, 0);
                        ResetInput();
                    }
                    catch (Exception) { }
                    break;
                case CalculatorOperation.Mul:
                    result *= input;
                    try
                    {
                        DidUpdateValue?.Invoke(this, result.Value, 0);
                        ResetInput();
                    } 
                    catch (Exception) { }
                    break;
                case CalculatorOperation.Div:
                    if (input.HasValue && input.Value != 0)
                    {
                        result = result / input;
                        try
                        {
                            DidUpdateValue?.Invoke(this, result.Value, 0);
                            ResetInput();
                        }
                        catch (Exception) { }
                    }
                    else
                        ComputationError?.Invoke(this, "Division by Zero");
                    break;


            }
        }

        void ResetInput()
        {
            input = null;
            fractionDigits = 0;
            hasPoint = false;
        }

        public void Clear()
        {
            ResetInput();
            DidUpdateValue?.Invoke(this, 0, 0);
        }

        public void clearAll()
        {
            Clear();
            op = null;
        }
    }
}
