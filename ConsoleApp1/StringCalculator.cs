namespace ConsoleApp1;

public class StringCalculator
{
    public int Calculate(string arg)
    {
        var delimiters = new List<string> { ",", "\n" };
        if (arg.StartsWith("//"))
        {
            var indexOfNewLine = arg.IndexOf('\n');
            var delimiterString = arg[2..indexOfNewLine];
            if (delimiterString.Length == 1)
                delimiters.Add(delimiterString);
            else
            {
                if (delimiterString[0] != '[' || delimiterString[^1] != ']') throw new FormatException();
                var delimiterSplit = delimiterString[1..^1].Split("][");
                delimiters.AddRange(delimiterSplit);
            }
            arg = arg[(indexOfNewLine + 1)..];
        }
        var split = arg.Split(delimiters.ToArray(), StringSplitOptions.None);
        return split.Sum(str => int.TryParse(str, out var result)
            ? result switch
            {
                < 0 => throw new FormatException(),
                > 1000 => 0,
                _ => result
            }
            : 0
        );
    }
}