using DIYManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq;

namespace DIYManagementAPI.Data
{
    public class DIYDAO
    {
        private readonly DatabaseContext _context;

        public DIYDAO(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<DIYEveningModel> CreateDIYEvening(DIYEveningModel diyEvening)
        {
            _context.DIYEvening.Add(diyEvening);
            await _context.SaveChangesAsync();

            return diyEvening;
        }

        public async Task<IEnumerable<DIYEveningModel>> GetDIYEvenings()
        {
            return await _context.DIYEvening.ToListAsync();
        }

        public async Task<IEnumerable<DIYEveningModel>> GetFutureDIYEvenings()
        {
            var now = DateTime.Now;
            var query = _context.DIYEvening
                        .OrderBy(e => e.StartDate)
                        .Where(e => e.EndDate > now);
            return await query.ToListAsync();
        }

        public async Task<DIYEveningModel> GetDIYEveningById(int id)
        {
            return await _context.DIYEvening.FindAsync(id);
        }

        public async Task RegisterDIYEveningCustomer(DIYRegistration registration)
        {
            _context.DIYRegistrations.Add(registration);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DIYRegistration>> GetRegistrationsForDIYEvening(int diyEveningId)
        {
            return await _context.DIYRegistrations.Where(r => r.DIYEveningId == diyEveningId).ToListAsync();
        }

        public async Task<DIYEveningModel> CancelDIYEvening(int id)
        {
            var diyEvening = await _context.DIYEvening.FindAsync(id);
            if (diyEvening == null)
            {
                throw new Exception("DIY Evening not found");
            }

            diyEvening.Cancelled = true;
            await _context.SaveChangesAsync();

            return diyEvening;
        }

        public async Task<bool> CancelDIYRegistration(int diyRegistrationId)
        {
            var registration = await _context.DIYRegistrations.FindAsync(diyRegistrationId);

            if (registration == null)
            {
                return false;
            }

            _context.DIYRegistrations.Remove(registration);

            await _context.SaveChangesAsync();

            return true;
        }
        
        public async Task<DIYFeedback> RegisterDIYFeedback(DIYFeedback diyFeedback)
        {   
            _context.DIYFeedback.Add(diyFeedback);
            await _context.SaveChangesAsync();
            return diyFeedback;
        }
    }
}
