﻿using System.Reflection;
using Abp.BackgroundJobs;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Zero;
using Abp.Zero.Configuration;
using EasyFast.Common.Extend.Job;
using EasyFast.Core.Authorization;
using EasyFast.Core.Authorization.Roles;
using EasyFast.Core.HtmlGenreate;
using EasyFast.Core.MultiTenancy;
using EasyFast.Core.Users;

namespace EasyFast.Core
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class EasyFastCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            //Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            //Remove the following line to disable multi-tenancy.
            Configuration.MultiTenancy.IsEnabled = false;

            //Add/remove localization sources here
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    EasyFastConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "EasyFast.Core.Localization.Source"
                        )
                    )
                );

            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Authorization.Providers.Add<EasyFastAuthorizationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            var jobManager = IocManager.Resolve<IBackgroundJobManager>();
            //每天凌晨两点进行文件清理
            jobManager.AddOrUpdate<CleanStaticFileJob, int?>(null, PeriodicCorn.Daily(2));

            base.PostInitialize();
        }
    }
}
