using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BLL.Interfaces;
using NLog;
using ILogger = NLog.ILogger;
using LogLevel = NLog.LogLevel;

namespace BLL
{
    public class TradeValidator : IValidator<string>
    {
        private ILogger logger;
        private readonly List<RegionInfo> regions = new List<RegionInfo>(CultureInfo.GetCultures(CultureTypes.AllCultures)
            .Where(x => !x.Equals(CultureInfo
                .InvariantCulture))
            .Where(x => !x.IsNeutralCulture) 
            .Select(x => new RegionInfo(x.LCID)));

        public TradeValidator()
        {
            this.logger = LogManager.GetCurrentClassLogger();
            var config = new NLog.Config.LoggingConfiguration();

            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "logfile.txt" };
            var logConsole = new NLog.Targets.ConsoleTarget("logConsole");
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logConsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
            logConsole.Dispose();
            logfile.Dispose();
            NLog.LogManager.Configuration = config;
        }

        public bool Validate(string obj)
        {
            string[] objArray = obj.Split(',');
            if (objArray.Length != 3)
            {
                this.logger.Error($"{obj} - There are more or less than two commas in the string.");
                return false;
            }

            int checkInt;
            double checkDouble;
            if (objArray[0].Length == 6)
            {
                if (!double.TryParse(objArray[1], out checkDouble))
                {
                    this.logger.Error($"{obj} - Could not parse trade price.");
                    return false;
                }

                if (!int.TryParse(objArray[1], out checkInt))
                {
                    this.logger.Error($"{obj} - Could not parse trades number.");
                    return false;
                }

                bool isFirstRegionCorrect = false, isSecondRegionCorrect = false;
                string firstRegion = objArray[0].Substring(0, 3);
                string secondRegion = objArray[0].Substring(3, 3);
                foreach (var region in regions)
                {
                    if (region.ISOCurrencySymbol.Equals(firstRegion, StringComparison.InvariantCultureIgnoreCase))
                    {
                        isFirstRegionCorrect = true;
                    }

                    if (region.ISOCurrencySymbol.Equals(secondRegion, StringComparison.InvariantCultureIgnoreCase))
                    {
                        isSecondRegionCorrect = true;
                    }

                    if (isFirstRegionCorrect && isSecondRegionCorrect)
                    {
                        return true;
                    }
                }

                if (isFirstRegionCorrect)
                {
                    this.logger.Error($"{obj} - Second region is incorrect.");
                    return false;
                }

                if (isSecondRegionCorrect)
                {
                    this.logger.Error($"{obj} - First region is incorrect.");
                    return false;
                }
            }

            this.logger.Error($"{obj} - Region names are incorrect.");
            return false;
        }
    }
}
