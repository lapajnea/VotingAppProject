using System;
using System.Collections.Generic;

namespace VotingApp.Models
{
    public partial class User
    {
        public int VotingUserId { get; set; }
        public int VotingEventId { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; } = null!;
    }
}
