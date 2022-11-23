using System.Data.SQLite;

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
            $"[ConsumptionFuel] real, " +
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
}