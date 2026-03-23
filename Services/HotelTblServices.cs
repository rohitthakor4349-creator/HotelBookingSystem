using HotelBookingSystem.Entity;
using HotelBookingSystem.Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Services
{
   public interface IHotelTblServices
    {
        public Task<string> AddHotel(HotelTbl Model);
        public Task<string> UpdateHotel(int HotelId,HotelTbl Model);
        public Task<string> DeleteHotel(int HotelId);
        public Task<HotelTbl> GetByHotelId(int HotelId);
        public Task<List<HotelTbl>> GetByAllHotelList();
    }
    public class HotelTblServices : IHotelTblServices, IDisposable
    {
        private readonly EntityDbContext db;

        public HotelTblServices(EntityDbContext db)
        {
            this.db = db;
        }
        public async Task<string> AddHotel(HotelTbl Model)
        {
            try
            {
                if (Model == null)
                {
                    return "Model is null";
                }

                var Data = db.HotelTbls.Where(m => m.Name == Model.Name).FirstOrDefaultAsync();

                if (Data !=  null)
                {
                    return "Hotel Name is All ready Exist";
                }

                await db.HotelTbls.AddAsync(Model);

                int row = await db.SaveChangesAsync();

                if (row > 0)
                {
                    return "SuccessFully Store Hotel Data";
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

        public async Task<string> DeleteHotel(int HotelId)
        {
            try
            {
                if (HotelId == 0)
                {
                    return "HotelId is Zero";
                }

                var Data = db.HotelTbls.Find(HotelId);

                if (Data == null)
                {
                    return "There Not Give Data in Id";
                }

                 db.HotelTbls.Remove(Data);

                int row = await db.SaveChangesAsync();

                if (row > 0)
                {
                    return "SuccessFully Delete Hotel Data";
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

        public async Task<List<HotelTbl>> GetByAllHotelList()
        {
            var Data = await db.HotelTbls.ToListAsync();

            if (Data != null)
            {
                Data = new List<HotelTbl>();
            }
            return Data;
        }

        public async Task<HotelTbl> GetByHotelId(int HotelId)
        {
            var Data = await db.HotelTbls.FindAsync(HotelId);
            if (Data != null)
            {
                Data = new HotelTbl();
            }
            return Data;
        }

        public async Task<string> UpdateHotel(int HotelId, HotelTbl Model)
        {
            try
            {
                if (HotelId == 0)
                {
                    return "HotelId is Zero";
                }
                if (Model == null)
                {
                    return "Model is  null";
                }

                var Data = db.HotelTbls.Find(HotelId);

                if (Data == null)
                {
                    return "There Not Give Data in Id";
                }

                Data.Name = Model.Name;
                Data.Location = Model.Location; 

                int row = await db.SaveChangesAsync();

                if (row > 0)
                {
                    return "SuccessFully Update Hotel Data";
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
