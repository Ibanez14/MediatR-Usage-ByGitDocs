using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediatR___CQRS_Testing.Infrastructure
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> ops):base(ops)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                        .Property(p => p.Id).ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }
    }



    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
