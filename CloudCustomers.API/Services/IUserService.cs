using CloudCustomers.API.Models;
using System.Collections.ObjectModel;

namespace CloudCustomers.API.Services;

public interface IUserService
{
    Task<List<User>> GetAllUsers();
}