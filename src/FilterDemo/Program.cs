using FilterDemo.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilterDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {

            // set up db and test data
            var context = new FilterDemoContext();
            await context.Database.EnsureCreatedAsync();
            context.People.Add(new Person(0, "James", "Smith", "London", 42));
            context.People.Add(new Person(0, "James", "World", "London", 47));
            context.People.Add(new Person(0, "Derek", "Jones", "London", 47));
            await context.SaveChangesAsync();


            var repo = new PersonRepository(context);

            // setup some filters
            var filters = new Dictionary<string, object[]>
            {
                { "Age", new object[] { 42, 47 } },
                { "FirstName", new object[] { "James" } }
            };

            var people = await repo.GetAllAsync(filters);

            foreach (var person in people) Console.WriteLine(person);

            await context.Database.EnsureDeletedAsync();
        }
    }
}
