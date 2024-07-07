using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;

namespace WcfService
{
    [TableName("users")]
    public class User
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        [MapField("FirstName")]
        public string FirstName { get; set; }

        [MapField("LastName")]
        public string LastName { get; set; }

        [MapField("Patronymic")]
        public string Patronymic { get; set; }

        [MapField("IdentificationNumber")]
        public string IdentificationNumber { get; set; }

        [MapField("Email")]
        public string Email { get; set; }

        [MapField("ContactPhone")]
        public string ContactPhone { get; set; }

        [MapField("CreationDate")]
        public DateTime CreationDate { get; set; }

        [MapField("LastModifiedDate")]
        public DateTime LastModifiedDate { get; set; }
    }
}