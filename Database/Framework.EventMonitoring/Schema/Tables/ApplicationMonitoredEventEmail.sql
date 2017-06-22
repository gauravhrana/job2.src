IF OBJECT_ID ('dbo.ApplicationMonitoredEventEmail') IS NOT NULL
	DROP TABLE dbo.ApplicationMonitoredEventEmail
GO

CREATE TABLE dbo.ApplicationMonitoredEventEmail 
(
		ApplicationMonitoredEventEmailId		INT			NOT NULL	
	,	ApplicationId							INT			NOT NULL    
	,	ApplicationMonitoredEventSourceId		INT			NOT NULL
	,	UserId									INT			NOT NULL
	,	CorrespondenceLevel						VARCHAR(20) NULL
	,	Active									BIT			NULL
);

