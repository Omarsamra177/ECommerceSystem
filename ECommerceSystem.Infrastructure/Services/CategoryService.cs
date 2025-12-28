using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceSystem.Core.Entities;
using ECommerceSystem.Core.Interfaces;
using ECommerceSystem.Core.Services;

namespace ECommerceSystem.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _unitOfWork.Categories.GetAllAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.Categories.GetByIdAsync(id);
        }

        public async Task<Category> CreateAsync(string name)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return category;
        }

        public async Task UpdateAsync(Guid id, string name)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);

            if (category == null)
                throw new Exception("Category not found");

            category.Name = name;
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.Categories.DeleteByIdAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
