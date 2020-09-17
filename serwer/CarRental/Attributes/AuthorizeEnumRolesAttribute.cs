using CarRental.API.Controllers;
using CarRental.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Attributes
{
    public class AuthorizeEnumRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeEnumRolesAttribute(params RoleOfWorker[] roles)
        {
            this.Roles = string.Join(",", roles.Select(p => Enum.GetName(p.GetType(), p)));
        }
    }
}
