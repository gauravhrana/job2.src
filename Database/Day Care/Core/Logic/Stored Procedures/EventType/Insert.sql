IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'EventTypeInsert')
BEGIN
	PRINT 'Dropping Procedure EventTypeInsert'
	DROP  Procedure  EventTypeInsert
END
GO

PRINT 'Creating Procedure EventTypeInsert'
GO

/******************************************************************************
**		File: 
**		Name: EventTypeInsert
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
CREATE Procedure dbo.EventTypeInsert
(
		@EventTypeId			 INT				= NULL 	OUTPUT		
	,	@ApplicationId			 INT				
	,	@Name					 VARCHAR(50)
	,	@Description			 VARCHAR(500)		= NULL
	,	@SortOrder				 INT				= 1
	,   @AuditId			     INT			
    ,   @AuditDate				 DATETIME		    = NULL
	,	@SystemEntityType		 VARCHAR(50)			= 'EventType'	
)
AS
BEGIN
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @EventTypeId OUTPUT
		
	INSERT INTO dbo.EventType
	(
			EventTypeId
		,	ApplicationId
		,	Name
		,	Description
		,	SortOrder
	)
	VALUES
	(
			@EventTypeId
		,	@ApplicationId
		,	@Name
		,	@Description
		,	@SortOrder
		
	)

--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @EventTypeId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	
END
GO