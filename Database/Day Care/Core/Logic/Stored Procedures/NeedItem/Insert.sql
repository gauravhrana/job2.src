IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NeedItemInsert')
BEGIN
	PRINT 'Dropping ProcedureNeedItemInsert'
	DROP  Procedure NeedItemInsert
END
GO

PRINT 'Creating ProcedureNeedItemInsert'
GO

/******************************************************************************
**		File: 
**		Name: pNeedItemInsert
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
CREATE Procedure dbo.NeedItemInsert
(
		@NeedItemId				 INT				= NULL 	OUTPUT		
	,	@ApplicationId			 INT				
	,	@Name					 VARCHAR(50)
	,	@Description			 VARCHAR(500)		= NULL
	,	@SortOrder				 INT				= 1
	,   @AuditId			     INT			
    ,   @AuditDate				 DATETIME		    = NULL
	,	@SystemEntityType		 VARCHAR(50)		= 'NeedItem'	
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @NeedItemId OUTPUT
		
	INSERT INTO dbo.NeedItem
	(
			NeedItemId
		,	ApplicationId
		,	Name
		,	Description
		,	SortOrder
	)
	VALUES
	(
			@NeedItemId
		,	@ApplicationId
		,	@Name
		,	@Description
		,	@SortOrder
		
	)

--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @NeedItemId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	
END
GO