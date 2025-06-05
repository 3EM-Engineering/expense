using System;

namespace backend.Dto
{
    public class GroupExpenceDto
    {
        public string? Id { get; set; }
        public string? Nome { get; set; }
        public string CreatoreId { get; set; }
        public List<string> MembriIds { get; set; } = new List<string>();
        public List<int> SpeseIds { get; set; } = new List<int>();
    }
}
