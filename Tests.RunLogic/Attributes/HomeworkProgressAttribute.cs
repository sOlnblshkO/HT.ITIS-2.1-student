namespace Tests.RunLogic.Attributes;

[AttributeUsage(AttributeTargets.Assembly)]
public class HomeworkProgressAttribute : Attribute
{
    public int Number { get; }

    public HomeworkProgressAttribute(Homeworks homeworks) : this((int)homeworks) { }

    public HomeworkProgressAttribute(int number)
    {
        if (number is < 0 or > 12)
        {
            throw new ArgumentOutOfRangeException(nameof(number), "Number must be 0 <= number < 12");
        }

        Number = number;
    }
}