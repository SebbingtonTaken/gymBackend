using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class UpdatePermissionsRequest: BaseDTO
    {
        public List<int> PermissionIds { get; set; }
        public List<int> PermissionsForDelete { get; set; }
    }
}