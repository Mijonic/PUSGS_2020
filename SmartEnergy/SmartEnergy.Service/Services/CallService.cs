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
using SmartEnergy.Contract.Enums;
using SmartEnergy.Contract.CustomExceptions.Location;
using SmartEnergy.Contract.CustomExceptions.Incident;

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
            return _mapper.Map<List<CallDto>>(_dbContext.Calls.Include(x => x.Location)
                                                              .ThenInclude(x => x.Consumers)
                                                              .ToList());
        }

        public CallDto Insert(CallDto entity)
        {
            throw new NotImplementedException();
        }

        public CallDto Update(CallDto entity)
        {
            Call updatedCall = _mapper.Map<Call>(entity);
            Call oldCall = _dbContext.Calls.FirstOrDefault(x => x.ID.Equals(updatedCall.ID));


            updatedCall.Location = null;
           
            if (oldCall == null)
                throw new CallNotFoundExcpetion($"Call with Id = {updatedCall.ID} does not exists!");

            if (updatedCall.Hazard.Trim().Equals("") || updatedCall.Hazard == null)
                throw new InvalidCallException("You have to enter hazard!");

            if (!Enum.IsDefined(typeof(CallReason), entity.CallReason))
                throw new InvalidCallException("Undefined call reason!");

 

            if (_dbContext.Location.Where(x => x.ID.Equals(updatedCall.LocationID)) == null)
                throw new LocationNotFoundException($"Location with id = {updatedCall.LocationID} does not exists!");


            if (_dbContext.Incidents.Where(x => x.ID.Equals(updatedCall.IncidentID)) == null)
                throw new IncidentNotFoundException($"Incident with id = {updatedCall.IncidentID} does not exists!");


            oldCall.UpdateCall(updatedCall);
            _dbContext.SaveChanges();

            return _mapper.Map<CallDto>(oldCall);
        }
    }
}
