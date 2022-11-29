using Models.Model;

namespace tour2.Service;

public class AnalyticsService
{
    private readonly DroneService _droneService;
    private readonly FlightService _flightService;

    public AnalyticsService(DroneService droneService, FlightService flightService)
    {
        _droneService = droneService;
        _flightService = flightService;
    }

    private double GetProductivityForOneDrone(DroneModel model)
    {
        var flights = _flightService.GetAllFlights();
        var drone = _droneService.GetAllDrones().FirstOrDefault(x => x.Id == model.Id);

        var flightForOneDrone = flights.Where(f => f.DroneId == drone.Id).ToList();

        var flightTime = flightForOneDrone.Sum(f => f.FlightTime);
        var flightDistance = flightForOneDrone.Average(f => f.Distance);
        var flightHeight = flightForOneDrone.Average(f => f.Height);
        var flightMission = flightForOneDrone.Count(f => f.MissionCompleted == 1);
        var fightConsumptionFuel = flightForOneDrone.Sum(f => f.ConsumptionFuel);
        var isShotSown = flightForOneDrone.Any(f => f.IsShotSown == true);

        double productivity = ((flightTime / 60) * (flightDistance / 15) * (flightHeight / 20) * (flightMission * 5)) / (fightConsumptionFuel / 10) *
                              (isShotSown ? 1 : 0.5);
        return productivity;
    }

    public List<DroneModel> GetDronesWithProductivity(bool acsending)
    {
        var drones = _droneService.GetAllDrones();
        var dronesWithProductivity = drones
            .Select(d => new
            {
                Drone = d,
                Productivity = GetProductivityForOneDrone(d)
            })
            .OrderByDescending(d => d.Productivity)
            .Select(d => d.Drone)
            .ToList();

        if (!acsending)
        {
            dronesWithProductivity.Reverse();
        }

        return dronesWithProductivity;
    }

    public List<DroneModel> GetDronesWithProductivityByModel(bool acsending, string model)
    {
        var drones = _droneService.GetAllDrones();
        var dronesWithProductivity = drones
            .Where(d => d.Model == model)
            .Select(d => new
            {
                Drone = d,
                Productivity = GetProductivityForOneDrone(d)
            })
            .OrderByDescending(d => d.Productivity)
            .Select(d => d.Drone)
            .ToList();

        if (!acsending)
        {
            dronesWithProductivity.Reverse();
        }

        return dronesWithProductivity;
    }

    public List<DroneModel> GetDronesWithConsumption(bool acsending)
    {
        var drones = _droneService.GetAllDrones();
        var dronesWithConsumption  = drones
            .Select(d => new
            {
                Drone = d,
                Consumption = _flightService.GetAllFlights().Where(f => f.DroneId == d.Id).Sum(f => f.ConsumptionFuel)
            })
            .OrderByDescending(d => d.Consumption)
            .Select(d => d.Drone)
            .ToList();

        if (!acsending)
        {
            dronesWithConsumption.Reverse();
        }

        return dronesWithConsumption;
    }


    public List<DroneModel> GetDronesWithConsumptionByModel(bool b, string s)
    {
        var drones = _droneService.GetAllDrones();
        var dronesWithConsumption = drones
            .Where(d => d.Model == s)
            .Select(d => new
            {
                Drone = d,
                Consumption = _flightService.GetAllFlights().Where(f => f.DroneId == d.Id).Sum(f => f.ConsumptionFuel)
            })
            .OrderByDescending(d => d.Consumption)
            .Select(d => d.Drone)
            .ToList();

        if (!b)
        {
            dronesWithConsumption.Reverse();
        }

        return dronesWithConsumption;
    }

    public List<DroneModel> GetDronesWithCondition(bool b)
    {
        var drones = _droneService.GetAllDrones();
        var dronesWithCondition = drones
            .Select(d => new
            {
                Drone = d,
                Condition = _flightService.GetAllFlights().Where(f => f.DroneId == d.Id).Average(f => f.Distance/f.FlightTime)
            })
            .OrderByDescending(d => d.Condition)
            .Select(d => d.Drone)
            .ToList();

        if (!b)
        {
            dronesWithCondition.Reverse();
        }

        return dronesWithCondition;
    }
}