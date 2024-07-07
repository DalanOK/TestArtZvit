using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    public static class Mapper
    {
        public static ServiceReference1.User MapUserToServiceReference1User(ClientApp.User user)
        {
            return new ServiceReference1.User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Patronymic = user.Patronymic,
                IdentificationNumber = user.IdentificationNumber,
                Email = user.Email,
                ContactPhone = user.ContactPhone,
                CreationDate = user.CreationDate,
                LastModifiedDate = user.LastModifiedDate
            };
        }
    }
}
