using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NJFairground.Web.DTO.Base
{
    public enum CrudAction
    {
        Insert = 1,
        BulkInsert = 2,
        Update = 3,
        BulkUpdate = 4,
        Delete = 5,
        BulkDelete = 6,
        Select = 7,
        BulkSelect = 8
    }

    public enum RespStatus
    {
        Success = 1,
        Failure = 0
    }
}