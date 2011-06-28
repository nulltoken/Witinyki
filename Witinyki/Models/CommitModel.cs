using System;
using LibGit2Sharp;

namespace Witinyki.Models
{
    public class CommitModel
    {
        public CommitModel(Commit commit)
        {
            Hash = commit.Id.Sha;
            Who = commit.Author.Name;
            When = commit.Author.When;
            What = commit.MessageShort;
        }

        public string Hash { get; private set; }
        public string Who { get; private set; }
        public DateTimeOffset When { get; private set; }
        public string What { get; private set; }
    }
}