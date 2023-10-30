namespace POS.Application.Dtos.DocumentType.Request
{
    public class DocumentTypeRequestDto
    {
        public string? Code { get; set; }
        public string?  Name { get; set; }
        public string? Abbreviation { get; set; }
        public int State { get; set; }
    }
}
