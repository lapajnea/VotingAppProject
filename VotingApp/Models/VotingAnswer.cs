using System;
using System.Collections.Generic;

namespace VotingApp.Models
{
    public partial class VotingAnswer
    {
        public int VotingAnswerId { get; set; }
        public int VotingQuestionId { get; set; }
        public string VotingAnswerText { get; set; } = null!;
        public int VotingEventId { get; set; }
    }
}
