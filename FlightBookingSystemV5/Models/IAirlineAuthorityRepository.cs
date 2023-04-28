namespace FlightBookingSystemV5.Models
{
    public interface IAirlineAuthorityRepository
    {
        public AirlineAuthority Get(string name);
        public List<AirlineAuthority> GetAll();
        public AirlineAuthority Delete(string name);
        public void Add(AirlineAuthority airlineAuthority);
        public AirlineAuthority Update(string name, AirlineAuthority airlineAuthority);
    }
}
