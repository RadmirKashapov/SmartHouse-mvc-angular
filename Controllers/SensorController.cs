using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHouse.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSensor.WEB.Controllers
{
    [ApiController]
    [Route("api/sensors")]
    public class SensorController : Controller
    {
        ApplicationContext db;
        private int Count { get; set; } = 0;
        public SensorController(ApplicationContext context)
        {
            db = context;
            if (!db.Sensors.Any())
            {
                db.Sensors.Add(new Sensor { HouseId = 1, RoomId = 0 });
                Count++;
                db.SaveChanges();
            }
        }
        [HttpGet]
        public IEnumerable<Sensor> Get()
        {
            return db.Sensors;
        }
        [HttpGet("{id}")]
        public Sensor Get(int id)
        {
            return db.Sensors.Find(id);
        }
        [HttpPost]
        public IActionResult Post(Sensor sensor)
        {
            if (ModelState.IsValid)
            {
                db.Sensors.Add(sensor);
                db.SaveChanges();
                Count++;
                return Ok(sensor);
            }
            else return BadRequest(ModelState);
        }
        [HttpPut]
        public IActionResult Put(Sensor sensor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sensor).State = EntityState.Modified;
                db.SaveChanges();
                return Ok(sensor);
            }
            else return BadRequest(ModelState);
        }

        public IEnumerable<Sensor> Find(Func<Sensor, Boolean> predicate)
        {
            return db.Sensors.Where(predicate).ToList();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Sensor sensor = db.Sensors.Find(id);
            if (sensor != null)
            {
                db.Sensors.Remove(sensor);
                Count--;
                db.SaveChanges();
                return Ok(sensor);
            }
            return BadRequest(sensor);
        }

        public int GetCount()
        {
            return Count;
        }

        public IEnumerable<Sensor> GetSelectedSensors(int HouseId, int roomId)
        {
            var sensor = from t in Get()
                         where t.HouseId == HouseId && t.RoomId == roomId
                         select t;

            return sensor;
        }
    }
}
