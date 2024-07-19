namespace CarbuilderAPI.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public int TechnologyId { get; set; }
    public int PaintId { get; set; }
    public int InteriorId { get; set; }
    public int WheelId { get; set; }
    public Technology? Technology { get; set; }
    public Paint? Paint { get; set; }
    public Interior? Interior { get; set; }
    public Wheels? Wheel { get; set; }
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

