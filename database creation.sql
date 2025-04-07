-- Dropping tables if they exist (correct order to avoid FK issues)
DROP TABLE IF EXISTS Reviews;
DROP TABLE IF EXISTS OrdersDetails;
DROP TABLE IF EXISTS BooksDiscounts;
DROP TABLE IF EXISTS CustomersDiscounts;
DROP TABLE IF EXISTS Orders;
DROP TABLE IF EXISTS Shippers;
DROP TABLE IF EXISTS Discounts;
DROP TABLE IF EXISTS Customers;
DROP TABLE IF EXISTS BooksGenres;
DROP TABLE IF EXISTS Books;
DROP TABLE IF EXISTS Publishers;
DROP TABLE IF EXISTS Genres;
DROP TABLE IF EXISTS Authors;

-- Authors
CREATE TABLE Authors (
    AuthorId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    CompanyName NVARCHAR(100),
    CHECK ((FirstName IS NOT NULL AND LastName IS NOT NULL) OR CompanyName IS NOT NULL)
);

-- Genres
CREATE TABLE Genres (
    GenreId INT IDENTITY(1,1) PRIMARY KEY,
    GenreName NVARCHAR(100) UNIQUE NOT NULL
);

-- Publishers
CREATE TABLE Publishers (
    PublisherId INT IDENTITY(1,1) PRIMARY KEY,
    PublisherName NVARCHAR(100) UNIQUE NOT NULL
);

-- Books
CREATE TABLE Books (
    Isbn NVARCHAR(20) PRIMARY KEY, -- Provide length explicitly
    Title NVARCHAR(100) NOT NULL,
    PublicationDate DATE CHECK (PublicationDate <= GETDATE()),
    Edition INT,
    AvailableQuantity INT NOT NULL DEFAULT 0 CHECK (AvailableQuantity >= 0),
    Price DECIMAL(6, 2) CHECK (Price > 0),
    Author INT REFERENCES Authors(AuthorId) ON DELETE CASCADE,
    Publisher INT REFERENCES Publishers(PublisherId) ON DELETE CASCADE
);

-- BooksGenres (Many-to-many Books-Genres)
CREATE TABLE BooksGenres (
    BookId NVARCHAR(20) REFERENCES Books (Isbn) ON DELETE CASCADE,
    GenreId INT REFERENCES Genres (GenreId) ON DELETE CASCADE,
    PRIMARY KEY (BookId, GenreId)
);

-- Customers
CREATE TABLE Customers (
    CustomerId BIGINT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    BirthDate DATE NULL,
    ProfilePic VARBINARY(MAX) NULL CHECK (DATALENGTH(ProfilePic) <= 10485760),
    Login NVARCHAR(100) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(512) NOT NULL,
    PostalCode NVARCHAR(6),
    Street NVARCHAR(100),
    BuildingNo NVARCHAR(5),
    FlatNo NVARCHAR(5),
    City NVARCHAR(100),
    PhoneNumber NVARCHAR(9) NOT NULL,
    AcceptsMarketing BIT NOT NULL DEFAULT 0
);

-- Shippers
CREATE TABLE Shippers (
    ShipperId BIGINT IDENTITY(1,1) PRIMARY KEY,
    ShipperName NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(9) NOT NULL
);

-- Discounts
CREATE TABLE Discounts (
    DiscountId BIGINT IDENTITY(1,1) PRIMARY KEY,
    DiscountName NVARCHAR(100),
    Value DECIMAL(3, 2) NOT NULL DEFAULT 0 CHECK (Value >= 0.00 AND Value <= 1.00)
);

-- CustomersDiscounts (Many-to-many Customers-Discounts)
CREATE TABLE CustomersDiscounts (
    CustomerId BIGINT REFERENCES Customers(CustomerId) ON DELETE CASCADE,
    DiscountId BIGINT REFERENCES Discounts(DiscountId) ON DELETE CASCADE,
    PRIMARY KEY (CustomerId, DiscountId)
);

-- BooksDiscounts (Many-to-many Books-Discounts)
CREATE TABLE BooksDiscounts (
    BookId NVARCHAR(20) REFERENCES Books (Isbn) ON DELETE CASCADE,
    DiscountId BIGINT REFERENCES Discounts (DiscountId) ON DELETE CASCADE,
    PRIMARY KEY (BookId, DiscountId)
);

-- Orders
CREATE TABLE Orders (
    OrderId BIGINT IDENTITY(1,1) PRIMARY KEY,
    CustomerId BIGINT NOT NULL REFERENCES Customers (CustomerId) ON DELETE CASCADE,
    [Date] DATE NOT NULL DEFAULT GETDATE() CHECK ([Date] <= GETDATE()),
    DiscountId BIGINT REFERENCES Discounts (DiscountId) ON DELETE CASCADE,
    Shipper BIGINT NOT NULL REFERENCES Shippers (ShipperId) ON DELETE CASCADE,
    State NVARCHAR(20) NOT NULL DEFAULT 'PENDING' 
        CHECK (State IN ('PENDING', 'PAID', 'SENT'))
);

-- OrdersDetails (Many-to-many Orders-Books)
CREATE TABLE OrdersDetails (
    OrderId BIGINT REFERENCES Orders (OrderId) ON DELETE CASCADE,
    BookId NVARCHAR(20) REFERENCES Books (Isbn) ON DELETE CASCADE,
    Amount INT NOT NULL CHECK (Amount > 0),
    PRIMARY KEY (OrderId, BookId)
);

-- Reviews
CREATE TABLE Reviews (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BookId NVARCHAR(20) NOT NULL REFERENCES Books (Isbn) ON DELETE CASCADE,
    CustomerId BIGINT NOT NULL REFERENCES Customers (CustomerId) ON DELETE CASCADE,
    Review INT NOT NULL CHECK (Review BETWEEN 0 AND 10),
    [Date] DATE NOT NULL DEFAULT GETDATE()
);



