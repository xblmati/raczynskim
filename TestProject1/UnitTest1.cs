using ConsoleApp1;

namespace TestProject1;

public class Tests
{
    private StringCalculator calculator;
    
    [SetUp]
    public void Setup()
    {
        calculator = new StringCalculator();
    }

    [Test]
    public void Test1()
    {
        var emptyStr = "";
        Assert.That(calculator.Calculate(emptyStr), Is.Zero);
    }

    [Test]
    public void Test2()
    {
        foreach (var number in ExampleNumbers)
        {
            var numStr = number.ToString();
            Assert.That(calculator.Calculate(numStr), Is.EqualTo(number));
        }
    }

    [Test]
    public void Test3()
    {
        foreach (var a in ExampleNumbers)
        {
            foreach (var b in ExampleNumbers)
            {
                var sum = a + b;
                var str = $"{a},{b}";
                Assert.That(calculator.Calculate(str), Is.EqualTo(sum));
            }
        }
    }

    [Test]
    public void Test4()
    {
        foreach (var a in ExampleNumbers)
        foreach (var b in ExampleNumbers)
        {
            var sum = a + b;
            var str = $"{a}\n{b}";
            Assert.That(calculator.Calculate(str), Is.EqualTo(sum));
        }
    }
    [Test]
    public void Test5()
    {
        foreach (var a in ExampleNumbers)
        foreach (var b in ExampleNumbers)
        foreach (var c in ExampleNumbers)
        {
            var sum = a + b + c;
            var str1 = $"{a},{b},{c}";
            Assert.That(calculator.Calculate(str1), Is.EqualTo(sum));
            var str2 = $"{a}\n{b}\n{c}";
            Assert.That(calculator.Calculate(str2), Is.EqualTo(sum));
            var str3 = $"{a},{b}\n{c}";
            Assert.That(calculator.Calculate(str3), Is.EqualTo(sum));
            var str4 = $"{a}\n{b},{c}";
            Assert.That(calculator.Calculate(str4), Is.EqualTo(sum));
        }
    }

    [Test]
    public void Test6()
    {
        foreach (var a in ExampleNumbers.Where(it => it != 0))
        {
            var minusA = -a;
            Assert.Throws<FormatException>(() => calculator.Calculate(minusA.ToString()));
        }
    }

    //7. numbers greater than 1000 ignored
    [Test]
    public void Test7()
    {
        foreach (var a in ExampleNumbers.Concat(BigNumbers))
        foreach (var b in ExampleNumbers.Concat(BigNumbers))
        foreach (var c in ExampleNumbers.Concat(BigNumbers))
        {
            var sum = 
                (a > 1000 ? 0 : a) +
                (b > 1000 ? 0 : b) +
                (c > 1000 ? 0 : c);
            var str1 = $"{a},{b},{c}";
            Assert.That(calculator.Calculate(str1), Is.EqualTo(sum));
            var str2 = $"{a}\n{b}\n{c}";
            Assert.That(calculator.Calculate(str2), Is.EqualTo(sum));
            var str3 = $"{a},{b}\n{c}";
            Assert.That(calculator.Calculate(str3), Is.EqualTo(sum));
            var str4 = $"{a}\n{b},{c}";
            Assert.That(calculator.Calculate(str4), Is.EqualTo(sum));
        }
    }

    //8. a single char delimiter on the first line (e.g. //# for # delimiter)
    [Test]
    public void Test8()
    {
        foreach (var a in ExampleNumbers)
        foreach (var b in ExampleNumbers)
        {
            var sum = a + b;
            var str = $"//#\n{a}#{b}";
            Assert.That(calculator.Calculate(str), Is.EqualTo(sum));
            var str2 = $"//$\n{a}${b}";
            Assert.That(calculator.Calculate(str2), Is.EqualTo(sum));
        }
    }
    
    // 9. A multi char delimiter can be defined on the first line (e.g. //[###] for ‘###’ as the delimiter)
    [Test]
    public void Test9()
    {
        foreach (var a in ExampleNumbers)
        foreach (var b in ExampleNumbers)
        {
            var sum = a + b;
            var delimiter = "###";
            var str = $"//[{delimiter}]\n{a}{delimiter}{b}";
            Assert.That(calculator.Calculate(str), Is.EqualTo(sum));
            delimiter = "@$@";
            var str2 = $"//[{delimiter}]\n{a}{delimiter}{b}";
            Assert.That(calculator.Calculate(str2), Is.EqualTo(sum));
        }
    }
    
    // 10. Many single or multi-char delimiters can be defined (each wrapped in square brackets)
    [Test]
    public void Test10()
    {
        foreach (var a in ExampleNumbers)
        foreach (var b in ExampleNumbers)
        {
            var sum = a + b + a + b;
            var delimiter1 = "###";
            var delimiter2 = "@$@";
            var delimiter3 = '%';
            var str = $"//[{delimiter1}][{delimiter2}][{delimiter3}]\n{a}{delimiter1}{b}{delimiter3}{a}{delimiter2}{b}";
            Assert.That(calculator.Calculate(str), Is.EqualTo(sum));
        }
    }

    private static readonly int[] ExampleNumbers = [0, 2, 5, 42, 100, 999];
    private static readonly int[] BigNumbers = [1025, 42100, 9999999];
}