using FlightBookingSystemV5.Models;

namespace FlightBookingSystemV5.Interface
{
  public interface IBookingDetailRepository
  {
    public BookingDetail Get(int id);
    public List<BookingDetail> GetAll();
    public BookingDetail Delete(int id);
    public void Add(BookingDetail bookingDetail);
    public BookingDetail Update(int id, BookingDetail bookingDetail);
  }
}
