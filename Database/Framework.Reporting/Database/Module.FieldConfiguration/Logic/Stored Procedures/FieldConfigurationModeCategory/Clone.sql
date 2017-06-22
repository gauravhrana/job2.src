IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeCategoryClone')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeCategoryClone'
	DROP  Procedure FieldConfigurationModeCategoryClone
END
GO

PRINT 'Creating Procedure FieldConfigurationModeCategoryClone'
GO

/*********************************************************************************************
**		File: 
**		Name: FieldConfigurationModeCategoryClone
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
CREATE Procedure dbo.FieldConfigurationModeCategoryClone
(
		@FieldConfigurationModeCategoryId		INT				= NULL 	OUTPUT	
	,	@ApplicationId						INT				= NULL
	,	@Name								VARCHAR(100)						
	,	@Description						VARCHAR (500)						
	,	@SortOrder							INT									
	,	@AuditId							INT									
	,	@AuditDate							DATETIME		= NULL			
	,	@SystemEntityType					VARCHAR(50)		= 'FieldConfigurationModeCategory'
)
AS
BEGIN
	IF @FieldConfigurationModeCategoryId IS NULL OR @FieldConfigurationModeCategoryId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @FieldConfigurationModeCategoryId OUTPUT
	END			
	
	SELECT	@ApplicationId	= ApplicationId
		,	@Description	= Description
		,	@SortOrder		= SortOrder		
	FROM	dbo.FieldConfigurationModeCategory
	WHERE	FieldConfigurationModeCategoryId		= @FieldConfigurationModeCategoryId

	EXEC dbo.FieldConfigurationModeCategoryInsert 
			@FieldConfigurationModeCategoryId		=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@Name				=	@Name
		,	@Description		=	@Description
		,	@SortOrder			=	@SortOrder
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'FieldConfigurationModeCategory'
		,	@EntityKey				= @FieldConfigurationModeCategoryId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END
GO