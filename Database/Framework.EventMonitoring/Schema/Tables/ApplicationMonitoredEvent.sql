IF OBJECT_ID ('dbo.ApplicationMonitoredEvent') IS NOT NULL
	DROP TABLE dbo.ApplicationMonitoredEvent
GO

CREATE TABLE dbo.ApplicationMonitoredEvent 
(
		ApplicationMonitoredEventId					INT				NOT NULL	
	,	ApplicationId								INT				NOT NULL    
	,	ApplicationMonitoredEventSourceId			INT				NOT NULL
	,	ApplicationMonitoredEventProcessingStateId	INT				NOT NULL
	,	ReferenceId									INT				NULL
	,	ReferenceCode								VARCHAR(50)		NULL
	,	Category									VARCHAR (50)	NOT NULL
	,	Message										VARCHAR (500)	NOT NULL
	,	IsDuplicate									BIT				NOT NULL
	,	LastModifiedBy								VARCHAR (50)	NOT NULL
	,	LastModifiedOn								DATETIME		NOT NULL
);