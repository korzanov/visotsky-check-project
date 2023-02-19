using Ardalis.Specification;
using TaskList.Domain.Entities;

namespace TaskList.Domain.Specifications;

public sealed class UserWithCredentialsSpecification : Specification<User>
{
    public UserWithCredentialsSpecification(string login, string password)
    {
        Query.Where(u => u.Login == login && u.Password == password);
    }
}