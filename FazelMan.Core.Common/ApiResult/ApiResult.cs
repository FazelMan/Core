using System;

namespace Core.Common.ApiResult
{
    public class ApiResult
    {
        public string Message { get; set; }
        public object Result { get; set; }
        public DateTime ResultDate => DateTime.Now;
    }

    public class ApiResult<T>
    {
        public string Message { get; set; }
        public T Result { get; set; }
        public DateTime ResultDate => DateTime.Now;
    }
}
