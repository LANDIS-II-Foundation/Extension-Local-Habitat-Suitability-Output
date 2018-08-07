using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Landis.Library.Metadata;
using Landis.Utilities;
using Landis.Core;
using System.IO;

namespace Landis.Extension.Output.LocalHabitat
{
    public static class MetadataHandler
    {

        
        public static ExtensionMetadata Extension {get; set;}

        public static void InitializeMetadata(int Timestep, string SpeciesMapFileName, IEnumerable<ISuitabilityParameters> suitabilityParams, ICore mCore)
        {
            ScenarioReplicationMetadata scenRep = new ScenarioReplicationMetadata() {
                RasterOutCellArea = PlugIn.ModelCore.CellArea,
                TimeMin = PlugIn.ModelCore.StartTime,
                TimeMax = PlugIn.ModelCore.EndTime//,
                //ProjectionFilePath = "Projection.?" 
            };

            Extension = new ExtensionMetadata(mCore){
                Name = PlugIn.PlugInName,
                TimeInterval = Timestep, 
                ScenarioReplicationMetadata = scenRep
            };

            
            //---------------------------------------
            //          table outputs:   
            //---------------------------------------

            // NONE
                        
            //---------------------------------------            
            //          map outputs:         
            //---------------------------------------

            foreach (SuitabilityParameters habitatModel in suitabilityParams)
            {
                
                string sppMapPath = MapFileNames.ReplaceTemplateVars(SpeciesMapFileName, habitatModel.HabitatName);
                
                OutputMetadata mapOut_LocalHabitat = new OutputMetadata()
                {
                    Type = OutputType.Map,
                    Name = ("Local Habitat Map: " + habitatModel.HabitatName),
                    //sppModel.Name,
                    FilePath = @sppMapPath,
                    Map_DataType = MapDataType.Continuous,
                    Map_Unit = "Habitat Class",
                    Visualize = true,
                };
                Extension.OutputMetadatas.Add(mapOut_LocalHabitat);
            }
            //---------------------------------------
            MetadataProvider mp = new MetadataProvider(Extension);
            mp.WriteMetadataToXMLFile("Metadata", Extension.Name, Extension.Name);




        }
    }
}
