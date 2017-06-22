IF OBJECT_ID ('dbo.ApplicationMonitoredEventSource') IS NOT NULL
	DROP TABLE dbo.ApplicationMonitoredEventSource
GO

CREATE TABLE dbo.ApplicationMonitoredEventSource 
(
		ApplicationMonitoredEventSourceId		INT				NOT NULL	
	,	ApplicationId							INT				NOT NULL
    ,	Code									VARCHAR (20)	NOT NULL	
    ,	Description								VARCHAR (50)	NOT NULL
);

