namespace ConsolespaceshipsServer
{
    public class SectorTransform
    {

        public Vector3Int sector;

        
        public SectorTransform ()
        {
            sector.x = 0;
            sector.y = 0;
            sector.z = 0;
        }

        public SectorTransform (int x, int y, int z)
        {
            sector.x = x;
            sector.y = y;
            sector.z = z;
        }

        public override string ToString()
        {
            return sector.ToString();
        }


    }
}