namespace tour2.Model;

public class DroneModel
{
    public int Id   { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public string Engine { get; set; }
    public double MaxSpeed { get; set; }
    public double MaxFlightTime { get; set; }
    public double MaxFlightDistance { get; set; }
    public double ConsumptionFuel { get; set; }
    public double Weight { get; set; }
}