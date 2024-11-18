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
                date = DateOnly.FromDateTime(DateTime.Parse("2/20/2023")),
                temperature = "4 C",
                season = "Winter",
                time = DateTime.Parse("2023.3.4 18:50").ToString("MM/dd/yyyy h:mm")
            });
            _dates.Add(new LocalDate()
            {
                date = DateOnly.FromDateTime(DateTime.Parse("4/16/2023")),
                temperature = "23 C",
                season = "Spring",
                time = DateTime.Parse("2023.5.3 19:34").ToString("MM/dd/yyyy h:mm")
            });
            _dates.Add(new LocalDate()
            {
                date = DateOnly.FromDateTime(DateTime.Parse("7/26/2023")),
                temperature = "38 C",
                season = "Summer",
                time = DateTime.Parse("2023.8.1 18:05").ToString("MM/dd/yyyy h:mm")
            });
            _dates.Add(new LocalDate()
            {
                date = DateOnly.FromDateTime(DateTime.Parse("9/4/2023")),
                temperature = "28 C",
                season = "Autumn",
                time = DateTime.Parse("2023.10.6 21:40").ToString("MM/dd/yyyy h:mm")
            });
        }
        public List<LocalDate> GetDate()
        {
            return _dates;
        }
    }
}
