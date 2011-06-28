using System;
using MvcContrib.TestHelper;
using NUnit.Framework;
using Witinyki.Tests.TestHelpers;

namespace Witinyki.Tests
{
    [TestFixture]
    public class RoutesFixture : BaseFixture
    {
        private readonly string Unknown = Guid.NewGuid().ToString();
        private const string Home = Constants.HomePage;
        private const string DeadHash = "deadbeefdeadbeefdeadbeefdeadbeefdeadbeef";
        private const string Hash = "743defa73fa4136ef6863db67de7b6c56332f315";

        [Test]
        public void wiki()
        {
            var res = RT.Ensure("~/wiki", c => c.Index());
            RT.ResultRedirects(res, "/wiki/" + Home);
        }

        [Test]
        public void wiki_Home()
        {
            var res = RT.Ensure("~/wiki/" + Home, c => c.Browse(Home, null));
            res.AssertViewRendered();
        }

        [Test]
        public void wiki_Home_history()
        {
            var res = RT.Ensure("~/wiki/" + Home + "/" + Constants.HistoryAction, c => c.History(Home));
            res.AssertViewRendered();
        }

        [Test]
        public void wiki_Home_invalidVersion()
        {
            var res = RT.Ensure("~/wiki/" + Home + "/" + DeadHash, c => c.Browse(Home, DeadHash));
            RT.ResultRedirects(res, "/wiki/" + Home);
        }

        [Test]
        public void wiki_Home_version()
        {
            var res = RT.Ensure("~/wiki/" + Home + "/" + Hash, c => c.Browse(Home, Hash));
            res.AssertViewRendered();
        }

        [Test]
        public void wiki_history()
        {
            var res = RT.Ensure("~/wiki/" + Constants.HistoryAction, c => c.History(null));
            res.AssertViewRendered();
        }

        [Test]
        public void wiki_pages()
        {
            var res = RT.Ensure("~/wiki/" + Constants.AllPagesAction, c => c.AllPages());
            res.AssertViewRendered();
        }

        [Test]
        public void wiki_unknownPageName()
        {
            var res = RT.Ensure("~/wiki/" + Unknown, c => c.Browse(Unknown, null));
            RT.ResultRedirects(res, "/wiki/" + Unknown + "/" + Constants.CreateAction);
        }

        [Test]
        public void wiki_unknownPageName_history()
        {
            var res = RT.Ensure("~/wiki/" + Unknown + "/" + Constants.HistoryAction, c => c.History(Unknown));
            RT.ResultRedirects(res, "/wiki/" + Unknown);
        }

        [Test]
        public void wiki_unknownPageName_invalidVersion()
        {
            var res = RT.Ensure("~/wiki/" + Unknown + "/" + DeadHash, c => c.Browse(Unknown, DeadHash));
            RT.ResultRedirects(res, "/wiki/" + Unknown);
        }

        [Test]
        [Ignore("Not implemented yet.")]
        public void wiki_unknownPageName_new()
        {
            var res = RT.Ensure("~/wiki/" + Unknown + "/" + Constants.CreateAction, c => c.Create(Unknown));
            res.AssertViewRendered();
        }

        [Test]
        public void wiki_unknownPageName_validVersion()
        {
            var res = RT.Ensure("~/wiki/" + Unknown + "/" + Hash, c => c.Browse(Unknown, Hash));
            RT.ResultRedirects(res, "/wiki/" + Unknown);
        }
    }
}