namespace EntityFrameworkAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BuildDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        DateOfBirth = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookAuthors",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        fkBookId = c.Int(),
                        fkAuthorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.fkAuthorId)
                .ForeignKey("dbo.Books", t => t.fkBookId)
                .Index(t => t.fkBookId)
                .Index(t => t.fkAuthorId);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 60),
                        ISBN = c.Int(),
                        PublicationYear = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Borrows",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        BorrowDate = c.DateTime(),
                        ReturnDate = c.DateTime(),
                        fkBookId = c.Int(),
                        fkMemberId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.fkBookId)
                .ForeignKey("dbo.Members", t => t.fkMemberId)
                .Index(t => t.fkBookId)
                .Index(t => t.fkMemberId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(),
                        MemershipDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookAuthors", "fkBookId", "dbo.Books");
            DropForeignKey("dbo.Borrows", "fkMemberId", "dbo.Members");
            DropForeignKey("dbo.Borrows", "fkBookId", "dbo.Books");
            DropForeignKey("dbo.BookAuthors", "fkAuthorId", "dbo.Authors");
            DropIndex("dbo.Borrows", new[] { "fkMemberId" });
            DropIndex("dbo.Borrows", new[] { "fkBookId" });
            DropIndex("dbo.BookAuthors", new[] { "fkAuthorId" });
            DropIndex("dbo.BookAuthors", new[] { "fkBookId" });
            DropTable("dbo.Members");
            DropTable("dbo.Borrows");
            DropTable("dbo.Books");
            DropTable("dbo.BookAuthors");
            DropTable("dbo.Authors");
        }
    }
}
