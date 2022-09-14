using System;
using System.Collections.Generic;

namespace VotingApp.Models
{
    public partial class VotingResult
    {
        public int VotingResultId { get; set; }
        public int VotingUserId { get; set; }
        public int VotingEventId { get; set; }
        public int VotingQuestionId { get; set; }
        public int VotingAnswerId { get; set; }
    }
}
