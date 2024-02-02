namespace ConsoleEF.Entities
{
    /// <summary>
    /// Класс, который представляет сущность желания
    /// </summary>
    public class Wish
    {
        /// <summary>
        /// Свойство, которое хранит идентификатор желания
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Свойство, которое хранит название желания
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Свойство, которое хранит статус желания
        /// </summary>
        public WishStatus Status { get; set; } = WishStatus.New;

        public Guid UserId { get; set; }
        /// <summary>
        /// Свойство, которое хранит ссылку на пользователя, которому принадлежит желание
        /// </summary>
        public User User { get; set; }

        public Guid? ExecutorId { get; set; }
        /// <summary>
        /// Свойство, которое хранит ссылку на пользователя, который выбрал желание для исполнения
        /// </summary>
        public User? Executor { get; set; }

        /// <summary>
        /// Свойство, которое хранит дату выбора желания
        /// </summary>
        public DateTime? ChosenDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Свойство, которое хранит дату исполнения желания
        /// </summary>
        public DateTime? DoneDate { get; set; }

        /// <summary>
        /// Свойство, которое хранит оценку исполненного желания
        /// </summary>
        public int? Rating { get; set; }
        /// <summary>
        /// Свойство хранит описание желания
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Конструктор, который принимает название желания
        /// </summary>
        /// <param name="name"></param>
        public Wish(string name)
        {
            Name = name;
            Status = WishStatus.New;
        }

        /// <summary>
        /// Метод, который проверяет, может ли желание быть выбрано для исполнения
        /// </summary>
        /// <returns></returns>
        public bool CanBeChosen()
        {
            return Status == WishStatus.New;
        }

        /// <summary>
        /// Метод, который проверяет, может ли желание быть перемещено в исполненное
        /// </summary>
        /// <returns></returns>
        public bool CanBeMovedToDone()
        {
            return Status == WishStatus.Chosen;
        }

        /// <summary>
        /// Метод, который проверяет, может ли желание быть оценено
        /// </summary>
        /// <returns></returns>
        public bool CanBeRated()
        {
            return Status == WishStatus.Done;
        }

        /// <summary>
        /// Метод, который проверяет, может ли желание быть назначено исполнителем
        /// </summary>
        /// <returns></returns>
        public bool CanBeAssigned()
        {
            return Status == WishStatus.New;
        }

        /// <summary>
        /// Метод, который проверяет, может ли желание быть отменено
        /// </summary>
        /// <returns></returns>
        public bool CanBeCanceled()
        {
            return Status == WishStatus.Chosen || Status == WishStatus.Assigned;
        }
    }
}

