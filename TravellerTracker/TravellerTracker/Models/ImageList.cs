namespace TravellerTracker.Models
{
    public class ImageList
    {
        public int ImageListID { get; set; }
        public int ShipID { get; set; }
        public int WorldID { get; set; }
        public byte[] theImage { get; set; }
        public string Description { get; set; }
    }
}
