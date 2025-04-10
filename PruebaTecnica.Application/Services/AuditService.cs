using Microsoft.AspNetCore.Http;
using PruebaTecnica.Application.Interfaces;
using System.Security.Claims;

namespace PruebaTecnica.Application.Services
{
    public class AuditService : IAuditService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditService(IHttpContextAccessor httpContextAccessor) { _httpContextAccessor = httpContextAccessor; }
        public string? GetUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.FindFirst(ClaimTypes.Name)?.Value ??
                   user?.FindFirst("sub")?.Value ??
                   user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
