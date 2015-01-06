namespace ImageLoader.Models
{
    public class Image
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public string Name { get; set; }
        public byte[,] Data { get; set; }
    }
}