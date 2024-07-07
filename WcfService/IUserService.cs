using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        List<User> GetAllUsers();
        [OperationContract]
        void AddUser(User user);
        [OperationContract]
        void UpdateUser(User user);
    }
}