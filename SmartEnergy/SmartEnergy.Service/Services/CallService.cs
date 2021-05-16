using AutoMapper;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;
using SmartEnergy.Infrastructure;
using SmartEnergyDomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SmartEnergy.Contract.CustomExceptions.Call;
using Microsoft.EntityFrameworkCore;

namespace SmartEnergy.Service.Services
{
    public class CallService : ICallService
    {

        private readonly SmartEnergyDbContext _dbContext;
        private readonly IMapper _mapper;

        public CallService(SmartEnergyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Delete(int id)
        {
            Call call = _dbContext.Calls.FirstOrDefault(x => x.ID == id);

            if (call == null)
                throw new CallNotFoundExcpetion("Call not found!");

            _dbContext.Calls.Remove(call);
            _dbContext.SaveChanges();
        }

        public CallDto Get(int id)
        {
            return _mapper.Map<CallDto>(_dbContext.Calls.FirstOrDefault(x => x.ID == id));
        }

        public List<CallDto> GetAll()
        {
            return _mapper.Map<List<CallDto>>(_dbContext.Calls.ToList());
        }

        public CallDto Insert(CallDto entity)
        {
            throw new NotImplementedException();
        }

        public CallDto Update(CallDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
