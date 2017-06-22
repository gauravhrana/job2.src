IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeXApplicationUserDelete')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeXApplicationUserDelete'
	DROP  Procedure  FieldConfigurationModeXApplicationUserDelete
END
GO

PRINT 'Creating Procedure FieldConfigurationModeXApplicationUserDelete'
GO

/******************************************************************************
**		File: 
**		Name: FieldConfigurationModeXApplicationUserDelete
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
CREATE Procedure dbo.FieldConfigurationModeXApplicationUserDelete
(
		@FieldConfigurationModeXApplicationUserId	INT			= NULL		
	,	@FieldConfigurationModeId					INT			= NULL	
	,	@ApplicationUserId							INT			= NULL	
	,	@FieldConfigurationModeAccessModeId			INT			= NULL	
	,	@AuditId									INT					
	,	@AuditDate									DATETIME	= NULL
	,	@SystemEntityType							VARCHAR(50)	= 'FieldConfigurationModeXApplicationUser'
)
AS
BEGIN

	DELETE	dbo.FieldConfigurationModeXApplicationUser
	WHERE	FieldConfigurationModeXApplicationUserId		=	ISNULL(@FieldConfigurationModeXApplicationUserId,	FieldConfigurationModeXApplicationUserId)	
	AND		FieldConfigurationModeId						=   ISNULL(@FieldConfigurationModeId,	FieldConfigurationModeId)
	AND		ApplicationUserId								=	ISNULL(@ApplicationUserId,			ApplicationUserId)
	AND		FieldConfigurationModeAccessModeId				=	ISNULL(@FieldConfigurationModeAccessModeId, FieldConfigurationModeAccessModeId)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FieldConfigurationModeXApplicationUserId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END

GO


