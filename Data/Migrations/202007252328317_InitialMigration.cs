namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Park",
                c => new
                    {
                        ParkId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        ParkSize = c.Int(nullable: false),
                        YearEstablished = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ParkId);
            
            CreateTable(
                "dbo.Trail",
                c => new
                    {
                        TrailId = c.Int(nullable: false, identity: true),
                        ParkId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Length = c.String(nullable: false),
                        TrailDifficulty = c.Int(nullable: false),
                        IsTrailALoop = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        AverageRating = c.Double(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TrailId)
                .ForeignKey("dbo.Park", t => t.ParkId, cascadeDelete: true)
                .Index(t => t.ParkId);
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        Score = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Title = c.String(nullable: false),
                        Comment = c.String(nullable: false),
                        VisitDate = c.DateTime(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(precision: 7),
                        IsDeleted = c.Boolean(nullable: false),
                        TrailId = c.Int(),
                        ParkId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Trail", t => t.TrailId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUser", t => t.UserId)
                .ForeignKey("dbo.Park", t => t.ParkId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.TrailId)
                .Index(t => t.ParkId);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Review", "ParkId", "dbo.Park");
            DropForeignKey("dbo.Review", "UserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.Review", "TrailId", "dbo.Trail");
            DropForeignKey("dbo.Trail", "ParkId", "dbo.Park");
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Review", new[] { "ParkId" });
            DropIndex("dbo.Review", new[] { "TrailId" });
            DropIndex("dbo.Review", new[] { "UserId" });
            DropIndex("dbo.Trail", new[] { "ParkId" });
            DropTable("dbo.IdentityRole");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.Review");
            DropTable("dbo.Trail");
            DropTable("dbo.Park");
        }
    }
}
