IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DiaperStatusClone')
BEGIN
	PRINT 'Dropping Procedure DiaperStatusClone'
	DROP  Procedure DiaperStatusClone
END
GO

PRINT 'Creating Procedure DiaperStatusClone'
GO

/*********************************************************************************************
**		File: 
**		Name: DiaperStatusClone
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

CREATE Procedure dbo.DiaperStatusClone
(
		@DiaperStatusId			INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT			
	,	@Name					VARCHAR(50)							
	,	@Description            VARCHAR (500)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'DiaperStatus'				
)

AS

BEGIN

	IF @DiaperStatusId IS NULL OR @DiaperStatusId = -9999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @DiaperStatusId OUTPUT
	END	
		
	
	SELECT	@ApplicationId		= ApplicationId
		,	@Name				= Name
		,	@Description		= Description
		,	@SortOrder			= SortOrder				
	FROM	dbo.DiaperStatus
	WHERE	DiaperStatusId	= @DiaperStatusId 
	AND		ApplicationId	= @ApplicationId

	EXEC dbo.DiaperStatusInsert 
			@DiaperStatusId		=	NULL
		,	@ApplicationId		=   @ApplicationId
		,	@Name				=	@Name
		,	@Description		=	@Description
		,	@SortOrder			=	@SortOrder
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @DiaperStatusId
		,	@AuditAction			= 'Clone' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
