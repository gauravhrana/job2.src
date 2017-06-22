IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeAccessModeClone')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeAccessModeClone'
	DROP  Procedure FieldConfigurationModeAccessModeClone
END
GO

PRINT 'Creating Procedure FieldConfigurationModeAccessModeClone'
GO

/*********************************************************************************************
**		File: 
**		Name: FieldConfigurationModeAccessModeClone
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
CREATE Procedure dbo.FieldConfigurationModeAccessModeClone
(
		@FieldConfigurationModeAccessModeId		INT				= NULL 	OUTPUT	
	,	@ApplicationId							INT				= NULL
	,	@Name									VARCHAR(100)						
	,	@Description							VARCHAR (500)						
	,	@SortOrder								INT									
	,	@AuditId								INT									
	,	@AuditDate								DATETIME		= NULL			
	,	@SystemEntityType						VARCHAR(50)		= 'FieldConfigurationModeAccessMode'
)
AS
BEGIN

	IF @FieldConfigurationModeAccessModeId IS NULL OR @FieldConfigurationModeAccessModeId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @FieldConfigurationModeAccessModeId OUTPUT
	END			
	
	SELECT	@ApplicationId	= ApplicationId
		,	@Description	= Description
		,	@SortOrder		= SortOrder		
	FROM	dbo.FieldConfigurationModeAccessMode
	WHERE	FieldConfigurationModeAccessModeId		= @FieldConfigurationModeAccessModeId

	EXEC dbo.FieldConfigurationModeAccessModeInsert 
			@FieldConfigurationModeAccessModeId		=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@Name				=	@Name
		,	@Description		=	@Description
		,	@SortOrder			=	@SortOrder
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FieldConfigurationModeAccessModeId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END
GO