using FlightBookingSystemV5.Auth;

namespace FlightBookingSystemV5.Models
{
    public class SQLJourneyDetailRepository:IJourneyDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public SQLJourneyDetailRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public JourneyDetail Get(int id)
        {
            return (_context.JourneyDetails.FirstOrDefault(u => u.JourneyId == id));
        }
        public List<JourneyDetail> GetAll()
        {
            return (_context.JourneyDetails.ToList());
        }
        public JourneyDetail Delete(int id)
        {
            JourneyDetail journeyDetail = _context.JourneyDetails.FirstOrDefault(u => u.JourneyId == id);
            if (journeyDetail != null)
            {
                _context.JourneyDetails.Remove(journeyDetail);
                _context.SaveChanges();
            }
            return journeyDetail;
        }
        public void Add(JourneyDetail journeyDetail)
        {
            _context.JourneyDetails.Add(journeyDetail);
            _context.SaveChanges();
        }
        public JourneyDetail Update(int id, JourneyDetail journeyDetail)
        {
            JourneyDetail journeyDetailG = _context.JourneyDetails.FirstOrDefault();
            if (journeyDetailG != null)
            {
                journeyDetailG = journeyDetail;
                _context.SaveChanges();
            }
            return journeyDetailG;
        }
    }
}
