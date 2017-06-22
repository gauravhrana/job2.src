IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeXApplicationUserUpdate')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeXApplicationUserUpdate'
	DROP  Procedure  FieldConfigurationModeXApplicationUserUpdate
END
GO

PRINT 'Creating Procedure FieldConfigurationModeXApplicationUserUpdate'
GO

/******************************************************************************
**		File: 
**		Name: FieldConfigurationModeXApplicationUserUpdate
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

CREATE PROCEDURE dbo.FieldConfigurationModeXApplicationUserUpdate
(
		@FieldConfigurationModeXApplicationUserId	INT		
	,	@FieldConfigurationModeId					INT								
	,	@ApplicationUserId							INT	
	,	@FieldConfigurationModeAccessModeId			INT							
	,	@AuditId									INT				
	,	@AuditDate									DATETIME	= NULL	
	,	@SystemEntityType							VARCHAR(50)	= 'FieldConfigurationModeXApplicationUser'
)
AS
BEGIN 

	UPDATE	dbo.FieldConfigurationModeXApplicationUser 
	SET		FieldConfigurationModeId					=	@FieldConfigurationModeId		
		,	ApplicationUserId							=	@ApplicationUserId	
		,	FieldConfigurationModeAccessModeId			=	@FieldConfigurationModeAccessModeId					
	WHERE	FieldConfigurationModeXApplicationUserId	=	@FieldConfigurationModeXApplicationUserId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @FieldConfigurationModeXApplicationUserId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		

GO

