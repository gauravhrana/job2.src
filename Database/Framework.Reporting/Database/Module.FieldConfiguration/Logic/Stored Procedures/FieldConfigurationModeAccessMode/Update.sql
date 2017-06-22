IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeAccessModeUpdate')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeAccessModeUpdate'
	DROP  Procedure  FieldConfigurationModeAccessModeUpdate
END
GO

PRINT 'Creating Procedure FieldConfigurationModeAccessModeUpdate'
GO

/******************************************************************************
**		File: 
**		Name: FieldConfigurationModeAccessModeUpdate
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
CREATE Procedure dbo.FieldConfigurationModeAccessModeUpdate
(
		@FieldConfigurationModeAccessModeId		INT		
	,	@ApplicationId							INT			 			
	,	@Name									VARCHAR(100)				
	,	@Description							VARCHAR (500)			
	,	@SortOrder								INT					
	,	@AuditId								INT					
	,	@AuditDate								DATETIME		= NULL	
	,	@SystemEntityType						VARCHAR(50)		= 'FieldConfigurationModeAccessMode'
)
AS
BEGIN 

	UPDATE	dbo.FieldConfigurationModeAccessMode 
	SET		Name			=	@Name	
		,	ApplicationId	=	@ApplicationId		
		,	Description		=	@Description				
		,	SortOrder		=	@SortOrder					
	WHERE	FieldConfigurationModeAccessModeId		=	@FieldConfigurationModeAccessModeId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FieldConfigurationModeAccessModeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

 END
GO
