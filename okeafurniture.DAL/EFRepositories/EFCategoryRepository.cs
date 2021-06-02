﻿using Microsoft.EntityFrameworkCore;
using okeafurniture.CORE.Entites;
using okeafurniture.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.DAL.EFRepositories
{
    public class EFCategoryRepository : ICategoryRepository
    {
        OkeaFurnitureContext _context;

        public EFCategoryRepository(OkeaFurnitureContext context)
        {
            _context = context;
        }

        public Response<Category> Get(int id)
        {
            var response = new Response<Category>();

            var category = _context.Categories
                .Include(c => c.Items)
                .SingleOrDefault(c => c.CategoryId == id);

            if (category == null)
            {
                response.Success = false;
                response.Message = "Category not found. ";
            }
            else
            {
                response.Success = true;
                response.Data = category;
            }
            return response;
        }

        public Response<List<Category>> GetAll()
        {
            var response = new Response<List<Category>>();

            var categories = _context.Categories
                .Include(c => c.Items)
                .ToList();

            if (categories == null || categories.Count == 0)
            {
                response.Success = false;
                response.Message = "No categories found. ";
            }
            else
            {
                response.Success = true;
                response.Data = categories;
            }
            return response;
        }

        public Response<Category> Add(Category category)
        {
            var response = new Response<Category>();

            _context.Add(category);
            _context.SaveChanges();

            response.Success = true;
            response.Data = category;
            return response;
        }

        public Response Update(Category category)
        {
            var response = new Response();

            if (!Get(category.CategoryId).Success)
            {
                response.Success = false;
                response.Message = "Category not found. ";
            }
            else
            {
                _context.Categories.Update(category);
                _context.SaveChanges();

                response.Success = true;
            }
            return response;
        }
        public Response Delete(int id)
        {
            var response = new Response();

            var category = Get(id).Data;
            if (category == null)
            {
                response.Success = false;
                response.Message = "Category not found. ";
            }
            else
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();

                response.Success = true;
            }
            return response;
        }
    }
}
