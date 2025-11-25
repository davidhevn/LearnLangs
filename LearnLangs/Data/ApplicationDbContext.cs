using LearnLangs.Models;
using LearnLangs.Models.Dictation;
using LearnLangs.Models.Flashcards;
using LearnLangs.Models.Games;
// Nếu các model PlacementTest nằm namespace riêng thì nhớ thêm using đúng namespace:
// using LearnLangs.Models.PlacementTests;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LearnLangs.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // ===== Core learning =====
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Lesson> Lessons => Set<Lesson>();
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<UserLesson> UserLessons => Set<UserLesson>();

        // ===== Dictation =====
        public DbSet<DictationTopic> DictationTopics => Set<DictationTopic>();
        public DbSet<DictationSet> DictationSets => Set<DictationSet>();
        public DbSet<DictationItem> DictationItems => Set<DictationItem>();
        public DbSet<UserDictationProgress> UserDictationProgresses => Set<UserDictationProgress>();

        // ===== Flashcards =====
        public DbSet<FlashcardDeck> FlashcardDecks => Set<FlashcardDeck>();
        public DbSet<FlashcardCard> FlashcardCards => Set<FlashcardCard>();
        // Nếu có Category:
        public DbSet<FlashcardCategory> FlashcardCategories => Set<FlashcardCategory>();

        // ===== Games & Exams =====
        public DbSet<GameLevel> GameLevels => Set<GameLevel>();
        public DbSet<GameQuestion> GameQuestions => Set<GameQuestion>();
        public DbSet<GameResult> GameResults => Set<GameResult>();
        public DbSet<Exam> Exams => Set<Exam>();
        public DbSet<ExamAttempt> ExamAttempts => Set<ExamAttempt>();

        // ===== Placement Test (MỚI) =====
        public DbSet<PlacementTest> PlacementTests => Set<PlacementTest>();
        public DbSet<TestQuestion> TestQuestions => Set<TestQuestion>();
        public DbSet<QuestionOption> QuestionOptions => Set<QuestionOption>();
        public DbSet<TestResult> TestResults => Set<TestResult>();
        public DbSet<UserAnswer> UserAnswers => Set<UserAnswer>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ---------------- Core Learning ----------------
            builder.Entity<Lesson>()
                .HasIndex(l => new { l.CourseId, l.OrderIndex })
                .IsUnique();

            builder.Entity<UserLesson>()
                .HasIndex(ul => new { ul.UserId, ul.LessonId })
                .IsUnique();

            // ---------------- Dictation ----------------
            builder.Entity<DictationSet>()
                .HasOne(s => s.Topic)
                .WithMany(t => t.Sets)
                .HasForeignKey(s => s.TopicId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DictationItem>()
                .HasOne(i => i.Set)
                .WithMany(s => s.Items)
                .HasForeignKey(i => i.SetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DictationSet>()
                .HasIndex(s => new { s.TopicId, s.OrderIndex });

            builder.Entity<DictationItem>()
                .HasIndex(i => new { i.SetId, i.OrderIndex });

            builder.Entity<UserDictationProgress>()
                .HasIndex(p => new { p.UserId, p.SetId })
                .IsUnique();

            // ---------------- Flashcards ----------------
            builder.Entity<FlashcardCard>()
                .HasOne(c => c.Deck)
                .WithMany(d => d.Cards)
                .HasForeignKey(c => c.DeckId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<FlashcardDeck>()
                .HasIndex(d => new { d.Mode, d.OrderIndex });

            builder.Entity<FlashcardCard>()
                .HasIndex(c => new { c.DeckId, c.OrderIndex });

            // ---------------- Games ----------------
            builder.Entity<GameLevel>()
                .HasMany(l => l.Questions)
                .WithOne(q => q.GameLevel)
                .HasForeignKey(q => q.GameLevelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<GameResult>()
                .HasIndex(r => new { r.UserId, r.GameLevelId });

            // ---------------- Exams ----------------
            builder.Entity<Exam>()
                .HasOne(e => e.GameLevel)
                .WithMany()
                .HasForeignKey(e => e.GameLevelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ExamAttempt>()
                .HasOne(a => a.Exam)
                .WithMany(e => e.Attempts)
                .HasForeignKey(a => a.ExamId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ExamAttempt>()
                .HasIndex(a => new { a.UserId, a.ExamId });

            // ---------------- Placement Test (MỚI) ----------------
            builder.Entity<PlacementTest>()
                .HasMany(t => t.Questions)
                .WithOne(q => q.PlacementTest)
                .HasForeignKey(q => q.TestId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TestQuestion>()
                .HasMany(q => q.Options)
                .WithOne(o => o.TestQuestion)
                .HasForeignKey(o => o.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TestResult>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TestResult>()
                .HasOne(r => r.PlacementTest)
                .WithMany()
                .HasForeignKey(r => r.TestId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TestResult>()
                .HasMany(r => r.UserAnswers)
                .WithOne(ua => ua.TestResult)
                .HasForeignKey(ua => ua.ResultId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserAnswer>()
                .HasOne(ua => ua.TestQuestion)
                .WithMany()
                .HasForeignKey(ua => ua.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserAnswer>()
                .HasOne(ua => ua.SelectedOption)
                .WithMany()
                .HasForeignKey(ua => ua.SelectedOptionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes cho Placement Test
            builder.Entity<TestQuestion>()
                .HasIndex(q => q.TestId);

            builder.Entity<QuestionOption>()
                .HasIndex(o => o.QuestionId);

            builder.Entity<TestResult>()
                .HasIndex(r => r.UserId);

            builder.Entity<TestResult>()
                .HasIndex(r => r.TestId);

            builder.Entity<UserAnswer>()
                .HasIndex(ua => ua.ResultId);

            // ---------------- Seed dữ liệu demo ----------------
            builder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "Spanish – Beginner", Description = "Basics of Spanish" }
            );

            builder.Entity<Lesson>().HasData(
                new Lesson { Id = 1, CourseId = 1, Title = "Greetings", OrderIndex = 1, XpReward = 10 },
                new Lesson { Id = 2, CourseId = 1, Title = "Numbers", OrderIndex = 2, XpReward = 10 }
            );

            builder.Entity<Question>().HasData(
                new Question
                {
                    Id = 1,
                    LessonId = 1,
                    Prompt = "Hola = ?",
                    IsMultipleChoice = true,
                    OptionA = "Hello",
                    OptionB = "Goodbye",
                    OptionC = "Please",
                    OptionD = "Thanks",
                    CorrectAnswer = "A"
                },
                new Question
                {
                    Id = 2,
                    LessonId = 2,
                    Prompt = "Dos = ?",
                    IsMultipleChoice = true,
                    OptionA = "One",
                    OptionB = "Two",
                    OptionC = "Three",
                    OptionD = "Four",
                    CorrectAnswer = "B"
                }
            );

            // ====== Chinese course (demo) ======
            const int chineseCourseId = 100;

            builder.Entity<Course>().HasData(
                new Course
                {
                    Id = chineseCourseId,
                    Name = "Chinese course",
                    Description = "Beginner Mandarin: greetings, numbers, self-intro with pinyin."
                }
            );

            builder.Entity<Lesson>().HasData(
                new Lesson { Id = 101, CourseId = chineseCourseId, Title = "Lesson 1 – Greetings", OrderIndex = 1, XpReward = 30 },
                new Lesson { Id = 102, CourseId = chineseCourseId, Title = "Lesson 2 – Numbers 1–10", OrderIndex = 2, XpReward = 30 },
                new Lesson { Id = 103, CourseId = chineseCourseId, Title = "Lesson 3 – Self-Introduction", OrderIndex = 3, XpReward = 40 }
            );

            builder.Entity<Question>().HasData(
                new Question
                {
                    Id = 1001,
                    LessonId = 101,
                    Prompt = "“你好” nghĩa là gì?",
                    IsMultipleChoice = true,
                    OptionA = "Tạm biệt",
                    OptionB = "Xin chào",
                    OptionC = "Cảm ơn",
                    OptionD = "Xin lỗi",
                    CorrectAnswer = "B"
                },
                new Question
                {
                    Id = 1002,
                    LessonId = 101,
                    Prompt = "“早上好” nghĩa là…",
                    IsMultipleChoice = true,
                    OptionA = "Chào buổi sáng",
                    OptionB = "Chúc ngủ ngon",
                    OptionC = "Chúc mừng",
                    OptionD = "Hẹn gặp lại",
                    CorrectAnswer = "A"
                },
                new Question
                {
                    Id = 1003,
                    LessonId = 101,
                    Prompt = "“你好吗？” nghĩa là…",
                    IsMultipleChoice = true,
                    OptionA = "Bạn tên gì?",
                    OptionB = "Bạn khỏe không?",
                    OptionC = "Bạn ở đâu?",
                    OptionD = "Bạn bao nhiêu tuổi?",
                    CorrectAnswer = "B"
                }
            );



            // ====== PlacementTests DEMO ======
            builder.Entity<PlacementTest>().HasData(
                new PlacementTest
                {
                    TestId = 1,
                    TestType = TestType.GeneralEnglish,
                    TestName = "General English",
                    Description = "Kiểm tra nhanh ngữ pháp và từ vựng giao tiếp hàng ngày.",
                    TotalQuestions = 5,
                    DurationMinutes = 10,
                    IsActive = true,
                    CreatedDate = new DateTime(2025, 1, 1)
                },
                new PlacementTest
                {
                    TestId = 2,
                    TestType = TestType.ForSchools,
                    TestName = "For Schools",
                    Description = "Bài test phù hợp học sinh, tập trung cấu trúc câu cơ bản.",
                    TotalQuestions = 5,
                    DurationMinutes = 10,
                    IsActive = true,
                    CreatedDate = new DateTime(2025, 1, 1)
                },
                new PlacementTest
                {
                    TestId = 3,
                    TestType = TestType.BusinessEnglish,
                    TestName = "Business English",
                    Description = "Kiểm tra vốn từ vựng và cấu trúc dùng trong môi trường công sở.",
                    TotalQuestions = 5,
                    DurationMinutes = 10,
                    IsActive = true,
                    CreatedDate = new DateTime(2025, 1, 1)
                },
                new PlacementTest
                {
                    TestId = 4,
                    TestType = TestType.YoungLearners,
                    TestName = "Young Learners",
                    Description = "Bài test thân thiện cho người mới bắt đầu và trẻ em.",
                    TotalQuestions = 5,
                    DurationMinutes = 10,
                    IsActive = true,
                    CreatedDate = new DateTime(2025, 1, 1)
                }
            );

            // ====== Questions DEMO (5 câu mỗi bài) ======
            builder.Entity<TestQuestion>().HasData(
                // Test 1: General English (id 1)
                new TestQuestion
                {
                    QuestionId = 101,
                    TestId = 1,
                    QuestionText = "Choose the correct option: She ___ to work every day.",
                    OrderNumber = 1,
                    Points = 1
                },
                new TestQuestion
                {
                    QuestionId = 102,
                    TestId = 1,
                    QuestionText = "Which sentence is correct?",
                    OrderNumber = 2,
                    Points = 1
                },
                new TestQuestion
                {
                    QuestionId = 103,
                    TestId = 1,
                    QuestionText = "What is the past simple of \"go\"?",
                    OrderNumber = 3,
                    Points = 1
                },
                new TestQuestion
                {
                    QuestionId = 104,
                    TestId = 1,
                    QuestionText = "Choose the correct article: I have ___ orange.",
                    OrderNumber = 4,
                    Points = 1
                },
                new TestQuestion
                {
                    QuestionId = 105,
                    TestId = 1,
                    QuestionText = "Choose the best response: \"How are you?\"",
                    OrderNumber = 5,
                    Points = 1
                },

                // Test 2: For Schools (id 2)
                new TestQuestion
                {
                    QuestionId = 201,
                    TestId = 2,
                    QuestionText = "Choose the correct word: This is my ___ . (mother / eat / blue / quickly)",
                    OrderNumber = 1,
                    Points = 1
                },
                new TestQuestion
                {
                    QuestionId = 202,
                    TestId = 2,
                    QuestionText = "Which is a question?",
                    OrderNumber = 2,
                    Points = 1
                },
                new TestQuestion
                {
                    QuestionId = 203,
                    TestId = 2,
                    QuestionText = "Choose the correct form: They ___ football after school.",
                    OrderNumber = 3,
                    Points = 1
                },
                new TestQuestion
                {
                    QuestionId = 204,
                    TestId = 2,
                    QuestionText = "What time is it? 7:30",
                    OrderNumber = 4,
                    Points = 1
                },
                new TestQuestion
                {
                    QuestionId = 205,
                    TestId = 2,
                    QuestionText = "Choose the opposite of \"big\".",
                    OrderNumber = 5,
                    Points = 1
                },

                // Test 3: Business English (id 3)
                new TestQuestion
                {
                    QuestionId = 301,
                    TestId = 3,
                    QuestionText = "Choose the best word: We need to ___ the meeting to next Monday.",
                    OrderNumber = 1,
                    Points = 1
                },
                new TestQuestion
                {
                    QuestionId = 302,
                    TestId = 3,
                    QuestionText = "Which sentence is appropriate in a formal email?",
                    OrderNumber = 2,
                    Points = 1
                },
                new TestQuestion
                {
                    QuestionId = 303,
                    TestId = 3,
                    QuestionText = "Choose the correct phrase: \"I am writing to ___ information about your product.\"",
                    OrderNumber = 3,
                    Points = 1
                },
                new TestQuestion
                {
                    QuestionId = 304,
                    TestId = 3,
                    QuestionText = "What does \"deadline\" mean?",
                    OrderNumber = 4,
                    Points = 1
                },
                new TestQuestion
                {
                    QuestionId = 305,
                    TestId = 3,
                    QuestionText = "Choose the correct option: \"Could you please ___ the file by Friday?\"",
                    OrderNumber = 5,
                    Points = 1
                },

                // Test 4: Young Learners (id 4)
                new TestQuestion
                {
                    QuestionId = 401,
                    TestId = 4,
                    QuestionText = "What color is the sky on a sunny day?",
                    OrderNumber = 1,
                    Points = 1
                },
                new TestQuestion
                {
                    QuestionId = 402,
                    TestId = 4,
                    QuestionText = "Which animal can fly?",
                    OrderNumber = 2,
                    Points = 1
                },
                new TestQuestion
                {
                    QuestionId = 403,
                    TestId = 4,
                    QuestionText = "How many days are there in a week?",
                    OrderNumber = 3,
                    Points = 1
                },
                new TestQuestion
                {
                    QuestionId = 404,
                    TestId = 4,
                    QuestionText = "Choose the correct word: \"I ___ ice cream.\"",
                    OrderNumber = 4,
                    Points = 1
                },
                new TestQuestion
                {
                    QuestionId = 405,
                    TestId = 4,
                    QuestionText = "Which one is a fruit?",
                    OrderNumber = 5,
                    Points = 1
                }
            );

            builder.Entity<QuestionOption>().HasData(
                // -------- Test 1: General English --------
                // Q101
                new QuestionOption { OptionId = 1001, QuestionId = 101, OptionText = "go", IsCorrect = false, OrderNumber = 1 },
                new QuestionOption { OptionId = 1002, QuestionId = 101, OptionText = "goes", IsCorrect = true, OrderNumber = 2 },
                new QuestionOption { OptionId = 1003, QuestionId = 101, OptionText = "going", IsCorrect = false, OrderNumber = 3 },
                new QuestionOption { OptionId = 1004, QuestionId = 101, OptionText = "went", IsCorrect = false, OrderNumber = 4 },

                // Q102
                new QuestionOption { OptionId = 1005, QuestionId = 102, OptionText = "He go to school.", IsCorrect = false, OrderNumber = 1 },
                new QuestionOption { OptionId = 1006, QuestionId = 102, OptionText = "He goes to school.", IsCorrect = true, OrderNumber = 2 },
                new QuestionOption { OptionId = 1007, QuestionId = 102, OptionText = "He to school goes.", IsCorrect = false, OrderNumber = 3 },
                new QuestionOption { OptionId = 1008, QuestionId = 102, OptionText = "Goes he to school.", IsCorrect = false, OrderNumber = 4 },

                // Q103
                new QuestionOption { OptionId = 1009, QuestionId = 103, OptionText = "goed", IsCorrect = false, OrderNumber = 1 },
                new QuestionOption { OptionId = 1010, QuestionId = 103, OptionText = "went", IsCorrect = true, OrderNumber = 2 },
                new QuestionOption { OptionId = 1011, QuestionId = 103, OptionText = "goes", IsCorrect = false, OrderNumber = 3 },
                new QuestionOption { OptionId = 1012, QuestionId = 103, OptionText = "going", IsCorrect = false, OrderNumber = 4 },

                // Q104
                new QuestionOption { OptionId = 1013, QuestionId = 104, OptionText = "a", IsCorrect = false, OrderNumber = 1 },
                new QuestionOption { OptionId = 1014, QuestionId = 104, OptionText = "an", IsCorrect = true, OrderNumber = 2 },
                new QuestionOption { OptionId = 1015, QuestionId = 104, OptionText = "the", IsCorrect = false, OrderNumber = 3 },
                new QuestionOption { OptionId = 1016, QuestionId = 104, OptionText = "no article", IsCorrect = false, OrderNumber = 4 },

                // Q105
                new QuestionOption { OptionId = 1017, QuestionId = 105, OptionText = "I'm fine, thank you.", IsCorrect = true, OrderNumber = 1 },
                new QuestionOption { OptionId = 1018, QuestionId = 105, OptionText = "I'm twenty years.", IsCorrect = false, OrderNumber = 2 },
                new QuestionOption { OptionId = 1019, QuestionId = 105, OptionText = "I am from student.", IsCorrect = false, OrderNumber = 3 },
                new QuestionOption { OptionId = 1020, QuestionId = 105, OptionText = "I play football.", IsCorrect = false, OrderNumber = 4 },

                // -------- Test 2: For Schools --------
                // Q201
                new QuestionOption { OptionId = 1021, QuestionId = 201, OptionText = "mother", IsCorrect = true, OrderNumber = 1 },
                new QuestionOption { OptionId = 1022, QuestionId = 201, OptionText = "eat", IsCorrect = false, OrderNumber = 2 },
                new QuestionOption { OptionId = 1023, QuestionId = 201, OptionText = "blue", IsCorrect = false, OrderNumber = 3 },
                new QuestionOption { OptionId = 1024, QuestionId = 201, OptionText = "quickly", IsCorrect = false, OrderNumber = 4 },

                // Q202
                new QuestionOption { OptionId = 1025, QuestionId = 202, OptionText = "I like pizza.", IsCorrect = false, OrderNumber = 1 },
                new QuestionOption { OptionId = 1026, QuestionId = 202, OptionText = "Do you like pizza?", IsCorrect = true, OrderNumber = 2 },
                new QuestionOption { OptionId = 1027, QuestionId = 202, OptionText = "He playing football.", IsCorrect = false, OrderNumber = 3 },
                new QuestionOption { OptionId = 1028, QuestionId = 202, OptionText = "She my sister.", IsCorrect = false, OrderNumber = 4 },

                // Q203
                new QuestionOption { OptionId = 1029, QuestionId = 203, OptionText = "plays", IsCorrect = false, OrderNumber = 1 },
                new QuestionOption { OptionId = 1030, QuestionId = 203, OptionText = "play", IsCorrect = true, OrderNumber = 2 },
                new QuestionOption { OptionId = 1031, QuestionId = 203, OptionText = "playing", IsCorrect = false, OrderNumber = 3 },
                new QuestionOption { OptionId = 1032, QuestionId = 203, OptionText = "played", IsCorrect = false, OrderNumber = 4 },

                // Q204
                new QuestionOption { OptionId = 1033, QuestionId = 204, OptionText = "It's seven thirty.", IsCorrect = true, OrderNumber = 1 },
                new QuestionOption { OptionId = 1034, QuestionId = 204, OptionText = "It's seven thirteen.", IsCorrect = false, OrderNumber = 2 },
                new QuestionOption { OptionId = 1035, QuestionId = 204, OptionText = "It's six thirty.", IsCorrect = false, OrderNumber = 3 },
                new QuestionOption { OptionId = 1036, QuestionId = 204, OptionText = "It's eight thirty.", IsCorrect = false, OrderNumber = 4 },

                // Q205
                new QuestionOption { OptionId = 1037, QuestionId = 205, OptionText = "small", IsCorrect = true, OrderNumber = 1 },
                new QuestionOption { OptionId = 1038, QuestionId = 205, OptionText = "long", IsCorrect = false, OrderNumber = 2 },
                new QuestionOption { OptionId = 1039, QuestionId = 205, OptionText = "fast", IsCorrect = false, OrderNumber = 3 },
                new QuestionOption { OptionId = 1040, QuestionId = 205, OptionText = "young", IsCorrect = false, OrderNumber = 4 },

                // -------- Test 3: Business English --------
                // Q301
                new QuestionOption { OptionId = 1041, QuestionId = 301, OptionText = "cancel", IsCorrect = false, OrderNumber = 1 },
                new QuestionOption { OptionId = 1042, QuestionId = 301, OptionText = "move", IsCorrect = false, OrderNumber = 2 },
                new QuestionOption { OptionId = 1043, QuestionId = 301, OptionText = "reschedule", IsCorrect = true, OrderNumber = 3 },
                new QuestionOption { OptionId = 1044, QuestionId = 301, OptionText = "forget", IsCorrect = false, OrderNumber = 4 },

                // Q302
                new QuestionOption { OptionId = 1045, QuestionId = 302, OptionText = "Hey! Send me the file.", IsCorrect = false, OrderNumber = 1 },
                new QuestionOption { OptionId = 1046, QuestionId = 302, OptionText = "Please send me the file now!!!", IsCorrect = false, OrderNumber = 2 },
                new QuestionOption { OptionId = 1047, QuestionId = 302, OptionText = "Could you please send me the file?", IsCorrect = true, OrderNumber = 3 },
                new QuestionOption { OptionId = 1048, QuestionId = 302, OptionText = "You send file.", IsCorrect = false, OrderNumber = 4 },

                // Q303
                new QuestionOption { OptionId = 1049, QuestionId = 303, OptionText = "ask", IsCorrect = false, OrderNumber = 1 },
                new QuestionOption { OptionId = 1050, QuestionId = 303, OptionText = "request", IsCorrect = false, OrderNumber = 2 },
                new QuestionOption { OptionId = 1051, QuestionId = 303, OptionText = "inquire about", IsCorrect = true, OrderNumber = 3 },
                new QuestionOption { OptionId = 1052, QuestionId = 303, OptionText = "say", IsCorrect = false, OrderNumber = 4 },

                // Q304
                new QuestionOption { OptionId = 1053, QuestionId = 304, OptionText = "the place of the meeting", IsCorrect = false, OrderNumber = 1 },
                new QuestionOption { OptionId = 1054, QuestionId = 304, OptionText = "the latest time to finish something", IsCorrect = true, OrderNumber = 2 },
                new QuestionOption { OptionId = 1055, QuestionId = 304, OptionText = "the first day at work", IsCorrect = false, OrderNumber = 3 },
                new QuestionOption { OptionId = 1056, QuestionId = 304, OptionText = "a coffee break", IsCorrect = false, OrderNumber = 4 },

                // Q305
                new QuestionOption { OptionId = 1057, QuestionId = 305, OptionText = "send", IsCorrect = false, OrderNumber = 1 },
                new QuestionOption { OptionId = 1058, QuestionId = 305, OptionText = "finish", IsCorrect = true, OrderNumber = 2 },
                new QuestionOption { OptionId = 1059, QuestionId = 305, OptionText = "play", IsCorrect = false, OrderNumber = 3 },
                new QuestionOption { OptionId = 1060, QuestionId = 305, OptionText = "eat", IsCorrect = false, OrderNumber = 4 },

                // -------- Test 4: Young Learners --------
                // Q401
                new QuestionOption { OptionId = 1061, QuestionId = 401, OptionText = "blue", IsCorrect = true, OrderNumber = 1 },
                new QuestionOption { OptionId = 1062, QuestionId = 401, OptionText = "black", IsCorrect = false, OrderNumber = 2 },
                new QuestionOption { OptionId = 1063, QuestionId = 401, OptionText = "green", IsCorrect = false, OrderNumber = 3 },
                new QuestionOption { OptionId = 1064, QuestionId = 401, OptionText = "yellow", IsCorrect = false, OrderNumber = 4 },

                // Q402
                new QuestionOption { OptionId = 1065, QuestionId = 402, OptionText = "dog", IsCorrect = false, OrderNumber = 1 },
                new QuestionOption { OptionId = 1066, QuestionId = 402, OptionText = "fish", IsCorrect = false, OrderNumber = 2 },
                new QuestionOption { OptionId = 1067, QuestionId = 402, OptionText = "bird", IsCorrect = true, OrderNumber = 3 },
                new QuestionOption { OptionId = 1068, QuestionId = 402, OptionText = "turtle", IsCorrect = false, OrderNumber = 4 },

                // Q403
                new QuestionOption { OptionId = 1069, QuestionId = 403, OptionText = "5", IsCorrect = false, OrderNumber = 1 },
                new QuestionOption { OptionId = 1070, QuestionId = 403, OptionText = "6", IsCorrect = false, OrderNumber = 2 },
                new QuestionOption { OptionId = 1071, QuestionId = 403, OptionText = "7", IsCorrect = true, OrderNumber = 3 },
                new QuestionOption { OptionId = 1072, QuestionId = 403, OptionText = "8", IsCorrect = false, OrderNumber = 4 },

                // Q404
                new QuestionOption { OptionId = 1073, QuestionId = 404, OptionText = "like", IsCorrect = true, OrderNumber = 1 },
                new QuestionOption { OptionId = 1074, QuestionId = 404, OptionText = "likes", IsCorrect = false, OrderNumber = 2 },
                new QuestionOption { OptionId = 1075, QuestionId = 404, OptionText = "liking", IsCorrect = false, OrderNumber = 3 },
                new QuestionOption { OptionId = 1076, QuestionId = 404, OptionText = "liked", IsCorrect = false, OrderNumber = 4 },

                // Q405
                new QuestionOption { OptionId = 1077, QuestionId = 405, OptionText = "car", IsCorrect = false, OrderNumber = 1 },
                new QuestionOption { OptionId = 1078, QuestionId = 405, OptionText = "banana", IsCorrect = true, OrderNumber = 2 },
                new QuestionOption { OptionId = 1079, QuestionId = 405, OptionText = "chair", IsCorrect = false, OrderNumber = 3 },
                new QuestionOption { OptionId = 1080, QuestionId = 405, OptionText = "pencil", IsCorrect = false, OrderNumber = 4 }
            );




            // ====== Seed Dictation DEMO (tuỳ chọn) ======
            var topicId = 2000;
            var setId = 2100;

            builder.Entity<DictationTopic>().HasData(
                new DictationTopic
                {
                    Id = topicId,
                    Title = "IELTS Listening (Demo)",
                    Description = "Mini demo",
                    CoverUrl = "/img/ielts.png"
                }
            );

            builder.Entity<DictationSet>().HasData(
                new DictationSet
                {
                    Id = setId,
                    TopicId = topicId,
                    Title = "Cam 20 – Test 1 – Part 1 (Demo)",
                    Level = "B2",
                    OrderIndex = 1
                }
            );

            builder.Entity<DictationItem>().HasData(
                new DictationItem
                {
                    Id = 2101,
                    SetId = setId,
                    OrderIndex = 1,
                    Transcript = "I've been meaning to ask you for some advice about restaurants.",
                    AudioUrl = "/audio/demo1.mp3"
                },
                new DictationItem
                {
                    Id = 2102,
                    SetId = setId,
                    OrderIndex = 2,
                    Transcript = "I need to book somewhere to celebrate my sister's thirtieth birthday.",
                    AudioUrl = "/audio/demo2.mp3"
                }
            );
        }
    }
}
