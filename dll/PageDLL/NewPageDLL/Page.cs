using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPageDLL
{
    public class Page
    {
        public static Dictionary<string, object> QueryData(int page, int size, IQueryable<object> data)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            IQueryable<object> new_data = null;
            int count = data.Count();
            if (page == 1)
            {
                new_data = data.Take(size);
            }
            else if (page * size <= count)
            {
                new_data = data.Skip((page - 1) * size).Take(size);
            }
            else
            {
                new_data = data.Skip((page - 1) * size);
            }
            int total_page = 0;
            if (count % size == 0)
            {
                total_page = count / size;
            }
            else
            {
                total_page = count / size + 1;
            }
            result.Add("page", page);
            result.Add("totalPage", total_page);
            result.Add("count", count);
            result.Add("pageSize", size);
            result.Add("data", new_data);
            return result;
        }
    }
}
