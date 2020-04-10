using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KSquare.DMS.Domain.Models
{
    public class ResponseModel<TModel>
    {
        public bool IsError { get; set; }

        public string Message { get; set; }

        public TModel Model { get; set; }
    }
}
