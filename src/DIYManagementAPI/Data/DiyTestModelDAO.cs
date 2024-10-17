using DIYManagementAPI.Models;

namespace DIYManagementAPI.Data
{
    public class DiyTestModelDAO
    {
        private readonly DatabaseContext _context;

        public DiyTestModelDAO(DatabaseContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<DiyTestModel>> GetTestResults()
        {
            // creates a list of fake data 
            var results = new List<DiyTestModel>
            {
                new DiyTestModel { Name = "Test 1", Description = "This is a test" },
                new DiyTestModel { Name = "Test 2", Description = "This is a test" },
                new DiyTestModel { Name = "Test 3", Description = "This is a test" },
                new DiyTestModel { Name = "Test 4", Description = "This is a test" },
                new DiyTestModel { Name = "Test 5", Description = "This is a test" }
            };

            return Task.FromResult<IEnumerable<DiyTestModel>>(results);
        }
    }
}
