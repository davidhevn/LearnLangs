using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LearnLangs.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedPlacementTestsDemo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PlacementTests",
                columns: new[] { "TestId", "CreatedDate", "Description", "DurationMinutes", "IsActive", "TestName", "TestType", "TotalQuestions" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kiểm tra nhanh ngữ pháp và từ vựng giao tiếp hàng ngày.", 10, true, "General English", 1, 5 },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bài test phù hợp học sinh, tập trung cấu trúc câu cơ bản.", 10, true, "For Schools", 2, 5 },
                    { 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kiểm tra vốn từ vựng và cấu trúc dùng trong môi trường công sở.", 10, true, "Business English", 3, 5 },
                    { 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bài test thân thiện cho người mới bắt đầu và trẻ em.", 10, true, "Young Learners", 4, 5 }
                });

            migrationBuilder.InsertData(
                table: "TestQuestions",
                columns: new[] { "QuestionId", "OrderNumber", "Points", "QuestionText", "TestId" },
                values: new object[,]
                {
                    { 101, 1, 1, "Choose the correct option: She ___ to work every day.", 1 },
                    { 102, 2, 1, "Which sentence is correct?", 1 },
                    { 103, 3, 1, "What is the past simple of \"go\"?", 1 },
                    { 104, 4, 1, "Choose the correct article: I have ___ orange.", 1 },
                    { 105, 5, 1, "Choose the best response: \"How are you?\"", 1 },
                    { 201, 1, 1, "Choose the correct word: This is my ___ . (mother / eat / blue / quickly)", 2 },
                    { 202, 2, 1, "Which is a question?", 2 },
                    { 203, 3, 1, "Choose the correct form: They ___ football after school.", 2 },
                    { 204, 4, 1, "What time is it? 7:30", 2 },
                    { 205, 5, 1, "Choose the opposite of \"big\".", 2 },
                    { 301, 1, 1, "Choose the best word: We need to ___ the meeting to next Monday.", 3 },
                    { 302, 2, 1, "Which sentence is appropriate in a formal email?", 3 },
                    { 303, 3, 1, "Choose the correct phrase: \"I am writing to ___ information about your product.\"", 3 },
                    { 304, 4, 1, "What does \"deadline\" mean?", 3 },
                    { 305, 5, 1, "Choose the correct option: \"Could you please ___ the file by Friday?\"", 3 },
                    { 401, 1, 1, "What color is the sky on a sunny day?", 4 },
                    { 402, 2, 1, "Which animal can fly?", 4 },
                    { 403, 3, 1, "How many days are there in a week?", 4 },
                    { 404, 4, 1, "Choose the correct word: \"I ___ ice cream.\"", 4 },
                    { 405, 5, 1, "Which one is a fruit?", 4 }
                });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "OptionId", "IsCorrect", "OptionText", "OrderNumber", "QuestionId" },
                values: new object[,]
                {
                    { 1001, false, "go", 1, 101 },
                    { 1002, true, "goes", 2, 101 },
                    { 1003, false, "going", 3, 101 },
                    { 1004, false, "went", 4, 101 },
                    { 1005, false, "He go to school.", 1, 102 },
                    { 1006, true, "He goes to school.", 2, 102 },
                    { 1007, false, "He to school goes.", 3, 102 },
                    { 1008, false, "Goes he to school.", 4, 102 },
                    { 1009, false, "goed", 1, 103 },
                    { 1010, true, "went", 2, 103 },
                    { 1011, false, "goes", 3, 103 },
                    { 1012, false, "going", 4, 103 },
                    { 1013, false, "a", 1, 104 },
                    { 1014, true, "an", 2, 104 },
                    { 1015, false, "the", 3, 104 },
                    { 1016, false, "no article", 4, 104 },
                    { 1017, true, "I'm fine, thank you.", 1, 105 },
                    { 1018, false, "I'm twenty years.", 2, 105 },
                    { 1019, false, "I am from student.", 3, 105 },
                    { 1020, false, "I play football.", 4, 105 },
                    { 1021, true, "mother", 1, 201 },
                    { 1022, false, "eat", 2, 201 },
                    { 1023, false, "blue", 3, 201 },
                    { 1024, false, "quickly", 4, 201 },
                    { 1025, false, "I like pizza.", 1, 202 },
                    { 1026, true, "Do you like pizza?", 2, 202 },
                    { 1027, false, "He playing football.", 3, 202 },
                    { 1028, false, "She my sister.", 4, 202 },
                    { 1029, false, "plays", 1, 203 },
                    { 1030, true, "play", 2, 203 },
                    { 1031, false, "playing", 3, 203 },
                    { 1032, false, "played", 4, 203 },
                    { 1033, true, "It's seven thirty.", 1, 204 },
                    { 1034, false, "It's seven thirteen.", 2, 204 },
                    { 1035, false, "It's six thirty.", 3, 204 },
                    { 1036, false, "It's eight thirty.", 4, 204 },
                    { 1037, true, "small", 1, 205 },
                    { 1038, false, "long", 2, 205 },
                    { 1039, false, "fast", 3, 205 },
                    { 1040, false, "young", 4, 205 },
                    { 1041, false, "cancel", 1, 301 },
                    { 1042, false, "move", 2, 301 },
                    { 1043, true, "reschedule", 3, 301 },
                    { 1044, false, "forget", 4, 301 },
                    { 1045, false, "Hey! Send me the file.", 1, 302 },
                    { 1046, false, "Please send me the file now!!!", 2, 302 },
                    { 1047, true, "Could you please send me the file?", 3, 302 },
                    { 1048, false, "You send file.", 4, 302 },
                    { 1049, false, "ask", 1, 303 },
                    { 1050, false, "request", 2, 303 },
                    { 1051, true, "inquire about", 3, 303 },
                    { 1052, false, "say", 4, 303 },
                    { 1053, false, "the place of the meeting", 1, 304 },
                    { 1054, true, "the latest time to finish something", 2, 304 },
                    { 1055, false, "the first day at work", 3, 304 },
                    { 1056, false, "a coffee break", 4, 304 },
                    { 1057, false, "send", 1, 305 },
                    { 1058, true, "finish", 2, 305 },
                    { 1059, false, "play", 3, 305 },
                    { 1060, false, "eat", 4, 305 },
                    { 1061, true, "blue", 1, 401 },
                    { 1062, false, "black", 2, 401 },
                    { 1063, false, "green", 3, 401 },
                    { 1064, false, "yellow", 4, 401 },
                    { 1065, false, "dog", 1, 402 },
                    { 1066, false, "fish", 2, 402 },
                    { 1067, true, "bird", 3, 402 },
                    { 1068, false, "turtle", 4, 402 },
                    { 1069, false, "5", 1, 403 },
                    { 1070, false, "6", 2, 403 },
                    { 1071, true, "7", 3, 403 },
                    { 1072, false, "8", 4, 403 },
                    { 1073, true, "like", 1, 404 },
                    { 1074, false, "likes", 2, 404 },
                    { 1075, false, "liking", 3, 404 },
                    { 1076, false, "liked", 4, 404 },
                    { 1077, false, "car", 1, 405 },
                    { 1078, true, "banana", 2, 405 },
                    { 1079, false, "chair", 3, 405 },
                    { 1080, false, "pencil", 4, 405 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1007);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1008);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1009);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1010);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1011);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1012);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1013);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1014);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1015);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1016);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1017);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1018);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1019);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1020);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1021);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1022);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1023);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1024);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1025);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1026);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1027);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1028);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1029);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1030);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1031);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1032);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1033);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1034);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1035);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1036);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1037);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1038);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1039);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1040);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1041);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1042);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1043);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1044);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1045);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1046);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1047);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1048);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1049);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1050);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1051);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1052);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1053);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1054);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1055);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1056);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1057);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1058);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1059);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1060);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1061);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1062);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1063);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1064);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1065);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1066);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1067);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1068);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1069);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1070);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1071);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1072);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1073);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1074);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1075);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1076);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1077);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1078);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1079);

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "OptionId",
                keyValue: 1080);

            migrationBuilder.DeleteData(
                table: "TestQuestions",
                keyColumn: "QuestionId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "TestQuestions",
                keyColumn: "QuestionId",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "TestQuestions",
                keyColumn: "QuestionId",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "TestQuestions",
                keyColumn: "QuestionId",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "TestQuestions",
                keyColumn: "QuestionId",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "TestQuestions",
                keyColumn: "QuestionId",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "TestQuestions",
                keyColumn: "QuestionId",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "TestQuestions",
                keyColumn: "QuestionId",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "TestQuestions",
                keyColumn: "QuestionId",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "TestQuestions",
                keyColumn: "QuestionId",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "TestQuestions",
                keyColumn: "QuestionId",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "TestQuestions",
                keyColumn: "QuestionId",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "TestQuestions",
                keyColumn: "QuestionId",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "TestQuestions",
                keyColumn: "QuestionId",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "TestQuestions",
                keyColumn: "QuestionId",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "TestQuestions",
                keyColumn: "QuestionId",
                keyValue: 401);

            migrationBuilder.DeleteData(
                table: "TestQuestions",
                keyColumn: "QuestionId",
                keyValue: 402);

            migrationBuilder.DeleteData(
                table: "TestQuestions",
                keyColumn: "QuestionId",
                keyValue: 403);

            migrationBuilder.DeleteData(
                table: "TestQuestions",
                keyColumn: "QuestionId",
                keyValue: 404);

            migrationBuilder.DeleteData(
                table: "TestQuestions",
                keyColumn: "QuestionId",
                keyValue: 405);

            migrationBuilder.DeleteData(
                table: "PlacementTests",
                keyColumn: "TestId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PlacementTests",
                keyColumn: "TestId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PlacementTests",
                keyColumn: "TestId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PlacementTests",
                keyColumn: "TestId",
                keyValue: 4);
        }
    }
}
