using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.ViewModels
{
    public partial class AboutVM : ObservableObject
    {
        [ObservableProperty]
        public string isFirst;

        [ObservableProperty]
        public string currentVersionIsFirst;

        [ObservableProperty]
        public string currentBuildIsFirst;

        [ObservableProperty]
        public string currentVersion;

        [ObservableProperty]
        public string currentBuild;

        [ObservableProperty]
        public string firstInstalledVer;

        [ObservableProperty]
        public string firstInstalledBuild;

        [ObservableProperty]
        public string versionHistory;

        [ObservableProperty]
        public string buildHistory;

        [ObservableProperty]
        public string previousVersion;

        [ObservableProperty]
        public string previousBuild;

        public AboutVM()
        {
            isFirst = VersionTracking.Default.IsFirstLaunchEver.ToString();
            currentVersionIsFirst = VersionTracking.Default.IsFirstLaunchForCurrentVersion.ToString();
            currentBuildIsFirst = VersionTracking.Default.IsFirstLaunchForCurrentBuild.ToString();
            currentVersion = VersionTracking.Default.CurrentVersion.ToString();
            currentBuild = VersionTracking.Default.CurrentBuild.ToString();
            firstInstalledVer = VersionTracking.Default.FirstInstalledVersion.ToString();
            firstInstalledBuild = VersionTracking.Default.FirstInstalledBuild.ToString();
            versionHistory = String.Join(',', VersionTracking.Default.VersionHistory);
            buildHistory = String.Join(',', VersionTracking.Default.BuildHistory);

            // These two properties may be null if this is the first version
            previousVersion = VersionTracking.Default.PreviousVersion?.ToString() ?? "none";
            previousBuild = VersionTracking.Default.PreviousBuild?.ToString() ?? "none";
        }
    }
}