using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHouse.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHouse.WEB.Controllers
{
    public class UserController : Controller
    {
        ApplicationContext db;
        private int Count { get; set; } = 0;
        public UserController(ApplicationContext context)
        {
            db = context;
        }

        public IEnumerable<User> Get()
        {
            return db.Users;
        }
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return db.Users.Find(id);
        }
        [HttpPost]
        public IActionResult Post(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                Count++;
                return Ok(user);
            }
            else return BadRequest(ModelState);
        }
        [HttpPut]
        public IActionResult Put(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return Ok(user);
            }
            else return BadRequest(ModelState);
        }

        public IEnumerable<User> Find(Func<User, Boolean> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                Count--;
                db.SaveChanges();
            }
            return Ok(user);
        }

    }
}
