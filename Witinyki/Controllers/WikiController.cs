using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using Witinyki.Models;

namespace Witinyki.Controllers
{
    public class WikiController : Controller
    {
        public ActionResult Index()
        {
            return this.RedirectToAction(c => c.Browse(Constants.HomePage, null));
        }

        [ActionName(Constants.HistoryAction)]
        public ActionResult History(string pageName)
        {
            ViewBag.PageName = pageName;

            if (!string.IsNullOrEmpty(pageName))
            {
                VersionModel[] versionModels = WikiService.FindVersionsOf(pageName);
                if (versionModels == null)
                {
                    return this.RedirectToAction(c => c.Browse(pageName, null));
                }

                ViewBag.Title = string.Concat("History for ", pageName);
                ViewBag.Versions = versionModels;

                return View(Constants.PageHistoryView);
            }

            ViewBag.Title = "History";
            ViewBag.Commits = WikiService.FindAllCommits();

            return View(Constants.HistoryAction);
        }

        [ActionName(Constants.AllPagesAction)]
        public ActionResult AllPages()
        {
            IEnumerable<string> pages = WikiService.FindAllPages();

            ViewBag.Pages = pages;
            ViewBag.Title = "Pages";

            return View(Constants.AllPagesAction);
        }

        public ActionResult Browse(string pageName, string version)
        {
            byte[] content = WikiService.RetrieveContentOfPage(pageName, version);

            if (content != null)
            {
                ViewBag.Title = pageName;
                ViewBag.Page = new PageModel(pageName, version, content);

                return View(Constants.BrowseAction);
            }

            if (version == null)
            {
                return this.RedirectToAction(c => c.Create(pageName));
            }

            return this.RedirectToAction(c => c.Browse(pageName, null));
        }

        [ActionName(Constants.CreateAction)]
        public ActionResult Create(string pageName)
        {
            throw new NotImplementedException();
        }

        [ActionName(Constants.DeleteAction)]
        public ActionResult Delete(string pageName)
        {
            throw new NotImplementedException();
        }
    }
}