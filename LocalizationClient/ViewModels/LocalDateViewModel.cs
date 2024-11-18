using LocalizationClient.Model;
using LocalizationClient.Services;

namespace LocalizationClient.ViewModels
{
    public class LocalDateViewModel
    {
        private LocalDateService _dateTimeService = LocalDateService.Instance();
        public void AddData()
        {
            _dateTimeService.AddHungarianDatas();
        }
        public List<LocalDate> GetData()
        {
            return _dateTimeService.GetDate();
        }
    }
}
