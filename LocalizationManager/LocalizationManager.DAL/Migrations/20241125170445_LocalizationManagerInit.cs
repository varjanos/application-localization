using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LocalizationManager.DAL.Migrations
{
    /// <inheritdoc />
    public partial class LocalizationManagerInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalizationValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Key = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LanguageCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizationValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegisteredApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AppName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SupportedLanguages = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "LanguageCode", "LanguageName" },
                values: new object[,]
                {
                    { 1, "ab", "Abkhazian" },
                    { 2, "aa", "Afar" },
                    { 3, "af", "Afrikaans" },
                    { 4, "ak", "Akan" },
                    { 5, "sq", "Albanian" },
                    { 6, "am", "Amharic" },
                    { 7, "ar", "Arabic" },
                    { 8, "an", "Aragonese" },
                    { 9, "hy", "Armenian" },
                    { 10, "as", "Assamese" },
                    { 11, "av", "Avaric" },
                    { 12, "ae", "Avestan" },
                    { 13, "ay", "Aymara" },
                    { 14, "az", "Azerbaijani" },
                    { 15, "bm", "Bambara" },
                    { 16, "ba", "Bashkir" },
                    { 17, "eu", "Basque" },
                    { 18, "be", "Belarusian" },
                    { 19, "bn", "Bengali" },
                    { 20, "bi", "Bislama" },
                    { 21, "bs", "Bosnian" },
                    { 22, "br", "Breton" },
                    { 23, "bg", "Bulgarian" },
                    { 24, "my", "Burmese" },
                    { 25, "ca", "Catalan, Valencian" },
                    { 26, "ch", "Chamorro" },
                    { 27, "ce", "Chechen" },
                    { 28, "ny", "Chichewa, Chewa, Nyanja" },
                    { 29, "zh", "Chinese" },
                    { 30, "cu", "Church Slavonic, Old Slavonic, Old Church Slavonic" },
                    { 31, "cv", "Chuvash" },
                    { 32, "kw", "Cornish" },
                    { 33, "co", "Corsican" },
                    { 34, "cr", "Cree" },
                    { 35, "hr", "Croatian" },
                    { 36, "cs", "Czech" },
                    { 37, "da", "Danish" },
                    { 38, "dv", "Divehi, Dhivehi, Maldivian" },
                    { 39, "nl", "Dutch, Flemish" },
                    { 40, "dz", "Dzongkha" },
                    { 41, "en", "English" },
                    { 42, "eo", "Esperanto" },
                    { 43, "et", "Estonian" },
                    { 44, "ee", "Ewe" },
                    { 45, "fo", "Faroese" },
                    { 46, "fj", "Fijian" },
                    { 47, "fi", "Finnish" },
                    { 48, "fr", "French" },
                    { 49, "fy", "Western Frisian" },
                    { 50, "ff", "Fulah" },
                    { 51, "gd", "Gaelic, Scottish Gaelic" },
                    { 52, "gl", "Galician" },
                    { 53, "lg", "Ganda" },
                    { 54, "ka", "Georgian" },
                    { 55, "de", "German" },
                    { 56, "el", "Greek, Modern (1453–)" },
                    { 57, "kl", "Kalaallisut, Greenlandic" },
                    { 58, "gn", "Guarani" },
                    { 59, "gu", "Gujarati" },
                    { 60, "ht", "Haitian, Haitian Creole" },
                    { 61, "ha", "Hausa" },
                    { 62, "he", "Hebrew" },
                    { 63, "hz", "Herero" },
                    { 64, "hi", "Hindi" },
                    { 65, "ho", "Hiri Motu" },
                    { 66, "hu", "Hungarian" },
                    { 67, "is", "Icelandic" },
                    { 68, "io", "Ido" },
                    { 69, "ig", "Igbo" },
                    { 70, "id", "Indonesian" },
                    { 71, "ia", "Interlingua (International Auxiliary Language Association)" },
                    { 72, "ie", "Interlingue, Occidental" },
                    { 73, "iu", "Inuktitut" },
                    { 74, "ik", "Inupiaq" },
                    { 75, "ga", "Irish" },
                    { 76, "it", "Italian" },
                    { 77, "ja", "Japanese" },
                    { 78, "jv", "Javanese" },
                    { 79, "kn", "Kannada" },
                    { 80, "kr", "Kanuri" },
                    { 81, "ks", "Kashmiri" },
                    { 82, "kk", "Kazakh" },
                    { 83, "km", "Central Khmer" },
                    { 84, "ki", "Kikuyu, Gikuyu" },
                    { 85, "rw", "Kinyarwanda" },
                    { 86, "ky", "Kirghiz, Kyrgyz" },
                    { 87, "kv", "Komi" },
                    { 88, "kg", "Kongo" },
                    { 89, "ko", "Korean" },
                    { 90, "kj", "Kuanyama, Kwanyama" },
                    { 91, "ku", "Kurdish" },
                    { 92, "lo", "Lao" },
                    { 93, "la", "Latin" },
                    { 94, "lv", "Latvian" },
                    { 95, "li", "Limburgan, Limburger, Limburgish" },
                    { 96, "ln", "Lingala" },
                    { 97, "lt", "Lithuanian" },
                    { 98, "lu", "Luba-Katanga" },
                    { 99, "lb", "Luxembourgish, Letzeburgesch" },
                    { 100, "mk", "Macedonian" },
                    { 101, "mg", "Malagasy" },
                    { 102, "ms", "Malay" },
                    { 103, "ml", "Malayalam" },
                    { 104, "mt", "Maltese" },
                    { 105, "gv", "Manx" },
                    { 106, "mi", "Maori" },
                    { 107, "mr", "Marathi" },
                    { 108, "mh", "Marshallese" },
                    { 109, "mn", "Mongolian" },
                    { 110, "na", "Nauru" },
                    { 111, "nv", "Navajo, Navaho" },
                    { 112, "nd", "North Ndebele" },
                    { 113, "nr", "South Ndebele" },
                    { 114, "ng", "Ndonga" },
                    { 115, "ne", "Nepali" },
                    { 116, "no", "Norwegian" },
                    { 117, "nb", "Norwegian Bokmål" },
                    { 118, "nn", "Norwegian Nynorsk" },
                    { 119, "oc", "Occitan" },
                    { 120, "oj", "Ojibwa" },
                    { 121, "or", "Oriya" },
                    { 122, "om", "Oromo" },
                    { 123, "os", "Ossetian, Ossetic" },
                    { 124, "pi", "Pali" },
                    { 125, "ps", "Pashto, Pushto" },
                    { 126, "fa", "Persian" },
                    { 127, "pl", "Polish" },
                    { 128, "pt", "Portuguese" },
                    { 129, "pa", "Punjabi, Panjabi" },
                    { 130, "qu", "Quechua" },
                    { 131, "ro", "Romanian, Moldavian, Moldovan" },
                    { 132, "rm", "Romansh" },
                    { 133, "rn", "Rundi" },
                    { 134, "ru", "Russian" },
                    { 135, "se", "Northern Sami" },
                    { 136, "sm", "Samoan" },
                    { 137, "sg", "Sango" },
                    { 138, "sa", "Sanskrit" },
                    { 139, "sc", "Sardinian" },
                    { 140, "sr", "Serbian" },
                    { 141, "sn", "Shona" },
                    { 142, "sd", "Sindhi" },
                    { 143, "si", "Sinhala, Sinhalese" },
                    { 144, "sk", "Slovak" },
                    { 145, "sl", "Slovenian" },
                    { 146, "so", "Somali" },
                    { 147, "st", "Southern Sotho" },
                    { 148, "es", "Spanish, Castilian" },
                    { 149, "su", "Sundanese" },
                    { 150, "sw", "Swahili" },
                    { 151, "ss", "Swati" },
                    { 152, "sv", "Swedish" },
                    { 153, "tl", "Tagalog" },
                    { 154, "ty", "Tahitian" },
                    { 155, "tg", "Tajik" },
                    { 156, "ta", "Tamil" },
                    { 157, "tt", "Tatar" },
                    { 158, "te", "Telugu" },
                    { 159, "th", "Thai" },
                    { 160, "bo", "Tibetan" },
                    { 161, "ti", "Tigrinya" },
                    { 162, "to", "Tonga (Tonga Islands)" },
                    { 163, "ts", "Tsonga" },
                    { 164, "tn", "Tswana" },
                    { 165, "tr", "Turkish" },
                    { 166, "tk", "Turkmen" },
                    { 167, "tw", "Twi" },
                    { 168, "ug", "Uighur, Uyghur" },
                    { 169, "uk", "Ukrainian" },
                    { 170, "ur", "Urdu" },
                    { 171, "uz", "Uzbek" },
                    { 172, "ve", "Venda" },
                    { 173, "vi", "Vietnamese" },
                    { 174, "vo", "Volapük" },
                    { 175, "wa", "Walloon" },
                    { 176, "cy", "Welsh" },
                    { 177, "wo", "Wolof" },
                    { 178, "xh", "Xhosa" },
                    { 179, "ii", "Sichuan Yi, Nuosu" },
                    { 180, "yi", "Yiddish" },
                    { 181, "yo", "Yoruba" },
                    { 182, "za", "Zhuang, Chuang" },
                    { 183, "zu", "Zulu" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizationValues_AppId_LanguageCode_Key",
                table: "LocalizationValues",
                columns: new[] { "AppId", "LanguageCode", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredApplications_AppId",
                table: "RegisteredApplications",
                column: "AppId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "LocalizationValues");

            migrationBuilder.DropTable(
                name: "RegisteredApplications");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
