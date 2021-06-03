﻿using Microsoft.EntityFrameworkCore;
using okeafurniture.CORE.Entites;
using okeafurniture.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
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
            try
            {
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
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Could not locate selected record. ";                
            }
            return response;
        }

        public Response<List<Category>> GetAll()
        {
            var response = new Response<List<Category>>();
            try
            {
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
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Could not locate selected records. ";
            }
            return response;
        }

        public Response<Category> Add(Category category)
        {
            //data argument
            var response = new Response<Category>();
            try
            {
                _context.Add(category);
                _context.SaveChanges();

                response.Success = true;
                response.Data = category;
            }
            catch (DataException)
            {
                response.Success = false;
                response.Message = "Could not add record. ";
            }
            catch (ArgumentException)
            {
                response.Success = false;
                response.Message = "Could not add record. ";
            }
            return response;
        }

        public Response Update(Category category)
        {
            var response = new Response();
            try
            {
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
            }
            catch (DbUpdateException)
            {
                response.Success = false;
                response.Message = "Could not update selected record. ";
            }
            return response;
        }
        public Response Delete(int id)
        {
            var response = new Response();
            var category = Get(id).Data;

            try
            {
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
            }
            catch (ArgumentNullException)
            {
                response.Success = false;
                response.Message = "Could not locate selected record. ";
            }
            catch (DataException)
            {
                response.Success = false;
                response.Message = "Could not delete selected record. ";
            }
            return response;
        }
    }
}