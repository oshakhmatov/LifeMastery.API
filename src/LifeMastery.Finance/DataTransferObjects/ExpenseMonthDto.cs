namespace LifeMastery.Finance.DataTransferObjects;

public class ExpenseMonthDto
{
    public string Name { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is ExpenseMonthDto expenseMonthDto)
        {
            return expenseMonthDto.Year == Year && expenseMonthDto.Month == Month;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Month.GetHashCode() ^ Year.GetHashCode();
    }
}