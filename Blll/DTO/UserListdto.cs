using System.Collections.Generic;

namespace Bll.DTO
{
    public class UsersListDto
    {
        public IEnumerable<UsersDto> UserList { get; set; }
        public int CountOfItems { get; set; }
    }
}
