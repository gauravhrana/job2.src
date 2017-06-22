IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DiaperStatusUpdate')
BEGIN
	PRINT 'Dropping Procedure DiaperStatusUpdate'
	DROP  Procedure  DiaperStatusUpdate
END
GO

PRINT 'Creating Procedure DiaperStatusUpdate'

GO

/******************************************************************************
**		File: 
**		Name: DiaperStatusUpdate
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
**		----------						-----------
**
**		Auth:
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.DiaperStatusUpdate
(       
		@DiaperStatusId		INT		
	,	@Name				VARCHAR(50)
	,	@Description		VARCHAR(500)	= NULL
	,	@SortOrder			INT				= 1
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME		= NULL 
	,	@SystemEntityType	VARCHAR(50)		= 'DiaperStatus'
)
AS
BEGIN
	UPDATE	dbo.DiaperStatus
	SET		Name					    = @Name			   
		,	Description                 = @Description     
		,	SortOrder					= @SortOrder
	WHERE	DiaperStatusId		     	= @DiaperStatusId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @DiaperStatusId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END
GO

