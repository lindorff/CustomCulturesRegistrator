using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CustomCulturesRegistrator.AppConfig;
using CustomCulturesRegistrator.Engine;
using CustomCulturesRegistrator.Enums;

namespace CustomCulturesRegistrator
{
    public class CultureRegistrator
    {
        private bool isRegisteringProcess = true;

        public CultureRegistrator(bool registering)
        {
            isRegisteringProcess = registering;
        }

        public virtual List<string> SearchUnregisteredCulturesIn(string path, string[] searchCriterions)
        {
            var resourceFiles = ResourceFilesSearcher.SearchResourcesInDirectory(path, searchCriterions);
            var cultureCodes = ResourceFilesSearcher.GetCulturesFromResourcesFileNames(resourceFiles);

            return cultureCodes.Where(CultureInfoTreater.IsNotCultureRegistered).Distinct().ToList();
        }

        public virtual RegistrationResults TreatCulture(string culture)
        {
            return isRegisteringProcess 
                ? CultureInfoTreater.RegisterCulture(culture) 
                : CultureInfoTreater.UnregisterCulture(culture);
        }

        public virtual Dictionary<string, RegistrationResults> RegisterCulturesCountinedInFoldersTree(string baseFolder, string[] searchCreterions)
        {
            var searchingPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, baseFolder));
            var culturesToTreat = SearchUnregisteredCulturesIn(searchingPath, searchCreterions);

            var registrationResults = culturesToTreat.Select(TreatCulture).ToList();
            var result = registrationResults.Zip(culturesToTreat, (rr, c) => new { registrationResults = rr, culture = c })
                                            .ToDictionary(e => e.culture, e => e.registrationResults);

            return result;
        }

        public virtual Dictionary<string, RegistrationResults> RegisterListOfCultures(List<string> culturesToTreatFromCmdParams)
        {
            var registrationResults = culturesToTreatFromCmdParams.Select(TreatCulture)
                    .Zip(culturesToTreatFromCmdParams, (rr, c) => new { registrationResult = rr, culture = c })
                    .ToDictionary(e => e.culture, e => e.registrationResult);

            return registrationResults;
        }
    }
}
