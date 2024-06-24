namespace Legacy.Api.Models;

public record PaginatedRequest(int PageSize = 10, int PageIndex = 0);
