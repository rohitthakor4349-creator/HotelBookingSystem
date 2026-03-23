using HotelBookingSystem.Entity;
using HotelBookingSystem.Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Services
{
    public interface IBookingTblServices
    {
        public Task<string> AddBooking(BookingTbl Model);
        public Task<string> UpdateBooking(int BookingId, BookingTbl Model);
        public Task<string> DeleteBooking(int BookingId);
        public Task<BookingTbl> getByBookingId(int BookingId);
        public Task<List<BookingTbl>> GetByBookingList();
    }
    public class BookingTblServices : IBookingTblServices, IDisposable
    {
        private readonly EntityDbContext db;

        public BookingTblServices(EntityDbContext db)
        {
            this.db = db;
        }
        public async Task<string> AddBooking(BookingTbl Model)
        {
            try
            {
                if (Model == null)
                {
                    return "Model is nul";
                }

                var Data = await db.BookingTbls.AnyAsync(m => m.HotelId == Model.HotelId && Model.CheckInDate < m.CheckOutDate && Model.CheckOutDate > m.CheckInDate);

                if (Data != null)
                {
                    return "Booking already exists for selected dates";
                }
                await db.BookingTbls.AddAsync(Model);

                int row = await db.SaveChangesAsync();

                if (row > 0)
                {
                    return "Booking Created Successfully";
                }
                else
                {
                    return "Something Wrong Data";
                }
            }
            catch (Exception Ex)
            {

                return Ex.ToString();
            }
        }

        public async Task<string> DeleteBooking(int BookingId)
        {
            try
            {
                if (BookingId == 0)
                {
                    return "BookingId is Zero";
                }

                var Data = await db.BookingTbls.FindAsync(BookingId);

                if (Data == null)
                {
                    return "There is Not Given Data id";
                }
                 db.BookingTbls.Remove(Data);

                int row = await db.SaveChangesAsync();

                if (row > 0)
                {
                    return "Booking delete Successfully";
                }
                else
                {
                    return "Something Wrong Data";
                }
            }
            catch (Exception Ex)
            {

                return Ex.ToString();
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public async Task<BookingTbl> getByBookingId(int BookingId)
        {
            var Data = await db.BookingTbls.FindAsync(BookingId);

            if (Data != null)
            {
                return Data = new BookingTbl();
            }

            return Data;
        }

        public async Task<List<BookingTbl>> GetByBookingList()
        {
            var Data = await db.BookingTbls.ToListAsync();

            if (Data != null)
            {
                return Data = new List<BookingTbl>();
            }
            return Data;
        }

        public async Task<string> UpdateBooking(int BookingId, BookingTbl Model)
        {
            try
            {
                if (BookingId == 0)
                {
                    return "BookingId is Zero";
                }
                if (Model == null)
                {
                    return "Model is null";
                }

                var Data = await db.BookingTbls.FindAsync(BookingId);

                if (Data == null)
                {
                    return "There is Not Given Data id";
                }
               
                Data.guestName = Model.guestName;
                Data.CheckInDate = DateTime.Now;
                Data.CheckOutDate = DateTime.Now;
                Data.CreatedBy = Model.CreatedBy;

                int row = await db.SaveChangesAsync();

                if (row > 0)
                {
                    return "Booking Update Successfully";
                }
                else
                {
                    return "Something Wrong Data";
                }
            }
            catch (Exception Ex)
            {

                return Ex.ToString();
            }
        }
    }
}
