using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Data
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class AppDb : DbContext
    {
        public AppDb() : base("DefaultConnection")
        {

        }

        public DbSet<Student> std { get; set; }
        public DbSet<UserDetails> std2 { get; set; }

    }
}