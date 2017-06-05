using HNCJ.IBLL;
using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.BLL
{
    public partial class ActionItemService : BaseService<ActionItem>, IActionItemService
    {
        public IQueryable<ActionItem> LoagPageData(BaseParam queryParam)
        {
            return null;
        }
        public override bool Insert(ActionItem entity)
        {
            var model=GetModel(u => u.ActionItemNo == entity.ActionItemNo);
            if (model != null)
            {
                return false;
            }
            return base.Insert(entity);
        }
        public override bool Update(ActionItem entity)
        {
            var model = GetModel(u => u.ID == entity.ID);
            if (!model.ActionItemNo.Equals(entity.ActionItemNo))
            {
                var model1 = GetModel(u => u.ActionItemNo == entity.ActionItemNo);
                if (model1 != null)
                {
                    return false;
                }
            }
            return base.Update(entity);
        }
    }
}
