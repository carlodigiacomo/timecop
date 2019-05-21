using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary.User.Commands
{
    public class GetUserDetail
    {
        public GetUserDetail(int? userId)
        {
            UserId = userId;
        }

        public int? UserId { get; }
    }
}
