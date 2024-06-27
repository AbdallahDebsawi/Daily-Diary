using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily_Diary
{
    public class DailyDiary
    {
        public string FilePath { get; set; }

        public DailyDiary(string filePath)
        {
            FilePath = filePath;
        }

        public List<Entry> ReadDiaryFile()
        {
            List<Entry> entries = new List<Entry>();
            try
            {
                if (!File.Exists(FilePath))
                {
                    Console.WriteLine("Diary file not found.");
                    return entries;
                }

                var lines = File.ReadAllLines(FilePath);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(lines[i])) continue; // Skip empty lines

                    DateTime date;
                    if (DateTime.TryParse(lines[i], out date) && i + 1 < lines.Length)
                    {
                        string content = lines[i + 1].Trim();
                        entries.Add(new Entry(date, content));
                        i++; // Skip the next line as it's already read as content
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Diary file not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading diary file: {ex.Message}");
            }

            return entries;
        }

        public void AddEntry(Entry entry)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(FilePath))
                {
                    sw.WriteLine(entry.Date.ToString("yyyy-MM-dd"));
                    sw.WriteLine(entry.Content);
                    sw.WriteLine(); // Add a blank line between entries
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding entry: {ex.Message}");
            }
        }

        public void DeleteEntry(DateTime date)
        {
            try
            {
                var entries = ReadDiaryFile();
                var filteredEntries = entries.Where(e => e.Date != date).ToList();

                using (StreamWriter sw = new StreamWriter(FilePath))
                {
                    foreach (var entry in filteredEntries)
                    {
                        sw.WriteLine(entry.Date.ToString("yyyy-MM-dd"));
                        sw.WriteLine(entry.Content);
                        sw.WriteLine(); // Add a blank line between entries
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting entry: {ex.Message}");
            }
        }

        public int CountEntries()
        {
            return ReadDiaryFile().Count;
        }

        public List<Entry> SearchEntries(DateTime date)
        {
            return ReadDiaryFile().Where(e => e.Date.Date == date.Date).ToList();
        }
    }
}
