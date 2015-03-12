using System;

namespace DLUProject.Domain
{
    [Flags]
    public enum AccessFlag
    {
        Read = 1,
        Create = 2,
        Edit = 4,
        Delete = 8,
        Aprrove = 16,
        Publish = 32,
        Moderate = 64,
        Upload = 128,
        Import=128,
        Download = 256,
        Special = 512
    }
    public enum LevelErrorEnum
    {
        Login = 1,
        Logout = 2,
        Insert = 3,
        Update = 4,
        Delete = 5,
        Import = 6,
        View = 7
    }
}
