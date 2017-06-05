using HNCJ.IBLL;
using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.BLL
{
    public partial class SysLogService:BaseService<SysLog>,ISysLogService
    {
        public IQueryable<SysLog> LoagPageData(BaseParam queryParam)
        {
            var temp = DbSession.SysLogDal.GetEntity(u => u.DelFlag == true);
            if (!string.IsNullOrEmpty(queryParam.KeyString))
            {
                switch (queryParam.Itemid)
                {
                    case 1: temp = temp.Where(u => u.UserName.Contains(queryParam.KeyString)).AsQueryable(); break;
                    default: temp = temp.Where(u => u.UserName.Contains(queryParam.KeyString)).AsQueryable(); break;
                }

            }

            queryParam.Total = temp.Count();

            return temp.OrderBy(u => u.ID).OrderByDescending(u=>u.OperationTime).Skip(queryParam.PageSize * (queryParam.PageIndex - 1)).Take(queryParam.PageSize).AsQueryable();
        }

        public void Insert(UserInfo userinfo, string operation)
        {
            if (userinfo == null) userinfo = new UserInfo();
            SysLog syslog = new SysLog
            {
                DelFlag = true,
                OperationTime = DateTime.Now,
                Operation = operation,
                UserInfoID = userinfo.ID,
                UserName = userinfo.UserName
            };
            Insert(syslog);
        }
    }
}
