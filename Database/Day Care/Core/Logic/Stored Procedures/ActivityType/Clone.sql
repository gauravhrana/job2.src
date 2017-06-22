IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ActivityTypeClone')
BEGIN
	PRINT 'Dropping Procedure ActivityTypeClone'
	DROP  Procedure ActivityTypeClone
END
GO

PRINT 'Creating Procedure ActivityTypeClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ActivityTypeClone
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

CREATE Procedure dbo.ActivityTypeClone
(
		@ActivityTypeId			INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT			
	,	@Name					VARCHAR(50)							
	,	@Description            VARCHAR(500)						
	,	@SortOrder				INT									
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'ActivityType'				
)

AS

BEGIN

	IF @ActivityTypeId IS NULL OR @ActivityTypeId = -9999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ActivityTypeId OUTPUT
	END	
		
	
	SELECT	@ApplicationId		= ApplicationId
		,	@Name				= Name
		,	@Description		= Description
		,	@SortOrder			= SortOrder				
	FROM	dbo.ActivityType
	WHERE	ActivityTypeId	= @ActivityTypeId 
	AND		ApplicationId   = @ApplicationId

	EXEC dbo.ActivityTypeInsert 
			@ActivityTypeId		=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@Name				=	@Name
		,	@Description		=	@Description
		,	@SortOrder			=	@SortOrder
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @ActivityTypeId
		,	@AuditAction			= 'Clone' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
