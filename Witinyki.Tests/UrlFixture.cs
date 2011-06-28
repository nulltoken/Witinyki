using NUnit.Framework;
using Witinyki.Tests.TestHelpers;

namespace Witinyki.Tests
{
    [TestFixture]
    public class UrlFixture : BaseFixture
    {
        private const string pageName = "RandomPage";
        private const string version = "deadbeef";

        [Test]
        public void Wiki_AllPages()
        {
            Assert.AreEqual("/wiki/" + Constants.AllPagesAction, RT.GetLinkFor(c => c.AllPages()));
        }

        [Test]
        public void Wiki_History()
        {
            Assert.AreEqual("/wiki/" + Constants.HistoryAction, RT.GetLinkFor(c => c.History(null)));
        }

        [Test]
        public void Wiki_Index()
        {
            Assert.AreEqual("/wiki", RT.GetLinkFor(c => c.Index()));
        }

        [Test]
        public void Wiki_Page_Browse()
        {
            Assert.AreEqual("/wiki/" + pageName, RT.GetLinkFor(c => c.Browse(pageName, null)));
        }

        [Test]
        public void Wiki_Page_Create()
        {
            Assert.AreEqual("/wiki/" + pageName + "/" + Constants.CreateAction, RT.GetLinkFor(c => c.Create(pageName)));
        }

        [Test]
        public void Wiki_Page_Delete()
        {
            Assert.AreEqual("/wiki/" + pageName + "/" + Constants.DeleteAction, RT.GetLinkFor(c => c.Delete(pageName)));
        }

        [Test]
        public void Wiki_Page_History()
        {
            Assert.AreEqual("/wiki/" + pageName + "/" + Constants.HistoryAction, RT.GetLinkFor(c => c.History(pageName)));
        }

        [Test]
        public void Wiki_VersionOfPage_Browse()
        {
            Assert.AreEqual("/wiki/" + pageName + "/" + version, RT.GetLinkFor(c => c.Browse(pageName, version)));
        }
    }
}