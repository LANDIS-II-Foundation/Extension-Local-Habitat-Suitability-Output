
using Landis.Library.UniversalCohorts;


namespace Landis.Extension.Output.LocalHabitat
{
    /// <summary>
    /// Methods for computing biomass for groups of cohorts.
    /// </summary>
    public static class Util
    {
        public static int ComputeBiomass(ISpeciesCohorts cohorts)
        {
            int total = 0;
            if (cohorts != null)
                foreach (ICohort cohort in cohorts)
                    total += cohort.Data.Biomass;
            return total;
        }

        //---------------------------------------------------------------------

        public static int ComputeBiomass(ISiteCohorts cohorts)
        {
            int total = 0;
            if (cohorts != null)
                foreach (ISpeciesCohorts speciesCohorts in cohorts)
                    total += ComputeBiomass(speciesCohorts);
            return total;
        }
    }
}
