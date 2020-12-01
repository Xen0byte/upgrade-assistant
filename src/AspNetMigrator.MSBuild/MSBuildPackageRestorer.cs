﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AspNetMigrator.Engine;
using Microsoft.Build.Execution;
using Microsoft.Extensions.Logging;

namespace AspNetMigrator.MSBuild
{
    public class MSBuildPackageRestorer : IPackageRestorer
    {
        private const string LockFileName = "project.assets.json";
        private readonly ILogger<MSBuildPackageRestorer> _logger;

        public MSBuildPackageRestorer(ILogger<MSBuildPackageRestorer> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<RestoreOutput> RestorePackagesAsync(IMigrationContext context, CancellationToken token)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var rootElement = await context.GetProjectRootElementAsync(token).ConfigureAwait(false);

            if (rootElement is null)
            {
                throw new ArgumentException("Migration context must include a valid project before restoring packages");
            }

            // Create a project instance and run MSBuild /t:Restore
            var project = new ProjectInstance(rootElement);
            return RestorePackages(project);
        }

        public RestoreOutput RestorePackages(ProjectInstance project)
        {
            if (project is null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            var restoreRequest = new BuildRequestData(project, new[] { "Restore" });
            _logger.LogDebug("Restoring NuGet packages for project {ProjectPath}", project.FullPath);
            var restoreResult = BuildManager.DefaultBuildManager.Build(new BuildParameters(), restoreRequest);
            _logger.LogDebug("MSBuild exited with status {RestoreStatus}", restoreResult.OverallResult);
            if (restoreResult.Exception != null)
            {
                _logger.LogError(restoreResult.Exception, "MSBuild threw an unexpected exception");
                throw restoreResult.Exception;
            }

            // Check for the lock file's existence rather than success since a bad NuGet reference won't
            // prevent other (valid) packages from being restored and we may still have a (partial) lock file.
            var lockFilePath = Path.Combine(project.GetPropertyValue("MSBuildProjectExtensionsPath"), LockFileName);
            if (!Path.IsPathRooted(lockFilePath))
            {
                lockFilePath = Path.Combine(project.Directory, lockFilePath);
            }

            // Get the path used for caching NuGet packages
            var nugetCachePath = project.GetPropertyValue("NuGetPackageRoot");

            return new RestoreOutput(File.Exists(lockFilePath) ? lockFilePath : null, Directory.Exists(nugetCachePath) ? nugetCachePath : null);
        }
    }
}