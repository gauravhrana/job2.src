IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeAccessModeDelete')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeAccessModeDelete'
	DROP  Procedure FieldConfigurationModeAccessModeDelete
END
GO

PRINT 'Creating Procedure FieldConfigurationModeAccessModeDelete'
GO
/******************************************************************************
**		File: 
**		Name: FieldConfigurationModeAccessModeDelete
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
CREATE Procedure dbo.FieldConfigurationModeAccessModeDelete
(
			@FieldConfigurationModeAccessModeId 	INT						
		,	@AuditId								INT						
		,	@AuditDate								DATETIME	= NULL		
		,	@SystemEntityType						VARCHAR(50)	= 'FieldConfigurationModeAccessMode'
)
AS
BEGIN

	DELETE	 dbo.FieldConfigurationModeAccessMode
	WHERE	 FieldConfigurationModeAccessModeId = @FieldConfigurationModeAccessModeId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FieldConfigurationModeAccessModeId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO