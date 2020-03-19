using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SmartHouse.WEB.Models;
using System;
using Microsoft.EntityFrameworkCore;

namespace SmartHouse.WEB.Controllers
{
    [ApiController]
    [Route("api/houses")]
    public class HouseController : Controller
    {
        ApplicationContext db;
        public int Count { get; set; } = 0;
        public HouseController(ApplicationContext context)
        {
            db = context;
            if (!db.Houses.Any())
            {
                db.Houses.Add(new House { Name = "House 1" });
                Count++;
                db.Rooms.Add(new Room { Name = "Room 1" });
                Count++;
                db.SaveChanges();
            }
        }
        [HttpGet]
        public IEnumerable<House> Get()
        {
            return db.Houses;
        }
        [HttpGet("{id}")]
        public House Get(int id)
        {
            return db.Houses.Find(id);
        }
        [HttpPost]
        public IActionResult Post(House home)
        {
            if (ModelState.IsValid)
            {
                db.Houses.Add(home);
                db.SaveChanges();
                Count++;
                return Ok(home);
            }
            else return BadRequest(ModelState); 
        }
        [HttpPut]
        public IActionResult Put(House home)
        {
            if (ModelState.IsValid)
            {
                db.Entry(home).State = EntityState.Modified;
                db.SaveChanges();
                return Ok(home);
            }
            else return BadRequest(ModelState);
        }

        public IEnumerable<House> Find(Func<House, Boolean> predicate)
        {
            return db.Houses.Where(predicate).ToList();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            House home = db.Houses.Find(id);
            if (home != null)
            {
                db.Houses.Remove(home);
                Count--;

                foreach (var element in db.Rooms.Where(t => t.HouseId == home.Id))
                {
                    db.Rooms.Remove(element);
                };

                foreach (var element in db.Sensors.Where(t => t.HouseId == home.Id))
                {
                    db.Sensors.Remove(element);
                };
                db.SaveChanges();
            }
            return Ok(home);
        }

        public int GetCount()
        {
            return Count;
        }

        public House GetLast()
        {
            House house = new House
            {
                Name = "Undefined"
            };

            if (Get() != null)
            {
                foreach (var elem in Get())
                {
                    house = elem;
                }
                return house;
            }
            else
            {
                db.Houses.Add(house);
                return house;

            }
        }

    }
}
