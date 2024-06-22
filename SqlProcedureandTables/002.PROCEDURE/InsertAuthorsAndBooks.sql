CREATE OR ALTER  PROCEDURE InsertAuthorsAndBooks
    @AuthorBookData NVARCHAR(MAX)  -- JSON or XML string representing multiple authors and books
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @TempTable TABLE (
        LastName VARCHAR(100),
        FirstName VARCHAR(100),
        Publisher VARCHAR(100),
        Title VARCHAR(255),
        Price DECIMAL(10, 2)
    );

  
   DECLARE @AuthorID TABLE( AuthorID int);
    -- Parse JSON or XML input into a temporary table
    INSERT INTO @TempTable (LastName, FirstName, Publisher, Title, Price)
    SELECT
        JSON_VALUE(value, '$.authorLastName') AS LastName,
        JSON_VALUE(value, '$.authorFirstName') AS FirstName,
        JSON_VALUE(value, '$.publisher') AS Publisher,
        JSON_VALUE(value, '$.title') AS Title,
        CAST(JSON_VALUE(value, '$.price') AS DECIMAL(10, 2)) AS Price
    FROM OPENJSON(@AuthorBookData)
    CROSS APPLY OPENJSON(value) WITH (
        LastName VARCHAR(100),
        FirstName VARCHAR(100),
        Publisher VARCHAR(100),
        Title VARCHAR(255),
        Price DECIMAL(10, 2)
    );

    BEGIN TRY
        BEGIN TRANSACTION;
		 
        -- Insert into Authors table and retrieve AuthorID
        INSERT INTO Authors (LastName, FirstName)
        OUTPUT inserted.AuthorID INTO @AuthorID
        SELECT LastName, FirstName
        FROM @TempTable;

        -- Insert into Books table using retrieved AuthorID
        INSERT INTO Books (Publisher, Title, AuthorID, Price)
        SELECT t.Publisher, t.Title, a.AuthorID, t.Price
        FROM @TempTable t
        INNER JOIN Authors a ON t.LastName = a.LastName AND t.FirstName = a.FirstName;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

       
        THROW;
    END CATCH;
END;
