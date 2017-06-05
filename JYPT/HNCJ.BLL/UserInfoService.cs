using HNCJ.Common;
using HNCJ.IBLL;
using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.BLL
{
    public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        #region 多条件查询加分页
        public IQueryable<UserInfo> LoagPageData(BaseParam queryParam)
        {
            var temp = DbSession.UserInfoDal.GetEntity(u => u.DelFlag == true && u.DeptID == queryParam.Itemid);
            

            queryParam.Total = temp.Count();

            return temp.OrderBy(u => u.ID).Skip(queryParam.PageSize * (queryParam.PageIndex - 1)).Take(queryParam.PageSize).AsQueryable();
        }

        public IQueryable<UserInfo> LoagPageData(int page, int size, int deptId, out int total)
        {
            total = 0;
            var temp = DbSession.UserInfoDal.GetEntity(u => u.DelFlag == true && u.DeptID == deptId);
            total = temp.Count();
            return temp.OrderBy(u => u.ID).Skip(size * (page - 1)).Take(size);
        }  
        #endregion

        #region 设置角色
        public bool SetRole(int userId, List<int> roleIds)
        {
            var user = DbSession.UserInfoDal.GetModel(u => u.ID == userId);
            user.RoleInfo.Clear();
            var allRoles = DbSession.RoleInfoDal.GetEntity(r => roleIds.Contains(r.ID));
            foreach (var roleInfo in allRoles)
            {
                user.RoleInfo.Add(roleInfo);
            }
            return DbSession.SaveChanges() > 0;

        } 
        #endregion

        #region 设置权限
        public bool SetPermission(int userId, List<int> actionIds)
        {
            var user = DbSession.UserInfoDal.GetModel(u => u.ID == userId);
            user.ActionInfo.Clear();
            var allActions = DbSession.ActionInfoDal.GetEntity(r => actionIds.Contains(r.ID));
            foreach (var actionInfo in allActions)
            {
                user.ActionInfo.Add(actionInfo);
            }

            return DbSession.SaveChanges() > 0;
        }
        public bool SetPermissionItem(int userId, List<int> actionIds)
        {
            var user = DbSession.UserInfoDal.GetModel(u => u.ID == userId);
            user.ActionItem.Clear();
            var allActions = DbSession.ActionItemDal.GetEntity(r => actionIds.Contains(r.ID));
            foreach (var actionInfo in allActions)
            {
                user.ActionItem.Add(actionInfo);
            }

            return DbSession.SaveChanges() > 0;
        }
        
        #endregion
       

        #region 登录验证
        public UserInfo GetModel(int id)
        {
            return DbSession.UserInfoDal.GetEntity(u => u.DelFlag == true && u.ID==id).FirstOrDefault();
        }
        public UserInfo GetModel(string name, string pwd, bool is_encrypt)
        {
            //检查一下是否需要加密
            if (is_encrypt)
            {
                //先取得该用户的随机密钥
                string salt=GetSalt(name);
                if(string.IsNullOrEmpty(salt))
                {
                    return null;
                }
                //把明文进行加密重新赋值
                pwd = DESEncrypt.Encrypt(pwd,salt);

            }
            return GetModel(name, pwd);
        }
        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        private UserInfo GetModel(string name, string pwd) {
            return DbSession.UserInfoDal.GetEntity(u=>u.DelFlag==true&&u.UserName==name&&u.UserPwd==pwd).FirstOrDefault();
        }
        /// <summary>
        /// 根据用户名取得Salt
        /// </summary>
        private string GetSalt(string name) {
            UserInfo user = DbSession.UserInfoDal.GetEntity(u => u.DelFlag == true && u.UserName == name).FirstOrDefault();
            if (user == null && string.IsNullOrEmpty(user.salt))
            {
                return null;
            }
            return user.salt;
        }
        #endregion

        #region 修改密码
        public bool UpdatePwd(int id,string oldpwd, string newpwd) {
           var user= DbSession.UserInfoDal.GetEntity(u =>u.ID == id).FirstOrDefault();
           if (user == null) return false;
           oldpwd=DESEncrypt.Encrypt(oldpwd, user.salt);
           if (!user.UserPwd.Equals(oldpwd)) {
               return false;
           }
           user.UserPwd = DESEncrypt.Encrypt(newpwd,user.salt);
           DbSession.UserInfoDal.Update(user);
           return DbSession.SaveChanges()>0;
        }
        public bool UpdatePwd(string username, string newpwd)
        {
            var user = DbSession.UserInfoDal.GetModel(u => u.UserName == username);
            if (user == null) return false;
            user.UserPwd = DESEncrypt.Encrypt(newpwd, user.salt);
            DbSession.UserInfoDal.Update(user);
            return DbSession.SaveChanges() > 0;
        }
        #endregion

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public bool SetPassword(int userid) {
            var userInfo = GetModel(u => u.ID == userid);
            userInfo.ModfiedOn = DateTime.Now;
            userInfo.salt = Utils.GetCheckCode(6); //获得6位的salt加密字符串
            userInfo.UserPwd = DESEncrypt.Encrypt("123456", userInfo.salt);
            return Update(userInfo);;
        }
        #region 设置头像
        public bool SetAvatar(int ID, string path) {
            var user = DbSession.UserInfoDal.GetEntity(u => u.DelFlag == true && u.ID == ID).FirstOrDefault();
            if (user == null) return false;
            user.Avatar = path;
            user.ModfiedOn = DateTime.Now;
            DbSession.UserInfoDal.Update(user);
            return DbSession.SaveChanges()>0;
        }
        #endregion

        #region 判断用户名是否存在
        public bool Exits(string name) {
            UserInfo user = DbSession.UserInfoDal.GetModel(u => u.DelFlag == true && u.UserName == name);
            if (user != null)
            {
                return true;
            }
            else {
                return false;            
            }
        }
        #endregion
    }
}
