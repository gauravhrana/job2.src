IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NeedsUpdate')
BEGIN
	PRINT 'Dropping Procedure NeedsUpdate'
	DROP  Procedure  NeedsUpdate
END
GO

PRINT 'Creating Procedure NeedsUpdate'

GO

/******************************************************************************
**		File: 
**		Name: NeedsUpdate
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

CREATE Procedure dbo.NeedsUpdate
(     
		@NeedsId		    INT		
	,	@StudentId			INT
	,	@RequestDate		DATETIME
	,   @ReceivedDate		DATETIME
	,	@NeedItemId		    INT
	,	@NeedItemStatus		VARCHAR(50)
	,	@NeedItemBy			DATETIME
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME		= NULL
	,	@SystemEntityType	VARCHAR(50)	    = 'Needs'
	 
)
AS
 BEGIN
	UPDATE	 dbo.Needs
	SET		 NeedsId		=	@NeedsId		
        ,    StudentId		=	@StudentId
        ,	 RequestDate	=	@RequestDate
		,	 ReceivedDate	=	@ReceivedDate
		,	 NeedItemId		=	@NeedItemId
		,	 NeedItemStatus =   @NeedItemStatus
		,    NeedItemBy		=	@NeedItemBy	
	WHERE	 NeedsId		=   @NeedsId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NeedsId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId		
 END
GO

