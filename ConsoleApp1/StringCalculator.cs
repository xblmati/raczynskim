namespace ConsoleApp1;

public class StringCalculator
{
    public int Calculate(string arg)
    {
        var delimiters = new List<char> { ',', '\n' };
        if (arg.StartsWith("//"))
        {
            delimiters.Add(arg[2]);
            arg = arg[3..];
        }
        var split = arg.Split(delimiters.ToArray());
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