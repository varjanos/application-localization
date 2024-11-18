using LocalizationClient.Model;

namespace LocalizationClient.Services
{
    public class LocalDateService
    {
        private static LocalDateService _instance;
        private LocalDateService() { }
        private List<LocalDate> _dates = new List<LocalDate>();
        public static LocalDateService Instance()
        {
            if (_instance == null)
            {
                _instance = new LocalDateService();
            }
            return _instance;
        }
        public void AddHungarianDatas()
        {
            _dates.Add(new LocalDate()
            {
                date = DateOnly.FromDateTime(DateTime.Parse("2023.2.20")),
                temperature = "4 C",
                season = "Winter"
            });
            _dates.Add(new LocalDate()
            {
                date = DateOnly.FromDateTime(DateTime.Parse("2023.4.16")),
                temperature = "23 C",
                season = "Spring"
            });
            _dates.Add(new LocalDate()
            {
                date = DateOnly.FromDateTime(DateTime.Parse("2023.7.26")),
                temperature = "38 C",
                season = "Summer"
            });
            _dates.Add(new LocalDate()
            {
                date = DateOnly.FromDateTime(DateTime.Parse("2023.9.4")),
                temperature = "28 C",
                season = "Autumn"
            });
        }
        public List<LocalDate> GetDate()
        {
            return _dates;
        }
    }
}
