using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            return aResponse;
        }

        public Response<Item> Get(int itemId)
        {
            Response<Item> response = new Response<Item>();
            Item item = _context.Items.Find(itemId);

            response.Data = item;
            response.Success = true;
            return response;
        }

        public Response<List<Item>> GetAll()
        {
            Response<List<Item>> response = new Response<List<Item>>();
            List<Item> items = _context.Items.ToList();
            response.Success = true;
            response.Data = items;

            return response;
        }

        public Response<List<Item>> GetByCategory(int categoryId)
        {
            Response<List<Item>> response = new Response<List<Item>>();

            List<Item> items = _context.Items.Where(z => z.CategoryId == categoryId).ToList();

            response.Data = items;
            response.Success = true;
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
                response.Data = item;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting agent" + ex.Message);
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
