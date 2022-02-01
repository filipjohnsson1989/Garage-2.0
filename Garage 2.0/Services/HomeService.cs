using AutoMapper;
using Garage_2._0.Data;

namespace Garage_2._0.Services;

public class HomeService : ServiceBase
{
    public HomeService(IMapper _mapper, AppDbContext _context) : base(_mapper, _context)
    {
    }
}
