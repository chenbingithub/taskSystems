﻿using HNCJ.IBLL;
using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.BLL
{
    public partial class LinksInfoService : BaseService<LinksInfo>, ILinksInfoService
    {
        public IQueryable<LinksInfo> LoagPageData(BaseParam queryParam)
        {
            var temp = DbSession.LinksInfoDal.GetEntity(u => u.DelFlag == true);
            if (!string.IsNullOrEmpty(queryParam.KeyString))
            {
                switch (queryParam.Itemid)
                {
                    case 1: temp = temp.Where(u => u.Title.Contains(queryParam.KeyString)).AsQueryable(); break;
                    case 2: temp = temp.Where(u => u.Url.Contains(queryParam.KeyString)).AsQueryable(); break;
                    default: temp = temp.Where(u => u.Title.Contains(queryParam.KeyString)).AsQueryable(); break;
                }

            }
            queryParam.Total = temp.Count();

            return temp.OrderBy(u => u.ID).Skip(queryParam.PageSize * (queryParam.PageIndex - 1)).Take(queryParam.PageSize).AsQueryable();
        }


       
    }
}
