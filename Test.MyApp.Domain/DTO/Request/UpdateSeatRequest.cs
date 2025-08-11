namespace Test.MyApp.Domain.DTO.Request
{
    public class UpdateSeatRequest
    {
        public string RowChar { get; set; }
        public int? ColNumber { get; set; }
        public int? SeatColorId { get; set; }
        public string? Floor { get; set; }
    }
}
