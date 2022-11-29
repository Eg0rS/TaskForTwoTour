using DataBase;
using Models.Model;
using tour2.Service;

namespace tour2;

public class Program
{
    private static DbConnection _dbConnection;
    private static DroneService _droneService;
    private static FlightService _flightService;
    private static AnalyticsService _analyticsService;

    private static void Main(string[] args)
    {
        _dbConnection = new DbConnection();
        _droneService = new DroneService(_dbConnection);
        _flightService = new FlightService(_dbConnection);
        _analyticsService = new AnalyticsService(_droneService, _flightService);

        if (NotEnoughArguments(args, 1)) return;

        switch (args[0])
        {
            case "-help":
                Console.WriteLine("Программа для сбора аналитических данных по беспелотным летаельным аппаратам");
                break;
            case "-drone":
                if (NotEnoughArguments(args, 2, "-drone", GetHelp(new[] { "-drone" }))) return;
                switch (args[1])
                {
                    case "-add":
                        if (NotEnoughArguments(args, 8, "-drone -add", GetHelp(new[] { "-drone", "-add" }))) return;
                        var drone = new DroneModel
                        {
                            Name = args[2],
                            Model = args[3],
                            Engine = args[4],
                            MaxSpeed = int.Parse(args[5]),
                            MaxFlightTime = int.Parse(args[6]),
                            MaxFlightDistance = int.Parse(args[7]),
                            Weight = int.Parse(args[8])
                        };
                        _droneService.AddDrone(drone);
                        Console.WriteLine($"Добавлен новый дрон: {drone}");
                        break;
                    case "-list":
                        var allDrones = _droneService.GetAllDrones();
                        foreach (var d in allDrones)
                        {
                            Console.WriteLine(d);
                        }

                        break;
                    case "-delete":
                        if (NotEnoughArguments(args, 2, "-drone -delete", GetHelp(new[] { "-drone", "-delete" }))) return;
                        _droneService.DeleteDrone(int.Parse(args[2]));
                        Console.WriteLine($"Дрон с id {args[2]} удален");
                        break;
                    case "-edit":
                        if (NotEnoughArguments(args, 9, "-drone -edit", GetHelp(new[] { "-drone", "-edit" }))) return;
                        var droneEdit = new DroneModel
                        {
                            Id = int.Parse(args[2]),
                            Name = args[3],
                            Model = args[4],
                            Engine = args[5],
                            MaxSpeed = int.Parse(args[6]),
                            MaxFlightTime = int.Parse(args[7]),
                            MaxFlightDistance = int.Parse(args[8]),
                            Weight = int.Parse(args[9])
                        };
                        _droneService.UpdateDrone(droneEdit);
                        Console.WriteLine($"Дрон с id {args[2]} изменен");
                        break;
                    case "-help":
                        GetHelp(new[] { "-drone" });
                        break;
                    default:
                        Console.WriteLine($"Такого параметра {args[1]} нет");
                        GetHelp(new[] { "-drone" });
                        break;
                }

                break;
            case "-flight":
                if (NotEnoughArguments(args, 2, "-flight", GetHelp(new[] { "-flight" }))) return;

                switch (args[1])
                {
                    case "-add":
                        if (NotEnoughArguments(args, 8, "-flight -add", GetHelp(new[] { "-flight", "-add" }))) return;
                        var flight = new SortieModel
                        {
                            DroneId = int.Parse(args[2]),
                            FlightTime = int.Parse(args[3]),
                            Distance = int.Parse(args[4]),
                            Height = int.Parse(args[5]),
                            MissionCompleted = int.Parse(args[6]),
                            IsShotSown = int.Parse(args[7]) == 1,
                            ConsumptionFuel = int.Parse(args[8])
                        };
                        _flightService.AddFlight(flight);
                        Console.WriteLine($"Добавлен новый полет: {flight}");
                        break;
                    case "-list":
                        var allFlights = _flightService.GetAllFlights();
                        foreach (var f in allFlights)
                        {
                            Console.WriteLine(f);
                        }

                        break;
                    case "-delete":
                        if (NotEnoughArguments(args, 2, "-flight -delete", GetHelp(new[] { "-flight", "-delete" }))) return;
                        _flightService.DeleteFlight(int.Parse(args[2]));
                        Console.WriteLine($"Полет с id {args[2]} удален");
                        break;
                    case "-help":
                        GetHelp(new[] { "-flight" });
                        break;
                    default:
                        Console.WriteLine($"Такого параметра {args[1]} нет");
                        break;
                }

                break;
            case "-analitycs":
                if (NotEnoughArguments(args, 2, "-analitycs", GetHelp(new[] { "-analitycs" }))) return;

                switch (args[1])
                {
                    case "-get":
                        if (NotEnoughArguments(args, 3, "-get", GetHelp(new[] { "-analitycs", "-get" }))) return;
                        switch (args[2])
                        {
                            case "-productive":
                                if (NotEnoughArguments(args, 4, "-productive", GetHelp(new[] { "-analitycs", "-get", "-productive" }))) return;
                                List<DroneModel> productiveDrones = new List<DroneModel>();
                                switch (args[3])
                                {
                                    case "-desc":
                                        productiveDrones = _analyticsService.GetDronesWithProductivity(false);
                                        break;
                                    case "-asc":
                                        productiveDrones = _analyticsService.GetDronesWithProductivity(true);
                                        break;
                                    case "-model":
                                        if (NotEnoughArguments(args, 5, "-model", GetHelp(new[] { "-analitycs", "-get", "-productive" }))) return;
                                        switch (args[4])
                                        {
                                            case "-desc":
                                                productiveDrones = _analyticsService.GetDronesWithProductivityByModel(false, args[4]);
                                                break;
                                            case "-asc":
                                                productiveDrones = _analyticsService.GetDronesWithProductivityByModel(true, args[4]);
                                                break;
                                            default:
                                                Console.WriteLine($"Такого параметра {args[4]} нет");
                                                break;
                                        }

                                        break;
                                    default:
                                        Console.WriteLine($"Такого параметра {args[3]} нет");
                                        break;
                                }

                                foreach (var d in productiveDrones)
                                {
                                    Console.WriteLine(d);
                                }

                                break;
                            case "-consumption":
                                if (NotEnoughArguments(args, 4, "-consumption", GetHelp(new[] { "-analitycs", "-get", "-consumption" }))) return;
                                List<DroneModel> consumptionDrones = new List<DroneModel>();
                                switch (args[3])
                                {
                                    case "-desc":
                                        consumptionDrones = _analyticsService.GetDronesWithConsumption(false);
                                        break;
                                    case "-asc":
                                        consumptionDrones = _analyticsService.GetDronesWithConsumption(true);
                                        break;
                                    case "-model":
                                        if (NotEnoughArguments(args, 5, "-model", GetHelp(new[] { "-analitycs", "-get", "-consumption" }))) return;

                                        switch (args[4])
                                        {
                                            case "-desc":
                                                consumptionDrones = _analyticsService.GetDronesWithConsumptionByModel(false, args[4]);
                                                break;
                                            case "-asc":
                                                consumptionDrones = _analyticsService.GetDronesWithConsumptionByModel(true, args[4]);
                                                break;
                                            default:
                                                Console.WriteLine($"Такого параметра {args[4]} нет");
                                                break;
                                        }

                                        break;
                                    default:
                                        Console.WriteLine($"Такого параметра {args[3]} нет");
                                        break;
                                }

                                foreach (var d in consumptionDrones)
                                {
                                    Console.WriteLine(d);
                                }

                                break;
                            case "-condition":
                                if (NotEnoughArguments(args, 4, "-condition", GetHelp(new[] { "-analitycs", "-get", "-condition" }))) return;
                                List<DroneModel> conditionDrones = new List<DroneModel>();
                                switch (args[3])
                                {
                                    case "-desc":
                                        conditionDrones = _analyticsService.GetDronesWithCondition(false);
                                        break;
                                    case "-asc":
                                        conditionDrones = _analyticsService.GetDronesWithCondition(true);
                                        break;
                                    default:
                                        Console.WriteLine($"Такого параметра {args[3]} нет");
                                        break;
                                }

                                foreach (var d in conditionDrones)
                                {
                                    Console.WriteLine(d);
                                }

                                break;
                            default:
                                Console.WriteLine($"Такого параметра {args[2]} нет");
                                break;
                        }

                        break;
                    case "-help":
                        Console.WriteLine("Справка по аналитическим данным");
                        break;
                    default:
                        Console.WriteLine($"Такого параметра {args[1]} нет");
                        break;
                }

                break;

            default:
                Console.WriteLine($"Такого параметра {args[0]} нет");
                break;
        }
    }

