using HNCJ.DalFactory;
using HNCJ.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace HNCJ.BLL
{
   public abstract class BaseService<T> where T:class,new ()
    {
       public IBaseDal<T> CurretDal { get; set; }
       public IDbSession DbSession { get { return DbSessionFactory.GetCurrentDbSession(); } }
       public abstract void SetCurrentDal();//抽象方法，让子类来实现

        #region 构造方法
       public BaseService()
       {
           SetCurrentDal();
       } 
       #endregion

        #region 查询操作
       public IQueryable<T> GetList()
       {
           return CurretDal.GetList();
       }
       public T GetModel(Expression<Func<T, bool>> where)
       {
           return CurretDal.GetModel(where);
       }
       public IQueryable<T> GetEntity(Expression<Func<T, bool>> whereLambda)//条件查询方法
       {
           return CurretDal.GetEntity(whereLambda);
       } 
       #endregion

        #region 分页查询

       public IQueryable<T> GetPageEntity<S>(int pagesize, int pageIndex, out int total, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderbyLambda, bool isAsc)//分页条件查询
       {
           return CurretDal.GetPageEntity(pagesize, pageIndex, out total, whereLambda, orderbyLambda, isAsc);
       }
       
       #endregion

        #region 添加操作
           public T Add(T entity)
           {
               CurretDal.Add(entity);
               DbSession.SaveChanges();
               return entity;
           }
           public virtual bool Insert(T entity) {
               CurretDal.Add(entity);
               return DbSession.SaveChanges()>0;
           }
        #endregion

        #region 修改操作

         public virtual bool Update(T entity)
        {
            CurretDal.Update(entity);
            return DbSession.SaveChanges() > 0;
        }
        
        #endregion

        #region 删除操作
        public bool Delete(T entity)
        {
            CurretDal.Delete(entity);
            return DbSession.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            CurretDal.Delete(id);
            return DbSession.SaveChanges() > 0;
        }
        public bool DeleteList(List<int> ids)
        {
            ids.ForEach(x => { CurretDal.Delete(x); });
            return DbSession.SaveChanges()>0;
        } 
        #endregion
        #region 批量删除之软删除
        public int DeleteListByLogical(List<int> ids)
        {
            CurretDal.DeleteListByLogical(ids);
            return DbSession.SaveChanges();
        } 
        #endregion
    }
}
