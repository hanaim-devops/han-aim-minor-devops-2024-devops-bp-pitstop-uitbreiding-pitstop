using DIYManagementAPI.Models;
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
            // TODO: reparateurs ophalen uit de database en zetten
           
            _context.DIYEvening.Add(diyEvening);
            await _context.SaveChangesAsync();

            return diyEvening;
        }

        public async Task<IEnumerable<DIYEveningModel>> GetDIYEvenings()
        {
            return await _context.DIYEvening.ToListAsync();
        }

        public async Task<DIYEveningModel> CancelDIYEvening(int id)
        {
            var diyEvening = await _context.DIYEvening.FindAsync(id);
            if (diyEvening == null)
            {
                return null;
            }

            diyEvening.Cancelled = true;
            await _context.SaveChangesAsync();

            return diyEvening;
        }
    }
}
