using System;
using System.Runtime.CompilerServices;

namespace DaySeven
{
    public static class Cogitator
    {
        public static bool PossibleToMatchTarget(long target, string[] equation, List<string> operators)
        {
            var operationConfigurations = GenerateEquations(equation, operators.ToArray());

            for (int i = 0; i < operationConfigurations.Count; i++)
            {
                if (EquationProcessor.ProcessEquation(operationConfigurations[i]) == target)
                {
                    Console.WriteLine(EquationProcessor.ProcessEquation(operationConfigurations[i]));
                    return true;
                }
            }

            return false;
        }

        public static List<string> GenerateEquations(string[] equationArr, string[] operators)
        {
            var equations = new List<string>();
            GenerateEquationsRecursive(equationArr, operators, 0, "", equations);
            return equations;
        }

        static void GenerateEquationsRecursive(string[] numbers, string[] operators, int index, string currentEquation, List<string> result)
        {
            if (index == numbers.Length - 1)
            {
                currentEquation += numbers[index];
                result.Add(currentEquation);
                return;
            }

            currentEquation += numbers[index];

            foreach (string op in operators)
            {
                GenerateEquationsRecursive(numbers, operators, index + 1, currentEquation + " " + op + " ", result);
            }
        }
    }
}
