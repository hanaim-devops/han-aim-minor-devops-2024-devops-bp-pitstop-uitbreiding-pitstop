using DIYManagementAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

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

        public async Task<DIYFeedback> RegisterDIYFeedback(DIYFeedback diyFeedback)
        {   
            _context.DIYFeedback.Add(diyFeedback);
            await _context.SaveChangesAsync();
            return diyFeedback;
        }
    }
}
