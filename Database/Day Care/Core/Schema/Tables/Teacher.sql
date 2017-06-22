IF OBJECT_Id ('dbo.Teacher') IS NOT NULL
PRINT 'Dropping Table Teacher'
   DROP TABLE dbo.Teacher
GO
PRINT 'Creating Table Teacher'
CREATE TABLE dbo.Teacher 
(
		TeacherId   INT NOT NULL
    ,	LastName	INT NOT NULL
    ,	FirstName	INT NOT NULL
)
GO

