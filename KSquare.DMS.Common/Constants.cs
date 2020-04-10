using System;
using System.Collections.Generic;
using System.Text;

namespace KSquare.DMS.Common
{
    public static class Constants
    {
    }

    public enum UserCategoryEnum
    {
        SuperAdmin = 1,
        Client = 2,
        Customer = 3,
        Consumer = 4
    }

    public enum UserRoleEnum
    {
        Admin = 1,
        CustomerAdmin = 2,
        ClientAdmin = 3,
        SubCustomer = 4
    }
}
