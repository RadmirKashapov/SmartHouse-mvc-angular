using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHouse.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHouse.WEB.Controllers
{
    [ApiController]
    [Route("api/rooms")]
    public class RoomController : Controller
    {
        private ApplicationContext db;
        public int Count { get; set; } = 0;

        public RoomController(ApplicationContext context)
        {
            this.db = context;
        }
        [HttpGet]
        public IEnumerable<Room> Get()
        {
            return db.Rooms;
        }
        [HttpGet("{id}")]
        public Room Get(int id)
        {
            return db.Rooms.Find(id);
        }
        [HttpPost]
        public IActionResult Post(Room room)
        {
            if (ModelState.IsValid)
            {
                db.Rooms.Add(room);
                Count++;
                db.SaveChanges();
                return Ok(room);
            }
            else return BadRequest(ModelState);
        }
        [HttpPut]
        public IActionResult Put(Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return Ok(room);
            } else return BadRequest(ModelState);
        }

        public IEnumerable<Room> Find(Func<Room, Boolean> predicate)
        {
            return db.Rooms.Where(predicate).ToList(); //linq
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Room room = db.Rooms.Find(id);
            if (room != null)
            {
                db.Rooms.Remove(room);
                Count--;
                foreach (var element in db.Sensors.Where(t => t.RoomId == room.Id))
                {
                    db.Sensors.Remove(element);
                };
                db.SaveChanges();
                return Ok(room);
            }
            return BadRequest(room);
        }
        public int GetCount()
        {
            return Count;
        }

        public IEnumerable<Room> GetSelectedRooms(int houseId)
        {
            var selectedRooms = from t in Get()
                                where t.HouseId == houseId
                                select t;
            return selectedRooms;
        }
    }
}
