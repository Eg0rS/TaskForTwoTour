namespace Models.Model;

public class SortieModel
{
    public int Id { get; set; }
    public int DroneId { get; set; }
    public double FlightTime { get; set; }
    public double Distance { get; set; }
    public double Height { get; set; }
    public int MissionCompleted { get; set; }
    public bool IsShotSown {get; set; }
    public double ConsumptionFuel { get; set; }
}