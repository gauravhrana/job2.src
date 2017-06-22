IF OBJECT_ID ('dbo.ApplicationUser') IS NOT NULL
	DROP TABLE dbo.ApplicationUser
GO


CREATE TABLE dbo.ApplicationUser 
(
    ApplicationUserId  INT          NOT NULL,
    FirstName VARCHAR (50) NOT NULL,
    LastName  VARCHAR (50) NOT NULL,
	MiddleName VARCHAR(50) NOT NULL,
);

