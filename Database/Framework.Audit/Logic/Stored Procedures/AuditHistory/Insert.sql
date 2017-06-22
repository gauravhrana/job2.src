IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'AuditHistoryInsert')
BEGIN
	PRINT 'Dropping Procedure AuditHistoryInsert'
	DROP  Procedure AuditHistoryInsert
END
GO

PRINT 'Creating Procedure AuditHistoryInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:AuditHistoryInsert
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------							-----------
**
**		Auth: 
**		Date: 
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**********************************************************************************************/
CREATE Procedure dbo.AuditHistoryInsert
(
		@AuditHistoryId			INT			= NULL		OUTPUT
	,	@SystemEntityType		VARCHAR(50) 
	,	@EntityKey				INT			
	,	@AuditAction			VARCHAR(50) 
	,	@CreatedDate			DATETIME	= NULL
	,	@TraceId				INT			= NULL
	,	@CreatedByPersonId		INT									
)
AS
BEGIN

	
	SET NOCOUNT ON

	IF	@TraceId	IS NULL
	BEGIN
		SET	@TraceId = -1
	END

	DECLARE	@AuditActionId	 INT

	-- Convert @AuditActionId from @AuditAction
	SELECT	@AuditActionId = AuditActionId 
	FROM	dbo.AuditAction
	WHERE	Name = @AuditAction

	-- if no date provided, assume current date
	SET @CreatedDate = ISNULL(@CreatedDate, GetDate())
	
	-- if null then set to -1 for key --> WHen searching for more than one
	SET @EntityKey = ISNULL(@EntityKey, -1)

	-- Convert @@SystemEntityTypeId from @@SystemEntityType
	DECLARE @SystemEntityTypeId AS INT
	EXEC	dbo.SystemEntityTypeGetId 
			@SystemEntityType	=	@SystemEntityType
		,	@SystemEntityTypeId =	@SystemEntityTypeId OUTPUT

	-- not going to do this for perfomance reasons
	-- table strcuture is using INDENTITY (SQL Server LEVEL)
	-- get next Id for insertion, if none provided
	--IF @AuditHistoryId IS NULL
	--BEGIN
	--	SELECT @AuditHistoryId = MAX(ISNULL(AuditHistoryId, 1)) + 1
	--	FROM   dbo.AuditHistory
	--END	
	
	INSERT INTO dbo.AuditHistory
	(
			SystemEntityId		
		,	EntityKey			
		,	AuditActionId		
		,	CreatedDate			
		,	CreatedByPersonId
		,	TraceId
	)
	VALUES 
	(
			@SystemEntityTypeId
		,	@EntityKey
		,	@AuditActionId
		,	@CreatedDate
		,	@CreatedByPersonId	
		,	@TraceId								
	)

END	
GO

 