using DIYManagementAPI.Data;
using DIYManagementAPI.Models;

namespace DIYManagementAPI.Services
{
    public class DIYService
    {
        private readonly DIYDAO _dao;
        public DIYService(DIYDAO dao)
        {
            _dao = dao;
        }

        public async Task<DIYEveningModel> CreateDIYEvening(DIYEveningModel diyEvening)
        {
            return await _dao.CreateDIYEvening(diyEvening);
        }

        public async Task<IEnumerable<DIYEveningModel>> GetDIYEvenings()
        {
            return await _dao.GetDIYEvenings();
        }

        public async Task<DIYEveningModel> CancelDIYEvening(int id)
        {
            return await _dao.CancelDIYEvening(id);
        }
    }
}
