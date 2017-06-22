IF OBJECT_ID ('dbo.ApplicationRouteParameter') IS NOT NULL
	DROP TABLE dbo.ApplicationRouteParameter
GO

CREATE TABLE dbo.ApplicationRouteParameter 
(
		ApplicationRouteParameterId				INT				NOT NULL
	,	ApplicationRouteId						INT	NOT NULL	
	,	ApplicationId					INT				NOT NULL	
	,	ParameterName						VARCHAR (100)	NULL		
	,	ParameterValue					VARCHAR (100)	NOT NULL	
		
);


   
    