namespace Database.Models;

public class FilterOrderModel {
    public DateTime? StartDate {get; set;}
    public DateTime? EndDate {get; set;}

    public String? Adres {get; set;}

    public String? Customer {get; set;}

    public String? Furniture {get; set;}

    public decimal? StartCost {get; set;}

    public decimal? EndCost {get; set;}
}