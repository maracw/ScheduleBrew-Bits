using System;
using Microsoft.EntityFrameworkCore;
using ScheduleBrewClasses;
using ScheduleBrewClasses.Models;
using NUnit.Framework;
using Microsoft.Extensions.FileSystemGlobbing;

namespace ScheduleBrewTests
{
    [TestFixture]
    public class RecipeTests
	{
        ScheduleBrewContext context;
        Recipe recipe;
        List<Recipe>? recipes;

        [SetUp]
        public void Setup()
        {
            context = new ScheduleBrewContext();
            recipe = new Recipe();
        }

        [Test]
        public void GetAllTest()
        {
            recipes = context.Recipes.OrderBy(b => b.RecipeId).ToList();
            Assert.That(recipes.Count, Is.EqualTo(4));
            Assert.That(recipes[0].Name, Is.EqualTo("Fuzzy Tales Juicy IPA"));
            PrintAll(recipes);
        }

        [Test]
        public void GetRecipeByFullName()
        {
            string name = "Cascade Orange Pale Ale";
            recipes = context.Recipes.Where(r=>r.Name.Contains(name)).ToList();
            Assert.That(recipes.Count, Is.EqualTo(1));
            Assert.That(recipes[0].RecipeId, Is.EqualTo(3));
            //Assert.That(recipes[0].Name, Is.EqualTo("Fuzzy Tales Juicy IPA"));
            PrintAll(recipes);
        }

        [Test]
        public void GetRecipeByPartialName()
        {
            string name = "Cascade";
            recipes = context.Recipes.Where(r => r.Name.Contains(name)).ToList();
            Assert.That(recipes.Count, Is.EqualTo(1));
            Assert.That(recipes[0].RecipeId, Is.EqualTo(3));
            PrintAll(recipes);
        }

        /* ScheduleABrew page does not allow users to create, update, or delete recipes*/


        /*Test for user to see all batches by recipe name*/
               
        [Test]
        public void GetWithBatchesByRecipeName()
        {
            // get the recipe by searching name
            //RecipeID 2 has 3 batches
            recipe = context.Recipes.Include("Batches").Where(r => r.Name.Contains("Krampus")).SingleOrDefault();

            Assert.IsNotNull(recipe);
            Assert.AreEqual(2, recipe.RecipeId);
            Assert.AreEqual(3, recipe.Batches.Count);
            Console.WriteLine(recipe);
            PrintAllBatches(recipe);
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
        }
    }
}

