using System;
using System.Collections.Generic;

namespace VotingApp.Models
{
    public partial class VotingEvent
    {
        public int VotingEventId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int PublishedVoting { get; set; }
    }
}
