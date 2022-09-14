using System.Linq;
using VotingApp.DTO;
using VotingApp.Models;

namespace VotingApp.Repositories
{
    public class VotingRepository : IVotingRepository
    {
        public VotingDataContext Context { get; set; }
        public VotingRepository(VotingDataContext context)
        {
            Context = context;
        }

        public Task<List<VotingEvent>> FetchAllVotingEvents()
        {
            var list = Context.VotingEvents.ToList();
            return Task.FromResult(list);
        }

        public Task<List<VotingQuestion>> FetchAllQuestions(int eventId)
        {
            var list = Context.VotingQuestions.Where(x => x.VotingEventId == eventId).ToList();
            return Task.FromResult(list);
        }


        public Task<int> InsertToVotingUser(VotingUser user)
        {
            Context.Add(user);
            Context.SaveChanges();
            return Task.FromResult(user.VotingUserId);
        }

        public Task<List<VotingQuestion>> FetchAllQuestionsAndAnswers(int eventId)
        {
            var list = Context.VotingQuestions.Where(x => x.VotingEventId == eventId).ToList();
            return Task.FromResult(list);
        }


        public Task<List<VotingAnswer>> AllAnswers()
        {
            var list = Context.VotingAnswers.ToList();
            return Task.FromResult(list);
        }

        /// <summary>
        /// ta mi poišče vse odgovre na vprašanja iz eventa
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public Task<List<VotingAnswer>> FetchAllAnswers(int questionId)
        {
            var list = Context.VotingAnswers.Where(x => x.VotingQuestionId == questionId).ToList();
            return Task.FromResult(list);
        }
        /// <summary>
        /// vse odgovore na vsa vprašanja iz eventa
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public Task<List<VotingAnswer>> FetchAllQuestionAnswers(int questionId, int eventId)
        {
            var list = Context.VotingAnswers.Where(x => x.VotingEventId == eventId).Where(x => x.VotingQuestionId == questionId).ToList();
            return Task.FromResult(list);
        }


        public Task AddNewEvent(EventClass newEvent)
        {
            VotingEvent votingEvent = new();

            votingEvent.Title = newEvent.Title;
            votingEvent.DateStart = newEvent.DateStart;
            votingEvent.DateEnd = newEvent.DateEnd;
            votingEvent.PublishedVoting = 0;
            Context.Add(votingEvent);
            Context.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<Task> AddQuestionAndAnswers(int EventId, string QuestionText, List<string> answers)
        {
            var votingQuestion = new VotingQuestion();
            VotingAnswer singleAnswer = new VotingAnswer();

            votingQuestion.QuestionText = QuestionText;
            votingQuestion.VotingEventId = EventId;
            Context.Add(votingQuestion);
            foreach (var answer in answers)
            {
                singleAnswer.VotingAnswerText = answer;
                singleAnswer.VotingEventId = EventId;
                int questionId = await GetLastQuestionId();
                singleAnswer.VotingQuestionId = questionId + 1;

                Context.Add(singleAnswer);
            }
            
            Context.SaveChanges();

            return Task.CompletedTask;
        }

        /// <summary>
        /// dodam samo vprašanje v bazo
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="questionText"></param>
        /// <returns></returns>
        public Task AddQuestion(int eventId, string questionText)
        {
            VotingQuestion newQuestion = new();
            newQuestion.QuestionText = questionText;
            newQuestion.VotingEventId = eventId;
            Context.Add(newQuestion);
            return Task.CompletedTask;
        }



        public Task<int> GetQuestionIdByText(string questionText)
        {
            var record = Context.VotingQuestions.Where(x => x.QuestionText == questionText).FirstOrDefault();
            return Task.FromResult(record.VotingQuestionId);
            
        }

        public Task AddSingleAnswer(VotingAnswer newAnswer)
        {
            Context.Add(newAnswer);
            Context.SaveChanges();
            return Task.CompletedTask;
        }
        /// <summary>
        /// poiščem Id eventa
        /// </summary>
        /// <param name="Title"></param>
        /// <returns></returns>
        public Task<int> GetEventId()
        {

            var lastElement = Context.VotingEvents.OrderByDescending(x => x.VotingEventId).FirstOrDefault();
            return Task.FromResult(lastElement.VotingEventId);

        }


        public Task<string> GetEventTitle(int eventId)
        {
            var record = Context.VotingEvents.Where(x => x.VotingEventId == eventId).FirstOrDefault();
            return Task.FromResult(record.Title);
        }

        /// <summary>
        /// vne id vprašanja
        /// </summary>
        /// <param name="questionText"></param>
        /// <returns></returns>
        public Task<int> GetLastQuestionId()
        {
            var record = Context.VotingQuestions.Max(x => x.VotingQuestionId);
            if(record == null)
            {
                return Task.FromResult(-1);
            }
            var recordd = Context.VotingQuestions.FirstOrDefault(x => x.VotingQuestionId == record);
            return Task.FromResult(recordd.VotingQuestionId);
            

        }
        public Task InsertVotingResults(VotingResult result)
        {
            VotingResult newResult = new();
            newResult.VotingUserId = result.VotingUserId;
            newResult.VotingEventId = result.VotingEventId;
            newResult.VotingQuestionId = result.VotingQuestionId;
            newResult.VotingAnswerId = result.VotingAnswerId;

            Context.Add(newResult);
            Context.SaveChanges();
            return Task.CompletedTask;
        }


      /// za rezultate ->

        public Task<List<VotingResult>> FetchAllResults(int eventId)
        {
            var list = Context.VotingResults.Where(x=> x.VotingEventId == eventId).ToList();
            return Task.FromResult(list);
        }

        public Task<int> FetchNumberOfParticipants(int eventId, string sex)
        {
            var list = Context.VotingUsers.Where(x => x.Sex == sex).Where(y => y.VotingEventId == eventId).ToList();
            return Task.FromResult(list.Count());
        }

        public Task<int> FetchNumberOfChoosenAnswerBySex(int eventId, string sex, int answerId)
        {
            var listOfIds = Context.VotingUsers.Where(x => x.VotingEventId == eventId).Where(x=>x.Sex == sex).ToList();
            List<int> ids = new List<int>();
            foreach(var elt in listOfIds)
            {
                ids.Add(elt.VotingUserId);
            }
            
            var list = Context.VotingResults.Where(x => ids.Contains(x.VotingUserId)).Where(x => x.VotingAnswerId == answerId).Count();

            return Task.FromResult(list);
        }

        //prikaz rezultatov

       public Task PublishEventResults(int eventId)
        {
            var record = Context.VotingEvents.Where(x => x.VotingEventId == eventId).FirstOrDefault();
            record.PublishedVoting = 1;
            Context.SaveChanges();
            return Task.CompletedTask;
        }
        public Task HideEventResults(int eventId)
        {
            var record = Context.VotingEvents.Where(x => x.VotingEventId == eventId).FirstOrDefault();
            record.PublishedVoting = 0;
            Context.SaveChanges();
            return Task.CompletedTask;
        }
    }
} 
