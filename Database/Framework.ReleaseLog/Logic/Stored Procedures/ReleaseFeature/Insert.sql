IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseFeatureInsert')
BEGIN
	PRINT 'Dropping Procedure ReleaseFeatureInsert'
	DROP  Procedure ReleaseFeatureInsert
END
GO

PRINT 'Creating Procedure ReleaseFeatureInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ReleaseFeatureInsert
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
**********************************************************************************************/
CREATE Procedure dbo.ReleaseFeatureInsert
(
		@ReleaseFeatureId				INT				= NULL 	OUTPUT
	,	@ApplicationId					INT				= NULL		
	,	@Name							VARCHAR(50)						
	,	@Description					VARCHAR (500)					
	,	@SortOrder						INT									
	,	@AuditId						INT									
	,	@AuditDate						DATETIME		= NULL				
	,	@SystemEntityType				VARCHAR(50)		= 'ReleaseFeature'
)
AS
BEGIN
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ReleaseFeatureId OUTPUT, @AuditId

	DECLARE		@DateCreated		AS		DATETIME
	DECLARE		@DateModified		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT	
	

	SET @DateCreated		= GETDATE() 
	SET @DateModified		= @DateCreated
	SET @CreatedByAuditId	= @AuditId
	SET @ModifiedByAuditId	= @AuditId

	
	INSERT INTO dbo.ReleaseFeature 
	( 
			ReleaseFeatureId
		,	ApplicationId							
		,	Name				
		,	Description			
		,	SortOrder	
		,	DateCreated
		,	DateModified
		,	CreatedByAuditId
		,	ModifiedByAuditId
			
	)
	VALUES 
	(  
			@ReleaseFeatureId
		,	@ApplicationId			
		,	@Name				
		,	@Description
		,	@SortOrder	
		,	@DateCreated
		,	@DateModified
		,	@CreatedByAuditId
		,	@ModifiedByAuditId
	)

	SELECT @ReleaseFeatureId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseFeature'
		,	@EntityKey				= @ReleaseFeatureId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 