    private static bool NotEnoughArguments(string[] args, int needArgsLength, string method = "", string help = "")
    {
        if (args.Length < needArgsLength)
        {
            Console.WriteLine($"Аргументы не заданы");
            if (method != "")
            {
                Console.WriteLine($"Аргументы для метода {method} не заданы");
                Console.WriteLine($"Для вызова справки по методу {method} введите -help");
            }

            if (help != "")
            {
                Console.WriteLine(help);
            }

            return true;
        }

        return false;
    }

    private static string GetHelp(string[] args)
    {
        switch (args[0])
        {
            case "-help":
                return $"Программа для сбора аналитических данных по беспелотным летаельным аппаратам \n имеет методы: \n" +
                       $"-help - Справка о програме \n" +
                       $"-drone - Методы радоты с беспилотниками \n" +
                       $"-analytics - Методы работы с аналитикой полетов \n" +
                       $"-flight - Методы работы с полетами бемпилотников";
            case "-drone":
                switch (args[1])
                {
                    case "-add":
                        return "Метод должен содержать параметры: \n" + "" +
                               "name - Название беспилотника \n" +
                               "model - Модель беспилотника \n" +
                               "engine - Тип двигателя беспилотника \n" +
                               "maxSpeed - Максимальная скорость беспилотника \n" +
                               "maxFlightTime - Максимальное время полета беспилотника \n" +
                               "maxFlightDistance - Максимальная дистанция полета беспилотника \n" +
                               "weight - Вес беспилотника";
                    case "-delete":
                        return "Метод должен содержать параметры: \n" + "" +
                               "id - идентефикатор беспилотника, можно получить с помощью метода -list \n" +
                               "model - Модель беспилотника";
                    case "-edit":
                        return "Метод должен содержать параметры: \n" + "" +
                               "id - идентефикатор беспилотника, можно получить с помощью метода -list \n" +
                               "name - Название беспилотника \n" +
                               "model - Модель беспилотника \n" +
                               "engine - Тип двигателя беспилотника \n" +
                               "maxSpeed - Максимальная скорость беспилотника \n" +
                               "maxFlightTime - Максимальное время полета беспилотника \n" +
                               "maxFlightDistance - Максимальная дистанция полета беспилотника \n" +
                               "weight - Вес беспилотника";
                    default: break;
                }

                return $"Методы работы с беспилотниками: \n" +
                       $"-help - Справка по методу \n" +
                       $"-add - Добавить беспилотник \n" +
                       $"-delete - Удалить беспилотник \n" +
                       $"-edit - Редактировать беспилотник \n" +
                       $"-list - Список беспилотников";
            case "-flight":
                switch (args[1])
                {
                    case "-add":
                        return "Метод должен содержать параметры: \n" + "" +
                               "droneId - Идентефикатор беспилотника, можно получить с помощью метода -list \n" +
                               "distance - Дистанция полета \n" +
                               "flightTime - Время полета \n" +
                               "height - Максимальная высота \n" +
                               "MissionCompleted - количество выполненных задач \n" +
                               "IsShotSown - был ли сбит беспилотник (0 - нет, 1 - да)" +
                               "ConsumptionFuel - Расход топлива";
                    case "-delete":
                        return "Метод должен содержать параметры: \n" + "" +
                               "id - идентефикатор полета, можно получить с помощью метода -list \n";
                    default: break;
                }

                return $"Методы работы с полетами беспилотников: \n" +
                       $"-help - Справка по методу \n" +
                       $"-add - Добавить полет \n" +
                       $"-delete - Удалить полет \n" +
                       $"-list - Список полетов";
            case "-analytics":
                switch (args[1])
                {
                    case "-get":
                        switch (args[2])
                        {
                            case "-productive":
                            case "-consumption":
                                return "Метод принимает параметры \n" +
                                       "-desc - выборка по убыванию \n" +
                                       "-asc - выборка по убыванию по возрастанию \n" +
                                       "-model - выборка по модели беспилотника \n" +
                                       "-model - так же имеет параметры \n" +
                                       "-desc - выборка по убыванию \n" +
                                       "-asc - выборка по убыванию по возрастанию \n";
                            case "-condition":
                                return "Метод принимает параметры \n" +
                                       "-desc - выборка по убыванию \n" +
                                       "-asc - выборка по убыванию по возрастанию \n";
                        }

                        break;
                }

                return $"Методы работы с аналитикой полетов: \n" +
                       $"-help - Справка по методу \n" +
                       $"-list - Список аналитических данных \n" +
                       $"-get -productive - Список самых продуктивных беспилотных летательных аппаратов" +
                       $"-get -consumption - Список самых экономных беспилотных летательных аппаратов" +
                       $"-get -condition - Список самых надежных беспилотных летательных аппаратов";
            default: break;
        }

        return "";
    }
}