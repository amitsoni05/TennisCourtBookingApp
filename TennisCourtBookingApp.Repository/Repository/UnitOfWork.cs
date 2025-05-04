
using TennisCourtBookingApp.Repository.Models;
using TennisCourtBookingApp.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCourtBookingApp.Repository.Repository
{
    public class UnitOfWork : IDisposable
    {
        #region Variables   
        private TestSPC5Context context = new TestSPC5Context();
        private GenericRepository<TennisCourt> _TennisCourt;
        private GenericRepository<TennisCourtBooking> _TennisCourtBooking;

        private GenericRepository<TennisCourtBookingRole> _TennisCourtBookingRole;

        private GenericRepository<TennisCourtBookingUser> _TennisCourtBookingUser;

       


        #endregion

        #region Repository Methods

        public GenericRepository<TennisCourt> TennisCourt
        {
            get
            {
                if (_TennisCourt == null)
                    _TennisCourt = new GenericRepository<TennisCourt>(context);
                return _TennisCourt;
            }
        }
        public GenericRepository<TennisCourtBooking> TennisCourtBooking
        {
            get
            {
                if (_TennisCourtBooking == null)
                    _TennisCourtBooking = new GenericRepository<TennisCourtBooking>(context);
                return _TennisCourtBooking;
            }
        }
        public GenericRepository<TennisCourtBookingRole> TennisCourtBookingRole
        {
            get
            {
                if (_TennisCourtBookingRole == null)
                    _TennisCourtBookingRole = new GenericRepository<TennisCourtBookingRole>(context);
                return _TennisCourtBookingRole;
            }
        }
        public GenericRepository<TennisCourtBookingUser> TennisCourtBookingUser
        {
            get
            {
                if (_TennisCourtBookingUser == null)
                    _TennisCourtBookingUser = new GenericRepository<TennisCourtBookingUser>(context);
                return _TennisCourtBookingUser;
            }
        }

        #endregion

        #region Save and Dispose
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
