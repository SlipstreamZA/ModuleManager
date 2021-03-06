﻿using System;
using ModuleManager.Progress;

namespace ModuleManager.Patches.PassSpecifiers
{
    public class ForPassSpecifier : IPassSpecifier
    {
        public readonly string mod;
        public readonly UrlDir.UrlConfig urlConfig;

        public ForPassSpecifier(string mod, UrlDir.UrlConfig urlConfig)
        {
            if (mod == string.Empty) throw new ArgumentException("can't be empty", nameof(mod));
            this.mod = mod ?? throw new ArgumentNullException(nameof(mod));
            this.urlConfig = urlConfig ?? throw new ArgumentNullException(nameof(urlConfig));
        }

        public bool CheckNeeds(INeedsChecker needsChecker, IPatchProgress progress)
        {
            if (needsChecker == null) throw new ArgumentNullException(nameof(needsChecker));
            if (progress == null) throw new ArgumentNullException(nameof(progress));
            bool result = needsChecker.CheckNeeds(mod);
            if (!result) progress.NeedsUnsatisfiedFor(urlConfig);
            return result;
        }

        public string Descriptor => $":FOR[{mod.ToUpper()}]";
    }
}
