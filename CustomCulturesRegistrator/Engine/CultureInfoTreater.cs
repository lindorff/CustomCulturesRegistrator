using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using CustomCulturesRegistrator.Enums;

namespace CustomCulturesRegistrator.Engine
{
    class CultureInfoTreater
    {
        public static RegistrationResults UnregisterCulture(string cultureCode)
        {
            try
            {
                CultureAndRegionInfoBuilder.Unregister(cultureCode);
            }
            catch (Exception ex)
            {
                return RegistrationResults.AlreadyUnregistered;
            }

            return RegistrationResults.JustUnregistered;
        }

        public static RegistrationResults RegisterCultureBasedOn(string newCultureCode, CultureInfo baseCulture, RegionInfo baseRegionInfo)
        {
            if (string.IsNullOrEmpty(newCultureCode)
                || baseCulture == null
                || baseRegionInfo == null)
            {
                return RegistrationResults.WrongCultureFromat;
            }

            try
            {
                var cultureInfoBuilder = new CultureAndRegionInfoBuilder(newCultureCode, CultureAndRegionModifiers.Neutral);
                cultureInfoBuilder.LoadDataFromCultureInfo(baseCulture);
                cultureInfoBuilder.LoadDataFromRegionInfo(baseRegionInfo);

            
                cultureInfoBuilder.Register();
            }
            catch (Exception ex)
            {
                return RegistrationResults.AlreadyRegistered;
            }

            return RegistrationResults.JustRegistered;
        }

        public static RegistrationResults RegisterCultureBasedOn(string newCultureCode, string languageCode, string regionCode)
        {
            var baseCulture = new CultureInfo(languageCode);
            var baseRegionInfo = new RegionInfo(regionCode);

            return RegisterCultureBasedOn(newCultureCode, baseCulture, baseRegionInfo);
        }

        public static RegistrationResults RegisterCulture(string newCultureCode)
        {
            if (string.IsNullOrEmpty(newCultureCode))
            {
                return RegistrationResults.WrongCultureFromat;
            }

            var baseCultureCode = Regex.Match(newCultureCode, @"^[\w]{2}").Value;
            var baseRegionCode = Regex.Match(newCultureCode, @"(?<=-)[\w]{2}$").Value.ToUpper();
            
            return RegisterCultureBasedOn(newCultureCode, baseCultureCode, baseRegionCode);
        }

        public static bool IsCultureRegistered(string cultureCode)
        {
            try
            {
                var culture = new CultureInfo(cultureCode);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool IsNotCultureRegistered(string cultureCode)
        {
            return !IsCultureRegistered(cultureCode);
        }
    }
}
