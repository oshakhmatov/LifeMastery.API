namespace LifeMastery.Finance.DataTransferObjects;

public record ExpenseMonthDto(
    int Month, 
    int Year,
    string Name);