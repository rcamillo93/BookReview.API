using Bogus;
using Bogus.DataSets;
using BookReview.Application.Commads.BookCommands.Create;
using BookReview.Application.Commads.ReviewCommans.Create;
using BookReview.Core.Entity;
using FluentAssertions.Equivalency;

namespace BookReview.UnitTests.Fakes
{
    public static class FakeDataHelper
    {
        private static readonly Faker _faker = new();

        public static Book CreateFakeBookV1()
        {
            return new Book(
                _faker.Name.Random.String(25),
                _faker.Name.Random.String(25),
                _faker.Name.Random.String(5),
                _faker.Random.Int(1, 10),
                _faker.Commerce.ProductName(),
                _faker.Random.Int(1, 10),
                _faker.Random.Int(1990, 2020),
                _faker.Random.Int(100, 450),
                _faker.Random.String(20)
                );
        }

        public static Review CreateFakeReviewV1()
        {
            return new Review(
               _faker.Name.Random.String(25),
               _faker.Random.Int(1, 10),
               _faker.Random.Int(1, 10),
               _faker.Random.Int(1, 5),
               _faker.Date.Past(1)
                );
        }

        public static User CreateFakeUserV1()
        {
            return new User(
                _faker.Person.FullName,
                _faker.Person.Email,
                _faker.Person.Random.AlphaNumeric(6)
                );
        }

        public static Author CreateFakeAuthorV1() {

            return new Author(
                _faker.Person.FullName,
                _faker.Date.Past(40)
                );
        }      

        private static readonly Faker<Book> _bookFaker = new Faker<Book>()
                .CustomInstantiator(b => new Book(                    
                    b.Lorem.Sentence(15),
                    b.Lorem.Sentence(25),
                    b.Lorem.Sentence(5),
                    b.Random.Int(1, 10),
                    b.Commerce.ProductName(),
                    b.Random.Int(1, 10),
                    b.Random.Int(1990, 2020),
                    b.Random.Int(100, 450),
                    b.Internet.Url()
                    ));

        private static readonly Faker<CreateReviewCommand> _createReviewCommandFaker = new Faker<CreateReviewCommand>()
           .CustomInstantiator(f => new CreateReviewCommand(             
               f.Lorem.Sentence(10),
               f.Random.Int(1, 10),
               f.Random.Int(1, 5),
               f.Random.Int(1, 5),        
               f.Date.Past()             
           ));

        private static readonly Faker<CreateBookCommand> _createBookCommandFaker = new Faker<CreateBookCommand>()
            .CustomInstantiator(f => new CreateBookCommand(
                f.Lorem.Sentence(15),      // Title
                f.Lorem.Sentence(25),      // Description
                f.Random.AlphaNumeric(13), // ISBN
                f.Random.Int(1, 10),       // AuthorId
                f.Commerce.ProductName(),  // Publisher
                f.Random.Int(1, 10),       // GenreId
                f.Random.Int(1990, 2020),  // PublicationYear
                f.Random.Int(100, 450),    // QuantityPages
                f.Internet.Url()           // BookCover
            ));

        public static Book CreateFakeBook() => _bookFaker.Generate();

        public static CreateReviewCommand CreateFakeReviewCommand() => _createReviewCommandFaker.Generate();

        public static CreateBookCommand CreateFakeBookCommand() => _createBookCommandFaker.Generate();
    }
}
