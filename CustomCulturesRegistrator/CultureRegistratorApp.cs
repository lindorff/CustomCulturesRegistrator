using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CustomCulturesRegistrator.AppConfig;
using CustomCulturesRegistrator.Enums;


namespace CustomCulturesRegistrator
{
    class CultureRegistratorApp
    {
        public static void Main(string[] args)
        {
            var culturesToTreatFromCmdParams = args.Where(e => new Regex(@"^[\w]{2}-[\w]{2}$").IsMatch(e)).Distinct().ToList();

            var cultureRegistrator = new CultureRegistrator(!args.Contains("-u"));

            Dictionary<string, RegistrationResults> registrationResults;
            if (culturesToTreatFromCmdParams.Any())
            {
                registrationResults = cultureRegistrator.RegisterListOfCultures(culturesToTreatFromCmdParams);
            }
            else
            {
                registrationResults = cultureRegistrator.RegisterCulturesCountinedInFoldersTree(
                    baseFolder: "..\\..\\", 
                    searchCreterions: ConfigHelper.SearchCreterions);
            }

            Console.Write(BuildReport(registrationResults));
        }

        private static string BuildReport(Dictionary<string, RegistrationResults> registrationResults)
        {
            var reportBuilder = new StringBuilder();

            reportBuilder.AppendLine("Cultures was treated:");
            reportBuilder.AppendLine();
            foreach (var result in registrationResults)
            {
                reportBuilder.AppendFormat("culture: \"{0}\" {1}.", result.Key,
                                           ParseRegistrationResultToMessage(result.Value));
                reportBuilder.AppendLine();
            }

            return reportBuilder.ToString();
        }

        private static string ParseRegistrationResultToMessage(RegistrationResults registrationResults)
        {
            switch (registrationResults)
            {
                case RegistrationResults.WrongCultureFromat:
                    return "has wrong format";
                case RegistrationResults.JustRegistered:
                    return "registered successfully";
                case RegistrationResults.JustUnregistered:
                    return "unregistered successfully";
                case RegistrationResults.AlreadyRegistered:
                    return "wasn't registered because it already exists";
                case RegistrationResults.AlreadyUnregistered:
                    return "wasn't unregistered because it already not exists or it's a base-culture which can't be unregistered";
                default:
                    return registrationResults.ToString();
            }
        }
    }
}
