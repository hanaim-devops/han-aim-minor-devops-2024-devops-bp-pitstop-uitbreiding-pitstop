using DIYManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DIYManagementAPI.Data
{
    public class DYIDAO
    {
        private readonly DatabaseContext _context;

        public DYIDAO(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<DIYEveningModel> CreateDIYEvening(DIYEveningModel diyEvening)
        {
            _context.DIYEveningModels.Add(diyEvening);
            await _context.SaveChangesAsync();
            return diyEvening;
        }

        public async Task<DIYFeedback> RegisterDIYFeedback(DIYFeedback diyFeedback)
        {   
            _context.DIYFeedback.Add(diyFeedback);
            await _context.SaveChangesAsync();
            return diyFeedback;
        }

        public async Task<IEnumerable<DIYEveningModel>> GetDIYEvenings()
        {
            return await _context.DIYEveningModels.ToListAsync();
        }

        public async Task<DIYEveningModel> GetDIYEveningById(int id)
        {
            return await _context.DIYEveningModels.FindAsync(id);
        }

        public async Task RegisterDIYEveningCustomer(DIYRegistration registration)
        {
            _context.DIYRegistrations.Add(registration);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DIYFeedback>> GetFeedbackByDIYEveningIdAsync(int diyEveningId)
        {
            var feedback = await _context.DIYFeedback
            .Where(f => f.DIYEveningID == diyEveningId)
            .ToListAsync();

            return feedback ?? new List<DIYFeedback>();
        }
    }
}
