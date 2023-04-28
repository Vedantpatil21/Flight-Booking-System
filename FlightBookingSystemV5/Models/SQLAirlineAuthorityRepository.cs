using FlightBookingSystemV5.Auth;

namespace FlightBookingSystemV5.Models
{
    public class SQLAirlineAuthorityRepository:IAirlineAuthorityRepository
    {
        private readonly ApplicationDbContext _context;
        public SQLAirlineAuthorityRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public AirlineAuthority Get(string name)
        {
            return (_context.AirlineAuthorities.FirstOrDefault(u => u.AirlineName == name));
        }
        public List<AirlineAuthority> GetAll()
        {
            return (_context.AirlineAuthorities.ToList());
        }
        public AirlineAuthority Delete(string name)
        {
            AirlineAuthority airlineAuthority = _context.AirlineAuthorities.FirstOrDefault(u => u.AirlineName == name);
            if (airlineAuthority != null)
            {
                _context.AirlineAuthorities.Remove(airlineAuthority);
                _context.SaveChanges();
            }
            return airlineAuthority;
        }
        public void Add(AirlineAuthority airlineAuthority)
        {
            _context.AirlineAuthorities.Add(airlineAuthority);
            _context.SaveChanges();
        }
        public AirlineAuthority Update(string name, AirlineAuthority airlineAuthority)
        {
            AirlineAuthority airlineAuthorityG = _context.AirlineAuthorities.FirstOrDefault();
            if (airlineAuthorityG != null)
            {
                airlineAuthorityG = airlineAuthority;
                _context.SaveChanges();
            }
            return airlineAuthorityG;
        }
    }
}
