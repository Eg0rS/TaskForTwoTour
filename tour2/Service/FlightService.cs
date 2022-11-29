using DataBase;
using Models.Model;

namespace tour2.Service;

public class FlightService
{
    private readonly DbConnection _dbConnection;

    public FlightService(DbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public IEnumerable<SortieModel> GetAllFlights()
    {
        return _dbConnection.Sortie;
    }

    public  void AddFlight(SortieModel sortie)
    {
        _dbConnection.AddSortie(sortie);
    }

    public void DeleteFlight(int id)
    {
        _dbConnection.RemoveSortie(id);
    }
}