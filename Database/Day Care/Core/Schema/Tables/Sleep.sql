IF OBJECT_Id ('dbo.Sleep') IS NOT NULL
PRINT 'Dropping Table Sleep'
   DROP TABLE dbo.Sleep
GO
PRINT 'Creating Table Sleep'
CREATE TABLE dbo.Sleep 
(
		SleepId   INT      NOT NULL
    ,	StudentId INT      NOT NULL
    ,	Date      DATETIME NOT NULL
    ,	NapStart  DATETIME NOT NULL
    ,	NapEnd    DATETIME NOT NULL
)
GO

