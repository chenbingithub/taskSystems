using HNCJ.IBLL;
using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.BLL
{
    public partial class DeptService : BaseService<Dept>, IDeptService
    {
        public IQueryable<Dept> LoagPageData(BaseParam queryParam)
        {
            var temp = DbSession.DeptDal.GetEntity(u => u.DelFlag == true);
            if (!string.IsNullOrEmpty(queryParam.KeyString))
            {
                switch (queryParam.Itemid)
                {
                    case 1: temp = temp.Where(u => u.deptno.Contains(queryParam.KeyString)).AsQueryable(); break;
                    case 2: temp = temp.Where(u => u.deptname.Contains(queryParam.KeyString)).AsQueryable(); break;
                    case 3: temp = temp.Where(u => u.deptmanager.Contains(queryParam.KeyString)).AsQueryable(); break;
                    case 4: temp = temp.Where(u => u.telphone.Contains(queryParam.KeyString)).AsQueryable(); break;
                    default: temp = temp.Where(u => u.deptname.Contains(queryParam.KeyString)).AsQueryable(); break;
                }

            }
            
            queryParam.Total = temp.Count();

            return temp.OrderByDescending(u => u.RegTime).Skip(queryParam.PageSize * (queryParam.PageIndex - 1)).Take(queryParam.PageSize).AsQueryable();
        }
        
        public override bool Update(Dept entity)
        {
            var dept=GetModel(d => d.ID == entity.ID);
            if (entity.ID == entity.ParentId) return false;
           var listdept= DbSession.DeptDal.GetEntity(u => u.ParentId == entity.ID).ToList();
            if (listdept.Count!=0&&!dept.deptname.Equals(entity.deptname))
            {
                listdept.ForEach(x =>
                {
                    x.ParentName = entity.deptname;
                    DbSession.DeptDal.Update(x);
                });
            }
            return base.Update(entity);
        }
       
    }
}
