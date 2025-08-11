namespace Test.MyApp.Domain.DTO.Response
{
    public class GetSeatResponse
    {
        public int Id { get; set; }
        public string? RowChar { get; set; }
        public int ColNumber { get; set; }
        public GetColorResponse? SeatColor { get; set; }
        public string? Floor { get; set; }
    }
}
