

CREATE TRIGGER DatabaseChangeLogTrigger ON DATABASE

FOR DDL_DATABASE_LEVEL_EVENTS

AS

	SET NOCOUNT ON

	DECLARE @EventType NVARCHAR(MAX) 
	DECLARE @SchemaName NVARCHAR(MAX) 
	DECLARE @ObjectName NVARCHAR(MAX) 
	DECLARE @ObjectType NVARCHAR(MAX)
	DECLARE @DBName VARCHAR(100)
	DECLARE @Message VARCHAR(1000)
	DECLARE @TSQL NVARCHAR(MAX)

	SELECT	@EventType	=	EVENTDATA().value('(/EVENT_INSTANCE/EventType)[1]','NVARCHAR(MAX)')
		,	@SchemaName =	EVENTDATA().value('(/EVENT_INSTANCE/SchemaName)[1]','NVARCHAR(MAX)')
		,	@ObjectName =	EVENTDATA().value('(/EVENT_INSTANCE/ObjectName)[1]','NVARCHAR(MAX)')
		,	@ObjectType =	EVENTDATA().value('(/EVENT_INSTANCE/ObjectType)[1]','NVARCHAR(MAX)')
		,	@DBName		=	EVENTDATA().value('(/EVENT_INSTANCE/DatabaseName)[1]','NVARCHAR(MAX)')
		,	@TSQL		=	EVENTDATA().value('(/EVENT_INSTANCE/TSQLCommand/CommandText)[1]','NVARCHAR(MAX)')

	IF @SchemaName = ' '
	BEGIN

		SELECT	@SchemaName = default_schema_name
		FROM	sys.sysusers SysUser
		INNER JOIN	sys.database_principals Pri
			ON	SysUser.uid = Pri.principal_id
		WHERE	SysUser.name = CURRENT_USER

	END

	INSERT INTO LoggingAndTrace.dbo.DatabaseChangeLog
	(	
			DataBaseName
		,	SchemaName	
		,	ObjectName	
		,	ObjectType	
		,	EventType	
		,	RecordDate	
		,	SystemUser	
		,	CurrentUser	
		,	OriginalUser
		,	CommandText	
		,	EventData		
	)

	SELECT	@DBName
		,	@SchemaName
		,	@ObjectName
		,	@ObjectType
		,	@EventType
		,	GETDATE()
		,	SUSER_SNAME()
		,	CURRENT_USER
		,	ORIGINAL_LOGIN()
		,	@TSQL
		,	EVENTDATA()

GO

ENABLE TRIGGER DatabaseChangeLogTrigger ON DATABASE

GO