using LearnLangs.Models.Games;
using Microsoft.EntityFrameworkCore;

namespace LearnLangs.Data
{
    public static class GameSeed
    {
        public static async Task SeedAsync(ApplicationDbContext db)
        {
            // Nếu Level 1 đã tồn tại thì bỏ qua (tránh seed trùng)
            if (await db.GameLevels.AnyAsync(l => l.Order == 1))
                return;

            // ===== Level 1: Fill in the blank =====
            var level1 = new GameLevel
            {
                Name = "Level 1 - Fill in blank",
                Type = GameType.FillInBlank,
                Order = 1,
                IsActive = true
            };

            level1.Questions = new List<GameQuestion>
            {
                new GameQuestion {
                    Type = GameType.FillInBlank,
                    // Dùng {blank} làm chỗ trống, tí nữa view sẽ tự render dấu gạch
                    Prompt = "My mother was {blank} singing when she was a young girl.",
                    CorrectText = "interested in"
                },
                new GameQuestion {
                    Type = GameType.FillInBlank,
                    Prompt = "It {blank} for him to keep that letter.",
                    CorrectText = "was unnecessary"
                },
                new GameQuestion {
                    Type = GameType.FillInBlank,
                    Prompt = "They are looking {blank} a new apartment.",
                    CorrectText = "for"
                },
                new GameQuestion {
                    Type = GameType.FillInBlank,
                    Prompt = "He is {blank} good {blank} math.",
                    CorrectText = "very at"
                }
            };

            db.GameLevels.Add(level1);
            await db.SaveChangesAsync(); // có Id cho level1

            // ===== Exams: tạo 4 bài thi map tới Level 1 (nếu chưa có) =====
            if (!await db.Exams.AnyAsync())
            {
                db.Exams.AddRange(
                    new Exam { Title = "Làm bài thi 1", Order = 1, GameLevelId = level1.Id },
                    new Exam { Title = "Làm bài thi 2", Order = 2, GameLevelId = level1.Id },
                    new Exam { Title = "Làm bài thi 3", Order = 3, GameLevelId = level1.Id },
                    new Exam { Title = "Làm bài thi 4", Order = 4, GameLevelId = level1.Id }
                );
                await db.SaveChangesAsync();
            }
        }
    }
}
