using System;
using System.Collections.Generic;

namespace VotingApp.Models
{
    public partial class VotingQuestion
    {
        public int VotingQuestionId { get; set; }
        public int VotingEventId { get; set; }
        public string QuestionText { get; set; } = null!;
    }
}
