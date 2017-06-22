IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeCategoryUpdate')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeCategoryUpdate'
	DROP  Procedure  FieldConfigurationModeCategoryUpdate
END
GO

PRINT 'Creating Procedure FieldConfigurationModeCategoryUpdate'
GO

/******************************************************************************
**		File: 
**		Name: FieldConfigurationModeCategoryUpdate
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.FieldConfigurationModeCategoryUpdate
(
		@FieldConfigurationModeCategoryId			INT		
	,	@ApplicationId							INT			 			
	,	@Name									VARCHAR(100)				
	,	@Description							VARCHAR (500)			
	,	@SortOrder								INT					
	,	@AuditId								INT					
	,	@AuditDate								DATETIME		= NULL	
	,	@SystemEntityType						VARCHAR(50)		= 'FieldConfigurationModeCategory'
)
AS
BEGIN 
	UPDATE	dbo.FieldConfigurationModeCategory 
	SET		Name			=	@Name	
		,	ApplicationId	=	@ApplicationId		
		,	Description		=	@Description				
		,	SortOrder		=	@SortOrder					
	WHERE	FieldConfigurationModeCategoryId		=	@FieldConfigurationModeCategoryId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'FieldConfigurationModeCategory'
		,	@EntityKey				= @FieldConfigurationModeCategoryId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO
