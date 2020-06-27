using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    delegate void CalculatorUpdateOutput(Calculator sender, double value, int precision);
    delegate void CalculatorInfernalError(Calculator sender, string message);
    enum CalculatorOperation { Pls, Min, Mul, Div}
    enum CalculatorTransformation { Negate, Percent, Sqr, Sqrt, Inverse }
    class Calculator
    {
        double? input = null;
        double? result = null;

        CalculatorOperation? op = null;

        bool hasPoint;
        int fractionDigits = 0;

        public event CalculatorUpdateOutput didUpdateValue;
        public event CalculatorInfernalError InputError;
        public event CalculatorInfernalError DivError;

        public void AddDigit(int digit) {
            if (hasPoint == false)
            {
                if (input.HasValue && Math.Log10(input.Value) > 9)
                {
                    InputError?.Invoke(this, "Too long value");
                    return;
                }
                input = (input ?? 0) * 10 + digit;
            }
            else
            {
                if (fractionDigits > 8)
                {
                    InputError?.Invoke(this, "Too long value");
                    return;
                }
                fractionDigits++;
                input = (input ?? 0) + digit * Math.Pow(10, -fractionDigits);
            }
            didUpdateValue?.Invoke(this, input.Value, fractionDigits);
        }

        public void RemoveDigit()
        {
            if (hasPoint == false)
            {
                long i = (long) input / 10;
                input = i;
            }
            didUpdateValue?.Invoke(this, input.Value, fractionDigits);
        }

        public void AddPoint()
        {
            hasPoint = true;
        }

        public void TransformInput(CalculatorTransformation t)
        {
            input = input ?? 0;
            switch (t)
            {
                case CalculatorTransformation.Negate:
                    input = -input;
                    break;
                case CalculatorTransformation.Percent:
                    if (input.HasValue && input != 0)
                    {
                        input = result * (input / 100);
                        didUpdateValue?.Invoke(this, input.Value, 0);
                    }
                    break;
                case CalculatorTransformation.Sqr:
                    input = Math.Pow(input.Value, 2);
                    didUpdateValue?.Invoke(this, input.Value, 0);
                    break;
                case CalculatorTransformation.Sqrt:
                    input = Math.Sqrt(input.Value);
                    didUpdateValue?.Invoke(this, input.Value, 0);
                    break;
                case CalculatorTransformation.Inverse:
                    input = 1 / input;
                    didUpdateValue?.Invoke(this, input.Value, 0);
                    break;
            }
            didUpdateValue?.Invoke(this, input.Value, fractionDigits);
        }

        public void AddOperation(CalculatorOperation op)
        {
            if (this.op.HasValue)
                Compute();
            this.op = op;
            if (input.HasValue)
            {
                result = input;
                this.clear();
            }
        }

        public void Compute()
        {
            switch(this.op)
            {
                case CalculatorOperation.Pls:
                    result += input;
                    didUpdateValue?.Invoke(this, result.Value, 0);
                    ResetInput();
                    break;
                case CalculatorOperation.Min:
                    result -= input;
                    didUpdateValue?.Invoke(this, result.Value, 0);
                    ResetInput();
                    break;
                case CalculatorOperation.Mul:
                    result *= input;
                    didUpdateValue?.Invoke(this, result.Value, 0);
                    ResetInput();
                    break;
                case CalculatorOperation.Div:
                    if (input.HasValue && input != 0)
                    {
                        result /= input;
                        didUpdateValue?.Invoke(this, result.Value, 0);
                        ResetInput();
                    }
                    else
                        DivError?.Invoke(this, "Division by 0");
                    break;
            }
        }

        void ResetInput()
        {
            input = null;
            fractionDigits = 0;
            hasPoint = false;
        }

        public void clear()
        {
            ResetInput();
            didUpdateValue?.Invoke(this, 0, 0);
        }

        public void clearAll()
        {
            clear();
            op = null;
        }
    }
}
