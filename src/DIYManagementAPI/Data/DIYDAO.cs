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
    }
}
