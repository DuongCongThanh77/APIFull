using Data.Model;
using Data.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppApi.Model;

namespace Service.Interface
{
  public  interface IUserService
    {
        public User Login(UserView user);

        public AppSettings getAppSettings();

    }
}
