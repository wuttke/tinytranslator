using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyTranslatorApplicationServer.Model;

namespace TinyTranslatorApplicationServer.Tasks
{
    public class TranslationStatusUtil
    {

        public void CalcBundleTranslationStatusFromResources(ResourceBundle bundle)
        {
            TranslationStatus worstStatus = (TranslationStatus)int.MaxValue;
            foreach (var resource in bundle.Resources)
                if (resource.NeedsTranslation()
                    && resource.WorstTranslationStatus >= 0
                    && resource.WorstTranslationStatus < worstStatus)
                    worstStatus = resource.WorstTranslationStatus;
            if (worstStatus == (TranslationStatus)int.MaxValue)
                worstStatus = TranslationStatus.NO_NEED_TO_TRANSLATE;
            bundle.WorstTranslationStatus = worstStatus;
        }

        public void CalcResourceTranslationStatusFromTranslations(List<ProjectLocale> locales, Resource resource)
        {
            if (LocaleMissing(locales, resource))
            {
                resource.WorstTranslationStatus = TranslationStatus.NOT_TRANSLATED;
                return;
            }

            TranslationStatus worstStatus = (TranslationStatus)int.MaxValue;
            foreach (var translation in resource.Translations)
                if (translation.TranslationStatus < worstStatus)
                    worstStatus = translation.TranslationStatus;

            if (worstStatus == (TranslationStatus)int.MaxValue)
                worstStatus = TranslationStatus.NOT_TRANSLATED;

            resource.WorstTranslationStatus = worstStatus;
        }

        private bool LocaleMissing(List<ProjectLocale> locales, Resource resource)
        {
            foreach (var locale in locales)
                if (!resource.Translations.Any(t => t.Locale == locale.LocaleCode))
                    return true;

            return false;
        }
    }
}