IF OBJECT_Id ('dbo.AccidentPlace') IS NOT NULL
	DROP TABLE dbo.AccidentPlace
GO
	
CREATE TABLE dbo.AccidentPlace 
(
		AccidentPlaceId INT           NOT NULL	
	,	ApplicationId   INT           NOT NULL		
	,	Name            VARCHAR (50)  NOT NULL	
	,	Description     VARCHAR (500) NOT NULL	
	,	SortOrder       INT           NOT NULL
)
GO

