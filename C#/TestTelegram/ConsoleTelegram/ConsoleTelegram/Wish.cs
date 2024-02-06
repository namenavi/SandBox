namespace ConsoleTelegram
{
    public class Wish
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}
