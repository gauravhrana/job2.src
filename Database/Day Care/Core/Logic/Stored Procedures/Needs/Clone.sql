IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NeedsClone')
BEGIN
	PRINT 'Dropping Procedure NeedsClone'
	DROP  Procedure NeedsClone
END
GO

PRINT 'Creating Procedure NeedsClone'
GO

/*********************************************************************************************
**		File: 
**		Name: NeedsClone
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
**		
**********************************************************************************************/

CREATE Procedure dbo.NeedsClone
(
		@NeedsId				INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT							
	,	@StudentId				INT									
	,	@RequestDate			DATETIME							
	,	@ReceivedDate			DATETIME						   
	,	@NeedItemId				INT									
	,	@NeedItemStatus			VARCHAR(50)							
	,	@NeedItemBy				DATETIME							
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Needs'				
)

AS

BEGIN

	IF @NeedsId IS NULL OR @NeedsId = -9999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'Needs', @NeedsId OUTPUT
	END	
		
	
	SELECT	@StudentId		=	StudentId
		,	@ApplicationId  =	ApplicationId
        ,	@RequestDate	=	RequestDate
		,	@ReceivedDate	=	ReceivedDate
		,	@NeedItemId		=	NeedItemId
		,	@NeedItemStatus	=	NeedItemStatus
		,   @NeedItemBy		=	NeedItemBy					
	FROM	dbo.Needs
	WHERE	NeedsId			= @NeedsId  
	AND		ApplicationId	= @ApplicationId

	EXEC dbo.NeedsInsert 
			@NeedsId		=	NULL
		,	@ApplicationId  =	ApplicationId
		,	@StudentId		=	@StudentId
        ,	@RequestDate	=	@RequestDate
		,	@ReceivedDate	=	@ReceivedDate
		,	@NeedItemId		=	@NeedItemId
		,	@NeedItemStatus	=	@NeedItemStatus
		,   @NeedItemBy		=	@NeedItemBy
		,	@AuditId		=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'Needs'	
		,	@EntityKey				= @NeedsId
		,	@AuditAction			= 'Clone' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
