using AutoMapper;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;
using SmartEnergy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SmartEnergy.Contract.Enums;
using Microsoft.EntityFrameworkCore;
using SmartEnergyDomainModels;
using SmartEnergy.Contract.CustomExceptions;
using SmartEnergy.Contract.CustomExceptions.User;

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

        public UserDto ApproveUser(int userId)
        {
            User user = _dbContext.Users.Find(userId);
            if (user == null)
                throw new UserNotFoundException($"User does not exist.");
            if(user.UserStatus != UserStatus.PENDING)
                throw new UserInvalidStatusException("User can't be approved , as his status is not Pending.");

            user.UserStatus = UserStatus.APPROVED;
            _dbContext.SaveChanges();

            return _mapper.Map<UserDto>(user);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public UserDto DenyUser(int userId)
        {
            User user = _dbContext.Users.Find(userId);
            if (user == null)
                throw new UserNotFoundException($"User does not exist.");
            if (user.UserStatus != UserStatus.PENDING)
                throw new UserInvalidStatusException("User can't be denied , as his status is not Pending.");

            user.UserStatus = UserStatus.DENIED;
            _dbContext.SaveChanges();

            return _mapper.Map<UserDto>(user);
        }

        public UserDto Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserDto> GetAll()
        {
            return _mapper.Map<List<UserDto>>(_dbContext.Users.Include("Location").ToList());
        }

        public List<UserDto> GetAllUnassignedCrewMembers()
        {
            return _mapper.Map<List<UserDto>>(_dbContext.Users.Where(x => x.UserType == UserType.CREW_MEMBER && x.CrewID == null && x.UserStatus == UserStatus.APPROVED).ToList());
        }

        public UserDto Insert(UserDto entity)
        {
            Location userLocation = _dbContext.Location.Find(entity.Location.ID);
            if(userLocation == null)
            { 
                throw new Exception();
            }

            User user = _mapper.Map<User>(entity);

            if (user.UserType == UserType.ADMIN)
                throw new InvalidUserDataException("User cannot register as admin!");

            if(user.UserType != UserType.CREW_MEMBER && user.Crew != null)
                throw new InvalidUserDataException("User can be part of a crew only if he is a crew member!");

            if(user.Crew != null)
            {
                Crew crew = _dbContext.Crews.Find(user.Crew.ID);
                if (crew == null)
                    throw new CrewNotFoundException("Selected user crew does not exist.");

            }

            user.ID = 0;
            user.UserStatus = UserStatus.PENDING;//Just in case
            user.LocationID = userLocation.ID;
            

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return _mapper.Map<UserDto>(user);

        }

        public UserDto Update(UserDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
