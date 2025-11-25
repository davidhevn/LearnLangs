using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnLangs.Models
{
    // Loại bài test
    public enum TestType
    {
        GeneralEnglish = 1,
        ForSchools = 2,
        BusinessEnglish = 3,
        YoungLearners = 4
    }

    // Mức độ kết quả
    public enum ProficiencyLevel
    {
        Beginner = 1,          // A1
        Elementary = 2,        // A2
        Intermediate = 3,      // B1
        UpperIntermediate = 4, // B2
        Advanced = 5,          // C1
        Proficient = 6         // C2
    }

    // Bài test tổng quát
    public class PlacementTest
    {
        [Key]
        public int TestId { get; set; }

        [Required]
        public TestType TestType { get; set; }

        [Required]
        [StringLength(200)]
        public string TestName { get; set; } = default!;

        [StringLength(1000)]
        public string? Description { get; set; }

        public int TotalQuestions { get; set; }

        public int DurationMinutes { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Navigation property
        public virtual ICollection<TestQuestion> Questions { get; set; } = new List<TestQuestion>();
    }

    // Câu hỏi trong bài test
    public class TestQuestion
    {
        [Key]
        public int QuestionId { get; set; }

        [ForeignKey(nameof(PlacementTest))]
        public int TestId { get; set; }

        [Required]
        [StringLength(1000)]
        public string QuestionText { get; set; } = default!;

        public int OrderNumber { get; set; }

        public int Points { get; set; } // Điểm cho câu hỏi này

        // Navigation properties
        public virtual PlacementTest PlacementTest { get; set; } = default!;
        public virtual ICollection<QuestionOption> Options { get; set; } = new List<QuestionOption>();
    }

    // Các đáp án cho mỗi câu hỏi
    public class QuestionOption
    {
        [Key]
        public int OptionId { get; set; }

        [ForeignKey(nameof(TestQuestion))]
        public int QuestionId { get; set; }

        [Required]
        [StringLength(500)]
        public string OptionText { get; set; } = default!;

        public bool IsCorrect { get; set; }

        public int OrderNumber { get; set; }

        // Navigation property
        public virtual TestQuestion TestQuestion { get; set; } = default!;
    }

    // Kết quả test của user
    public class TestResult
    {
        [Key]
        public int ResultId { get; set; }

        // User của Identity => ApplicationUser, key mặc định là string
        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; } = default!;

        [ForeignKey(nameof(PlacementTest))]
        public int TestId { get; set; }

        public int TotalScore { get; set; }

        public int MaxScore { get; set; }

        public double Percentage { get; set; }

        public ProficiencyLevel Level { get; set; }

        [StringLength(2000)]
        public string? Recommendation { get; set; } // Gợi ý lộ trình học

        public DateTime CompletedDate { get; set; }

        // Navigation properties
        public virtual ApplicationUser User { get; set; } = default!;
        public virtual PlacementTest PlacementTest { get; set; } = default!;
        public virtual ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
    }

    // Câu trả lời của user
    public class UserAnswer
    {
        [Key]
        public int UserAnswerId { get; set; }

        [ForeignKey(nameof(TestResult))]
        public int ResultId { get; set; }

        [ForeignKey(nameof(TestQuestion))]
        public int QuestionId { get; set; }

        [ForeignKey(nameof(QuestionOption))]
        public int SelectedOptionId { get; set; }

        public bool IsCorrect { get; set; }

        // Navigation properties
        public virtual TestResult TestResult { get; set; } = default!;
        public virtual TestQuestion TestQuestion { get; set; } = default!;
        public virtual QuestionOption SelectedOption { get; set; } = default!;
    }
}
