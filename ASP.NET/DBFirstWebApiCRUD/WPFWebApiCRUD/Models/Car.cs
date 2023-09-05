namespace WPFWebApiCRUD.Models;

public class Car
{
    public int carId { get; set; }
    public int ownerId { get; set; }
    public string model { get; set; } = null!;
    public int maxSpeed { get; set; }
}