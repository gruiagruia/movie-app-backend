namespace Models
{
    public class MovieList
    {
        public string MovieListId { get; set; }
        public string  ListName { get; set; }
        public string  ListDescription { get; set; } 
        public string MProfileId { get; set; }
        public List<string> ImbdIds { get; set; }
    }
}

