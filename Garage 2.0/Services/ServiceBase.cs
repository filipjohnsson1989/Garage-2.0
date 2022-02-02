using Garage_2._0.Data;

namespace Garage_2._0.Services;

public class ServiceBase
{
    protected readonly Garage_2_0Context _context;

    public ServiceBase(Garage_2_0Context context)
    {
        this._context = context;
    }
}
