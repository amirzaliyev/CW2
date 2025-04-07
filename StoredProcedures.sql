CREATE OR ALTER PROCEDURE udpGetAllCustomers
AS 
BEGIN 
    SELECT 
        CustomerId,
        FirstName,
        LastName,
        BirthDate,
        ProfilePic,
        Login,
        PasswordHash,
        PostalCode,
        Street,
        BuildingNo,
        FlatNo,
        City,
        PhoneNumber,
        AcceptsMarketing
    FROM Customers;
END
GO

GO

CREATE OR ALTER PROCEDURE udpGetCustomerById (
    @CustomerId BIGINT
)
AS 
BEGIN 
    SELECT 
        CustomerId,
        FirstName,
        LastName,
        BirthDate,
        ProfilePic,
        Login,
        PasswordHash,
        PostalCode,
        Street,
        BuildingNo,
        FlatNo,
        City,
        PhoneNumber,
        AcceptsMarketing
    FROM Customers
    WHERE CustomerId = @CustomerId;
END
GO


GO

CREATE OR ALTER PROCEDURE udpCreateCustomer (
    @FirstName NVARCHAR(100), 
    @LastName NVARCHAR(100),
    @BirthDate DATE,
    @ProfilePic VARBINARY(MAX),
    @Login NVARCHAR(100), 
    @PasswordHash NVARCHAR(512),
    @PostalCode NVARCHAR(6), 
    @Street NVARCHAR(100),
    @BuildingNo NVARCHAR(5), 
    @FlatNo NVARCHAR(5), 
    @City NVARCHAR(100),
    @PhoneNumber NVARCHAR(9),
    @AcceptsMarketing BIT,
    @CustomerId BIGINT OUTPUT,
    @Error NVARCHAR(2000) OUTPUT
)
AS 
BEGIN 
    SET NOCOUNT ON;

    BEGIN TRY
        INSERT INTO Customers (
            FirstName, LastName, BirthDate, ProfilePic, Login, 
            PasswordHash, PostalCode, Street, BuildingNo, FlatNo, City, 
            PhoneNumber, AcceptsMarketing
        )
        VALUES (
            @FirstName, @LastName, @BirthDate, @ProfilePic, @Login, 
            @PasswordHash, @PostalCode, @Street, @BuildingNo, @FlatNo, @City, 
            @PhoneNumber, @AcceptsMarketing
        );

        SET @CustomerId = SCOPE_IDENTITY();
        SET @Error = NULL;
        RETURN 0;
    END TRY
    BEGIN CATCH
        SET @CustomerId = NULL;
        SET @Error = ERROR_MESSAGE();
        RETURN ERROR_NUMBER();
    END CATCH;        
END
GO



GO

CREATE OR ALTER PROCEDURE udpUpdateCustomer (
    @CustomerId BIGINT,
    @FirstName NVARCHAR(100), 
    @LastName NVARCHAR(100),
    @BirthDate DATE,
    @ProfilePic VARBINARY(MAX),
    @Login NVARCHAR(100), 
    @PasswordHash NVARCHAR(512),
    @PostalCode NVARCHAR(6), 
    @Street NVARCHAR(100),
    @BuildingNo NVARCHAR(5), 
    @FlatNo NVARCHAR(5), 
    @City NVARCHAR(100),
    @PhoneNumber NVARCHAR(9),
    @AcceptsMarketing BIT,
    @Error NVARCHAR(2000) OUTPUT
)
AS 
BEGIN 
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE Customers SET
            FirstName = @FirstName, 
            LastName = @LastName,
            BirthDate = @BirthDate,
            ProfilePic = @ProfilePic,
            Login = @Login,
            PostalCode = @PostalCode, 
            Street = @Street,
            BuildingNo = @BuildingNo, 
            FlatNo = @FlatNo, 
            City = @City,
            PhoneNumber = @PhoneNumber,
            AcceptsMarketing = @AcceptsMarketing,
                             
            PasswordHash = CASE 
                              WHEN @PasswordHash IS NOT NULL AND LEN(@PasswordHash) > 0 
                              THEN @PasswordHash 
                              ELSE PasswordHash 
                           END

        WHERE CustomerId = @CustomerId;

        SET @Error = NULL;
        RETURN 0;
    END TRY
    BEGIN CATCH
        SET @Error = ERROR_MESSAGE();
        RETURN ERROR_NUMBER();
    END CATCH;        
END
GO


GO

CREATE OR ALTER PROCEDURE udpDeleteCustomer (
    @CustomerId BIGINT
)
AS
BEGIN 
    SET NOCOUNT ON;

    DELETE FROM Customers 
    OUTPUT deleted.CustomerId
    WHERE CustomerId = @CustomerId;
END

