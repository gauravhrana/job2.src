IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NeedsDetails')
BEGIN
	PRINT 'Dropping Procedure NeedsDetails'
	DROP  Procedure NeedsDetails
END
GO

PRINT 'Creating Procedure NeedsDetails'
GO


/******************************************************************************
**		File: 
**		Name: NeedsDetails
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.NeedsDetails
(
		@NeedsId		    INT
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME	= NULL	
	,   @SystemEntityType	VARCHAR(50)	= 'Needs'	
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@NeedsId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	NeedsId
		,	ApplicationId 
        ,   StudentId
        ,	RequestDate
		,	ReceivedDate
		,	NeedItemId
		,	NeedItemStatus
		,   NeedItemBy	
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'								
	FROM	Needs 
	WHERE	NeedsId		  = @NeedsId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= 'Needs'
		,	@EntityKey				= @NeedsId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END		
GO
   