using System.IO;
using System.Web;

namespace Witinyki
{
    public static class Constants
    {
        public const string HomePage = "Home";

        private const string ActionPrefix = "_";
        public const string BrowseAction = ActionPrefix + "browse";
        public const string DeleteAction = ActionPrefix + "delete";
        public const string AllPagesAction = ActionPrefix + "pages";
        public const string HistoryAction = ActionPrefix + "history";
        public const string CreateAction = ActionPrefix + "new";
        public const string PageHistoryView = ActionPrefix + "pageHistory";
        public static readonly string RepositoryPath;
        public static readonly string WorkingDirectory;

        static Constants()
        {
            WorkingDirectory = GetPathToWorkingDirectory();
            RepositoryPath = Path.Combine(WorkingDirectory, ".git");
        }

        private static string GetPathToWorkingDirectory()
        {
            if (HttpContext.Current == null)
            {
                return "./WikiRepo";
            }

            return HttpContext.Current.Server.MapPath("~/App_Data/WikiRepo");
        }
    }
}