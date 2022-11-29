using System.Data.SQLite;
using Models.Model;

namespace DataBase;

public class DbConnection
{
    private static DbConnection _instance = null;
    private const string ConnectionString = "Data Source=data.db;";

    public DbConnection()
    {
        if (!File.Exists("data.db"))
        {
            SQLiteConnection.CreateFile("data.db");
        }

        Connection = new SQLiteConnection(ConnectionString);
        var commandText1 =
            $"CREATE TABLE IF NOT EXISTS [Drone]" +
            $" ( [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
            $" [Name] TEXT, " +
            $"[Model] TEXT, " +
            $"[Engine] real, " +
            $"[MaxSpeed] real, " +
            $"[MaxFlightTime] real," +
            $" [MaxFlightDistance] real, " +
            $"[Weight] real)";
        var commandText2 =
            $"CREATE TABLE IF NOT EXISTS [Sortie]" +
            $"( [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
            $"[DroneId] INTEGER, " +
            $"[FlightTime] real, " +
            $"[Distance] real, " +
            $"[Height] real, " +
            $"[MissionCompleted] INTEGER," +
            $" [IsShotSown] INTEGER, " +
            $"[ConsumptionFuel] real) ";
        var command1 = new SQLiteCommand(commandText1, Connection);
        var command2 = new SQLiteCommand(commandText2, Connection);
        Connection.Open();
        command1.ExecuteNonQuery();
        command2.ExecuteNonQuery();
        Connection.Close();
    }

    public static DbConnection Instance
    {
        get { return _instance ??= new DbConnection(); }
    }

    public SQLiteConnection? Connection { get; } = null;

    public IEnumerable<DroneModel> Drones
    {
        get
        {
            var cmdText = "SELECT * FROM Drone";
            Connection?.Open();
            var command = new SQLiteCommand(cmdText, Connection);
            var reader = command.ExecuteReader();
            var drones = new List<DroneModel>();
            while (reader.Read())
            {
                var drone = new DroneModel
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Model = reader.GetString(2),
                    Engine = reader.GetString(3),
                    MaxSpeed = reader.GetDouble(4),
                    MaxFlightTime = reader.GetDouble(5),
                    MaxFlightDistance = reader.GetDouble(6),
                    Weight = reader.GetDouble(7)
                };
                drones.Add(drone);
            }

            Connection?.Close();
            return drones;
        }
    }

    public IEnumerable<SortieModel> Sortie
    {
        get
        {
            var cmdText = "SELECT * FROM Sortie";
            Connection?.Open();
            var command = new SQLiteCommand(cmdText, Connection);
            var reader = command.ExecuteReader();
            var sorties = new List<SortieModel>();
            while (reader.Read())
            {
                var sortie = new SortieModel
                {
                    Id = reader.GetInt32(0),
                    DroneId = reader.GetInt32(1),
                    FlightTime = reader.GetDouble(2),
                    Distance = reader.GetDouble(3),
                    Height = reader.GetDouble(4),
                    MissionCompleted = reader.GetInt32(5),
                    IsShotSown = reader.GetBoolean(6),
                    ConsumptionFuel = reader.GetDouble(7)
                };
                sorties.Add(sortie);
            }

            Connection?.Close();
            return sorties;
        }
    }

    public void AddDrone(DroneModel drone)
    {
        var cmdText = $"INSERT INTO Drone (Name, Model, Engine, MaxSpeed, MaxFlightTime, MaxFlightDistance, Weight) " +
                      $"VALUES ('{drone.Name}', '{drone.Model}', '{drone.Engine}', {drone.MaxSpeed}, {drone.MaxFlightTime}, {drone.MaxFlightDistance},  {drone.Weight})";
        Connection?.Open();
        var command = new SQLiteCommand(cmdText, Connection);
        command.ExecuteNonQuery();
        Connection?.Close();
    }

    public void UpdateDrone(DroneModel drone)
    {
        var cmdText =
            $"UPDATE Drone SET Name = '{drone.Name}', Model = '{drone.Model}', Engine = '{drone.Engine}', MaxSpeed = {drone.MaxSpeed}, MaxFlightTime = {drone.MaxFlightTime}, MaxFlightDistance = {drone.MaxFlightDistance}, Weight = {drone.Weight} WHERE Id = {drone.Id}";
        Connection?.Open();
        var command = new SQLiteCommand(cmdText, Connection);
        command.ExecuteNonQuery();
        Connection?.Close();
    }

    public void DeleteDrone(int id)
    {
        var cmdText = $"DELETE FROM Drone WHERE Id = {id}";
        Connection?.Open();
        var command = new SQLiteCommand(cmdText, Connection);
        command.ExecuteNonQuery();
        Connection?.Close();
    }

    public void AddSortie(SortieModel sortie)
    {
        var cmdText = $"INSERT INTO Sortie (DroneId, FlightTime, Distance, Height, MissionCompleted, IsShotSown, ConsumptionFuel) " +
                      $"VALUES ({sortie.DroneId}, {sortie.FlightTime}, {sortie.Distance}, {sortie.Height}, {sortie.MissionCompleted}, {sortie.IsShotSown}, {sortie.ConsumptionFuel})";
        Connection?.Open();
        var command = new SQLiteCommand(cmdText, Connection);
        command.ExecuteNonQuery();
        Connection?.Close();
    }

    public void RemoveSortie(int id)
    {
        var cmdText = $"DELETE FROM Sortie WHERE Id = {id}";
        Connection?.Open();
        var command = new SQLiteCommand(cmdText, Connection);
        command.ExecuteNonQuery();
        Connection?.Close();
    }
}