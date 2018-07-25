﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GeniusBar.Models;

namespace GeniusBar.Controllers
{
    public class StaticsController : ApiController
    {
        private GeniusBarContext db = new GeniusBarContext();

   
        // GET: /api/data/repair_amount/
        [Route("api/data/repair_amount/")]
        [HttpGet]
        public IHttpActionResult GetUserRepairAmount()
        {
            var re = db.RepairOrders
                .GroupBy(e => new {e.Customer_ID, e.Price})
                .Select(g => new
                {
                    ID = g.Key.Customer_ID,
                    count = g.Count(),
                    tot_price = g.Sum(e => e.Price)
                })
                .Join(db.Users, a=>a.ID, b=>b.ID, (a,b)=>new {a.ID, b.Name, a.count, a.tot_price});
            return Ok(re.ToList());
        }
        
        // GET: /api/data/recycle_amount/
        [Route("api/data/recycle_amount/")]
        [HttpGet]
        public IHttpActionResult GetUserRecycleAmount()
        {
            var re = db.RecycleOrders
                .GroupBy(e => new {e.Customer_ID, e.Price})
                .Select(g => new
                {
                    ID = g.Key.Customer_ID,
                    count = g.Count(),
                    tot_price = g.Sum(e => e.Price)
                })
                .Join(db.Users, a=>a.ID, b=>b.ID, (a,b)=>new {a.ID, b.Name, a.count, a.tot_price});
            return Ok(re.ToList());
        }

    }
}