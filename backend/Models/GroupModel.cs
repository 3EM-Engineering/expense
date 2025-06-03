namespace backend.Model
{
    public class GroupModel
    public class Gruppo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CreatoreId { get; set; }
        public User Creatore { get; set; } // relazione con User
        public List<User> Membri { get; set; } = new List<User>();
        public List<Spesa> SpeseCollegate { get; set; } = new List<Spesa>();
    }
}
