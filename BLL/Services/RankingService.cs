using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.ResponseDTO;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class RankingService(RankingRepository _repo)
    {
        //Lấy danh sách trong bảng ranking dựa vào số điểm xếp giảm dần
        public async Task<List<RankingResponseDTO>> GetAllAsync()
        {
            var rankList = await _repo.GetAll();

            var sortedRankings = rankList
                .OrderByDescending(r => r.TotalScore ?? 0)
                .ToList();

            // Đánh số thứ hạng (Rank), score bằng nhau thì cùng vị trí
            var result = new List<RankingResponseDTO>();
            int rank = 1;
            int prevScore = -1;
            int sameRankCount = 0;

            for (int i = 0; i < sortedRankings.Count; i++)
            {
                var r = sortedRankings[i];
                int score = r.TotalScore ?? 0;

                if (score != prevScore)
                {
                    rank = i + 1;
                    sameRankCount = 1;
                }
                else
                {
                    sameRankCount++;
                }

                result.Add(new RankingResponseDTO
                {
                    Rank = rank,
                    FullName = r.Member.User.FullName ?? string.Empty,
                    Badge = r.Badge ?? string.Empty,
                    TotalScore = score
                });

                prevScore = score;
            }

            return result;
        }

        public async Task<RankingResponseDTO> GetByAccountIdAsync(int accountId)
        {
            var ranking = await _repo.GetById(accountId);
            if (ranking == null) return null;
            //Lay danh sach toan bo de tinh thu hang
            var rankList = await _repo.GetAll();
            var sortedRank = rankList
                .OrderByDescending(r => r.TotalScore ?? 0)
                .ToList();

            //tim vi tri cua account id trong bang xep hang
            int rank = sortedRank.FindIndex(r => r.Member.AccountId == accountId) + 1;
            return new RankingResponseDTO
            {
                Rank = rank,
                FullName = ranking.Member?.User?.FullName ?? string.Empty,
                Badge = ranking.Badge ?? string.Empty,
                TotalScore = ranking.TotalScore ?? 0
            };
        }
    }
}
