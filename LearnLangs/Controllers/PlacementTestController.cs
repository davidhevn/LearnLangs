using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using LearnLangs.Data;
using LearnLangs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnLangs.Controllers
{
    [Authorize]
    public class PlacementTestController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PlacementTestController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: /PlacementTest
        public async Task<IActionResult> Index()
        {
            var tests = await _db.PlacementTests
                .Where(t => t.IsActive)
                .OrderBy(t => t.TestId)
                .ToListAsync();

            return View(tests);
        }

        // GET: /PlacementTest/Start/1
        [HttpGet]
        public async Task<IActionResult> Start(int id)
        {
            var test = await _db.PlacementTests
                .Include(t => t.Questions)
                    .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(t => t.TestId == id && t.IsActive);

            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // POST: /PlacementTest/Submit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(int testId)
        {
            var test = await _db.PlacementTests
                .Include(t => t.Questions)
                    .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(t => t.TestId == testId && t.IsActive);

            if (test == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Challenge(); // bắt login
            }

            // ===== 1. Đọc form & kiểm tra câu hỏi chưa trả lời =====
            var selectedMap = new Dictionary<int, int>();   // QuestionId -> OptionId
            var missingQuestions = new List<int>();

            foreach (var question in test.Questions.OrderBy(q => q.OrderNumber))
            {
                string formKey = $"q_{question.QuestionId}";
                var value = Request.Form[formKey].FirstOrDefault();

                if (string.IsNullOrWhiteSpace(value) ||
                    !int.TryParse(value, out var selectedOptionId) ||
                    !question.Options.Any(o => o.OptionId == selectedOptionId))
                {
                    // chưa chọn hoặc chọn option không hợp lệ
                    missingQuestions.Add(question.QuestionId);
                }
                else
                {
                    selectedMap[question.QuestionId] = selectedOptionId;
                }
            }

            if (missingQuestions.Any())
            {
                // Không cho nộp khi còn câu hỏi trống
                ModelState.AddModelError(string.Empty,
                    "Bạn phải trả lời tất cả câu hỏi trước khi nộp bài.");

                // Trả lại view Start với cùng bài test
                return View("Start", test);
            }

            // ===== 2. Tính điểm & tạo TestResult =====
            int maxScore = test.Questions.Sum(q => q.Points);
            int totalScore = 0;

            var result = new TestResult
            {
                TestId = test.TestId,
                UserId = userId,
                CompletedDate = DateTime.UtcNow,
                MaxScore = maxScore,
                UserAnswers = new List<UserAnswer>()
            };

            foreach (var question in test.Questions.OrderBy(q => q.OrderNumber))
            {
                var selectedOptionId = selectedMap[question.QuestionId];

                bool isCorrect = question.Options.Any(o => o.OptionId == selectedOptionId && o.IsCorrect);
                if (isCorrect)
                {
                    totalScore += question.Points;
                }

                result.UserAnswers.Add(new UserAnswer
                {
                    QuestionId = question.QuestionId,
                    SelectedOptionId = selectedOptionId,
                    IsCorrect = isCorrect
                });
            }

            result.TotalScore = totalScore;
            result.Percentage = maxScore == 0 ? 0 : (double)totalScore / maxScore * 100.0;
            result.Level = MapToLevel(result.Percentage);
            result.Recommendation = BuildRecommendation(result.Level);

            _db.TestResults.Add(result);
            await _db.SaveChangesAsync();

            // load lại kèm PlacementTest để show tên test
            var fullResult = await _db.TestResults
                .Include(r => r.PlacementTest)
                .FirstAsync(r => r.ResultId == result.ResultId);

            return View("Result", fullResult);
        }

        private ProficiencyLevel MapToLevel(double percentage)
        {
            if (percentage < 30) return ProficiencyLevel.Beginner;          // A1
            if (percentage < 50) return ProficiencyLevel.Elementary;        // A2
            if (percentage < 70) return ProficiencyLevel.Intermediate;      // B1
            if (percentage < 85) return ProficiencyLevel.UpperIntermediate; // B2
            if (percentage < 95) return ProficiencyLevel.Advanced;          // C1
            return ProficiencyLevel.Proficient;                             // C2
        }

        private string BuildRecommendation(ProficiencyLevel level)
        {
            switch (level)
            {
                case ProficiencyLevel.Beginner:
                    return "Trình độ khoảng A1 – nên bắt đầu với khoá cơ bản: alphabet, câu chào hỏi, ngữ pháp hiện tại đơn.";
                case ProficiencyLevel.Elementary:
                    return "Trình độ khoảng A2 – hãy luyện thêm từ vựng giao tiếp hàng ngày và thì quá khứ đơn.";
                case ProficiencyLevel.Intermediate:
                    return "Trình độ khoảng B1 – bạn có thể theo học các khoá giao tiếp trung cấp, luyện kỹ năng nghe – nói nhiều hơn.";
                case ProficiencyLevel.UpperIntermediate:
                    return "Trình độ khoảng B2 – phù hợp với khoá luyện thi IELTS 5.5–6.5 hoặc Business English cơ bản.";
                case ProficiencyLevel.Advanced:
                    return "Trình độ khoảng C1 – có thể học các khoá học học thuật, presentation, debate bằng tiếng Anh.";
                case ProficiencyLevel.Proficient:
                    return "Trình độ khoảng C2 – bạn sử dụng tiếng Anh rất tốt, nên tập trung vào chuyên ngành hoặc mục tiêu cụ thể.";
            }
            return "";
        }
    }
}
