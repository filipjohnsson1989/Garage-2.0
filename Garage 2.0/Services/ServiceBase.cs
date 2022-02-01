using AutoMapper;
using Garage_2._0.Data;

namespace Garage_2._0.Services
{
    public class ServiceBase
    {
        protected readonly IMapper _mapper;
        protected readonly AppDbContext _context;

        public ServiceBase(IMapper mapper, AppDbContext context)
        {
            this._mapper = mapper;
            this._context = context;
        }
    }
}
