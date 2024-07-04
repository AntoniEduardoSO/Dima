using System.ComponentModel;

namespace Dima.core.Requests.Categories;
public class GetCategoryByIdRequest : Request
{
    public long Id { get; private set; }
}
