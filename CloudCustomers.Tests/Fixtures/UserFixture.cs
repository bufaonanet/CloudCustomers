using CloudCustomers.API.Models;

namespace CloudCustomers.Tests.Fixtures;

public static class UserFixture
{
    public static List<User> GetTestUsers() => new()
    {
        new User()
        {
            Id=1,
            Name="dougals",
            Email = "email@teste.com",
            Address = new()
            {
                Street = "123 main st",
                City="Madison",
                ZipCode ="53123"
            }
        },
        new User()
        {
            Id=2,
            Name="bufao",
            Email = "bufao@teste.com",
            Address = new()
            {
                Street = "123 main st",
                City="Madison",
                ZipCode ="53123"
            }
        },
        new User()
        {
            Id=3,
            Name="bufaonanet",
            Email = "bufao@teste.com",
            Address = new()
            {
                Street = "123 main st",
                City="Madison",
                ZipCode ="53123"
            }
        },

    };

}
