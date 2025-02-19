using BookReview.Core.Entity;

namespace BookReview.UnitTests.Core
{
    public class BookTests
    {
        [Fact]
        public void Constructor_Should_Initialize_Properties_Correctly()
        {
            // Arrange
            var title = "Test Book";
            var description = "Test Book Description";
            var isbn = "1234567890";
            var authorId = 1;
            var publisher = "Test Publisher";
            var genreId = 2;
            var publicationYear = 2023;
            var quantityPages = 250;
            var bookCover = "cover.jpg";

            // Act
            var book = new Book(title, description, isbn, authorId, publisher, genreId, publicationYear, quantityPages, bookCover);

            // Assert
            Assert.Equal(title, book.Title);
            Assert.Equal(description, book.Description);
            Assert.Equal(isbn, book.ISBN);
            Assert.Equal(authorId, book.AuthorId);
            Assert.Equal(publisher, book.Publisher);
            Assert.Equal(genreId, book.GenreId);
            Assert.Equal(publicationYear, book.PublicationYear);
            Assert.Equal(quantityPages, book.QuantityPages);
            Assert.Equal(bookCover, book.BookCover);
            Assert.Null(book.AverageGrade);
            Assert.NotNull(book.Reviews);
            Assert.Empty(book.Reviews);
        }

        [Fact]
        public void Update_Should_Update_Properties_Correctly()
        {
            // Arrange
            var book = new Book("Old Title", "Old Description", "000", 1, "Old Publisher", 1, 2020, 100, "old_cover.jpg");
            var newTitle = "New Title";
            var newDescription = "New Description";
            var newIsbn = "1111111111";
            var newAuthorId = 2;
            var newPublisher = "New Publisher";
            var newGenreId = 3;
            var newPublicationYear = 2024;
            var newQuantityPages = 300;
            var newBookCover = "new_cover.jpg";

            // Act
            book.Update(newTitle, newDescription, newIsbn, newAuthorId, newPublisher, newGenreId, newPublicationYear, newQuantityPages, newBookCover);

            // Assert
            Assert.Equal(newTitle, book.Title);
            Assert.Equal(newDescription, book.Description);
            Assert.Equal(newIsbn, book.ISBN);
            Assert.Equal(newAuthorId, book.AuthorId);
            Assert.Equal(newPublisher, book.Publisher);
            Assert.Equal(newGenreId, book.GenreId);
            Assert.Equal(newPublicationYear, book.PublicationYear);
            Assert.Equal(newQuantityPages, book.QuantityPages);
            Assert.Equal(newBookCover, book.BookCover);
        }

        [Fact]
        public void UpdateAverageGrade_Should_Assign_Rating_When_AverageGrade_Is_Null()
        {
            // Arrange
            var book = new Book("Title", "Description", "ISBN", 1, "Publisher", 1, 2023, 150, "cover.jpg");
            decimal rating = 8.0m;
            int reviewCount = 1; // First review

            // Act
            book.UpdateAverageGrade(reviewCount, rating);

            // Assert
            Assert.Equal(rating, book.AverageGrade);
        }

        [Fact]
        public void UpdateAverageGrade_Should_Recalculate_Correctly_When_AverageGrade_Already_Set()
        {
            // Arrange
            var book = new Book("Title", "Description", "ISBN", 1, "Publisher", 1, 2023, 150, "cover.jpg");

            // First rating
            decimal rating1 = 8.0m;
            book.UpdateAverageGrade(1, rating1); 

            // Second rating
            decimal rating2 = 6.0m;
            int reviewCount = 2;

            // Act
            book.UpdateAverageGrade(reviewCount, rating2);

            // Assert
            // Expected calculation: ((8.0 * (2 - 1)) + 6.0) / 2 = (8.0 + 6.0) / 2 = 7.0
            decimal expectedAverage = 7.0m;
            Assert.Equal(expectedAverage, book.AverageGrade);
        }

        [Fact]
        public void UpdateAverageGrade_Multiple_Calls_Should_Maintain_Correct_Average()
        {
            // Arrange
            var book = new Book("Title", "Description", "ISBN", 1, "Publisher", 1, 2023, 150, "cover.jpg");
            decimal rating1 = 8.0m;
            decimal rating2 = 6.0m;
            decimal rating3 = 7.0m;

            // Act & Assert
            // First call: AverageGrade becomes 8.0
            book.UpdateAverageGrade(1, rating1);
            Assert.Equal(rating1, book.AverageGrade);

            // Second call: ((8.0 * 1) + 6.0) / 2 = 7.0
            book.UpdateAverageGrade(2, rating2);
            Assert.Equal(7.0m, book.AverageGrade);

            // Third call: ((7.0 * 2) + 7.0) / 3 = 7.0
            book.UpdateAverageGrade(3, rating3);
            Assert.Equal(7.0m, book.AverageGrade);
        }

        [Fact]
        public void RecalculateAverage_Should_Recalculate_Average_Correctly()
        {
            // Arrange
            var book = new Book("Title", "Description", "ISBN", 1, "Publisher", 1, 2023, 150, "cover.jpg");

            // Set an initial rating
            book.UpdateAverageGrade(1, 4.0m); // AverageGrade becomes 4.0
            int reviewCount = 2;

            // Act
            book.RecalculateAverage(reviewCount);

            // Assert
            // Expected calculation: (4.0 * (2 - 1)) / 2 = 4.0 / 2 = 2.0
            Assert.Equal(2.0m, book.AverageGrade);
        }

        [Fact]
        public void RecalculateAverage_With_Zero_Reviews_Should_Throw_Exception()
        {
            // Arrange
            var book = new Book("Title", "Description", "ISBN", 1, "Publisher", 1, 2023, 150, "cover.jpg");
            book.UpdateAverageGrade(1, 4.0m);
            int reviewCount = 0;

            // Act & Assert
            Assert.Throws<DivideByZeroException>(() => book.RecalculateAverage(reviewCount));
        }
    }
}
