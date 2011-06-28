using System;
using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;

namespace Witinyki.Models
{
    public class WikiService
    {
        public static string[] FindAllPages()
        {
            using (var repo = new Repository(Constants.RepositoryPath))
            {
                if (repo.Info.IsEmpty)
                {
                    return new string[] {};
                }

                return repo.Head.Tip.Tree.Where(t => t.Target is Blob).Select(t => t.Name).ToArray();
            }
        }

        public static CommitModel[] FindAllCommits()
        {
            using (var repo = new Repository(Constants.RepositoryPath))
            {
                if (repo.Info.IsEmpty)
                {
                    return new VersionModel[] {};
                }

                return repo.Commits.Select(c => new CommitModel(c)).ToArray();
            }
        }

        public static byte[] RetrieveContentOfPage(string pageName, string version)
        {
            Func<Repository, Tree> treeFinder = r => r.Head.Tip.Tree;

            using (var repo = new Repository(Constants.RepositoryPath))
            {
                if (!string.IsNullOrEmpty(version))
                {
                    var commit = repo.Lookup<Commit>(version);
                    if (commit == null)
                    {
                        return null;
                    }

                    treeFinder = r => commit.Tree;
                }

                return RetrieveContentOfBlobNamed(treeFinder(repo), pageName);
            }
        }

        private static byte[] RetrieveContentOfBlobNamed(IEnumerable<TreeEntry> tree, string pageName)
        {
            var blob = RetrieveBlobNamed(tree, pageName);

            return blob == null ? null : blob.Content;
        }

        private static Blob RetrieveBlobNamed(IEnumerable<TreeEntry> tree, string pageName)
        {
            TreeEntry treeEntry = FindTreeEntry(tree, pageName);

            if (treeEntry == null)
            {
                return null;
            }

            return (Blob) treeEntry.Target;
        }

        private static TreeEntry FindTreeEntry(IEnumerable<TreeEntry> tree, string pageName)
        {
            //TODO: Remove this once this bug is fixed in LibGit2Sharp and replace with tree[pagename]
            return tree.FirstOrDefault(treeEntry => string.Equals(treeEntry.Name, pageName, StringComparison.Ordinal));
        }

        public static VersionModel[] FindVersionsOf(string pageName)
        {
            using (var repo = new Repository(Constants.RepositoryPath))
            {
                var stack = new Stack<Tuple<Commit, ObjectId>>();
                foreach (Commit commit in repo.Commits)
                {
                    Blob blob = RetrieveBlobNamed(commit.Tree, pageName);
                    if (blob == null)
                    {
                        break;
                    }
                    if (stack.Count == 0)
                    {
                        stack.Push(new Tuple<Commit, ObjectId>(commit, blob.Id));
                        continue;
                    }

                    if (stack.Peek().Item2 == blob.Id)
                    {
                        stack.Pop();
                        stack.Push(new Tuple<Commit, ObjectId>(commit, blob.Id));
                        continue;
                    }
                    stack.Push(new Tuple<Commit, ObjectId>(commit, blob.Id));
                }

                if (stack.Count == 0)
                {
                    return null;
                }

                return stack.Reverse().Select(t => new VersionModel(pageName, t.Item1)).ToArray();
            }
        }
    }
}