IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FeatureOwnerStatusClone')
BEGIN
	PRINT 'Dropping Procedure FeatureOwnerStatusClone'
	DROP  Procedure FeatureOwnerStatusClone
END
GO

PRINT 'Creating Procedure FeatureOwnerStatusClone'
GO

/*********************************************************************************************
**		File: 
**		Name: FeatureOwnerStatusClone
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

CREATE Procedure dbo.FeatureOwnerStatusClone
(
		@FeatureOwnerStatusId		INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(50)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'FeatureOwnerStatus'
)
AS
BEGIN

	IF @FeatureOwnerStatusId IS NULL OR @FeatureOwnerStatusId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @FeatureOwnerStatusId OUTPUT
	END						
	
	SELECT	@ApplicationId		=	ApplicationId
		,	@Description		=	Description
		,	@SortOrder			=	SortOrder				
	FROM	dbo.FeatureOwnerStatus
	WHERE   FeatureOwnerStatusId		=	@FeatureOwnerStatusId
	ORDER BY FeatureOwnerStatusId

	EXEC dbo.FeatureOwnerStatusInsert 
			@FeatureOwnerStatusId		=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@Name					=	@Name
		,	@Description			=	@Description
		,	@SortOrder				=	@SortOrder
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'FeatureOwnerStatus'
		,	@EntityKey				= @FeatureOwnerStatusId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
