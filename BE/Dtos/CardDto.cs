namespace SummerPracticeWebApi.Dtos
{
    public class CardDto
    {
        public uint Id { get; set; }
        public string CardNumber { get; set; }
        public string CardHolder { get; set; }
        public DateOnly Validity { get; set; }
    }
}
