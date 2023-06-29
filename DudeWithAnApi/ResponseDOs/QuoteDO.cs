namespace DudeWithAnApi.ResponseDOs
{
    public class QuoteDO 
    {
        public int Id { get; set; }
        public string? QuoteText { get; set; }
        public string? SecondaryText { get; set; }
        public string? Url { get; set; }
        public DateTime CreationDate { get; set; }
        public int? IsActive { get; set; }
        public int? IsDeleted { get; set; }
        public int? IsCSV { get; set; }
    
	}
}

