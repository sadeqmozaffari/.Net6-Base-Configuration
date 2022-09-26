using App.Entities.Identity;
using System.Threading.Tasks;

namespace App.Services.Api.Contract
{
    public interface IjwtService
    {
        Task<string> GenerateTokenAsync(User User);
    }
}
