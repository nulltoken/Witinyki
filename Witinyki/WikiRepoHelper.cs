using System;
using System.IO;
using LibGit2Sharp;

namespace Witinyki
{
    public static class WikiRepoHelper
    {
        public static bool HasWikiRepoBeenCreated()
        {
            return Directory.Exists(Constants.RepositoryPath);
        }

        public static void BuildSampleWikiRepo()
        {
            var repoPath = Repository.Init(Constants.WorkingDirectory);

            using (var repo = new Repository(repoPath))
            {
                string workDir = repo.Info.WorkingDirectory;

                string homePath = Path.Combine(workDir, "Home");
                var sig = new Signature("A. U. Thor", "thor@valhalla.asgard.com",
                                        new DateTimeOffset(2011, 06, 16, 10, 58, 27, TimeSpan.FromHours(2)));

                CreateHomePage(repo, homePath, sig);

                Signature sig2 = Shift(sig, TimeSpan.FromMinutes(2));
                UpdateHomePageContent(repo, homePath, sig2);

                string myWishListPath = Path.Combine(workDir, "MyWish");

                Signature sig3 = Shift(sig2, TimeSpan.FromMinutes(17));
                CreateMyWishListPage(repo, myWishListPath, sig3);

                Signature sig4 = Shift(sig3, TimeSpan.FromMinutes(31));
                RenameMyWishListPage(repo, myWishListPath, sig4);
            }
        }

        private static void RenameMyWishListPage(Repository repo, string myWishListPath, Signature sig)
        {
            repo.Index.Unstage(myWishListPath);
            File.Move(myWishListPath, myWishListPath + "List");
            repo.Index.Stage(myWishListPath + "List");
            repo.Commit(sig, sig, "Fix MyWishList page name");
        }

        private static void CreateMyWishListPage(Repository repo, string myWishListPath, Signature sig)
        {
            File.AppendAllText(myWishListPath, " - Checkout\n - Clone\n - Push/Pull\n - Diff\n");
            repo.Index.Stage(myWishListPath);
            repo.Commit(sig, sig, "Add MyWishList page");
        }

        private static void UpdateHomePageContent(Repository repo, string homePath, Signature sig)
        {
            File.AppendAllText(homePath, "\nThis will be a bare bone user experience.\n");
            repo.Index.Stage(homePath);
            repo.Commit(sig, sig,
                        "Add warning to the Home page\n\nA very informational explicit message preventing the user from expecting too much.");
        }

        private static void CreateHomePage(Repository repo, string homePath, Signature sig)
        {
            File.AppendAllText(homePath, "Welcome to Witinyki!\n");
            repo.Index.Stage(homePath);
            repo.Commit(sig, sig, "Add Home page");
        }

        private static Signature Shift(Signature signature, TimeSpan offset)
        {
            return new Signature(signature.Name, signature.Email, signature.When.Add(offset));
        }
    }
}