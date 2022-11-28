using System;
using Microsoft.EntityFrameworkCore;
using ScheduleBrewClasses;
using ScheduleBrewClasses.Models;
using NUnit.Framework;

namespace ScheduleBrewTests
{
    [TestFixture]
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
        /*Test for user to see all batches by recipe name*/
           
        [Test]
        public void GetBatchesByRecipeName()
        {
            // get the batches by searching recipe name
            //RecipeID 2 has 3 batches
            batches = context.Batches.Include("Recipe").Where(b => b.Recipe.Name.Contains("Krampus")).ToList();

            Assert.IsNotNull(batches);
            Assert.AreEqual(2, batches[0].RecipeId);
            Assert.AreEqual(3, batches.Count);
            PrintAllWithRecipeName(batches);
        }

        public void GetBatchesByStyleName()
        {
            // get the batches by searching recipe name
            //RecipeID 2 has 3 batches
            batches = context.Batches.Include("Recipe").Where(b => b.Recipe.Name.Contains("Krampus")).ToList();

            Assert.IsNotNull(batches);
            Assert.AreEqual(2, batches[0].RecipeId);
            Assert.AreEqual(3, batches.Count);
            PrintAllWithRecipeName(batches);
        }
        /*edit a batch's time*/
        public void PrintAll(List<Batch> batches)
        {
            foreach (Batch b in batches)
            {
                Console.WriteLine(b);
            }
        }
        public void PrintAllWithRecipeName(List<Batch> batches)
        {
            foreach (Batch b in batches)
            {
                Console.WriteLine(b);
                Console.WriteLine(b.Recipe);
            }
        }
    }
}

