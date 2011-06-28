using System.Linq;
using NUnit.Framework;
using Witinyki.Models;
using Witinyki.Tests.TestHelpers;

namespace Witinyki.Tests
{
    [TestFixture]
    public class ServiceFixture : BaseFixture
    {
        [Test]
        public void HomeHasBeenChangedTwice()
        {
            Assert.AreEqual(2, WikiService.FindVersionsOf(Constants.HomePage).Length);
        }

        [Test]
        public void HomeHasBeenModifiedByTwoKnownCommits()
        {
            VersionModel[] findVersionsOf = WikiService.FindVersionsOf(Constants.HomePage);
            CollectionAssert.AreEqual(
                new[] {"2155bd40f7adff8544a6f771584a4e5cc4405916", "743defa73fa4136ef6863db67de7b6c56332f315"},
                findVersionsOf.Select(m => m.Hash));
        }
    }
}