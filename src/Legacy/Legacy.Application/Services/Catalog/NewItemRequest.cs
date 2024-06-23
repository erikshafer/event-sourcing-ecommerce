using MediatR;

namespace Legacy.Application.Services.Catalog;

public class NewItemRequest : IRequest
{
    public int ItemId { get; set; }
    public string Name { get; set; }
    public int? BrandId { get; set; }
    public int? CategoryId { get; set; }
}
