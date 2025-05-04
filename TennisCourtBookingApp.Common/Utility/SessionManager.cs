using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization.Formatters.Binary;
namespace TennisCourtBookingApp.Common.Utility
{

    public class SessionManager : ISessionManager
    {
        private string sessionKey = "DMEPOSSeesionKEY";
        private readonly IHttpContextAccessor _httpContextAccessor = null;
        private SessionModel SessionData { get; set; }

        public int UserId
        {
            get
            {
                return GetSession().UserId;
            }
            set
            {
                SessionData.UserId = value;
                SetSession();
            }
        }
        public string Email
        {
            get
            {
                return GetSession().Email;
            }
            set
            {
                SessionData.Email = value;
                SetSession();
            }

        }
       
        public string RoleName
        {
            get
            {
                return GetSession().RoleName;
            }
            set
            {
                SessionData.RoleName = value;
                SetSession();
            }

        }
       
        public int RoleId
        {
            get
            {
                return GetSession().RoleId;
            }
            set
            {
                SessionData.RoleId = value;
                SetSession();
            }
        }
        public string CaptchaCode
        {
            get
            {
                return GetSession().CaptchaCode;
            }
            set
            {
                SessionData.CaptchaCode = value;
                SetSession();
            }
        }
        public string Image
        {
            get
            {
                return GetSession().Image;
            }
            set
            {
                SessionData.Image = value;
                SetSession();
            }
        }
        public string CurrentVersion
        {
            get
            {
                return GetSession().CurrentVersion;
            }
            set
            {
                SessionData.CurrentVersion = value;
                SetSession();
            }
        }
        public string CurrentVersionDate
        {
            get
            {
                return GetSession().CurrentVersionDate;
            }
            set
            {
                SessionData.CurrentVersionDate = value;
                SetSession();
            }
        }
        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            SessionData = GetSession();
            if (SessionData == null)
            {
                SessionData = new SessionModel();
            }
        }
        public SessionModel GetSession()
        {
            var session = _httpContextAccessor.HttpContext.Session.Get(sessionKey);
            return (SessionModel)(session != null ? FromByteArray<SessionModel>(session) : new SessionModel());
        }
        public void SetSession()
        {
            _httpContextAccessor.HttpContext.Session.Set(sessionKey, ObjectToByteArray(SessionData));
        }
        public string GetSessionId()
        {
            return _httpContextAccessor.HttpContext.Session.Id.ToString();
        }
        public void ClearSession()
        {
            SessionData = new SessionModel();
            _httpContextAccessor.HttpContext.Session.Remove(sessionKey);
            _httpContextAccessor.HttpContext.Session.Clear();
        }
        public string GetIP()
        {
            return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }
        private static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
        private static T FromByteArray<T>(byte[] data)
        {
            if (data == null)
                return default(T);
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }
    }
}
