using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTOs.ResponseDTO;
using DAL.Repositories;

namespace BLL.Services
{
    public class PackageMembershipService
    {
        private readonly PackageMembershipRepository _repo;

        public PackageMembershipService(PackageMembershipRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<PackageMembershipResponseDTO>> GetAllPackageMembershipsAsync()
        {
            var packages = await _repo.GetAll();
            var result = new List<PackageMembershipResponseDTO>();
            foreach (var p in packages)
            {
                result.Add(new PackageMembershipResponseDTO
                {
                    Package_membership_ID = p.PackageMembershipId,
                    Category = p.Category ?? string.Empty,
                    Price = p.Price ?? 0,
                    Description = p.Description ?? string.Empty,
                    Duration = p.Duration ?? 0,
                    Status = (p.Price ?? 0) > 0 ? "Active" : "Inactive" // hoặc gán logic khác tùy ý
                });
            }
            return result;
        }
    }
}