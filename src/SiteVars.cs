using Landis.Library.UniversalCohorts;
using Landis.SpatialModeling;
using System.Collections.Generic;

namespace Landis.Extension.Output.LocalHabitat
{
    public static class SiteVars
    {
        private static ISiteVar<SiteCohorts> cohorts;

        private static ISiteVar<string> prescriptionName;
        private static ISiteVar<byte> fireSeverity;

        private static ISiteVar<Dictionary<int, int>> yearOfFire;
        private static ISiteVar<Dictionary<int, int>> yearOfHarvest;
        private static ISiteVar<int[]> dominantAge;
        private static ISiteVar<Dictionary<int, int[]>> forestType;
        private static ISiteVar<Dictionary<int, double>> suitabilityValue;
        private static ISiteVar<Dictionary<int, int>> ageAtFireYear;
        private static ISiteVar<Dictionary<int, int>> ageAtHarvestYear;
        private static ISiteVar<Dictionary<int, double>> suitabilityWeight;
        private static ISiteVar<Dictionary<int, int>> forestTypeAtFireYear;
        private static ISiteVar<Dictionary<int, int>> forestTypeAtHarvestYear;
        private static ISiteVar<int> timeOfLastHarvest;
        private static ISiteVar<int> timeOfLastFire;



        

        //---------------------------------------------------------------------

        public static void Initialize(int suitabilityCount)
        {
            cohorts = PlugIn.ModelCore.GetSiteVar<SiteCohorts>("Succession.UniversalCohorts");
            prescriptionName = PlugIn.ModelCore.GetSiteVar<string>("Harvest.PrescriptionName");
            timeOfLastHarvest = PlugIn.ModelCore.GetSiteVar<int>("Harvest.TimeOfLastEvent");
            fireSeverity = PlugIn.ModelCore.GetSiteVar<byte>("Fire.Severity");
            timeOfLastFire = PlugIn.ModelCore.GetSiteVar<int>("Fire.TimeOfLastEvent");
            if (timeOfLastFire == null)
                timeOfLastFire = PlugIn.ModelCore.GetSiteVar<int>("Fire.TimeOfLastFire");  // Temporary fix for Base Fire - should be renamed Fire.TimeOfLastEvent - Base Fire Issue #12

            yearOfFire = PlugIn.ModelCore.Landscape.NewSiteVar<Dictionary<int,int>>();
            ageAtFireYear = PlugIn.ModelCore.Landscape.NewSiteVar<Dictionary<int, int>>();
            yearOfHarvest = PlugIn.ModelCore.Landscape.NewSiteVar<Dictionary<int, int>>();
            ageAtHarvestYear = PlugIn.ModelCore.Landscape.NewSiteVar<Dictionary<int, int>>();
            dominantAge = PlugIn.ModelCore.Landscape.NewSiteVar<int[]>();
            forestType = PlugIn.ModelCore.Landscape.NewSiteVar<Dictionary<int, int[]>>();
            forestTypeAtFireYear = PlugIn.ModelCore.Landscape.NewSiteVar<Dictionary<int, int>>();
            forestTypeAtHarvestYear = PlugIn.ModelCore.Landscape.NewSiteVar<Dictionary<int, int>>();
            suitabilityValue = PlugIn.ModelCore.Landscape.NewSiteVar<Dictionary<int, double>>();
            suitabilityWeight = PlugIn.ModelCore.Landscape.NewSiteVar<Dictionary<int, double>>();


            //if (cohorts == null)
            //{
            //    string mesg = string.Format("Cohorts are empty.  Please double-check that this extension is compatible with your chosen succession extension.");
            //    throw new System.ApplicationException(mesg);
            //}

            foreach (Site site in PlugIn.ModelCore.Landscape.ActiveSites)
            {
                Dictionary<int, int> yofDict = new Dictionary<int, int>();
                for (int index = 0; index < suitabilityCount; index++)
                {
                    yofDict.Add(index, -99999);
                }
                SiteVars.YearOfFire[site] = yofDict;

                Dictionary<int, int> ageFireDict = new Dictionary<int, int>();
                for (int index = 0; index < suitabilityCount; index++)
                {
                    ageFireDict.Add(index, 0);
                }
                SiteVars.AgeAtFireYear[site] = ageFireDict;

                Dictionary<int, int> yohDict = new Dictionary<int, int>();
                for (int index = 0; index < suitabilityCount; index++)
                {
                    yohDict.Add(index, 0);
                }
                SiteVars.YearOfHarvest[site] = yohDict;

                Dictionary<int, int> ageHarvestDict = new Dictionary<int, int>();
                for (int index = 0; index < suitabilityCount; index++)
                {
                    ageHarvestDict.Add(index, 0);
                }
                SiteVars.AgeAtHarvestYear[site] = ageHarvestDict;

                int[] domAgeArray = new int[2];
                SiteVars.DominantAge[site] = domAgeArray;

                Dictionary<int, int> forestTypeFireDict = new Dictionary<int, int>();
                for (int index = 0; index < suitabilityCount; index++)
                {
                    forestTypeFireDict.Add(index, 0);
                }
                SiteVars.ForestTypeAtFireYear[site] = forestTypeFireDict;

                Dictionary<int, int> forestTypeHarvestDict = new Dictionary<int, int>();
                for (int index = 0; index < suitabilityCount; index++)
                {
                    forestTypeHarvestDict.Add(index, 0);
                }
                SiteVars.ForestTypeAtHarvestYear[site] = forestTypeHarvestDict;

                Dictionary<int, double> suitValDict = new Dictionary<int, double>();
                for (int index = 0; index < suitabilityCount; index++)
                {
                    suitValDict.Add(index, 0.0);
                }
                SiteVars.SuitabilityValue[site] = suitValDict;

                Dictionary<int, double> suitWtDict = new Dictionary<int, double>();
                for (int index = 0; index < suitabilityCount; index++)
                {
                    suitWtDict.Add(index, 0.0);
                }
                SiteVars.SuitabilityWeight[site] = suitWtDict;

                Dictionary<int, int[]> forTypeDict = new Dictionary<int, int[]>();
                for (int index = 0; index < suitabilityCount; index++)
                {
                    int[] forTypeArray = new int[2];
                    forTypeDict.Add(index, forTypeArray);
                }
                SiteVars.ForestType[site] = forTypeDict;
            }
        }
       
