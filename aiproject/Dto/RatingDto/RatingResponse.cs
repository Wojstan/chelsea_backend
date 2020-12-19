namespace aiproject.Dto
{
    public class RatingResponse
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int Appearance { get; set; }
        public RatingPlayerResponse Player { get; set; }
    }

    public class RatingPlayerResponse
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Img { get; set; }
        
    }
}