using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTOs.RequestDTO;
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
                    Category = p.Category!,
                    Price = p.Price,
                    Description = p.Description!,
                    Duration = p.Duration,
                    Status = (p.Price) > 0 ? "Active" : "Inactive" 
                });
            }
            return result;
        }

        public async Task UpdateAsync(int id,PackageMembershipUpdateRequestDTO request)
        {
            var package = await _repo.GetById(id);
            if (package == null)
            {
                throw new KeyNotFoundException("Package not found");
            }
            if (request.Category != null)
                package.Category = request.Category;
            if (request.Price.HasValue)
                package.Price = request.Price.Value;
            if (request.Description != null)
                package.Description = request.Description;
            if (request.Duration.HasValue)
                package.Duration = request.Duration.Value;
            await _repo.Update(package);
        }

    }
}