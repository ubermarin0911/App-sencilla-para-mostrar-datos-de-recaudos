﻿using System.Collections.Generic;

namespace Conteo_y_recaudo.Helpers
{
    public class Pagination<T> where T : class
    {
        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public Pagination(int pageIndex, int pageSize, int count, T data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            DataObject = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
        public T DataObject { get; set; }
    }
}
