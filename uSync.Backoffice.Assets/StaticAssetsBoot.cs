﻿
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Manifest;

using uSync.BackOffice;

namespace uSync.Backoffice.Assets
{
    public class StaticAssetsBoot : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.AdduSyncStaticAssets();
        }
    }

    public static class uSyncStaticAssetsExtensions
    {
        public static IUmbracoBuilder AdduSyncStaticAssets(this IUmbracoBuilder builder)
        {
            // don't add if the filter is already there .
            if (builder.ManifestFilters().Has<uSyncAssetManifestFilter>())
                return builder;

            // add the package manifest programatically. 
            builder.ManifestFilters().Append<uSyncAssetManifestFilter>();

            return builder;
        }
    }

    internal class uSyncAssetManifestFilter : IManifestFilter
    {
        public void Filter(List<PackageManifest> manifests)
        {
            manifests.Add(new PackageManifest
            {
                PackageName = uSyncConstants.Package.Name,
                Version = typeof(uSyncAssetManifestFilter).Assembly.GetName().Version.ToString(3),
                AllowPackageTelemetry = true,
                BundleOptions = BundleOptions.None,
                Scripts = new[]
                {
                    uSyncConstants.Package.PluginPath + "/usync.10.0.0.min.js"
                },
                Stylesheets = new[]
                {
                    uSyncConstants.Package.PluginPath + "/usync.10.0.0.min.css"
                }
            }); ;
        }
    }
}
