IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleasePublishCategoryClone')
BEGIN
	PRINT 'Dropping Procedure ReleasePublishCategoryClone'
	DROP  Procedure ReleasePublishCategoryClone
END
GO

PRINT 'Creating Procedure ReleasePublishCategoryClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ReleasePublishCategoryClone
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
**     ----------						-----------
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
CREATE Procedure dbo.ReleasePublishCategoryClone
(
		@ReleasePublishCategoryId		        INT			= NULL 	OUTPUT	
	,	@ApplicationId			INT			
	,	@Name					VARCHAR(50)						
	,	@Description            VARCHAR (500)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ReleasePublishCategory'
)
AS
BEGIN
		IF @ReleasePublishCategoryId IS NULL OR @ReleasePublishCategoryId = -999999
		BEGIN
			EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ReleasePublishCategoryId OUTPUT
		END						
	
		SELECT	@ApplicationId	= ApplicationId
			,	@Description	= Description
			,	@SortOrder		= SortOrder				
		FROM	dbo.ReleasePublishCategory
		WHERE   ReleasePublishCategoryId		= @ReleasePublishCategoryId

		EXEC dbo.ReleasePublishCategoryInsert 
			@ReleasePublishCategoryId		=	NULL
		,	@ApplicationId					=	@ApplicationId
		,	@Name							=	@Name
		,	@Description					=	@Description
		,	@SortOrder						=	@SortOrder
		,	@AuditId						=	@AuditId

		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleasePublishCategory'
		,	@EntityKey				= @ReleasePublishCategoryId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

	END	
GO
