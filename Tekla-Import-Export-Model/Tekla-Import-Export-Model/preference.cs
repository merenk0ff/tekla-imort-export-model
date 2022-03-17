namespace Tekla_Import_Export_Model
{
    public class Preference
    {
       public bool ContourPlates { get; set; }
       public bool Beams { get; set; }
       public bool PolyBeams { get; set; }
       public bool BooleanParts { get; set; }
       public bool Fittings { get; set; }
       public bool BoltArrays { get; set; }
       public bool BoltXY{ get; set; }
       public bool Welds{ get; set; }
       public bool CheckPlates{ get; set; }

       public Preference()
       {
           ContourPlates = true;
           Beams = true;
           PolyBeams = true;
           BooleanParts = true;
           Fittings = true;
           BoltArrays = true;
           BoltXY = true;
           Welds = true;
           CheckPlates = true;
       }


    }
}
