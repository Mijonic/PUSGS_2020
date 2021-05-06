using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.DTO
{
    public class UsersListDto
    {
        public List<UserDto> Users { get; set; }
        public int TotalCount { get; set; }
    }
}
