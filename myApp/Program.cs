using Bogus;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using myApp.Models;
using System.Diagnostics;

partial class Program
{
    private readonly static PtmkContext _context = new PtmkContext();
    private static string[] _gender = new string[]
    {
       " Мужской",
        "Женский"

    };
    static void Main(string[] args)
    {
   
        if (args.Length > 0)
        {
            switch (args[0])
            {
                case "1":

                    _context.Database.Migrate();

                    break;
                case "2":
                    if (!string.IsNullOrEmpty(args[1]) || !string.IsNullOrEmpty(args[2]) || !string.IsNullOrEmpty(args[3]))
                    {

                        _context.Users.Add(new User
                        {
                            Fio = args[1],
                            BirthDay = Convert.ToDateTime(args[2]),
                            Gender = args[3]
                        });
                        _context.SaveChanges();


                        Console.WriteLine("Данные добавлены");

                    }
                    break;
                case "3":

                    var users = _context.Users.Select(x => new
                    {
                        x.Fio,
                        Age = GetFullAge(x.BirthDay),
                        x.Gender,
                        x.BirthDay,

                    }).OrderBy(x => x.Fio).ToList().DistinctBy(x => x.Fio).DistinctBy(x => x.BirthDay);
                    foreach (var item in users)
                    {

                        Console.WriteLine($"ФИО - {item.Fio}");
                        Console.WriteLine($"Дата рождения - {item.BirthDay}");
                        Console.WriteLine($"Пол - {item.Gender}");
                        Console.WriteLine($"Возраст - {item.Age}");
                        Console.WriteLine();
                    }



                    break;

                case "4":
                    var faker = new Faker("ru");

                    for (int i = 1; i < 101; i++)
                    {
                        var user = new User
                        {
                            BirthDay = faker.Date.Between(DateTime.Now.AddYears(-100), DateTime.Now),
                            Gender = "Мужской",
                            Fio = $"F{faker.Name.LastName(Name.Gender.Male)} {faker.Name.FirstName(Name.Gender.Male)} {RandomPatronymic()}",
                        };
                        _context.Users.Add(user);

                    }
                    for (int i = 1; i <= 1000000; i++)
                    {
                        var user = new User
                        {
                            BirthDay = faker.Date.Between(DateTime.Now.AddYears(-100), DateTime.Now),
                            Gender = _gender[Random.Shared.Next(0, 1)],
                            Fio = $"{faker.Name.LastName()} {faker.Name.FirstName()} {RandomPatronymic()}",
                        };
                        _context.Users.Add(user);

                    }
                    _context.SaveChanges();
                    Console.WriteLine("Данные добавлены");
                    break;
                case "5":
                    var timer = new Stopwatch();
                    timer.Start();
                    var userswithfilters = _context.Users.Where(x => x.Gender == "Мужской" && x.Fio.StartsWith("F"));
                    timer.Stop();


                    string timetaken = "Время затрачено: " + timer.Elapsed.ToString(@"m\:ss\.fff");
                    Console.WriteLine(timetaken);
                    foreach (var item in userswithfilters)
                    {
                        Console.WriteLine($"ФИО - {item.Fio}");
                        Console.WriteLine($"Дата рождения - {item.BirthDay}");
                        Console.WriteLine($"Пол - {item.Gender}");
                        Console.WriteLine();
                    }
                    break;
            }
        }



    }

    private static int GetFullAge(DateTime birthDay)
    {
        var currentDate = DateTime.Today;
        var age = currentDate.Year - birthDay.Year;
        if (birthDay.Date > currentDate.AddYears(-age)) age--;
        return age;
    }

    public static string RandomPatronymic()
    {
        string[] patronymics = new string[]
        {
            "Алексеевич",
            "Александрович",
            "Андреевич",
            "Анатольевич",
            "Артёмович",
            "Афанасьевич",
            "Борисович",
            "Васильевич",
            "Владимирович",
            "Владиславович",
            "Григорьевич",
            "Евгеньевич",
            "Дмитриевич",
            "Константинович",
            "Иванович",
            "Максимович",
            "Петрович",
            "Сергеевич",
            "Юрьевич",
            "Романович",
            "Степанович",
            "Ярославович",
            "Сергеевич",
        };
        return patronymics[Random.Shared.Next(patronymics.Length)];
    }
    
}


