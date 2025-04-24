using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Repositories;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.DataAccessLayer.EntityFramework;

public class EfCargoCustomerDal : GenericRepository<CargoCustomer>, ICargoCustomerDal
{
    private readonly CargoContext _context;

    public EfCargoCustomerDal(CargoContext context) : base(context)
    {
        _context = context;
    }

    public CargoCustomer GetCargoCustomerById(string id)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Customer ID cannot be null or empty.", nameof(id));

            var customer = _context.CargoCustomers.FirstOrDefault(x => x.UserCustomerId == id);

            if (customer == null)
                throw new InvalidOperationException($"No CargoCustomer found with UserCustomerId: {id}");

            return customer;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving the cargo customer by ID.", ex);
        }
    }
}
