using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.Interfaces
{
    public interface IUserService : IGenericService<UserDto>
    {
        public List<UserDto> GetAllUnassignedCrewMembers();

        public UsersListDto GetUsersPaged(UserField sortBy, SortingDirection direction, int page,
                                          int perPage, UserStatusFilter status, UserTypeFilter type,
                                          string searchParam);
        public UserDto ApproveUser(int userId);
        public UserDto DenyUser(int userId);
    }
}
