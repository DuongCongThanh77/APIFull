using AutoMapper;
using Data.ContexDb;
using Data.Model;
using Data.ModelView;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
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
        public readonly IMapper _map;

        public UserService(SaleContexDb saleContexDb, IOptionsMonitor<AppSettings> opption, IMapper map)
        {
            this.saleContexDb = saleContexDb;
            this.appSettings = opption.CurrentValue;
            this._map = map;
        }

        public AppSettings getAppSettings()
        {
            return this.appSettings;
        }

        public List<UserView> GetUser()
        {
            //  // SqlConnection con = new SqlConnection(@"Data Source =.; Initial Catalog = SaleOnline; Integrated Security = True");
            //   //  var copy = new SqlBulkCopy(@"Data Source =.; Initial Catalog = SaleOnline; Integrated Security = True");
            ////   DataTable tb = new DataTable();
            //   con.Open();
            //   new SqlDataAdapter(new SqlCommand("Select * from [dbo].[User]", con)).Fill(tb);
            //   con.Close();
            //   //  var s = tb.Rows[0];
            //   //var   userView= tb.AsEnumerable().Select(select => this._map.Map<DataRow, UserView>(select)   );

            //   //  return null;

            //   tb.Rows[0].ItemArray[0] = Guid.NewGuid();
            //   tb.Rows[1].ItemArray[0] = Guid.NewGuid();

            //   var Users = new List<User>();

            //   Users.Add(new User { UserID = Guid.NewGuid(), UserName = "admin", Password = "admin" });
            //   Users.Add(new User { UserID = Guid.NewGuid(), UserName = "admin", Password = "admin" });

            //   string json = JsonConvert.SerializeObject(Users);
            //   DataTable pDt = JsonConvert.DeserializeObject<DataTable>(json);
            //   string json2 = JsonConvert.SerializeObject(pDt);
            //   List<User> us = JsonConvert.DeserializeObject<List<User>>(json2);
            DataTable tb = new DataTable();
            tb.Columns.Add("Id", typeof(string));
            tb.Columns.Add("Value", typeof(string));
            tb.Rows.Add(new Object[]{"test", "test"});
            tb.Rows.Add(new Object[]{"test", "test"});
            tb.Rows.Add(new Object[]{"test", "test"});
            tb.Rows.Add(new Object[]{"test", "test"});
            tb.Rows.Add(new Object[]{"test", "test"});
            tb.Rows.Add(new Object[]{"test", "test"});
            tb.Rows.Add(new Object[]{"test", "test"});
            tb.Rows.Add(new Object[]{"test", "test"});
            tb.Rows.Add(new Object[]{"test", "test"});
            tb.Rows.Add(new Object[]{"test", "test"});
            tb.Rows.Add(new Object[]{"test", "test"});
            tb.Rows.Add(new Object[]{"test", "test"});
            tb.Rows.Add(new Object[]{"test", "test"});
            tb.Rows.Add(new Object[]{"test", "test"});
            tb.Rows.Add(new Object[]{"test", "test"});
            tb.Rows.Add(new Object[]{"test", "test"});
            tb.Rows.Add(new Object[]{"test", "test"});
            tb.Rows.Add(new Object[]{"test", "test"});
            tb.Rows.Add(new Object[]{"test", "test"});
            tb.Rows.Add(new Object[] { "test", "test" });
            tb.Rows.Add(new Object[] { "test", "test" });
            tb.Rows.Add(new Object[] { "test", "test" });
            tb.Rows.Add(new Object[] { "test", "test" });
            tb.Rows.Add(new Object[] { "test", "test" });
            tb.Rows.Add(new Object[] { "test", "test" });
            tb.Rows.Add(new Object[] { "test", "test" });
            tb.Rows.Add(new Object[] { "test", "test" });
            tb.Rows.Add(new Object[] { "test", "test" });
            tb.Rows.Add(new Object[] { "test", "test" });
            tb.Rows.Add(new Object[] { "test", "test" });
            tb.Rows.Add(new Object[] { "test", "test" });
            tb.Rows.Add(new Object[] { "test", "test" });
            tb.Rows.Add(new Object[] { "test", "test" });
            tb.Rows.Add(new Object[] { "test", "test" });
            tb.Rows.Add(new Object[] { "test", "test" });
            tb.Rows.Add(new Object[] { "test", "test" });
            tb.Rows.Add(new Object[] { "test", "test" });
            tb.Rows.Add(new Object[] { "test", "test" });


            // pDt.Columns["UserID"].DataType = typeof(Guid);
            string csDestination = @"Data Source =.; Initial Catalog = SaleOnline; Integrated Security = True";

            using (SqlConnection connection = new SqlConnection(csDestination))
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
            {
                connection.Open();
                bulkCopy.DestinationTableName = "[SaleOnline].[dbo].[Demo]";
                bulkCopy.WriteToServer(tb);
                connection.Close();

            }
           
            return new List<UserView>();
        }



        public User Login(UserView user)
        {
            var User = saleContexDb.Users.SingleOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
            return User;
        
        }

        public RefreshToken AddReFreshToken(RefreshToken refreshToken)
        {
            this.saleContexDb.RefreshToken.Add(refreshToken);
            this.saleContexDb.SaveChanges();
            return refreshToken;
        }

        public bool CheckReFreshToken(string refreshToken)
        {
            return !(this.saleContexDb.RefreshToken.SingleOrDefault(x => x.Token == refreshToken) == null);
        }

        public RefreshToken GetReFreshToken(string refreshToken)
        {
           return this.saleContexDb.RefreshToken.SingleOrDefault(x => x.Token == refreshToken);
        }

        public User GetUser(Guid userId)
        {
            return this.saleContexDb.Users.FirstOrDefault(x => x.UserID == userId);
        }

        public bool UpdateReFreshToken(RefreshToken refreshToken)
        {
            this.saleContexDb.RefreshToken.Update(refreshToken);
            return true;
        }
    }
}
