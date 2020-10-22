using System;

namespace TheaterApplication.Utils
{
    public class DataWithPaging<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => PageSize != 0
            ? (int)Math.Ceiling((double)TotalCount / PageSize)
            : default;
        public int TotalCount { get; set; }
        public T[] Data { get; set; }
    }
}
