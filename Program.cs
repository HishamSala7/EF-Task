using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppContext app = new AppContext();

            #region Insert Data

            //List<Book> books = new List<Book>()
            //{
            //    new Book{Id = 101, Title = "C Programming", ISBN = 456789 , PublicationYear = 1930},
            //    new Book{Id = 102, Title = "OOP", ISBN = 123456 , PublicationYear = 1970},
            //    new Book{Id = 103, Title = "Java", ISBN = 789456 , PublicationYear = 2000},
            //    new Book{Id = 104, Title = "Design Patterns", ISBN = 147258 , PublicationYear = 2010}
            //};

            //app.Books.AddRange(books);


            //List<Author> Authors = new List<Author>()
            //{
            //    new Author{Id = 50, Name = "Dr. Moataz", DateOfBirth = new DateTime(1940,6,21)},
            //    new Author{Id = 51, Name = "Dr. Ibrahem", DateOfBirth = new DateTime(1950,8,11)},
            //    new Author{Id = 52, Name = "Dr. Nour", DateOfBirth = new DateTime(1955,7,19)},
            //    new Author{Id = 53, Name = "Dr. Abdrelrahman", DateOfBirth = new DateTime(1966,11,9)}
            //};

            //app.Authors.AddRange(Authors);

            //List<Member> Members = new List<Member>()
            //{
            //    new Member{Id = 1 , Name = "Ahmed", Email="ahmed@gmail.com", MemershipDate = new DateTime(2015,10,3)},
            //    new Member{Id = 2 , Name = "Omar", Email="Omar@gmail.com", MemershipDate = new DateTime(2017,9,8)},
            //    new Member{Id = 3 , Name = "Ali", Email="Ali@gmail.com", MemershipDate = new DateTime(2020,3,20)},
            //    new Member{Id = 4 , Name = "khaled", Email="Khaled@gmail.com", MemershipDate = new DateTime(2019,8,13)}

            //};

            //app.Members.AddRange(Members);


            //List<BookAuthor> BookAuthors = new List<BookAuthor>()
            //{
            //    new BookAuthor{Id = 1 , fkBookId = 101, fkAuthorId = 50},
            //    new BookAuthor{Id = 2 , fkBookId = 104, fkAuthorId = 50},
            //    new BookAuthor{Id = 3 , fkBookId = 102, fkAuthorId = 52},
            //    new BookAuthor{Id = 4 , fkBookId = 103, fkAuthorId = 51},
            //    new BookAuthor{Id = 5 , fkBookId = 104, fkAuthorId = 51},
            //    new BookAuthor{Id = 6 , fkBookId = 101, fkAuthorId = 53},

            //};

            //app.BookAuthors.AddRange(BookAuthors);


            //List<Borrow> Borrows = new List<Borrow>()
            //{
            //    new Borrow{Id = 1 , BorrowDate = new DateTime(2020,5,3) , ReturnDate = new DateTime(2020,6,1), fkBookId = 102, fkMemberId = 1},
            //    new Borrow{Id = 2 , BorrowDate = new DateTime(2020,10,6) , ReturnDate = null, fkBookId = 103, fkMemberId = 2},
            //    new Borrow{Id = 3 , BorrowDate = new DateTime(2020,2,2) , ReturnDate = new DateTime(2020,3,9), fkBookId = 101, fkMemberId = 2},
            //    new Borrow{Id = 4 , BorrowDate = new DateTime(2021,4,10) , ReturnDate = null, fkBookId = 103, fkMemberId = 3},
            //    new Borrow{Id = 5 , BorrowDate = new DateTime(2021,6,13) , ReturnDate = new DateTime(2021,7,19), fkBookId = 102, fkMemberId = 1},
            //    
            //};

            //app.Borrows.AddRange(Borrows);

            //app.SaveChanges();

            #endregion


            ///1.Query: Get all books along with their authors
            var q1 = app.BookAuthors
                .Join(
                    app.Books,
                    bookAuth => bookAuth.fkBookId,
                    book => book.Id,
                    (bookAuth, book) => new
                    {
                        BookId = bookAuth.fkBookId,
                        AuthId = bookAuth.fkAuthorId,
                        BookTitle = book.Title,
                    }
                ).Join(
                    app.Authors,
                    book => book.AuthId,
                    auth => auth.Id,
                    (book, Auth) => new
                    {
                        book.BookTitle,
                        Auth.Name
                    }
                ).ToList();

            //foreach (var item in q1)
            //    Console.WriteLine($"Title = {item.BookTitle} , Author = {item.Name} ");

            //************************************************

            ///2.Query: Get all members and their borrowed books
            var q2 = app.Borrows
                .Join(
                    app.Books,
                    borrow => borrow.fkBookId,
                    book => book.Id,
                    (borrow, book) => new
                    {
                        MemberId = borrow.fkMemberId,
                        BookId = borrow.fkBookId,
                        Title = book.Title
                    }
                ).Join(
                    app.Members,
                    borrow => borrow.MemberId,
                    member => member.Id,
                    (borrow, member) => new
                    {
                        BookTitle = borrow.Title,
                        MemberName = member.Name,
                        Email = member.Email
                    }
                    ).ToList();


            //foreach (var item in q2)
            //    Console.WriteLine($"Title = {item.BookTitle} ,, Member Name = {item.MemberName} , " +
            //        $"Email = {item.Email}");

            //***************************************************


            ///3.Query: Get the total number of books borrowed by each member
            var q3 = (from Member in app.Members
                      join borr in app.Borrows
                      on Member.Id equals borr.fkMemberId
                      group Member by Member.Name into NewTable
                      select new { MemberName = NewTable.Key, Count = NewTable.Count() }).ToList();


            //foreach (var item in q3)
            //    Console.WriteLine(item.MemberName + " " + item.Count);



            ///***************************************************


            ///4.Query: Get the list of books published after a specific year
            var q4 = app.Books.Where(x => x.PublicationYear > 1960).Select(x => x.Title).ToList();

            //foreach( var x in q4 )
            //    Console.WriteLine(x);

            ///****************************************************


            ///5.Query: Get all authors and the books they have written
            var q5 = app.BookAuthors
                .Join(
                    app.Authors,
                    bookAuth => bookAuth.fkAuthorId,
                    auth => auth.Id,
                    (bookAuth, Auth) => new
                    {
                        BookId = bookAuth.fkBookId,
                        AuthName = Auth.Name,
                        DateOfBirth = Auth.DateOfBirth
                    }
                ).Join(
                    app.Books,
                    auth => auth.BookId,
                    book => book.Id,
                    (auth, book) => new
                    {
                        auth.AuthName,
                        auth.DateOfBirth,
                        book.Title,
                        book.ISBN
                    }
                ).ToList();

            //foreach (var item in q5)
            //    Console.WriteLine($"AuthName = {item.AuthName} , DateOfBirth = {item.DateOfBirth} ," +
            //        $"BookTitle = {item.Title} , ISBN = {item.ISBN} ");


            ///****************************************************


            ///6.Query: Get all books that have not been borrowed
            //app.Database.Log = log =>Debug.WriteLine(log);

            var q6 = (from b in app.Books
                      join borr in app.Borrows
                      on b.Id equals borr.fkBookId into NewTable
                      from NT in NewTable.DefaultIfEmpty()
                      where NT.fkBookId == null
                      select b.Title).ToList();


            //foreach( var b in q6 )
            //    Console.WriteLine(b);


            ///*****************************************************

            /// 7.Query: Get members who have borrowed books but haven't returned them
            var q7 = (from b in app.Books
                      join borr in app.Borrows
                      on b.Id equals borr.fkBookId
                      join m in app.Members
                      on borr.fkMemberId equals m.Id
                      where borr.ReturnDate == null
                      select new { MemberName = m.Name, BookName = b.Title }).ToList();

            
            //foreach ( var item in q7)
            //    Console.WriteLine($"MemberName = {item.MemberName} ,, BookName = {item.BookName}");


            

            Console.ReadKey();



        }
    }
}
