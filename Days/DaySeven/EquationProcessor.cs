namespace DaySeven
{
    public static class EquationProcessor
    {
        private static string _currentOp = string.Empty;
        private static string _firstNum = string.Empty;
        private static string _secondNum = string.Empty;
        private static string _equation = string.Empty;
        private static string[] _eq = Array.Empty<string>();
        public static long ProcessEquation(string equation)
        {
            _equation = equation;
            _eq = _equation.Split(' ');
            var sum = 0L;
            for (var i = 0; i < _eq.Length; i+=2)
            {
                if (i + 2 < _eq.Length)
                {
                    _currentOp = _eq[i + 1];
                    _firstNum = i == 0 ? _eq[i] : _firstNum;
                    _secondNum = _eq[i + 2];
                    sum = ExecuteOperation(i);
                }
            }
            return sum;
        }

        public static long ExecuteOperation(int index)
        {
            if (!long.TryParse(_firstNum, out var first) || !long.TryParse(_secondNum, out var second))
            {
                return 0;
            }
            switch ( _currentOp)
            {
                case "+":
                    var addition = first + second;
                    _firstNum = addition.ToString();
                    return addition;
                case "*":
                    var multiplication = first * second;
                    _firstNum = multiplication.ToString();
                    return multiplication;
                case "||":
                    var concatenation = first.ToString() + second.ToString();
                    _firstNum = concatenation;
                    return long.Parse(concatenation);
                default:
                    return 0;
            }
        }
    }
}
