using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Contract.Core;
using Contract.Core.Helpers;
using Contract.Data;
using Contract.Data.Models;
using Contract.Services.Interface;
using Contract.ViewModels;
using Contract.ViewModels.DAL;

namespace Contract.Services
{
    public class CategoryService : ICategoryService
    {
        private static readonly Func<CategoryViewModel, Category, Category> ToDomain = (x, y) =>
        {
            if (y == null)
            {
                y = new Category();
            }
            y.Id = x.Id;
            y.Code = x.Code;
            y.Note = x.Note;
            y.Counter = x.Counter;

            return y;
        };

        private static Func<Category, CategoryViewModel> ToViewModel
        {
            get
            {
                return x =>
                {
                    return x == null ? new CategoryViewModel() : new CategoryViewModel
                    {
                        Id = x.Id,
                        Code = x.Code,
                        Note = x.Note,
                        Counter = x.Counter
                    };
                };
            }
        }

        public IRepository<Category> Repository
        {
            get;
            private set;
        }

        public CategoryService()
        {
            var dbContext = DiConfig.Resolve<RepositoryContextBase>();

            Repository = dbContext.GetRepository<Category>();
        }

        public IList<CategoryViewModel> GetAll()
        {
            var result = Repository.GetAll()
                .AsEnumerable()
                .Select(x => ToViewModel(x))
                .ToList();

            return result;
        }

        public int CreateOrUpdate(CategoryViewModel item)
        {
            var data = (Category)null;

            if (item.Id > 0)
            {
                data = Repository.FirstOrDefault(x => x.Id == item.Id);
            }

            data = ToDomain(item, data);
            Repository.Store(data);
            Repository.SaveChanges();

            return data.Id;
        }

        public CategoryViewModel GetById(int id)
        {
            var data = Repository.FirstOrDefault(x => x.Id == id);

            if (data == null)
            {
                ErrorHelper.NotFound("Not found ");
            }

            return ToViewModel(data);
        }

        public CategoryViewModel Get(Expression<Func<Category, bool>> predicate)
        {
            var data = Repository.FirstOrDefault(predicate);

            if (data == null)
            {
                ErrorHelper.NotFound("Not found this  WorkType");

                return null;
            }

            return ToViewModel(data);
        }

        public bool Delete(int id)
        {
            var data = Repository.FirstOrDefault(x => x.Id == id);

            if (data == null)
            {
                ErrorHelper.NotFound("Not found");
            }
            Repository.Delete(data);

            return Repository.SaveChanges() > 0;
        }

        public void CreateOrUpdateByRange(IList<CategoryViewModel> t)
        {
            throw new NotImplementedException();
        }

        public IList<CategoryViewModel> GetAll(Expression<Func<Category, bool>> predicate)
        {
            var result = Repository.Where(predicate)
                      .AsEnumerable()
                      .Select(x => ToViewModel(x))
                      .ToList();

            return result;
        }

        public int Max(Func<Category, int> selector)
        {
            return Repository.Max(selector);
        }
    }
}