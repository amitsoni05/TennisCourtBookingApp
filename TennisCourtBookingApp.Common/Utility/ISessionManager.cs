
namespace TennisCourtBookingApp.Common.Utility
{

    public interface ISessionManager
    {
        int UserId { get; set; }
        string Email { get; set; }

        string RoleName { get; set; }
        int RoleId { get; set; }
        string CaptchaCode { get; set; }
        string GetSessionId();
        void ClearSession();
        string CurrentVersion { get; set; }
        string CurrentVersionDate { get; set; }

        string GetIP();

    }
}
