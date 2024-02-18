

namespace ConsoleTelegram
{
    /// <summary>
    /// Класс, который представляет сущность пользователя
    /// </summary>
    public class User
    {
        /// <summary>
        /// Свойство, которое хранит идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Свойство, которое хранит логин пользователя
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// Свойство, которое хранит телефон пользователя
        /// </summary>
        public string? Phone { get; set; }
        public string? LastName { get; set; }
        public string? FistName { get; set; }

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
        public long ChatId { get; set; }

        /// <summary>
        /// Состояние чата
        /// </summary>
        public ChatMode ChatMode { get; set; }
    }
}
