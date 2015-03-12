using System;

namespace DLUProject.Domain
{
    public enum AccountResult
    {
        Success = 0,
        InvalidUserName = 1,
        InvalidPassword = 2,
        InvalidQuestion = 3,
        InvalidAnswer = 4,
        InvalidEmail = 5,
        DuplicateUserName = 6,
        DuplicateEmail = 7,
        UserRejected = 8,
        InvalidProviderUserKey = 9,
        DuplicateProviderUserKey = 10,
        Fail = 11
    }
    public enum LoginResult
    {
        Success = 0,
        InvalidPassword = 1,
        NotApproved = 2,
        IsLockedOut = 3,
        LoginFaild=4
    }
}
