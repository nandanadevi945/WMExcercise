namespace WM.Api.Models
{
    public class NewGlobalUniqueIdentifierModel
    {
        private string? guid = System.Guid.NewGuid().ToString("N").ToUpper();
        private int? expiryDays = 30;

        public string?   Guid
        {
            get => guid;
            set => guid = value;
        }
        public int? ExpiryDays { get => expiryDays; set => expiryDays = value; }
        public string Usr { get; set; }
    }
}
