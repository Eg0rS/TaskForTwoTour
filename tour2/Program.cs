using DataBase;

namespace tour2;

public class Program
{
    private static DbConnection _dbConnection;

    private static void Main(string[] args)
    {
        InitializeDatabase();
        InitializeRepository();
        InitializeService();
        
        if (NotEnoughArguments(args, 1)) return;
        switch (args[0])
        {
            case "-help":
                Console.WriteLine("Программа для сбора аналитических данных по беспелотным летаельным аппаратам");
                break;
            case "-drone":
                if (NotEnoughArguments(args, 2, "-drone", GetHelp("-drone"))) return;
                switch (args[1])
                {
                    case "-add":
                        Console.WriteLine("Добавление нового беспилотного летательного аппарата");
                        break;
                    case "-list":
                        Console.WriteLine("Список беспилотных летательных аппаратов");
                        break;
                    case "-delete":
                        Console.WriteLine("Удаление беспилотного летательного аппарата");
                        break;
                    case "-edit":
                        Console.WriteLine("Редактирование беспилотного летательного аппарата");
                        break;
                    case "-help":
                        Console.WriteLine("Справка по беспилотным летательным аппаратам");
                        break;
                    default:
                        Console.WriteLine($"Такого параметра {args[1]} нет");
                        break;
                }

                break;
            case "-flight":
                if (NotEnoughArguments(args, 2, "-flight", GetHelp("-flight"))) return;

                switch (args[1])
                {
                    case "-add":
                        Console.WriteLine("Добавление нового полета");
                        break;
                    case "-list":
                        Console.WriteLine("Список полетов");
                        break;
                    case "-delete":
                        Console.WriteLine("Удаление полета");
                        break;
                    case "-help":
                        Console.WriteLine("Справка по полетам");
                        break;
                    default:
                        Console.WriteLine($"Такого параметра {args[1]} нет");
                        break;
                }

                break;
            case "-analitycs":
                if (NotEnoughArguments(args, 2, "-analitycs", GetHelp("-analitycs"))) return;

                switch (args[1])
                {
                    case "-get":
                        if (NotEnoughArguments(args, 3, "-get", GetHelp("-get"))) return;

                        switch (args[2])
                        {
                            case "-productive":
                                if (NotEnoughArguments(args, 4, "-productive", GetHelp("-productive"))) return;

                                switch (args[3])
                                {
                                    case "-desc":
                                        Console.WriteLine("Список беспилотных летательных аппаратов по убыванию продуктивности");
                                        break;
                                    case "-asc":
                                        Console.WriteLine("Список беспилотных летательных аппаратов по возрастанию продуктивности");
                                        break;
                                    case "-model":
                                        if (NotEnoughArguments(args, 5, "-model", GetHelp("-model"))) return;

                                        switch (args[4])
                                        {
                                            case "-desc":
                                                Console.WriteLine("Список беспилотных летательных аппаратов по убыванию продуктивности по модели");
                                                break;
                                            case "-asc":
                                                Console.WriteLine("Список беспилотных летательных аппаратов по возрастанию продуктивности по модели");
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

                                Console.WriteLine("Список самых продуктивных беспилотных летательных аппаратов");
                                break;
                            case "-consumption":
                                if (NotEnoughArguments(args, 4, "-consumption", GetHelp("-consumption"))) return;

                                switch (args[3])
                                {
                                    case "-desc":
                                        Console.WriteLine("Список беспилотных летательных аппаратов по убыванию расхода топлива");
                                        break;
                                    case "-asc":
                                        Console.WriteLine("Список беспилотных летательных аппаратов по возрастанию расхода топлива");
                                        break;
                                    case "-model":
                                        if (NotEnoughArguments(args, 5, "-model", GetHelp("-model"))) return;

                                        switch (args[4])
                                        {
                                            case "-desc":
                                                Console.WriteLine("Список беспилотных летательных аппаратов по убыванию расхода по модели");
                                                break;
                                            case "-asc":
                                                Console.WriteLine("Список беспилотных летательных аппаратов по возрастанию расхода по модели");
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

                                Console.WriteLine("Список самых расходных беспилотных летательных аппаратов");
                                break;
                            case "-condition":
                                if (NotEnoughArguments(args, 4, "-condition", GetHelp("-condition"))) return;
                                switch (args[3])
                                {
                                    case "-desc":
                                        Console.WriteLine("Список беспилотных летательных аппаратов по убыванию состояния");
                                        break;
                                    case "-asc":
                                        Console.WriteLine("Список беспилотных летательных аппаратов по возрастанию состояния");
                                        break;
                                    default:
                                        Console.WriteLine($"Такого параметра {args[3]} нет");
                                        break;
                                }

                                Console.WriteLine("Список самых надежных беспилотных летательных аппаратов");
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

    private static void InitializeDatabase()
    {
        _dbConnection = new DbConnection();
    }

    private static void InitializeRepository()
    {
        
    }

    private static void InitializeService()
    {
        
    }

    private static string GetHelp(string method)
    {
        switch (method)
        {
            case "add":
                return "Добавление нового беспилотного летательного аппарата";
            case "delete":
                return "Удаление беспилотного летательного аппарата";
            default: break;
        }

        return "";
    }
}