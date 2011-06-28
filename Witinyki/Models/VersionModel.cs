using LibGit2Sharp;

namespace Witinyki.Models
{
    public class VersionModel : CommitModel
    {
        public VersionModel(string pageName, Commit commit) : base(commit)
        {
            PageName = pageName;
        }

        public string PageName { get; set; }
    }
}