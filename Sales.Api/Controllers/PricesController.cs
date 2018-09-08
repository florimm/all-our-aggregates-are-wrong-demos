﻿using Microsoft.AspNetCore.Mvc;
using Sales.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricesController : ControllerBase
    {
        [HttpGet]
        [Route("product/{id}")]
        public dynamic Get(int id)
        {
            using (var db = SalesContext.Create())
            {
                var item = db.ProductsPrices
                    .Where(o => o.Id == id)
                    .SingleOrDefault();

                return item;
            }
        }

        [HttpGet]
        [Route("products/{ids}")]
        public IEnumerable<dynamic> Get(string ids)
        {
            using (var db = SalesContext.Create())
            {
                db.Database.EnsureCreated();

                var productIds = ids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();
                var items = db.ProductsPrices
                    .Where(status => productIds.Any(id => id == status.Id))
                    .ToArray();

                return items;
            }
        }
    }
}
