IF OBJECT_ID ('dbo.ApplicationRoute') IS NOT NULL
	DROP TABLE dbo.ApplicationRoute
GO

CREATE TABLE dbo.ApplicationRoute 
(
		ApplicationRouteId				INT				NOT NULL
	,	ApplicationId					INT				NOT NULL
	,	RouteName						VARCHAR (100)	NOT NULL		
	,	EntityName						VARCHAR (100)	NULL		
	,	ProposedRoute					VARCHAR (100)	NOT NULL	
	,	RelativeRoute					VARCHAR (200)	NOT NULL	
	,	Description						VARCHAR (200)	NOT NULL	
);


    