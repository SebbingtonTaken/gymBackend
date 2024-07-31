
namespace DTOs
{
    public class Measure: BaseDTO
    {
        public int UserId { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string FatPercentage { get; set; }
        public DateTime MeasureDate { get; set; }


    }
}
