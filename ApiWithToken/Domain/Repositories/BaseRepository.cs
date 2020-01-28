using ApiWithToken.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Repositories
{
    public class BaseRepository
    {
        protected readonly ApiWithTokenDBContext context;

        public BaseRepository(ApiWithTokenDBContext context)
        {
            this.context = context;
        }
    }
}