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
                _context.Items.Remove(_context.Items.Find(itemId));
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
            Response<Item> response = new Response<Item>();
            Item item = _context.Items.Include(a => a.Categories)
                .Include(b => b.CartItems).SingleOrDefault(i=>i.ItemId==itemId);

            response.Data = item;
            response.Success = true;
            response.Message = "Successfully retrieved Item";
            return response;
        }

        public Response<List<Item>> GetAll()
        {
            Response<List<Item>> response = new Response<List<Item>>();
            List<Item> items = _context.Items.ToList();
            response.Success = true;
            response.Data = items;
            response.Message = "Successfully retrieved Items";

            return response;
        }

        public Response<List<Item>> GetByCategory(int categoryId)
        {
            Response<List<Item>> response = new Response<List<Item>>();

            List<Item> items = _context.Items.Include(a => a.Categories).Where(i => i.Categories.Any(p => p.CategoryId == categoryId)).ToList();

            response.Data = items;
            response.Success = true;
            response.Message = "Successfully retrieved item's category";
            return response;
        }

        public Response<Item> Insert(Item item)
        {
            Response<Item> response = new Response<Item>();

            try
            {
                _context.Items.Add(item);
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

                    _context.Items.Update(item);
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
