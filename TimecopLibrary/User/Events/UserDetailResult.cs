using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary.User.Events
{
    public class UserDetailResult
    {
        public UserDetailResult(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public Exception Exception { get; }
        public bool HasError { get { return Exception != null; } }

    }
}
