using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnimetalWeb.Models
{
    public class StatusCommonResponse : CommonResponse
    {
        public string StatusMessage { get; set; }

        public int PrimaryKey { get; set; }
    }
}
