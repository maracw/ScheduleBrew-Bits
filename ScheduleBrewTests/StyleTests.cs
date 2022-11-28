using System;
using Microsoft.EntityFrameworkCore;
using ScheduleBrewClasses;
using ScheduleBrewClasses.Models;
using NUnit.Framework;
using Microsoft.Extensions.FileSystemGlobbing;

namespace ScheduleBrewTests
{
    [TestFixture]
    public class StyleTests
	{
        ScheduleBrewContext context;
        Style style;
        List<Style>? styles;

        [SetUp]
        public void Setup()
        {
            context = new ScheduleBrewContext();
            style = new Style();
        }

        [Test]
        public void GetAllTest()
        {
            styles = context.Styles.OrderBy(b => b.StyleId).ToList();
            Assert.That(styles.Count, Is.EqualTo(198));
            Assert.That(styles[0].Name, Is.EqualTo("Fuzzy Tales Juicy IPA"));
            //PrintAll();
        }
        /*

        [Test]
        public void GetRecipeByFullName()
        {
            string name = "Cascade Orange Pale Ale";
            styles = context.Recipes.Where(r=>r.Name.Contains(name)).ToList();
            Assert.That(styles.Count, Is.EqualTo(1));
            Assert.That(styles[0].RecipeId, Is.EqualTo(3));
            //Assert.That(recipes[0].Name, Is.EqualTo("Fuzzy Tales Juicy IPA"));
            PrintAll(styles);
        }

        [Test]
        public void GetRecipeByPartialName()
        {
            string name = "Cascade";
            styles = context.Recipes.Where(r => r.Name.Contains(name)).ToList();
            Assert.That(styles.Count, Is.EqualTo(1));
            Assert.That(styles[0].RecipeId, Is.EqualTo(3));
            PrintAll(styles);
        }
        */
        /* ScheduleABrew page does not allow users to create, update, or delete recipes*/


        /*Test for user to see all batches by recipe name*/

        /*
               
        [Test]
        public void GetWithBatchesByRecipeName()
        {
            // get the recipe by searching name
            //RecipeID 2 has 3 batches
            style = context.Recipes.Include("Batches").Where(r => r.Name.Contains("Krampus")).SingleOrDefault();

            Assert.IsNotNull(style);
            Assert.AreEqual(2, style.RecipeId);
            Assert.AreEqual(3, style.Batches.Count);
            Console.WriteLine(style);
            PrintAllBatches(style);
        }

        
        public void PrintAll(List<Recipe> recipes)
        {
            foreach (Recipe r in recipes)
            {
                Console.WriteLine(r);
            }
        }

        public void PrintAllBatches(Recipe recipe)
        {
            foreach (Batch b in recipe.Batches)
            {
                Console.WriteLine(b);
            }
        }*/
    }
}

