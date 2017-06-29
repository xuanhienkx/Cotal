using System;
using System.Collections.Generic;
using System.Linq;
using Cotal.App.Data.Contexts;
using Cotal.App.Model.Models;
using Cotal.Core.InfacBase.Uow;
using Microsoft.EntityFrameworkCore;

namespace Cotal.App.Data.Services
{
    public interface IFunctionService
    {
        void Create(Function function);

        IEnumerable<Function> GetAll(string filter);

        IEnumerable<Function> GetAllWithPermission(int roleId);

        IEnumerable<Function> GetAllWithParentId(string parentId);

        Function Get(string id);

        void Update(Function function);

        void Delete(string id);

        int Save();
        bool CheckExistedId(string id);
    }
    public class FunctionService : ServiceBace<Function, string>, IFunctionService
    {
        private CotalContex _contex;
        public FunctionService(IUowProvider uowProvider, CotalContex contex) : base(uowProvider)
        {
            _contex = contex;
        }
        public void Create(Function function)
        {
            Repository.Add(function);
        }

        public IEnumerable<Function> GetAll(string filter)
        {
            return Repository.Query(x => x.Status && (string.IsNullOrEmpty(filter) || x.Name.Contains(filter)));
        }

        public IEnumerable<Function> GetAllWithPermission(int roleId)
        {
            var qr = (from f in _contex.Functions
                      join p in _contex.Permissions on f.Id equals p.FunctionId
                      where p.RoleId == roleId && (p.CanRead == true)
                      select f);
            var parentIds = qr.Select(x => x.ParentId).Distinct();
            qr = qr.Union(Repository.Query(x => parentIds.Contains(x.Id)));
            return qr.AsEnumerable();
        }

        public IEnumerable<Function> GetAllWithParentId(string parentId)
        {
            return Repository.Query(f => f.ParentId == parentId, o => o.OrderBy(x => x.ParentId));
        }

        public Function Get(string id)
        {
            return Repository.Get(id);
        }

        public void Update(Function function)
        {
            Repository.Update(function);
        }

        public void Delete(string id)
        {
            Repository.Remove(id);
        }

        public int Save()
        {
            return UnitOfWork.SaveChanges();
        }

        public bool CheckExistedId(string id)
        {
            return Repository.Any(x => x.Id == id);
        }        
    }
}