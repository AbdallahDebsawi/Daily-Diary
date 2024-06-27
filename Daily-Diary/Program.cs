using System.Globalization;

namespace Daily_Diary
{
    public class Program
    {
        static void Main(string[] args)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "mydiary.txt");
            DailyDiary diary = new DailyDiary(filePath);
            bool running = true;

            while (running)
            {
                Console.WriteLine("1. Read diary");
                Console.WriteLine("2. Add entry");
                Console.WriteLine("3. Delete entry");
                Console.WriteLine("4. Count entries");
                Console.WriteLine("5. Search entries by date");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        var entries = diary.ReadDiaryFile();
                        if (entries.Count == 0)
                        {
                            Console.WriteLine("No entries found.");
                        }
                        else
                        {
                            foreach (var entry in entries)
                            {
                                Console.WriteLine($"{entry.Date:yyyy-MM-dd}");
                                Console.WriteLine(entry.Content);
                                Console.WriteLine();
                            }
                        }
                        break;
                    case "2":
                        Console.Write("Enter the date (YYYY-MM-DD): ");
                        DateTime date;
                        if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                        {
                            Console.Write("Enter the content: ");
                            string content = Console.ReadLine();
                            diary.AddEntry(new Entry(date, content));
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format.");
                        }
                        break;
                    case "3":
                        Console.Write("Enter the date (YYYY-MM-DD) of the entry to delete: ");
                        if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                        {
                            diary.DeleteEntry(date);
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format.");
                        }
                        break;
                    case "4":
                        Console.WriteLine($"Total number of entries: {diary.CountEntries()}");
                        break;
                    case "5":
                        Console.Write("Enter the date (YYYY-MM-DD) to search: ");
                        if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                        {
                            var searchResults = diary.SearchEntries(date);
                            if (searchResults.Count == 0)
                            {
                                Console.WriteLine("No entries found for the specified date.");
                            }
                            else
                            {
                                foreach (var entry in searchResults)
                                {
                                    Console.WriteLine($"{entry.Date:yyyy-MM-dd}");
                                    Console.WriteLine(entry.Content);
                                    Console.WriteLine();
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format.");
                        }
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}
