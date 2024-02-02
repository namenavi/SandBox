namespace ConsoleEF.Entities
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

        ///// <summary>
        ///// Конструктор, который принимает имя пользователя
        ///// </summary>
        ///// <param name="name"></param>
        //public User(string name)
        //{
        //    Name = name;
        //    Wishes = new List<Wish>();
        //}

        ///// <summary>
        ///// Метод, который добавляет желание в список желаний пользователя
        ///// </summary>
        ///// <param name="wish"></param>
        //public void AddWish(Wish wish)
        //{
        //    /// Присваиваем свойству Owner желания ссылку на текущего пользователя
        //    wish.Owner = this;
        //    Wishes.Add(wish);
        //}

        ///// <summary>
        ///// Метод, который удаляет желание из списка желаний пользователя
        ///// </summary>
        ///// <param name="wish"></param>
        //public void DeleteWish(Wish wish)
        //{
        //    Wishes.Remove(wish);
        //}

        ///// <summary>
        ///// Метод, который перемещает желание в исполненное
        ///// </summary>
        ///// <param name="wish"></param>
        //public void MoveWishToDone(Wish wish)
        //{
        //    wish.Status = WishStatus.Done;
        //    wish.DoneDate = DateTime.Now;
        //}

        ///// <summary>
        ///// Метод, который выбирает желание другого пользователя для исполнения
        ///// </summary>
        ///// <param name="otherUser"></param>
        ///// <param name="wish"></param>
        //public void ChooseWish(User otherUser, Wish wish)
        //{
        //    wish.Status = WishStatus.Chosen;
        //    wish.Executor = this;
        //    wish.ChosenDate = DateTime.Now;
        //}

        ///// <summary>
        ///// Метод, который оценивает исполненное желание
        ///// </summary>
        ///// <param name="otherUser"></param>
        ///// <param name="wish"></param>
        ///// <param name="rating"></param>
        //public void RateWish(User otherUser, Wish wish, int rating)
        //{
        //    wish.Rating = rating;
        //}

        ///// <summary>
        ///// Метод, который назначает исполнителя желания
        ///// </summary>
        ///// <param name="otherUser"></param>
        ///// <param name="wish"></param>
        //public void AssignWish(User otherUser, Wish wish)
        //{
        //    wish.Status = WishStatus.Assigned;
        //    wish.Executor = this;
        //}

        ///// <summary>
        ///// Метод, который отменяет выбор или назначение желания
        ///// </summary>
        ///// <param name="otherUser"></param>
        ///// <param name="wish"></param>
        //public void CancelWish(User otherUser, Wish wish)
        //{
        //    wish.Status = WishStatus.New;
        //    wish.Executor = null;
        //    wish.ChosenDate = null;
        //}
    }
}

