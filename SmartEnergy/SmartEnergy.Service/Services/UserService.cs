using AutoMapper;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;
using SmartEnergy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SmartEnergy.Contract.Enums;

namespace SmartEnergy.Service.Services
{
    public class UserService : IUserService
    {
        private readonly SmartEnergyDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(SmartEnergyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public UserDto Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<UserDto> GetAllUnassignedCrewMembers()
        {
            return _mapper.Map<List<UserDto>>(_dbContext.Users.Where(x => x.UserType == UserType.CREW_MEMBER && x.CrewID == null).ToList());
        }

        public UserDto Insert(UserDto entity)
        {
            throw new NotImplementedException();
        }

        public UserDto Update(UserDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
