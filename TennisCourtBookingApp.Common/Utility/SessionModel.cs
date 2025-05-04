namespace TennisCourtBookingApp.Common.Utility
{
    [Serializable]
    public class SessionModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
       
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public string CaptchaCode { get; set; }
        public string Image { get; set; }
        public string CurrentVersion { get; set; }
        public string CurrentVersionDate { get; set; }

    }
}
