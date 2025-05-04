using AutoMapper;
using TennisCourtBookingApp.Common.BusinessEntities;
using TennisCourtBookingApp.Common.Utility;
using TennisCourtBookingApp.Common.CommonEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisCourtBookingApp.Provider.IProvider;
using TennisCourtBookingApp.Repository.ADO;
using TennisCourtBookingApp.Repository.Models;
using TennisCourtBookingApp.Repository.Repository;
using Microsoft.AspNetCore.DataProtection;





namespace TennisCourtBookingApp.Provider.Provider
{
    public class CommonProvider : ICommonProvider
    {

        protected UnitOfWork unitofwork = new UnitOfWork();
        private readonly IMapper _mapper;

        private IDataProtector _IDataProtector;
        private DBConnectivity dBConnectivity = new DBConnectivity();


        public CommonProvider(IMapper mapper, IDataProtectionProvider dataProtector)
        {
            _mapper = mapper;

            _IDataProtector = dataProtector.CreateProtector(AppCommon.Protection);
        }

        #region Encrypt/Decrypt

        public string Protect(int value)
        {
            return _IDataProtector.Protect(value.ToString());
        }
        public string ProtectLong(long value)
        {
            return _IDataProtector.Protect(value.ToString());
        }
        public string ProtectShort(short value)
        {
            return _IDataProtector.Protect(value.ToString());
        }
        public int UnProtect(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                string data = _IDataProtector.Unprotect(value);
                data = data ?? "0";
                return Convert.ToInt32(data);
            }
            else
                return 0;

        }
        public string ProtectString(string value)
        {
            value = value ?? string.Empty;
            return _IDataProtector.Protect(value);
        }
        public string UnProtectString(string value)
        {
            return _IDataProtector.Unprotect(value);
        }
        public long UnProtectLong(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                string data = _IDataProtector.Unprotect(value);
                data = data ?? "0";
                return Convert.ToInt64(data);
            }
            else
                return 0;
        }

        #endregion

        public TennisCourtBookingRoleModel FindRole(TennisCourtBookingUserModel loginDetail)
        {
            var userData = unitofwork.TennisCourtBookingUser.GetAll(u => u.Email == loginDetail.Email).FirstOrDefault();
            if (userData != null)
            {
                string decPass = AES.DecryptAES(loginDetail.Password);
                if (PasswordHash.ValidatePassword(decPass, userData.Password))
                {
                    //var roleDetail = unitofwork.TennisCourtBookingUser.Get(u => u.Email == loginDetail.Email && u.Password == loginDetail.Password);



                    if (userData != null)
                    {


                        var roleName = unitofwork.TennisCourtBookingRole.GetById(userData.RoleId);
                        var Name = new TennisCourtBookingRoleModel()
                        {
                            RoleName = roleName.RoleName,
                            RoleId = roleName.RoleId,
                        };
                        return Name;
                    }
                }
            }
            return null;
        }
        public int FindUser(TennisCourtBookingUserModel userDetails)
        {
            var details = unitofwork.TennisCourtBookingUser.GetAll(u => u.Email == userDetails.Email).FirstOrDefault();

            var userId = details.UserId;
            return userId;
        }
        public void RegisterUser(TennisCourtBookingUserModel signupDetails)
        {
            try
            {
                string decPass = AES.DecryptAES(signupDetails.Password);
                List<StoredProcModel> parms = new List<StoredProcModel>();

                parms.Add(new StoredProcModel() { Key = "UserId", Value = DBNull.Value });
                parms.Add(new StoredProcModel() { Key = "UserName", Value = signupDetails.UserName });
                parms.Add(new StoredProcModel() { Key = "PhoneNo", Value = signupDetails.PhoneNo });
                parms.Add(new StoredProcModel() { Key = "Email", Value = signupDetails.Email });
                parms.Add(new StoredProcModel() { Key = "Password", Value = PasswordHash.CreateHash(decPass), });
                parms.Add(new StoredProcModel() { Key = "Address", Value = signupDetails.Address });
                parms.Add(new StoredProcModel() { Key = "RoleId", Value = 2 });
                parms.Add(new StoredProcModel() { Key = "Image", Value = signupDetails.Image });
                parms.Add(new StoredProcModel() { Key = "CreatedOn", Value = DateTime.Now });

                DataTable dt = dBConnectivity.GetDataFromSP(parms, "spSaveOrEdit_User");
                var respone = ConvertDataTableToList<ResultModel>(dt);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<T> ConvertDataTableToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        try
                        {
                            if (row[pro.Name] != DBNull.Value)
                                pro.SetValue(objT, row[pro.Name]);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                return objT;
            }).ToList();
        }
        public string VerifyUsername(string email)
        {
            TennisCourtBookingUserModel res = new TennisCourtBookingUserModel();
            var result = "";
            try
            {
                var _user = unitofwork.TennisCourtBookingUser.GetAll(x => x.Email == email).FirstOrDefault();

                if (_user != null)
                {


                    string resetLink = AppCommon.AppUrl + ProtectString(_user.UserId.ToString() + "|" + DateTime.Now.Ticks.ToString());

                    string mailBody = $@"Hello {_user.UserName},<br><br>
                                    
                                        Click below link to reset your password. <br><br> 
                                        <a href='{resetLink}'>{resetLink}</a><br>   <br>                                   
                                        <b>Note:</b> Above link will expire with in 30 min.<br> 
                                        If link is not clickable then please copy and paste in your browser.<br><br>

                                        <b>Thanks & Regard</b><br>
                                        {AppCommon.ApplicationLongTitle}
                                    ";

                    EmailSender.SendEmail(_user.Email, AppCommon.ApplicationTitle, "Reset Password", mailBody);
                    result = "true";
                    result = "Reset password link sent to your email.";

                }
                else
                    result = "Invalid username!";
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public List<TennisCourtBookingUser> GetAllUserData()
        {
            List<TennisCourtBookingUser> userMasters = new List<TennisCourtBookingUser>();
            userMasters = unitofwork.TennisCourtBookingUser.GetAll().Select(x => new TennisCourtBookingUser()
            {
                UserId=x.UserId,
                Email = x.Email,
                PhoneNo = x.PhoneNo,
            }).ToList();
            return userMasters;
        }


    }
}

