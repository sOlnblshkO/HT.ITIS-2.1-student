namespace Tests.RunLogic.Attributes;

[AttributeUsage(AttributeTargets.Assembly)]
public class HomeworkProgressAttribute : Attribute
{
    public int Number { get; }

    public HomeworkProgressAttribute(Homeworks homeworks) : this((int)homeworks) { }

    public HomeworkProgressAttribute(int number)
    {
        if (!Enum.IsDefined((Homeworks)number))
        {
            var enumElementsCount = Enum.GetNames(typeof(Homeworks)).Length - 1;
            throw new ArgumentOutOfRangeException(nameof(number), $"Number must be 0 <= number < {enumElementsCount}");
        }

        Number = number;
    }
}