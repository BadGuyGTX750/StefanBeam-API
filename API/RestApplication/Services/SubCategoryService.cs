﻿using Microsoft.EntityFrameworkCore;
using Polly;
using RestApplication.Data;
using RestApplication.Models.Category;

namespace RestApplication.Repositories
{
    public class SubCategoryService
    {
        private readonly SubCategoryRepository repository;


        public SubCategoryService(SubCategoryRepository repository)
        {
            this.repository = repository;
        }


        public async Task<bool> AddSubCategory(SubCategoryModel category)
        {
            return await repository.AddSubCategory(category);
        }


        public async Task<SubCategoryModel> GetSubCategoryById(Guid id)
        {
            return await repository.GetSubCategoryById(id);
        }


        public async Task<SubCategoryModel> GetSubCategoryByName(string name)
        {
            return await repository.GetSubCategoryByName(name);
        }


        public async Task<List<SubCategoryModel>> GetSubCategoryByParentName(string parentName)
        {
            return await repository.GetSubCategoryByParentName(parentName);
        }


        public async Task<List<SubCategoryModel>> GetBottomSubCategories()
        {
            return await repository.GetBottomSubCategories();
        }


        public async Task<bool> UpdateSubCategory(SubCategoryModel subCategory)
        {
            return await repository.UpdateSubCategory(subCategory);
        }


        public async Task<bool> DeleteSubCategoryById(Guid id)
        {
            return await repository.DeleteSubCategoryById(id);
        }


        public async Task<bool> DeleteSubCategoryByName(string name)
        {
            return await repository.DeleteSubCategoryByName(name);
        }
    }
}
