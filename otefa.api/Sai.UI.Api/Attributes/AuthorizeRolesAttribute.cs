using System.Web.Http;
using System.Linq;
using System;
using Otefa.Domain.Model.Entities;

namespace Otefa.UI.Api.Attributes
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params Roles[] allowedRoles)
        {
            var allowedRolesAsStrings = allowedRoles.Select(x => Enum.GetName(typeof(Roles), x));

            Roles = string.Join(",", allowedRolesAsStrings);
        }
    }
}