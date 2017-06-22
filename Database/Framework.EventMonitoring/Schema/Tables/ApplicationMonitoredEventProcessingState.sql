IF OBJECT_ID ('dbo.ApplicationMonitoredEventProcessingState') IS NOT NULL
	DROP TABLE dbo.ApplicationMonitoredEventProcessingState
GO

CREATE TABLE dbo.ApplicationMonitoredEventProcessingState 
(
		ApplicationMonitoredEventProcessingStateId		INT				NOT NULL	
	,	ApplicationId									INT				NOT NULL
    ,	Code											VARCHAR (20)	NOT NULL	
    ,	Description										VARCHAR (50)	NOT NULL
);

