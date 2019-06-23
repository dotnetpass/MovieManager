using System.Collections.Generic;
using MovieManagerContext;
using MovieInterface;
using MovieEntity;
using System.Linq;
using Newtonsoft.Json;


namespace MovieManagerImplement
{
    public class IndexDao :IIndexDao
    {

        private IndexContext context;

        public IndexDao(IndexContext context)
        {
            this.context = context;
        }

    }
}
