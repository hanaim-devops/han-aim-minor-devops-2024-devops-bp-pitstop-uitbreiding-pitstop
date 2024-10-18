﻿using DIYManagementAPI.Data;
using DIYManagementAPI.Models;

namespace DIYManagementAPI.Services
{
    public class DYIService
    {
        private readonly DYIDAO _dao;
        public DYIService(DYIDAO dao)
        {
            _dao = dao;
        }

        public async Task<DIYEveningModel> CreateDIYEvening(DIYEveningModel diyEvening)
        {
            return await _dao.CreateDIYEvening(diyEvening);
        }

        // public async Task<DIYEveningModel> CreateDIYFeedback(DIYFeedbackModel feedback)
        // {
        //     return await _dao.CreateDIYFeedback(feedback);
        // }

        public async Task<IEnumerable<DIYEveningModel>> GetDIYEvenings()
        {
            return await _dao.GetDIYEvenings();
        }

        public async Task<DIYEveningModel> GetDIYEveningById(int id)
        {
            return await _dao.GetDIYEveningById(id);
        }

        public async Task RegisterDIYEveningCustomer(DIYRegistration registration)
        {
            await _dao.RegisterDIYEveningCustomer(registration);
        }

        public async Task RegisterDIYFeedback(DIYFeedback feedback)
        {
            // await _dao.RegisterDIYFeedback(feedback);
        }
    }
}
