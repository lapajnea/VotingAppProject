using System.Data;
using VotingApp.Models;
using VotingApp.Pages;

namespace VotingApp.DTO
{
    public class EventDataClass
    {
        public string QuestionText { get; set; }
        public string Answer1 { get; set; }
        public string Answer2{ get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public string Answer5 { get; set; }
        public string Answer6 { get; set; }

        public List<string> ListAnswers = new List<string>();   
       
        

        
        
       
        
        

    }
    public class QuestionAnswerModel
    {
        public string AnswerText{ get; set; }
    }
}
