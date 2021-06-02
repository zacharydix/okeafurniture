﻿using okeafurniture.CORE.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.CORE.Interfaces
{
    public interface IItemCategoryRepository
    {
        public Response<ItemCategory> Get(int id);
        public Response<List<ItemCategory>> GetAllByCategory(int categoryId);
        public Response<ItemCategory> Add(ItemCategory itemCategory);
        public Response Update(ItemCategory itemCategory);
        public Response Delete(int id);
    }
}