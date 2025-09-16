using System;
using System.Collections.Generic;
using System.Linq;

namespace FolhaDePonto
{
    public class Employee
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public double DailyHours { get; set; }
        public double WeekendHours { get; set; }
        public bool AlternateWeekends { get; set; }
        public string SaturdayPattern { get; set; }
        public bool HasTwoShifts { get; set; } = false;
    }

    public class TimeEntry
    {
        public DateTime Date { get; set; }
        public TimeSpan Entry1 { get; set; }
        public TimeSpan Exit1 { get; set; }
        public TimeSpan? Entry2 { get; set; }
        public TimeSpan? Exit2 { get; set; }
        public string Observation { get; set; }
    }

    public class Timesheet
    {
        private readonly Dictionary<string, List<TimeEntry>> _entries = new();

        public void AddEntry(string employee, TimeEntry entry)
        {
            if (!_entries.ContainsKey(employee))
            {
                _entries[employee] = new List<TimeEntry>();
            }
            _entries[employee].Add(entry);
        }

        public double CalculateWorkedHours(TimeEntry entry)
        {
            double total = 0.0;
            if (entry.Entry1 != default && entry.Exit1 != default)
            {
                total += (entry.Exit1 - entry.Entry1).TotalHours;
            }
            if (entry.Entry2.HasValue && entry.Exit2.HasValue)
            {
                total += (entry.Exit2.Value - entry.Entry2.Value).TotalHours;
            }
            return total;
        }

        public double GetTotalHours(string employee)
        {
            return _entries.TryGetValue(employee, out var list)
                ? list.Sum(CalculateWorkedHours)
                : 0.0;
        }
    }

    class Program
    {
        static void Main()
        {
            var employees = new Dictionary<string, Employee>
            {
                ["Jhaspion"] = new Employee { Name = "Jhaspion", Role = "Fixo", DailyHours = 8.5, WeekendHours = 7, AlternateWeekends = true, SaturdayPattern = "odd", HasTwoShifts = true },
                ["Mayane"] = new Employee { Name = "Mayane", Role = "Fixo", DailyHours = 8.5, WeekendHours = 7, AlternateWeekends = true, SaturdayPattern = "even", HasTwoShifts = true },
                ["Dayane"] = new Employee { Name = "Dayane", Role = "Flexível", DailyHours = 8, WeekendHours = 4, AlternateWeekends = false, HasTwoShifts = true },
                ["Leonardo"] = new Employee { Name = "Leonardo", Role = "Professor", DailyHours = 6, WeekendHours = 6, AlternateWeekends = false, HasTwoShifts = true },
                ["Rafael"] = new Employee { Name = "Rafael", Role = "Professor", DailyHours = 3, WeekendHours = 3, AlternateWeekends = false },
                ["Matheus"] = new Employee { Name = "Matheus", Role = "Professor", DailyHours = 2, WeekendHours = 0, AlternateWeekends = false },
                ["Luiz"] = new Employee { Name = "Luiz", Role = "Professor", DailyHours = 3, WeekendHours = 3, AlternateWeekends = false },
                ["Dediana"] = new Employee { Name = "Dediana", Role = "Professor", DailyHours = 3, WeekendHours = 3, AlternateWeekends = false }
            };

            var timesheet = new Timesheet();

            Console.WriteLine("Sistema de Folha de Ponto (simplificado)");
            Console.WriteLine("Digite 'l' para listar horas, 'a' para adicionar entrada, 'q' para sair.");

            while (true)
            {
                Console.Write("> ");
                var command = Console.ReadLine()?.Trim().ToLower();
                if (command == "q") break;
                if (command == "l")
                {
                    foreach (var kv in employees)
                    {
                        var total = timesheet.GetTotalHours(kv.Key);
                        Console.WriteLine($"{kv.Key} - {kv.Value.Role}: {total:F2} horas trabalhadas");
                    }
                }
                else if (command == "a")
                {
                    Console.Write("Nome do colaborador: ");
                    var name = Console.ReadLine()?.Trim();
                    if (!employees.ContainsKey(name))
                    {
                        Console.WriteLine("Colaborador não encontrado.");
                        continue;
                    }
                    Console.Write("Data (YYYY-MM-DD): ");
                    if (!DateTime.TryParse(Console.ReadLine(), out var date))
                    {
                        Console.WriteLine("Data inválida.");
                        continue;
                    }
                    Console.Write("Entrada 1 (HH:MM): ");
                    var e1 = TimeSpan.Parse(Console.ReadLine());
                    Console.Write("Saída 1 (HH:MM): ");
                    var s1 = TimeSpan.Parse(Console.ReadLine());
                    Console.Write("Entrada 2 (HH:MM ou vazio): ");
                    var e2Str = Console.ReadLine();
                    TimeSpan? e2 = string.IsNullOrWhiteSpace(e2Str) ? null : TimeSpan.Parse(e2Str);
                    Console.Write("Saída 2 (HH:MM ou vazio): ");
                    var s2Str = Console.ReadLine();
                    TimeSpan? s2 = string.IsNullOrWhiteSpace(s2Str) ? null : TimeSpan.Parse(s2Str);
                    Console.Write("Observação: ");
                    var obs = Console.ReadLine();

                    timesheet.AddEntry(name, new TimeEntry { Date = date, Entry1 = e1, Exit1 = s1, Entry2 = e2, Exit2 = s2, Observation = obs });
                    Console.WriteLine("Entrada adicionada!");
                }
            }

            Console.WriteLine("Encerrando...");
        }
    }
}
