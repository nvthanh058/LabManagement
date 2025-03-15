using LabManagement.Infrastructure.IRespository;
using LabManagement.Models;
using Dapper;
using System.Data;
using System.Runtime;

namespace LabManagement.Infrastructure.Respository
{
    public class UserInfoResposity : IUserInfoResposity
    {
        private readonly IDapperServices _services;
        private readonly string TableName = "SYS_UserTable";

        public UserInfoResposity(IDapperServices services)
        {
            _services = services;
        }

        public async Task<int> DeleteGroup(string GroupID)
        {
            var query = @"SYS_DeleteGroup";
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();

                dbParams.Add("@GroupID", GroupID);

                res = Task.FromResult(_services.ExcuteScaler<UserGroupInfo>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }

            return res;
        }

        public async Task<int> DeleteUser(string UserID)
        {
            var query = @"SYS_DeleteUser";
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();

                dbParams.Add("@UserID", UserID);
               
                res = Task.FromResult(_services.ExcuteScaler<UserInfo>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }

            return res;
        }

        public async Task<List<UserInfo>>  GetAll(string UserID,string GroupID,string Search)
        {
            var query = @"SYS_GetUsers";

            var lst = new List<UserInfo>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@UserID", UserID);
                dbParams.Add("@GroupID", GroupID);
                dbParams.Add("@Search", Search);
             

                lst = Task.FromResult(_services.GetAll<UserInfo>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<UserInfo> GetUser(string UserID)
        {
            var query = @"SYS_GetUsers";

            var model = new UserInfo();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@UserID", UserID);
                dbParams.Add("@GroupID", "");
                dbParams.Add("@Search", "");

                model = Task.FromResult(_services.Get<UserInfo>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return model;
        }

        public async Task<UserInfo> GetUserByRef(string UserRefID)
        {
            var query = @"SYS_GetUserByRef";

            var model = new UserInfo();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@EmplRefID", UserRefID);


                model = Task.FromResult(_services.Get<UserInfo>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return model;
        }

        public async Task<List<UserGroupInfo>> GetAllGroup(string GroupID, string Search)
        {
            var query = @"SYS_GetGroups";

            var lst = new List<UserGroupInfo>();

            try
            {
                var dbParams = new DynamicParameters();                
                dbParams.Add("@GroupID", GroupID);
                dbParams.Add("@Search", Search);


                lst = Task.FromResult(_services.GetAll<UserGroupInfo>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
        }

        public async Task<int> SaveGroup(UserGroupInfo item)
        {
            var query = @"SYS_SaveGroup";
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();

                dbParams.Add("@RecID", item.RecID);                
                dbParams.Add("@GroupID", item.GroupID);
                dbParams.Add("@GroupName", item.GroupName);

                res = Task.FromResult(_services.ExcuteScaler<UserInfo>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }

            return res;
        }

        public async Task<int> SaveUser(UserInfo item)
        {
            var query = @"SYS_SaveUser";
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();
              
                dbParams.Add("@UserID", item.UserID);
                dbParams.Add("@UserName", item.UserName);
                dbParams.Add("@GroupID", item.GroupID);
                dbParams.Add("@CreatedUser", "ThanhNguyen");
                dbParams.Add("@EmplRefID", item.EmplRefID);
                dbParams.Add("@DATAAREAID", _services.DATAAREAID());
                res = Task.FromResult(_services.ExcuteScaler<UserInfo>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }

            return res;
        }

        public async Task<int> UpdateUserPassword(string UserID, string Password)
        {
            var query = @"SYS_UpdateUserPassword";
            var res = 0;
            try
            {
                var dbParams = new DynamicParameters();

                dbParams.Add("@UserID", UserID);
                dbParams.Add("@Password", Password);

                res = Task.FromResult(_services.ExcuteScaler<UserInfo>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }

            return res;
        }

        public async Task<List<OrganizationModel>> GetOrganization(int RecID, int ParentID)
        {
           
            var query = @"LAB_GetOrganization";

            var lst = new List<OrganizationModel>();

            try
            {
                var dbParams = new DynamicParameters();
                dbParams.Add("@RecID", RecID);
                dbParams.Add("@ParentID", ParentID);

                lst = Task.FromResult(_services.GetAll<OrganizationModel>(query, dbParams, commandType: CommandType.StoredProcedure)).Result;
            }
            catch (Exception ex) { }
            return lst;
      
        }
    }
}
