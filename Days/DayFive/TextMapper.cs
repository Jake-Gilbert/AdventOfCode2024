using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayFive
{
    public static class TextMapper
    {
        public static Rules ConstructRules()
        {
            var rules = new Rules();
            try
            {
                string[] lines = File.ReadAllLines("TestData/ChallengeData.TXT");
                
                var rulesPair = new Dictionary<int, List<int>>();
                var newLineIndex = Array.FindIndex(lines, x => string.IsNullOrEmpty(x)); 
                for (int i = 0; i < newLineIndex; i += 1)
                {
                    if (i <= newLineIndex - 1)
                    {
                        var firstVal = int.Parse(lines[i][0].ToString() + lines[i][1].ToString());
                        var secondVal = int.Parse(lines[i][3].ToString() + lines[i][4].ToString());
                        if (rulesPair.ContainsKey(secondVal))
                        {
                            var list = rulesPair[secondVal];
                            list.Add(firstVal);
                        }
                        else
                        {
                            rulesPair.Add(secondVal, new List<int> { firstVal });
                        }
                    }
                }

                var instructions = new List<string>();
                for (int i = newLineIndex + 1; i < lines.Length; i++)
                {
                    instructions.Add(lines[i]);
                }
                rules.InstructionSets = rulesPair;
                rules.Instructions = instructions;
            }
            catch (Exception e)
            {
                Console.WriteLine($"File not found or something went wrong {e.Message}");
                throw new FileNotFoundException(e.Message);
            }
            return rules;
        }
    }
}
