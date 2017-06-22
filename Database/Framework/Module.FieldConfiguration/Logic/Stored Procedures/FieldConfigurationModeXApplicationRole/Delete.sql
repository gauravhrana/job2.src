IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeXApplicationRoleDelete')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeXApplicationRoleDelete'
	DROP  Procedure  FieldConfigurationModeXApplicationRoleDelete
END
GO

PRINT 'Creating Procedure FieldConfigurationModeXApplicationRoleDelete'
GO

/******************************************************************************
**		File: 
**		Name: FieldConfigurationModeXApplicationRoleDelete
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
CREATE Procedure dbo.FieldConfigurationModeXApplicationRoleDelete
(
		@FieldConfigurationModeXApplicationRoleId	INT			= NULL	
	,	@FieldConfigurationModeId					INT			= NULL	
	,	@ApplicationRoleId							INT			= NULL	
	,	@FieldConfigurationModeAccessModeId			INT			= NULL
	,	@AuditId									INT					
	,	@AuditDate									DATETIME	= NULL
	,	@SystemEntityType							VARCHAR(50)	= 'FieldConfigurationModeXApplicationRole'
)
AS
BEGIN

	DELETE	dbo.FieldConfigurationModeXApplicationRole
	WHERE	FieldConfigurationModeXApplicationRoleId	=	ISNULL(@FieldConfigurationModeXApplicationRoleId, FieldConfigurationModeXApplicationRoleId)
	AND		FieldConfigurationModeId					=	ISNULL(@FieldConfigurationModeId, FieldConfigurationModeId)
	AND		ApplicationRoleId							=	ISNULL(@ApplicationRoleId, ApplicationRoleId)
	AND		FieldConfigurationModeAccessModeId			=	ISNULL(@FieldConfigurationModeAccessModeId, FieldConfigurationModeAccessModeId)
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FieldConfigurationModeXApplicationRoleId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END

GO


