namespace Csharp_Advanced.DataTransferObjects
{
    public class SearchRequestDto
    {
        public int? Features { get; set; }
        public int? Type { get; set; }
        public int? Rooms { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }

}
