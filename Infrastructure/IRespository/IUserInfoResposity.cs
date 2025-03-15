using LabManagement.Models;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace LabManagement.Infrastructure.IRespository
{
    public interface IUserInfoResposity
    {
        Task<List<UserGroupInfo>> GetAllGroup(string GroupID, string Search);
        Task<int> SaveGroup(UserGroupInfo item);
        Task<int> DeleteGroup(string GroupID);

        Task<List<UserInfo>> GetAll(string UserID,string GroupID,string Search);
        Task<UserInfo> GetUser(string UserID);
        Task<UserInfo> GetUserByRef(string UserRefID);
        Task<int> SaveUser(UserInfo item);
        Task<int> DeleteUser(string UserID);

        Task<int> UpdateUserPassword(string UserID,string Password);

        Task<List<OrganizationModel>> GetOrganization(int RecID, int ParentID);

    }
}
