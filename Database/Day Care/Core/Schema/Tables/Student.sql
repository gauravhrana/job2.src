IF OBJECT_Id ('dbo.Student') IS NOT NULL
PRINT 'Dropping Table Student'
   DROP TABLE dbo.Student
GO
PRINT 'Creating Table Student'
CREATE TABLE dbo.Student
(
		StudentId INT         NOT NULL
    ,	LastName  VARCHAR(50) NOT NULL
    ,	FirstName VARCHAR(50) NOT NULL
)
GO

