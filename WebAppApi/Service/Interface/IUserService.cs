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
        public List<UserView> GetUser();

        public User GetUser(Guid userId);
        public RefreshToken AddReFreshToken(RefreshToken refreshToken);
        public bool UpdateReFreshToken(RefreshToken refreshToken);

        public bool CheckReFreshToken(string refreshToken);
        public RefreshToken GetReFreshToken(string refreshToken);

        public AppSettings getAppSettings();

    }
}
