using DataBase;
using Models.Model;

namespace tour2.Service;

public class DroneService
{
    private readonly DbConnection _dbConnection;
    public DroneService(DbConnection dbConnection)
    {
       _dbConnection = dbConnection;
    }

    public IEnumerable<DroneModel> GetAllDrones()
    {
        var drones = _dbConnection.Drones;
        return drones;
    }
    
    public void AddDrone(DroneModel drone)
    {
        _dbConnection.AddDrone(drone);
    }
    
    public void UpdateDrone(DroneModel drone)
    {
        _dbConnection.UpdateDrone(drone);
    }
    
    public void DeleteDrone(int id)
    {
        _dbConnection.DeleteDrone(id);
    }
    
}