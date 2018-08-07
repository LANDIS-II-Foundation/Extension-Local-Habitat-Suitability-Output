
using Landis.Utilities;
using System.Collections.Generic;

namespace Landis.Extension.Output.LocalHabitat
{
    /// <summary>
    /// Methods for working with the template for filenames of reclass maps.
    /// </summary>
    public static class MapFileNames
    {
        public const string NameVar = "HabitatName";
        public const string TimestepVar = "timestep";

        private static IDictionary<string, bool> knownVars;
        private static IDictionary<string, string> varValues;

        //---------------------------------------------------------------------

        static MapFileNames()
        {
            knownVars = new Dictionary<string, bool>();
            knownVars[NameVar] = true;
            knownVars[TimestepVar] = true;

            varValues = new Dictionary<string, string>();
        }

        //---------------------------------------------------------------------

        public static void CheckTemplateVars(string template)
        {
            OutputPath.CheckTemplateVars(template, knownVars);
        }

        //---------------------------------------------------------------------

        public static string ReplaceTemplateVars(string template,
                                                 string habitatName,
                                                 int    timestep)
        {
            varValues[NameVar] = habitatName;
            varValues[TimestepVar] = timestep.ToString();
            return OutputPath.ReplaceTemplateVars(template, varValues);
        }
        //---------------------------------------------------------------------

        public static string ReplaceTemplateVars(string template,
                                                 string habitatName)
        {
            varValues[NameVar] = habitatName;
            varValues[TimestepVar] = "{timestep}";
            return OutputPath.ReplaceTemplateVars(template, varValues);
        }
    }
}
