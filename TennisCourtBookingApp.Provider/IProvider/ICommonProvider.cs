using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisCourtBookingApp.Common.BusinessEntities;
using TennisCourtBookingApp.Common.CommonEntities;

using TennisCourtBookingApp.Repository.Models;
using TennisCourtBookingApp.Repository.Repository;


namespace TennisCourtBookingApp.Provider.IProvider
{
    public interface ICommonProvider
    {
        #region Encrypt Properties

        string Protect(int value);

        string ProtectLong(long value);

        string ProtectShort(short value);

        int UnProtect(string value);

        string ProtectString(string value);

        string UnProtectString(string value);

        long UnProtectLong(string value);

        #endregion

        public TennisCourtBookingRoleModel FindRole(TennisCourtBookingUserModel loginDetail);
        public int FindUser(TennisCourtBookingUserModel userDetails);
        public void RegisterUser(TennisCourtBookingUserModel signupDetails);
        public string VerifyUsername(string email);
        public List<T> ConvertDataTableToList<T>(DataTable dt);
        List<TennisCourtBookingUser> GetAllUserData();



    }
}
