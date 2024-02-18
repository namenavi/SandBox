

namespace ConsoleTelegram
{
    /// <summary>
    /// Класс, который представляет сущность пользователя
    /// </summary>
    public partial class User
    {
        /// <summary>
        /// Свойство, которое хранит идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Свойство, которое хранит имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Свойство, которое хранит список желаний пользователя
        /// </summary>
        public ICollection<Wish>? Wishes { get; set; } = new List<Wish>();
        /// <summary>
        /// Свойство, которое хранит список желаний для исполнения
        /// </summary>
        public ICollection<Wish>? ExecutableWishes { get; set; } = new List<Wish>();
        /// <summary>
        /// Идентификатор чата
        /// </summary>
        public int ChatId { get; set; }
    }
}
