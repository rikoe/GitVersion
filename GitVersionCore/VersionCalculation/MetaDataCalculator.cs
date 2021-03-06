﻿namespace GitVersion.VersionCalculation
{
    using System.Linq;
    using LibGit2Sharp;

    public class MetaDataCalculator : IMetaDataCalculator
    {
        public SemanticVersionBuildMetaData Create(Commit baseVersionSource, GitVersionContext context)
        {
            var qf = new CommitFilter
            {
                Since = context.CurrentCommit,
                Until = baseVersionSource,
                SortBy = CommitSortStrategies.Topological | CommitSortStrategies.Time
            };

            var commitLog = context.Repository.Commits.QueryBy(qf);
            var commitsSinceTag = commitLog.Count();

            return new SemanticVersionBuildMetaData(
                commitsSinceTag,
                context.CurrentBranch.Name,
                context.CurrentCommit.Sha,
                context.CurrentCommit.When());
        }
    }
}