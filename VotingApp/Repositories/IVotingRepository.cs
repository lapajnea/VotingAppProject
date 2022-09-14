using VotingApp.DTO;
using VotingApp.Models;

namespace VotingApp.Repositories
{
    public interface IVotingRepository
    {
        Task<List<VotingQuestion>> FetchAllQuestionsAndAnswers(int id);
        Task<List<VotingEvent>> FetchAllVotingEvents();
        Task<List<VotingQuestion>> FetchAllQuestions(int eventId);
        Task<List<VotingAnswer>> FetchAllAnswers(int questionId);
        Task<List<VotingAnswer>> FetchAllQuestionAnswers(int questionId, int eventId);

        Task AddNewEvent(EventClass newEvent);
        

        //Task<List<VotingAnswer>> SingleQuestionAnswer(int questionID);
        Task<int> InsertToVotingUser(VotingUser user);

        Task<List<VotingAnswer>> AllAnswers();
        Task<int> GetEventId();

        Task<string> GetEventTitle(int eventId);
        Task InsertVotingResults(VotingResult result);

        //za vnos odgovorv
        Task<Task> AddQuestionAndAnswers(int EventId, string QuestionText, List<string> answers);
        Task<int> GetLastQuestionId();


        Task AddQuestion(int eventId, string questionText);
        Task<int> GetQuestionIdByText(string questionText);
        Task AddSingleAnswer(VotingAnswer newAnswer);
        //za rezultate

        Task<List<VotingResult>> FetchAllResults(int eventId);
        Task<int> FetchNumberOfParticipants(int eventId, string sex);
        Task<int> FetchNumberOfChoosenAnswerBySex(int eventId, string sex, int answerId);

        //prikaz rezultatov
        Task PublishEventResults(int eventId);
        Task HideEventResults(int eventId);

    }
}