        //---------------------------------------------------------------------
        public static ISiteVar<SiteCohorts> Cohorts
        {
            get
            {
                return cohorts;
            }
        }
        //---------------------------------------------------------------------
        public static ISiteVar<string> PrescriptionName
        {
            get
            {
                return prescriptionName;
            }
        }
        //---------------------------------------------------------------------
        public static ISiteVar<int> TimeOfLastHarvest
        {
            get
            {
                return timeOfLastHarvest;
            }
        }
        //---------------------------------------------------------------------
        public static ISiteVar<byte> FireSeverity
        {
            get
            {
                return fireSeverity;
            }
        }
        //---------------------------------------------------------------------
        public static ISiteVar<int> TimeOfLastFire
        {
            get
            {
                return timeOfLastFire;
            }
        }

        //---------------------------------------------------------------------
        public static ISiteVar<Dictionary<int, int>> YearOfFire
        {
            get
            {
                return yearOfFire;
            }
            set
            {
                yearOfFire = value;
            }
        }
        //---------------------------------------------------------------------
        public static ISiteVar<Dictionary<int, int>> AgeAtFireYear
        {
            get
            {
                return ageAtFireYear;
            }
            set
            {
                ageAtFireYear = value;
            }
        }
        //---------------------------------------------------------------------
        public static ISiteVar<Dictionary<int, int>> YearOfHarvest
        {
            get
            {
                return yearOfHarvest;
            }
            set
            {
                yearOfHarvest = value;
            }
        }
        //---------------------------------------------------------------------
        public static ISiteVar<Dictionary<int, int>> AgeAtHarvestYear
        {
            get
            {
                return ageAtHarvestYear;
            }
            set
            {
                ageAtHarvestYear = value;
            }
        }
        //---------------------------------------------------------------------
        // Dictionary with key equal the index of the suitability file and 
        // value equal to an array of this year's and last year's dominant age class
        // [0] is this year, [1] is last year
        public static ISiteVar<int[]> DominantAge
        {
            get
            {
                return dominantAge;
            }
            set
            {
                dominantAge = value;
            }
        }
        //---------------------------------------------------------------------
        // Dictionary with key equal the index of the suitability file and 
        // value equal to an array of this year's and last year's dominant forest type
        // [0] is this year, [1] is last year
        public static ISiteVar<Dictionary<int, int[]>> ForestType
        {
            get
            {
                return forestType;
            }
            set
            {
                forestType = value;
            }
        }
        //---------------------------------------------------------------------
        public static ISiteVar<Dictionary<int, int>> ForestTypeAtFireYear
        {
            get
            {
                return forestTypeAtFireYear;
            }
            set
            {
                forestTypeAtFireYear = value;
            }
        }
        //---------------------------------------------------------------------
        public static ISiteVar<Dictionary<int, int>> ForestTypeAtHarvestYear
        {
            get
            {
                return forestTypeAtHarvestYear;
            }
            set
            {
                forestTypeAtHarvestYear = value;
            }
        }
        //---------------------------------------------------------------------
        // Dictionary with key equal the index of the suitability file and 
        // value equal to the calculated suitability
        public static ISiteVar<Dictionary<int, double>> SuitabilityValue
        {
            get
            {
                return suitabilityValue;
            }
            set
            {
                suitabilityValue = value;
            }
        }
        //---------------------------------------------------------------------
        // Dictionary with key equal the index of the suitability file and 
        // value equal to the suitability weight
        public static ISiteVar<Dictionary<int, double>> SuitabilityWeight
        {
            get
            {
                return suitabilityWeight;
            }
            set
            {
                suitabilityWeight = value;
            }
        }
        //---------------------------------------------------------------------
    }
}
