IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ActivitySubTypeClone')
BEGIN
	PRINT 'Dropping Procedure ActivitySubTypeClone'
	DROP  Procedure ActivitySubTypeClone
END
GO

PRINT 'Creating Procedure ActivitySubTypeClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ActivitySubTypeClone
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

CREATE Procedure dbo.ActivitySubTypeClone
(
		@ActivitySubTypeId		INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT					
	,	@ActivityTypeId			INT							
	,	@Name					VARCHAR(50)							
	,	@Description            VARCHAR(500)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ActivitySubType'			
)

AS

BEGIN

	IF @ActivitySubTypeId IS NULL OR @ActivitySubTypeId = -9999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ActivitySubTypeId OUTPUT
	END	
		
	
	SELECT	@ApplicationId		= ApplicationId
		,	@ActivityTypeId		= ActivityTypeId
		,	@Name				= Name
		,	@Description		= Description
		,	@SortOrder			= SortOrder				
	FROM	dbo.ActivitySubType
	WHERE	ActivitySubTypeId	= @ActivitySubTypeId 
	AND		ApplicationId		= @ApplicationId

	EXEC dbo.ActivitySubTypeInsert 
			@ActivitySubTypeId	=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@ActivityTypeId		=	@ActivityTypeId
		,	@Name				=	@Name
		,	@Description		=	@Description
		,	@SortOrder			=	@SortOrder
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @ActivitySubTypeId
		,	@AuditAction			= 'Clone' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
