using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SmartHouse.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHouse.WEB.Controllers
{
    [ApiController]
    [Route("api/records")]
    public class RecordController : Controller
    {
        ApplicationContext db;
        private int Count { get; set; } = 0;
        public RecordController(ApplicationContext context)
        {
            db = context;
            if (!db.Records.Any())
            {
                db.Records.Add(new Record { Data = 13, Date = DateTimeOffset.Now.AddDays(-1), SensorId = 1 });
                Count++;
                db.Records.Add(new Record { Data = 19, Date = DateTimeOffset.Now.AddDays(-1).AddHours(-1), SensorId = 1 });
                Count++;
                db.Records.Add(new Record { Data = 13, Date = DateTimeOffset.Now.AddDays(-365), SensorId = 1 });
                Count++;
                db.Records.Add(new Record { Data = 19, Date = DateTimeOffset.Now.AddDays(-365).AddHours(-1), SensorId = 1 });
                Count++;
                db.Records.Add(new Record { Data = 13, Date = DateTimeOffset.Now.AddDays(-30), SensorId = 1 });
                Count++;
                db.Records.Add(new Record { Data = 19, Date = DateTimeOffset.Now.AddDays(-30).AddHours(-1), SensorId = 1 });
                Count++;
                db.SaveChanges();
            }
        }
        [HttpGet]
        public IEnumerable<Record> Get()
        {
            return db.Records;
        }
        [HttpGet("{id}")]
        public Record Get(int id)
        {
            return db.Records.Find(id);
        }
        [HttpPost]
        public IActionResult Post(Record record)
        {
            if (ModelState.IsValid)
            {
                db.Records.Add(record);
                Count++;
                db.SaveChanges();
                return Ok(record);
            }
            else return BadRequest(ModelState);
        }
        [HttpPut]
        public IActionResult Put(Record record)
        {
            if (ModelState.IsValid)
            {
                db.Entry(record).State = EntityState.Modified;
                db.SaveChanges();
                return Ok(record);
            }
            else return BadRequest(ModelState);
        }

        public IEnumerable<Record> Find(Func<Record, Boolean> predicate)
        {
            return db.Records.Where(predicate).ToList();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Record record = db.Records.Find(id);
            if (record != null)
            {
                db.Records.Remove(record);
                Count--;
                db.SaveChanges();
                return Ok(record);
            }
            else return BadRequest(record);
        }

        public int GetCount()
        {
            return Count;
        }

        public IEnumerable<int> GetSelectedRecordsPerDay(int sensorId)
        {
            DateTimeOffset endDate = DateTimeOffset.Now;
            DateTimeOffset startDate = endDate.AddDays(-1);

            var x = from t in Get()
                    where t.SensorId == sensorId && t.Date >= startDate && t.Date <= endDate
                    select t.Data;

            return x;
        }

        public IEnumerable<int> GetSelectedRecordsPerMonth(int sensorId)
        {
            DateTimeOffset endDate = DateTimeOffset.Now;
            DateTimeOffset startDate = endDate.AddMonths(-1);

            var x = from t in Get()
                    where t.SensorId == sensorId && t.Date >= startDate && t.Date <= endDate
                    select t.Data;

            return x;
        }

        public IEnumerable<int> GetSelectedRecordsPerYear(int sensorId)
        {
            DateTimeOffset endDate = DateTimeOffset.Now;
            DateTimeOffset startDate = endDate.AddYears(-1);

            var x = from t in Get()
                    where t.SensorId == sensorId && t.Date >= startDate && t.Date <= endDate
                    select t.Data;

            return x;
        }
    }
}
