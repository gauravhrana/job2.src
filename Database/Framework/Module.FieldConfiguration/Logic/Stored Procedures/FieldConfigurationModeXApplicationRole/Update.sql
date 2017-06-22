IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeXApplicationRoleUpdate')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeXApplicationRoleUpdate'
	DROP  Procedure  FieldConfigurationModeXApplicationRoleUpdate
END
GO

PRINT 'Creating Procedure FieldConfigurationModeXApplicationRoleUpdate'
GO

/******************************************************************************
**		File: 
**		Name: FieldConfigurationModeXApplicationRoleUpdate
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE PROCEDURE dbo.FieldConfigurationModeXApplicationRoleUpdate
(
		@FieldConfigurationModeXApplicationRoleId	INT		
	,	@FieldConfigurationModeId					INT								
	,	@ApplicationRoleId							INT
	,	@FieldConfigurationModeAccessModeId			INT		
	,	@ApplicationId								INT						
	,	@AuditId									INT				
	,	@AuditDate									DATETIME	= NULL	
	,	@SystemEntityType							VARCHAR(50)	= 'FieldConfigurationModeXApplicationRole'
)
AS
BEGIN 

	UPDATE	dbo.FieldConfigurationModeXApplicationRole 
	SET		FieldConfigurationModeId					=	@FieldConfigurationModeId		
		,	ApplicationRoleId							=	@ApplicationRoleId	
		,	FieldConfigurationModeAccessModeId			=	@FieldConfigurationModeAccessModeId	
		,	ApplicationId								=	@ApplicationId					
	WHERE	FieldConfigurationModeXApplicationRoleId	=	@FieldConfigurationModeXApplicationRoleId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @FieldConfigurationModeXApplicationRoleId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		

GO

