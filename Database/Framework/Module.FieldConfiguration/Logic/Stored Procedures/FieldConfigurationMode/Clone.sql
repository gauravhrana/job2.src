IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeClone')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeClone'
	DROP  Procedure FieldConfigurationModeClone
END
GO

PRINT 'Creating Procedure FieldConfigurationModeClone'
GO

/*********************************************************************************************
**		File: 
**		Name: FieldConfigurationModeClone
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
CREATE Procedure dbo.FieldConfigurationModeClone
(
		@FieldConfigurationModeId		INT				= NULL 	OUTPUT	
	,	@ApplicationId						INT				= NULL
	,	@Name								VARCHAR(100)						
	,	@Description						VARCHAR (500)						
	,	@SortOrder							INT									
	,	@AuditId							INT									
	,	@AuditDate							DATETIME		= NULL			
	,	@SystemEntityType					VARCHAR(50)		= 'FieldConfigurationMode'
)
AS
BEGIN
	IF @FieldConfigurationModeId IS NULL OR @FieldConfigurationModeId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @FieldConfigurationModeId OUTPUT
	END			
	
	SELECT	@ApplicationId	= ApplicationId
		,	@Description	= Description
		,	@SortOrder		= SortOrder		
	FROM	dbo.FieldConfigurationMode
	WHERE	FieldConfigurationModeId		= @FieldConfigurationModeId

	EXEC dbo.FieldConfigurationModeInsert 
			@FieldConfigurationModeId		=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@Name				=	@Name
		,	@Description		=	@Description
		,	@SortOrder			=	@SortOrder
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'FieldConfigurationMode'
		,	@EntityKey				= @FieldConfigurationModeId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END
GO