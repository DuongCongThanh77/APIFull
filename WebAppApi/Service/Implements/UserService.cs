using Data.ContexDb;
using Data.Model;
using Data.ModelView;
using Microsoft.Extensions.Options;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppApi.Model;

namespace Service.Implements
{
    public class UserService : IUserService
    {
        private readonly SaleContexDb saleContexDb;
        private readonly AppSettings appSettings;

        public UserService(SaleContexDb saleContexDb, IOptionsMonitor<AppSettings> opption)
        {
            this.saleContexDb = saleContexDb;
            this.appSettings = opption.CurrentValue;
        }

        public AppSettings getAppSettings()
        {
            return this.appSettings;
        }

        public User Login(UserView user)
        {
            var User = saleContexDb.Users.SingleOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
            return User;
        
        }
    }
}
