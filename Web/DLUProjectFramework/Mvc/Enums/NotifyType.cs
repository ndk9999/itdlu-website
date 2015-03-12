using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DLUProjectFramework.Mvc
{
    public enum NotifyType
    {
        InvalidData,
        Fail,
        Success,
        CreateFail,
        CreateSuccess,
        EditFail,
        EditSuccess,
        DeleteFail,
        DeleteAllFail,
        DeleteAllSuccess,
        DeleteNotAllSuccess,
        SaveFail,
        SaveAllSuccess,
        SaveNotAllSuccess,
        ViolationOfPK
    }
}