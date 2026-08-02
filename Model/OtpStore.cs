namespace DriveHubMongo.Model
{
    public class OtpStore
    {
        public string Email { get; set; } = string.Empty;

        public string Otp { get; set; } = string.Empty;

        public DateTime ExpiryTime { get; set; }
    }
}