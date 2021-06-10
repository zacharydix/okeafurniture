using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using okeafurniture.CORE;
using okeafurniture.CORE.Entites;
using okeafurniture.CORE.Interfaces;

namespace okeafurniture.DAL.EFRepositories
{
    public class EFItemRepository : IItemRepository
    {
        OkeaFurnitureContext _context;

        public EFItemRepository(OkeaFurnitureContext dbcontext)
        {
            _context = dbcontext;
        }
        public Response Delete(int itemId)
        {
            Response aResponse = new Response();

            try
            {
                _context.Item.Remove(_context.Item.Find(itemId));
                foreach (var entry in _context.CategoryItem.Where(a => a.ItemId == itemId))
                {
                    _context.CategoryItem.Remove(entry);
                }
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting item" + ex.Message);
                aResponse.Success = false;
                return aResponse;
            }

            aResponse.Success = true;
            aResponse.Message = "Successfully deleted Item";

            return aResponse;
        }

        public Response<Item> Get(int itemId)
        {
            var response = new Response<Item>();
            try
            {
                var item = _context.Item.Include(a => a.CategoryItems)
                .Include(b => b.CartItems).SingleOrDefault(i => i.ItemId == itemId);

                if (item == null)
                {
                    response.Success = false;
                    response.Message = "Item not found.";
                }
                else
                {
                    response.Success = true;
                    response.Message = "Successfully retrieved Item";
                    response.Data = item;
                }
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Could not locate selected record.";
            }
            return response;
        }

        public Response<List<Item>> GetAll()
        {
            Response<List<Item>> response = new Response<List<Item>>();
            try
            {
                List<Item> items = _context.Item.Include(a => a.CategoryItems)
                    .Include(b => b.CartItems).ToList();
                if (items.Count() == 0)
                {
                    response.Success = false;
                    response.Data = items;
                    response.Message = "Could not locate records.";
                }
                response.Success = true;
                response.Data = items;
                response.Message = "Successfully retrieved Items";
            }
            catch (Exception)
            {
                response.Message = ("Error retrieving items");
                response.Success = false;
                response.Data = null;
            }
            

            return response;
        }

        public Response<List<Item>> GetByCategory(int categoryId)
        {
            Response<List<Item>> response = new Response<List<Item>>();
            try
            {
                List<Item> items = _context.Item.Include(a => a.CategoryItems).Where(b => b.CategoryItems.Intersect(_context.CategoryItem.Where(c => c.CategoryId == categoryId)).Any()).ToList();
                if (items.Count() == 0)
                {
                    response.Success = false;
                    response.Data = items;
                    response.Message = "Could not locate records.";
                }
                response.Success = true;
                response.Data = items;
                response.Message = "Successfully retrieved Items";
            }
            catch (Exception)
            {
                response.Message = ("Error retrieving items");
                response.Success = false;
                response.Data = null;
            }
            return response;
        }

        public Response<Item> Insert(Item item)
        {
            Response<Item> response = new Response<Item>();

            try
            {
                _context.Item.Add(item);
                _context.SaveChanges();
                response.Success = true;
                response.Message = "Successfully inserted Item";
                response.Data = item;
            }
            catch (Exception ex)
            {
                response.Message=("Error inserting item" + ex.Message);
                response.Success = false;
            }
            return response;
        }

        public Response Update(Item item)
        {
            {
                Response response = new Response();

                try
                {

                    _context.Item.Update(item);
                    _context.SaveChanges();
                    
                    response.Success = true;
                    response.Message = "update was successful";


                }
                catch (Exception ex)
                {
                    response.Message=("Error inserting item" + ex.Message);
                    response.Success = false;
                }

                return response;
            }
        }
    }
}
