IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ActivityClone')
BEGIN
	PRINT 'Dropping Procedure ActivityClone'
	DROP  Procedure ActivityClone
END
GO

PRINT 'Creating Procedure ActivityClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ActivityClone
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

CREATE Procedure dbo.ActivityClone
(
		@ActivityId				INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT	
	,	@StudentId				INT									
	,	@ActivityTypeId			INT									
	,	@ActivitySubTypeId		INT									
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50) = 'Activity'				
)

AS

BEGIN

	IF @ActivityId IS NULL OR @ActivityId = -9999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ActivityId OUTPUT	
	END	
		
	
	SELECT	@ApplicationId		=	ApplicationId
		,	@StudentId			=	StudentId					
		,	@ActivityTypeId		=	ActivityTypeId	    
		,	@ActivitySubTypeId	=	ActivitySubTypeId				
	FROM	dbo.Activity
	WHERE	ActivityId		= @ActivityId 
	AND		ApplicationId	= @ApplicationId

	EXEC dbo.ActivityInsert 
			@ActivityId			=	NULL				
		,	@ApplicationId		=	@ApplicationId
		,	@StudentId			=	@StudentId					
		,	@ActivityTypeId		=	@ActivityTypeId	    
		,	@ActivitySubTypeId	=	@ActivitySubTypeId		
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ActivityId
		,	@AuditAction			= 'Clone' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
