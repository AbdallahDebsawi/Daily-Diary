using Daily_Diary;

namespace DiaryManagerTests
{
    public class UnitTest1
    {
        public string TestFilePath = "test_mydiary.txt";

        [Fact]
        public void ReadDiaryFile_ShouldReturnEntries()
        {
            // Arrange
            File.WriteAllText(TestFilePath, "2024-06-25\nTest entry 1\n\n2024-06-26\nTest entry 2\n");
            DailyDiary diary = new DailyDiary(TestFilePath);

            // Act
            var entries = diary.ReadDiaryFile();

            // Assert
            Assert.Equal(2, entries.Count);
        }

        [Fact]
        public void AddEntry_ShouldIncreaseEntryCount()
        {
            // Arrange
            File.WriteAllText(TestFilePath, "");
            DailyDiary diary = new DailyDiary(TestFilePath);
            var entry = new Entry(new DateTime(2024, 6, 25), "Test entry");

            // Act
            diary.AddEntry(entry);
            var entries = diary.ReadDiaryFile();

            // Assert
            Assert.Single(entries);
        }

        [Fact]
        public void DeleteEntry_ShouldRemoveSpecificEntry()
        {
            // Arrange
            File.WriteAllText(TestFilePath, "2024-06-25\nTest entry 1\n\n2024-06-26\nTest entry 2\n");
            DailyDiary diary = new DailyDiary(TestFilePath);

            // Act
            diary.DeleteEntry(new DateTime(2024, 6, 25));
            var entries = diary.ReadDiaryFile();

            // Assert
            Assert.Single(entries);
            Assert.DoesNotContain(entries, e => e.Date == new DateTime(2024, 6, 25));
        }

        [Fact]
        public void CountEntries_ShouldReturnCorrectCount()
        {
            // Arrange
            File.WriteAllText(TestFilePath, "2024-06-25\nTest entry 1\n\n2024-06-26\nTest entry 2\n");
            DailyDiary diary = new DailyDiary(TestFilePath);

            // Act
            var count = diary.CountEntries();

            // Assert
            Assert.Equal(2, count);
        }

        [Fact]
        public void SearchEntries_ShouldReturnMatchingEntries()
        {
            // Arrange
            File.WriteAllText(TestFilePath, "2024-06-25\nTest entry 1\n\n2024-06-26\nTest entry 2\n");
            DailyDiary diary = new DailyDiary(TestFilePath);

            // Act
            var results = diary.SearchEntries(new DateTime(2024, 6, 25));

            // Assert
            Assert.Single(results);
            Assert.Equal("Test entry 1", results[0].Content);
        }
    }
}