using System;
using Microsoft.EntityFrameworkCore;
using ScheduleBrewClasses;
using ScheduleBrewClasses.Models;

namespace ScheduleBrewTests
{
	public class BatchTests
	{
        ScheduleBrewContext context;
        Batch batch;
        List<Batch>? batches;

        [SetUp]
        public void Setup()
        {
            context = new ScheduleBrewContext();
            batch = new Batch();
        }

        [Test]
        public void GetAllTest()
        {
            batches = context.Batches.OrderBy(b => b.ScheduledStartDate).ToList();
            Assert.That(batches.Count, Is.EqualTo(3));
            Assert.AreEqual(1, batches[0].RecipeId);
            PrintAll(batches);
        }
        [Test]
        public void GetByPrimaryKeyTest()
        {
            batch = context.Batches.Find(1);
            Assert.IsNotNull(batch);
            Assert.AreEqual(5, batch.Volume);
            Assert.That(batch.Volume, Is.EqualTo(5));
            Console.WriteLine(batch);

        }

        [Test]
        public void GetBatchByDate()
        {
            DateTime date = new DateTime(2020,04,01);
            batches = context.Batches.Where(b => b.ScheduledStartDate == date).ToList();
            Assert.That(batches.Count, Is.EqualTo(3));
            DateTime date2 = new DateTime(2021, 04, 01);
            batches = context.Batches.Where(b => b.ScheduledStartDate >= date2).ToList();
            Assert.That(batches.Count, Is.EqualTo(0));
        }

        [Test]
        public void UpdateBatchDateByID()
        {
            batch = context.Batches.Find(2);
            DateTime date = new DateTime(2020, 05, 01);
            batch.ScheduledStartDate = date;
            context.Update(batch);
            context.SaveChanges();

            Assert.That(batch.ScheduledStartDate, Is.EqualTo(date));
            Console.WriteLine(batch);
            
            DateTime date2 = new DateTime(2020, 04, 01);
            batches = context.Batches.Where(b => b.ScheduledStartDate == date2).ToList();
            Assert.That(batches.Count, Is.EqualTo(2));
            PrintAll(batches);
        }

        [Test]
        public void GetBatchByDateRange()
        {
            DateTime date1 = new DateTime(2020, 04, 15);
            DateTime date2 = new DateTime(2021, 05, 15);
            batches = context.Batches.Where(b => b.ScheduledStartDate > date1 && b.ScheduledStartDate < date2).ToList();
            Assert.That(batches.Count, Is.EqualTo(1));
            PrintAll(batches);
        }


        /*Add a new Batch
         look at validation*/

        [Test]
        public void CreateTest()
        {
            batch = new Batch();
            batch.RecipeId = 2;
            batch.EquipmentId = 1;
            batch.Volume = 36;
            batch.Notes = "Created in unit test";
            context.Batches.Add(batch);
            context.SaveChanges();

            batch = context.Batches.Where(b => b.RecipeId == 2 && b.Notes == "Created in unit test" && b.Volume == 36).SingleOrDefault();
          
            Assert.NotNull(context.Batches.Where(b => b.RecipeId == 2 && b.Notes == "Created in unit test" && b.Volume == 36));
            Assert.AreEqual(batch.RecipeId, 2);
            Console.WriteLine(batch);


        }

        /*edit a batch's time*/
        public void PrintAll(List<Batch> batches)
        {
            foreach (Batch b in batches)
            {
                Console.WriteLine(b);
            }
        }
    }
}

