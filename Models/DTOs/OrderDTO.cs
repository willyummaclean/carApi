namespace CarbuilderAPI.Models.DTOs;

public class OrderDTO
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public int TechnologyId { get; set; }
    public int PaintId { get; set; }
    public int InteriorId { get; set; }
    public int WheelId { get; set; }
    public TechnologyDTO? Technology { get; set; }
    public PaintDTO? Paint { get; set; }
    public InteriorDTO? Interior { get; set; }
    public WheelsDTO? Wheel { get; set; }
    public decimal TotalCost
    {
        get
        {
            return Technology.Price + Interior.Price + Wheel.Price + Paint.Price;
        }
    }

    public DateTime Completed { get; set; }
    // public bool Fulfilled { get; set; }
    public bool Fulfilled
    {
        get
        {
            if (Completed > Timestamp)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