GO
CREATE OR ALTER PROCEDURE udpFilterCustomers 
(
    @FirstName NVARCHAR(100) = NULL, 
    @LastName NVARCHAR(100) = NULL,
    @PostalCode NVARCHAR(6) = NULL, 
    @Street NVARCHAR(100) = NULL,
    @BuildingNo NVARCHAR(5) = NULL, 
    @FlatNo NVARCHAR(5) = NULL, 
    @City NVARCHAR(100) = NULL,
    @PhoneNumber NVARCHAR(9) = NULL,
    @Page INT = 1,
    @PageSize INT = 20,
    @SortColumn NVARCHAR(30) = 'CustomerId',
    @SortDesc BIT = 0,
    @TotalCount INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    IF @SortColumn NOT IN (
        'CustomerId', 'FirstName', 'LastName', 'PostalCode', 
        'Street', 'BuildingNo', 'FlatNo', 'City', 'PhoneNumber'
    )
    BEGIN
        SET @SortColumn = 'CustomerId';
    END

    DECLARE @SQL NVARCHAR(MAX);
    DECLARE @SQLCount NVARCHAR(MAX);
    DECLARE @OrderBy NVARCHAR(100);

    SET @OrderBy = QUOTENAME(@SortColumn) + CASE WHEN @SortDesc = 1 THEN ' DESC' ELSE ' ASC' END;

    DECLARE @BaseFilter NVARCHAR(MAX) = '
    FROM Customers
    WHERE 1 = 1';

    IF @FirstName IS NOT NULL
        SET @BaseFilter += ' AND FirstName LIKE @FirstName + ''%''';
    IF @LastName IS NOT NULL
        SET @BaseFilter += ' AND LastName LIKE @LastName + ''%''';
    IF @PostalCode IS NOT NULL
        SET @BaseFilter += ' AND PostalCode LIKE @PostalCode + ''%''';
    IF @Street IS NOT NULL
        SET @BaseFilter += ' AND Street LIKE @Street + ''%''';
    IF @BuildingNo IS NOT NULL
        SET @BaseFilter += ' AND BuildingNo LIKE @BuildingNo + ''%''';
    IF @FlatNo IS NOT NULL
        SET @BaseFilter += ' AND FlatNo LIKE @FlatNo + ''%''';
    IF @City IS NOT NULL
        SET @BaseFilter += ' AND City LIKE @City + ''%''';
    IF @PhoneNumber IS NOT NULL
        SET @BaseFilter += ' AND PhoneNumber LIKE @PhoneNumber + ''%''';

    SET @SQLCount = 'SELECT @TotalCount = COUNT(*) ' + @BaseFilter;

    EXEC sp_executesql 
        @SQLCount,
        N'@FirstName NVARCHAR(100), @LastName NVARCHAR(100), @PostalCode NVARCHAR(6), 
          @Street NVARCHAR(100), @BuildingNo NVARCHAR(5), @FlatNo NVARCHAR(5), 
          @City NVARCHAR(100), @PhoneNumber NVARCHAR(9), @TotalCount INT OUTPUT',
        @FirstName, @LastName, @PostalCode, @Street, @BuildingNo, @FlatNo, @City, @PhoneNumber, @TotalCount OUTPUT;

    SET @SQL = '
    SELECT *
    ' + @BaseFilter + '
    ORDER BY ' + @OrderBy + '
    OFFSET (@Page - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;';

    EXEC sp_executesql 
        @SQL,
        N'@FirstName NVARCHAR(100), @LastName NVARCHAR(100), @PostalCode NVARCHAR(6), 
          @Street NVARCHAR(100), @BuildingNo NVARCHAR(5), @FlatNo NVARCHAR(5), 
          @City NVARCHAR(100), @PhoneNumber NVARCHAR(9), @Page INT, @PageSize INT',
        @FirstName, @LastName, @PostalCode, @Street, @BuildingNo, @FlatNo, @City, @PhoneNumber, @Page, @PageSize;
END

GO

CREATE OR ALTER PROCEDURE udpGetFilteredOrders
(
    @CustomerName NVARCHAR(100) = NULL,
    @OrderDateFrom DATE = NULL,
    @State NVARCHAR(20) = NULL,
    @ShipperName NVARCHAR(100) = NULL,
    @Page INT = 1,
    @PageSize INT = 10,
    @SortColumn NVARCHAR(50) = 'OrderId',
    @SortDesc BIT = 0,
    @TotalCount INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    IF @SortColumn NOT IN ('OrderId', 'Date', 'State', 'CustomerName', 'ShipperName')
    BEGIN
        SET @SortColumn = 'OrderId';
    END

    DECLARE @OrderBy NVARCHAR(100) = QUOTENAME(@SortColumn) + CASE WHEN @SortDesc = 1 THEN ' DESC' ELSE ' ASC' END;

    DECLARE @BaseSql NVARCHAR(MAX) = '
    FROM Orders o
    INNER JOIN Customers c ON o.CustomerId = c.CustomerId
    INNER JOIN Shippers s ON o.Shipper = s.ShipperId
    WHERE 1 = 1';

    IF @CustomerName IS NOT NULL
        SET @BaseSql += ' AND (c.FirstName + '' '' + c.LastName) LIKE @CustomerName + ''%''';
    IF @OrderDateFrom IS NOT NULL
        SET @BaseSql += ' AND o.Date >= @OrderDateFrom';
    IF @State IS NOT NULL
        SET @BaseSql += ' AND o.State = @State';

    DECLARE @SQLCount NVARCHAR(MAX) = 'SELECT @TotalCount = COUNT(*) ' + @BaseSql;

    EXEC sp_executesql 
        @SQLCount,
        N'@CustomerName NVARCHAR(100), @OrderDateFrom DATE, @State NVARCHAR(20), @TotalCount INT OUTPUT',
        @CustomerName, @OrderDateFrom, @State, @TotalCount OUTPUT;

    DECLARE @SQL NVARCHAR(MAX) = '
    SELECT
        o.OrderId,
        o.Date,
        o.State,
        c.FirstName + '' '' + c.LastName AS CustomerName,
        s.ShipperName
    ' + @BaseSql + '
    ORDER BY ' + @OrderBy + '
    OFFSET (@Page - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;';

    EXEC sp_executesql 
        @SQL,
        N'@CustomerName NVARCHAR(100), @OrderDateFrom DATE, @State NVARCHAR(20), @Page INT, @PageSize INT',
        @CustomerName, @OrderDateFrom, @State, @Page, @PageSize;
END

