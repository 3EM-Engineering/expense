using System;

namespace backend.Dto
{
    public class GroupExpenceDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CreatoreId { get; set; }
        public List<int> MembriIds { get; set; } = new List<int>();
        public List<int> SpeseIds { get; set; } = new List<int>();
    }
}
