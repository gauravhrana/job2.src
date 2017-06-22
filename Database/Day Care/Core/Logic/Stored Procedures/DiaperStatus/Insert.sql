IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DiaperStatusInsert')
BEGIN
	PRINT 'Dropping Procedure DiaperStatusInsert'
	DROP PROCEDURE DiaperStatusInsert
END
GO

PRINT 'Creating Procedure DiaperStatusInsert'
GO

/******************************************************************************
**		File: 
**		Name: DiaperStatusInsert
**		Desc: 
**
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**		----------						-----------
**
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:			Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE dbo.DiaperStatusInsert
(
		@DiaperStatusId 	INT			= NULL 	OUTPUT
	,	@ApplicationId		INT			
	,	@Name				VARCHAR(50)
	,	@Description		VARCHAR(500)	
	,	@SortOrder			INT	
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME	= NULL
	,	@SystemEntityType	VARCHAR(50)	= 'DiaperStatus'
)
AS
BEGIN
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @DiaperStatusId OUTPUT
		
	INSERT INTO dbo.DiaperStatus
	(
			DiaperStatusId
		,	ApplicationId
		,	Name
		,	Description
		,	SortOrder
	)
	VALUES
	(
			@DiaperStatusId
		,	@ApplicationId
		,	@Name
		,	@Description
		,	@SortOrder
	)
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @DiaperStatusId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
