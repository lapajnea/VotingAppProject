using System;
using System.Collections.Generic;

namespace VotingApp.Models
{
    public partial class Voting
    {
        public int VotingId { get; set; }
        public int EventId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int UserId { get; set; }
    }
}
