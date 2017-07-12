namespace mvc_migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            //Copy these script from table once we have tweek into AccountController's Register Method.

            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6ff330f7-dc13-430a-9282-c7b06021e7b6', N'guest@gmail.com', 0, N'AGjMHmUk/4eAHflIDDxm6czBnLQe8e74R7m81qdc4sOKYK64k21jPxqyrY9eKpssow==', N'84d477c4-69ce-4247-a2fe-c9d19417eb3b', NULL, 0, 0, NULL, 1, 0, N'guest@gmail.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'cd4d8e3a-0a6d-48cc-b5ec-d08b283b7914', N'admin@gmail.com', 0, N'AIIp6WeFN2T0FbJ1xP87vbJqTqL/nIU0DtHOFz9PphzS6TRu6beGY5QynIl05KCv/g==', N'90e477fc-0b21-4d4a-9976-15b43246c068', NULL, 0, 0, NULL, 1, 0, N'admin@gmail.com')

                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'190692b4-a715-4bc8-9392-94a8710624ce', N'CanManageMovies')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'cd4d8e3a-0a6d-48cc-b5ec-d08b283b7914', N'190692b4-a715-4bc8-9392-94a8710624ce')
            ");
        }

        public override void Down()
        {
        }
    }
}
