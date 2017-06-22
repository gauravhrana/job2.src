IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeUpdate')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeUpdate'
	DROP  Procedure  FieldConfigurationModeUpdate
END
GO

PRINT 'Creating Procedure FieldConfigurationModeUpdate'
GO

/******************************************************************************
**		File: 
**		Name: FieldConfigurationModeUpdate
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
CREATE Procedure dbo.FieldConfigurationModeUpdate
(
		@FieldConfigurationModeId			INT		
	,	@ApplicationId							INT			 			
	,	@Name									VARCHAR(100)				
	,	@Description							VARCHAR (500)			
	,	@SortOrder								INT					
	,	@AuditId								INT					
	,	@AuditDate								DATETIME		= NULL	
	,	@SystemEntityType						VARCHAR(50)		= 'FieldConfigurationMode'
)
AS
BEGIN 
	UPDATE	dbo.FieldConfigurationMode 
	SET		Name			=	@Name	
		,	ApplicationId	=	@ApplicationId		
		,	Description		=	@Description				
		,	SortOrder		=	@SortOrder					
	WHERE	FieldConfigurationModeId		=	@FieldConfigurationModeId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'FieldConfigurationMode'
		,	@EntityKey				= @FieldConfigurationModeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO
