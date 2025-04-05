
CREATE OR ALTER PROCEDURE udpGetAllCustomers
AS 
BEGIN 
	SELECT [CustomerId]
      ,[FirstName]
      ,[LastName]
      ,[ProfilePic]
      ,[Login]
      ,[PasswordHash]
      ,[PostalCode]
      ,[Street]
      ,[BuildingNo]
      ,[FlatNo]
      ,[City]
      ,[Tin]
      ,[PhoneNumber]
	FROM [BookStore].[dbo].[Customers]
END

GO

CREATE OR ALTER PROCEDURE udpGetCustomerById (@CustomerId BIGINT)
AS 
BEGIN 
	SELECT [CustomerId]
      ,[FirstName]
      ,[LastName]
      ,[ProfilePic]
      ,[Login]
      ,[PasswordHash]
      ,[PostalCode]
      ,[Street]
      ,[BuildingNo]
      ,[FlatNo]
      ,[City]
      ,[Tin]
      ,[PhoneNumber]
	FROM [BookStore].[dbo].[Customers] 
    WHERE CustomerId = @CustomerId
END

GO

CREATE OR ALTER PROCEDURE udpCreateCustomer (
    @FirstName NVARCHAR(100), 
    @LastName NVARCHAR(100),
    @ProfilePic VARBINARY(MAX),
    @Login NVARCHAR(100), 
    @PasswordHash NVARCHAR(100),
    @PostalCode NVARCHAR(6), 
    @Street NVARCHAR,
    @BuildingNo NVARCHAR(5), 
    @FlatNo NVARCHAR(5), 
    @City NVARCHAR(100),
    @Tin NVARCHAR(9), 
    @PhoneNumber NVARCHAR(9),
    @Error NVARCHAR(2000) OUT
)
AS 
BEGIN 
    BEGIN TRY
	    INSERT INTO 
            Customers (
                [FirstName], [LastName], [ProfilePic], [Login], 
                [PasswordHash], [PostalCode], [Street],
                [BuildingNo], [FlatNo], [City],
                [Tin], [PhoneNumber]
            )
        OUTPUT inserted.CustomerId
        VALUES (@FirstName, @LastName, @ProfilePic, @Login, 
                @PasswordHash, @PostalCode, @Street,
                @BuildingNo, @FlatNo, @City,
                @Tin, @PhoneNumber
        )
        RETURN (0);

    END TRY

    BEGIN CATCH
        SET @Error = ERROR_MESSAGE();
        RETURN (1);
    END CATCH;        
END

GO

CREATE OR ALTER PROCEDURE udpUpdateCustomer (
    @CustomerId BIGINT,
    @FirstName NVARCHAR(100), 
    @LastName NVARCHAR(100),
    @ProfilePic VARBINARY(MAX),
    @Login NVARCHAR(100), 
    @PasswordHash NVARCHAR(100),
    @PostalCode NVARCHAR(6), 
    @Street NVARCHAR,
    @BuildingNo NVARCHAR(5), 
    @FlatNo NVARCHAR(5), 
    @City NVARCHAR(100),
    @Tin NVARCHAR(9), 
    @PhoneNumber NVARCHAR(9),
    @Error NVARCHAR(2000) OUT
)
AS 
BEGIN 
    BEGIN TRY
	    UPDATE Customers SET
            FirstName = @FirstName, 
            LastName = @LastName, 
            ProfilePic = @ProfilePic, 
            Login = @Login,                 
            PasswordHash = @PasswordHash, 
            PostalCode = @PostalCode, 
            Street = @Street,
            BuildingNo = @BuildingNo, 
            FlatNo = @FlatNo, 
            City = @City,
            Tin = @Tin, 
            PhoneNumber = @PhoneNumber
        WHERE CustomerId = @CustomerId
        RETURN (0);

    END TRY

    BEGIN CATCH
        SET @Error = ERROR_MESSAGE();
        RETURN (1);
    END CATCH;        
END

GO

CREATE OR ALTER PROCEDURE udpDeleteCustomer (@CustomerId BIGINT)
AS
BEGIN 

    DELETE FROM Customers 
    OUTPUT deleted.CustomerId
    WHERE CustomerId = @CustomerId;


END;

GO
CREATE OR ALTER PROCEDURE udpUpdateCustomerPic (
    @CustomerId BIGINT,
    @ProfilePic VARBINARY(MAX),
    @Error NVARCHAR(2000) OUT
)
AS 
BEGIN 
    BEGIN TRY
	    UPDATE Customers SET
            ProfilePic = @ProfilePic
        WHERE CustomerId = @CustomerId
        RETURN (0);

    END TRY

    BEGIN CATCH
        SET @Error = ERROR_MESSAGE();
        RETURN (1);
    END CATCH;        
END

GO