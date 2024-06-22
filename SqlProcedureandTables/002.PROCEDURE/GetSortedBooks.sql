CREATE OR ALTER  PROCEDURE GetSortedBooks 
AS

BEGIN

    SELECT 
        b.Publisher,
        CONCAT(a.FirstName, ' ', a.LastName) AS Author,
        b.Title
    FROM 
        Books b
        INNER JOIN Authors a ON b.AuthorID = a.AuthorID
    WHERE 
        b.IsDeleted = 0 AND a.IsDeleted = 0
    ORDER BY 
        b.Publisher, a.LastName, a.FirstName, b.Title;


END;

