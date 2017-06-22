IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NeedsList')
BEGIN
	PRINT 'Dropping Procedure NeedsList'
	DROP PROCEDURE NeedsList
END
GO

PRINT 'Creating Procedure NeedsList'
GO

/******************************************************************************
**		File: 
**		Name: NeedsList
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

CREATE Procedure dbo.NeedsList
(
		@AuditId				INT	
	,	@ApplicationId			INT			= NULL	
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Needs'
)
AS
BEGIN
		SELECT	a.NeedsId
			,	a.ApplicationId		
			,	a.RequestDate			
			,	a.ReceivedDate		
			,	a.NeedItemId
			,	a.NeedItemStatus
			,	a.NeedItemBy	
		FROM   dbo.Needs a
		WHERE  ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
		ORDER BY NeedsId	ASC

	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO

