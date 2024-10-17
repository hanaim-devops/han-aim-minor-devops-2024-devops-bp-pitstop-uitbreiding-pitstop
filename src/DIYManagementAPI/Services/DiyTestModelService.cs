using DIYManagementAPI.Data;
using DIYManagementAPI.Models;

namespace DIYManagementAPI.Services
{
    public class DiyTestModelService
    {
        private readonly DiyTestModelDAO _dao;
        public DiyTestModelService(DiyTestModelDAO dao)
        {
            _dao = dao;
        }

        public Task<IEnumerable<DiyTestModel>> GetTestResults()
        {
            return _dao.GetTestResults();
        }
    }
}
