using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.IBLL
{
    public partial interface IUserInfoService:IBaseService<UserInfo>
    {
        IQueryable<UserInfo> LoagPageData(BaseParam queryParam);
        bool SetRole(int userId,List<int> roleIds);
        bool SetPermission(int userId, List<int> actionIds);
        bool SetPermissionItem(int userId, List<int> actionIds);
        UserInfo GetModel(string name, string pwd, bool is_encrypt);
        UserInfo GetModel(int id);
        bool Exits(string name);
        bool SetPassword(int userid);
        bool UpdatePwd(int ID, string oldpwd, string newpwd);
        bool UpdatePwd(string username, string newpwd);
        bool SetAvatar(int ID, string path);
        IQueryable<UserInfo> LoagPageData(int page, int size, int deptId, out int total);
    }
}
